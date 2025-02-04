﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class CementJobMappings:Profile
    {
        public CementJobMappings()
        {
            CreateMap<CementJob, CementJobDto>().ReverseMap();
            CreateMap<CementJobMdWater, CementJobMdWaterDto>().ReverseMap();
            CreateMap<CementJobWoc, CementJobWocDto>().ReverseMap();
            CreateMap<CementJobMdPlugTop, CementJobMdPlugTopDto>().ReverseMap();
            CreateMap<CementJobMdPlugBot, CementJobMdPlugBotDto>().ReverseMap();
            CreateMap<CementJobMdHole, CementJobMdHoleDto>().ReverseMap(); 
            CreateMap<CementJobMdShoe, CementJobMdShoeDto>().ReverseMap();
            CreateMap<CementJobTvdShoe, CementJobTvdShoeDto>().ReverseMap();
            CreateMap<CementJobMdStringSet, CementJobMdStringSetDto>().ReverseMap();
            CreateMap<CementJobTvdStringSet, CementJobTvdStringSetDto>().ReverseMap();
            CreateMap<CementJobVolExcess, CementJobVolExcessDto>().ReverseMap();
            CreateMap<CementJobFlowrateDisplaceAv, CementJobFlowrateDisplaceAvDto>().ReverseMap();
            CreateMap<CementJobFlowrateDisplaceMx, CementJobFlowrateDisplaceMxDto>().ReverseMap();
            CreateMap<CementJobPresDisplace, CementJobPresDisplaceDto>().ReverseMap();
            CreateMap<CementJobVolReturns, CementJobVolReturnsDto>().ReverseMap();
            CreateMap<CementJobETimMudCirculation, CementJobETimMudCirculationDto>().ReverseMap();
            CreateMap<CementJobFlowrateMudCirc, CementJobFlowrateMudCircDto>().ReverseMap();
            CreateMap<CementJobPresMudCirc, CementJobPresMudCircDto>().ReverseMap();
            CreateMap<CementJobFlowrateEnd, CementJobFlowrateEndDto>().ReverseMap();
            CreateMap<CementJobMdFluidTop, CementJobMdFluidTopDto>().ReverseMap();
            CreateMap<CementJobMdFluidBottom, CementJobMdFluidBottomDto>().ReverseMap();
            CreateMap<CementJobVolWater, CementJobVolWaterDto>().ReverseMap();
            CreateMap<CementJobVolCement, CementJobVolCementDto>().ReverseMap();
            CreateMap<CementJobRatioMixWater, CementJobRatioMixWaterDto>().ReverseMap();
            CreateMap<CementJobVolFluid, CementJobVolFluidDto>().ReverseMap();
            CreateMap<CementJobMdTop, CementJobMdTopDto>().ReverseMap();
            CreateMap<CementJobMdBottom, CementJobMdBottomDto>().ReverseMap();
            CreateMap<CementJobTempBHCT, CementJobTempBHCTDto>().ReverseMap();
            CreateMap<CementJobTempBHST, CementJobTempBHSTDto>().ReverseMap();
            CreateMap<CementJobETimPump, CementJobETimPumpDto>().ReverseMap();
            CreateMap<CementJobRatePump, CementJobRatePumpDto>().ReverseMap();
            CreateMap<CementJobVolPump, CementJobVolPumpDto>().ReverseMap();
            CreateMap<CementJobPresBack, CementJobPresBackDto>().ReverseMap();
            CreateMap<CementJobETimShutdown, CementJobETimShutdownDto>().ReverseMap();
            CreateMap<CementJobCementPumpSchedule, CementJobCementPumpScheduleDto>().ReverseMap();
            CreateMap<CementJobExcessPc, CementJobExcessPcDto>().ReverseMap();
            CreateMap<CementJobVolYield, CementJobVolYieldDto>().ReverseMap();
            CreateMap<CementJobSolidVolumeFraction, CementJobSolidVolumeFractionDto>().ReverseMap();
            CreateMap<CementJobVolPumped, CementJobVolPumpedDto>().ReverseMap();
            CreateMap<CementJobVolOther, CementJobVolOtherDto>().ReverseMap();
            CreateMap<CementJobVis, CementJobVisDto>().ReverseMap();
            CreateMap<CementJobYp, CementJobYpDto>().ReverseMap();
            CreateMap<CementJobN, CementJobNDto>().ReverseMap();
            CreateMap<CementJobK, CementJobKDto>().ReverseMap();
            CreateMap<CementJobGel10SecReading, CementJobGel10SecReadingDto>().ReverseMap();
            CreateMap<CementJobGel10SecStrength, CementJobGel10SecStrengthDto>().ReverseMap();
            CreateMap<CementJobGel1MinReading, CementJobGel1MinReadingDto>().ReverseMap();
            CreateMap<CementJobGel1MinStrength, CementJobGel1MinStrengthDto>().ReverseMap();
            CreateMap<CementJobGel10MinReading, CementJobGel10MinReadingDto>().ReverseMap();
            CreateMap<CementJobGel10MinStrength, CementJobGel10MinStrengthDto>().ReverseMap();
            CreateMap<CementJobDensBaseFluid, CementJobDensBaseFluidDto>().ReverseMap();
            CreateMap<CementJobMassDryBlend, CementJobMassDryBlendDto>().ReverseMap();
            CreateMap<CementJobMassSackDryBlend, CementJobMassSackDryBlendDto>().ReverseMap();
            CreateMap<CementJobDensAdd, CementJobDensAddDto>().ReverseMap();
            CreateMap<CementJobAdditive, CementJobAdditiveDto>().ReverseMap();
            CreateMap<CementJobCementAdditive, CementJobCementAdditiveDto>().ReverseMap();
            CreateMap<CementJobVolGasFoam, CementJobVolGasFoamDto>().ReverseMap();
            CreateMap<CementJobRatioConstGasMethodAv, CementJobRatioConstGasMethodAvDto>().ReverseMap();
            CreateMap<CementJobDensConstGasMethod, CementJobDensConstGasMethodDto>().ReverseMap();
            CreateMap<CementJobRatioConstGasMethodStart, CementJobRatioConstGasMethodStartDto>().ReverseMap(); 
            CreateMap<CementJobRatioConstGasMethodEnd, CementJobRatioConstGasMethodEndDto>().ReverseMap();
            CreateMap<CementJobDensConstGasFoam, CementJobDensConstGasFoamDto>().ReverseMap();
            CreateMap<CementJobETimThickening, CementJobETimThickeningDto>().ReverseMap();
            CreateMap<CementJobTempThickening, CementJobTempThickeningDto>().ReverseMap();
            CreateMap<CementJobPresTestThickening, CementJobPresTestThickeningDto>().ReverseMap();
            CreateMap<CementJobConsTestThickening, CementJobConsTestThickeningDto>().ReverseMap(); 
            CreateMap<CementJobPcFreeWater, CementJobPcFreeWaterDto>().ReverseMap();
            CreateMap<CementJobTempFreeWater, CementJobTempFreeWaterDto>().ReverseMap();
            CreateMap<CementJobVolTestFluidLoss, CementJobVolTestFluidLossDto>().ReverseMap();
            CreateMap<CementJobTempFluidLoss, CementJobTempFluidLossDto>().ReverseMap();
            CreateMap<CementJobVolAPIFluidLoss, CementJobVolAPIFluidLossDto>().ReverseMap();
            CreateMap<CementJobETimComprStren1, CementJobETimComprStren1Dto>().ReverseMap();
            CreateMap<CementJobETimComprStren2, CementJobETimComprStren2Dto>().ReverseMap();
            CreateMap<CementJobPresComprStren1, CementJobPresComprStren1Dto>().ReverseMap();
            CreateMap<CementJobPresComprStren2, CementJobPresComprStren2Dto>().ReverseMap();
            CreateMap<CementJobTempComprStren1, CementJobTempComprStren1Dto>().ReverseMap();
            CreateMap<CementJobTempComprStren2, CementJobTempComprStren2Dto>().ReverseMap();
            CreateMap<CementJobDensAtPres, CementJobDensAtPresDto>().ReverseMap();
            CreateMap<CementJobVolReserved, CementJobVolReservedDto>().ReverseMap();
            CreateMap<CementJobVolTotSlurry, CementJobVolTotSlurryDto>().ReverseMap();
            CreateMap<CementJobCementingFluid, CementJobCementingFluidDto>().ReverseMap();
            CreateMap<CementJobMdString, CementJobMdStringDto>().ReverseMap();
            CreateMap<CementJobMdTool, CementJobMdToolDto>().ReverseMap();
            CreateMap<CementJobMdCoilTbg, CementJobMdCoilTbgDto>().ReverseMap();
            CreateMap<CementJobVolCsgIn, CementJobVolCsgInDto>().ReverseMap();
            CreateMap<CementJobVolCsgOut, CementJobVolCsgOutDto>().ReverseMap();
            CreateMap<CementJobDiaTailPipe, CementJobDiaTailPipeDto>().ReverseMap();
            CreateMap<CementJobPresTbgStart, CementJobPresTbgStartDto>().ReverseMap();
            CreateMap<CementJobPresTbgEnd, CementJobPresTbgEndDto>().ReverseMap();
            CreateMap<CementJobPresCsgStart, CementJobPresCsgStartDto>().ReverseMap();
            CreateMap<CementJobPresCsgEnd, CementJobPresCsgEndDto>().ReverseMap();
            CreateMap<CementJobPresBackPressure, CementJobPresBackPressureDto>().ReverseMap();
            CreateMap<CementJobPresCoilTbgStart, CementJobPresCoilTbgStartDto>().ReverseMap();
            CreateMap<CementJobPresCoilTbgEnd, CementJobPresCoilTbgEndDto>().ReverseMap();
            CreateMap<CementJobPresBreakDown, CementJobPresBreakDownDto>().ReverseMap();
            CreateMap<CementJobFlowrateBreakDown, CementJobFlowrateBreakDownDto>().ReverseMap();
            CreateMap<CementJobPresSqueezeAv, CementJobPresSqueezeAvDto>().ReverseMap();
            CreateMap<CementJobPresSqueezeEnd, CementJobPresSqueezeEndDto>().ReverseMap();
            CreateMap<CementJobPresSqueeze, CementJobPresSqueezeDto>().ReverseMap();
            CreateMap<CementJobETimPresHeld, CementJobETimPresHeldDto>().ReverseMap();
            CreateMap<CementJobFlowrateSqueezeAv, CementJobFlowrateSqueezeAvDto>().ReverseMap();
            CreateMap<CementJobFlowrateSqueezeMx, CementJobFlowrateSqueezeMxDto>().ReverseMap();
            CreateMap<CementJobFlowratePumpStart, CementJobFlowratePumpStartDto>().ReverseMap();
            CreateMap<CementJobFlowratePumpEnd, CementJobFlowratePumpEndDto>().ReverseMap();
            CreateMap<CementJobMdCircOut, CementJobMdCircOutDto>().ReverseMap();
            CreateMap<CementJobVolCircPrior, CementJobVolCircPriorDto>().ReverseMap();
            CreateMap<CementJobVisFunnelMud, CementJobVisFunnelMudDto>().ReverseMap();
            CreateMap<CementJobPvMud, CementJobPvMudDto>().ReverseMap();
            CreateMap<CementJobYpMud, CementJobYpMudDto>().ReverseMap();
            CreateMap<CementJobPresHeld, CementJobPresHeldDto>().ReverseMap();
            CreateMap<CementJobVolMudLost, CementJobVolMudLostDto>().ReverseMap();
            CreateMap<CementJobDensDisplaceFluid, CementJobDensDisplaceFluidDto>().ReverseMap();
            CreateMap<CementJobVolDisplaceFluid, CementJobVolDisplaceFluidDto>().ReverseMap();
            CreateMap<CementJobCementStage, CementJobCementStageDto>().ReverseMap();
            CreateMap<CementJobPresTest, CementJobPresTestDto>().ReverseMap();
            CreateMap<CementJobETimTest, CementJobETimTestDto>().ReverseMap();
            CreateMap<CementJobCblPres, CementJobCblPresDto>().ReverseMap();
            CreateMap<CementJobETimCementLog, CementJobETimCementLogDto>().ReverseMap();
            CreateMap<CementJobFormPit, CementJobFormPitDto>().ReverseMap();
            CreateMap<CementJobETimPitStart, CementJobETimPitStartDto>().ReverseMap();
            CreateMap<CementJobMdCementTop, CementJobMdCementTopDto>().ReverseMap();
            CreateMap<CementJobLinerTop, CementJobLinerTopDto>().ReverseMap();
            CreateMap<CementJobDensity, CementJobDensityDto>().ReverseMap();
            CreateMap<CementJobLinerLap, CementJobLinerLapDto>().ReverseMap();
            CreateMap<CementJobETimBeforeTest, CementJobETimBeforeTestDto>().ReverseMap();
            CreateMap<CementJobTestNegativeEmw, CementJobTestNegativeEmwDto>().ReverseMap();
            CreateMap<CementJobTestPositiveEmw, CementJobTestPositiveEmwDto>().ReverseMap();
            CreateMap<CementJobMdDVTool, CementJobMdDVToolDto>().ReverseMap();
            CreateMap<CementJobCementTest, CementJobCementTestDto>().ReverseMap();
            CreateMap<CementJobMdSqueeze, CementJobMdSqueezeDto>().ReverseMap();
            CreateMap<CementJobRpmPipe, CementJobRpmPipeDto>().ReverseMap();
            CreateMap<CementJobTqInitPipeRot, CementJobTqInitPipeRotDto>().ReverseMap();
            CreateMap<CementJobTqPipeAv, CementJobTqPipeAvDto>().ReverseMap();
            CreateMap<CementJobRpmPipeRecip, CementJobRpmPipeRecipDto>().ReverseMap();
            CreateMap<CementJobLenPipeRecipStroke, CementJobLenPipeRecipStrokeDto>().ReverseMap();
            CreateMap<CementJobCommonData, CementJobCommonDataDto>().ReverseMap();
            CreateMap<CementJobDensDryBlend, CementJobDensDryBlendDto>().ReverseMap();
            CreateMap<CementJobConcentration, CementJobConcentrationDto>().ReverseMap();
            CreateMap<CementJobPresTestFluidLoss, CementJobPresTestFluidLossDto>().ReverseMap();
            CreateMap<CementJobTimeFluidLoss, CementJobTimeFluidLossDto>().ReverseMap();
            CreateMap<CementJobGel10Sec, CementJobGel10SecDto>().ReverseMap();
            CreateMap<CementJobGel10Min, CementJobGel10MinDto>().ReverseMap();
            CreateMap<CementJobPresPriorBump, CementJobPresPriorBumpDto>().ReverseMap();
            CreateMap<CementJobPresBump, CementJobPresBumpDto>().ReverseMap();
            CreateMap<CementJobTqPipeMx, CementJobTqPipeMxDto>().ReverseMap();
            CreateMap<CementJobOverPull, CementJobOverPullDto>().ReverseMap();
            CreateMap<CementJobSlackOff, CementJobSlackOffDto>().ReverseMap();
            CreateMap<CementJobWtMud, CementJobWtMudDto>().ReverseMap();


        }

        
    }
}
