using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobTubulars
    {
        [Key]
        public int TubularId { get; set; }
        public string Type { get; set; }
        public int? IdId { get; set; }
        public int? OdId { get; set; }
        public int? WeightId { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomId { get; set; }
        public int? VolumeFactorId { get; set; }
        public string Uid { get; set; }
        public int? FlowPathId { get; set; }

        [ForeignKey(nameof(FlowPathId))]
        [InverseProperty(nameof(StimJobFlowPaths.StimJobTubulars))]
        public virtual StimJobFlowPaths FlowPath { get; set; }
        [ForeignKey(nameof(IdId))]
        [InverseProperty(nameof(StimJobIds.StimJobTubulars))]
        public virtual StimJobIds Id { get; set; }
        [ForeignKey(nameof(MdBottomId))]
        [InverseProperty(nameof(StimJobMdBottoms.StimJobTubulars))]
        public virtual StimJobMdBottoms MdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(StimJobMdTops.StimJobTubulars))]
        public virtual StimJobMdTops MdTop { get; set; }
        [ForeignKey(nameof(OdId))]
        [InverseProperty(nameof(StimJobOds.StimJobTubulars))]
        public virtual StimJobOds Od { get; set; }
        [ForeignKey(nameof(VolumeFactorId))]
        [InverseProperty(nameof(StimJobVolumeFactors.StimJobTubulars))]
        public virtual StimJobVolumeFactors VolumeFactor { get; set; }
        [ForeignKey(nameof(WeightId))]
        [InverseProperty(nameof(StimJobWeights.StimJobTubulars))]
        public virtual StimJobWeights Weight { get; set; }
    }
}
