using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobGrossPayThicknesss
    {
        public StimJobGrossPayThicknesss()
        {
            StimJobReservoirIntervals = new HashSet<StimJobReservoirIntervals>();
        }

        [Key]
        public int GrossPayThicknessId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GrossPayThickness")]
        public virtual ICollection<StimJobReservoirIntervals> StimJobReservoirIntervals { get; set; }
    }
}
