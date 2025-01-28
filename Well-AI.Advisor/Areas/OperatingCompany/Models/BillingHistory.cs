using System;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class BillingHistory
    {
        [ScaffoldColumn(false)]
        public int Id
        {
            get;
            set;
        }

        public string Name { get; set; }
        public string Invoice { get; set; }

        public DateTime? BillDate
        {
            get;
            set;
        }
        public double Sum
        {
            get;
            set;
        }
    }
}
