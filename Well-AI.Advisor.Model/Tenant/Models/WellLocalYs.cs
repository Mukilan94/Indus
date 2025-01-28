using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellLocalYs
    {
        public WellLocalYs()
        {
            WellLocations = new HashSet<WellLocations>();
        }

        [Key]
        [Column("LocalYId")]
        public int LocalYid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LocalY")]
        public virtual ICollection<WellLocations> WellLocations { get; set; }
    }
}
