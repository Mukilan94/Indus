using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("BasinType")]
   public class BasinType
    {
      [Key]
        public string Basin_ID { get; set; }
        public string BasinType_name { get; set; }
    }
}
