using Statuses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Statuses.ViewModel
{
    public class StatusAndTransitionVM
    {
        public string StatusName { get; set; }
        public List<SelectListItem> StatusesToSelect { get; set; }
        public Dictionary<string, Status> Statuses { get; set; }
        public Dictionary<string, Transition> Transitions { get; set; }
    }

    //public class CreateEStatusAndTransitionVMValidator : AbstractValidator<StatusAndTransitionVM>
    //{
    //    public CreateEmployeeViewModelValidator()
    //    {
    //        RuleFor(m => m.FirstName)
    //             .NotEmpty()
    //             .WithMessage("First name required")
    //             .Length(1, 50)
    //             .WithMessage("First name must not be greater than 50 characters");

    //        RuleFor(m => m.LastName)
    //             .NotEmpty()
    //             .WithMessage("Last name required")
    //             .Length(1, 50)
    //             .WithMessage("Last name must not be greater than 50 characters");
    //    }
    //}
}