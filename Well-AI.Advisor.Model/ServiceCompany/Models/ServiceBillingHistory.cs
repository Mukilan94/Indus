using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    [Table("ServiceBillingHistory")]
    public class ServiceBillingHistory
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }

        [StringLength(254)]
        public string Name { get; set; }
        [StringLength(254)]
        public string Invoice { get; set; }

        public DateTime? BillDate { get; set; }

        [Column("InvoiceAmount")]
        public double Amount { get; set; }
        public int Subscriptions { get; set; }
        public string PayMethod { get; set; }

        [StringLength(254)]
        public string AddressId { get; set; }
        [StringLength(254)]
        public string TenantId { get; set; }
    }
}
