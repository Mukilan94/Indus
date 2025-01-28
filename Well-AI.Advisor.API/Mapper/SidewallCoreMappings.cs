using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class SidewallCoreMappings : Profile
    {
        public SidewallCoreMappings()
        {
            CreateMap<MdToolReference, MdToolReferenceDto>().ReverseMap();
            CreateMap<MdCore, MdCoreDto>().ReverseMap();
            CreateMap<SideWallCoreDiaHole, SideWallCoreDiaHoleDto>().ReverseMap();
            CreateMap<DiaPlug, DiaPlugDto>().ReverseMap();
            CreateMap<SideWallMd, SideWallMdDto>().ReverseMap(); 
            CreateMap<SideWallCoreLithPc, SideWallCoreLithPcDto>().ReverseMap();
            CreateMap<SideWallCoreLithology, SideWallCoreLithologyDto>().ReverseMap();
            CreateMap<SideWallCoreDensShale, SideWallCoreDensShaleDto>().ReverseMap();
            CreateMap<SideWallCoreAbundance, SideWallCoreAbundanceDto>().ReverseMap();
            CreateMap<SideWallCoreQualifier, SideWallCoreQualifierDto>().ReverseMap();
            CreateMap<SideWallCoreStainPc, SideWallCoreStainPcDto>().ReverseMap();
            CreateMap<SideWallCoreNatFlorPc, SideWallCoreNatFlorPcDto>().ReverseMap();
            CreateMap<SideWallCoreShow,  SideWallCoreShowDto>().ReverseMap();
            CreateMap<SideWallCoreSwcSample, SideWallCoreSwcSampleDto>().ReverseMap();
            CreateMap<SideWallCoreCommonData, SideWallCoreCommonDataDto>().ReverseMap();
            CreateMap<SidewallCore, SidewallCoreDto>().ReverseMap();
           
        }

        
    }
}
