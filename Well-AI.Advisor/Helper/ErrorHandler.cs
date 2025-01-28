using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Helper
{
    //ErrorHandler class build to handle it at common place to call errorLog business.
    public class ErrorHandler
    {
        private SignInManager<WellIdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private WebAIAdvisorContext db;
        public ErrorHandler(SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
        }

        //adding error into errorlog entity
        public bool ErrorLog(string message, string createBy,string erCode)
        {
            IErrorLogBusiness errorBusiness = new ErrorLogBusiness(db, _roleManager, _userManager);
            var errorLog = new ErrorLog { ErrorMessage = message, CreatedBy = createBy, CreateDate = DateTime.Now, ErrorCode = erCode };
            var result = errorBusiness.CreateErrorLog(errorLog);
            return true;
        }
    }
}
