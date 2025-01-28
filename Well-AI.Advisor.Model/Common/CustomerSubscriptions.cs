using System;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.Common
{
    public class CustomerSubscriptions
    {
        [Display(Name = "Name")]
        public string SubscriptionName { get; set; }
        public int PackageOrder { get; set; }
        [Display(Name = "Description")]
        public string SubscriptionDescription { get; set; }
        [Display(Name = "Per Price")]
        public string SubscriptionPrice { get; set; }
        [Display(Name = "Operator Rigs")]
        public int SubscriptionUsersCount { get; set; }
        [Display(Name = "Total Amount")]
        public decimal SubscriptionTotalAmount { get; set; }
        [Display(Name = "Start")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? SubscriptionStart { get; set; }
        [Display(Name = "End")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? SubscriptionEnd { get; set; }
        public string IsEnableSubscription { get; set; }
        public string SubscriptionId { get; set; }
        public int AccountType { get; set; }
    }
}
