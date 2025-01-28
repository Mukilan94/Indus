using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellNorthings
    {
        public WellNorthings()
        {
            WellLocations = new HashSet<WellLocations>();
        }

        [Key]
        public int NorthingId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Northing")]
        public virtual ICollection<WellLocations> WellLocations { get; set; }
    }
}
