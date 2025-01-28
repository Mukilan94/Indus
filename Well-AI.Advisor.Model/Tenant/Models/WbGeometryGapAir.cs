using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WbGeometryGapAir
    {
        public WbGeometryGapAir()
        {
            WbGeometrys = new HashSet<WbGeometrys>();
        }

        [Key]
        public int GapAirId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GapAir")]
        public virtual ICollection<WbGeometrys> WbGeometrys { get; set; }
    }
}
