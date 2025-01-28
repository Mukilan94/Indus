using System;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class PaymentMethod
    {
        [ScaffoldColumn(false)]
        public int PayMethodID
        {
            get;
            set;
        }

        public string Holder { get; set; }
        public string Number { get; set; }

        public DateTime? Expire
        {
            get;
            set;
        }
        public string System
        {
            get;
            set;
        }

        public bool Default
        {
            get;
            set;
        }
    }
}
