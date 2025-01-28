using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class objectGroupMappings : Profile
    {
        public objectGroupMappings()
        {
             CreateMap<ObjectGroupParam, ObjectGroupParamDto>().ReverseMap();
        CreateMap<ObjectGroupObjectReference, ObjectGroupObjectReferenceDto>().ReverseMap();
        CreateMap<ObjectGroupSequence1, ObjectGroupSequence1Dto>().ReverseMap();
        CreateMap<ObjectGroupSequence2, ObjectGroupSequence2Dto>().ReverseMap();
        CreateMap<ObjectGroupSequence3, ObjectGroupSequence3Dto>().ReverseMap();
        CreateMap<ObjectGroupRangeMin, ObjectGroupRangeMinDto>().ReverseMap();
        CreateMap<ObjectGroupRangeMax, ObjectGroupRangeMaxDto>().ReverseMap();
        CreateMap<ObjectGroupReferenceDepth, ObjectGroupReferenceDepthDto>().ReverseMap();
        CreateMap<ObjectGroupValue, ObjectGroupValueDto>().ReverseMap();
        CreateMap<ObjectGroupMd, ObjectGroupMdDto>().ReverseMap();
        CreateMap<ObjectGroupExtensionNameValue, ObjectGroupExtensionNameValueDto>().ReverseMap();
        CreateMap<ObjectGroupMemberObject, ObjectGroupMemberObjectDto>().ReverseMap();
        CreateMap<ObjectGroupAcquisitionTimeZone, ObjectGroupAcquisitionTimeZoneDto>().ReverseMap();
        CreateMap<ObjectGroupDefaultDatum, ObjectGroupDefaultDatumDto>().ReverseMap();
        CreateMap<ObjectGroupCommonData, ObjectGroupCommonDataDto>().ReverseMap();
        CreateMap<ObjectGroup, ObjectGroupDto>().ReverseMap();

        }

        
    }
}
