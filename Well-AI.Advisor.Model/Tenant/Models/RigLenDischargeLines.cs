using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigLenDischargeLines
    {
        public RigLenDischargeLines()
        {
            RigSurfaceEquipments = new HashSet<RigSurfaceEquipments>();
        }

        [Key]
        public int LenDischargeLineId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LenDischargeLine")]
        public virtual ICollection<RigSurfaceEquipments> RigSurfaceEquipments { get; set; }
    }
}
