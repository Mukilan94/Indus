using System;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class TechnicianSRV
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
