using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuses.Models
{
    public class Status
    {
        public string Name { get; set; }
        public bool IsInit { get; set; }
        public bool IsOrphan { get; set; }
        public bool IsFinal { get; set; }

        public Status()
        {

        }
        public Status(string name)
        {
            Name = name;
        }
    }
}