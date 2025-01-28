using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class MessageMappings : Profile
    {
        public MessageMappings()
        {
            CreateMap<MessageMd, MessageMdDto>().ReverseMap();
            CreateMap<MessageMdBit, MessageMdBitDto>().ReverseMap();
            CreateMap<MessageParam, MessageParamDto>().ReverseMap();
            CreateMap<MessageCommonData, MessageCommonDataDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap(); 
            
        }

        
    }
}
