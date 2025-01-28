using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellGroundElevations
    {
        public WellGroundElevations()
        {
            Wells = new HashSet<Wells>();
        }

        [Key]
        public int GroundElevationId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GroundElevation")]
        public virtual ICollection<Wells> Wells { get; set; }
    }
}
