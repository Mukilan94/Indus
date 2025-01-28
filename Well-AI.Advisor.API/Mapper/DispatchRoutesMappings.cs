using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Models;
using WellAI.Advisor.API.Models.Dtos;
namespace WellAI.Advisor.API
{
    public class DispatchRoutesMappings : Profile
    {
        public DispatchRoutesMappings()
        {
            CreateMap<DispatchRoutesResponse, DispatchRoutesResponeDto>().ReverseMap();
        }
    }
}
