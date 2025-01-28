using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("RigsDepthPermission")]
   public class RigsDepth_Permission
    {
        [Key]
        public string ID { get; set; }
        public string RigId { get; set; }
        public string WellId { get; set; }
        public string OprTenantId { get; set; }
        public string SerTenantId { get; set; }
        public bool DepthPermission { get; set; }
    }
}
