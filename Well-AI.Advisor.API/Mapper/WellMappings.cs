using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class WellMappings : Profile
    {
        public WellMappings()
        {
            CreateMap<WellPcInterest, WellPcInterestDto>().ReverseMap();
            CreateMap<WellheadElevation, WellheadElevationDto>().ReverseMap();
            CreateMap<WellDatumName, WellDatumNameDto>().ReverseMap();
            CreateMap<WellGroundElevation, WellGroundElevationDto>().ReverseMap();
            CreateMap<WellWaterDepth, WellWaterDepthDto>().ReverseMap();
            CreateMap<WellGeodeticCRS, WellGeodeticCRSDto>().ReverseMap();
            CreateMap<WellLocation, WellLocationDto>().ReverseMap();
            CreateMap<WellCRS, WellCRSDto>().ReverseMap();
            CreateMap<WellEasting, WellEastingDto>().ReverseMap();
            CreateMap<WellNorthing, WellNorthingDto>().ReverseMap();
            CreateMap<WellLocalX, WellLocalXDto>().ReverseMap();
            CreateMap<WellLocalY, WellLocalYDto>().ReverseMap();
            CreateMap<WellLatitude, WellLatitudeDto>().ReverseMap();
            CreateMap<WellLongitude, WellLongitudeDto>().ReverseMap();
            CreateMap<WellElevation, WellElevationDto>().ReverseMap();
            CreateMap<WellReferencePoint, WellReferencePointDto>().ReverseMap();
            CreateMap<WellMeasuredDepth, WellMeasuredDepthDto>().ReverseMap();
            CreateMap<WellMapProjectionCRS, WellMapProjectionCRSDto>().ReverseMap();
            CreateMap<WellYAxisAzimuth, WellYAxisAzimuthDto>().ReverseMap();
            CreateMap<WellLocalCRS, WellLocalCRSDto>().ReverseMap();
            CreateMap<WellDefaultDatum, WellDefaultDatumDto>().ReverseMap();
            CreateMap<WellDatum, WellDatumDto>().ReverseMap();
            CreateMap<WellCommonData, WellCommonDataDto>().ReverseMap();
            CreateMap<Well, WellDto>().ReverseMap();
           
        }

        
    }
}
