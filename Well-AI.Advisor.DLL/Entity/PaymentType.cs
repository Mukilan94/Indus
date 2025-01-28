using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("PaymentType")]
    public class PaymentTypeEntity
    {
        [Key]
        [StringLength(50)]
        public string ID { get; set; }
        [StringLength(254)]
        public string Name { get; set; }
    }
}
