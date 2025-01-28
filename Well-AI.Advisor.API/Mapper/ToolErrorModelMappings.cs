using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class ToolErrorModelMappings : Profile
    {
        public ToolErrorModelMappings()
        {
            CreateMap<Authorization, AuthorizationDto>().ReverseMap();
            CreateMap<OperatingCondition, OperatingConditionDto>().ReverseMap();
            CreateMap<UseErrorTermSet, UseErrorTermSetDto>().ReverseMap();
            CreateMap<Term, TermDto>().ReverseMap();
            CreateMap<ToolErrorValue, ToolErrorValueDto>().ReverseMap(); 
            CreateMap<ErrorTermValue, ErrorTermValueDto>().ReverseMap();
            CreateMap<Start, StartDto>().ReverseMap();
            CreateMap<End, EndDto>().ReverseMap();
            CreateMap<OperatingInterval, OperatingIntervalDto>().ReverseMap();
            CreateMap<Speed, SpeedDto>().ReverseMap();
            CreateMap<GyroInitialization, GyroInitializationDto>().ReverseMap();
            CreateMap<GyroReinitializationDistance, GyroReinitializationDistanceDto>().ReverseMap();
            CreateMap<ModelParameters, ModelParametersDto>().ReverseMap();
            CreateMap<ToolErrorModelCommonData, ToolErrorModelCommonDataDto>().ReverseMap();
            CreateMap<Min, MinDto>().ReverseMap();
            CreateMap<Max, MaxDto>().ReverseMap();
            CreateMap<ToolErrorModel, ToolErrorModelDto>().ReverseMap();
        }


    }
}
