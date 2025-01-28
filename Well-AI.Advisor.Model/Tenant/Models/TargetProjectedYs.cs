using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetProjectedYs
    {
        public TargetProjectedYs()
        {
            TargetLocations = new HashSet<TargetLocations>();
        }

        [Key]
        [Column("ProjectedYId")]
        public int ProjectedYid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ProjectedY")]
        public virtual ICollection<TargetLocations> TargetLocations { get; set; }
    }
}
