using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Business
{
    public class ErrorLogBusiness : IErrorLogBusiness
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public ErrorLogBusiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public bool CreateErrorLog(ErrorLog errorLog)
        {
            IErrorLogRepository repostirory = new ErrorLogRepository(db, _roleManager, _userManager);
            return repostirory.CreateErrorLog(errorLog);
        }
    }
}
