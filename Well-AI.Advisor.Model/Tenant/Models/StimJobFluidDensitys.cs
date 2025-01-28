using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFluidDensitys
    {
        public StimJobFluidDensitys()
        {
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
        }

        [Key]
        public int FluidDensityId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FluidDensity")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
    }
}
