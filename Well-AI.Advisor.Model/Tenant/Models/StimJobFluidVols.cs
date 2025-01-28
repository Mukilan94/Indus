using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFluidVols
    {
        public StimJobFluidVols()
        {
            StimJobStageFluids = new HashSet<StimJobStageFluids>();
        }

        [Key]
        public int FluidVolId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FluidVol")]
        public virtual ICollection<StimJobStageFluids> StimJobStageFluids { get; set; }
    }
}
