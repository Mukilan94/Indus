using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellLocalXs
    {
        public WellLocalXs()
        {
            WellLocations = new HashSet<WellLocations>();
        }

        [Key]
        [Column("LocalXId")]
        public int LocalXid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LocalX")]
        public virtual ICollection<WellLocations> WellLocations { get; set; }
    }
}
