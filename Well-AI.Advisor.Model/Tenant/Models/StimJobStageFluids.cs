using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobStageFluids
    {
        public StimJobStageFluids()
        {
            StimJobAdditives = new HashSet<StimJobAdditives>();
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int StageFluidId { get; set; }
        public string Name { get; set; }
        public int? FluidVolId { get; set; }
        public string WaterSource { get; set; }
        public int? ProppantId { get; set; }

        [ForeignKey(nameof(FluidVolId))]
        [InverseProperty(nameof(StimJobFluidVols.StimJobStageFluids))]
        public virtual StimJobFluidVols FluidVol { get; set; }
        [ForeignKey(nameof(ProppantId))]
        [InverseProperty(nameof(StimJobProppants.StimJobStageFluids))]
        public virtual StimJobProppants Proppant { get; set; }
        [InverseProperty("StageFluid")]
        public virtual ICollection<StimJobAdditives> StimJobAdditives { get; set; }
        [InverseProperty("StageFluid")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
