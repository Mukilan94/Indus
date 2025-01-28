using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFluidThermalConductivitys
    {
        public StimJobFluidThermalConductivitys()
        {
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
        }

        [Key]
        public int FluidThermalConductivityId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FluidThermalConductivity")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
    }
}
