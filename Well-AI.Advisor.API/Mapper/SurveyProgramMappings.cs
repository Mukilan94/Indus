using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class SurveyProgramMappings : Profile
    {
        public SurveyProgramMappings()
        {
            CreateMap<FileCreationInformation, FileCreationInformationDto>().ReverseMap();
            CreateMap<MdStart, MdStartDto>().ReverseMap();
            CreateMap<MdEnd, MdEndDto>().ReverseMap();
            CreateMap<FrequencyMx, FrequencyMxDto>().ReverseMap();
            CreateMap<SurveySection, SurveySectionDto>().ReverseMap();
            CreateMap<SurveyProgramCommonData, SurveyCommonDataDto>().ReverseMap(); 
            CreateMap<SurveyProgram, SurveyProgramDto>().ReverseMap();
          
        }

        
    }
}
