using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("Subscription")]
    public class ProductSubscriptionModel
    {
        [Key]

        public System.Guid ID { get; set; }
        [StringLength(450)]
        public string SubscriptionId { get; set; }
        public int SubscriptionCount { get; set; }
        public int? SubscriptionDispatchCount { get; set; }
        public int? CurrentCount { get; set; }
        [StringLength(50)]
        public string PackageId { get; set; }
        public decimal PackageAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsEnable { get; set; }
        public DateTime? SubStartdate { get; set; }
        public DateTime? SubEndDate { get; set; }
        
        [StringLength(450)]
        public string TenantId { get; set; }
        [StringLength(50)]
        public string CorporateProfileId { get; set; }
        [StringLength(50)]
        public DateTime? NextRenewalDate { get; set; }
        public byte? SubscriptionType { get; set; }
        [StringLength(450)]
        public string? PaymentMethodId { get; set; }
    }

    public class CurrentSubscription
    {
        public string Subscriptiontier { get; set; }
        public DateTime? SubscriptionStartdate { get; set; }
        public DateTime? SubscriptionEnddate { get; set; }
        public string SubsCatListing { get; set; }
        public bool SubscriptionServiceStatus { get; set; }
    }

    public class ModuleAvaliable
    {
        public string Modulesubstier { get; set; }
        public DateTime? ModuleStartdate { get; set; }
        public DateTime? ModuleEnddate { get; set; }
        public bool ModuleServiceStatus { get; set; }
    }
}
