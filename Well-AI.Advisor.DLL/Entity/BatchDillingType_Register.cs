using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("BatchDillingType_Register")]
   public class BatchDillingType_Register
    {
        [Key]
        [StringLength(40)]
        public string BatchDrillingType_Id { get; set; }
        public string BatchDrillingType { get; set; }
        
    }
}
