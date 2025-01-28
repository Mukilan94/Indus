using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPerforationIntervals
    {
        public StimJobPerforationIntervals()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int PerforationIntervalId { get; set; }
        public string Type { get; set; }
        public int? MdPerforationsTopId { get; set; }
        public int? MdPerforationsBottomId { get; set; }
        public int? TvdPerforationsTopId { get; set; }
        public int? TvdPerforationsBottomId { get; set; }
        public string PerforationCount { get; set; }
        public int? SizeId { get; set; }
        public int? DensityPerforationId { get; set; }
        public int? PhasingPerforationId { get; set; }
        public string FrictionFactor { get; set; }
        public int? FrictionPresId { get; set; }
        public string DischargeCoefficient { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(DensityPerforationId))]
        [InverseProperty(nameof(StimJobDensityPerforations.StimJobPerforationIntervals))]
        public virtual StimJobDensityPerforations DensityPerforation { get; set; }
        [ForeignKey(nameof(FrictionPresId))]
        [InverseProperty(nameof(StimJobFrictionPress.StimJobPerforationIntervals))]
        public virtual StimJobFrictionPress FrictionPres { get; set; }
        [ForeignKey(nameof(MdPerforationsBottomId))]
        [InverseProperty(nameof(StimJobMdPerforationsBottoms.StimJobPerforationIntervals))]
        public virtual StimJobMdPerforationsBottoms MdPerforationsBottom { get; set; }
        [ForeignKey(nameof(MdPerforationsTopId))]
        [InverseProperty(nameof(StimJobMdPerforationsTops.StimJobPerforationIntervals))]
        public virtual StimJobMdPerforationsTops MdPerforationsTop { get; set; }
        [ForeignKey(nameof(PhasingPerforationId))]
        [InverseProperty(nameof(StimJobPhasingPerforations.StimJobPerforationIntervals))]
        public virtual StimJobPhasingPerforations PhasingPerforation { get; set; }
        [ForeignKey(nameof(SizeId))]
        [InverseProperty(nameof(StimJobSizes.StimJobPerforationIntervals))]
        public virtual StimJobSizes Size { get; set; }
        [ForeignKey(nameof(TvdPerforationsBottomId))]
        [InverseProperty(nameof(StimJobTvdPerforationsBottoms.StimJobPerforationIntervals))]
        public virtual StimJobTvdPerforationsBottoms TvdPerforationsBottom { get; set; }
        [ForeignKey(nameof(TvdPerforationsTopId))]
        [InverseProperty(nameof(StimJobTvdPerforationsTops.StimJobPerforationIntervals))]
        public virtual StimJobTvdPerforationsTops TvdPerforationsTop { get; set; }
        [InverseProperty("PerforationInterval")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
