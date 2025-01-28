using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class ChangeLogMappings : Profile
    {
        public ChangeLogMappings()
        {
            CreateMap<ChangeLog, ChangeLogDto>().ReverseMap();
            CreateMap<ChangeHistory, ChangeHistoryDto>().ReverseMap();
            CreateMap<StartIndex, StartIndexDto>().ReverseMap();
            CreateMap<EndIndex, EndIndexDto>().ReverseMap();
            CreateMap<ChangeLogCommonData, ChangeLogCommonDataDto>().ReverseMap(); 
            
           
        }

        
    }
}
