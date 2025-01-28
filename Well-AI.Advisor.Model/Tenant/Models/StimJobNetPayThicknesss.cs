using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobNetPayThicknesss
    {
        public StimJobNetPayThicknesss()
        {
            StimJobReservoirIntervals = new HashSet<StimJobReservoirIntervals>();
        }

        [Key]
        public int NetPayThicknessId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("NetPayThickness")]
        public virtual ICollection<StimJobReservoirIntervals> StimJobReservoirIntervals { get; set; }
    }
}
