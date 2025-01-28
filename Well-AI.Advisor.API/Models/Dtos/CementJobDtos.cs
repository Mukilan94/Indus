/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Dtos
{
    public class CementJobMdWaterDto
    {
        [Key]

        public int MdWaterId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobWocDto
    {
        [Key]

        public int WocId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

 
    public class CementJobMdPlugBotDto
    {
        [Key]

        public int MdPlugBotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdHoleDto
    {
        [Key]

        public int MdHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdShoeDto
    {
        [Key]

        public int MdShoeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobTvdShoeDto
    {
        [Key]

        public int TvdShoeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdStringSetDto
    {
        [Key]

        public int MdStringSetId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobTvdStringSetDto
    {
        [Key]

        public int TvdStringSetId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }




    public class CementJobVolExcessDto
    {
        [Key]

        public int VolExcessId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobFlowrateDisplaceAvDto
    {
        [Key]

        public int FlowrateDisplaceAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobFlowrateDisplaceMxDto
    {
        [Key]

        public int FlowrateDisplaceMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobPresDisplaceDto
    {
        [Key]

        public int PresDisplaceId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolReturnsDto
    {
        [Key]

        public int VolReturnsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobETimMudCirculationDto
    {
        [Key]

        public int ETimMudCirculationId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobFlowrateMudCircDto
    {
        [Key]

        public int CementJobFlowrateMudCircId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobPresMudCircDto
    {
        [Key]

        public int PresMudCircId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobFlowrateEndDto
    {
        [Key]

        public int FlowrateEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdFluidTopDto
    {
        [Key]

        public int MdFluidTopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdFluidBottomDto
    {
        [Key]

        public int MdFluidBottomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolWaterDto
    {
        [Key]

        public int VolWaterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolCementDto
    {
        [Key]

        public int VolCementId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobRatioMixWaterDto
    {
        [Key]

        public int RatioMixWaterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolFluidDto
    {
        [Key]

        public int VolFluidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobETimPumpDto
    {
        [Key]

        public int ETimPumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobRatePumpDto
    {
        [Key]
        public int RatePumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolPumpDto
    {
        [Key]

        public int VolPumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobPresBackDto
    {
        [Key]

        public int PresBackId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobETimShutdownDto
    {
        [Key]
        public int ETimShutdownId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobCementPumpScheduleDto
    {
        [Key]

        public int CementPumpScheduleId { get; set; }
        public CementJobETimPumpDto ETimPump { get; set; }

        public CementJobRatePumpDto RatePump { get; set; }

        public CementJobVolPumpDto VolPump { get; set; }

        public string StrokePump { get; set; }

        public CementJobPresBackDto PresBack { get; set; }

        public CementJobETimShutdownDto ETimShutdown { get; set; }
        public string Comments { get; set; }
    }

    public class CementJobExcessPcDto
    {
        [Key]
        public int ExcessPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolYieldDto
    {
        [Key]

        public int VolYieldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }
 
    public class CementJobSolidVolumeFractionDto
    {
        [Key]
        public int SolidVolumeFractionId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolPumpedDto
    {
        [Key]

        public int VolPumpedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolOtherDto
    {
        [Key]

        public int VolOtherId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVisDto
    {
        [Key]

        public int VisId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobYpDto
    {
        [Key]

        public int JobYpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobNDto
    {
        [Key]

        public int NId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobKDto
    {
        [Key]

        public int KId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10SecReadingDto
    {
        [Key]

        public int Gel10SecReadingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10SecStrengthDto
    {
        [Key]

        public int Gel10SecStrengthId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel1MinReadingDto
    {
        [Key]

        public int Gel1MinReadingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel1MinStrengthDto
    {
        [Key]
        public int Gel1MinStrengthId { get; set; }
        public string Text { get; set; }
    }

    public class CementJobGel10MinReadingDto
    {
        [Key]

        public int Gel10MinReadingId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10MinStrengthDto
    {
        [Key]

        public int Gel10MinStrengthId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensBaseFluidDto
    {
        [Key]

        public int DensBaseFluidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMassDryBlendDto
    {
        [Key]

        public int MassDryBlendId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensDryBlendDto
    {
        [Key]

        public int DensDryBlendId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMassSackDryBlendDto
    {
        [Key]

        public int MassSackDryBlendId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensAddDto
    {
        [Key]

        public int DensAddId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobConcentrationDto
    {
        [Key]

        public int ConcentrationId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobAdditiveDto
    {
        [Key]

        public int AdditiveId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobCementAdditiveDto
    {
        [Key]
        public int CementAdditiveId { get; set; }
        public string NameAdd { get; set; }
        public string TypeAdd { get; set; }
        public string FormAdd { get; set; }
        public CementJobDensAddDto DensAdd { get; set; }
        public string TypeConc { get; set; }
        public CementJobConcentrationDto Concentration { get; set; }
        public CementJobAdditiveDto Additive { get; set; }
        public string Uid { get; set; }
    }

    public class CementJobVolGasFoamDto
    {
        [Key]

        public int VolGasFoamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRatioConstGasMethodAvDto
    {
        [Key]

        public int RatioConstGasMethodAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensConstGasMethodDto
    {
        [Key]

        public int DensConstGasMethodId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRatioConstGasMethodStartDto
    {
        [Key]

        public int RatioConstGasMethodStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRatioConstGasMethodEndDto
    {
        [Key]
        public int RatioConstGasMethodEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensConstGasFoamDto
    {
        [Key]

        public int DensConstGasFoamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimThickeningDto
    {
        [Key]
        public int ETimThickeningId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempThickeningDto
    {
        [Key]

        public int TempThickeningId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresTestThickeningDto
    {
        [Key]

        public int PresTestThickeningId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobConsTestThickeningDto
    {
        [Key]

        public int ConsTestThickeningId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPcFreeWaterDto
    {
        [Key]

        public int PcFreeWaterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempFreeWaterDto
    {
        [Key]

        public int TempFreeWaterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolTestFluidLossDto
    {
        [Key]
        public int VolTestFluidLossId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempFluidLossDto
    {
        [Key]

        public int TempFluidLossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresTestFluidLossDto
    {
        [Key]

        public int PresTestFluidLossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTimeFluidLossDto
    {
        [Key]

        public int TimeFluidLossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolAPIFluidLossDto
    {
        [Key]

        public int VolAPIFluidLossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimComprStren1Dto
    {
        [Key]

        public int ETimComprStren1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimComprStren2Dto
    {
        [Key]
        public int ETimComprStren2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresComprStren1Dto
    {
        [Key]

        public int PresComprStren1Id { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresComprStren2Dto
    {
        [Key]

        public int PresComprStren2Id { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempComprStren1Dto
    {
        [Key]

        public int TempComprStren1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobTempComprStren2Dto
    {
        [Key]

        public int TempComprStren2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobDensAtPresDto
    {
        [Key]

        public int DensAtPresId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolReservedDto
    {
        [Key]

        public int VolReservedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobVolTotSlurryDto
    {
        [Key]

        public int VolTotSlurryId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobCementingFluidDto
    {
        [Key]

        public int CementingFluidId { get; set; }

        public int FluidIndex { get; set; }

        public string TypeFluid { get; set; }
        public string DescFluid { get; set; }
        public string Purpose { get; set; }
        public string ClassSlurryDryBlend { get; set; }
        public CementJobMdFluidTopDto MdFluidTop { get; set; }
        public CementJobMdFluidBottomDto MdFluidBottom { get; set; }
        public string SourceWater { get; set; }
        public CementJobVolWaterDto VolWater { get; set; }
        public CementJobVolCementDto VolCement { get; set; }
        public CementJobRatioMixWaterDto RatioMixWater { get; set; }
        public CementJobVolFluidDto VolFluid { get; set; }
        public CementJobCementPumpScheduleDto CementPumpSchedule { get; set; }
        public CementJobExcessPcDto ExcessPc { get; set; }
        public CementJobVolYieldDto VolYield { get; set; }
        public CementJobDensityDto Density { get; set; }
        public CementJobSolidVolumeFractionDto SolidVolumeFraction { get; set; }
        public CementJobVolPumpedDto VolPumped { get; set; }
        public CementJobVolOtherDto VolOther { get; set; }
        public string FluidRheologicalModel { get; set; }
        public CementJobVisDto Vis { get; set; }
        public CementJobYpDto Yp { get; set; }
        public CementJobNDto N { get; set; }
        public CementJobKDto K { get; set; }
        public CementJobGel10SecReadingDto Gel10SecReading { get; set; }
        public CementJobGel10SecStrengthDto Gel10SecStrength { get; set; }
        public CementJobGel1MinReadingDto Gel1MinReading { get; set; }
        public CementJobGel1MinStrengthDto Gel1MinStrength { get; set; }
        public CementJobGel10MinReadingDto Gel10MinReading { get; set; }
        public CementJobGel10MinStrengthDto Gel10MinStrength { get; set; }
        public string TypeBaseFluid { get; set; }
        public CementJobDensBaseFluidDto DensBaseFluid { get; set; }
        public string DryBlendName { get; set; }
        public string DryBlendDescription { get; set; }
        public CementJobMassDryBlendDto MassDryBlend { get; set; }
        public CementJobDensDryBlendDto DensDryBlend { get; set; }
        public CementJobMassSackDryBlendDto MassSackDryBlend { get; set; }
        public CementJobCementAdditiveDto CementAdditive { get; set; }
        public string FoamUsed { get; set; }
        public string TypeGasFoam { get; set; }
        public CementJobVolGasFoamDto VolGasFoam { get; set; }
        public CementJobRatioConstGasMethodAvDto RatioConstGasMethodAv { get; set; }
        public CementJobDensConstGasMethodDto DensConstGasMethod { get; set; }
        public CementJobRatioConstGasMethodStartDto RatioConstGasMethodStart { get; set; }
        public CementJobRatioConstGasMethodEndDto RatioConstGasMethodEnd { get; set; }
        public CementJobDensConstGasFoamDto DensConstGasFoam { get; set; }
        public CementJobETimThickeningDto ETimThickening { get; set; }
        public CementJobTempThickeningDto TempThickening { get; set; }
        public CementJobPresTestThickeningDto PresTestThickening { get; set; }
        public CementJobConsTestThickeningDto ConsTestThickening { get; set; }
        public CementJobPcFreeWaterDto PcFreeWater { get; set; }
        public CementJobTempFreeWaterDto TempFreeWater { get; set; }
        public CementJobVolTestFluidLossDto VolTestFluidLoss { get; set; }
        public CementJobTempFluidLossDto TempFluidLoss { get; set; }
        public CementJobPresTestFluidLossDto PresTestFluidLoss { get; set; }
        public CementJobTimeFluidLossDto TimeFluidLoss { get; set; }
        public CementJobVolAPIFluidLossDto VolAPIFluidLoss { get; set; }
        public CementJobETimComprStren1Dto ETimComprStren1 { get; set; }
        public CementJobETimComprStren2Dto ETimComprStren2 { get; set; }
        public CementJobPresComprStren1Dto PresComprStren1 { get; set; }
        public CementJobPresComprStren2Dto PresComprStren2 { get; set; }
        public CementJobTempComprStren1Dto TempComprStren1 { get; set; }
        public CementJobTempComprStren2Dto TempComprStren2 { get; set; }
        public CementJobDensAtPresDto DensAtPres { get; set; }
        public CementJobVolReservedDto VolReserved { get; set; }
        public CementJobVolTotSlurryDto VolTotSlurry { get; set; }
    }

    public class CementJobMdStringDto
    {
        [Key]

        public int MdStringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdToolDto
    {
        [Key]

        public int MdToolId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdCoilTbgDto
    {
        [Key]

        public int MdCoilTbgId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolCsgInDto
    {
        [Key]

        public int VolCsgInId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolCsgOutDto
    {
        [Key]

        public int VolCsgOutId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDiaTailPipeDto
    {
        [Key]

        public int DiaTailPipeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresTbgStartDto
    {
        [Key]

        public int PresTbgStId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresTbgEndDto
    {
        [Key]

        public int PresTbgEndId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresCsgStartDto
    {
        [Key]

        public int PresCsgStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresCsgEndDto
    {
        [Key]

        public int PresCsgEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresBackPressureDto
    {
        [Key]

        public int PresBackPressureId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresCoilTbgStartDto
    {
        [Key]

        public int PresCoilTbgStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresCoilTbgEndDto
    {
        [Key]

        public int PresCoilTbgEndId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresBreakDownDto
    {
        [Key]

        public int PresBreakDownId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowrateBreakDownDto
    {
        [Key]

        public int FlowrateBreakDownId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresSqueezeAvDto
    {
        [Key]

        public int PresSqueezeAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresSqueezeEndDto
    {
        [Key]

        public int PresSqueezeEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresSqueezeDto
    {
        [Key]

        public int PresSqueezeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimPresHeldDto
    {
        [Key]

        public int ETimPresHeldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowrateSqueezeAvDto
    {
        [Key]

        public int FlowrateSqueezeAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowrateSqueezeMxDto
    {
        [Key]

        public int FlowrateSqueezeMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowratePumpStartDto
    {
        [Key]

        public int FlowratePumpStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFlowratePumpEndDto
    {
        [Key]

        public int FlowratePumpEndId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdCircOutDto
    {
        [Key]

        public int MdCircOutId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolCircPriorDto
    {
        [Key]

        public int VolCircPriorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }
 

    public class CementJobVisFunnelMudDto
    {
        [Key]

        public int VisFunnelMudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPvMudDto
    {
        [Key]

        public int PvMudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobYpMudDto
    {
        [Key]

        public int YpMudId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10SecDto
    {
        [Key]

        public int Gel10SecId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobGel10MinDto
    {
        [Key]

        public int Gel10MinId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobPresPriorBumpDto
    {
        [Key]

        public int PresPriorBumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresBumpDto
    {
        [Key]

        public int PresBumpId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobPresHeldDto
    {
        [Key]
        public int PresHeldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolMudLostDto
    {
        [Key]

        public int VolMudLostId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDensDisplaceFluidDto
    {
        [Key]

        public int DensDisplaceFluidId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobVolDisplaceFluidDto
    {
        [Key]

        public int VolDisplaceFluidId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobWtMudDto
    {
        [Key]

        public int WtMudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobDensityDto
    {
        [Key]


        public int DensityId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdTopDto
    {
        [Key]
        

        public int MdTopId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobMdBottomDto
    {
        [Key]
        

        public int MdBottomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempBHCTDto
    {
        [Key]
        

        public int TempBHCTId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTempBHSTDto
    {
        [Key]
        

        public int TempBHSTId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobCementStageDto
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
        public CementJobMdTopDto MdTop { get; set; }
        public CementJobMdBottomDto MdBottom { get; set; }
        public CementJobVolExcessDto VolExcess { get; set; }
        public CementJobFlowrateDisplaceAvDto FlowrateDisplaceAv { get; set; }
        public CementJobFlowrateDisplaceMxDto FlowrateDisplaceMx { get; set; }
        public CementJobPresDisplaceDto PresDisplace { get; set; }
        public CementJobVolReturnsDto VolReturns { get; set; }
        public CementJobETimMudCirculationDto ETimMudCirculation { get; set; }
        public CementJobFlowrateMudCircDto FlowrateMudCirc { get; set; }
        public CementJobPresMudCircDto PresMudCirc { get; set; }
        public CementJobFlowrateEndDto FlowrateEnd { get; set; }
        public CementJobCementingFluidDto CementingFluid { get; set; }
        public string AfterFlowAnn { get; set; }
        public string SqueezeObj { get; set; }
        public string SqueezeObtained { get; set; }
        public CementJobMdStringDto MdString { get; set; }
        public CementJobMdToolDto MdTool { get; set; }
        public CementJobMdCoilTbgDto MdCoilTbg { get; set; }
        public CementJobVolCsgInDto VolCsgIn { get; set; }
        public CementJobVolCsgOutDto VolCsgOut { get; set; }
        public string TailPipeUsed { get; set; }
        public CementJobDiaTailPipeDto DiaTailPipe { get; set; }
        public string TailPipePerf { get; set; }
        public CementJobPresTbgStartDto PresTbgStart { get; set; }
        public CementJobPresTbgEndDto PresTbgEnd { get; set; }
        public CementJobPresCsgStartDto PresCsgStart { get; set; }
        public CementJobPresCsgEndDto PresCsgEnd { get; set; }
        public CementJobPresBackPressureDto PresBackPressure { get; set; }
        public CementJobPresCoilTbgStartDto PresCoilTbgStart { get; set; }
        public CementJobPresCoilTbgEndDto PresCoilTbgEnd { get; set; }
        public CementJobPresBreakDownDto PresBreakDown { get; set; }
        public CementJobFlowrateBreakDownDto FlowrateBreakDown { get; set; }
        public CementJobPresSqueezeAvDto PresSqueezeAv { get; set; }
        public CementJobPresSqueezeEndDto PresSqueezeEnd { get; set; }
        public string PresSqueezeHeld { get; set; }
        public CementJobPresSqueezeDto PresSqueeze { get; set; }
        public CementJobETimPresHeldDto ETimPresHeld { get; set; }
        public CementJobFlowrateSqueezeAvDto FlowrateSqueezeAv { get; set; }
        public CementJobFlowrateSqueezeMxDto FlowrateSqueezeMx { get; set; }
        public CementJobFlowratePumpStartDto FlowratePumpStart { get; set; }
        public CementJobFlowratePumpEndDto FlowratePumpEnd { get; set; }
        public string PillBelowPlug { get; set; }
        public string PlugCatcher { get; set; }
        public CementJobMdCircOutDto MdCircOut { get; set; }
        public CementJobVolCircPriorDto VolCircPrior { get; set; }
        public string TypeOriginalMud { get; set; }
        public CementJobWtMudDto WtMud { get; set; }
        public CementJobVisFunnelMudDto VisFunnelMud { get; set; }
        public CementJobPvMudDto PvMud { get; set; }
        public CementJobYpMudDto YpMud { get; set; }
        public CementJobGel10SecDto Gel10Sec { get; set; }
        public CementJobGel10MinDto Gel10Min { get; set; }
        public CementJobTempBHCTDto TempBHCT { get; set; }
        public CementJobTempBHSTDto TempBHST { get; set; }
        public string VolExcessMethod { get; set; }
        public string MixMethod { get; set; }
        public string DensMeasBy { get; set; }
        public string AnnFlowAfter { get; set; }
        public string TopPlug { get; set; }
        public string BotPlug { get; set; }
        public string BotPlugNumber { get; set; }
        public string PlugBumped { get; set; }
        public CementJobPresPriorBumpDto PresPriorBump { get; set; }
        public CementJobPresBumpDto PresBump { get; set; }
        public CementJobPresHeldDto PresHeld { get; set; }
        public string FloatHeld { get; set; }
        public CementJobVolMudLostDto VolMudLost { get; set; }
        public string FluidDisplace { get; set; }
        public CementJobDensDisplaceFluidDto DensDisplaceFluid { get; set; }
        public CementJobVolDisplaceFluidDto VolDisplaceFluid { get; set; }


    }

    public class CementJobPresTestDto
    {
        [Key]

        public int PresTestId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimTestDto
    {
        [Key]

        public int ETimTestId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobCblPresDto
    {
        [Key]

        public int CblPresId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimCementLogDto
    {
        [Key]

        public int ETimCementLogId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobFormPitDto
    {
        [Key]

        public int FormPitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimPitStartDto
    {
        [Key]

        public int ETimPitStartId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdCementTopDto
    {
        [Key]

        public int MdCementTopId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobLinerTopDto
    {
        [Key]

        public int LinerTopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobLinerLapDto
    {
        [Key]

        public int LinerLapId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobETimBeforeTestDto
    {
        [Key]

        public int ETimBeforeTestId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTestNegativeEmwDto
    {
        [Key]

        public int TestNegativeEmwId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTestPositiveEmwDto
    {
        [Key]

        public int TestPositiveEmwId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobMdDVToolDto
    {
        [Key]

        public int JobMdDVToolId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobCementTestDto
    {
        [Key]
       
        public int CementTestId { get; set; }
        public CementJobPresTestDto PresTest { get; set; }
        public CementJobETimTestDto ETimTest { get; set; }
        public string CementShoeCollar { get; set; }
        public string CetRun { get; set; }
        public string CetBondQual { get; set; }
        public string CblRun { get; set; }
        public string CblBondQual { get; set; }
        public CementJobCblPresDto CblPres { get; set; }
        public string TempSurvey { get; set; }
        public CementJobETimCementLogDto ETimCementLog { get; set; }
        public CementJobFormPitDto FormPit { get; set; }
        public string ToolCompanyPit { get; set; }
        public CementJobETimPitStartDto ETimPitStart { get; set; }
        public CementJobMdCementTopDto MdCementTop { get; set; }
        public string TopCementMethod { get; set; }
        public string TocOK { get; set; }
        public string JobRating { get; set; }
        public string RemedialCement { get; set; }
        public string NumRemedial { get; set; }
        public string FailureMethod { get; set; }
        public CementJobLinerTopDto LinerTop { get; set; }
        public CementJobLinerLapDto LinerLap { get; set; }
        public CementJobETimBeforeTestDto ETimBeforeTest { get; set; }
        public string TestNegativeTool { get; set; }
        public CementJobTestNegativeEmwDto TestNegativeEmw { get; set; }
        public string TestPositiveTool { get; set; }
        public CementJobTestPositiveEmwDto TestPositiveEmw { get; set; }
        public string CementFoundOnTool { get; set; }
        public CementJobMdDVToolDto MdDVTool { get; set; }
    }

    public class CementJobMdSqueezeDto
    {
        [Key]

        public int MdSqueezeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRpmPipeDto
    {
        [Key]

        public int RpmPipeId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTqInitPipeRotDto
    {
        [Key]

        public int TqInitPipeRotId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTqPipeAvDto
    {
        [Key]

        public int TqPipeAvId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobTqPipeMxDto
    {
        [Key]

        public int TqPipeMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobOverPullDto
    {
        [Key]

        public int OverPullId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class CementJobSlackOffDto
    {
        [Key]

        public int SlackOffId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobRpmPipeRecipDto
    {
        [Key]

        public int RpmPipeRecipId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobLenPipeRecipStrokeDto
    {
        [Key]

        public int LenPipeRecipStrokeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }
 
    public class CementJobCommonDataDto
    {
        [Key]

        public int CommonDataId { get; set; }
        public string ItemState { get; set; }

        public string Comments { get; set; }
    }

    public class CementJobMdPlugTopDto
    {
        [Key]
        public int MdPlugTopId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class CementJobDto
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
        public CementJobMdWaterDto MdWater { get; set; }
        public string ReturnsToSeabed { get; set; }
        public string Reciprocating { get; set; }
        public CementJobWocDto Woc { get; set; }
        public CementJobMdPlugTopDto MdPlugTop { get; set; }
        public CementJobMdPlugBotDto MdPlugBot { get; set; }
        public CementJobMdHoleDto MdHole { get; set; }
        public CementJobMdShoeDto MdShoe { get; set; }
        public CementJobTvdShoeDto TvdShoe { get; set; }
        public CementJobMdStringSetDto MdStringSet { get; set; }
        public CementJobTvdStringSetDto TvdStringSet { get; set; }
        public CementJobCementStageDto CementStage { get; set; }
        public CementJobCementTestDto CementTest { get; set; }
        public string TypePlug { get; set; }
        public string NameCementString { get; set; }
        public string DTimPlugSet { get; set; }
        public string CementDrillOut { get; set; }
        public string DTimCementDrillOut { get; set; }
        public string TypeSqueeze { get; set; }
        public CementJobMdSqueezeDto MdSqueeze { get; set; }
        public string DTimSqueeze { get; set; }
        public string ToolCompany { get; set; }
        public string TypeTool { get; set; }
        public string DTimPipeRotStart { get; set; }
        public string DTimPipeRotEnd { get; set; }
        public CementJobRpmPipeDto RpmPipe { get; set; }
        public CementJobTqInitPipeRotDto TqInitPipeRot { get; set; }
        public CementJobTqPipeAvDto TqPipeAv { get; set; }
        public CementJobTqPipeMxDto TqPipeMx { get; set; }
        public string DTimRecipStart { get; set; }
        public string DTimRecipEnd { get; set; }
        public CementJobOverPullDto OverPull { get; set; }
        public CementJobSlackOffDto SlackOff { get; set; }
        public CementJobRpmPipeRecipDto RpmPipeRecip { get; set; }
        public CementJobLenPipeRecipStrokeDto LenPipeRecipStroke { get; set; }
        public string CoilTubing { get; set; }
        public CementJobCommonDataDto CommonData { get; set; }

        public string UidWellbore { get; set; }
        public string UidWell { get; set; }
    }

} 
