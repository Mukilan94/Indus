using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class MudLogMappings : Profile
    {
        public MudLogMappings()
        {
            CreateMap<MudLogStartMd, MudLogStartMdDto>().ReverseMap();
            CreateMap<MudLogEndMd, MudLogEndMdDto>().ReverseMap();
            CreateMap<MudLogMdTop, MudLogMdTopDto>().ReverseMap();
            CreateMap<MudLogMdBottom, MudLogMdBottomDto>().ReverseMap();
            CreateMap<MudLogCommonTime, MudLogCommonTimeDto>().ReverseMap(); 
            CreateMap<MudLogParameter, MudLogParameterDto>().ReverseMap();
            CreateMap<MudLogForce, MudLogForceDto>().ReverseMap();
            CreateMap<MudLogTvdTop, MudLogTvdTopDto>().ReverseMap();
            CreateMap<MudLogTvdBase, MudLogTvdBaseDto>().ReverseMap();
            CreateMap<MudLogRopAv, MudLogRopAvDto>().ReverseMap();
            CreateMap<MudLogRopMn, MudLogRopMnDto>().ReverseMap();
            CreateMap<MudLogRopMx, MudLogRopMxDto>().ReverseMap();
            CreateMap<MudLogWobAv, MudLogWobAvDto>().ReverseMap();
            CreateMap<MudLogTqAv, MudLogTqAvDto>().ReverseMap();
            CreateMap<MudLogRpmAv, MudLogRpmAvDto>().ReverseMap();
            CreateMap<MudLogWtMudAv, MudLogWtMudAvDto>().ReverseMap();
            CreateMap<MudLogEcdTdAv, MudLogEcdTdAvDto>().ReverseMap();
            CreateMap<MudLogDensShale, MudLogDensShaleDto>().ReverseMap();
            CreateMap<MudLogAbundance, MudLogAbundanceDto>().ReverseMap();
            CreateMap<MudLogLithology, MudLogLithologyDto>().ReverseMap();
            CreateMap<MudLogStainPc, MudLogStainPcDto>().ReverseMap();
            CreateMap<MudLogNatFlorPc, MudLogNatFlorPcDto>().ReverseMap();
            CreateMap<MudLogShow, MudLogShowDto>().ReverseMap();
            CreateMap<MudLogWtMudIn, MudLogWtMudInDto>().ReverseMap();
            CreateMap<MudLogWtMudOut, MudLogWtMudOutDto>().ReverseMap();
            CreateMap<MudLogETimChromCycle, MudLogETimChromCycleDto>().ReverseMap();
            CreateMap<MudLogMethAv, MudLogMethAvDto>().ReverseMap();
            CreateMap<MudLogMethMn, MudLogMethMnDto>().ReverseMap();
            CreateMap<MudLogMethMx, MudLogMethMxDto>().ReverseMap();
            CreateMap<MudLogEthAv, MudLogEthAvDto>().ReverseMap();
            CreateMap<MudLogEthMn, MudLogEthMnDto>().ReverseMap();
            CreateMap<MudLogEthMx, MudLogEthMxDto>().ReverseMap();
            CreateMap<MudLogQualifier, MudLogQualifierDto>().ReverseMap();
            
            CreateMap<MudLogPropAv, MudLogPropAvDto>().ReverseMap();
            CreateMap<MudLogPropMn, MudLogPropMnDto>().ReverseMap();
            CreateMap<MudLogPropMx, MudLogPropMxDto>().ReverseMap();
            CreateMap<MudLogIbutAv, MudLogIbutAvDto>().ReverseMap();
            CreateMap<MudLogIbutMn, MudLogIbutMnDto>().ReverseMap();
            CreateMap<MudLogIbutMx, MudLogIbutMxDto>().ReverseMap();
            CreateMap<MudLogNbutAv, MudLogNbutAvDto>().ReverseMap();
            CreateMap<MudLogNbutMn, MudLogNbutMnDto>().ReverseMap();
            CreateMap<MudLogNbutMx, MudLogNbutMxDto>().ReverseMap();
            CreateMap<MudLogIpentAv, MudLogIpentAvDto>().ReverseMap();
            CreateMap<MudLogIpentMn, MudLogIpentMnDto>().ReverseMap();
            CreateMap<MudLogIpentMx, MudLogIpentMxDto>().ReverseMap();
            CreateMap<MudLogNpentAv, MudLogNpentAvDto>().ReverseMap();
            CreateMap<MudLogNpentMn, MudLogNpentMnDto>().ReverseMap();
            CreateMap<MudLogNpentMx, MudLogNpentMxDto>().ReverseMap();
            CreateMap<MudLogEpentAv, MudLogEpentAvDto>().ReverseMap();
            CreateMap<MudLogEpentMn, MudLogEpentMnDto>().ReverseMap();
            CreateMap<MudLogEpentMx, MudLogEpentMxDto>().ReverseMap();
            CreateMap<MudLogIhexAv, MudLogIhexAvDto>().ReverseMap();
            CreateMap<MudLogIhexMn, MudLogIhexMnDto>().ReverseMap();
            CreateMap<MudLogIhexMx, MudLogIhexMxDto>().ReverseMap();
            CreateMap<MudLogNhexAv, MudLogNhexAvDto>().ReverseMap(); 
            CreateMap<MudLogNhexMn, MudLogNhexMnDto>().ReverseMap();
            CreateMap<MudLogNhexMx, MudLogNhexMxDto>().ReverseMap();
            CreateMap<MudLogCo2Av, MudLogCo2AvDto>().ReverseMap();
            CreateMap<MudLogCo2Mn, MudLogCo2MnDto>().ReverseMap();
            CreateMap<MudLogCo2Mx, MudLogCo2MxDto>().ReverseMap();
            CreateMap<MudLogH2sAv, MudLogH2sAvDto>().ReverseMap(); 
            CreateMap<MudLogH2sMn, MudLogH2sMnDto>().ReverseMap();
            CreateMap<MudLogH2sMx, MudLogH2sMxDto>().ReverseMap();
            CreateMap<MudLogAcetylene, MudLogAcetyleneDto>().ReverseMap();
            CreateMap<MudLogChromatograph, MudLogChromatographDto>().ReverseMap();
            CreateMap<MudLogGasAv, MudLogGasAvDto>().ReverseMap();
            CreateMap<MudLogGasPeak, MudLogGasPeakDto>().ReverseMap();
            CreateMap<MudLogGasBackgnd, MudLogGasBackgndDto>().ReverseMap();
            CreateMap<MudLogGasConAv, MudLogGasConAvDto>().ReverseMap();
            CreateMap<MudLogGasConMx, MudLogGasConMxDto>().ReverseMap();
            CreateMap<MudLogGasTrip, MudLogGasTripDto>().ReverseMap();
            CreateMap<MudLogMudGas, MudLogMudGasDto>().ReverseMap();
            CreateMap<MudLogDensBulk, MudLogDensBulkDto>().ReverseMap();
            CreateMap<MudLogCalcite, MudLogCalciteDto>().ReverseMap();
            CreateMap<MudLogDolomite, MudLogDolomiteDto>().ReverseMap();
            CreateMap<MudLogCec, MudLogCecDto>().ReverseMap();
            CreateMap<MudLogCalcStab, MudLogCalcStabDto>().ReverseMap();
            CreateMap<MudLogChronostratigraphic, MudLogChronostratigraphicDto>().ReverseMap();
            CreateMap<MudLogSizeMn, MudLogSizeMnDto>().ReverseMap();
            CreateMap<MudLogSizeMx, MudLogSizeMxDto>().ReverseMap();
            CreateMap<MudLogLenPlug, MudLogLenPlugDto>().ReverseMap();
            CreateMap<MudLogGeologyInterval, MudLogGeologyIntervalDto>().ReverseMap();
            CreateMap<MudLogLithostratigraphic, MudLogLithostratigraphicDto>().ReverseMap();
            CreateMap<MudLogCommonData, MudLogCommonDataDto>().ReverseMap();
            CreateMap<MudLog, MudLogDto>().ReverseMap();
           
        }

        
    }
}
