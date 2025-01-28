using System;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class Technician
    {
        [ScaffoldColumn(false)]
        public int Id
        {
            get;
            set;
        }

        public string Vehicle { get; set; }
        public string Status { get; set; }
        public string Speed { get; set; }

        public DateTime? Reported
        {
            get;
            set;
        }
    }
}
