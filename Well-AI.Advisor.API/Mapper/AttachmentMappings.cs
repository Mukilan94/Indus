using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;
using WellAI.Advisor.API.Models;
using WellAI.Advisor.API.Models.Dtos;

namespace Well_AI.Advisor.API.BharunMapper
{
    public class AttchmentMappings :Profile
    {
        public AttchmentMappings()
        {
            CreateMap<Attachment, AttachmentDto>().ReverseMap();
            CreateMap<AttachmentObjectReference, AttachmentObjectReferenceDto>().ReverseMap();
            CreateMap<AttchmentCommonData, AttchmentCommonDataDto>().ReverseMap();
            
        }
    }
}
