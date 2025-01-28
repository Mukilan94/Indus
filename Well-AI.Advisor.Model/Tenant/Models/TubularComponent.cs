using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularComponent
    {
        public TubularComponent()
        {
            TubularNozzle = new HashSet<TubularNozzle>();
        }

        [Key]
        public int TubularComponentId { get; set; }
        public string TypeTubularComp { get; set; }
        public string Sequence { get; set; }
        public string Description { get; set; }
        public int? IdTubularIdId { get; set; }
        public int? OdId { get; set; }
        public int? LenId { get; set; }
        public int? LenJointAvId { get; set; }
        public string NumJointStand { get; set; }
        public int? WtPerLenId { get; set; }
        public string Grade { get; set; }
        public int? OdDriftId { get; set; }
        public int? TensYieldId { get; set; }
        public int? TqYieldId { get; set; }
        public int? StressFatigId { get; set; }
        public int? LenFishneckId { get; set; }
        public int? IdFishneckId { get; set; }
        public int? OdFishneckId { get; set; }
        public int? DispId { get; set; }
        public int? PresBurstId { get; set; }
        public int? PresCollapseId { get; set; }
        public string ClassService { get; set; }
        public int? WearWallId { get; set; }
        public int? ThickWallId { get; set; }
        public string ConfigCon { get; set; }
        public int? BendStiffnessId { get; set; }
        public int? AxialStiffnessId { get; set; }
        public int? TorsionalStiffnessId { get; set; }
        public string TypeMaterial { get; set; }
        public int? DoglegMxId { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public int? NameTagId { get; set; }
        public int? BitRecordBitId { get; set; }
        public int? AreaNozzleFlowId { get; set; }
        public string Uid { get; set; }
        public int? HoleOpenerId { get; set; }
        public int? StabilizerId { get; set; }
        public int? MotorId { get; set; }
        public int? BendId { get; set; }
        public int? MwdToolId { get; set; }
        public int? ConnectionId { get; set; }
        public int? JarId { get; set; }
        public int? TubularId { get; set; }

        [ForeignKey(nameof(AreaNozzleFlowId))]
        [InverseProperty(nameof(TubularAreaNozzleFlow.TubularComponent))]
        public virtual TubularAreaNozzleFlow AreaNozzleFlow { get; set; }
        [ForeignKey(nameof(AxialStiffnessId))]
        [InverseProperty(nameof(TubularAxialStiffness.TubularComponent))]
        public virtual TubularAxialStiffness AxialStiffness { get; set; }
        [ForeignKey(nameof(BendId))]
        [InverseProperty(nameof(TubularBend.TubularComponent))]
        public virtual TubularBend Bend { get; set; }
        [ForeignKey(nameof(BendStiffnessId))]
        [InverseProperty(nameof(TubularBendStiffness.TubularComponent))]
        public virtual TubularBendStiffness BendStiffness { get; set; }
        [ForeignKey(nameof(BitRecordBitId))]
        [InverseProperty(nameof(TubularBitRecord.TubularComponent))]
        public virtual TubularBitRecord BitRecordBit { get; set; }
        [ForeignKey(nameof(ConnectionId))]
        [InverseProperty(nameof(TubularConnection.TubularComponent))]
        public virtual TubularConnection Connection { get; set; }
        [ForeignKey(nameof(DispId))]
        [InverseProperty(nameof(TubularDisp.TubularComponent))]
        public virtual TubularDisp Disp { get; set; }
        [ForeignKey(nameof(DoglegMxId))]
        [InverseProperty(nameof(TubularDoglegMx.TubularComponent))]
        public virtual TubularDoglegMx DoglegMx { get; set; }
        [ForeignKey(nameof(HoleOpenerId))]
        [InverseProperty(nameof(TubularHoleOpener.TubularComponent))]
        public virtual TubularHoleOpener HoleOpener { get; set; }
        [ForeignKey(nameof(IdFishneckId))]
        [InverseProperty(nameof(TubularIdFishneck.TubularComponent))]
        public virtual TubularIdFishneck IdFishneck { get; set; }
        [ForeignKey(nameof(IdTubularIdId))]
        [InverseProperty("TubularComponent")]
        public virtual TubularId IdTubularId { get; set; }
        [ForeignKey(nameof(JarId))]
        [InverseProperty(nameof(TubularJar.TubularComponent))]
        public virtual TubularJar Jar { get; set; }
        [ForeignKey(nameof(LenId))]
        [InverseProperty(nameof(TubularLen.TubularComponent))]
        public virtual TubularLen Len { get; set; }
        [ForeignKey(nameof(LenFishneckId))]
        [InverseProperty(nameof(TubularLenFishneck.TubularComponent))]
        public virtual TubularLenFishneck LenFishneck { get; set; }
        [ForeignKey(nameof(LenJointAvId))]
        [InverseProperty(nameof(TubularLenJointAv.TubularComponent))]
        public virtual TubularLenJointAv LenJointAv { get; set; }
        [ForeignKey(nameof(MotorId))]
        [InverseProperty(nameof(TubularMotor.TubularComponent))]
        public virtual TubularMotor Motor { get; set; }
        [ForeignKey(nameof(MwdToolId))]
        [InverseProperty(nameof(TubularMwdTool.TubularComponent))]
        public virtual TubularMwdTool MwdTool { get; set; }
        [ForeignKey(nameof(NameTagId))]
        [InverseProperty(nameof(TubularNameTag.TubularComponent))]
        public virtual TubularNameTag NameTag { get; set; }
        [ForeignKey(nameof(OdId))]
        [InverseProperty(nameof(TubularOd.TubularComponent))]
        public virtual TubularOd Od { get; set; }
        [ForeignKey(nameof(OdDriftId))]
        [InverseProperty(nameof(TubularOdDrifts.TubularComponent))]
        public virtual TubularOdDrifts OdDrift { get; set; }
        [ForeignKey(nameof(OdFishneckId))]
        [InverseProperty(nameof(TubularOdFishneck.TubularComponent))]
        public virtual TubularOdFishneck OdFishneck { get; set; }
        [ForeignKey(nameof(PresBurstId))]
        [InverseProperty(nameof(TubularPresBurst.TubularComponent))]
        public virtual TubularPresBurst PresBurst { get; set; }
        [ForeignKey(nameof(PresCollapseId))]
        [InverseProperty(nameof(TubularPresCollapse.TubularComponent))]
        public virtual TubularPresCollapse PresCollapse { get; set; }
        [ForeignKey(nameof(StabilizerId))]
        [InverseProperty(nameof(TubularStabilizer.TubularComponent))]
        public virtual TubularStabilizer Stabilizer { get; set; }
        [ForeignKey(nameof(StressFatigId))]
        [InverseProperty(nameof(TubularStressFatig.TubularComponent))]
        public virtual TubularStressFatig StressFatig { get; set; }
        [ForeignKey(nameof(TensYieldId))]
        [InverseProperty(nameof(TubularTensYield.TubularComponent))]
        public virtual TubularTensYield TensYield { get; set; }
        [ForeignKey(nameof(ThickWallId))]
        [InverseProperty(nameof(TubularThickWall.TubularComponent))]
        public virtual TubularThickWall ThickWall { get; set; }
        [ForeignKey(nameof(TorsionalStiffnessId))]
        [InverseProperty(nameof(TubularTorsionalStiffness.TubularComponent))]
        public virtual TubularTorsionalStiffness TorsionalStiffness { get; set; }
        [ForeignKey(nameof(TqYieldId))]
        [InverseProperty(nameof(TubularTqYield.TubularComponent))]
        public virtual TubularTqYield TqYield { get; set; }
        [ForeignKey(nameof(TubularId))]
        [InverseProperty(nameof(Tubulars.TubularComponent))]
        public virtual Tubulars Tubular { get; set; }
        [ForeignKey(nameof(WearWallId))]
        [InverseProperty(nameof(TubularWearWall.TubularComponent))]
        public virtual TubularWearWall WearWall { get; set; }
        [ForeignKey(nameof(WtPerLenId))]
        [InverseProperty(nameof(TubularWtPerLen.TubularComponent))]
        public virtual TubularWtPerLen WtPerLen { get; set; }
        [InverseProperty("TubularComponent")]
        public virtual ICollection<TubularNozzle> TubularNozzle { get; set; }
    }
}
