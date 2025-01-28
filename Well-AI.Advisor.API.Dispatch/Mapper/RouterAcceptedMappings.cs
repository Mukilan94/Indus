using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Dispatch.Models;
using WellAI.Advisor.API.Models.Dtos;

namespace WellAI.Advisor.API.Dispatch.Mapper
{
    public class RouterAcceptedMappings: Profile
    {
        public RouterAcceptedMappings()
        {
            CreateMap<RouteAcceptedResponse, RouteAcceptedResponseDto>().ReverseMap();
        }
    }
}
