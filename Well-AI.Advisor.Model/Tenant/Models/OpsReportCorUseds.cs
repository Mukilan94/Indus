using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCorUseds
    {
        public OpsReportCorUseds()
        {
            OpsReportTrajectoryStations = new HashSet<OpsReportTrajectoryStations>();
        }

        [Key]
        public int CorUsedId { get; set; }
        public int? GravAxialAccelCorId { get; set; }
        public int? GravTran1AccelCorId { get; set; }
        public int? GravTran2AccelCorId { get; set; }
        public int? MagAxialDrlstrCorId { get; set; }
        public int? MagTran1DrlstrCorId { get; set; }
        public int? MagTran2DrlstrCorId { get; set; }
        public int? SagIncCorId { get; set; }
        public int? SagAziCorId { get; set; }
        public int? StnMagDeclUsedId { get; set; }
        public int? StnGridCorUsedId { get; set; }
        public int? DirSensorOffsetId { get; set; }

        [ForeignKey(nameof(DirSensorOffsetId))]
        [InverseProperty(nameof(OpsReportDirSensorOffsets.OpsReportCorUseds))]
        public virtual OpsReportDirSensorOffsets DirSensorOffset { get; set; }
        [ForeignKey(nameof(GravAxialAccelCorId))]
        [InverseProperty(nameof(OpsReportGravAxialAccelCors.OpsReportCorUseds))]
        public virtual OpsReportGravAxialAccelCors GravAxialAccelCor { get; set; }
        [ForeignKey(nameof(GravTran1AccelCorId))]
        [InverseProperty(nameof(OpsReportGravTran1AccelCors.OpsReportCorUseds))]
        public virtual OpsReportGravTran1AccelCors GravTran1AccelCor { get; set; }
        [ForeignKey(nameof(GravTran2AccelCorId))]
        [InverseProperty(nameof(OpsReportGravTran2AccelCors.OpsReportCorUseds))]
        public virtual OpsReportGravTran2AccelCors GravTran2AccelCor { get; set; }
        [ForeignKey(nameof(MagAxialDrlstrCorId))]
        [InverseProperty(nameof(OpsReportMagAxialDrlstrCors.OpsReportCorUseds))]
        public virtual OpsReportMagAxialDrlstrCors MagAxialDrlstrCor { get; set; }
        [ForeignKey(nameof(MagTran1DrlstrCorId))]
        [InverseProperty(nameof(OpsReportMagTran1DrlstrCors.OpsReportCorUseds))]
        public virtual OpsReportMagTran1DrlstrCors MagTran1DrlstrCor { get; set; }
        [ForeignKey(nameof(MagTran2DrlstrCorId))]
        [InverseProperty(nameof(OpsReportMagTran2DrlstrCors.OpsReportCorUseds))]
        public virtual OpsReportMagTran2DrlstrCors MagTran2DrlstrCor { get; set; }
        [ForeignKey(nameof(SagAziCorId))]
        [InverseProperty(nameof(OpsReportSagAziCors.OpsReportCorUseds))]
        public virtual OpsReportSagAziCors SagAziCor { get; set; }
        [ForeignKey(nameof(SagIncCorId))]
        [InverseProperty(nameof(OpsReportSagIncCors.OpsReportCorUseds))]
        public virtual OpsReportSagIncCors SagIncCor { get; set; }
        [ForeignKey(nameof(StnGridCorUsedId))]
        [InverseProperty(nameof(OpsReportStnGridCorUseds.OpsReportCorUseds))]
        public virtual OpsReportStnGridCorUseds StnGridCorUsed { get; set; }
        [ForeignKey(nameof(StnMagDeclUsedId))]
        [InverseProperty(nameof(OpsReportStnMagDeclUseds.OpsReportCorUseds))]
        public virtual OpsReportStnMagDeclUseds StnMagDeclUsed { get; set; }
        [InverseProperty("CorUsed")]
        public virtual ICollection<OpsReportTrajectoryStations> OpsReportTrajectoryStations { get; set; }
    }
}
