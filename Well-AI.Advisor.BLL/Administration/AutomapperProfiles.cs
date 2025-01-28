using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.BLL.Administration
{
    public class AutomapperProfiles: Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<CorporateProfile, CustomerProfileModel>().ReverseMap();
            CreateMap<WellIdentityUser, CustomerUsersModel>().ReverseMap();
        }
    }
}
