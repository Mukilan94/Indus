using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class FormationMarkerMappings : Profile
    {
        public FormationMarkerMappings()
        {
            CreateMap<FormationMarkerMdPrognosed, FormationMarkerMdPrognosedDto>().ReverseMap();
            CreateMap<FormationMarkerTvdPrognosed, FormationMarkerTvdPrognosedDto>().ReverseMap();
            CreateMap<FormationMarkerMdTopSample, FormationMarkerMdTopSampleDto>().ReverseMap();
            CreateMap<FormationMarkerTvdTopSample, FormationMarkerTvdTopSampleDto>().ReverseMap();
            CreateMap<FormationMarkerThicknessBed, FormationMarkerThicknessBedDto>().ReverseMap();
            CreateMap<FormationMarkerThicknessApparent, FormationMarkerThicknessApparentDto>().ReverseMap();
            CreateMap<FormationMarkerThicknessPerpen, FormationMarkerThicknessPerpenDto>().ReverseMap();
            CreateMap<FormationMarkerMdLogSample, FormationMarkerMdLogSampleDto>().ReverseMap();
            CreateMap<FormationMarkerTvdLogSample, FormationMarkerTvdLogSampleDto>().ReverseMap();
            CreateMap<FormationMarkerDip, FormationMarkerDipDto>().ReverseMap();
            CreateMap<FormationMarkerDipDirection, FormationMarkerDipDirectionDto>().ReverseMap();
            CreateMap<FormationMarkerLithostratigraphic, FormationMarkerLithostratigraphicDto>().ReverseMap();
            CreateMap<FormationMarkerChronostratigraphic, FormationMarkerChronostratigraphicDto>().ReverseMap();
            CreateMap<FormationMarkerCommonData, FormationMarkerCommonDataDto>().ReverseMap();
            CreateMap<FormationMarker, FormationMarkerDto>().ReverseMap();
        }

        
    }
}
