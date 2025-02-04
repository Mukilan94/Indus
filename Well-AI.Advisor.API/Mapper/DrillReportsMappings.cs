﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class DrillReportsMappings : Profile
    {
        public DrillReportsMappings()
        {
            CreateMap<DrillReport, DrillReportDto>().ReverseMap();
            CreateMap<DrillReportWellAlias, DrillReportWellAliasDto>().ReverseMap();
            CreateMap<DrillReportWellboreAlias, DrillReportWellboreAliasDto>().ReverseMap();
            CreateMap<DrillReportElevation, DrillReportElevationDto>().ReverseMap();
            CreateMap<DrillReportWellDatum, DrillReportWellDatumDto>().ReverseMap(); 
            CreateMap<DrillReportElevation, DrillReportElevationDto>().ReverseMap();
            CreateMap<DrillReportGeodeticCRS, DrillReportGeodeticCRSDto>().ReverseMap();
            CreateMap<DrillReportWellCRS, DrillReportWellCRSDto>().ReverseMap();
            CreateMap<DrillReportWellboreInfo, DrillReportWellboreInfoDto>().ReverseMap();
            CreateMap<DrillReportRigAlias, DrillReportRigAliasDto>().ReverseMap();
            CreateMap<DrillReportMd, DrillReportMdDto>().ReverseMap();
            CreateMap<DrillReportTvd, DrillReportTvdDto>().ReverseMap();
            CreateMap<DrillReportMdPlugTop, DrillReportMdPlugTopDto>().ReverseMap();
            CreateMap<DrillReportDiaHole, DrillReportDiaHoleDto>().ReverseMap();
            CreateMap<DrillReportMdDiaHoleStart, DrillReportMdDiaHoleStartDto>().ReverseMap();
            CreateMap<DrillReportDiaPilot, DrillReportDiaPilotDto>().ReverseMap();
            CreateMap<DrillReportMdDiaPilotPlan, DrillReportMdDiaPilotPlanDto>().ReverseMap();
            CreateMap<DrillReportMdKickoff, DrillReportMdKickoffDto>().ReverseMap();
            CreateMap<DrillReportStrengthForm, DrillReportStrengthFormDto>().ReverseMap();
            CreateMap<DrillReportMdStrengthForm, DrillReportMdStrengthFormDto>().ReverseMap();
            CreateMap<DrillReportDiaCsgLast, DrillReportDiaCsgLastDto>().ReverseMap();
            CreateMap<DrillReportMdCsgLast, DrillReportMdCsgLastDto>().ReverseMap();
            CreateMap<DrillReportDistDrill, DrillReportDistDrillDto>().ReverseMap();
            CreateMap<DrillReportRopCurrent, DrillReportRopCurrentDto>().ReverseMap();
            CreateMap<DrillReportStatusInfo, DrillReportStatusInfoDto>().ReverseMap();
            CreateMap<DrillReportTvd, DrillReportTvdDto>().ReverseMap();
            CreateMap<DrillReportMdPlugTop, DrillReportMdPlugTopDto>().ReverseMap();
            CreateMap<DrillReportDiaHole, DrillReportDiaHoleDto>().ReverseMap();
            CreateMap<DrillReportMdDiaHoleStart, DrillReportMdDiaHoleStartDto>().ReverseMap();
            CreateMap<DrillReportDiaPilot, DrillReportDiaPilotDto>().ReverseMap();
            CreateMap<DrillReportMdDiaPilotPlan, DrillReportMdDiaPilotPlanDto>().ReverseMap();
            CreateMap<DrillReportMdKickoff, DrillReportMdKickoffDto>().ReverseMap();
            CreateMap<DrillReportStrengthForm, DrillReportStrengthFormDto>().ReverseMap();
            CreateMap<DrillReportMdStrengthForm, DrillReportMdStrengthFormDto>().ReverseMap();
            CreateMap<DrillReportDistDrill, DrillReportDistDrillDto>().ReverseMap();
            CreateMap<DrillReportRopCurrent, DrillReportRopCurrentDto>().ReverseMap();
            CreateMap<DrillReportDiaBit, DrillReportDiaBitDto>().ReverseMap();
            CreateMap<DrillReportPresBopRating, DrillReportPresBopRatingDto>().ReverseMap();
            CreateMap<DrillReportDensity, DrillReportDensityDto>().ReverseMap();
            CreateMap<DrillReportTempVis, DrillReportTempVisDto>().ReverseMap();
            CreateMap<DrillReportPv, DrillReportPvDto>().ReverseMap();
            CreateMap<DrillReportFluid, DrillReportFluidDto>().ReverseMap();
            CreateMap<DrillReportAzi, DrillReportAziDto>().ReverseMap();
            CreateMap<DrillReportEquivalentMudWeight, DrillReportEquivalentMudWeightDto>().ReverseMap();
            CreateMap<DrillReportPorePressure, DrillReportPorePressureDto>().ReverseMap();
            CreateMap<DrillReportExtendedReport, DrillReportExtendedReportDto>().ReverseMap();
            CreateMap<DrillReportIncl, DrillReportInclDto>().ReverseMap();
            CreateMap<DrillReportActivity, DrillReportActivityDto>().ReverseMap();
            CreateMap<DrillReportMdTop, DrillReportMdTopDto>().ReverseMap();
            CreateMap<DrillReportTvdBottom, DrillReportTvdBottomDto>().ReverseMap();
            CreateMap<DrillReportTempBHCT, DrillReportTempBHCTDto>().ReverseMap();
            CreateMap<DrillReportMdTempTool, DrillReportMdTempToolDto>().ReverseMap(); 
            CreateMap<DrillReportTvdTempTool, DrillReportTvdTempToolDto>().ReverseMap();
            CreateMap<DrillReportLogInfo, DrillReportLogInfoDto>().ReverseMap();
            CreateMap<DrillReportMdBottom, DrillReportMdBottomDto>().ReverseMap();
            CreateMap<DrillReportTvdTop, DrillReportTvdTopDto>().ReverseMap();
            CreateMap<DrillReportTempBHST, DrillReportTempBHSTDto>().ReverseMap();
            CreateMap<DrillReportETimStatic, DrillReportETimStaticDto>().ReverseMap(); 
            CreateMap<DrillReportLenRecovered, DrillReportLenRecoveredDto>().ReverseMap();
            CreateMap<DrillReportRecoverPc, DrillReportRecoverPcDto>().ReverseMap();
            CreateMap<DrillReportLenBarrel, DrillReportLenBarrelDto>().ReverseMap();
            CreateMap<DrillReportCoreInfo, DrillReportCoreInfoDto>().ReverseMap();
            CreateMap<DrillReportChokeOrificeSize, DrillReportChokeOrificeSizeDto>().ReverseMap();
            CreateMap<DrillReportDensityOil, DrillReportDensityOilDto>().ReverseMap();
            CreateMap<DrillReportDensityWater, DrillReportDensityWaterDto>().ReverseMap();
            CreateMap<DrillReportDensityGas, DrillReportDensityGasDto>().ReverseMap();
            CreateMap<DrillReportFlowRateOil, DrillReportFlowRateOilDto>().ReverseMap();
            CreateMap<DrillReportFlowRateWater, DrillReportFlowRateWaterDto>().ReverseMap();
            CreateMap<DrillReportFlowRateGas, DrillReportFlowRateGasDto>().ReverseMap();
            CreateMap<DrillReportPresShutIn, DrillReportPresShutInDto>().ReverseMap();
            CreateMap<DrillReportPresFlowing, DrillReportPresFlowingDto>().ReverseMap();
            CreateMap<DrillReportPresBottom, DrillReportPresBottomDto>().ReverseMap();
            CreateMap<DrillReportGasOilRatio, DrillReportGasOilRatioDto>().ReverseMap();
            CreateMap<DrillReportWaterOilRatio, DrillReportWaterOilRatioDto>().ReverseMap();
            CreateMap<DrillReportChloride, DrillReportChlorideDto>().ReverseMap();
            CreateMap<DrillReportCarbonDioxide, DrillReportCarbonDioxideDto>().ReverseMap();
            CreateMap<DrillReportHydrogenSulfide, DrillReportHydrogenSulfideDto>().ReverseMap();
            CreateMap<DrillReportVolGasTotal, DrillReportVolGasTotalDto>().ReverseMap();
            CreateMap<DrillReportVolWaterTotal, DrillReportVolWaterTotalDto>().ReverseMap();
            CreateMap<DrillReportVolOilStored, DrillReportVolOilStoredDto>().ReverseMap();
            CreateMap<DrillReportWellTestInfo, DrillReportWellTestInfoDto>().ReverseMap();
            CreateMap<DrillReportVolOilTotal, DrillReportVolOilTotalDto>().ReverseMap();
            CreateMap<DrillReportPresPore, DrillReportPresPoreDto>().ReverseMap();
            CreateMap<DrillReportMdSample, DrillReportMdSampleDto>().ReverseMap();
            CreateMap<DrillReportDensityHC, DrillReportDensityHCDto>().ReverseMap();
            CreateMap<DrillReportVolumeSample, DrillReportVolumeSampleDto>().ReverseMap();
            CreateMap<DrillReportFormTestInfo, DrillReportFormTestInfoDto>().ReverseMap();
            CreateMap<DrillReportLithShowInfo, DrillReportLithShowInfoDto>().ReverseMap();
            CreateMap<DrillReportETimMissProduction, DrillReportETimMissProductionDto>().ReverseMap();
            CreateMap<DrillReportEquipFailureInfo, DrillReportEquipFailureInfoDto>().ReverseMap();
            CreateMap<DrillReportMdInflow, DrillReportMdInflowDto>().ReverseMap();
            CreateMap<DrillReportTvdInflow, DrillReportTvdInflowDto>().ReverseMap();
            CreateMap<DrillReportETimLost, DrillReportETimLostDto>().ReverseMap();
            CreateMap<DrillReportMdBit, DrillReportMdBitDto>().ReverseMap();
            CreateMap<DrillReportVolMudGained, DrillReportVolMudGainedDto>().ReverseMap();
            CreateMap<DrillReportPresShutInCasing, DrillReportPresShutInCasingDto>().ReverseMap();
            CreateMap<DrillReportPresShutInDrill, DrillReportPresShutInDrillDto>().ReverseMap();
            CreateMap<DrillReportTempBottom, DrillReportTempBottomDto>().ReverseMap();
            CreateMap<DrillReportPresMaxChoke, DrillReportPresMaxChokeDto>().ReverseMap();
            CreateMap<DrillReportControlIncidentInfo, DrillReportControlIncidentInfoDto>().ReverseMap();
            CreateMap<DrillReportWtMud, DrillReportWtMudDto>().ReverseMap();
            CreateMap<DrillReportStratInfo, DrillReportStratInfoDto>().ReverseMap();
            CreateMap<DrillReportPerfInfo, DrillReportPerfInfoDto>().ReverseMap();
            CreateMap<DrillReportGasHigh, DrillReportGasHighDto>().ReverseMap();
            CreateMap<DrillReportGasLow, DrillReportGasLowDto>().ReverseMap();
            CreateMap<DrillReportMeth, DrillReportMethDto>().ReverseMap();
            CreateMap<DrillReportEth, DrillReportEthDto>().ReverseMap();
            CreateMap<DrillReportProp, DrillReportPropDto>().ReverseMap();
            CreateMap<DrillReportIbut, DrillReportIbutDto>().ReverseMap();
            CreateMap<DrillReportNbut, DrillReportNbutDto>().ReverseMap();
            CreateMap<DrillReportIpent, DrillReportIpentDto>().ReverseMap();
            CreateMap<DrillReportDefaultDatum, DrillReportDefaultDatumDto>().ReverseMap();
            CreateMap<DrillReportBitRecord, DrillReportBitRecordDto>().ReverseMap();
            CreateMap<DrillReportSurveyStation, DrillReportSurveyStationDto>().ReverseMap();
            CreateMap<DrillReportActivity, DrillReportActivityDto>().ReverseMap();
            CreateMap<DrillReportGasReadingInfo, DrillReportGasReadingInfoDto>().ReverseMap();
            CreateMap<DrillReportCommonData, DrillReportCommonDataDto>().ReverseMap();
           
           
        }

        
    }
}
