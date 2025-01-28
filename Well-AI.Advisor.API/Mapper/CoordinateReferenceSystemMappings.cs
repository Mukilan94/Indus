using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class CoordinateReferenceSystemMappings : Profile
    {
        public CoordinateReferenceSystemMappings()
        {
            CreateMap<CoordinateReferenceSystem, CoordinateReferenceSystemDto>().ReverseMap();
            CreateMap<NameCRS, NameCRSDto>().ReverseMap();
            CreateMap<CoordinateReferenceSystemGeodeticCRS, CoordinateReferenceSystemGeodeticCRSDto>().ReverseMap();
            CreateMap<ProjectedCRS, ProjectedCRSDto>().ReverseMap();
            CreateMap<VerticalCRS, VerticalCRSDto>().ReverseMap(); 
            CreateMap<Identifier, IdentifierDto>().ReverseMap();
            CreateMap<Name, NameDto>().ReverseMap();
            CreateMap<AxisDirection, AxisDirectionDto>().ReverseMap();
            CreateMap<CoordinateSystemAxis, CoordinateSystemAxisDto>().ReverseMap();
            CreateMap<UsesAxis, UsesAxisDto>().ReverseMap();
            CreateMap<EllipsoidalCS, EllipsoidalCSDto>().ReverseMap();
            CreateMap<GreenwichLongitude, GreenwichLongitudeDto>().ReverseMap();
            CreateMap<PrimeMeridian, PrimeMeridianDto>().ReverseMap();
            CreateMap<UsesPrimeMeridian, UsesPrimeMeridianDto>().ReverseMap();
            CreateMap<SemiMajorAxis, SemiMajorAxisDto>().ReverseMap();
            CreateMap<InverseFlattening, InverseFlatteningDto>().ReverseMap();
            CreateMap<SecondDefiningParameter, SecondDefiningParameterDto>().ReverseMap();
            CreateMap<UsesEllipsoid, UsesEllipsoidDto>().ReverseMap();
            CreateMap<GeodeticDatum, GeodeticDatumDto>().ReverseMap(); 
            CreateMap<OperationParameter, OperationParameterDto>().ReverseMap();
            CreateMap<UsesParameter, UsesParameterDto>().ReverseMap();
            CreateMap<UsesMethod, UsesMethodDto>().ReverseMap();
            CreateMap<OperationMethod, OperationMethodDto>().ReverseMap();
            CreateMap<ValueOfParameter, ValueOfParameterDto>().ReverseMap();
            CreateMap<ParameterValue, ParameterValueDto>().ReverseMap();
            CreateMap<Conversion, ConversionDto>().ReverseMap();
            CreateMap<DefinedByConversion, DefinedByConversionDto>().ReverseMap(); 
            CreateMap<UsesEllipsoidalCS, UsesEllipsoidalCSDto>().ReverseMap();
            CreateMap<GeographicCRS, GeographicCRSDto>().ReverseMap();
            CreateMap<UsesGeodeticDatum, UsesGeodeticDatumDto>().ReverseMap();
            CreateMap<BaseGeographicCRS, BaseGeographicCRSDto>().ReverseMap();
            CreateMap<CartesianCS, CartesianCSDto>().ReverseMap();
            CreateMap<UsesCartesianCS, UsesCartesianCSDto>().ReverseMap();

        }

        
    }
}
