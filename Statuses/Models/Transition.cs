using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuses.Models
{
    public class Transition
    {
        public string Name { get; set; }
        public string FromStatusName { get; set; }
        public string ToStatusName { get; set; }

        public Transition()
        {

        }

        public Transition(string name, string from, string to)
        {
            Name = name;
            FromStatusName = from;
            ToStatusName = to;
        }
    }
}