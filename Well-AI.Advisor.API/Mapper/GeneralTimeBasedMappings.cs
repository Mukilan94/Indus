using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;
using WellAI.Advisor.API.Models;
using WellAI.Advisor.API.Models.Dtos;

namespace Well_AI.Advisor.API
{
    public class GeneralTimeBasedMappings :Profile
    {
        public GeneralTimeBasedMappings()
        {
            CreateMap<GeneralTimeBased, GeneralTimeBasedDto>().ReverseMap();
            
        }
    }
}
