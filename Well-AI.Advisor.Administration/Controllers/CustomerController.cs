using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Well_AI.Advisor.API.PEC.Services.IServices;
using WellAI.Advisor;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Well_AI.Advisor.API.PEC.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Well_AI.Advisor.Log.Error;
using Microsoft.Extensions.Logging;
using System.Net;
using Telerik.Web.PDF;
using WellAI.Advisor.Areas.Identity;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;

namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    //[SessionTimeOut]
    public class CustomerController : BaseController
    {
        //Phase II - Clear Warning
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
        //Phase II - Clear Warning

        //Phase II Changes-01/12/2021 - Add OperatingDBContext to Read Vendor Directory
        private TenantOperatingDbContext _tdbContext;
        private readonly TenantOperatingDbContext _operdb;
        private readonly object lLogger;
        private ISession _session;
        private TenantServiceDbContext _servicedb;
        private object _logger;

        public ISession Session { get { return _session; } }

        //Phase II Changes-01/12/2021 - Add _operdb for DbContext
        public CustomerController(IConfiguration _configuration, UserManager<WellIdentityUser> userManager,
           RoleManager<IdentityRole> roleManager,
            ISingletonAdministration _singleton, WebAIAdvisorContext db, TenantOperatingDbContext operdb, TenantOperatingDbContext tdbContext,
            TenantServiceDbContext servicedb, UserManager<StaffWellIdentityUser> staffUserManager) :base(_configuration,_singleton,db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _operdb = operdb;
            _tdbContext = tdbContext;
            _session = WellAIAppContext.Current.Session;
            _servicedb = servicedb;
            _staffUserManager = staffUserManager;

        }

        public async Task<IActionResult> Index(string id)
        {

            try
            {
                //if (_signInManager.IsSignedIn(User) == false)
                //{
                //    string returnUrl = @"/Identity/Account/Login";
                //    return LocalRedirect(returnUrl);
                //}

                WellAI.Advisor.Model.Common.Retrieve RetriveDetails = new WellAI.Advisor.Model.Common.Retrieve();


                // var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var tenantId = id;
                //Customer details
               // WellAIAppContext.Current.Session.SetString("TenantId", tenantId);
                ViewBag.CustomerDetails = db.CorporateProfile.Where(x => x.TenantId.Equals(tenantId)).FirstOrDefault();

                //Subscription details
                ViewBag.CustomerSubscriptions = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).FirstOrDefault();



                string packageId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.PackageId).FirstOrDefault();
                string paymentMethodId = db.Subscription.Where(x => x.TenantId.Equals(tenantId)).Select(s => s.PaymentMethodId).FirstOrDefault();

                //package details
                ViewBag.CustomerSubscriptionsPackage = db.SubscriptionPackage.Where(x => x.PackageId == new Guid(packageId)).FirstOrDefault();

                ViewBag.PaymentDetails = db.PaymentMethods.Where(x => x.ID == paymentMethodId).FirstOrDefault();
                ViewBag.PackageDetailsList = db.SubscriptionPackage.Where(x => x.PackageId.ToString().ToUpper()
                == "D350AC56-27E8-4442-9F1D-3EB6088DB9DA" || x.PackageId.ToString().ToUpper()
                == "558911F7-876E-40C2-AF4F-388DBD83CB6D" || x.PackageId.ToString().ToUpper()
                == "0D280653-7177-4DD3-9915-6CB83433BF70").OrderBy(x => x.PackageAmount).ToList();

                WellAIAppContext.Current.Session.SetString("TenantId", tenantId);
                //string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                //---
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
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Index", User.Identity.Name);
                string returnUrl = @"/DashboardSRV/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult Operator()
        {
            try
            {
                ViewBag.AccountType = 0;
                TempData["accountType"] = 0;
                ViewBag.accountType = 0;
                WellAIAppContext.Current.Session.SetInt32("accountType", 0);
                TempData.Keep();
                ViewBag.Customer = "Operator Company";

                //ViewBag.AccountType = "0,3";
                //TempData["accountType"] = "0,3";
                //ViewBag.accountType = "0,3";
                //WellAIAppContext.Current.Session.SetString("accountType", "0,3");
                //TempData.Keep();
                //ViewBag.Customer = "Operator Company";

                return View();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer Operator", User.Identity.Name);
                
                return null;
            }
        }
        public IActionResult Dispatch()
        {
            try
            {
                //ViewBag.AccountType = 2;
                //TempData["accountType"] = 2;
                //ViewBag.accountType = 2;
                //WellAIAppContext.Current.Session.SetInt32("accountType", 2);
                //TempData.Keep();
                //ViewBag.Customer = "Dispatch";
                ViewBag.AccountType = 2;
                TempData["accountType"] = 2;
                ViewBag.accountType = 2;
                WellAIAppContext.Current.Session.SetInt32("accountType", 2);
                TempData.Keep();
                ViewBag.Customer = "Dispatch";
                return View();
               // return PartialView("Dispatch");
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer Operator", User.Identity.Name);

                return null;
            }
        }
        public ActionResult CustomerByAccountType_Read([DataSourceRequest] DataSourceRequest request,int accountType)
        {
            try
            {
                var serviceCategory = _singleton.customerProfileBusiess.GetCustomerProfiles(accountType, request.Page, request.PageSize).Result;
                return Json(serviceCategory.Item1.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerByAccountType_Read", User.Identity.Name);
                return null;
            }         
        }

        #region Customer Users

        
        public IActionResult CustomerUser(string id)
        {
            //DWOP
            WellAIAppContext.Current.Session.SetString("AdminSessionCurrentTenantId", id.ToString());
            ViewBag.TenantId = id;
            return PartialView("CustomerUser");
        }
        public ActionResult CustomerUsers_Read([DataSourceRequest] DataSourceRequest request, string tenantId)
        {
            try
            {
                WellAIAppContext.Current.Session.SetString("AdminSessionCurrentTenantId", tenantId);
                var serviceCategory = _singleton.customerProfileBusiess.GetCustomerUsers(tenantId, request.Page, request.PageSize).Result;
                return Json(serviceCategory.Item1.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerUsers_Read", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> AddUpdateUsers_Create([DataSourceRequest] DataSourceRequest request, UserViewModel input, string tenantId)
        {

            try
            {
                var resultUserId = "";

                if (ModelState.IsValid && input != null)
                {

                    if (input.IsPrimary)
                    {
                        var tenantUsers = db.WellIdentityUser.Where(x => x.TenantId == input.UserTenantId && x.Primary == true).ToList();

                        if (tenantUsers.Count > 0)
                        {
                            ModelState.AddModelError("IsPrimary", "Primary Contact Already exits");
                            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                        }
                    }

                    var user = new WellIdentityUser
                    {
                        UserName = input.Email,
                        FirstName = input.FirstName,
                        MiddleName = input.MiddleName,
                        LastName = input.LastName,
                        JobTitle = input.JobTitle,
                        Mobile = input.Mobile,
                        Address = input.Address,
                        City = input.City,
                        State = input.State,
                        Zip = input.Zip,
                        AdditionalNotes = input.AdditionalNotes,
                        Email = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        Primary = input.IsPrimary,
                        TenantId = tenantId,
                        WellUser = input.WellOfficeUser,
                        Field = input.Field,
                        EmailConfirmed = true
                    };

                    var pass = Utils.GenerateRandomPassword();
                    var result = await _userManager.CreateAsync(user, pass);

                    if (result.Succeeded)
                    {
                        user.EmailConfirmed = true;
                        result = await _userManager.UpdateAsync(user);
                    }

                    if (result.Succeeded)
                    {
                        // var allroles = _singleton.commonBusiness.GetRoles(tenantId);                       
                           var allroles = _singleton.customerProfileBusiess.GetRoles(tenantId).Result;

                        if (!string.IsNullOrEmpty(input.SelectedRoles))
                        {
                            var roles = input.SelectedRoles.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < roles.Length; i++)
                            {
                                var roleName = allroles.FirstOrDefault(x => x.Id == roles[i]);
                                await _singleton.commonBusiness.AddUserRole(user, roleName.Name);
                            }
                        }

                        if (input.WellOfficeUser && !string.IsNullOrEmpty(input.SelectedWells))
                        {

                            var RigId = input.SelectedWells.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                            await _singleton.aiDataRepositoryBusiess.AssignRigsToUser(user.Id, RigId);

                        }

                        var tenantExistId = await _singleton.commonBusiness.GetTenantIdByUserId(user.Id);
                        if (tenantExistId == null || tenantExistId == "")
                        {
                            user.TenantId = tenantId;
                            var result1 = await _userManager.UpdateAsync(user);

                            await _singleton.commonBusiness.CreateTenantUser(user.Id, tenantId);
                        }

                        int accountType = Convert.ToInt32(WellAIAppContext.Current.Session.GetInt32("accountType"));// Convert.ToInt32(TempData["accountType"]);
                        //int accountType = ViewBag.AccountType;
                        TempData.Keep();
                        //ViewBag.accountType = accountType;

                        var crmUserBasicDetail = new CrmUserBasicDetail
                        {
                            UserId = user.Id,
                            Name = string.Format("{0} {1} {2}", input.FirstName, input.MiddleName, input.LastName),
                            AccountType = accountType,
                            IsActive = true,
                            IsMaster = false,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedDate = DateTime.UtcNow
                        };
                        var status = _singleton.commonBusiness.CreateUserBasicDetail(crmUserBasicDetail);
                        if (status == true)
                        {
                            try
                            {

                                EmailHandler emailHandler = new EmailHandler();
                                MessageToQueue message = new MessageToQueue
                                {
                                    FromEmail = null,
                                    FromName = "Well AI",
                                    MsgBody = $"Dear Customer, please login with auto generated password {pass}, thanks",
                                    MsgSubject = "Auto Generated Password",
                                    ToEmail = user.Email,
                                    ToName = user.FirstName
                                };
                                emailHandler.SendMessageToQueue(message);
                                _singleton.commonBusiness.UpdateUserPagesCompleteStatus(3, user.Id);
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError(string.Empty, ex.Message);
                            }

                        }

                        resultUserId = user.Id;

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                if (resultUserId != "")
                {
                    input.IsActive = true;
                    input.UserID = resultUserId;
                }

                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer AddUpdateUsers_Create", User.Identity.Name);
                
                return null;
            }
        }

        public async Task<IActionResult> AddUpdateUsers_Update([DataSourceRequest] DataSourceRequest request, CustomerUsersModel input)
        {
            try
            {
                if (input != null && ModelState.IsValid)
                {

                    if (input.IsPrimary)
                    {
                        var tenantUsers = db.WellIdentityUser.Where(x => x.TenantId == input.UserTenantId && x.Primary == true).ToList();

                        if (tenantUsers.Count > 0)
                        {
                            ModelState.AddModelError("IsPrimary", "Primary Contact Already exits");
                            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                        }
                    }

                    var user = new WellIdentityUser
                    {
                        Id = input.UserID,
                        UserName = input.Email,
                        FirstName = input.FirstName,
                        MiddleName = input.MiddleName,
                        LastName = input.LastName,
                        JobTitle = input.JobTitle,
                        Mobile = input.Mobile,
                        Address = input.Address,
                        City = input.City,
                        State = input.State,
                        Zip = input.Zip,
                        AdditionalNotes = input.AdditionalNotes,
                        Email = input.Email,
                        PhoneNumber = input.PhoneNumber,
                        Primary = input.IsPrimary,
                        TenantId = input.UserTenantId,
                        WellUser = input.WellOfficeUser,
                        Field = input.Field
                    };
                    var result = await _singleton.commonBusiness.UpdateUser(user);

                    var newuser = _singleton.commonBusiness.GetUserDetail(user.Id);
                    var allroles = _singleton.commonBusiness.GetRoles(input.UserTenantId);
                    var userRoleNames = await _singleton.commonBusiness.GetUserRoleNames(newuser);
                    // remove all roles of user
                    await _singleton.commonBusiness.RemoveAllUserRoles(newuser, userRoleNames);
                    // add new roles
                    if (!string.IsNullOrEmpty(input.SelectedRoles))
                    {
                        var roles = input.SelectedRoles.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < roles.Length; i++)
                        {
                            var roleName = allroles.FirstOrDefault(x => x.Id == roles[i]);
                          if(roleName!=null)
                            {
                                await _singleton.commonBusiness.AddUserRole(newuser, roleName.Name);
                            }
                        }
                          
                    }
                    //int accountType = Convert.ToInt32(TempData["accountType"]);
                    int accountType = Convert.ToInt32(WellAIAppContext.Current.Session.GetInt32("accountType"));// Convert.ToInt32(TempData["accountType"]);
                    TempData.Keep();
                    if (accountType == 0)
                    {


                        var RigId = new List<string>();

                        // save assigned wells only for well type users
                        if (input.WellOfficeUser && !string.IsNullOrEmpty(input.SelectedWells))
                            RigId = input.SelectedWells.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                        await _singleton.customerProfileBusiess.AssignRigsToUser(user.Id, RigId);
                    }

                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer AddUpdateUsers_Update", User.Identity.Name);
                
                return null;
            }

            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
        }

        public async Task<JsonResult> ForgotPassword(string Email)
        {
            try
            {
                bool result = true;
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    var pass = Utils.GenerateRandomPassword();
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, pass);
                    EmailHandler emailHandler = new EmailHandler();
                    MessageToQueue message = new MessageToQueue
                    {
                        FromEmail = null,
                        FromName = "Well AI",
                        MsgBody = $"Dear Customer, please login with auto generated password {pass}, thanks",
                        MsgSubject = "Reset Password",
                        ToEmail = user.Email,
                        ToName = user.FirstName
                    };
                    emailHandler.SendMessageToQueue(message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "email address is not existing into system.");
                    result = false;
                }
                return Json(result);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ForgotPassword", User.Identity.Name);
                
                return null;
            }
        }
        #endregion

        #region Well Data
        public IActionResult CustomerWellData(string id)
        {
            try
            {
                var tndid = id;

                var wellTypes = (from tp in db.WellType
                                 select new WellAI.Advisor.Model.OperatingCompany.Models.WellTypeModel { wellTypeId = tp.welltype_id, wellTypeName = tp.welltype_name }
                                 ).ToList();
                var riglist = (from rig in db.rig_register
                               where rig.TenantID.Equals(tndid) && rig.isActive.Equals(true)
                               select new RigList
                               {
                                   Rig_Id = rig.Rig_id,
                                   Rig_Name = rig.Rig_Name
                               }).ToList();
                var padlist = (from pad in db.pad_register
                               where pad.TenantID.Equals(tndid) && pad.isActive.Equals(true)
                               select new PadList
                               {
                                   Pad_Id = pad.Pad_id,
                                   Pad_Name = pad.Pad_Name
                               }).ToList();
                var batchlist = (from batch in db.BatchDillingType_Register
                                 select new BatchDillingType
                                 {
                                     BatchDrillingType_Id = batch.BatchDrillingType_Id,
                                     BatchDrillingType = batch.BatchDrillingType
                                 }).ToList();
                var BasinNmaes = (from basin in db.BasinTypes
                                  select new BasinTypeModel
                                  {
                                      Basin_ID = basin.Basin_ID,
                                      BasinType_name = basin.BasinType_name

                                  }).ToList();
                ViewData["BasinType_Names"] = BasinNmaes;
                ViewData["riglist"] = riglist;
                ViewData["batchlist"] = batchlist;
                ViewData["padlist"] = padlist;
                ViewData["wellTypes"] = wellTypes;
                ViewBag.TenantId = id;
                return View();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerWellData", User.Identity.Name);
                
                return null;
            }
        }
        public async Task<ActionResult> GetWellData([DataSourceRequest] DataSourceRequest request, string tenantId)
        {
            try
            {
                //DWOP
                WellAIAppContext.Current.Session.SetString("AdminSessionCurrentTenantId", tenantId);
                //var customerWellData = _singleton.customerProfileBusiess.GetCustomerWellData(tenantId, request.Page, request.PageSize).Result;                
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                List<WellMasterDataViewModel> wellMasterResult = new List<WellMasterDataViewModel>();
                wellMasterResult = await commonBusiness.GetWellRegister(tenantId, "", false);

                return Json(wellMasterResult.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetWellData", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> WellData_Create([DataSourceRequest] DataSourceRequest request, WellMasterDataViewModel input, IFormCollection form, string tenantId)
        {
            try
            {
                ModelState.Remove("chartColor");
                if (input != null)
                {
                    input.chartColor = form["chartColor2"].ToString();

                    input = await _singleton.customerProfileBusiess.CreateCustomerWellData(tenantId, input);
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer WellData_Create", User.Identity.Name);
                
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> WellData_Update([DataSourceRequest] DataSourceRequest request, WellMasterDataViewModel input,IFormCollection form, string tenantId)
        {
            try
            {
                ModelState.Remove("chartColor");
                if (input != null)
                {
                    input.chartColor = form["chartColor2"].ToString();

                    input = await _singleton.customerProfileBusiess.UpdateCustomerWellData(tenantId, input);
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer WellData_Update", User.Identity.Name);
                
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> WellDataDestroy(string wellId)
        {
            try
            {
                bool IsRemove = false;

                if (!string.IsNullOrEmpty(wellId))
                {
                    IsRemove = await _singleton.customerProfileBusiess.WellDataDestroy(wellId);
                }
                return Json(IsRemove);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer WellDataDestroy", User.Identity.Name);
                
                return null;
            }
        }
        #endregion

        #region Rigs
        public IActionResult CustomerRigs(string id)
        {
            try
            {
                ViewBag.TenantId = id;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerRigs", User.Identity.Name);
                
                return null;
            }
        }
        public async Task<IActionResult> GetRigMasterRead([DataSourceRequest] DataSourceRequest request, string tenantId)
        {
            try
            {
                var result = await _singleton.customerProfileBusiess.GetCustomerRigs(tenantId);

                return Json(result.ToDataSourceResult(request, ModelState));

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetRigMasterRead", User.Identity.Name);
                
                return null;
            }
        }
        public async Task<IActionResult> RigData_Create([DataSourceRequest] DataSourceRequest request, RigModel input, string tenantId)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    input.TenantID = tenantId;
                    input = await _singleton.customerProfileBusiess.CreateCustomerRigs(input);
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer RigData_Create", User.Identity.Name);
                
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> RigData_Update([DataSourceRequest] DataSourceRequest request, RigModel input, string tenantId)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    input.TenantID = tenantId;
                    input.isActive = true;
                    input = await _singleton.customerProfileBusiess.UpdateCustomerRigs(input);
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer RigData_Update", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> RigData_Destroy([DataSourceRequest] DataSourceRequest request, RigModel input, string tenantId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    input.TenantID = tenantId;
                    input.isActive = false;
                    input = await _singleton.customerProfileBusiess.UpdateCustomerRigs(input);
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer RigData_Destroy", User.Identity.Name);
                
                return null;
            }
        }
       
        #endregion

        #region Pads
        public IActionResult CustomerPads(string id)
        {
            try
            {
                ViewBag.TenantId = id;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerPads", User.Identity.Name);
                
                return null;
            }
        }
        public async Task<IActionResult> GetPadMasterRead([DataSourceRequest] DataSourceRequest request, string tenantId)
        {
            try
            {
                var result = await _singleton.customerProfileBusiess.GetCustomePads(tenantId);

                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetPadMasterRead", User.Identity.Name);
                
                return null;
            }
        }


        [AcceptVerbs("Post")]
        public async Task<IActionResult> PadData_Create([DataSourceRequest] DataSourceRequest request, PadModel input, string tenantId)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    input.TenantID = tenantId;
                    input = await _singleton.customerProfileBusiess.CreateCustomerPads(input);
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer PadData_Create", User.Identity.Name);
                
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> PadData_Update([DataSourceRequest] DataSourceRequest request, PadModel input, string tenantId)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    input.TenantID = tenantId;
                    input.isActive = true;
                    input = await _singleton.customerProfileBusiess.UpdateCustomerPads(input);
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer PadData_Update", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> PadData_Destroy([DataSourceRequest] DataSourceRequest request, PadModel input, string tenantId)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    input.TenantID = tenantId;
                    input.isActive = false;
                    input = await _singleton.customerProfileBusiess.UpdateCustomerPads(input);
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer PadData_Destroy", User.Identity.Name);
                
                return null;
            }
        }

        #endregion

        #region Manage Permissions
        //public async Task<IActionResult> CustomerManagePermissionsAsync(string id,int accountType)
        //{
        //    try
        //    {
        //        ViewBag.TenantId = id;
        //        ViewBag.AccountType = accountType;
        //        var components = await _singleton.customerProfileBusiess.GetAllPermittedComponents(accountType);
        //        var comppermissions = await _singleton.customerProfileBusiess.GetRolePermissions(id);
        //        var permissions = new List<UserPermission>();

        //        foreach (var compPermission in comppermissions)
        //        {
        //            var permission = new UserPermission { Id = compPermission.PermissionId, Title = compPermission.PermissionName };
        //            permission.Components = new List<UserAction>();

        //            foreach (var component in components)
        //            {
        //                var newaction = new UserAction { Id = component.ComponentId, Title = component.ComponentName };

        //                var componentExist = compPermission.RolePermissionComponent
        //                    .FirstOrDefault(x => x.ComponentId == component.ComponentId && x.ComponentName == component.ComponentName);

        //                newaction.IsActive = componentExist != null && componentExist.IsPermitted;

        //                permission.Components.Add(newaction);
        //            }
        //            permissions.Add(permission);
        //        }
        //        var result = Utils.ToDataTable(permissions, components);
        //        return View(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
        //        customErrorHandler.WriteError(ex, "Customer CustomerManagePermissionsAsync", User.Identity.Name);

        //        return null;
        //    }
        //}





        public async Task<IActionResult> CustomerManagePermissionsAsync(string id, int accountType)
        {
            try
            {
                ViewBag.TenantId = id;
                ViewBag.AccountType = accountType;
                var components = await _singleton.customerProfileBusiess.GetAllPermittedComponents(accountType);
                var comppermissions = await _singleton.customerProfileBusiess.GetRolePermissions(id);
                var permissions = new List<ComponentRowModel>();
                List<UserPermissionComponants> ComponentsPermissions = new List<UserPermissionComponants>();

                foreach (var compPermission in comppermissions)
                {
                    var permission = new ComponentRowModel { ComponentId = compPermission.PermissionId, ComponentName = compPermission.PermissionName };
                    permission.Components = new List<UserAction>();

                    foreach (var component in components)
                    {
                        var newaction = new UserAction { Id = component.ComponentId, Title = component.ComponentName };
                        //ComponentsPermissions.Add( new UserPermissionComponants {ComponentId = component.ComponentId,ComponentName = component.ComponentName, IsPermitted = component.IsPermitted});
                        var componentExist = compPermission.RolePermissionComponent
                            .FirstOrDefault(x => x.ComponentId == component.ComponentId && x.ComponentName == component.ComponentName);

                        newaction.IsActive = componentExist != null && componentExist.IsPermitted;

                        permission.Components.Add(newaction);
                    }

                    permissions.Add(permission);
                }

                foreach(var comp in components)
                {
                   var ComponentsPer = new UserPermissionComponants { Id = comp.ComponentId, Title = comp.ComponentName };
                    ComponentsPer.Components = new List<UserAction>();
                    foreach (var compPermission in comppermissions)
                    {
                        var newaction = new UserAction { Id = compPermission.PermissionId, Title = compPermission.PermissionName, IsActive = compPermission.IsPermitted };
                        var componentExist = compPermission.RolePermissionComponent
                            .FirstOrDefault(x => x.ComponentId == comp.ComponentId && x.ComponentName == comp.ComponentName);
                        newaction.IsActive = componentExist != null && componentExist.IsPermitted;
                        ComponentsPer.Components.Add(newaction);
                    }

                    ComponentsPermissions.Add(ComponentsPer);
                }


                var result = Utils.ToPermissionDataTable(permissions, ComponentsPermissions);
                return View(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerManagePermissionsAsync", User.Identity.Name);

                return null;
            }
        }



        public async Task<ActionResult> ManagePermissions_Read([DataSourceRequest] DataSourceRequest request,string tenantId,int accountType)
        {
            try
            {
                var components = await _singleton.customerProfileBusiess.GetAllPermittedComponents(accountType);
                var comppermissions = await _singleton.customerProfileBusiess.GetRolePermissions(tenantId);
                var permissions = new List<ComponentRowModel>();
                List<UserPermissionComponants> ComponentsPermissions = new List<UserPermissionComponants>();

                foreach (var compPermission in comppermissions)
                {
                    var permission = new ComponentRowModel { ComponentId = compPermission.PermissionId, ComponentName = compPermission.PermissionName };
                    permission.Components = new List<UserAction>();

                    foreach (var component in components)
                    {
                        var newaction = new UserAction { Id = component.ComponentId, Title = component.ComponentName };
                        //ComponentsPermissions.Add( new UserPermissionComponants {ComponentId = component.ComponentId,ComponentName = component.ComponentName, IsPermitted = component.IsPermitted});
                        var componentExist = compPermission.RolePermissionComponent
                            .FirstOrDefault(x => x.ComponentId == component.ComponentId && x.ComponentName == component.ComponentName);

                        newaction.IsActive = componentExist != null && componentExist.IsPermitted;

                        permission.Components.Add(newaction);
                    }

                    permissions.Add(permission);
                }

                foreach (var comp in components)
                {
                    var ComponentsPer = new UserPermissionComponants { Id = comp.ComponentId, Title = comp.ComponentName };
                    ComponentsPer.Components = new List<UserAction>();
                    foreach (var compPermission in comppermissions)
                    {
                        var newaction = new UserAction { Id = compPermission.PermissionId, Title = compPermission.PermissionName, IsActive = compPermission.IsPermitted };
                        var componentExist = compPermission.RolePermissionComponent
                            .FirstOrDefault(x => x.ComponentId == comp.ComponentId && x.ComponentName == comp.ComponentName);
                        newaction.IsActive = componentExist != null && componentExist.IsPermitted;
                        ComponentsPer.Components.Add(newaction);
                    }

                    ComponentsPermissions.Add(ComponentsPer);
                }

                var result = Utils.ToPermissionDataTable(permissions, ComponentsPermissions);
                return Json(result.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ManagePermissions_Read", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ManagePermissions_Create([DataSourceRequest] DataSourceRequest request, string data,string tenantId)
        {
            try
            {
                var userIdentityName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                var tocreate = input.Where(x => x["Id"] == null || x["Id"] == "").ToList();
                foreach (var newItem in tocreate)
                {
                    var permcomps = new List<RolePermissionComponentModel>();

                    foreach (var permstatus in newItem)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                        {
                            var permcomp = new RolePermissionComponentModel
                            {
                                ComponentName = permstatus.Key,
                                IsPermitted = permstatus.Value == "true"
                            };
                            permcomps.Add(permcomp);
                        }
                    }

                    await _singleton.customerProfileBusiess.CreatePermissionComponents(newItem["Title"], permcomps, userIdentityName, tenantId);
                }

                var dt = Utils.ToDataTable(new List<UserPermission>(), new List<ComponentModelRec>());
                return Json(dt.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ManagePermissions_Create", User.Identity.Name);
                
                return null;
            }
        }

        public async Task<IActionResult> UpdateManagePermissions([DataSourceRequest] DataSourceRequest request, string data, string tenantId)
        {
            try
            {
                var PerData = JsonConvert.DeserializeObject<List<Dictionary<string,string>>>(data);
                var toedit = PerData.Where(x => x["Id"] != null && x["Id"] != "").ToList();

                foreach(var PerComdata in toedit)
                {
                    var permcomps = new List<RolePermissionComponentModel>();

                    foreach (var permstatus in PerComdata)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                        {
                            var permcomp = new RolePermissionComponentModel
                            {
                                ComponentName = permstatus.Key,
                                IsPermitted = permstatus.Value == "true"
                            };
                            permcomps.Add(permcomp);
                        }
                    }
                }

                var dt = Utils.ToDataTable(new List<UserPermission>(), new List<ComponentModelRec>());
                return Json(dt.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ManagePermissions_Update", User.Identity.Name);
                return null;
            }
        }

        public async Task<ActionResult> ManagePermissions_Update([DataSourceRequest] DataSourceRequest request, string data, string tenantId)
        {
            try
            {
                var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                var toedit = input.Where(x => x["Id"] != null && x["Id"] != "").ToList();

                //As per new logic - Convert back rows to columns (Permission Components -Dashboard,..) - and columns to rows (Permission Titles - Supervisor,...) 
                //at update logic
                DataTable table = new DataTable();
                table.Columns.Add("Id", typeof(int));
                table.Columns.Add("Title", typeof(string));

                table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };
                //foreach (var col in columns)
                //    table.Columns.Add(col.ComponentName, typeof(bool));
                foreach (var editItem in toedit)
                {
                    try
                    {
                        foreach (var permstatus in editItem)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key == "Title")
                        {
                            table.Columns.Add(permstatus.Value, typeof(bool));
                        }
                    }
                    }
                    catch (Exception e)
                    {
                        string vl = e.ToString();
                    }
                    //break;
                }


                int i = 1;
                foreach (var editItem in toedit)
                {
                    var itemArray = editItem.ToArray();
                    for (int j = 2; j <= itemArray.Length - 1; j++)
                    {
                        DataRow row = table.NewRow();
                        var col = itemArray[j];
                        row["Id"] = i;
                        if (col.Key != "Id" && col.Key != "Title")
                        {
                            row["Title"] = col.Key;
                            table.Rows.Add(row);
                        }
                        //
                        i++;
                    }
                    break;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    //columns
                    var totalRows = toedit.ToArray();
                    for (int m = 0; m < totalRows.Length; m++)
                    {
                        try
                        {


                            var itemArray = totalRows[m].ToArray();
                            var colName = "";
                            for (int k = 1; k < itemArray.Length; k++)
                            {
                                var col = itemArray[k];

                                if (col.Key == "Title")
                                {
                                    colName = col.Value;
                                }

                                if (col.Key != "Id" && col.Key != "Title")
                                {
                                    if (col.Key == table.Rows[j][1].ToString())
                                    {
                                        //dr["Dashboard"]
                                        table.Rows[j][colName] = Convert.ToBoolean(col.Value);
                                        colName = "";
                                    }
                                }
                            }
                        }catch (Exception e)
                        {
                            string vl = e.ToString();
                        }
                    }

                    //table.Rows[j][0] = "";
                }
                tenantId= HttpContext.Session.GetString("AdminSessionCurrentTenantId");
                if ((tenantId != "" && tenantId != null).Equals(true))
                {
                    List<RolePermissionModel> permissions = db.RolePermissions.Where(x => x.TenantId == tenantId).Select(
                                                          x => new RolePermissionModel
                                                          {
                                                              PermissionId = x.RolePermissionId,
                                                              PermissionName = x.RolePermissionName
                                                          }).ToList();


                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        var permcomps = new List<RolePermissionComponentModel>();
                        //table.Rows[j][colName]
                        var dr = table.Rows[j];
                        for (int k = 2; k < table.Columns.Count; k++)
                        {
                            var permcomp = new RolePermissionComponentModel
                            {
                                ComponentName = table.Columns[k].ColumnName,
                                IsPermitted = Convert.ToBoolean(table.Rows[j][k])// == "true"
                            };
                            permcomps.Add(permcomp);
                        }
                        int permissionId = permissions.Where(x => x.PermissionName == table.Rows[j]["Title"].ToString()).Select(x => x.PermissionId).FirstOrDefault();

                        await _singleton.customerProfileBusiess.UpdatePermissionComponents(Convert.ToInt32(permissionId), table.Rows[j]["Title"].ToString(), permcomps);
                    }

                }

                //foreach (var editItem in toedit)
                //{
                //    var permcomps = new List<RolePermissionComponentModel>();

                //    foreach (var permstatus in editItem)
                //    {
                //        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                //        {
                //            var permcomp = new RolePermissionComponentModel
                //            {
                //                ComponentName = permstatus.Key,
                //                IsPermitted = permstatus.Value == "true"
                //            };
                //            permcomps.Add(permcomp);
                //        }
                //    }


                //    await _singleton.customerProfileBusiess.UpdatePermissionComponents(Convert.ToInt32(editItem["Id"]), editItem["Title"], permcomps);
                //}

                var dt = Utils.ToDataTable(new List<UserPermission>(), new List<ComponentModelRec>());
                return Json(dt.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ManagePermissions_Update", User.Identity.Name);

                return null;
            }
        }
        [HttpPost]

        [HttpPost]
        public async Task<ActionResult> ManagePermissions_Destroy(string permissionName)
        {
            string tid = HttpContext.Session.GetString("TenantId").ToString();
                
          //  HttpContext.Session.GetString("AdminSessionCurrentTenantId").ToString();
          //   var permissions = db.RolePermissions.Where(x => x.RolePermissionName == permissionName && x.TenantId == HttpContext.Session.GetString("AdminSessionCurrentTenantId").ToString()).FirstOrDefault();
            var permissions = db.RolePermissions.Where(x => x.RolePermissionName == permissionName && x.TenantId == HttpContext.Session.GetString("TenantId").ToString()).FirstOrDefault();
            if (permissions != null)
            {
                await _singleton.customerProfileBusiess.DeletePermission(permissions.RolePermissionId);
            }
            return Json(new object());
        }
        #endregion

        #region Manage Role
        public async Task<IActionResult> CustomerManageRole(string id)
        {
            try
            {
                ViewBag.TenantId = id;
                var permissions = await _singleton.customerProfileBusiess.GetAllPermissions(id);//await rolePermissionBusiness.GetAllPermissions(WellAIAppContext.Current.Session.GetString("TenantId"));
                var roles = await GetRoles(permissions, id);
                var dt = Utils.ToDataTable(roles, permissions);

                return View(dt);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerManageRole", User.Identity.Name);
                
                return null;
            }
        }
        public async Task<ActionResult> ManageRoles_Read([DataSourceRequest] DataSourceRequest request,string tenantId)
        {
            try
            {
                var permissions = await _singleton.customerProfileBusiess.GetAllPermissions(tenantId);//await rolePermissionBusiness.GetAllPermissions(WellAIAppContext.Current.Session.GetString("TenantId"));
                var roles = await GetRoles(permissions, tenantId);
                var dt = Utils.ToDataTable(roles, permissions);

                return Json(dt.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ManageRoles_Read", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManageRoles_CreateAsync([DataSourceRequest] DataSourceRequest request, string data, string tenantId)
        {
            try
            {
                if (tenantId == null)
                {
                    return Json(null);
                }
                var userIdentityName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                var tocreate = input.Where(x => x["Id"] == null || x["Id"] == "").ToList();
                foreach (var newItem in tocreate)
                {
                    var roleperms = new List<RolePermissions>();

                    foreach (var permstatus in newItem)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                        {
                            var roleperm = new RolePermissions
                            {
                                RolePermissionName = permstatus.Key,
                                IsActive = permstatus.Value == "true"
                            };
                            roleperms.Add(roleperm);
                        }
                    }
                    string Title = newItem["Title"];
                    await _singleton.customerProfileBusiess.CreateRolePermissions(Title, roleperms, userIdentityName, tenantId);
                }

                var permissions = await _singleton.customerProfileBusiess.GetAllPermissions(tenantId);
                var roles = await GetRoles(permissions, tenantId);
                var dt = Utils.ToDataTable(roles, permissions);

                return Json(dt.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ManageRoles_CreateAsync", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManageRoles_UpdateAsync([DataSourceRequest] DataSourceRequest request, string data, string tenantId)
        {
            try
            {
                var input = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                var toedit = input.Where(x => x["Id"] != null || x["Id"] != "").ToList();

                foreach (var editItem in toedit)
                {
                    var roleperms = new List<RolePermissions>();

                    foreach (var permstatus in editItem)
                    {
                        if (permstatus.Key != "Id" && permstatus.Key != "Title")
                        {
                            var roleperm = new RolePermissions
                            {
                                RolePermissionName = permstatus.Key,
                                IsActive = permstatus.Value == "true"
                            };
                            roleperms.Add(roleperm);
                        }
                    }

                    await _singleton.customerProfileBusiess.UpdateRolePermissions(editItem["Id"], editItem["Title"], roleperms);
                }

                var dt = Utils.ToDataTable(new List<UserRole>(), new List<RolePermissions>());

                return Json(dt.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ManageRoles_UpdateAsync", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<ActionResult> ManageRoles_Destroy(string roleId)
        {
            try
            {
                await _singleton.customerProfileBusiess.DeleteRolePermission(roleId);
                return Json(new object());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer ManageRoles_Destroy", User.Identity.Name);
                
                return null;
            }
        }

        private async Task<List<UserRole>> GetRoles(List<RolePermissions> permissions,string tenantId)
        {
            try
            {
                var rolePermissions = await _singleton.customerProfileBusiess.GetRoleWithRolePermissions(tenantId);

                var roles = new List<UserRole>();

                foreach (var rolePermission in rolePermissions)
                {
                    var role = new UserRole { Id = rolePermission.Id, Title = rolePermission.RoleName };
                    role.Permissions = new List<UserAction>();

                    foreach (var permission in permissions)
                    {
                        var newaction = new UserAction { Id = permission.RolePermissionId, Title = permission.RolePermissionName };

                        var permissionExist = rolePermission.RolePermissions
                            .FirstOrDefault(x => x.PermissionId == permission.RolePermissionId && x.PermissionName == permission.RolePermissionName);

                        newaction.IsActive = permissionExist != null && permissionExist.IsPermitted;

                        role.Permissions.Add(newaction);
                    }

                    roles.Add(role);
                }

                return roles.ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetRoles", User.Identity.Name);
                
                return null;
            }
        }


        #endregion

        #region Customer Work Station
        public IActionResult CustomerWorkStation(string id)
        {
            try
            {
                ViewBag.TenantId = id;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerWorkStation", User.Identity.Name);
                
                return null;
            }
        }
        public async Task<IActionResult> GetWorkstationRead([DataSourceRequest] DataSourceRequest request, string tenantId)
        {
            try
            {
                var result = await _singleton.aiDataRepositoryBusiess.GetWorkstationDetail(tenantId);
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetWorkstationRead", User.Identity.Name);
                
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> WorkstationData_Create([DataSourceRequest] DataSourceRequest request, WorkstationModel input, string tenantId)
        {
            try
            {
                RegistrationSecrateKeyUtil keyUtil = new RegistrationSecrateKeyUtil();
                WorkstationRegister workStation = new WorkstationRegister();
                ModelState.Remove("RegisterationId");

                if (!ModelState.IsValid)
                {

                    workStation.RegisterationId = Guid.NewGuid();
                    workStation.CustomerAccountIdentifier = tenantId;
                    workStation.DeviceName = input.DeviceName;
                    workStation.WorkstationIdentifier = input.WorkstationIdentifier;
                    workStation.IsActive = input.IsActive;
                    workStation.CreatedDate = DateTime.Now;
                    workStation.WorkstationToken = keyUtil.GenerateKey();
                    input.CreatedDate = workStation.CreatedDate;
                    input.WorkstationToken = workStation.WorkstationToken;
                    db.WorkstationRegister.Add(workStation);
                    await db.SaveChangesAsync();
                };
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer WorkstationData_Create", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> workstationData_Update([DataSourceRequest] DataSourceRequest request, WorkstationModel input, string tenantId)
        {
            try
            {
                WorkstationRegister workStation = new WorkstationRegister();
                ModelState.Remove("CustomerName");

                if (ModelState.IsValid)
                {
                    workStation.RegisterationId = input.RegisterationId;
                    workStation.CustomerAccountIdentifier = tenantId;
                    workStation.DeviceName = input.DeviceName;
                    workStation.WorkstationIdentifier = input.WorkstationIdentifier;
                    workStation.WorkstationToken = input.WorkstationToken;
                    workStation.IsActive = input.IsActive;
                    workStation.CreatedDate = input.CreatedDate;

                    workStation.ModifiedDate = DateTime.UtcNow;
                    db.WorkstationRegister.Update(workStation);
                    await db.SaveChangesAsync();
                };

                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer workstationData_Update", User.Identity.Name);
                
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> workstationData_Destroy([DataSourceRequest] DataSourceRequest request, WorkstationModel input, string tenantId)
        {
            try
            {
                var workstationObj = db.WorkstationRegister.FirstOrDefault(x => x.RegisterationId == input.RegisterationId);
                db.WorkstationRegister.Remove(workstationObj);
                db.SaveChanges();
                var result = await _singleton.aiDataRepositoryBusiess.GetWorkstationDetail(tenantId);
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer workstationData_Destroy", User.Identity.Name);
                
                return null;
            }
        }
        #endregion
        public IActionResult CustomerInvoiceHistory(string id)
        {
            try
            {
                ViewBag.TenantId = id;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerInvoiceHistory", User.Identity.Name);
                
                return null;
            }
        }
        public async Task<IActionResult> BillingHistory_Read([DataSourceRequest] DataSourceRequest request, string tenantId)
        {
            try
            {
                var model = await _singleton.customerProfileBusiess.GetCustomerInvoiceHistory(tenantId);
                if (model == null)
                    model = new List<BillingHistory>();
                return Json(model.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer BillingHistory_Read", User.Identity.Name);
                
                return null;
            }
        }

        public ActionResult CustomerUserDetailById(string id)
        {
            try
            {
                CustomerUsersModel service = new CustomerUsersModel();
                service = _singleton.customerProfileBusiess.CustomerUserDetailById(id).Result;
                return View("CustomerUserDetailById", service);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerUserDetailById", User.Identity.Name);
                
                return null;
            }
        }
        public IActionResult Service()
        {
            ViewBag.AccountType = 1;
            TempData["accountType"] = 1;
            ViewBag.accountType = 1;
            WellAIAppContext.Current.Session.SetInt32("accountType", 1);
            TempData.Keep();
            ViewBag.Customer = "Service Company";

            //ViewBag.AccountType = "1,4";
            //TempData["accountType"] = "1,4";
            //ViewBag.accountType = "1,4";
            //WellAIAppContext.Current.Session.SetString("accountType", "1,4");
            //TempData.Keep();
            //ViewBag.Customer = "Service Company";


            return View("Operator");
        }
        public JsonResult GetUserRolesList(string id)
        {
            List<IdentityRole> result = new List<IdentityRole>();
            try
            {
                
                    result = _singleton.customerProfileBusiess.GetRoles(id).Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetUserRolesList", User.Identity.Name);
                
                return null;
            }
            return Json(result);
        }

        public JsonResult GetRigMasterList(string id)
        {
            List<IdentityRole> result = new List<IdentityRole>();
            try
            {
                var wells = _singleton.aiDataRepositoryBusiess.GetRigsForOperationCompany(id).Result;
                return Json(wells);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetRigMasterList", User.Identity.Name);
                
                return null;
            }
            //Phase II - Clear warning
            //return Json(result);
        }


        public JsonResult GetWellMasterList(string id)
        {
            List<IdentityRole> result = new List<IdentityRole>();
            try
            {
                var wells = _singleton.aiDataRepositoryBusiess.GetWellsForOperationCompany(id).Result;
                return Json(wells);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetWellMasterList", User.Identity.Name);
                
                return null;
            }
            //Phase II - Clear warning
            //return Json(result);
        }
        public async Task<IActionResult> CustomerDetail(string id)
        {
            try
            {
                var twilioCls = new TwilioChat(db, _roleManager, _userManager);
                string AdminSupportUser = twilioCls.GetAdminSupportUser();
                ViewBag.AdminSupportUser = AdminSupportUser.ToString();
                string AdminSupportPhone = twilioCls.GetAdminSupportPhone();
                ViewBag.AdminSupportPhone = AdminSupportPhone.ToString();
                WellAIAppContext.Current.Session.SetString("AdminSessionCurrentTenantId", id.ToString());
                WellAIAppContext.Current.Session.SetString("UserTenantId", id);
                WellAIAppContext.Current.Session.SetString("TenantId", id);
                ViewBag.TenantId = id;
                //await InitViewDataDicts(id);
                var serviceCmp = await _singleton.customerProfileBusiess.GetCustomerDetail(id);
                if (serviceCmp.LogoPath != null && serviceCmp.LogoPath != "")
                    serviceCmp.LogoPath = await GetUrlOfImage(serviceCmp.LogoPath, serviceCmp.TenantId);
                int SubscriptionRigsCount = serviceCmp.SubscriptionRigsCount;
                ViewBag.SubscribedRigsCount = Convert.ToString(SubscriptionRigsCount);
                TempData["SubscriptionRigsCount"] = Convert.ToString(SubscriptionRigsCount);
                return View(serviceCmp);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerDetail", User.Identity.Name);
                
                return null;
            }

        }

        private async Task<string> GetUrlOfImage(string filename,string tenantId)
        {
                var blobSection = _configuration.GetSection("AzureBlob");
                var folderName = _configuration.GetSection("FolderName");

                var items = await AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], tenantId, folderName["CompanyProfile"], filename);
                return items;
        }

        public async Task<JsonResult> SetSelectedRoles(string userId)
        {
            List<object> result = new List<object>();
            try
            {
                 var user = await _singleton.commonBusiness.GetUser(userId);
                if (user != null)
                {
                    foreach (var role in user.roles)
                    {
                        result.Add(new { Id = role.Id, Name = role.Name });
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer SetSelectedRoles", User.Identity.Name);
                
                return null;
            }

            return Json(result);
        }

        public async Task<JsonResult> UpdateSubscriptioIsEnable(string id)
        {
            try
            {
                var result = await _singleton.customerProfileBusiess.UpdateSubscriptioIsEnable(id);

                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer UpdateSubscriptioIsEnable", User.Identity.Name);
                
                return null;
            }
        }

        [AcceptVerbs("Get")]
        public async Task<JsonResult> SetSelectedWells(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    var re = await _singleton.customerProfileBusiess.GetUserAssignedRigs(userId);
                    var result = re.Select(x => new { x.rigID, x.rigName }).ToList();
                    return Json(result);
                }
                else
                {
                    var result = new { };
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer SetSelectedWells", User.Identity.Name);
                
                return null;
            }
        }




        /// <summary>
        /// Vendor Directory of Selected Operator - Phase II Changes - 01/13/2021
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> CustomerVendors(string id)
        {
            try
            {
                WellAIAppContext.Current.Session.SetString("TenantId", id);
                ViewBag.TenantId = id;
                await InitViewDataDicts(id);
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer CustomerVendors", User.Identity.Name);

                return null;
            }
        }

        public async Task<IActionResult> ProviderDirectoryMSA_Read([DataSourceRequest]DataSourceRequest request, string companyId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? WellAI.Advisor.DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var msadocuments = await commonBusiness.GetApprovedMSAWellFilesOfServiceTenant(new List<string> { companyId }, tenantId, wellId);
                return Json(msadocuments);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectoryMSA_Read", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        public async Task<IActionResult> ProviderProfileMSA([DataSourceRequest] DataSourceRequest request, string companyId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? WellAI.Advisor.DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var msadocuments = await commonBusiness.GetMSAWellFilesFromServiceTenants(new List<string> { companyId }, tenantId, wellId);
                return Json(msadocuments.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectoryMSA_Read", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        /// <summary>
        /// Vendor Directory to Approve Vendors at Admin Panel - Phase II Changes - 01/12/2021
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProviderDirectory_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var result = await GetProviderDirectory(tenantId);
                return Json(result.Providers.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                //Phase II - Clear Warning
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectory_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Read Vendor Directory - Phase II Changes - 01/12/2021
        /// </summary>
        /// <param name="pendingFilter"></param>
        /// <param name="insureExpireFilter"></param>
        /// <returns></returns>
        private async Task<ProviderDirectoryModel> GetProviderDirectory(string tenantId, bool pendingFilter = false, bool insureExpireFilter = false)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tId = Guid.Parse(tenantId);
                var dbprefix = "oper";
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + tId.ToString("N"));

                var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);
                var operContext = new TenantOperatingDbContext(ti);
                _tdbContext = operContext;
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);

                var providers = await operrepo.GetServiceProviderDirectoriesForAdmin();

                var pecs = (from Provider in operContext.ProvidersDirectory
                            join pec in operContext.ProviderDirectoryPECs
                            on Provider.PEC equals pec.Id
                            where pec.Name != "Good"
                            select Provider).Count();
                var providerdir = new ProviderDirectoryModel
                {
                    InsExpiring90days = providers.Count(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0),
                    Pending = providers.Count(x => x.Approval == "Pending review"),
                    Records = providers.Count,
                    ComplienceAlert = providers.Count(p => p.PecStatus != "Good")
                };
                //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                providerdir.PreferredProvider = providers.FirstOrDefault(x => x.Preferred == Convert.ToByte(3));
                providerdir.SecondaryProvider = providers.FirstOrDefault(x => x.Secondary);
                if (pendingFilter == true && insureExpireFilter == false)
                {
                    providers = providers.Where(x => x.Approval == "Pending review").ToList();
                }
                if (insureExpireFilter == true && pendingFilter == false)
                {
                    providers = providers.Where(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0).ToList();
                }
                if (insureExpireFilter == true && pendingFilter == true)
                {
                    providers = providers.Where(x => x.PecStatus != "Good").ToList();
                }
                List<string> tenantIds = null;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? WellAI.Advisor.DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var companies = new List<CorporateProfile>();
                var msafiles = new List<MSA>();
                if (companies == null || companies.Count == 0)
                {
                    companies = await commonBusiness.GetServiceCompanies();
                    var operatingcompanies = await commonBusiness.GetOperatingCompanies();
                    var servtenantIds = (from cp in db.CorporateProfile
                                         join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                         where cub.AccountType == 1
                                         select new CorporateProfile { TenantId = cp.TenantId }).ToList();

                   

                    tenantIds = servtenantIds.Select(x => x.TenantId).ToList();

                    msafiles = await commonBusiness.GetMSAWellFilesFromServiceTenants(tenantIds, tenantId.ToString(), wellId);

                }
                else
                    tenantIds = companies.Select(x => x.TenantId).ToList();

                var insurefiles = await commonBusiness.GetInsuranceWellFilesFromServiceTenants(tenantIds, tenantId, wellId);
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                foreach (var provider in providers)
                {
                    provider.MSADocument = "";
                    var company = companies.FirstOrDefault(x => x.TenantId.ToString() == provider.CompanyId);//provider.CompanyId=tenantId of Service Company
                    if (company != null)
                    {
                        var site = string.IsNullOrEmpty(company.Website) || company.Website.StartsWith("http:") ? company.Website : "http://" + company.Website;
                        provider.Name = company.Name;
                        provider.CompanyId = company.TenantId;
                        provider.Website = site ?? "";
                        provider.Phone = company.Phone;
                        provider.User = await commonBusiness.GetPrimaryUser(company.TenantId);
                        provider.Location = string.Format("{0}{1},{2},{3},{4}", company.Address1, string.IsNullOrEmpty(company.Address2) ? "" : "," + company.Address2,
                            company.City, company.State, company.Zip);
                        provider.City = company.City;
                        provider.State = company.State;
                        provider.Zip = company.Zip;
                        provider.Address1 = company.Address1;
                        provider.Address2 = company.Address2;
                    }
                    if (!string.IsNullOrEmpty(provider.MSADocumentId))
                    {
                        var msafile = msafiles.FirstOrDefault(x => x.MsaId == provider.MSADocumentId);
                        if (msafile != null)
                            provider.MSADocument = msafile.Attachment;
                    }
                    if (!string.IsNullOrEmpty(provider.InsuranceId))
                    {
                        var Insu = insurefiles.Where(x => x.InsId == provider.InsuranceId).FirstOrDefault();
                        if (Insu != null)
                            provider.InsuranceDocument = Insu.Attachment;
                    }
                    var activeProjects = await auctionProposalBusiness.GetServiceCompanyAuctionProjects(provider.CompanyId, tenantId, true, wellId);
                    var currentActivity = new List<CurrentActivity>();
                    foreach (var activeProject in activeProjects)
                    {
                        currentActivity.Add(new CurrentActivity
                        {
                            CurrentActivityId = activeProject.ID,
                            Title = activeProject.ProjectTitle
                        });
                    }
                    provider.CurrentActivity = currentActivity;
                    var notactiveProjects = await auctionProposalBusiness.GetServiceCompanyAuctionProjects(provider.CompanyId, tenantId, false, wellId);
                    var upcomeActivity = new List<UpcomingActivity>();
                    foreach (var notactiveProject in notactiveProjects)
                    {
                        upcomeActivity.Add(new UpcomingActivity
                        {
                            UpcomingActivityId = notactiveProject.ID,
                            Title = notactiveProject.ProjectTitle
                        });
                    }
                    provider.UpcomingActivity = upcomeActivity;
                    var offerings = await commonBusiness.GetOperatingCompanyServices(provider.CompanyId);
                    provider.ServiceOffering = offerings;
                    if (!string.IsNullOrWhiteSpace(provider.MSADocumentId))
                    {
                        var msafile = msafiles.Where(x => x.MsaId == provider.MSADocumentId).ToList();
                        provider.Msa = msafile.OrderByDescending(x => x.Expiration).ToList();
                    }
                    var Ins = insurefiles.Where(x => x.Value == provider.CompanyId).ToList();
                    provider.Insurance = Ins;
                    provider.Proposals = await auctionProposalBusiness.GetServiceCompanyActualProposals(provider.CompanyId, tenantId, wellId);
                }
                providerdir.Providers = providers;
                return providerdir;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetProviderDirectory", User.Identity.Name);
                
                return null;
            }
        }


        public async Task<IActionResult> Insurance_Read([DataSourceRequest] DataSourceRequest request, string CompanyId)
        {
            try
            {
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? WellAI.Advisor.DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var ti = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var companies = await commonBusiness.GetServiceCompanies();
                var tenantIds = companies.Select(x => x.TenantId).ToList();
                var insurefiles = await commonBusiness.GetInsuranceWellFilesFromServiceTenants(tenantIds, ti, wellId);
                var Ins = insurefiles.Where(x => x.Value == CompanyId).ToList();
                return Json(Ins);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Insurance_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Update Vendor - Phase II Changes - 01/12/2021
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<IActionResult> SaveProvider([FromBody] ProviderProfile company)
        {
            CommunicationViewModel model = new CommunicationViewModel();
            //Phase II - Clear Warning
            //string pending = "1"; string expire = "1";
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var companies = await commonBusiness.GetServiceCompanies();
                var comp = companies.FirstOrDefault(cm => cm.ID == company.CompanyId);
                var companyId = "";
                if (comp != null)
                {
                    companyId = comp.TenantId;
                }
                if (companyId != "")
                {
                    company.CompanyId = companyId;
                    var pendingReview = (from ap in _tdbContext.ProviderDirectoryAppovals
                                         where ap.Name == "Pending review"
                                         select new WellAI.Advisor.Model.OperatingCompany.Models.ProviderDirectoryApproval
                                         {
                                             Id = ap.Id
                                         }
                                    ).SingleOrDefault();
                    var inactiveStatus = (from st in _tdbContext.ProviderDirectoryStatuses
                                          where st.Name == "Inactive"
                                          select new WellAI.Advisor.Model.OperatingCompany.Models.ProviderDirectoryStatus
                                          {
                                              Id = st.Id
                                          }
                                          ).SingleOrDefault();
                    company.ApprovalId = pendingReview.Id;
                    company.StatusId = inactiveStatus.Id;
                }
                int result = 0;
                if (company != null)
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var pecstatus = "";
                    var color = "#33E0FF";
                    if (color == "#72a017")
                    {
                        pecstatus = "Good";
                    }
                    else if (color == "")
                    {
                        pecstatus = "Average";
                    }
                    else
                    {
                        pecstatus = "Bad";
                    }
                    var res = await operrepo.UpdateProviderDirectory(company, pecstatus,db);
                    result = res;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Communication SaveProvider", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        /// <summary>
        /// Phase II Changes - 01/12/2021
        /// </summary>
        /// <param name="request"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_Update([DataSourceRequest] DataSourceRequest request, ProviderProfile input)
        {
            try
            {
                if (input != null)
                {
                    
                    //var services = new ServiceCollection();
                    //services.UseServices();
                    //var serviceProvider = services.BuildServiceProvider();
                    //var service = serviceProvider.GetRequiredService<IPecService>();
                    //string OrganizationId = "282acaa1-5b38-4831-99ec-a83e0030ed9f";                    
                    var TenantId = Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId"));

                    var dbprefix = "oper";
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                   dbprefix + "db_" + TenantId.ToString("N"));

                    var ti = new TenantInfo(TenantId.ToString("N"), TenantId.ToString("N"), TenantId.ToString("N"), connString, null);
                    var operContext = new TenantOperatingDbContext(ti);
                    _tdbContext = operContext;
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var pecstatus = "";
                    var color = "#33E0FF";
                    if (color == "#72a017")
                    {
                        pecstatus = "Good";
                    }
                    else if (color == "")
                    {
                        pecstatus = "Average";
                    }
                    else
                    {
                        pecstatus = "Bad";
                    }
                    var res = await operrepo.UpdateProviderDirectoryFromAdmin(input, pecstatus, TenantId.ToString("N"));
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectory_Update", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Well AI Phase II changes - //Open or View Operating Company Pdf Document files in PdfViewer
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="fileId"></param>
        /// <param name="TenId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPdfFile(int? pageNumber, string fileId, string TenId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = db.WellFiles.FirstOrDefault(x => x.FileId == fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                var blobSection = _configuration.GetSection("AzureBlob");
                var filebyte = AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                         blobSection["ContainerPrefixName"], msadocument.TenantId, msadocument.Category, msadocument.FileName);

                JsonResult jsonResult;
                WebClient client = new WebClient();
                byte[] arr = client.DownloadData(filebyte.Result);
                FixedDocument doc = FixedDocument.Load(arr);
                if (pageNumber == null)
                {
                    jsonResult = Json(doc.ToJson());
                }
                else
                {
                    jsonResult = Json(doc.GetPage((int)pageNumber));
                }

                return jsonResult;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetPdfFile", User.Identity.Name);
                
                return null;
            }
        }

        /// <summary>
        /// Phase II Changes - 01/12/2021
        /// </summary>
        /// <param name="tenId"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Download(string tenId, string fileId)
        {
            try
            {
                var filebytes = new KeyValuePair<string, byte[]>();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = await commonBusiness.GetWellFileById(fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                var userId = _userManager.GetUserId(User);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var blobSection = _configuration.GetSection("AzureBlob");
                filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], msadocument.TenantId, path);
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = msadocument.FileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectory Download", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Phase II Changes - 01/13/2021
        /// </summary>
        /// <returns></returns>
        private async Task InitViewDataDicts(string tenantId)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var companies = await commonBusiness.GetServiceCompanies();
            var tenantIds = companies.Select(x => x.TenantId).ToList();
            ViewData["AllCompanies"] = companies;
            var compdat = new List<ProviderCompany>();
            foreach (var company in companies)
            {
                compdat.Add(new ProviderCompany
                {
                    Name = company.Name,
                    CompanyId = company.TenantId
                });
            }
            ViewData["Companies"] = compdat;

            var tId = Guid.Parse(tenantId);
            var dbprefix = "oper";
            var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                   dbprefix + "db_" + tId.ToString("N"));

            var ti = new TenantInfo(tenantId, tenantId, tenantId, connString, null);
            var operContext = new TenantOperatingDbContext(ti);
            _tdbContext = operContext;
            var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
            if (ti != null)
            {
                var approvals = _tdbContext.ProviderDirectoryAppovals.ToList();
                var approvaldata = new List<SelectListItem>();
                foreach (var approval in approvals)
                {
                    approvaldata.Add(new SelectListItem
                    {
                        Text = approval.Name,
                        Value = approval.Id
                    });
                }
                ViewData["Approvals"] = approvaldata;
                var statuses = _tdbContext.ProviderDirectoryStatuses.ToList();
                var statusdata = new List<SelectListItem>();
                foreach (var status in statuses)
                {
                    statusdata.Add(new SelectListItem
                    {
                        Text = status.Name,
                        Value = status.Id
                    });
                }
                ViewData["Statuses"] = statusdata;
                var pecs = _tdbContext.ProviderDirectoryPECs.ToList();
                var pecdata = new List<SelectListItem>();
                foreach (var pec in pecs)
                {
                    pecdata.Add(new SelectListItem
                    {
                        Text = pec.Name,
                        Value = pec.Id
                    });
                }
                ViewData["PEC"] = pecdata;
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? WellAI.Advisor.DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var msadocuments = await commonBusiness.GetMSAWellFilesFromServiceTenants(tenantIds, ti.Id, wellId);
                ViewData["msa"] = msadocuments;
            }
        }
        public IActionResult SubscribedOperators(string id)
        {
            try
            {
                ViewBag.TenantId = id;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer SubscribedOperators", User.Identity.Name);
                
                return null;
            }
        }

        public async Task<IActionResult> SubscribedOperators_Read([DataSourceRequest] DataSourceRequest request,string TenantId)
        {
            try
            {
                var Result = await GetSubscribeOperators(TenantId);

                return Json(Result.Providers.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer SubscribedOperators_Read", User.Identity.Name);
                
                return null;
            }
        }

        public async Task<WellAI.Advisor.Model.ServiceCompany.Models.OperatingDirectoryModel> GetSubscribeOperators(string TenantId,bool pendingFilter = false, bool insureExpireFilter = false)
        {
            try
            {
                var tId = Guid.Parse(TenantId);
                var dbprefix = "serv";
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + tId.ToString("N"));

                var ti = new TenantInfo(TenantId, TenantId, TenantId, connString, null);
                var SerContext = new TenantServiceDbContext(ti);
                _servicedb = SerContext;
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                //Phase II Changes - 05/21/2021 
                //var providers = await operrepo.GetSubsriptionOperators();
                var providers = await operrepo.GetSubsriptionOperatorsForAdmin(TenantId);
                var providerdir = new WellAI.Advisor.Model.ServiceCompany.Models.OperatingDirectoryModel
                {
                    InsExpiring90days = providers.Count(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0),
                    Pending = providers.Count(x => x.Approval == "Pending review"),
                    Records = providers.Count
                };
                //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                providerdir.PreferredProvider = providers.FirstOrDefault(x => x.Preferred);
                providerdir.SecondaryProvider = providers.FirstOrDefault(x => x.Secondary);
                if (pendingFilter)
                {
                    providers = providers.Where(x => x.Approval == "Pending review").ToList();
                }
                if (insureExpireFilter)
                {
                    providers = providers.Where(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0).ToList();
                }
                var msafiles = (List<WellAI.Advisor.Model.ServiceCompany.Models.ServiceMSA>)ViewData["msa"];
                var companies = (List<CorporateProfile>)ViewData["AllCompanies"];
                List<string> tenantIds = null;
                if (companies == null)
                {
                    companies = await commonBusiness.GetOperatingCompanies();
                    tenantIds = companies.Select(x => x.TenantId).ToList();
                    msafiles = await commonBusiness.GetMSAWellFilesFromOperatingTenants(tenantIds, tenantId);
                }
                else
                    tenantIds = companies.Select(x => x.TenantId).ToList();
                IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                foreach (var provider in providers)
                {
                    var cmpprofile = db.CorporateProfile.Where(x => x.TenantId.Equals(provider.CompanyId)).FirstOrDefault();
                    var company = companies.FirstOrDefault(x => x.TenantId.ToString() == provider.CompanyId);//provider.CompanyId=tenantId of Service Company
                    if (cmpprofile != null)
                    {
                        var site = string.IsNullOrEmpty(cmpprofile.Website) || cmpprofile.Website.StartsWith("http:") ? cmpprofile.Website : "http://" + cmpprofile.Website;
                        provider.Name = cmpprofile.Name;
                        provider.CompanyId = cmpprofile.TenantId;
                        provider.Website = site;
                        provider.Phone = cmpprofile.Phone;
                        provider.User = await commonBusiness.GetPrimaryUserSRV(cmpprofile.TenantId);
                        provider.Location = string.Format("{0}{1},{2},{3},{4}", cmpprofile.Address1, string.IsNullOrEmpty(cmpprofile.Address2) ? "" : "," + cmpprofile.Address2,
                        cmpprofile.City, cmpprofile.State, cmpprofile.Zip);
                        provider.City = cmpprofile.City;
                        provider.State = db.USAStates.Where(x => x.StateId == Convert.ToInt32(cmpprofile.State)).Select(y => y.Name).FirstOrDefault();
                        provider.Zip = cmpprofile.Zip;
                        provider.Address1 = cmpprofile.Address1;
                        provider.Address2 = cmpprofile.Address2;
                    }
             
                    msafiles = await commonBusiness.GetMSAWellFilesFromOperatingTenants(new List<string> { provider.CompanyId }, ti.Id);
                    provider.MSADocument = "";
                    var msa = msafiles.FirstOrDefault(x => x.MsaId == provider.MSADocumentId);
                    if (msa != null)
                    {
                        provider.MSADocumentId = msa.MsaId;
                        provider.MSADocument = msa.Attachment;
                    }

                    //Phase II Changes - 02/25/21 - Commented Details Retrieve on Master list load 
                    var insurefiles = await commonBusiness.GetInsuranceFilesFromServiceTenants(tenantIds, ti.Id, provider.CompanyId);

                    if (!string.IsNullOrEmpty(provider.InsuranceId))
                    {
                        var Ins = insurefiles.Where(x => x.InsId == provider.InsuranceId).FirstOrDefault();
                        if (Ins != null)
                        {
                            provider.InsuranceDocument = Ins.Attachment;
                            provider.InsuranceId = Ins.InsId;
                        }
                    }
                    else
                    {
                        provider.InsuranceDocument = "";
                        provider.InsuranceId = "";
                    }
                    provider.ServiceTenantId = provider.ServiceTenantId;
                    
                }
                providerdir.Providers = providers;
                return providerdir;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetSubscribeOperators", User.Identity.Name);
                
                return null;
            }
        }


        [AcceptVerbs("Post")]
        public async Task<IActionResult> SubscribedOperator_Destroy([DataSourceRequest] DataSourceRequest request, string CompanyId, string TenantId)
        {
            try
            {
                var RigDepthPermission = db.RigsDepth_Permissions.Where(x => x.OprTenantId == CompanyId && x.SerTenantId == TenantId && x.DepthPermission == true).ToList();
                if (RigDepthPermission.Count > 0)
                {
                    var rig = new List<string>();
                    foreach (var dep in RigDepthPermission)
                    {
                        var rigname = db.rig_register.Where(x => x.Rig_id == dep.RigId).Select(y => y.Rig_Name);
                        rig.AddRange(rigname);
                    }

                    return Json(new { IsDepthPermission = true, RigName = string.Join(",", rig) });
                    //Json(new[] { request }.ToDataSourceResult(request, ModelState));
                }

                if (!string.IsNullOrEmpty(CompanyId))
                {
                    var companyId = CompanyId;
                    var tId = Guid.Parse(TenantId);
                    var dbprefix = "serv";
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + tId.ToString("N"));

                    var ti = new TenantInfo(TenantId, TenantId, TenantId, connString, null);
                    var SerContext = new TenantServiceDbContext(ti);
                    _servicedb = SerContext;
                    var SerRepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await SerRepo.DeleteSubsriptionOperators(companyId);
                    var res2 = await SerRepo.DeleteProviderDirectoryByCompanyId(companyId);
                    var Result = await SerRepo.DeleteSubscribeRigs(companyId);
                    return Json(new[] { request }.ToDataSourceResult(request, ModelState));
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer SubscribedOperator_Destroy", User.Identity.Name);
                
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }


        /// <summary>
        ///  Phase II Changes - 01/12/2021
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProviderProfile(string id)
        {
            try
            {           
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var dir = await GetAllProviderDirectory(false, false);
                var profile = dir.Providers.FirstOrDefault(x => x.ProviderId == id);
                TempData["CompanyId"] = profile.CompanyId;
                return View("CustomerVendorProfile", profile);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectory ProviderProfile", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        /// <summary>
        /// Phase II Changes - 01/21/2021
        /// </summary>
        /// <param name="tenId"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownloadForServiceCompany(string tenId, string fileId)
        {
            try
            {
                var filebytes = new KeyValuePair<string, byte[]>();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var msadocument = await commonBusiness.GetWellFileById(fileId);
                var path = msadocument.Category + "/" + msadocument.FileName;
                string tenantId = "";// WellAIAppContext.Current.Session.GetString("TenantId");
                if (msadocument != null)
                {
                    tenantId = msadocument.TenantId;
                }
                else
                {
                    tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                }

                var blobSection = _configuration.GetSection("AzureBlob");

                filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], tenantId, path);
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = msadocument.FileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, " Admin Vendor Profile Download", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Phase II Changes - 01/21/2021
        /// </summary>
        /// <param name="tenId"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownloadProposal(string tenId, string fileId)
        {
            try
            {
                var filebytes = new KeyValuePair<string, byte[]>();
                var auctionBusiness = new AuctionProposalBusiness(db, _userManager);
                var file = await auctionBusiness.GetServiceCompanyAttachment(tenId, fileId);
                var path = file.FilePatch + "/" + file.FileName;
                var blobSection = _configuration.GetSection("AzureBlob");
                filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                     blobSection["ContainerPrefixName"], tenId, path);
                if (string.IsNullOrEmpty(filebytes.Key) || filebytes.Value == null || filebytes.Value.Length == 0)
                    return RedirectToAction("Index");
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = file.FileName,
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
                return File(filebytes.Value, filebytes.Key);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, " Admin Vendor Profile DownloadProposal", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Phase II Changes - 01/21/2021
        /// </summary>
        /// <param name="tenId"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<FileResult> InsuranceDownload(string tenId, string fileId)
        {
            var filebytes = new KeyValuePair<string, byte[]>();
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var msadocument = await commonBusiness.GetWellFileById(fileId);
            string path = msadocument.Category + "/" + msadocument.FileName;
            try
            {
               
                    var blobSection = _configuration.GetSection("AzureBlob");
                    filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], tenId, path);
                var filebyte = filebytes.Value == null ? 0 : filebytes.Value.Length;
                if (filebytes.Key == "" || filebyte == 0)
                    return null;
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, " Admin Vendor Profile DownloadProposal", User.Identity.Name);
                            }
            return File(filebytes.Value, filebytes.Key);
        }

        /// <summary>
        /// Phase II Changes - 01/19/2021 - Approve MSA Documents
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ApproveServiceMSA(string fileId, string approvalStatus)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileId))
                {

                    var file = db.ProviderMSALinks.Where(x => x.FileId == fileId).FirstOrDefault();
                    var dbprefix = "serv";
                    var servguid = new Guid(file.ServiceTenantId);
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + servguid.ToString("N"));
                    var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                    var servDBContext = new TenantServiceDbContext(ti);
                    _servicedb = servDBContext;
                    var servdb = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);

                    var OperatingUserIds = (from cp in db.CorporateProfile
                                            join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                            where cub.AccountType == 0 && cub.IsMaster == true && cp.TenantId == file.OperationTenantId
                                            select new CrmUserBasicDetailModel { Id = cub.Id, UserId = cp.UserId, Name = cub.Name }).FirstOrDefault();

                    string operatingUserId = "";
                    if (OperatingUserIds != null)
                    {
                        operatingUserId = OperatingUserIds.UserId;
                    }

                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    var res = await commonBusiness.UpdateMSAApprovalStatus(fileId, approvalStatus == "true" ? true : false, operatingUserId,servdb);


                    //Phase II changes - 03/01/2021
                    //Welcome, Authorized and Preferred Vendor status update (1-Welcome,2-Authorized,3-Preferred)
                    var servTenantID = servguid.ToString("D");
                    var operTenantId = new Guid(file.OperationTenantId).ToString("D");
                    int statusResult = commonBusiness.UpdateVendorPreferredStatus(operTenantId, servTenantID);


                    if (statusResult > 0)
                    {
                        var operDbPrefix = "oper";
                        var operConnString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       operDbPrefix + "db_" + new Guid(file.OperationTenantId).ToString("N"));

                        ti = new TenantInfo(new Guid(file.OperationTenantId).ToString("N"), new Guid(file.OperationTenantId).ToString("N"), new Guid(file.OperationTenantId).ToString("N"), operConnString, null);
                        var operContext = new TenantOperatingDbContext(ti);
                        _tdbContext = operContext;

                        var provider = _tdbContext.ProvidersDirectory.Where(x => x.CompanyId == servTenantID).FirstOrDefault();
                        provider.Preferred = Convert.ToByte(statusResult);
                        _tdbContext.SaveChanges();

                        //Phase II changes - 02/09/2021 - Remove an MSA Id at Provider Directory if the MSA is unapproved
                        if (approvalStatus == "false")
                        {
                            if (provider != null)
                            {
                                provider.MSA = null;
                                _tdbContext.SaveChanges();
                            }
                        }
                        else if (approvalStatus == "true")
                        {
                            //MSA Permission
                            if (provider != null)
                            {
                                provider.MSA = fileId;
                                _tdbContext.SaveChanges();
                            }
                        }

                        //Phase II changes - 05/21/2021 - Update MSA at Service
                        if (approvalStatus == "false")
                        {
                            var servprovider = servDBContext.OperatingDirectory.Where(x => x.TenantId == servTenantID && x.CompanyId == operTenantId).FirstOrDefault();
                            if (servprovider != null)
                            {
                                servprovider.MSA = null;
                                servDBContext.SaveChanges();
                            }
                        }
                        else if (approvalStatus == "true")
                        {
                            //MSA Permission
                            var servprovider = servDBContext.OperatingDirectory.Where(x => x.TenantId == servTenantID && x.CompanyId == operTenantId).FirstOrDefault();
                            if (servprovider != null)
                            {
                                servprovider.MSA = fileId;
                                servDBContext.SaveChanges();
                            }
                        }
                    }

                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, " Admin ApproveServiceMSA", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        /// <summary>
        /// Phase II Changes - 01/21/2021 - UpdateRating
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        [AcceptVerbs("Post")]
        public async Task<IActionResult> UpdateRating(string provider, int rate)
        {
            try
            {
                if (!string.IsNullOrEmpty(provider))
                {
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var res = await operrepo.UpdateRatingProviderDirectory(provider, rate);
                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Admin UpdateRating", User.Identity.Name);

                

                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        /// <summary>
        /// Phase II Changes - 01/22/2021
        /// </summary>
        /// <param name="pendingFilter"></param>
        /// <param name="insureExpireFilter"></param>
        /// <returns></returns>
        private async Task<ProviderDirectoryModel> GetAllProviderDirectory(bool pendingFilter = false, bool insureExpireFilter = false)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var TenantId = Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId"));
            var dbprefix = "oper";
            var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                           dbprefix + "db_" + TenantId.ToString("N"));

            var ti = new TenantInfo(TenantId.ToString("N"), TenantId.ToString("N"), TenantId.ToString("N"), connString, null);
            var operContext = new TenantOperatingDbContext(ti);
            _tdbContext = operContext;
            var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);

            var providers = await operrepo.GetProviderDirectories(true);
            var pecs = (from Provider in _tdbContext.ProvidersDirectory
                        join pec in _tdbContext.ProviderDirectoryPECs
                        on Provider.PEC equals pec.Id
                        where pec.Name != "Good"
                        select Provider).Count();
            var providerdir = new ProviderDirectoryModel
            {
                InsExpiring90days = providers.Count(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0),
                Pending = providers.Count(x => x.Approval == "Pending review"),
                Records = providers.Count,
                ComplienceAlert = providers.Count(p => p.PecStatus != "Good")
            };

            //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
            providerdir.PreferredProvider = providers.FirstOrDefault(x => x.Preferred==Convert.ToByte(3));
            providerdir.SecondaryProvider = providers.FirstOrDefault(x => x.Secondary);
            if (pendingFilter == true && insureExpireFilter == false)
            {
                providers = providers.Where(x => x.Approval == "Pending review").ToList();
            }
            if (insureExpireFilter == true && pendingFilter == false)
            {
                providers = providers.Where(x => x.InsuranceExpire.HasValue && x.InsuranceExpire.Value.AddDays(-90).CompareTo(DateTime.Now) <= 0).ToList();
            }
            if (insureExpireFilter == true && pendingFilter == true)
            {
                providers = providers.Where(x => x.PecStatus != "Good").ToList();
            }
            List<string> tenantIds = null;
            var wellIdCookie = Request.Cookies["wellfilterlayout"];
            var wellId = string.IsNullOrEmpty(wellIdCookie) ? WellAI.Advisor.DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
            var companies = new List<CorporateProfile>();
            var msafiles = new List<MSA>();
            if (companies == null || companies.Count == 0)
            {
                companies = await commonBusiness.GetServiceCompanies();
                var operatingcompanies = await commonBusiness.GetOperatingCompanies();

                var servtenantIds = (from cp in db.CorporateProfile
                                     join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                     where cub.AccountType == 1
                                     select new CorporateProfile { TenantId = cp.TenantId }).ToList();          

                tenantIds = servtenantIds.Select(x => x.TenantId).ToList();

                msafiles = await commonBusiness.GetMSAWellFilesFromServiceTenants(tenantIds, TenantId.ToString(), wellId);
            }
            else
                tenantIds = companies.Select(x => x.TenantId).ToList();

            var insurefiles = await commonBusiness.GetInsuranceWellFilesFromServiceTenants(tenantIds, TenantId.ToString(), wellId);
            IAuctionProposalBusiness auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
            foreach (var provider in providers)
            {
                provider.MSADocument = "";
                var company = companies.FirstOrDefault(x => x.TenantId.ToString() == provider.CompanyId);//provider.CompanyId=tenantId of Service Company
                if (company != null)
                {
                    var site = string.IsNullOrEmpty(company.Website) || company.Website.StartsWith("http:") ? company.Website : "http://" + company.Website;
                    provider.Name = company.Name;
                    provider.CompanyId = company.TenantId;
                    provider.Website = site ?? "";
                    provider.Phone = company.Phone;
                    provider.User = await commonBusiness.GetPrimaryUser(company.TenantId);
                    provider.Location = string.Format("{0}{1},{2},{3},{4}", company.Address1, string.IsNullOrEmpty(company.Address2) ? "" : "," + company.Address2,
                        company.City, company.State, company.Zip);
                    provider.City = company.City;
                    provider.State = company.State;
                    provider.Zip = company.Zip;
                    provider.Address1 = company.Address1;
                    provider.Address2 = company.Address2;
                }
                if (!string.IsNullOrEmpty(provider.MSADocumentId))
                {
                    var msafile = msafiles.FirstOrDefault(x => x.MsaId == provider.MSADocumentId);
                    if (msafile != null)
                        provider.MSADocument = msafile.Attachment;
                }
                var activeProjects = await auctionProposalBusiness.GetServiceCompanyAuctionProjects(provider.CompanyId, TenantId.ToString(), true, wellId);
                var currentActivity = new List<CurrentActivity>();
                foreach (var activeProject in activeProjects)
                {
                    currentActivity.Add(new CurrentActivity
                    {
                        CurrentActivityId = activeProject.ID,
                        Title = activeProject.ProjectTitle
                    });
                }
                provider.CurrentActivity = currentActivity;
                var notactiveProjects = await auctionProposalBusiness.GetServiceCompanyAuctionProjects(provider.CompanyId, TenantId.ToString(), false, wellId);
                var upcomeActivity = new List<UpcomingActivity>();
                foreach (var notactiveProject in notactiveProjects)
                {
                    upcomeActivity.Add(new UpcomingActivity
                    {
                        UpcomingActivityId = notactiveProject.ID,
                        Title = notactiveProject.ProjectTitle
                    });
                }
                provider.UpcomingActivity = upcomeActivity;
                var offerings = await commonBusiness.GetOperatingCompanyServices(provider.CompanyId);
                provider.ServiceOffering = offerings;
                
                if (!string.IsNullOrWhiteSpace(provider.CompanyId))
                {
                    var msafile = msafiles.Where(x => x.CompanyId == provider.CompanyId).ToList();
                    provider.Msa = msafile;
                }

                var Ins = insurefiles.Where(x => x.Value == provider.CompanyId).ToList();
                provider.Insurance = Ins;
                provider.Proposals = await auctionProposalBusiness.GetServiceCompanyActualProposals(provider.CompanyId, TenantId.ToString(), wellId);
            }
            providerdir.Providers = providers;
            return providerdir;
        }

        public async Task<IActionResult> Update_SubscibeOPerator_Rigs([FromBody] List<WellAI.Advisor.Model.ServiceCompany.Models.SubscriptionOperatorRigs> SelectedRigs)
        {

            try
            {
                int result = 0;
                var TenantId = SelectedRigs[0].ID;
                var tId = Guid.Parse(TenantId);
                var dbprefix = "serv";
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + tId.ToString("N"));

                var ti = new TenantInfo(TenantId, TenantId, TenantId, connString, null);
                var SerContext = new TenantServiceDbContext(ti);
                _servicedb = SerContext;
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var OperatorsRigsCount = (from sub in db.Subscription
                                          join pcg in db.SubscriptionPackage on sub.PackageId equals pcg.PackageId.ToString()
                                          where sub.TenantId == TenantId && sub.IsPaid == true
                                          select new CustomerSubscriptions
                                          {
                                              SubscriptionUsersCount = sub.SubscriptionCount,
                                          }).FirstOrDefault();
                var Rigs = new List<string>();
                for (var i = 0; i < SelectedRigs.Count; i++)
                {
                    var RigList = SelectedRigs[i].RigId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    Rigs.AddRange(RigList);
                }

              
                if (Rigs.Count > OperatorsRigsCount.SubscriptionUsersCount)
                {
                    ModelState.AddModelError(string.Empty, "Your subscription does not allow adding more operator rigs. Upgrade your subscription if you need more rigs.");
                    return Json(new { success = true });
                }

                if (ModelState.IsValid)
                {
                    operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);

                    var CompanyId = SelectedRigs[0].CompanyId;
                    //var RigId = SelectedRigs.RigId;
                    var getRigs = await operrepo.Remove_SubsciberProviderRigs(CompanyId);

                    result = await operrepo.SaveSubsciberProviderRigs(SelectedRigs);

                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Update_SubscibeOPerator_Rigs", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);

            }

        }


        public IActionResult Read_SubscribeOPerator_Rigs(string CompanyId,string TenantId)
        {
            try
            {
                var tId = Guid.Parse(TenantId);
                var dbprefix = "serv";
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + tId.ToString("N"));

                var ti = new TenantInfo(TenantId, TenantId, TenantId, connString, null);
                var SerContext = new TenantServiceDbContext(ti);
                _servicedb = SerContext;
                var operrepo = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                var result = operrepo.Get_SubsciberProviderRigs(CompanyId);
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV Operators_Read", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }



        public IActionResult ReadSubscibeOPerator_Rigs([DataSourceRequest] DataSourceRequest request, string TenantId)
        {
            try
            {
                List<RigViewModel> RigList = new List<RigViewModel>();

                RigList = (from rig in db.rig_register
                           where rig.TenantID == TenantId && rig.isActive == true
                           select new RigViewModel
                           {
                               RigName = rig.Rig_Name,
                               RigId = rig.Rig_id
                           }).OrderBy(x => x.RigName).ToList();

                return Json(RigList.ToDataSourceResult(request));

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProductSubscriptionSRV ReadRigs", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }

        }

        public IActionResult GetRigs([DataSourceRequest] DataSourceRequest request, string CompanyId)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                var dbprefix = "serv";
                var servguid = new Guid(CompanyId);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var OperDBContext = new TenantServiceDbContext(ti);
                _servicedb = OperDBContext;
                var SubscribedRigs = _servicedb.subscriptionOperatorRigs.Where(x => x.CompanyId == tenantId).ToList();
                var Result = (from subrig in SubscribedRigs
                              join rig in db.rig_register on subrig.RigId equals rig.Rig_id
                              join well in db.WellRegister on subrig.RigId equals well.RigID
                              join depPermission in db.RigsDepth_Permissions on well.well_id equals depPermission.WellId into dp
                              from depPermission in dp.DefaultIfEmpty()
                              where subrig.CompanyId == tenantId /*&& rig.isActive == true*/
                              select new RigsDepthPermission_Model
                              {
                                  RigName = rig.Rig_Name,
                                  WellName = well.wellname,
                                  DepthPermission = depPermission == null ? false : depPermission.DepthPermission,
                                  RigId = rig.Rig_id,
                                  WellId = well.well_id,
                                  WellPrediction = well.Prediction
                              }).ToList();

                return Json(Result.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer GetRigs", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]

        public async Task<IActionResult> SaveRigsDepth_Permission(string Rigid, string Wellid, string Sertenant, bool DepthPermission)
        {
            try
            {
                RigsDepth_Permission DepthValue = new RigsDepth_Permission();
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var dbprefix = "oper";
                var servguid = new Guid(TenantId);
                var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                       dbprefix + "db_" + servguid.ToString("N"));
                var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                var OperDBContext = new TenantOperatingDbContext(ti);
                _tdbContext = OperDBContext;
                var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                if (ModelState.IsValid)
                {
                    DepthValue.ID = Guid.NewGuid().ToString();
                    DepthValue.RigId = Rigid;
                    DepthValue.WellId = Wellid;
                    DepthValue.DepthPermission = DepthPermission;
                    DepthValue.OprTenantId = TenantId;
                    DepthValue.SerTenantId = Sertenant;
                }
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                var Result = await commonBusiness.CreateDepthPermission(DepthValue);

                //Phase II changes - 03/01/2021
                //Welcome, Authorized and Preferred Vendor status update (1-Welcome,2-Authorized,3-Preferred)
              
                int statusResult = commonBusiness.UpdateVendorPreferredStatus(TenantId, Sertenant);

                if (statusResult > 0)
                {
                    var provider = _tdbContext.ProvidersDirectory.Where(x => x.CompanyId == Sertenant).FirstOrDefault();
                    provider.Preferred = Convert.ToByte(statusResult);
                    _tdbContext.SaveChanges();
                }

                return Ok(Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectory SaveRigsDepth_Permission", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> ProviderDirectory_Destroy(string companyId)
        {
            try
            {
                if (!string.IsNullOrEmpty(companyId))
                {
                    var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    var dbprefix = "oper";
                    var servguid = new Guid(TenantId);
                    var connString = _configuration.GetConnectionString("WellAITenantTemplateConnection").Replace("{%dbname%}", "wellai_" +
                                           dbprefix + "db_" + servguid.ToString("N"));
                    var ti = new TenantInfo(servguid.ToString("D"), servguid.ToString("D"), servguid.ToString("D"), connString, null);
                    var OperDBContext = new TenantOperatingDbContext(ti);
                    _tdbContext = OperDBContext;
                    var operrepo = new OperatingTenantRepository(_tdbContext, HttpContext, _userManager);
                    var res = await operrepo.DeleteProviderDirectory(companyId);
                    var MessageQueue = db.MessageQueues.Where(x => x.EntityId == companyId).ToList();
                    foreach (var message in MessageQueue)
                    {
                        db.MessageQueues.Remove(message);
                        await db.SaveChangesAsync();
                    }
                    return Json(new[] { res });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectory_Destroy", User.Identity.Name);
                
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpGet]
        public async Task<FileResult> InsurenceDownload(string tenId, string fileId)
        {
            var filebytes = new KeyValuePair<string, byte[]>();
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var msadocument = await commonBusiness.GetWellFileById(fileId);
            //var TenantId = WellAIAppContext.Current.Session.GetString("TenantId") == null ? tenId : WellAIAppContext.Current.Session.GetString("TenantId");

            string path = msadocument.Category + "/" + msadocument.FileName;
            try
            {
                if (msadocument != null)
                {
                    var blobSection = _configuration.GetSection("AzureBlob");
                    filebytes = await AzureBlobStorage.DownloadFilesFromBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], msadocument.TenantId, path);
                }
                var filebyte = filebytes.Value == null ? 0 : filebytes.Value.Length;
                if (filebytes.Key == "" || filebyte == 0)
                    return null;
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = path.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries)[1],
                    Inline = false
                };
                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ProviderDirectory SaveRigsDepth_Permission", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return null;
            }
            return File(filebytes.Value, filebytes.Key);
        }


        public async Task<JsonResult> GetWellDetailsByApiNumberAsync(string text)
        {
            List<Result> WellData = new List<Result>();

            try
            {
                WellApiData ResResult = new WellApiData();
                var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
                Result data = new Result();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    var Url = GetUrl + "search_wells/api_number/";
                    response = await client.GetAsync(Url + text).ConfigureAwait(true);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        ResResult = JsonConvert.DeserializeObject<WellApiData>(result);
                    }
                }

                if (ResResult.results != null )
                {
                    //WellApiData ResResult = new WellApiData();
                    //ResResult = JsonConvert.DeserializeObject<WellApiData>(JsonResult);
                    var wellResult = ResResult.results;

                    if (wellResult.Count > 0)
                    {
                        wellResult = wellResult.Where(ap => ap.api_number.Contains(text)).ToList();
                    }

                    return Json(wellResult);
                }

                else if (ResResult.message != null && text != null)
                {
                    TempData["Error"] = ResResult.message;
                    return Json(WellData);
                }
                else
                {
                    return Json(WellData);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "GetWellDetailsByApiNumberAsync", User.Identity.Name);
                
                return Json(WellData);
            }
        }

        //DWOP
        /// <summary>
        /// Get Template List for a Well Design and Operator Tenant
        /// </summary>
        /// <param name="wellDesign"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetChecklistTemplateForDesignAsync(string wellDesign)
        {
            List<ChecklistTemplate> templates = new List<ChecklistTemplate>();
            try
            {
                if (wellDesign !=null )
                {
                    var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    //var TenantId = ViewBag.TenantId;
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    List<WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel> templateList = new List<WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel>();
                    templateList = await commonBusiness.GetChecklistTemplateList(wellDesign,TenantId);
                    if (templateList !=null )
                    {
                        templates = (from t in templateList
                                    select new ChecklistTemplate
                                    {
                                         ChecklistTemplateId = t.TemplateId,
                                         ChecklistTemplateName = t.TemplateName
                                    }).ToList();
                    }
                    
                        return Json(templates);
                    
                }
                else
                {
                    return Json(templates);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetChecklistTemplateForDesignAsync", User.Identity.Name);
               
                return Json(templates);
            }
        }

        public IActionResult AddPermission(string PermisionName)
        {
            try
            {
                if (!string.IsNullOrEmpty(PermisionName))
                {
                    var RolePermissions = db.RolePermissions.Where(x => x.RolePermissionName == PermisionName).FirstOrDefault();
                    if (RolePermissions != null)
                    {
                        UserAction UserAction = new UserAction
                        {
                            Id = RolePermissions.RolePermissionId,
                            Title = RolePermissions.RolePermissionName,
                            IsActive = true
                        };

                        return PartialView("_AddEditPermission", UserAction);
                    }
                    else
                    {
                        return PartialView("_AddEditPermission");
                    }
                }
                else
                {
                    var TenantId = HttpContext.Session.GetString("AdminSessionCurrentTenantId");
                    //ViewBag.wellId = wellId;
                    //ViewBag.DrillPlanId = DrillPlanId;
                    return PartialView("_AddEditPermission");
                }
                
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CustomerController AddPermission", User.Identity.Name);
                //return LocalRedirect(returnUrl);
                return null;
            }
        }
        //var TenantId = HttpContext.Session.GetString("AdminSessionCurrentTenantId");

        public async Task<bool> SavePermission([FromBody] UserPermission permission)
        {
            try
            {  
                var tenantId = WellAIAppContext.Current.Session.GetString("UserTenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var components = db.Components.Where(x => x.IsActive == true && x.AccountType == 1).ToList();                   

                var userIdentityName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                bool result = false;
                var permcomps = new List<RolePermissionComponentModel>();
                if (db.RolePermissions.Where(x => x.RolePermissionId == permission.Id).FirstOrDefault() == null)
                {
                    foreach (var newItem in components)
                    {
                        var permcomp = new RolePermissionComponentModel
                        {
                            ComponentId = newItem.ComponentId,
                            ComponentName = newItem.Label,
                            IsPermitted = false
                        };
                        permcomps.Add(permcomp);
                    }
                    result = await _singleton.customerProfileBusiess.CreatePermissionComponents(permission.Title, permcomps, userIdentityName, tenantId);
                }
                else
                {
                    var role = db.RolePermissions.FirstOrDefault(x => x.RolePermissionId == permission.Id);
                    if (role.RolePermissionName != permission.Title)
                    {
                        
                            role.RolePermissionName = permission.Title;
                    }
                    db.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer SavePermission", User.Identity.Name);

                return false;
            }

        }

        public IActionResult GetDispatch(string id)
        {
            try
            {
                ViewBag.TenantId = id;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer Dispatch", User.Identity.Name);

                return null;
            }
        }
        public IActionResult DispatchService(string id)
        {
            try
            {
                ViewBag.TenantId = id;
                  return PartialView("DispatchService");
              //  return View("DispatchService");
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer Dispatch", User.Identity.Name);

                return null;
            }
        }


        [HttpPost]

        public async Task<IActionResult> Update(CorporateProfileAdmin input)
        {
            try
            {
                if (ModelState.IsValid || input.ID == null)
                {
                    var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                    var welluser = await _userManager.GetUserAsync(User);
                    var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                    //if (input.logofiles != null)
                    //{
                    //    string result = Task.Run(async () => await SaveFile(input.logofiles)).Result;
                    //    input.LogoPath = result;
                    //}
                    //else
                    //{
                    //    input.LogoPath = null;
                    //}
                    var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                    var categories = await auctionProposalBusiness.GetServiceCategorys();
                    //if (input.ServiceCategories != null)
                    //{
                    //    List<Model.ServiceCompany.Models.CompanyServicesModel> cServices = new List<Model.ServiceCompany.Models.CompanyServicesModel>();
                    //    string[] serviceCategoryList = input.ServiceCategories.Split(";");
                    //    if (serviceCategoryList.Length > 0)
                    //    {
                    //        var serviceResults = categories.Where(x => serviceCategoryList.Contains(x.ServiceCategoryId));
                    //        cServices = (from cs in serviceResults
                    //                     select new Model.ServiceCompany.Models.CompanyServicesModel
                    //                     {
                    //                         ServiceName = cs.Name
                    //                     }
                    //                          ).ToList();

                    //        if (serviceCategoryList.Length > 0)
                    //        {
                    //            input.CServices = JsonConvert.SerializeObject(cServices);
                    //        }
                    //    }

                    //}

                    var res = await commonbus.UpdateCustomerCorporateProfile(input, input.ID, tenantId);
                    //var res2 = commonbus.UpdateCompanyCategories(input.ServiceCategories, tenantId);
                }
                string returnUrl = @"/Customer/Operator";
                return LocalRedirect(returnUrl);
                // return RedirectToAction("Index",);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer UpdateProfileDetails", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            object result = null;
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var folderName = _configuration.GetSection("FolderName");
                    var blobSection = _configuration.GetSection("AzureBlob");
                    result = await AzureBlobStorage.UploadFileToBlobContainerWithFileName(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, file, folderName["CompanyProfile"], ti.Id);
                    string AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    System.Type type = result.GetType();
                    Uri docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                    return Path.GetFileName(docUri.OriginalString);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CommunicationSRV SaveFile", User.Identity.Name);
            }
            return "";
        }

    }
}