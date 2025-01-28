using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryStations
    {
        public TrajectoryStations()
        {
            TrajectoryLocations = new HashSet<TrajectoryLocations>();
            Trajectorys = new HashSet<Trajectorys>();
        }

        [Key]
        public int TrajectoryStationId { get; set; }
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
        public int? CorUsedTrajectoryCorUsedId { get; set; }
        public int? ValidId { get; set; }
        public int? MatrixCovId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AziId))]
        [InverseProperty(nameof(TrajectoryAzis.TrajectoryStations))]
        public virtual TrajectoryAzis Azi { get; set; }
        [ForeignKey(nameof(CorUsedTrajectoryCorUsedId))]
        [InverseProperty(nameof(TrajectoryCorUseds.TrajectoryStations))]
        public virtual TrajectoryCorUseds CorUsedTrajectoryCorUsed { get; set; }
        [ForeignKey(nameof(DipAngleUncertId))]
        [InverseProperty(nameof(TrajectoryDipAngleUncerts.TrajectoryStations))]
        public virtual TrajectoryDipAngleUncerts DipAngleUncert { get; set; }
        [ForeignKey(nameof(DispEwId))]
        [InverseProperty(nameof(TrajectoryDispEws.TrajectoryStations))]
        public virtual TrajectoryDispEws DispEw { get; set; }
        [ForeignKey(nameof(DispNsId))]
        [InverseProperty(nameof(TrajectoryDispNss.TrajectoryStations))]
        public virtual TrajectoryDispNss DispNs { get; set; }
        [ForeignKey(nameof(DlsId))]
        [InverseProperty(nameof(TrajectoryDlss.TrajectoryStations))]
        public virtual TrajectoryDlss Dls { get; set; }
        [ForeignKey(nameof(GravTotalUncertId))]
        [InverseProperty(nameof(TrajectoryGravTotalUncerts.TrajectoryStations))]
        public virtual TrajectoryGravTotalUncerts GravTotalUncert { get; set; }
        [ForeignKey(nameof(GtfId))]
        [InverseProperty(nameof(TrajectoryGtfs.TrajectoryStations))]
        public virtual TrajectoryGtfs Gtf { get; set; }
        [ForeignKey(nameof(InclId))]
        [InverseProperty(nameof(TrajectoryIncls.TrajectoryStations))]
        public virtual TrajectoryIncls Incl { get; set; }
        [ForeignKey(nameof(MagTotalUncertId))]
        [InverseProperty(nameof(TrajectoryMagTotalUncerts.TrajectoryStations))]
        public virtual TrajectoryMagTotalUncerts MagTotalUncert { get; set; }
        [ForeignKey(nameof(MatrixCovId))]
        [InverseProperty(nameof(TrajectoryMatrixCovs.TrajectoryStations))]
        public virtual TrajectoryMatrixCovs MatrixCov { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(TrajectoryMds.TrajectoryStations))]
        public virtual TrajectoryMds Md { get; set; }
        [ForeignKey(nameof(MdDeltaId))]
        [InverseProperty(nameof(TrajectoryMdDeltas.TrajectoryStations))]
        public virtual TrajectoryMdDeltas MdDelta { get; set; }
        [ForeignKey(nameof(MtfId))]
        [InverseProperty(nameof(TrajectoryMtfs.TrajectoryStations))]
        public virtual TrajectoryMtfs Mtf { get; set; }
        [ForeignKey(nameof(RateBuildId))]
        [InverseProperty(nameof(TrajectoryRateBuilds.TrajectoryStations))]
        public virtual TrajectoryRateBuilds RateBuild { get; set; }
        [ForeignKey(nameof(RateTurnId))]
        [InverseProperty(nameof(TrajectoryRateTurns.TrajectoryStations))]
        public virtual TrajectoryRateTurns RateTurn { get; set; }
        [ForeignKey(nameof(RawDataId))]
        [InverseProperty(nameof(TrajectoryRawDatas.TrajectoryStations))]
        public virtual TrajectoryRawDatas RawData { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(TrajectoryTvds.TrajectoryStations))]
        public virtual TrajectoryTvds Tvd { get; set; }
        [ForeignKey(nameof(TvdDeltaId))]
        [InverseProperty(nameof(TrajectoryTvdDeltas.TrajectoryStations))]
        public virtual TrajectoryTvdDeltas TvdDelta { get; set; }
        [ForeignKey(nameof(ValidId))]
        [InverseProperty(nameof(TrajectoryValids.TrajectoryStations))]
        public virtual TrajectoryValids Valid { get; set; }
        [ForeignKey(nameof(VertSectId))]
        [InverseProperty(nameof(TrajectoryVertSects.TrajectoryStations))]
        public virtual TrajectoryVertSects VertSect { get; set; }
        [InverseProperty("TrajectoryStation")]
        public virtual ICollection<TrajectoryLocations> TrajectoryLocations { get; set; }
        [InverseProperty("TrajectoryStation")]
        public virtual ICollection<Trajectorys> Trajectorys { get; set; }
    }
}
