using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class WbGeometryMappings : Profile
    {
        public WbGeometryMappings()
        {
            CreateMap<WbGeometryMdBottom, WbGeometryMdBottomDto>().ReverseMap();
            CreateMap<WbGeometryGapAir, WbGeometryGapAirDto>().ReverseMap();
            CreateMap<WbGeometryDepthWaterMean, WbGeometryDepthWaterMeanDto>().ReverseMap();
            CreateMap<WbGeometryMdTop, WbGeometryMdTopDto>().ReverseMap();
            CreateMap<WbGeometryTvdTop, WbGeometryTvdTopDto>().ReverseMap(); 
            CreateMap<WbGeometryTvdBottom, WbGeometryTvdBottomDto>().ReverseMap();
            CreateMap<WbGeometryIdSection, WbGeometryIdSectionDto>().ReverseMap();
            CreateMap<WbGeometryOdSection, WbGeometryOdSectionDto>().ReverseMap();
            CreateMap<WbGeometryWtPerLen, WbGeometryWtPerLenDto>().ReverseMap();
            CreateMap<WbGeometryDiaDrift, WbGeometryDiaDriftDto>().ReverseMap();
            CreateMap<WbGeometrySection, WbGeometrySectionDto>().ReverseMap();
            CreateMap<WbGeometryCommonData, WbGeometryCommonDataDto>().ReverseMap();
            CreateMap<WbGeometry, WbGeometryDto>().ReverseMap();
            
        }

        
    }
}
