using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class WellBoreMappings : Profile
    {
        public WellBoreMappings()
        {
            CreateMap<WellboreMd, WellboreMdDto>().ReverseMap();
            CreateMap<WellboreTvd, WellboreTvdDto>().ReverseMap();
            CreateMap<WellboreMdKickoff, WellboreMdKickoffDto>().ReverseMap();
            CreateMap<TvdKickoff, TvdKickoffDto>().ReverseMap();
            CreateMap<WellboreMdPlanned, WellboreMdPlannedDto>().ReverseMap(); 
            CreateMap<TvdPlanned, TvdPlannedDto>().ReverseMap();
            CreateMap<MdSubSeaPlanned, MdSubSeaPlannedDto>().ReverseMap();
            CreateMap<TvdSubSeaPlanned, TvdSubSeaPlannedDto>().ReverseMap();
            CreateMap<DayTarget, DayTargetDto>().ReverseMap();
            CreateMap<WellBoreCommonData, WellBoreCommonDataDto>().ReverseMap();
            CreateMap<WellBore, WellBoreDto>().ReverseMap();
            CreateMap<ParentWellbore, ParentWellboreDto>().ReverseMap();
            
        }

        
    }
}
