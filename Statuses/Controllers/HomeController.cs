using Statuses.Models;
using Statuses.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Statuses.Controllers
{
    public class HomeController : Controller
    {
        public static Dictionary<string, Status> StatusDictionary = new Dictionary<string, Status>();
        public static Dictionary<string, Transition> TransitionDictionary = new Dictionary<string, Transition>();
        public static string InitStatus;
        
        public ActionResult Index()
        {
            StatusAndTransitionVM model = new StatusAndTransitionVM();
            model.StatusesToSelect = StatusDictionary.Select(s => new SelectListItem { Value = s.Key, Text = s.Key }).ToList();
            model.Statuses = StatusDictionary;
            model.Transitions = TransitionDictionary;
            return View(model);
        }

        #region STATUS ACTIONS
        [HttpPost]
        public bool CreateStatus(string StatusName)
        {
            if (!StatusDictionary.ContainsKey(StatusName))
            {
                Status status = new Status(StatusName);
                StatusDictionary.Add(StatusName, status);
                return true;
            }
            return false;
        }

        [HttpPost]
        public bool DeleteStatus(string StatusName)
        {
            if (StatusDictionary.ContainsKey(StatusName))
            {
                //::TODO:: check if the deleted status is the init
                StatusDictionary.Remove(StatusName);
                RemoveTransitionWithDeletedStatus(StatusName);
                return true;
            }
            return false;
        }

        [HttpPost]
        public void UpdateSelectedStatusToInit(string SelectedStatus)
        {
            if (!string.IsNullOrEmpty(InitStatus))
                StatusDictionary[InitStatus].IsInit = false;
            InitStatus = SelectedStatus;
            StatusDictionary[SelectedStatus].IsInit = true;
        }

        public ActionResult ViewStatuses()
        {
            if (!string.IsNullOrEmpty(InitStatus))
            {
                UpdateDescriptionOfStatuses();
            }
            return PartialView("Statuses", StatusDictionary);
        }

        #endregion

        #region TRANSITION ACTIONS
        [HttpPost]
        public bool CreateTransition(string TransitionName, string FromStatus, string ToStatus)
        {
            if (IsValidTransition(TransitionName, FromStatus, ToStatus))
            {
                Transition transition = new Transition(TransitionName, FromStatus, ToStatus);
                TransitionDictionary.Add(TransitionName, transition);
                return true;
            }
            return false;
        }

        [HttpPost]
        public bool DeleteTransition(string TransitionName)
        {
            if (TransitionDictionary.ContainsKey(TransitionName))
            {
                TransitionDictionary.Remove(TransitionName);
                return true;
            }
            return false;
        }

        public ActionResult ViewTransitions()
        {
            return PartialView("Transitions", TransitionDictionary);
        }
        #endregion

        #region INTERNAL FUNCTIONS

        private void UpdateDescriptionOfStatuses()
        {
            foreach (var item in StatusDictionary)
            {
                item.Value.IsOrphan = CheckIfOrphanStatus(item.Key);
                item.Value.IsFinal = CheckIfFinalStaus(item.Key);
            }
        }

        private static void RemoveTransitionWithDeletedStatus(string StatusName)
        {
            List<string> KeysToRemove = new List<string>();
            foreach (var item in TransitionDictionary)
            {
                if (item.Value.FromStatusName == StatusName || item.Value.ToStatusName == StatusName)
                    KeysToRemove.Add(item.Key);
            }

            foreach (var item in KeysToRemove)
            {
                TransitionDictionary.Remove(item);
            }
        }

        private static bool IsValidTransition(string TransitionName, string FromStatus, string ToStatus)
        {
            return !TransitionDictionary.ContainsKey(TransitionName) && !string.IsNullOrEmpty(FromStatus) && !string.IsNullOrEmpty(ToStatus);
        }

        private bool CheckIfFinalStaus(string status)
        {
            foreach (var item in TransitionDictionary)
            {
                if (item.Value.FromStatusName == status)
                    return false;
            }
            return true;
        }

        private bool CheckIfOrphanStatus(string status)
        {
            if (status == InitStatus)
                return false;
            foreach (var item in TransitionDictionary)
            {
                if (item.Value.ToStatusName == status)
                    return CheckIfOrphanStatus(item.Value.FromStatusName);
            }
            return true;
        }
        #endregion

    }
}