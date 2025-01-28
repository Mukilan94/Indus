using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReports
    {
        public DrillReports()
        {
            DrillReportActivity = new HashSet<DrillReportActivity>();
            DrillReportFluids = new HashSet<DrillReportFluids>();
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
            DrillReportPorePressure = new HashSet<DrillReportPorePressure>();
            DrillReportWellDatum = new HashSet<DrillReportWellDatum>();
            DrillReportWellboreAlias = new HashSet<DrillReportWellboreAlias>();
        }

        [Key]
        public int DrillReportId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        [Column("DTimStart")]
        public string DtimStart { get; set; }
        [Column("DTimEnd")]
        public string DtimEnd { get; set; }
        public string VersionKind { get; set; }
        public string CreateDate { get; set; }
        public int? WellAliasId { get; set; }
        [Column("WellCRSUid")]
        public string WellCrsuid { get; set; }
        public int? WellboreInfoId { get; set; }
        public int? StatusInfoId { get; set; }
        public int? BitRecordId { get; set; }
        public int? ExtendedReportId { get; set; }
        public int? SurveyStationId { get; set; }
        public int? CoreInfoId { get; set; }
        public int? WellTestInfoId { get; set; }
        public int? FormTestInfoId { get; set; }
        public int? LithShowInfoId { get; set; }
        public int? EquipFailureInfoId { get; set; }
        public int? ControlIncidentInfoId { get; set; }
        public int? StratInfoId { get; set; }
        public int? PerfInfoId { get; set; }
        public int? GasReadingInfoId { get; set; }
        public int? CommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(BitRecordId))]
        [InverseProperty(nameof(DrillReportBitRecord.DrillReports))]
        public virtual DrillReportBitRecord BitRecord { get; set; }
        [ForeignKey(nameof(CommonDataId))]
        [InverseProperty(nameof(DrillReportCommonData.DrillReports))]
        public virtual DrillReportCommonData CommonData { get; set; }
        [ForeignKey(nameof(ControlIncidentInfoId))]
        [InverseProperty(nameof(DrillReportControlIncidentInfo.DrillReports))]
        public virtual DrillReportControlIncidentInfo ControlIncidentInfo { get; set; }
        [ForeignKey(nameof(CoreInfoId))]
        [InverseProperty(nameof(DrillReportCoreInfo.DrillReports))]
        public virtual DrillReportCoreInfo CoreInfo { get; set; }
        [ForeignKey(nameof(EquipFailureInfoId))]
        [InverseProperty(nameof(DrillReportEquipFailureInfo.DrillReports))]
        public virtual DrillReportEquipFailureInfo EquipFailureInfo { get; set; }
        [ForeignKey(nameof(ExtendedReportId))]
        [InverseProperty(nameof(DrillReportExtendedReport.DrillReports))]
        public virtual DrillReportExtendedReport ExtendedReport { get; set; }
        [ForeignKey(nameof(FormTestInfoId))]
        [InverseProperty(nameof(DrillReportFormTestInfo.DrillReports))]
        public virtual DrillReportFormTestInfo FormTestInfo { get; set; }
        [ForeignKey(nameof(GasReadingInfoId))]
        [InverseProperty(nameof(DrillReportGasReadingInfo.DrillReports))]
        public virtual DrillReportGasReadingInfo GasReadingInfo { get; set; }
        [ForeignKey(nameof(LithShowInfoId))]
        [InverseProperty(nameof(DrillReportLithShowInfo.DrillReports))]
        public virtual DrillReportLithShowInfo LithShowInfo { get; set; }
        [ForeignKey(nameof(PerfInfoId))]
        [InverseProperty(nameof(DrillReportPerfInfo.DrillReports))]
        public virtual DrillReportPerfInfo PerfInfo { get; set; }
        [ForeignKey(nameof(StatusInfoId))]
        [InverseProperty(nameof(DrillReportStatusInfo.DrillReports))]
        public virtual DrillReportStatusInfo StatusInfo { get; set; }
        [ForeignKey(nameof(StratInfoId))]
        [InverseProperty(nameof(DrillReportStratInfo.DrillReports))]
        public virtual DrillReportStratInfo StratInfo { get; set; }
        [ForeignKey(nameof(SurveyStationId))]
        [InverseProperty(nameof(DrillReportSurveyStation.DrillReports))]
        public virtual DrillReportSurveyStation SurveyStation { get; set; }
        [ForeignKey(nameof(WellAliasId))]
        [InverseProperty(nameof(DrillReportWellAlias.DrillReports))]
        public virtual DrillReportWellAlias WellAlias { get; set; }
        [ForeignKey(nameof(WellCrsuid))]
        [InverseProperty(nameof(DrillReportWellCr.DrillReports))]
        public virtual DrillReportWellCr WellCrsu { get; set; }
        [ForeignKey(nameof(WellTestInfoId))]
        [InverseProperty(nameof(DrillReportWellTestInfo.DrillReports))]
        public virtual DrillReportWellTestInfo WellTestInfo { get; set; }
        [ForeignKey(nameof(WellboreInfoId))]
        [InverseProperty(nameof(DrillReportWellboreInfo.DrillReports))]
        public virtual DrillReportWellboreInfo WellboreInfo { get; set; }
        [InverseProperty("DrillReport")]
        public virtual ICollection<DrillReportActivity> DrillReportActivity { get; set; }
        [InverseProperty("DrillReport")]
        public virtual ICollection<DrillReportFluids> DrillReportFluids { get; set; }
        [InverseProperty("DrillReport")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
        [InverseProperty("DrillReport")]
        public virtual ICollection<DrillReportPorePressure> DrillReportPorePressure { get; set; }
        [InverseProperty("DrillReport")]
        public virtual ICollection<DrillReportWellDatum> DrillReportWellDatum { get; set; }
        [InverseProperty("DrillReport")]
        public virtual ICollection<DrillReportWellboreAlias> DrillReportWellboreAlias { get; set; }
    }
}
