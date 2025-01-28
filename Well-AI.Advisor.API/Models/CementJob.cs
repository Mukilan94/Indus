/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Well_AI.Advisor.API.Models
{

    public class CementJobMdWater
    {
        [Key]

        public int MdWaterId { get; set; }
        
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobWoc
    {
        [Key]

        public int WocId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

 


    public class CementJobMdPlugBot
    {
        [Key]

        public int MdPlugBotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdHole
    {
        [Key]

        public int MdHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdShoe
    {
        [Key]

        public int MdShoeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobTvdShoe
    {
        [Key]

        public int TvdShoeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdStringSet
    {
        [Key]

        public int MdStringSetId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobTvdStringSet
    {
        [Key]

        public int TvdStringSetId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }




    public class CementJobVolExcess
    {
        [Key]

        public int VolExcessId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobFlowrateDisplaceAv
    {
        [Key]

        public int FlowrateDisplaceAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobFlowrateDisplaceMx
    {
        [Key]

        public int FlowrateDisplaceMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobPresDisplace
    {
        [Key]

        public int PresDisplaceId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolReturns
    {
        [Key]

        public int VolReturnsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobETimMudCirculation
    {
        [Key]

        public int ETimMudCirculationId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobFlowrateMudCirc
    {
        [Key]

        public int CementJobFlowrateMudCircId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobPresMudCirc
    {
        [Key]

        public int PresMudCircId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobFlowrateEnd
    {
        [Key]

        public int FlowrateEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdFluidTop
    {
        [Key]

        public int MdFluidTopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdFluidBottom
    {
        [Key]

        public int MdFluidBottomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolWater
    {
        [Key]

        public int VolWaterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolCement
    {
        [Key]

        public int VolCementId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobRatioMixWater
    {
        [Key]

        public int RatioMixWaterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolFluid
    {
        [Key]

        public int VolFluidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobETimPump
    {
        [Key]

        public int ETimPumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobRatePump
    {
        [Key]
        public int RatePumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolPump
    {
        [Key]

        public int VolPumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobPresBack
    {
        [Key]

        public int PresBackId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobETimShutdown
    {
        [Key]
        public int ETimShutdownId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobCementPumpSchedule
    {
        [Key]

        public int CementPumpScheduleId { get; set; }
        public CementJobETimPump ETimPump { get; set; }

        public CementJobRatePump RatePump { get; set; }

        public CementJobVolPump VolPump { get; set; }

        public string StrokePump { get; set; }

        public CementJobPresBack PresBack { get; set; }

        public CementJobETimShutdown ETimShutdown { get; set; }
        public string Comments { get; set; }
    }

    public class CementJobExcessPc
    {
        [Key]
        public int ExcessPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolYield
    {
        [Key]

        public int VolYieldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }
 
    public class CementJobSolidVolumeFraction
    {
        [Key]
        public int SolidVolumeFractionId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolPumped
    {
        [Key]

        public int VolPumpedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolOther
    {
        [Key]

        public int VolOtherId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVis
    {
        [Key]

        public int VisId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobYp
    {
        [Key]

        public int JobYpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobN
    {
        [Key]

        public int NId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobK
    {
        [Key]

        public int KId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10SecReading
    {
        [Key]

        public int Gel10SecReadingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10SecStrength
    {
        [Key]

        public int Gel10SecStrengthId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel1MinReading
    {
        [Key]

        public int Gel1MinReadingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel1MinStrength
    {
        [Key]
        public int Gel1MinStrengthId { get; set; }
        public string Text { get; set; }
    }

    public class CementJobGel10MinReading
    {
        [Key]

        public int Gel10MinReadingId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10MinStrength
    {
        [Key]

        public int Gel10MinStrengthId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensBaseFluid
    {
        [Key]

        public int DensBaseFluidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMassDryBlend
    {
        [Key]

        public int MassDryBlendId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensDryBlend
    {
        [Key]

        public int DensDryBlendId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMassSackDryBlend
    {
        [Key]

        public int MassSackDryBlendId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensAdd
    {
        [Key]

        public int DensAddId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobConcentration
    {
        [Key]

        public int ConcentrationId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobAdditive
    {
        [Key]

        public int AdditiveId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobCementAdditive
    {
        [Key]
        public int CementAdditiveId { get; set; }
        public string NameAdd { get; set; }
        public string TypeAdd { get; set; }
        public string FormAdd { get; set; }
        public CementJobDensAdd DensAdd { get; set; }
        public string TypeConc { get; set; }
        public CementJobConcentration Concentration { get; set; }
        public CementJobAdditive Additive { get; set; }
        public string Uid { get; set; }
    }

    public class CementJobVolGasFoam
    {
        [Key]

        public int VolGasFoamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRatioConstGasMethodAv
    {
        [Key]

        public int RatioConstGasMethodAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensConstGasMethod
    {
        [Key]

        public int DensConstGasMethodId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRatioConstGasMethodStart
    {
        [Key]

        public int RatioConstGasMethodStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRatioConstGasMethodEnd
    {
        [Key]
        public int RatioConstGasMethodEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensConstGasFoam
    {
        [Key]

        public int DensConstGasFoamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimThickening
    {
        [Key]
        public int ETimThickeningId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempThickening
    {
        [Key]

        public int TempThickeningId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresTestThickening
    {
        [Key]

        public int PresTestThickeningId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobConsTestThickening
    {
        [Key]

        public int ConsTestThickeningId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPcFreeWater
    {
        [Key]

        public int PcFreeWaterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempFreeWater
    {
        [Key]

        public int TempFreeWaterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolTestFluidLoss
    {
        [Key]
        public int VolTestFluidLossId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempFluidLoss
    {
        [Key]

        public int TempFluidLossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresTestFluidLoss
    {
        [Key]

        public int PresTestFluidLossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTimeFluidLoss
    {
        [Key]

        public int TimeFluidLossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolAPIFluidLoss
    {
        [Key]

        public int VolAPIFluidLossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimComprStren1
    {
        [Key]

        public int ETimComprStren1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimComprStren2
    {
        [Key]
        public int ETimComprStren2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresComprStren1
    {
        [Key]

        public int PresComprStren1Id { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresComprStren2
    {
        [Key]

        public int PresComprStren2Id { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempComprStren1
    {
        [Key]

        public int TempComprStren1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobTempComprStren2
    {
        [Key]

        public int TempComprStren2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobDensAtPres
    {
        [Key]

        public int DensAtPresId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolReserved
    {
        [Key]

        public int VolReservedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolTotSlurry
    {
        [Key]

        public int VolTotSlurryId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobCementingFluid
    {
        [Key]
        public int CementingFluidId { get; set; }
        
        public int FluidIndex { get; set; }
     
        public string TypeFluid { get; set; }
        public string DescFluid { get; set; }
        public string Purpose { get; set; }
        public string ClassSlurryDryBlend { get; set; }
        public CementJobMdFluidTop MdFluidTop { get; set; }
        public CementJobMdFluidBottom MdFluidBottom { get; set; }
        public string SourceWater { get; set; }
        public CementJobVolWater VolWater { get; set; }
        public CementJobVolCement VolCement { get; set; }
        public CementJobRatioMixWater RatioMixWater { get; set; }
        public CementJobVolFluid VolFluid { get; set; }
        public CementJobCementPumpSchedule CementPumpSchedule { get; set; }
        public CementJobExcessPc ExcessPc { get; set; }
        public CementJobVolYield VolYield { get; set; }
        public CementJobDensity Density { get; set; }
        public CementJobSolidVolumeFraction SolidVolumeFraction { get; set; }
        public CementJobVolPumped VolPumped { get; set; }
        public CementJobVolOther VolOther { get; set; }
        public string FluidRheologicalModel { get; set; }
        public CementJobVis Vis { get; set; }
        public CementJobYp Yp { get; set; }
        public CementJobN N { get; set; }
        public CementJobK K { get; set; }
        public CementJobGel10SecReading Gel10SecReading { get; set; }
        public CementJobGel10SecStrength Gel10SecStrength { get; set; }
        public CementJobGel1MinReading Gel1MinReading { get; set; }
        public CementJobGel1MinStrength Gel1MinStrength { get; set; }
        public CementJobGel10MinReading Gel10MinReading { get; set; }
        public CementJobGel10MinStrength Gel10MinStrength { get; set; }
        public string TypeBaseFluid { get; set; }
        public CementJobDensBaseFluid DensBaseFluid { get; set; }
        public string DryBlendName { get; set; }
        public string DryBlendDescription { get; set; }
        public CementJobMassDryBlend MassDryBlend { get; set; }
        public CementJobDensDryBlend DensDryBlend { get; set; }
        public CementJobMassSackDryBlend MassSackDryBlend { get; set; }
        public CementJobCementAdditive CementAdditive { get; set; }
        public string FoamUsed { get; set; }
        public string TypeGasFoam { get; set; }
        public CementJobVolGasFoam VolGasFoam { get; set; }
        public CementJobRatioConstGasMethodAv RatioConstGasMethodAv { get; set; }
        public CementJobDensConstGasMethod DensConstGasMethod { get; set; }
        public CementJobRatioConstGasMethodStart RatioConstGasMethodStart { get; set; }
        public CementJobRatioConstGasMethodEnd RatioConstGasMethodEnd { get; set; }
        public CementJobDensConstGasFoam DensConstGasFoam { get; set; }
        public CementJobETimThickening ETimThickening { get; set; }
        public CementJobTempThickening TempThickening { get; set; }
        public CementJobPresTestThickening PresTestThickening { get; set; }
        public CementJobConsTestThickening ConsTestThickening { get; set; }
        public CementJobPcFreeWater PcFreeWater { get; set; }
        public CementJobTempFreeWater TempFreeWater { get; set; }
        public CementJobVolTestFluidLoss VolTestFluidLoss { get; set; }
        public CementJobTempFluidLoss TempFluidLoss { get; set; }
        public CementJobPresTestFluidLoss PresTestFluidLoss { get; set; }
        public CementJobTimeFluidLoss TimeFluidLoss { get; set; }
        public CementJobVolAPIFluidLoss VolAPIFluidLoss { get; set; }
        public CementJobETimComprStren1 ETimComprStren1 { get; set; }
        public CementJobETimComprStren2 ETimComprStren2 { get; set; }
        public CementJobPresComprStren1 PresComprStren1 { get; set; }
        public CementJobPresComprStren2 PresComprStren2 { get; set; }
        public CementJobTempComprStren1 TempComprStren1 { get; set; }
        public CementJobTempComprStren2 TempComprStren2 { get; set; }
        public CementJobDensAtPres DensAtPres { get; set; }
        public CementJobVolReserved VolReserved { get; set; }
        public CementJobVolTotSlurry VolTotSlurry { get; set; }
    }

    public class CementJobMdString
    {
        [Key]

        public int MdStringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdTool
    {
        [Key]

        public int MdToolId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdCoilTbg
    {
        [Key]

        public int MdCoilTbgId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolCsgIn
    {
        [Key]

        public int VolCsgInId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolCsgOut
    {
        [Key]

        public int VolCsgOutId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDiaTailPipe
    {
        [Key]

        public int DiaTailPipeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresTbgStart
    {
        [Key]

        public int PresTbgStId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresTbgEnd
    {
        [Key]

        public int PresTbgEndId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresCsgStart
    {
        [Key]

        public int PresCsgStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresCsgEnd
    {
        [Key]

        public int PresCsgEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresBackPressure
    {
        [Key]

        public int PresBackPressureId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresCoilTbgStart
    {
        [Key]

        public int PresCoilTbgStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresCoilTbgEnd
    {
        [Key]

        public int PresCoilTbgEndId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresBreakDown
    {
        [Key]

        public int PresBreakDownId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowrateBreakDown
    {
        [Key]

        public int FlowrateBreakDownId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresSqueezeAv
    {
        [Key]

        public int PresSqueezeAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresSqueezeEnd
    {
        [Key]

        public int PresSqueezeEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresSqueeze
    {
        [Key]

        public int PresSqueezeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimPresHeld
    {
        [Key]

        public int ETimPresHeldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowrateSqueezeAv
    {
        [Key]

        public int FlowrateSqueezeAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowrateSqueezeMx
    {
        [Key]

        public int FlowrateSqueezeMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowratePumpStart
    {
        [Key]

        public int FlowratePumpStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowratePumpEnd
    {
        [Key]

        public int FlowratePumpEndId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdCircOut
    {
        [Key]

        public int MdCircOutId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolCircPrior
    {
        [Key]

        public int VolCircPriorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }
 
    public class CementJobVisFunnelMud
    {
        [Key]

        public int VisFunnelMudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPvMud
    {
        [Key]

        public int PvMudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobYpMud
    {
        [Key]

        public int YpMudId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10Sec
    {
        [Key]

        public int Gel10SecId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10Min
    {
        [Key]

        public int Gel10MinId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobPresPriorBump
    {
        [Key]

        public int PresPriorBumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresBump
    {
        [Key]

        public int PresBumpId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresHeld
    {
        [Key]
        public int PresHeldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolMudLost
    {
        [Key]

        public int VolMudLostId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensDisplaceFluid
    {
        [Key]

        public int DensDisplaceFluidId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolDisplaceFluid
    {
        [Key]

        public int VolDisplaceFluidId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobWtMud
    {
        [Key]
       
        public int WtMudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobDensity
    {
        [Key]
        

        public int DensityId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdTop
    {
        [Key]
        

        public int MdTopId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdBottom
    {
        [Key]
        

        public int MdBottomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempBHCT
    {
        [Key]
        

        public int TempBHCTId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempBHST
    {
        [Key]
        

        public int TempBHSTId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobCementStage
    {
        [Key]
        public int CementStageId { get; set; }

        public string Uid { get; set; }

        public string NumStage { get; set; }
        public string TypeStage { get; set; }
        public string DTimMixStart { get; set; }
        public string DTimPumpStart { get; set; }
        public string DTimPumpEnd { get; set; }
        public string DTimDisplaceStart { get; set; }
        public CementJobMdTop MdTop { get; set; }
        public CementJobMdBottom MdBottom { get; set; }
        public CementJobVolExcess VolExcess { get; set; }
        public CementJobFlowrateDisplaceAv FlowrateDisplaceAv { get; set; }
        public CementJobFlowrateDisplaceMx FlowrateDisplaceMx { get; set; }
        public CementJobPresDisplace PresDisplace { get; set; }
        public CementJobVolReturns VolReturns { get; set; }
        public CementJobETimMudCirculation ETimMudCirculation { get; set; }
        public CementJobFlowrateMudCirc FlowrateMudCirc { get; set; }
        public CementJobPresMudCirc PresMudCirc { get; set; }
        public CementJobFlowrateEnd FlowrateEnd { get; set; }
        public CementJobCementingFluid CementingFluid { get; set; }
        public string AfterFlowAnn { get; set; }
        public string SqueezeObj { get; set; }
        public string SqueezeObtained { get; set; }
        public CementJobMdString MdString { get; set; }
        public CementJobMdTool MdTool { get; set; }
        public CementJobMdCoilTbg MdCoilTbg { get; set; }
        public CementJobVolCsgIn VolCsgIn { get; set; }
        public CementJobVolCsgOut VolCsgOut { get; set; }
        public string TailPipeUsed { get; set; }
        public CementJobDiaTailPipe DiaTailPipe { get; set; }
        public string TailPipePerf { get; set; }
        public CementJobPresTbgStart PresTbgStart { get; set; }
        public CementJobPresTbgEnd PresTbgEnd { get; set; }
        public CementJobPresCsgStart PresCsgStart { get; set; }
        public CementJobPresCsgEnd PresCsgEnd { get; set; }
        public CementJobPresBackPressure PresBackPressure { get; set; }
        public CementJobPresCoilTbgStart PresCoilTbgStart { get; set; }
        public CementJobPresCoilTbgEnd PresCoilTbgEnd { get; set; }
        public CementJobPresBreakDown PresBreakDown { get; set; }
        public CementJobFlowrateBreakDown FlowrateBreakDown { get; set; }
        public CementJobPresSqueezeAv PresSqueezeAv { get; set; }
        public CementJobPresSqueezeEnd PresSqueezeEnd { get; set; }
        public string PresSqueezeHeld { get; set; }
        public CementJobPresSqueeze PresSqueeze { get; set; }
        public CementJobETimPresHeld ETimPresHeld { get; set; }
        public CementJobFlowrateSqueezeAv FlowrateSqueezeAv { get; set; }
        public CementJobFlowrateSqueezeMx FlowrateSqueezeMx { get; set; }
        public CementJobFlowratePumpStart FlowratePumpStart { get; set; }
        public CementJobFlowratePumpEnd FlowratePumpEnd { get; set; }
        public string PillBelowPlug { get; set; }
        public string PlugCatcher { get; set; }
        public CementJobMdCircOut MdCircOut { get; set; }
        public CementJobVolCircPrior VolCircPrior { get; set; }
        public string TypeOriginalMud { get; set; }
        public CementJobWtMud WtMud { get; set; }
        public CementJobVisFunnelMud VisFunnelMud { get; set; }
        public CementJobPvMud PvMud { get; set; }
        public CementJobYpMud YpMud { get; set; }
        public CementJobGel10Sec Gel10Sec { get; set; }
        public CementJobGel10Min Gel10Min { get; set; }
        public CementJobTempBHCT TempBHCT { get; set; }
        public CementJobTempBHST TempBHST { get; set; }
        public string VolExcessMethod { get; set; }
        public string MixMethod { get; set; }
        public string DensMeasBy { get; set; }
        public string AnnFlowAfter { get; set; }
        public string TopPlug { get; set; }
        public string BotPlug { get; set; }
        public string BotPlugNumber { get; set; }
        public string PlugBumped { get; set; }
        public CementJobPresPriorBump PresPriorBump { get; set; }
        public CementJobPresBump PresBump { get; set; }
        public CementJobPresHeld PresHeld { get; set; }
        public string FloatHeld { get; set; }
        public CementJobVolMudLost VolMudLost { get; set; }
        public string FluidDisplace { get; set; }
        public CementJobDensDisplaceFluid DensDisplaceFluid { get; set; }
        public CementJobVolDisplaceFluid VolDisplaceFluid { get; set; }


    }

    public class CementJobPresTest
    {
        [Key]

        public int PresTestId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimTest
    {
        [Key]

        public int ETimTestId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobCblPres
    {
        [Key]

        public int CblPresId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimCementLog
    {
        [Key]

        public int ETimCementLogId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFormPit
    {
        [Key]

        public int FormPitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimPitStart
    {
        [Key]

        public int ETimPitStartId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdCementTop
    {
        [Key]

        public int MdCementTopId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobLinerTop
    {
        [Key]

        public int LinerTopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobLinerLap
    {
        [Key]

        public int LinerLapId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimBeforeTest
    {
        [Key]

        public int ETimBeforeTestId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTestNegativeEmw
    {
        [Key]

        public int TestNegativeEmwId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTestPositiveEmw
    {
        [Key]

        public int TestPositiveEmwId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdDVTool
    {
        [Key]

        public int JobMdDVToolId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobCementTest
    {
        [Key]
        
        public int CementTestId { get; set; }
        public CementJobPresTest PresTest { get; set; }
        public CementJobETimTest ETimTest { get; set; }
        public string CementShoeCollar { get; set; }
        public string CetRun { get; set; }
        public string CetBondQual { get; set; }
        public string CblRun { get; set; }
        public string CblBondQual { get; set; }
        public CementJobCblPres CblPres { get; set; }
        public string TempSurvey { get; set; }
        public CementJobETimCementLog ETimCementLog { get; set; }
        public CementJobFormPit FormPit { get; set; }
        public string ToolCompanyPit { get; set; }
        public CementJobETimPitStart ETimPitStart { get; set; }
        public CementJobMdCementTop MdCementTop { get; set; }
        public string TopCementMethod { get; set; }
        public string TocOK { get; set; }
        public string JobRating { get; set; }
        public string RemedialCement { get; set; }
        public string NumRemedial { get; set; }
        public string FailureMethod { get; set; }
        public CementJobLinerTop LinerTop { get; set; }
        public CementJobLinerLap LinerLap { get; set; }
        public CementJobETimBeforeTest ETimBeforeTest { get; set; }
        public string TestNegativeTool { get; set; }
        public CementJobTestNegativeEmw TestNegativeEmw { get; set; }
        public string TestPositiveTool { get; set; }
        public CementJobTestPositiveEmw TestPositiveEmw { get; set; }
        public string CementFoundOnTool { get; set; }
        public CementJobMdDVTool MdDVTool { get; set; }
    }

    public class CementJobMdSqueeze
    {
        [Key]

        public int MdSqueezeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRpmPipe
    {
        [Key]

        public int RpmPipeId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTqInitPipeRot
    {
        [Key]

        public int TqInitPipeRotId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTqPipeAv
    {
        [Key]

        public int TqPipeAvId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTqPipeMx
    {
        [Key]

        public int TqPipeMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobOverPull
    {
        [Key]

        public int OverPullId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobSlackOff
    {
        [Key]

        public int SlackOffId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRpmPipeRecip
    {
        [Key]

        public int RpmPipeRecipId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobLenPipeRecipStroke
    {
        [Key]

        public int LenPipeRecipStrokeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }
 

    public class CementJobCommonData
    {
        [Key]

        public int CommonDataId { get; set; }
        public string ItemState { get; set; }

        public string Comments { get; set; }
    }

    public class CementJobMdPlugTop
    {
        [Key]
        public int MdPlugTopId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJob
    {
        [Key]
        public int CementJobId { get; set; }
        public string Uid { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string JobType { get; set; }
        public string JobConfig { get; set; }
        public string DTimJob { get; set; }
        public string NameCementedString { get; set; }
        public string NameWorkString { get; set; }
        public string Contractor { get; set; }
        public string CementEngr { get; set; }
        public string OffshoreJob { get; set; }
        public CementJobMdWater MdWater { get; set; }
        public string ReturnsToSeabed { get; set; }
        public string Reciprocating { get; set; }
        public CementJobWoc Woc { get; set; }
        public CementJobMdPlugTop MdPlugTop { get; set; }
        public CementJobMdPlugBot MdPlugBot { get; set; }
        public CementJobMdHole MdHole { get; set; }
        public CementJobMdShoe MdShoe { get; set; }
        public CementJobTvdShoe TvdShoe { get; set; }
        public CementJobMdStringSet MdStringSet { get; set; }
        public CementJobTvdStringSet TvdStringSet { get; set; }
        public CementJobCementStage CementStage { get; set; }
        public CementJobCementTest CementTest { get; set; }
        public string TypePlug { get; set; }
        public string NameCementString { get; set; }
        public string DTimPlugSet { get; set; }
        public string CementDrillOut { get; set; }
        public string DTimCementDrillOut { get; set; }
        public string TypeSqueeze { get; set; }
        public CementJobMdSqueeze MdSqueeze { get; set; }
        public string DTimSqueeze { get; set; }
        public string ToolCompany { get; set; }
        public string TypeTool { get; set; }
        public string DTimPipeRotStart { get; set; }
        public string DTimPipeRotEnd { get; set; }
        public CementJobRpmPipe RpmPipe { get; set; }
        public CementJobTqInitPipeRot TqInitPipeRot { get; set; }
        public CementJobTqPipeAv TqPipeAv { get; set; }
        public CementJobTqPipeMx TqPipeMx { get; set; }
        public string DTimRecipStart { get; set; }
        public string DTimRecipEnd { get; set; }
        public CementJobOverPull OverPull { get; set; }
        public CementJobSlackOff SlackOff { get; set; }
        public CementJobRpmPipeRecip RpmPipeRecip { get; set; }
        public CementJobLenPipeRecipStroke LenPipeRecipStroke { get; set; }
        public string CoilTubing { get; set; }
        public CementJobCommonData CommonData { get; set; }

        public string UidWellbore { get; set; }
        public string UidWell { get; set; }
    }
 
}
