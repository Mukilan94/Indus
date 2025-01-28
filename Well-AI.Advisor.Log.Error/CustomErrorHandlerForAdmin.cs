using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;

namespace Well_AI.Advisor.Log.Error
{
    public class CustomErrorHandlerForAdmin
    {
        private readonly Guid _UserId;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<StaffWellIdentityUser> _userManager;
        private WebAIAdvisorContext db;


        public CustomErrorHandlerForAdmin(RoleManager<IdentityRole> roleManager, UserManager<StaffWellIdentityUser> userManager, WebAIAdvisorContext dbContext, Guid? userId)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
            this._UserId = userId.HasValue ? userId.Value : Guid.Empty;
        }

        public void WriteError(Exception ex, string location, string createBy)
        {
            string message = ex.Message + Environment.NewLine + ex.StackTrace;

            if (ex.InnerException != null)
            {
                message = $"{message} ***************> {ex.InnerException.Message}";
            }

            IErrorLogRepositoryForAdmin repostirory = new IErrorLogRepositoryForAdmin(db, _roleManager, _userManager);
            var errorLog = new ErrorLog { ErrorMessage = message, UserId = _UserId, CompanyId =Guid.Empty, CreatedBy = createBy, CreateDate = DateTime.Now, Location = location, ErrorCode = ex.HResult.ToString() };
            var result = repostirory.CreateErrorLog(errorLog);
        }
    }
}
