using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.DLL.Repository
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public ErrorLogRepository(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public bool CreateErrorLog(ErrorLog errorLog)
        {
            try
            {
                db.ErrorLog.Add(errorLog);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}
