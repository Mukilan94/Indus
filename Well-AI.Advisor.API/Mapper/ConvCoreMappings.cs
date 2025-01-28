using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class ConvCoreMappings : Profile
    {
        public ConvCoreMappings()
        {
            
            CreateMap<ConvCoreMdCoreTop, ConvCoreMdCoreTopDto>().ReverseMap();
            CreateMap<ConvCoreMdCoreBottom, ConvCoreMdCoreBottomDto>().ReverseMap();
            CreateMap<ConvCoreLenBarrel, ConvCoreLenBarrelDto>().ReverseMap();
            CreateMap<ConvCoreGeologyInterval, ConvCoreGeologyIntervalDto>().ReverseMap();
            CreateMap<ConvCoreLithology, ConvCoreLithologyDto>().ReverseMap();
            CreateMap<ConvCoreQualifier, ConvCoreQualifierDto>().ReverseMap();
            CreateMap<ConvCoreDiaCore, ConvCoreDiaCoreDto>().ReverseMap();
            CreateMap<ConvCoreLenCored, ConvCoreLenCoredDto>().ReverseMap(); 
            CreateMap<ConvCoreInclHole, ConvCoreInclHoleDto>().ReverseMap();
            CreateMap<ConvCoreLithPc, ConvCoreLithPcDto>().ReverseMap();
            CreateMap<ConvCoreCommonData, ConvCoreCommonDataDto>().ReverseMap();
            CreateMap<ConvCoreDiaBit, ConvCoreDiaBitDto>().ReverseMap();
            CreateMap<ConvCoreLenRecovered, ConvCoreLenRecoveredDto>().ReverseMap();
            CreateMap<ConvCoreRecoverPc, ConvCoreRecoverPcDto>().ReverseMap();
            CreateMap<ConvCoreDensShale, ConvCoreDensShaleDto>().ReverseMap();
            CreateMap<ConvCoreStainPc, ConvCoreStainPcDto>().ReverseMap();
            CreateMap<ConvCoreNatFlorPc, ConvCoreNatFlorPcDto>().ReverseMap();
            CreateMap<ConvCoreShow, ConvCoreShowDto>().ReverseMap();
            CreateMap<ConvCoreMdTop, ConvCoreMdTopDto>().ReverseMap();
            CreateMap<ConvCoreMdBottom, ConvCoreMdBottomDto>().ReverseMap();
            CreateMap<ConvCoreTvdTop, ConvCoreTvdTopDto>().ReverseMap();
            CreateMap<ConvCoreTvdBase, ConvCoreTvdBaseDto>().ReverseMap();
            CreateMap<ConvCoreRopAv, ConvCoreRopAvDto>().ReverseMap();
            CreateMap<ConvCoreRopMn, ConvCoreRopMnDto>().ReverseMap();
            CreateMap<ConvCoreRopMx, ConvCoreRopMxDto>().ReverseMap();
            CreateMap<ConvCoreWobAv, ConvCoreWobAvDto>().ReverseMap();
            CreateMap<ConvCoreTqAv, ConvCoreTqAvDto>().ReverseMap();
            CreateMap<ConvCoreRpmAv, ConvCoreRpmAvDto>().ReverseMap();
            CreateMap<ConvCoreWtMudAv, ConvCoreWtMudAvDto>().ReverseMap();
            CreateMap<ConvCoreEcdTdAv, ConvCoreEcdTdAvDto>().ReverseMap();
            CreateMap<ConvCoreWtMudOut, ConvCoreWtMudOutDto>().ReverseMap();
            CreateMap<ConvCoreETimChromCycle, ConvCoreETimChromCycleDto>().ReverseMap();
            CreateMap<ConvCoreMethAv, ConvCoreMethAvDto>().ReverseMap();
            CreateMap<ConvCoreMethMn, ConvCoreMethMnDto>().ReverseMap(); 
            CreateMap<ConvCoreMethMx, ConvCoreMethMxDto>().ReverseMap();
            CreateMap<ConvCoreEthMn, ConvCoreEthMnDto>().ReverseMap();
            CreateMap<ConvCoreEthMx, ConvCoreEthMxDto>().ReverseMap();
            CreateMap<ConvCorePropAv, ConvCorePropAvDto>().ReverseMap();
            CreateMap<ConvCorePropMn, ConvCorePropMnDto>().ReverseMap();
            CreateMap<ConvCorePropMx, ConvCorePropMxDto>().ReverseMap();
            CreateMap<ConvCoreIbutAv, ConvCoreIbutAvDto>().ReverseMap();
            CreateMap<ConvCoreIbutMn, ConvCoreIbutMnDto>().ReverseMap();
            CreateMap<ConvCoreIbutMx, ConvCoreIbutMxDto>().ReverseMap();
            CreateMap<ConvCoreNbutAv, ConvCoreNbutAvDto>().ReverseMap();
            CreateMap<ConvCoreNbutMn, ConvCoreNbutMnDto>().ReverseMap();
            CreateMap<ConvCoreNbutMx, ConvCoreNbutMxDto>().ReverseMap();
            CreateMap<ConvCoreIpentAv, ConvCoreIpentAvDto>().ReverseMap();
            CreateMap<ConvCoreIpentMn, ConvCoreIpentMnDto>().ReverseMap();
            CreateMap<ConvCoreIpentMx, ConvCoreIpentMxDto>().ReverseMap();
            CreateMap<ConvCoreNpentAv, ConvCoreNpentAvDto>().ReverseMap();
            CreateMap<ConvCoreNpentMn, ConvCoreNpentMnDto>().ReverseMap();
            CreateMap<ConvCoreNpentMx, ConvCoreNpentMxDto>().ReverseMap();
            CreateMap<ConvCoreEpentAv, ConvCoreEpentAvDto>().ReverseMap();
            CreateMap<ConvCoreEpentMn, ConvCoreEpentMnDto>().ReverseMap();
            CreateMap<ConvCoreEpentMx, ConvCoreEpentMxDto>().ReverseMap();
            CreateMap<ConvCoreIhexAv, ConvCoreIhexAvDto>().ReverseMap();
            CreateMap<ConvCoreIhexMn, ConvCoreIhexMnDto>().ReverseMap();
            CreateMap<ConvCoreIhexMx, ConvCoreIhexMxDto>().ReverseMap();
            CreateMap<ConvCoreNhexAv, ConvCoreNhexAvDto>().ReverseMap();
            CreateMap<ConvCoreNhexMn, ConvCoreNhexMnDto>().ReverseMap();
            CreateMap<ConvCoreNhexMx, ConvCoreNhexMxDto>().ReverseMap();
            CreateMap<ConvCoreCo2Av, ConvCoreCo2AvDto>().ReverseMap();
            CreateMap<ConvCoreCo2Mn, ConvCoreCo2MnDto>().ReverseMap();
            CreateMap<ConvCoreCo2Mx, ConvCoreCo2MxDto>().ReverseMap();
            CreateMap<ConvCoreH2sAv, ConvCoreH2sAvDto>().ReverseMap();
            CreateMap<ConvCoreH2sMn, ConvCoreH2sMnDto>().ReverseMap();
            CreateMap<ConvCoreH2sMx, ConvCoreH2sMxDto>().ReverseMap();
            CreateMap<ConvCoreAcetylene, ConvCoreAcetyleneDto>().ReverseMap();
            CreateMap<ConvCoreChromatograph, ConvCoreChromatographDto>().ReverseMap();
            CreateMap<ConvCoreGasAv, ConvCoreGasAvDto>().ReverseMap();
            CreateMap<ConvCoreGasPeak, ConvCoreGasPeakDto>().ReverseMap();
            CreateMap<ConvCoreGasBackgnd, ConvCoreGasBackgndDto>().ReverseMap();
            CreateMap<ConvCoreGasConAv, ConvCoreGasConAvDto>().ReverseMap();
            CreateMap<ConvCoreGasConMx, ConvCoreGasConMxDto>().ReverseMap();
            CreateMap<ConvCoreGasTrip, ConvCoreGasTripDto>().ReverseMap();
            CreateMap<ConvCoreMudGas, ConvCoreMudGasDto>().ReverseMap();
            CreateMap<ConvCoreDensBulk, ConvCoreDensBulkDto>().ReverseMap();
            CreateMap<ConvCoreCalcite, ConvCoreCalciteDto>().ReverseMap();
            CreateMap<ConvCoreDolomite, ConvCoreDolomiteDto>().ReverseMap();
            CreateMap<ConvCoreCec, ConvCoreCecDto>().ReverseMap();
            CreateMap<ConvCoreCalcStab, ConvCoreCalcStabDto>().ReverseMap();
            CreateMap<ConvCoreSizeMn, ConvCoreSizeMnDto>().ReverseMap();
            CreateMap<ConvCoreSizeMx, ConvCoreSizeMxDto>().ReverseMap();
            CreateMap<ConvCoreLenPlug, ConvCoreLenPlugDto>().ReverseMap();
            CreateMap<ConvCoreWtMudIn, ConvCoreWtMudInDto>().ReverseMap();
            CreateMap<ConvCoreEthAv, ConvCoreEthAvDto>().ReverseMap();
            CreateMap<ConvCore, ConvCoreDto>().ReverseMap();



        }

        
    }
}
