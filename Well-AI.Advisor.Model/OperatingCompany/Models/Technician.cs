using System;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
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
