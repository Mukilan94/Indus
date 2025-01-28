using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellElevations
    {
        public WellElevations()
        {
            WellDatums = new HashSet<WellDatums>();
            WellReferencePoints = new HashSet<WellReferencePoints>();
        }

        [Key]
        public int ElevationId { get; set; }
        public string Uom { get; set; }
        public string Datum { get; set; }
        public string Text { get; set; }

        [InverseProperty("Elevation")]
        public virtual ICollection<WellDatums> WellDatums { get; set; }
        [InverseProperty("Elevation")]
        public virtual ICollection<WellReferencePoints> WellReferencePoints { get; set; }
    }
}
