using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class LogMappings : Profile
    {
        public LogMappings()
        {
            CreateMap<LogStartIndex, LogStartIndexDto>().ReverseMap();
            CreateMap<LogEndIndex, LogEndIndexDto>().ReverseMap();
            CreateMap<StepIncrement, StepIncrementDto>().ReverseMap();
            CreateMap<LogParam, LogParamDto>().ReverseMap();
            CreateMap<MinIndex, MinIndexDto>().ReverseMap(); 
            CreateMap<MaxIndex, MaxIndexDto>().ReverseMap();
            CreateMap<SensorOffset, SensorOffsetDto>().ReverseMap();
            CreateMap<LogCurveInfo, LogCurveInfoDto>().ReverseMap();
            CreateMap<LogData, LogDataDto>().ReverseMap();
            CreateMap<LogCommonData, LogCommonDataDto>().ReverseMap();
            CreateMap<Log, LogDto>().ReverseMap();
           
        }

        
    }
}
