using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetProjectedXs
    {
        public TargetProjectedXs()
        {
            TargetLocations = new HashSet<TargetLocations>();
        }

        [Key]
        [Column("ProjectedXId")]
        public int ProjectedXid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ProjectedX")]
        public virtual ICollection<TargetLocations> TargetLocations { get; set; }
    }
}
