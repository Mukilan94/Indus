using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Kendo.Mvc.UI;
using AuthorizeNet.Api.Contracts.V1;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Areas.ServiceCompany.Controllers;
using WellAI.Advisor.DLL.Entity;
using Well_AI_Advisior.API.Authorize.Net;
using Microsoft.Extensions.Configuration;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;
using Well_AI_Advisior.API.Authorize.Net.Model;
using Microsoft.Extensions.DependencyInjection;
using AuthorizeNet.Api.Contracts.V1;
using WellAI.Advisor.API.Repository;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("ServiceCompany")]
    [SessionTimeOut]
    public class NotificationSRVController : BaseController
    {
        private readonly ILogger<ProductSubscriptionController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private TenantOperatingDbContext _tdbContext;
        private TenantServiceDbContext _servicedb;
        private readonly IConfiguration _configuration;
        public NotificationSRVController(
            UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext,
           TenantOperatingDbContext tdbContext,
           IConfiguration configuration,
           ILogger<ProductSubscriptionController> logger) : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
            _configuration = configuration;
            _tdbContext = tdbContext;

        }



        public IActionResult Index()
        {

            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Notification Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return View();
        }

        public async Task<IActionResult> Notification_Read([DataSourceRequest] DataSourceRequest request)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            try
            {
                var userId = WellAIAppContext.Current.Session.GetString(DLL.Constants.SessionNotExpireKey);
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager, db);
                var model = await operrepo.GetNotifications(userId);

                if (model == null)
                    model = new List<MessageQueue>();
                return Json(model.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Notification_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> Notification_Destroy(int msgId)
        {
            try
            {
                if (msgId != 0)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager, db);
                    var res = await operrepo.DeleteNotification(msgId);
                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Notification_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSelectedNotifications([FromBody] List<MessageQueue> notifications)
        {
            CommunicationViewModel model = new CommunicationViewModel();
            try
            {
                if (notifications != null && notifications.Count > 0)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager, db);
                    var result = await operrepo.DeleteSelectedNotifications(notifications);
                    //if (result)
                    //{
                    //    model = await GetCommunicationModel();
                    //    return PartialView("_AddContacts", model.CommunicationModel);
                    //}
                    //else
                    //{
                    return Json(true);
                    //}
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Notification DeleteSelectedNotifications", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

    }
}
