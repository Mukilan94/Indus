using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobAdditives
    {
        [Key]
        public int AdditiveId { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public int? VolumeId { get; set; }
        public int? MassId { get; set; }
        public string Uid { get; set; }
        public int? StageFluidId { get; set; }

        [ForeignKey(nameof(MassId))]
        [InverseProperty(nameof(StimJobMasss.StimJobAdditives))]
        public virtual StimJobMasss Mass { get; set; }
        [ForeignKey(nameof(StageFluidId))]
        [InverseProperty(nameof(StimJobStageFluids.StimJobAdditives))]
        public virtual StimJobStageFluids StageFluid { get; set; }
        [ForeignKey(nameof(VolumeId))]
        [InverseProperty(nameof(StimJobVolumes.StimJobAdditives))]
        public virtual StimJobVolumes Volume { get; set; }
    }
}
