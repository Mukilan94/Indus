using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.DLL.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.Areas.Identity.Pages.Account;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class PaymentMethodsController : BaseController
    {
        private readonly ILogger<PaymentMethodsController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private TenantOperatingDbContext _tdbContext;
        public PaymentMethodsController(UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext, TenantOperatingDbContext tdbContext, ILogger<PaymentMethodsController> logger)
            : base(userManager, dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _tdbContext = tdbContext;
            _logger = logger;
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
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var months = new List<SelectListItem> {
                    new SelectListItem {
                        Text = "1- January",
                        Value = "1"
                    },
                    new SelectListItem {
                        Text = "2 - February",
                        Value = "2"
                    },
                    new SelectListItem {
                        Text = "3 - March",
                        Value = "3"
                    },
                    new SelectListItem {
                        Text = "4 - April",
                        Value = "4"
                    },
                    new SelectListItem {
                        Text = "5 - May",
                        Value = "5"
                    },
                    new SelectListItem {
                        Text = "6 - June",
                        Value = "6"
                    },
                    new SelectListItem {
                        Text = "7 - July",
                        Value = "7"
                    },
                    new SelectListItem {
                        Text = "8 - August",
                        Value = "8"
                    },
                    new SelectListItem {
                        Text = "9 - September",
                        Value = "9"
                    },
                    new SelectListItem {
                        Text = "10 - October",
                        Value = "10"
                    },
                    new SelectListItem {
                        Text = "11 - November",
                        Value = "11"
                    },
                    new SelectListItem {
                        Text = "12 - December",
                        Value = "12"
                    },
                };
                ViewData["MonthsData"] = months;
                var years = new List<SelectListItem>();
                var startYear = DateTime.Now.Year;
                for (int i = 0; i < 6; i++)
                {
                    years.Add(new SelectListItem
                    {
                        Text = (startYear + i).ToString(),
                        Value = (startYear + i).ToString()
                    });
                }
                ViewData["YearsData"] = years;
                var states = new List<SelectListItem>();
                var usastates = db.USAStates.ToList();
                for (int i = 0; i < usastates.Count; i++)
                {
                    states.Add(new SelectListItem
                    {
                        Text = usastates[i].Name,
                        Value = usastates[i].StateId.ToString()
                    });
                }
                ViewData["States"] = states;
                var countries = new List<SelectListItem>();
                var countrylist = db.Countries.ToList();
                for (int i = 0; i < countrylist.Count; i++)
                {
                    countries.Add(new SelectListItem
                    {
                        Text = countrylist[i].Name,
                        Value = countrylist[i].ID
                    });
                }
                ViewData["Countries"] = countries;
                var paytypes = new List<SelectListItem>();
                var paytypelist = db.PaymentType.ToList();
                for (int i = 0; i < paytypelist.Count; i++)
                {
                    paytypes.Add(new SelectListItem
                    {
                        Text = paytypelist[i].Name,
                        Value = paytypelist[i].ID
                    });
                }
                ViewData["PayTypes"] = paytypes;
                var creditCardTypes = new List<SelectListItem>();

                var creditCardTypeList = db.CreditcardType.ToList();
              
                for (int i = 0; i < creditCardTypeList.Count; i++)
                {
                        creditCardTypes.Add(new SelectListItem
                        {
                            Text = creditCardTypeList[i].CardType,
                            Value = creditCardTypeList[i].ID.ToString()
                        });
                }
                ViewData["CreditCardTypes"] = creditCardTypes;
                
                
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethods Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult Terms()
        {
            return View();
        }

        private bool GetComponentsBasedOnRole()
        {
            var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            List<string> rolesName = (from rl in roles
                                      select rl.Value
                                 ).ToList();

            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
            var rolesResult = roleResult.Result;
            if (roleResult != null)
            {
                List<string> roleIds = (from rl in rolesResult
                                        select rl.Id
                                        ).ToList();
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "PaymentMethods",TenantId);
            }
            else
            {
                return false;
            }
        }

        public async Task<IActionResult> PaymentMethods_Read([DataSourceRequest] DataSourceRequest request)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            try
            {               
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager,db);
                var model = await operrepo.GetPaymentMethods(tenantId);
                foreach (var Carddetails in model)
                {
                    Carddetails.Number = await operrepo.DecryptData(Carddetails.Number);
                    Carddetails.ExpireMonth = await operrepo.DecryptData(Carddetails.ExpireMonth);
                    Carddetails.ExpireYear = await operrepo.DecryptData(Carddetails.ExpireYear);
                }
                if (model == null)
                    model = new List<PaymentMethod>();
                return Json(model.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethods_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> PaymentMethods_Create([DataSourceRequest] DataSourceRequest request, PaymentMethod method)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            try
            {                
                if (method != null && ModelState.IsValid)
                {
                    method.TenantId = tenantId;
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager ,db);
                    method = await operrepo.EncryptData(method);
                    var res = await operrepo.UpdatePaymentMethod(method);
                }
                return Json(new[] { method }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethods_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> PaymentMethods_Update([DataSourceRequest] DataSourceRequest request, PaymentMethod method)
        {
            var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            try
            {
                if (method != null && ModelState.IsValid)
                {
                    method.TenantId = tenantId;
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager,db);
                    method = await operrepo.EncryptData(method);
                    var res = await operrepo.UpdatePaymentMethod(method);
                }
                return Json(new[] { method }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethods_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> PaymentMethods_Destroy(string methodId)
        {
            try
            {
                if (!string.IsNullOrEmpty(methodId))
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager,db);
                    var res = await operrepo.DeletePaymentMethod(methodId);
                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "PaymentMethods_Destroy", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}