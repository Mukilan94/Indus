using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigLenUmbilicals
    {
        public RigLenUmbilicals()
        {
            RigSurfaceEquipments = new HashSet<RigSurfaceEquipments>();
        }

        [Key]
        public int LenUmbilicalId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LenUmbilical")]
        public virtual ICollection<RigSurfaceEquipments> RigSurfaceEquipments { get; set; }
    }
}
