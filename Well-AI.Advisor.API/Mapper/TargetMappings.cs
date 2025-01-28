using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class TargetMappings : Profile
    {
        public TargetMappings()
        {
            CreateMap<DispNsCenter, DispNsCenterDto>().ReverseMap();
            CreateMap<DispEwCenter, DispEwCenterDto>().ReverseMap();
            CreateMap<TargetTvd, TargetTvdDto>().ReverseMap();
            CreateMap<DispNsOffset, DispNsOffsetDto>().ReverseMap();
            CreateMap<DispEwOffset, DispEwOffsetDto>().ReverseMap(); 
            CreateMap<ThickAbove, ThickAboveDto>().ReverseMap();
            CreateMap<ThickBelow, ThickBelowDto>().ReverseMap();
            CreateMap<TargetDip, TargetDipDto>().ReverseMap();
            CreateMap<Strike, StrikeDto>().ReverseMap();
            CreateMap<Rotation, RotationDto>().ReverseMap();
            CreateMap<LenMajorAxis, LenMajorAxisDto>().ReverseMap();
            CreateMap<WidMinorAxis, WidMinorAxisDto>().ReverseMap();
            CreateMap<DispNsSectOrig, DispNsSectOrigDto>().ReverseMap();
            CreateMap<DispEwSectOrig, DispEwSectOrigDto>().ReverseMap();
            CreateMap<TargetWellCRS, TargetWellCRSDto>().ReverseMap();
            CreateMap<TargetLatitude, TargetLatitudeDto>().ReverseMap();
            CreateMap<TargetLongitude, TargetLongitudeDto>().ReverseMap();
            CreateMap<TargetLocation, TargetLocationDto>().ReverseMap();
            CreateMap<TargetProjectedX, ProjectedXDto>().ReverseMap();
            CreateMap<TargetProjectedY, ProjectedYDto>().ReverseMap();
            CreateMap<LenRadius, LenRadiusDto>().ReverseMap();
            CreateMap<AngleArc, AngleArcDto>().ReverseMap();
            CreateMap<TargetSection, TargetSectionDto>().ReverseMap();
            CreateMap<TargetCommonData, TargetCommonDataDto>().ReverseMap();
            CreateMap<Target, TargetDto>().ReverseMap();
           
        }

        
    }
}
