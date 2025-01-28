using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class RiskMappings : Profile
    {
        public RiskMappings()
        {
            CreateMap<RiskObjectReference, RiskObjectReferenceDto>().ReverseMap();
            CreateMap<RiskMdHoleStart, RiskMdHoleStartDto>().ReverseMap();
            CreateMap<RiskMdHoleEnd, RiskMdHoleEndDto>().ReverseMap();
            CreateMap<RiskTvdHoleStart, RiskTvdHoleStartDto>().ReverseMap();
            CreateMap<RiskTvdHoleEnd, RiskTvdHoleEndDto>().ReverseMap(); 
            CreateMap<RiskDiaHole, RiskDiaHoleDto>().ReverseMap();
            CreateMap<Risk, RiskDto>().ReverseMap();
            
        }

        
    }
}
