using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryCorUseds
    {
        public TrajectoryCorUseds()
        {
            TrajectoryStations = new HashSet<TrajectoryStations>();
        }

        [Key]
        public int TrajectoryCorUsedId { get; set; }
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
        [InverseProperty(nameof(TrajectoryDirSensorOffsets.TrajectoryCorUseds))]
        public virtual TrajectoryDirSensorOffsets DirSensorOffset { get; set; }
        [ForeignKey(nameof(GravAxialAccelCorId))]
        [InverseProperty(nameof(TrajectoryGravAxialAccelCors.TrajectoryCorUseds))]
        public virtual TrajectoryGravAxialAccelCors GravAxialAccelCor { get; set; }
        [ForeignKey(nameof(GravTran1AccelCorId))]
        [InverseProperty(nameof(TrajectoryGravTran1AccelCors.TrajectoryCorUseds))]
        public virtual TrajectoryGravTran1AccelCors GravTran1AccelCor { get; set; }
        [ForeignKey(nameof(GravTran2AccelCorId))]
        [InverseProperty(nameof(TrajectoryGravTran2AccelCors.TrajectoryCorUseds))]
        public virtual TrajectoryGravTran2AccelCors GravTran2AccelCor { get; set; }
        [ForeignKey(nameof(MagAxialDrlstrCorId))]
        [InverseProperty(nameof(TrajectoryMagAxialDrlstrCors.TrajectoryCorUseds))]
        public virtual TrajectoryMagAxialDrlstrCors MagAxialDrlstrCor { get; set; }
        [ForeignKey(nameof(MagTran1DrlstrCorId))]
        [InverseProperty(nameof(TrajectoryMagTran1DrlstrCors.TrajectoryCorUseds))]
        public virtual TrajectoryMagTran1DrlstrCors MagTran1DrlstrCor { get; set; }
        [ForeignKey(nameof(MagTran2DrlstrCorId))]
        [InverseProperty(nameof(TrajectoryMagTran2DrlstrCors.TrajectoryCorUseds))]
        public virtual TrajectoryMagTran2DrlstrCors MagTran2DrlstrCor { get; set; }
        [ForeignKey(nameof(SagAziCorId))]
        [InverseProperty(nameof(TrajectorySagAziCors.TrajectoryCorUseds))]
        public virtual TrajectorySagAziCors SagAziCor { get; set; }
        [ForeignKey(nameof(SagIncCorId))]
        [InverseProperty(nameof(TrajectorySagIncCors.TrajectoryCorUseds))]
        public virtual TrajectorySagIncCors SagIncCor { get; set; }
        [ForeignKey(nameof(StnGridCorUsedId))]
        [InverseProperty(nameof(TrajectoryStnGridCorUseds.TrajectoryCorUseds))]
        public virtual TrajectoryStnGridCorUseds StnGridCorUsed { get; set; }
        [ForeignKey(nameof(StnMagDeclUsedId))]
        [InverseProperty(nameof(TrajectoryStnMagDeclUseds.TrajectoryCorUseds))]
        public virtual TrajectoryStnMagDeclUseds StnMagDeclUsed { get; set; }
        [InverseProperty("CorUsedTrajectoryCorUsed")]
        public virtual ICollection<TrajectoryStations> TrajectoryStations { get; set; }
    }
}
