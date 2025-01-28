using Microsoft.AspNetCore.Identity;
using System;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;

namespace Well_AI.Advisor.Log.Error
{
    internal class IErrorLogRepositoryForAdmin
    {
        private WebAIAdvisorContext db;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<StaffWellIdentityUser> userManager;

        public IErrorLogRepositoryForAdmin(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<StaffWellIdentityUser> userManager)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        internal object CreateErrorLog(ErrorLog errorLog)
        {
            db.ErrorLog.Add(errorLog);
            db.SaveChanges();
            return true;
        }
    }
}