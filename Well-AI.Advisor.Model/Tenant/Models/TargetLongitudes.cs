using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TargetLongitudes
    {
        public TargetLongitudes()
        {
            TargetLocations = new HashSet<TargetLocations>();
        }

        [Key]
        public int LongitudeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Longitude")]
        public virtual ICollection<TargetLocations> TargetLocations { get; set; }
    }
}
