using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.BharunMapper
{
    public class BharunMappings :Profile
    {
        public BharunMappings()
        {

        CreateMap<Bharun, BharunDto>().ReverseMap();
        CreateMap<BharunActDogleg, BharunActDoglegDto>().ReverseMap();
        CreateMap<BharunActDoglegMx, BharunActDoglegMxDto>().ReverseMap();
        CreateMap<BharunETimOpBit, BharunETimOpBitDto>().ReverseMap();
        CreateMap<BharunMdHoleStart, BharunMdHoleStartDto>().ReverseMap();
        CreateMap<BharunMdHoleStop, BharunMdHoleStopDto>().ReverseMap();
        CreateMap<BharunHkldRot, BharunHkldRotDto>().ReverseMap();
        CreateMap<BharunOverPull, BharunOverPullDto>().ReverseMap();
        CreateMap<BharunSlackOff, BharunSlackOffDto>().ReverseMap();
        CreateMap<BharunHkldUp, BharunHkldUpDto>().ReverseMap();
        CreateMap<BharunHkldDn, BharunHkldDnDto>().ReverseMap();
        CreateMap<BharunTqOnBotAv, BharunTqOnBotAvDto>().ReverseMap();
        CreateMap<BharunTqOnBotMx, BharunTqOnBotMxDto>().ReverseMap();
        CreateMap<BharunTqOnBotMn, BharunTqOnBotMnDto>().ReverseMap();
        CreateMap<BharunTqOffBotAv, BharunTqOffBotAvDto>().ReverseMap();
        CreateMap<BharunTqDhAv, BharunTqDhAvDto>().ReverseMap();
        CreateMap<BharunWtAboveJar, BharunWtAboveJarDto>().ReverseMap();
        CreateMap<BharunWtBelowJar, BharunWtBelowJarDto>().ReverseMap();
        CreateMap<BharunWtMud, BharunWtMudDto>().ReverseMap();
        CreateMap<BharunFlowratePump, BharunFlowratePumpDto>().ReverseMap();
        CreateMap<BharunPowBit, BharunPowBitDto>().ReverseMap();
        CreateMap<BharunVelNozzleAv, BharunVelNozzleAvDto>().ReverseMap();
        CreateMap<BharunPresDropBit, BharunPresDropBitDto>().ReverseMap();
        CreateMap<BharunCTimHold, BharunCTimHoldDto>().ReverseMap();
        CreateMap<BharunCTimSteering, BharunCTimSteeringDto>().ReverseMap();
        CreateMap<BharunCTimDrillRot, BharunCTimDrillRotDto>().ReverseMap();
        CreateMap<BharunCTimDrillSlid, BharunCTimDrillSlidDto>().ReverseMap();
        CreateMap<BharunCTimCirc, BharunCTimCircDto>().ReverseMap();
        CreateMap<BharunCTimReam, BharunCTimReamDto>().ReverseMap();
        CreateMap<BharunRpmAv, BharunRpmAvDto>().ReverseMap();
        CreateMap<BharunRopAv, BharunRopAvDto>().ReverseMap();
        CreateMap<BharunRopAv, BharunRopAvDto> ().ReverseMap();
        CreateMap<BharunRopMx, BharunRopMxDto>().ReverseMap();
        CreateMap<BharunRopMn, BharunRopMnDto>().ReverseMap();
        CreateMap<BharunWobAv, BharunWobAvDto>().ReverseMap();
        CreateMap<BharunDistDrillRot, BharunDistDrillRotDto>().ReverseMap();
        CreateMap<BharunDistDrillSlid, BharunDistDrillSlidDto>().ReverseMap();
        CreateMap<BharunDistReam, BharunDistReamDto>().ReverseMap();
        CreateMap<BharunDistHold, BharunDistHoldDto>().ReverseMap();
        CreateMap<BharunDistSteering, BharunDistSteeringDto>().ReverseMap();
        CreateMap<BharunRpmMx, BharunRpmMxDto>().ReverseMap();
        CreateMap<BharunRpmMn, BharunRpmMnDto>().ReverseMap();
        CreateMap<BharunRpmAvDh, BharunRpmAvDhDto>().ReverseMap();
        CreateMap<BharunWobMx, BharunWobMxDto>().ReverseMap();
        CreateMap<BharunWobMn, BharunWobMnDto>().ReverseMap();
        CreateMap<BharunWobAvDh, BharunWobAvDhDto>().ReverseMap();
        CreateMap<BharunAziTop, BharunAziTopDto>().ReverseMap();
        CreateMap<BharunAziBottom, BharunAziBottomDto>().ReverseMap();
        CreateMap<BharunInclStart, BharunInclStartDto>().ReverseMap();
        CreateMap<BharunInclMx, BharunInclMxDto>().ReverseMap();
        CreateMap<BharunInclMn, BharunInclMnDto>().ReverseMap();
        CreateMap<BharunInclStop, BharunInclStopDto>().ReverseMap();
        CreateMap<BharunTempMudDhMx, BharunTempMudDhMxDto>().ReverseMap();
        CreateMap<BharunPresPumpAv, BharunPresPumpAvDto>().ReverseMap();
        CreateMap<BharunFlowrateBit, BharunFlowrateBitDto>().ReverseMap();
        CreateMap<BharunDrillingParams, BharunDrillingParamsDto>().ReverseMap();
        CreateMap<BharunTubular, BharunTubularDto>().ReverseMap();
        CreateMap<BharunCommonData, BharunCommonDataDto>().ReverseMap();
        CreateMap<BharunPlanDogleg, BharunPlanDoglegDto>().ReverseMap();

        }
    }
}
