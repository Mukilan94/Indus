using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobs
    {
        [Key]
        public int StimJobId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public string CommodityCode { get; set; }
        public string ServiceCompany { get; set; }
        public string Supervisor { get; set; }
        public string ApiNumber { get; set; }
        public string CustomerName { get; set; }
        [Column("DTimArrival")]
        public string DtimArrival { get; set; }
        [Column("DTimStart")]
        public string DtimStart { get; set; }
        public int? TotalPumpTimeId { get; set; }
        public int? MaxJobPresId { get; set; }
        public int? MaxFluidRateId { get; set; }
        public int? AvgJobPresId { get; set; }
        public int? TotalJobVolumeId { get; set; }
        public int? TotalProppantWtId { get; set; }
        public string ProppantName { get; set; }
        public string PerfBallCount { get; set; }
        [Column("TotalN2StdVolumeId")]
        public int? TotalN2stdVolumeId { get; set; }
        [Column("TotalCO2MassId")]
        public int? TotalCo2massId { get; set; }
        public int? HhpOrderedId { get; set; }
        public int? HhpUsedId { get; set; }
        public string TreatmentCount { get; set; }
        public int? FluidEfficiencyId { get; set; }
        public int? FlowBackPresId { get; set; }
        public int? FlowBackRateId { get; set; }
        public int? FlowBackVolumeId { get; set; }
        public string TreatmentIntervalCount { get; set; }
        public int? BottomholeStaticTemperatureId { get; set; }
        public int? TreatingBottomholeTemperatureId { get; set; }
        public int? JobIntervalId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(AvgJobPresId))]
        [InverseProperty(nameof(StimJobAvgJobPress.StimJobs))]
        public virtual StimJobAvgJobPress AvgJobPres { get; set; }
        [ForeignKey(nameof(BottomholeStaticTemperatureId))]
        [InverseProperty(nameof(StimJobBottomholeStaticTemperatures.StimJobs))]
        public virtual StimJobBottomholeStaticTemperatures BottomholeStaticTemperature { get; set; }
        [ForeignKey(nameof(FlowBackPresId))]
        [InverseProperty(nameof(StimJobFlowBackPress.StimJobs))]
        public virtual StimJobFlowBackPress FlowBackPres { get; set; }
        [ForeignKey(nameof(FlowBackRateId))]
        [InverseProperty(nameof(StimJobFlowBackRates.StimJobs))]
        public virtual StimJobFlowBackRates FlowBackRate { get; set; }
        [ForeignKey(nameof(FlowBackVolumeId))]
        [InverseProperty(nameof(StimJobFlowBackVolumes.StimJobs))]
        public virtual StimJobFlowBackVolumes FlowBackVolume { get; set; }
        [ForeignKey(nameof(FluidEfficiencyId))]
        [InverseProperty(nameof(StimJobFluidEfficiencys.StimJobs))]
        public virtual StimJobFluidEfficiencys FluidEfficiency { get; set; }
        [ForeignKey(nameof(HhpOrderedId))]
        [InverseProperty(nameof(StimJobHhpOrdereds.StimJobs))]
        public virtual StimJobHhpOrdereds HhpOrdered { get; set; }
        [ForeignKey(nameof(HhpUsedId))]
        [InverseProperty(nameof(StimJobHhpUseds.StimJobs))]
        public virtual StimJobHhpUseds HhpUsed { get; set; }
        [ForeignKey(nameof(JobIntervalId))]
        [InverseProperty(nameof(StimJobJobIntervals.StimJobs))]
        public virtual StimJobJobIntervals JobInterval { get; set; }
        [ForeignKey(nameof(MaxFluidRateId))]
        [InverseProperty(nameof(StimJobMaxFluidRates.StimJobs))]
        public virtual StimJobMaxFluidRates MaxFluidRate { get; set; }
        [ForeignKey(nameof(MaxJobPresId))]
        [InverseProperty(nameof(StimJobMaxJobPress.StimJobs))]
        public virtual StimJobMaxJobPress MaxJobPres { get; set; }
        [ForeignKey(nameof(TotalCo2massId))]
        [InverseProperty(nameof(StimJobTotalCo2masss.StimJobs))]
        public virtual StimJobTotalCo2masss TotalCo2mass { get; set; }
        [ForeignKey(nameof(TotalJobVolumeId))]
        [InverseProperty(nameof(StimJobTotalJobVolumes.StimJobs))]
        public virtual StimJobTotalJobVolumes TotalJobVolume { get; set; }
        [ForeignKey(nameof(TotalN2stdVolumeId))]
        [InverseProperty(nameof(StimJobTotalN2stdVolumes.StimJobs))]
        public virtual StimJobTotalN2stdVolumes TotalN2stdVolume { get; set; }
        [ForeignKey(nameof(TotalProppantWtId))]
        [InverseProperty(nameof(StimJobTotalProppantWts.StimJobs))]
        public virtual StimJobTotalProppantWts TotalProppantWt { get; set; }
        [ForeignKey(nameof(TotalPumpTimeId))]
        [InverseProperty(nameof(StimJobTotalPumpTimes.StimJobs))]
        public virtual StimJobTotalPumpTimes TotalPumpTime { get; set; }
        [ForeignKey(nameof(TreatingBottomholeTemperatureId))]
        [InverseProperty(nameof(StimJobTreatingBottomholeTemperatures.StimJobs))]
        public virtual StimJobTreatingBottomholeTemperatures TreatingBottomholeTemperature { get; set; }
    }
}
