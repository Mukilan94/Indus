using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Well_AI.Advisor.API.PEC.Models;
using Well_AI.Advisor.API.PEC.Models.Dtos;

namespace Well_AI.Advisor.API.PEC.Mapper
{
   public class PecApiMapper : Profile
    {
        public PecApiMapper()
        {
            CreateMap<CoveredTaskModel, CoveredTaskDto>().ReverseMap();
            CreateMap<EmployeeQualification, EmployeeQualificationDto>().ReverseMap();
        }
    }
}
