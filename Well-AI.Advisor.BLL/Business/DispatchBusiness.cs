using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Common;
using Finbuckle.MultiTenant;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.Model.Administration;
using ChecklistTemplateModel = WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.DLL.IRepository;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.BLL.Business
{
    public class DispatchBusiness: IDispatchBusiness
    {
        private readonly WebAIAdvisorContext db;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public DispatchBusiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, IConfiguration configuration = null)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public Task<UserRoutes> GetUserCurrentRoutes(string userId,string dispatchAPIUrl)
        {
            IDispatchRepository commonRepository = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            return commonRepository.GetUserCurrentRoutes(userId,dispatchAPIUrl);
        }
        public Task<bool> UpdateUserRoutes(string userId, UserRoutes routes)
        {
            IDispatchRepository commonRepository = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            return commonRepository.UpdateUserRoutes(userId, routes);
        }

        public Task<UserRoutes> GetUserRoutes(string dispatchAPIUrl)
        {
            IDispatchRepository commonRepository = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            return commonRepository.GetUserRoutes(dispatchAPIUrl);
        }
        public List<DispatchRoutesViewModel> GetDispatchDetailsList(string userid)
        {

            IDispatchRepository dispatchRepository = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            return dispatchRepository.GetDispatchDetailsList(userid);
        }
        public List<DispatchRoutesViewModel> GetDispatchDetailsList_active(string userid)
        {

            IDispatchRepository dispatchRepository = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            return dispatchRepository.GetDispatchDetailsList_active(userid);
        }
        public DispatchUserStatusCount GetDispatchStatuscount(string userid)
        {

            IDispatchRepository dispatchRepository = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            return dispatchRepository.GetDispatchStatuscount(userid);
        }


    }
}
