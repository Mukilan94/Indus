using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.ServiceEntity
{
    [Table("Subscription")]
    public class ProductSubscription
    {
        [Key]
        
        public int ID { get; set; }
        [StringLength(450)]
        public string SubID { get; set; }
        [StringLength(50)]
        public string SubName { get; set; }
        public DateTime? SubStartdate { get; set; }
        public DateTime? SubEndDate { get; set; }
        [StringLength(50)]
        public string SubCatogery { get; set; }
        public bool SubStatus { get; set; }
        [StringLength(450)]
        public string TenantId { get; set; }
    }
}
