using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTrajectoryStations
    {
        public OpsReportTrajectoryStations()
        {
            OpsReportLocations = new HashSet<OpsReportLocations>();
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public string Uid { get; set; }
        [Column("DTimStn")]
        public string DtimStn { get; set; }
        public string TypeTrajStation { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public int? InclId { get; set; }
        public int? AziId { get; set; }
        public int? MtfId { get; set; }
        public int? GtfId { get; set; }
        public int? DispNsId { get; set; }
        public int? DispEwId { get; set; }
        public int? VertSectId { get; set; }
        public int? DlsId { get; set; }
        public int? RateTurnId { get; set; }
        public int? RateBuildId { get; set; }
        public int? MdDeltaId { get; set; }
        public int? TvdDeltaId { get; set; }
        public string ModelToolError { get; set; }
        public int? GravTotalUncertId { get; set; }
        public int? DipAngleUncertId { get; set; }
        public int? MagTotalUncertId { get; set; }
        public string GravAccelCorUsed { get; set; }
        [Column("MagXAxialCorUsed")]
        public string MagXaxialCorUsed { get; set; }
        public string SagCorUsed { get; set; }
        public string MagDrlstrCorUsed { get; set; }
        public string StatusTrajStation { get; set; }
        public int? RawDataId { get; set; }
        public int? CorUsedId { get; set; }
        public int? ValidId { get; set; }
        public int? MatrixCovId { get; set; }

        [ForeignKey(nameof(AziId))]
        [InverseProperty(nameof(OpsReportAzis.OpsReportTrajectoryStations))]
        public virtual OpsReportAzis Azi { get; set; }
        [ForeignKey(nameof(CorUsedId))]
        [InverseProperty(nameof(OpsReportCorUseds.OpsReportTrajectoryStations))]
        public virtual OpsReportCorUseds CorUsed { get; set; }
        [ForeignKey(nameof(DipAngleUncertId))]
        [InverseProperty(nameof(OpsReportDipAngleUncerts.OpsReportTrajectoryStations))]
        public virtual OpsReportDipAngleUncerts DipAngleUncert { get; set; }
        [ForeignKey(nameof(DispEwId))]
        [InverseProperty(nameof(OpsReportDispEws.OpsReportTrajectoryStations))]
        public virtual OpsReportDispEws DispEw { get; set; }
        [ForeignKey(nameof(DispNsId))]
        [InverseProperty(nameof(OpsReportDispNss.OpsReportTrajectoryStations))]
        public virtual OpsReportDispNss DispNs { get; set; }
        [ForeignKey(nameof(DlsId))]
        [InverseProperty(nameof(OpsReportDlss.OpsReportTrajectoryStations))]
        public virtual OpsReportDlss Dls { get; set; }
        [ForeignKey(nameof(GravTotalUncertId))]
        [InverseProperty(nameof(OpsReportGravTotalUncerts.OpsReportTrajectoryStations))]
        public virtual OpsReportGravTotalUncerts GravTotalUncert { get; set; }
        [ForeignKey(nameof(GtfId))]
        [InverseProperty(nameof(OpsReportGtfs.OpsReportTrajectoryStations))]
        public virtual OpsReportGtfs Gtf { get; set; }
        [ForeignKey(nameof(InclId))]
        [InverseProperty(nameof(OpsReportIncls.OpsReportTrajectoryStations))]
        public virtual OpsReportIncls Incl { get; set; }
        [ForeignKey(nameof(MagTotalUncertId))]
        [InverseProperty(nameof(OpsReportMagTotalUncerts.OpsReportTrajectoryStations))]
        public virtual OpsReportMagTotalUncerts MagTotalUncert { get; set; }
        [ForeignKey(nameof(MatrixCovId))]
        [InverseProperty(nameof(OpsReportMatrixCovs.OpsReportTrajectoryStations))]
        public virtual OpsReportMatrixCovs MatrixCov { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(OpsReportMds.OpsReportTrajectoryStations))]
        public virtual OpsReportMds Md { get; set; }
        [ForeignKey(nameof(MdDeltaId))]
        [InverseProperty(nameof(OpsReportMdDeltas.OpsReportTrajectoryStations))]
        public virtual OpsReportMdDeltas MdDelta { get; set; }
        [ForeignKey(nameof(MtfId))]
        [InverseProperty(nameof(OpsReportMtfs.OpsReportTrajectoryStations))]
        public virtual OpsReportMtfs Mtf { get; set; }
        [ForeignKey(nameof(RateBuildId))]
        [InverseProperty(nameof(OpsReportRateBuilds.OpsReportTrajectoryStations))]
        public virtual OpsReportRateBuilds RateBuild { get; set; }
        [ForeignKey(nameof(RateTurnId))]
        [InverseProperty(nameof(OpsReportRateTurns.OpsReportTrajectoryStations))]
        public virtual OpsReportRateTurns RateTurn { get; set; }
        [ForeignKey(nameof(RawDataId))]
        [InverseProperty(nameof(OpsReportRawDatas.OpsReportTrajectoryStations))]
        public virtual OpsReportRawDatas RawData { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(OpsReportTvds.OpsReportTrajectoryStations))]
        public virtual OpsReportTvds Tvd { get; set; }
        [ForeignKey(nameof(TvdDeltaId))]
        [InverseProperty(nameof(OpsReportTvdDeltas.OpsReportTrajectoryStations))]
        public virtual OpsReportTvdDeltas TvdDelta { get; set; }
        [ForeignKey(nameof(ValidId))]
        [InverseProperty(nameof(OpsReportValids.OpsReportTrajectoryStations))]
        public virtual OpsReportValids Valid { get; set; }
        [ForeignKey(nameof(VertSectId))]
        [InverseProperty(nameof(OpsReportVertSects.OpsReportTrajectoryStations))]
        public virtual OpsReportVertSects VertSect { get; set; }
        [InverseProperty("OpsReportTrajectoryStationU")]
        public virtual ICollection<OpsReportLocations> OpsReportLocations { get; set; }
        [InverseProperty("TrajectoryStationU")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
