using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class TubularMappings : Profile
    {
        public TubularMappings()
        {
            CreateMap<TubularDiaHoleAssy, TubularDiaHoleAssyDto>().ReverseMap();
            CreateMap<TubularDiaBit, TubularDiaBitDto>().ReverseMap();
            CreateMap<TubularOdDrift, TubularOdDriftDto>().ReverseMap();
            CreateMap<TubularId, TubularIdDto>().ReverseMap();
            CreateMap<TubularOd, TubularOdDto>().ReverseMap();
            CreateMap<TubularLen, TubularLenDto>().ReverseMap();
            CreateMap<TubularLenJointAv, TubularLenJointAvDto>().ReverseMap();
            CreateMap<TubularTensYield, TubularTensYieldDto>().ReverseMap();
            CreateMap<TubularTqYield, TubularTqYieldDto>().ReverseMap();
            CreateMap<TubularStressFatig, TubularStressFatigDto>().ReverseMap();
            CreateMap<TubularLenFishneck, TubularLenFishneckDto>().ReverseMap();
            CreateMap<TubularIdFishneck, TubularIdFishneckDto>().ReverseMap();
            CreateMap<TubularOdFishneck, TubularOdFishneckDto>().ReverseMap();
            CreateMap<TubularDisp, TubularDispDto>().ReverseMap();
            CreateMap<TubularPresBurst, TubularPresBurstDto>().ReverseMap();
            CreateMap<TubularPresCollapse, TubularPresCollapseDto>().ReverseMap();
            CreateMap<TubularWearWall, TubularWearWallDto>().ReverseMap();
            CreateMap<TubularThickWall, TubularThickWallDto>().ReverseMap();
            CreateMap<TubularBendStiffness, TubularBendStiffnessDto>().ReverseMap();
            CreateMap<TubularAxialStiffness, TubularAxialStiffnessDto>().ReverseMap();
            CreateMap<TubularTorsionalStiffness, TubularTorsionalStiffnessDto>().ReverseMap();
            CreateMap<TubularDoglegMx, TubularDoglegMxDto>().ReverseMap();
            CreateMap<TubularNameTag, TubularNameTagDto>().ReverseMap();
            CreateMap<TubularDiaPassThru, TubularDiaPassThruDto>().ReverseMap();
            CreateMap<TubularWtPerLen, TubularWtPerLenDto>().ReverseMap();
            CreateMap<TubularDiaPilot, TubularDiaPilotDto>().ReverseMap();
            CreateMap<TubularCost, TubularCostDto>().ReverseMap();
            CreateMap<TubularBitRecord, TubularBitRecordDto>().ReverseMap();
            CreateMap<TubularAreaNozzleFlow, TubularAreaNozzleFlowDto>().ReverseMap();
            CreateMap<TubularDiaNozzle, TubularDiaNozzleDto>().ReverseMap();
            CreateMap<TubularNozzle, TubularNozzleDto>().ReverseMap();
            CreateMap<TubularHoleOpener, TubularHoleOpenerDto>().ReverseMap();
            CreateMap<TubularStabilizer, TubularStabilizerDto>().ReverseMap();
            CreateMap<TubularMotor, TubularMotorDto>().ReverseMap();
            CreateMap<TubularOffsetTool, TubularOffsetToolDto>().ReverseMap();
            CreateMap<TubularFlowrateMn, TubularFlowrateMnDto>().ReverseMap();
            CreateMap<TubularFlowrateMx, TubularFlowrateMxDto>().ReverseMap();
            CreateMap<TubularDiaRotorNozzle, TubularDiaRotorNozzleDto>().ReverseMap();
            CreateMap<TubularClearanceBearBox, TubularClearanceBearBoxDto>().ReverseMap();
            CreateMap<TubularTempOpMx, TubularTempOpMxDto>().ReverseMap();
            CreateMap<TubularBendSettingsMn, TubularBendSettingsMnDto>().ReverseMap();
            CreateMap<TubularBendSettingsMx, TubularBendSettingsMxDto>().ReverseMap();
            CreateMap<TubularBend, TubularBendDto>().ReverseMap();
            CreateMap<TubularMwdTool, TubularMwdToolDto>().ReverseMap();
            CreateMap<TubularConnection, TubularConnectionDto>().ReverseMap();
            CreateMap<TubularJar, TubularJarDto>().ReverseMap();
            CreateMap<TubularOdBladeMn, TubularOdBladeMnDto>().ReverseMap();
            CreateMap<TubularDiaHoleOpener, TubularDiaHoleOpenerDto>().ReverseMap();
            CreateMap<TubularLenBlade, TubularLenBladeDto>().ReverseMap();
            CreateMap<TubularOdBladeMx, TubularOdBladeMxDto>().ReverseMap();
            CreateMap<TubularDistBladeBot, TubularDistBladeBotDto>().ReverseMap();
            CreateMap<TubularAngle, TubularAngleDto>().ReverseMap();
            CreateMap<TubularDistBendBot, TubularDistBendBotDto>().ReverseMap();
            CreateMap<TubularTempMx, TubularTempMxDto>().ReverseMap();
            CreateMap<TubularIdEquv, TubularIdEquvDto>().ReverseMap();
            CreateMap<TubularOffsetBot, TubularOffsetBotDto>().ReverseMap();
            CreateMap<TubularSensor, TubularSensorDto>().ReverseMap();
            CreateMap<TubularSizeThread, TubularSizeThreadDto>().ReverseMap();
            CreateMap<TubularCriticalCrossSection, TubularCriticalCrossSectionDto>().ReverseMap();
            CreateMap<TubularPresLeak, TubularPresLeakDto>().ReverseMap();
            CreateMap<TubularTqMakeup, TubularTqMakeupDto>().ReverseMap();
            CreateMap<TubularForUpSet, TubularForUpSetDto>().ReverseMap();
            CreateMap<TubularForDownSet, TubularForDownSetDto>().ReverseMap();
            CreateMap<TubularForUpTrip, TubularForUpTripDto>().ReverseMap();
            CreateMap<TubularForDownTrip, TubularForDownTripDto>().ReverseMap();
            CreateMap<TubularForPmpOpen, TubularForPmpOpenDto>().ReverseMap();
            CreateMap<TubularForSealFric, TubularForSealFricDto>().ReverseMap();
            CreateMap<TubularCommonData, TubularCommonDataDto>().ReverseMap();
            CreateMap<TubularComponent, TubularComponentDto>().ReverseMap();
            CreateMap<Tubular, TubularDto>().ReverseMap();





        }


    }
}
