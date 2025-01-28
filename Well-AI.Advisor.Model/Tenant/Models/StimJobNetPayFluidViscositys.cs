using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobNetPayFluidViscositys
    {
        public StimJobNetPayFluidViscositys()
        {
            StimJobReservoirIntervals = new HashSet<StimJobReservoirIntervals>();
        }

        [Key]
        public int NetPayFluidViscosityId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("NetPayFluidViscosity")]
        public virtual ICollection<StimJobReservoirIntervals> StimJobReservoirIntervals { get; set; }
    }
}
