using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("crmUserBasicDetail")]
    public class CrmUserBasicDetail
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public int? AccountType { get; set; }
        public bool IsActive { get; set; }
        public bool? IsMaster { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string SubscriptionId { get; set; }
        public int? RegisterPagesCompleteStatus { get; set; }
        public int? PaymentStatus { get; set; }
        public int? NoOfItems { get; set; }
        public string CorporateProfileId { get; set; }
    }
}
