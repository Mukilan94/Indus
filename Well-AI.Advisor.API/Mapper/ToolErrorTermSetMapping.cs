using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class ToolErrorTermSetMapping : Profile
    {
        public ToolErrorTermSetMapping()
        {
            CreateMap<Authorization, AuthorizationDto>().ReverseMap();
            CreateMap<ToolErrorTermSetParameter, ToolErrorTermSetParameterDto>().ReverseMap();
            CreateMap<Function, FunctionDto>().ReverseMap();
            CreateMap<Nomenclature, NomenclatureDto>().ReverseMap();
            CreateMap<ErrorCoefficient, ErrorCoefficientDto>().ReverseMap(); 
            CreateMap<ErrorTerm, ErrorTermDto>().ReverseMap();
            CreateMap<ToolErrorTermSet, ToolErrorTermSetDto>().ReverseMap();
            CreateMap<Constant, ConstantDto>().ReverseMap();
          
        }

        
    }
}
