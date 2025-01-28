using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportRigResponses
    {
        public OpsReportRigResponses()
        {
            OpsReportAnchorAngles = new HashSet<OpsReportAnchorAngles>();
            OpsReportAnchorTensions = new HashSet<OpsReportAnchorTensions>();
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int RigResponseId { get; set; }
        public int? RigHeadingId { get; set; }
        public int? RigHeaveId { get; set; }
        public int? RigPitchAngleId { get; set; }
        public int? RigRollAngleId { get; set; }
        public int? RiserAngleId { get; set; }
        public int? RiserDirectionId { get; set; }
        public int? RiserTensionId { get; set; }
        public int? VariableDeckLoadId { get; set; }
        public int? TotalDeckLoadId { get; set; }
        public int? GuideBaseAngleId { get; set; }
        public int? BallJointAngleId { get; set; }
        public int? BallJointDirectionId { get; set; }
        public int? OffsetRigId { get; set; }
        public int? LoadLeg1Id { get; set; }
        public int? LoadLeg2Id { get; set; }
        public int? LoadLeg3Id { get; set; }
        public int? LoadLeg4Id { get; set; }
        public int? PenetrationLeg1Id { get; set; }
        public int? PenetrationLeg2Id { get; set; }
        public int? PenetrationLeg3Id { get; set; }
        public int? PenetrationLeg4Id { get; set; }
        public int? DispRigId { get; set; }
        public int? MeanDraftId { get; set; }

        [ForeignKey(nameof(BallJointAngleId))]
        [InverseProperty(nameof(OpsReportBallJointAngles.OpsReportRigResponses))]
        public virtual OpsReportBallJointAngles BallJointAngle { get; set; }
        [ForeignKey(nameof(BallJointDirectionId))]
        [InverseProperty(nameof(OpsReportBallJointDirections.OpsReportRigResponses))]
        public virtual OpsReportBallJointDirections BallJointDirection { get; set; }
        [ForeignKey(nameof(DispRigId))]
        [InverseProperty(nameof(OpsReportDispRigs.OpsReportRigResponses))]
        public virtual OpsReportDispRigs DispRig { get; set; }
        [ForeignKey(nameof(GuideBaseAngleId))]
        [InverseProperty(nameof(OpsReportGuideBaseAngles.OpsReportRigResponses))]
        public virtual OpsReportGuideBaseAngles GuideBaseAngle { get; set; }
        [ForeignKey(nameof(LoadLeg1Id))]
        [InverseProperty(nameof(OpsReportLoadLeg1s.OpsReportRigResponses))]
        public virtual OpsReportLoadLeg1s LoadLeg1 { get; set; }
        [ForeignKey(nameof(LoadLeg2Id))]
        [InverseProperty(nameof(OpsReportLoadLeg2s.OpsReportRigResponses))]
        public virtual OpsReportLoadLeg2s LoadLeg2 { get; set; }
        [ForeignKey(nameof(LoadLeg3Id))]
        [InverseProperty(nameof(OpsReportLoadLeg3s.OpsReportRigResponses))]
        public virtual OpsReportLoadLeg3s LoadLeg3 { get; set; }
        [ForeignKey(nameof(LoadLeg4Id))]
        [InverseProperty(nameof(OpsReportLoadLeg4s.OpsReportRigResponses))]
        public virtual OpsReportLoadLeg4s LoadLeg4 { get; set; }
        [ForeignKey(nameof(MeanDraftId))]
        [InverseProperty(nameof(OpsReportMeanDrafts.OpsReportRigResponses))]
        public virtual OpsReportMeanDrafts MeanDraft { get; set; }
        [ForeignKey(nameof(OffsetRigId))]
        [InverseProperty(nameof(OpsReportOffsetRigs.OpsReportRigResponses))]
        public virtual OpsReportOffsetRigs OffsetRig { get; set; }
        [ForeignKey(nameof(PenetrationLeg1Id))]
        [InverseProperty(nameof(OpsReportPenetrationLeg1s.OpsReportRigResponses))]
        public virtual OpsReportPenetrationLeg1s PenetrationLeg1 { get; set; }
        [ForeignKey(nameof(PenetrationLeg2Id))]
        [InverseProperty(nameof(OpsReportPenetrationLeg2s.OpsReportRigResponses))]
        public virtual OpsReportPenetrationLeg2s PenetrationLeg2 { get; set; }
        [ForeignKey(nameof(PenetrationLeg3Id))]
        [InverseProperty(nameof(OpsReportPenetrationLeg3s.OpsReportRigResponses))]
        public virtual OpsReportPenetrationLeg3s PenetrationLeg3 { get; set; }
        [ForeignKey(nameof(PenetrationLeg4Id))]
        [InverseProperty(nameof(OpsReportPenetrationLeg4s.OpsReportRigResponses))]
        public virtual OpsReportPenetrationLeg4s PenetrationLeg4 { get; set; }
        [ForeignKey(nameof(RigHeadingId))]
        [InverseProperty(nameof(OpsReportRigHeadings.OpsReportRigResponses))]
        public virtual OpsReportRigHeadings RigHeading { get; set; }
        [ForeignKey(nameof(RigHeaveId))]
        [InverseProperty(nameof(OpsReportRigHeaves.OpsReportRigResponses))]
        public virtual OpsReportRigHeaves RigHeave { get; set; }
        [ForeignKey(nameof(RigPitchAngleId))]
        [InverseProperty(nameof(OpsReportRigPitchAngles.OpsReportRigResponses))]
        public virtual OpsReportRigPitchAngles RigPitchAngle { get; set; }
        [ForeignKey(nameof(RigRollAngleId))]
        [InverseProperty(nameof(OpsReportRigRollAngles.OpsReportRigResponses))]
        public virtual OpsReportRigRollAngles RigRollAngle { get; set; }
        [ForeignKey(nameof(RiserAngleId))]
        [InverseProperty(nameof(OpsReportRiserAngles.OpsReportRigResponses))]
        public virtual OpsReportRiserAngles RiserAngle { get; set; }
        [ForeignKey(nameof(RiserDirectionId))]
        [InverseProperty(nameof(OpsReportRiserDirections.OpsReportRigResponses))]
        public virtual OpsReportRiserDirections RiserDirection { get; set; }
        [ForeignKey(nameof(RiserTensionId))]
        [InverseProperty(nameof(OpsReportRiserTensions.OpsReportRigResponses))]
        public virtual OpsReportRiserTensions RiserTension { get; set; }
        [ForeignKey(nameof(TotalDeckLoadId))]
        [InverseProperty(nameof(OpsReportTotalDeckLoads.OpsReportRigResponses))]
        public virtual OpsReportTotalDeckLoads TotalDeckLoad { get; set; }
        [ForeignKey(nameof(VariableDeckLoadId))]
        [InverseProperty(nameof(OpsReportVariableDeckLoads.OpsReportRigResponses))]
        public virtual OpsReportVariableDeckLoads VariableDeckLoad { get; set; }
        [InverseProperty("OpsReportRigResponseRigResponse")]
        public virtual ICollection<OpsReportAnchorAngles> OpsReportAnchorAngles { get; set; }
        [InverseProperty("OpsReportRigResponseRigResponse")]
        public virtual ICollection<OpsReportAnchorTensions> OpsReportAnchorTensions { get; set; }
        [InverseProperty("RigResponse")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
