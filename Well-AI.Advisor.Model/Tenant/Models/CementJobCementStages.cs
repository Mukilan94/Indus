using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobCementStages
    {
        public CementJobCementStages()
        {
            CementJobs = new HashSet<CementJobs>();
        }

        [Key]
        public int CementStageId { get; set; }
        public string Uid { get; set; }
        public string NumStage { get; set; }
        public string TypeStage { get; set; }
        [Column("DTimMixStart")]
        public string DtimMixStart { get; set; }
        [Column("DTimPumpStart")]
        public string DtimPumpStart { get; set; }
        [Column("DTimPumpEnd")]
        public string DtimPumpEnd { get; set; }
        [Column("DTimDisplaceStart")]
        public string DtimDisplaceStart { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomId { get; set; }
        public int? VolExcessId { get; set; }
        public int? FlowrateDisplaceAvId { get; set; }
        public int? FlowrateDisplaceMxId { get; set; }
        public int? PresDisplaceId { get; set; }
        public int? VolReturnsId { get; set; }
        [Column("ETimMudCirculationId")]
        public int? EtimMudCirculationId { get; set; }
        public int? FlowrateMudCircCementJobFlowrateMudCircId { get; set; }
        public int? PresMudCircId { get; set; }
        public int? FlowrateEndId { get; set; }
        public int? CementingFluidId { get; set; }
        public string AfterFlowAnn { get; set; }
        public string SqueezeObj { get; set; }
        public string SqueezeObtained { get; set; }
        public int? MdStringId { get; set; }
        public int? MdToolId { get; set; }
        public int? MdCoilTbgId { get; set; }
        public int? VolCsgInId { get; set; }
        public int? VolCsgOutId { get; set; }
        public string TailPipeUsed { get; set; }
        public int? DiaTailPipeId { get; set; }
        public string TailPipePerf { get; set; }
        public int? PresTbgStartPresTbgStId { get; set; }
        public int? PresTbgEndId { get; set; }
        public int? PresCsgStartId { get; set; }
        public int? PresCsgEndId { get; set; }
        public int? PresBackPressureId { get; set; }
        public int? PresCoilTbgStartId { get; set; }
        public int? PresCoilTbgEndId { get; set; }
        public int? PresBreakDownId { get; set; }
        public int? FlowrateBreakDownId { get; set; }
        public int? PresSqueezeAvId { get; set; }
        public int? PresSqueezeEndId { get; set; }
        public string PresSqueezeHeld { get; set; }
        public int? PresSqueezeId { get; set; }
        [Column("ETimPresHeldId")]
        public int? EtimPresHeldId { get; set; }
        public int? FlowrateSqueezeAvId { get; set; }
        public int? FlowrateSqueezeMxId { get; set; }
        public int? FlowratePumpStartId { get; set; }
        public int? FlowratePumpEndId { get; set; }
        public string PillBelowPlug { get; set; }
        public string PlugCatcher { get; set; }
        public int? MdCircOutId { get; set; }
        public int? VolCircPriorId { get; set; }
        public string TypeOriginalMud { get; set; }
        public int? WtMudId { get; set; }
        public int? VisFunnelMudId { get; set; }
        public int? PvMudId { get; set; }
        public int? YpMudId { get; set; }
        public int? Gel10SecId { get; set; }
        public int? Gel10MinId { get; set; }
        [Column("TempBHCTId")]
        public int? TempBhctid { get; set; }
        [Column("TempBHSTId")]
        public int? TempBhstid { get; set; }
        public string VolExcessMethod { get; set; }
        public string MixMethod { get; set; }
        public string DensMeasBy { get; set; }
        public string AnnFlowAfter { get; set; }
        public string TopPlug { get; set; }
        public string BotPlug { get; set; }
        public string BotPlugNumber { get; set; }
        public string PlugBumped { get; set; }
        public int? PresPriorBumpId { get; set; }
        public int? PresBumpId { get; set; }
        public int? PresHeldId { get; set; }
        public string FloatHeld { get; set; }
        public int? VolMudLostId { get; set; }
        public string FluidDisplace { get; set; }
        public int? DensDisplaceFluidId { get; set; }
        public int? VolDisplaceFluidId { get; set; }

        [ForeignKey(nameof(CementingFluidId))]
        [InverseProperty(nameof(CementJobCementingFluids.CementJobCementStages))]
        public virtual CementJobCementingFluids CementingFluid { get; set; }
        [ForeignKey(nameof(DensDisplaceFluidId))]
        [InverseProperty(nameof(CementJobDensDisplaceFluids.CementJobCementStages))]
        public virtual CementJobDensDisplaceFluids DensDisplaceFluid { get; set; }
        [ForeignKey(nameof(DiaTailPipeId))]
        [InverseProperty(nameof(CementJobDiaTailPipes.CementJobCementStages))]
        public virtual CementJobDiaTailPipes DiaTailPipe { get; set; }
        [ForeignKey(nameof(EtimMudCirculationId))]
        [InverseProperty(nameof(CementJobEtimMudCirculations.CementJobCementStages))]
        public virtual CementJobEtimMudCirculations EtimMudCirculation { get; set; }
        [ForeignKey(nameof(EtimPresHeldId))]
        [InverseProperty(nameof(CementJobEtimPresHelds.CementJobCementStages))]
        public virtual CementJobEtimPresHelds EtimPresHeld { get; set; }
        [ForeignKey(nameof(FlowrateBreakDownId))]
        [InverseProperty(nameof(CementJobFlowrateBreakDowns.CementJobCementStages))]
        public virtual CementJobFlowrateBreakDowns FlowrateBreakDown { get; set; }
        [ForeignKey(nameof(FlowrateDisplaceAvId))]
        [InverseProperty(nameof(CementJobFlowrateDisplaceAvs.CementJobCementStages))]
        public virtual CementJobFlowrateDisplaceAvs FlowrateDisplaceAv { get; set; }
        [ForeignKey(nameof(FlowrateDisplaceMxId))]
        [InverseProperty(nameof(CementJobFlowrateDisplaceMxs.CementJobCementStages))]
        public virtual CementJobFlowrateDisplaceMxs FlowrateDisplaceMx { get; set; }
        [ForeignKey(nameof(FlowrateEndId))]
        [InverseProperty(nameof(CementJobFlowrateEnds.CementJobCementStages))]
        public virtual CementJobFlowrateEnds FlowrateEnd { get; set; }
        [ForeignKey(nameof(FlowrateMudCircCementJobFlowrateMudCircId))]
        [InverseProperty(nameof(CementJobFlowrateMudCircs.CementJobCementStages))]
        public virtual CementJobFlowrateMudCircs FlowrateMudCircCementJobFlowrateMudCirc { get; set; }
        [ForeignKey(nameof(FlowratePumpEndId))]
        [InverseProperty(nameof(CementJobFlowratePumpEnds.CementJobCementStages))]
        public virtual CementJobFlowratePumpEnds FlowratePumpEnd { get; set; }
        [ForeignKey(nameof(FlowratePumpStartId))]
        [InverseProperty(nameof(CementJobFlowratePumpStarts.CementJobCementStages))]
        public virtual CementJobFlowratePumpStarts FlowratePumpStart { get; set; }
        [ForeignKey(nameof(FlowrateSqueezeAvId))]
        [InverseProperty(nameof(CementJobFlowrateSqueezeAvs.CementJobCementStages))]
        public virtual CementJobFlowrateSqueezeAvs FlowrateSqueezeAv { get; set; }
        [ForeignKey(nameof(FlowrateSqueezeMxId))]
        [InverseProperty(nameof(CementJobFlowrateSqueezeMxs.CementJobCementStages))]
        public virtual CementJobFlowrateSqueezeMxs FlowrateSqueezeMx { get; set; }
        [ForeignKey(nameof(Gel10MinId))]
        [InverseProperty(nameof(CementJobGel10Mins.CementJobCementStages))]
        public virtual CementJobGel10Mins Gel10Min { get; set; }
        [ForeignKey(nameof(Gel10SecId))]
        [InverseProperty(nameof(CementJobGel10Secs.CementJobCementStages))]
        public virtual CementJobGel10Secs Gel10Sec { get; set; }
        [ForeignKey(nameof(MdBottomId))]
        [InverseProperty(nameof(CementJobMdBottoms.CementJobCementStages))]
        public virtual CementJobMdBottoms MdBottom { get; set; }
        [ForeignKey(nameof(MdCircOutId))]
        [InverseProperty(nameof(CementJobMdCircOuts.CementJobCementStages))]
        public virtual CementJobMdCircOuts MdCircOut { get; set; }
        [ForeignKey(nameof(MdCoilTbgId))]
        [InverseProperty(nameof(CementJobMdCoilTbgs.CementJobCementStages))]
        public virtual CementJobMdCoilTbgs MdCoilTbg { get; set; }
        [ForeignKey(nameof(MdStringId))]
        [InverseProperty(nameof(CementJobMdStrings.CementJobCementStages))]
        public virtual CementJobMdStrings MdString { get; set; }
        [ForeignKey(nameof(MdToolId))]
        [InverseProperty(nameof(CementJobMdTools.CementJobCementStages))]
        public virtual CementJobMdTools MdTool { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(CementJobMdTops.CementJobCementStages))]
        public virtual CementJobMdTops MdTop { get; set; }
        [ForeignKey(nameof(PresBackPressureId))]
        [InverseProperty(nameof(CementJobPresBackPressures.CementJobCementStages))]
        public virtual CementJobPresBackPressures PresBackPressure { get; set; }
        [ForeignKey(nameof(PresBreakDownId))]
        [InverseProperty(nameof(CementJobPresBreakDowns.CementJobCementStages))]
        public virtual CementJobPresBreakDowns PresBreakDown { get; set; }
        [ForeignKey(nameof(PresBumpId))]
        [InverseProperty(nameof(CementJobPresBumps.CementJobCementStages))]
        public virtual CementJobPresBumps PresBump { get; set; }
        [ForeignKey(nameof(PresCoilTbgEndId))]
        [InverseProperty(nameof(CementJobPresCoilTbgEnds.CementJobCementStages))]
        public virtual CementJobPresCoilTbgEnds PresCoilTbgEnd { get; set; }
        [ForeignKey(nameof(PresCoilTbgStartId))]
        [InverseProperty(nameof(CementJobPresCoilTbgStarts.CementJobCementStages))]
        public virtual CementJobPresCoilTbgStarts PresCoilTbgStart { get; set; }
        [ForeignKey(nameof(PresCsgEndId))]
        [InverseProperty(nameof(CementJobPresCsgEnds.CementJobCementStages))]
        public virtual CementJobPresCsgEnds PresCsgEnd { get; set; }
        [ForeignKey(nameof(PresCsgStartId))]
        [InverseProperty(nameof(CementJobPresCsgStarts.CementJobCementStages))]
        public virtual CementJobPresCsgStarts PresCsgStart { get; set; }
        [ForeignKey(nameof(PresDisplaceId))]
        [InverseProperty(nameof(CementJobPresDisplaces.CementJobCementStages))]
        public virtual CementJobPresDisplaces PresDisplace { get; set; }
        [ForeignKey(nameof(PresHeldId))]
        [InverseProperty(nameof(CementJobPresHelds.CementJobCementStages))]
        public virtual CementJobPresHelds PresHeld { get; set; }
        [ForeignKey(nameof(PresMudCircId))]
        [InverseProperty(nameof(CementJobPresMudCircs.CementJobCementStages))]
        public virtual CementJobPresMudCircs PresMudCirc { get; set; }
        [ForeignKey(nameof(PresPriorBumpId))]
        [InverseProperty(nameof(CementJobPresPriorBumps.CementJobCementStages))]
        public virtual CementJobPresPriorBumps PresPriorBump { get; set; }
        [ForeignKey(nameof(PresSqueezeId))]
        [InverseProperty(nameof(CementJobPresSqueezes.CementJobCementStages))]
        public virtual CementJobPresSqueezes PresSqueeze { get; set; }
        [ForeignKey(nameof(PresSqueezeAvId))]
        [InverseProperty(nameof(CementJobPresSqueezeAvs.CementJobCementStages))]
        public virtual CementJobPresSqueezeAvs PresSqueezeAv { get; set; }
        [ForeignKey(nameof(PresSqueezeEndId))]
        [InverseProperty(nameof(CementJobPresSqueezeEnds.CementJobCementStages))]
        public virtual CementJobPresSqueezeEnds PresSqueezeEnd { get; set; }
        [ForeignKey(nameof(PresTbgEndId))]
        [InverseProperty(nameof(CementJobPresTbgEnds.CementJobCementStages))]
        public virtual CementJobPresTbgEnds PresTbgEnd { get; set; }
        [ForeignKey(nameof(PresTbgStartPresTbgStId))]
        [InverseProperty(nameof(CementJobPresTbgStarts.CementJobCementStages))]
        public virtual CementJobPresTbgStarts PresTbgStartPresTbgSt { get; set; }
        [ForeignKey(nameof(PvMudId))]
        [InverseProperty(nameof(CementJobPvMuds.CementJobCementStages))]
        public virtual CementJobPvMuds PvMud { get; set; }
        [ForeignKey(nameof(TempBhctid))]
        [InverseProperty(nameof(CementJobTempBhcts.CementJobCementStages))]
        public virtual CementJobTempBhcts TempBhct { get; set; }
        [ForeignKey(nameof(TempBhstid))]
        [InverseProperty(nameof(CementJobTempBhsts.CementJobCementStages))]
        public virtual CementJobTempBhsts TempBhst { get; set; }
        [ForeignKey(nameof(VisFunnelMudId))]
        [InverseProperty(nameof(CementJobVisFunnelMuds.CementJobCementStages))]
        public virtual CementJobVisFunnelMuds VisFunnelMud { get; set; }
        [ForeignKey(nameof(VolCircPriorId))]
        [InverseProperty(nameof(CementJobVolCircPriors.CementJobCementStages))]
        public virtual CementJobVolCircPriors VolCircPrior { get; set; }
        [ForeignKey(nameof(VolCsgInId))]
        [InverseProperty(nameof(CementJobVolCsgIns.CementJobCementStages))]
        public virtual CementJobVolCsgIns VolCsgIn { get; set; }
        [ForeignKey(nameof(VolCsgOutId))]
        [InverseProperty(nameof(CementJobVolCsgOuts.CementJobCementStages))]
        public virtual CementJobVolCsgOuts VolCsgOut { get; set; }
        [ForeignKey(nameof(VolDisplaceFluidId))]
        [InverseProperty(nameof(CementJobVolDisplaceFluids.CementJobCementStages))]
        public virtual CementJobVolDisplaceFluids VolDisplaceFluid { get; set; }
        [ForeignKey(nameof(VolExcessId))]
        [InverseProperty(nameof(CementJobVolExcesss.CementJobCementStages))]
        public virtual CementJobVolExcesss VolExcess { get; set; }
        [ForeignKey(nameof(VolMudLostId))]
        [InverseProperty(nameof(CementJobVolMudLosts.CementJobCementStages))]
        public virtual CementJobVolMudLosts VolMudLost { get; set; }
        [ForeignKey(nameof(VolReturnsId))]
        [InverseProperty(nameof(CementJobVolReturnss.CementJobCementStages))]
        public virtual CementJobVolReturnss VolReturns { get; set; }
        [ForeignKey(nameof(WtMudId))]
        [InverseProperty(nameof(CementJobWtMuds.CementJobCementStages))]
        public virtual CementJobWtMuds WtMud { get; set; }
        [ForeignKey(nameof(YpMudId))]
        [InverseProperty(nameof(CementJobYpMuds.CementJobCementStages))]
        public virtual CementJobYpMuds YpMud { get; set; }
        [InverseProperty("CementStage")]
        public virtual ICollection<CementJobs> CementJobs { get; set; }
    }
}
