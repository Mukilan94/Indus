using Microsoft.AspNetCore.Identity;
using System;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Identity;

namespace Well_AI.Advisor.Communication
{
    internal class CustomErrorHandlerForCommunication
    {
        private readonly Guid _UserId;
        private readonly Guid _CompnayId;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private WebAIAdvisorContext db;


        public CustomErrorHandlerForCommunication(RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext, Guid? userId, Guid? companyId)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
            this._UserId = userId.HasValue ? userId.Value : Guid.Empty;
            this._CompnayId = companyId.HasValue ? companyId.Value : Guid.Empty;
        }

        public void WriteError(Exception ex, string location, string createBy)
        {
            string message = ex.Message + Environment.NewLine + ex.StackTrace;

            if (ex.InnerException != null)
            {
                message = $"{message} ***************> {ex.InnerException.Message}";
            }

            IErrorLogRepository repostirory = new ErrorLogRepository(db, _roleManager, _userManager);
            var errorLog = new ErrorLog { ErrorMessage = message, UserId = _UserId, CompanyId = _CompnayId, CreatedBy = createBy, CreateDate = DateTime.Now, Location = location, ErrorCode = ex.HResult.ToString() };
            var result = repostirory.CreateErrorLog(errorLog);
        }
    }
}