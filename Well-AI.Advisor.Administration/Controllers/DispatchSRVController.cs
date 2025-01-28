using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WellAI.Advisor;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Well_AI.Advisor.Log.Error;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.DLL.IRepository;
using Finbuckle.MultiTenant;
using System.Web.Mvc;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Administration;

namespace Well_AI.Advisor.Administration.Controllers
{
    public class DispatchSRVController : BaseController
    {
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
       
        private TenantOperatingDbContext _tdbContext;
        private readonly TenantOperatingDbContext _operdb;
        private ISession _session;
        private TenantServiceDbContext _servicedb;
        public DispatchSRVController(IConfiguration _configuration, UserManager<WellIdentityUser> userManager,
          RoleManager<IdentityRole> roleManager,
           ISingletonAdministration _singleton, WebAIAdvisorContext db, TenantOperatingDbContext operdb, TenantOperatingDbContext tdbContext,
           TenantServiceDbContext servicedb, UserManager<StaffWellIdentityUser> staffUserManager) : base(_configuration, _singleton, db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _operdb = operdb;
            _tdbContext = tdbContext;
            _session = WellAIAppContext.Current.Session;
            _servicedb = servicedb;
            _staffUserManager = staffUserManager;

        }

        public IActionResult Index(string id)
        {
            try
            {
               // WellAIAppContext.Current.Session.SetString("TenantId", id);
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                //  if (_signInManager.IsSignedIn(User) == false)
                //{
                //    string returnUrl = @"/Identity/Account/Login";
                //    return LocalRedirect(returnUrl);
                //}

                if (WellAIAppContext.Current.Session.GetString("ShareRoute") == "1")
                {
                    ViewData["ShareRoute"] = true;
                    ViewBag.ShareRoute = true;
                }
                else
                {
                    ViewData["ShareRoute"] = false;
                    ViewBag.ShareRoute = false;
                }

               // PartialView("Index");

                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch Index", User.Identity.Name);
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
        }

        public IActionResult Indexmain(string id)
        {
            try
            {
                WellAIAppContext.Current.Session.SetString("TenantId", id);
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ViewBag.TenantId = id;
                return PartialView("Indexmain");
                //  return View("DispatchService");
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "Customer Dispatch", User.Identity.Name);

                return null;
            }
        }


        public IActionResult DispatchSRV1()
        {
            try
            {
             //   if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch Index", User.Identity.Name);
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
        }

        public IActionResult LoadAdvisorUsers()
        {

            return PartialView("_AdvisorUsers");
        }


        public IActionResult LoadAssignDispatch(string userId)
        {

            ViewBag.UserId = userId;
            ViewBag.AssignDispatch = new List<SelectListItem>
            {
                new SelectListItem(){ Value = "1", Text = "Steven White" },
                new SelectListItem(){ Value = "2", Text = "Nancy King" },
                new SelectListItem(){ Value = "3", Text = "Nancy Davolio" },
                new SelectListItem(){ Value = "4", Text = "Michael Leverling" },
                new SelectListItem(){ Value = "5", Text = "Andrew Callahan" },
                new SelectListItem(){ Value = "6", Text = "Michael Suyama" },
            };
            //return PartialView("_AssignDispatch");
            return PartialView("_AssignDispatchWithPreview");
            //  return View("_AssignDispatchWithPreview");
        }



        public IActionResult LoadUserRoutes(string userId)
        {
            ViewBag.UserId = userId;



            return PartialView("_UserRoutes");
        }

        public IActionResult LoadDispatchNotes()
        {

            return PartialView("_DispatchNotes");
        }
        public async Task<IActionResult> GetDispatchList_Preview([DataSourceRequest] DataSourceRequest request, string userId)
        {
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            try
            {
                //  userId = "d1858e6c-5784-488e-8fdf-0b3f7e141e23";
                CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                result = objComBusiness.GetDispatchRoutes_Preview(userId, false).Result;

            }
            catch (System.Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchList", User.Identity.Name);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        public async Task<IActionResult> GetDispatchList([DataSourceRequest] DataSourceRequest request, string userId)
        {
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            try
            {
                CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                result = objComBusiness.GetDispatchRoutes(userId, false).Result;

            }
            catch (System.Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchList", User.Identity.Name);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }

        public async Task<IActionResult> GetDispatchList_V2([DataSourceRequest] DataSourceRequest request, string userId)
        {
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            try
            {
              
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                if (userId == null)
                {
                    userId = tenantId;
                }
                CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                result = objComBusiness.GetDispatchRoutes_V2(userId, false, tenantId).Result;

            }
            catch (System.Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchList", User.Identity.Name);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }

        public async Task<bool> AddDispatchUser(string userId)
        {
            bool userStatus = false;
            try
            {
                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var user = await _userManager.FindByIdAsync(userId);

                UsersOptions usersoptions = new UsersOptions();
                usersoptions.IsDispatchUser = true;
                usersoptions.DispatchNotes = "";
                var usersoptionsSerialize = JsonConvert.SerializeObject(usersoptions);
                user.UsersOptions = usersoptionsSerialize;
                var result = await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch AddDispatchUser", User.Identity.Name);
            }
            return userStatus;
        }

        public IActionResult AdvisorUsers_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                //var result = _userManager.Users.Where(x => x.TenantId == HttpContext.GetMultiTenantContext().TenantInfo.Id && (x.IsDispatchUser != true)).ToList();
                //ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();
                //List<WellIdentityUser> usersList = commonBusiness.GetComponentPermissionUsers("RouterUser", HttpContext.GetMultiTenantContext().TenantInfo.Id);

                ////var result = _userManager.Users.Where(x => x.TenantId == tenantId && (x.IsDispatchUser != true)).ToList();
                ////ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                ////List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();
                ////List<WellIdentityUser> usersList = commonBusiness.GetComponentPermissionUsers("RouterUser", tenantId);

                ////foreach (var user in usersList)
                ////{
                ////    UserViewSRVModel userViewModel = new UserViewSRVModel();
                ////    userViewModel.UserID = user.Id;
                ////    userViewModel.UserName = user.FirstName + " " + user.LastName ?? "";
                ////    userViewModel.JobTitle = user.JobTitle;
                ////    userViewModel.PhoneNumber = user.Mobile;
                ////    userViewModel.Email = user.Email;
                ////    userViewModelList.Add(userViewModel);
                ////}


                //---------------------------

                List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();

                //   int skip = (pageNumber - 1) * Convert.ToInt32(pageSize);
                var total = _userManager.Users.Where(x => x.TenantId == tenantId).Count();
                var results = (from u in _userManager.Users
                                   join tu in db.TenantUsers on u.Id equals tu.UserId
                                   join crm in db.CrmUserBasicDetail on u.Id equals crm.UserId into crmLJ
                                   from crm in crmLJ.DefaultIfEmpty()
                                   where tu.TenantId == tenantId && u.UserRouteStatus != "INACTIVE"
                                   select new CustomerUsersModel
                                   {
                                       UserID = u.Id,
                                       PhoneNumber = u.PhoneNumber,
                                       Email = u.Email,
                                       FirstName = u.FirstName,
                                       MiddleName = u.MiddleName,
                                       FullName = @$"{u.FirstName} {u.MiddleName} {u.LastName}",
                                       LastName = u.LastName,
                                       Mobile = u.Mobile,
                                       JobTitle = u.JobTitle,
                                       Address = u.Address,
                                       City = u.City,
                                       State = u.State,
                                       Zip = u.Zip,
                                       AccountType = crm == null ? 0 : (int)crm.AccountType,
                                       IsPrimary = u.Primary.HasValue ? u.Primary.Value : false,
                                       AdditionalNotes = u.AdditionalNotes,
                                       WellOfficeUser = u.WellUser.HasValue ? u.WellUser.Value : false,
                                       Field = u.Field.HasValue ? u.Field.Value : false,
                                       IsActive = crm == null ? false : crm.IsActive,
                                       IsMaster = crm == null ? false : crm.IsMaster,
                                       ProfileImageName = u.ProfileImageName,
                                       UserTenantId = u.TenantId,
                                       //}).Skip(skip).Take(pageSize).ToListAsync();
                                   }).ToList();

                foreach (var user in results)
                {
                    UserViewSRVModel userViewModel = new UserViewSRVModel();
                    userViewModel.UserID = user.UserID;
                    userViewModel.UserName = user.FirstName + " " + user.LastName ?? "";
                    userViewModel.JobTitle = user.JobTitle;
                    userViewModel.PhoneNumber = user.Mobile;
                    userViewModel.Email = user.Email;
                    userViewModelList.Add(userViewModel);
                }


                return Json(userViewModelList.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch AdvisorUsers", User.Identity.Name);
                return null;
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<IActionResult> DispatchRoutes_InitialRead([DataSourceRequest] DataSourceRequest request)
        {
            List<DispatchRoutesModel> routes = new List<DispatchRoutesModel>();
            return Json(routes.ToDataSourceResult(request));
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]

        public async Task<IActionResult> DispatchRoutes_Read(string user)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            List<DispatchRoutesModel> routes = new List<DispatchRoutesModel>();
            List<WellAI.Advisor.Model.ServiceCompany.Models.GeoLocations> locationsList = new List<WellAI.Advisor.Model.ServiceCompany.Models.GeoLocations>();
            try
            {
                routes = await commonBusiness.GetDispatchRoutes(user, false);
                locationsList = (from wr in routes
                                 select new WellAI.Advisor.Model.ServiceCompany.Models.GeoLocations
                                 {
                                     latitude = Convert.ToDouble(wr.latitude),
                                     longitude = Convert.ToDouble(wr.longitude),
                                     name = wr.address + wr.city != "" ? "," + wr.city : "" + wr.state != "" ? "," + wr.state : "" + wr.zip != "" ? "," + wr.zip : "",
                                     city = wr.city,
                                     state = wr.state,
                                     address = wr.address
                                 }
                               ).ToList();

                return Json(locationsList);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch DispatchRoutes_Read", User.Identity.Name);
                return Json(locationsList);
            }
        }



        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetWellDetailsByApiNumber(string text, string filterType)
        {
            List<Result> WellData = new List<Result>();

            if (text != null)
            {
                text = text.ToUpper();
            }

            if (filterType.Equals("null") == true)
            {
                filterType = "API";
            }
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

                    string Url = "";
                    if (filterType == "Name")
                    {
                        Url = GetUrl + "search_wells/name/";
                    }
                    else if (filterType == "API")
                    {
                        Url = GetUrl + "search_wells/api_number/";
                    }

                    response = await client.GetAsync(Url + text).ConfigureAwait(true);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        ResResult = JsonConvert.DeserializeObject<WellApiData>(result);
                    }
                }

                if (ResResult.results != null)
                {
                    //WellApiData ResResult = new WellApiData();
                    //ResResult = JsonConvert.DeserializeObject<WellApiData>(JsonResult);
                    var wellResult = ResResult.results;

                    if (filterType == "API")
                    {
                        if (wellResult.Count > 0)
                        {
                            wellResult = wellResult.Where(ap => ap.api_number.Contains(text)).ToList();
                        }
                    }
                    else if (filterType == "Name")
                    {

                        if (wellResult.Count > 0)
                        {
                            wellResult = wellResult.Where(ap => ap.name.Contains(text)).ToList();
                        }
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellDetailsByApiNumber", User.Identity.Name);
               // _logger.LogInformation(ex.Message);
                return Json(WellData);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetRigDetailsByNameAsync(string text)
        {
            List<RigResult> rigData = new List<RigResult>();

            try
            {
                RigApiData rigResults = new RigApiData();
                var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];

                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    string Url = "";
                    Url = GetUrl + "search_rigs/";
                    response = await client.GetAsync(Url + text.ToUpper()).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {

                        string result = response.Content.ReadAsStringAsync().Result;
                        rigResults = JsonConvert.DeserializeObject<RigApiData>(result);
                    }
                }
                if (rigResults.results != null)
                {
                    var rigDataResult = rigResults.results;


                    rigDataResult = rigDataResult.Where(ap => ap.rig.Contains(text.ToUpper())).ToList();

                    return Json(rigDataResult);
                }

                else if (rigResults.message != null && text != null)
                {
                    TempData["Error"] = rigResults.message;
                    return Json(rigData);
                }
                else
                {
                    return Json(rigData);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetRigDetailsByNameAsync", User.Identity.Name);
              //  _logger.LogInformation(ex.Message);
                return Json(rigData);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> AddNewDispatch([FromBody] DispatchRoutesModel dispatch)
        {
            try
            {
                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                DispatchRoutes objDispatch = new DispatchRoutes();

                objDispatch.LocationAddress = dispatch.address;
                objDispatch.LocationName = dispatch.locationname;
                objDispatch.LocationCity = dispatch.city;
                objDispatch.LocationState = dispatch.state;
                objDispatch.LocationZip = dispatch.zip;
                objDispatch.Latitude = Convert.ToDouble(dispatch.latitude);
                objDispatch.Longitude = Convert.ToDouble(dispatch.longitude);
                //objDispatch.DispatchNotes = dispatch.dispatchnotes;
                objDispatch.RouteOrder = dispatch.routeorder;
                objDispatch.UserId = dispatch.userid;
                objDispatch.CreatedDate = dispatch.createddate;
                objDispatch.Customer = dispatch.customer;
                objDispatch.APINumber = dispatch.api;
                objDispatch.WellName = dispatch.wellname;
                objDispatch.RigName = dispatch.rigname;
                objDispatch.WellId = dispatch.wellid;
                objDispatch.RigId = dispatch.rigid;
                objDispatch.ScheduledArrival = dispatch.scheduledArrivalDate;

                var status = commonBusiness.CreateDispatchRoute(objDispatch);
                var notifystatus = await SendDispatchRoutes(dispatch.userid, dispatch.dispatchnotes);

                return status;

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch AddDispatchUser", User.Identity.Name);
                return true;
            }

        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> Senddispatchrouts_V2([FromBody] string userId)
        {
            try
            {


                //  var notifystatus = await SendDispatchRoutes(dispatchrecord, dispatch.dispatchnotes);
                var notifystatus = await SendDispatchRoutes_V2(userId, "");
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch AddDispatchUser", User.Identity.Name);
                return true;
            }

        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> UpdateRouterStatus([FromBody] DispatchRoutes roustatusdetails)
        {
            try
            {
                //JavaScriptSerializer js = new JavaScriptSerializer();

                //  DispatchRoutesModel[] dispatchroutes = JsonConvert.DeserializeObject<DispatchRoutesModel[]>(Convert.ToString(dispatchrecord));
                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                //-----------

                var status = await commonBusiness.UpdateRouterStatus(roustatusdetails);


                return Json(status);

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch AddNewDispatch_V2", User.Identity.Name);
                return Json(customErrorHandler);
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> AddNewDispatch_V2([FromBody] dynamic dispatchrecord)
        {
            try
            {
                //JavaScriptSerializer js = new JavaScriptSerializer();

                DispatchRoutesModel[] dispatchroutes = JsonConvert.DeserializeObject<DispatchRoutesModel[]>(Convert.ToString(dispatchrecord));
                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                //-----------
                string Userid = "";
                string HistoryId = Guid.NewGuid().ToString("D");
                ViewBag.HistoryId = HistoryId;
                TempData["HistoryId"] = HistoryId;
                Userid = dispatchroutes[0].userid;
                //----------History details

                //---------DispatchRoutes Table

                dynamic fromval;
                dynamic toval;
                var status = true;
                int RouteOrder = 0;

                List<DispatchRoutes> objDispatch = new List<DispatchRoutes>();
                List<DispatchRoutesHistoryDetailsModel> routesList2 = new List<DispatchRoutesHistoryDetailsModel>();

                List<DispatchRoutesHistoryDetailsModel> routesList = new List<DispatchRoutesHistoryDetailsModel>();
                foreach (var dispatch in dispatchroutes)
                {
                    RouteOrder = RouteOrder + 1;
                    DispatchRoutes route = new DispatchRoutes();
                    DispatchRoutesHistoryDetailsModel routeDetails = new DispatchRoutesHistoryDetailsModel();
                    DateTime timeUtc = (TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local));
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                    string dispatchdispatchid = dispatch.dispatchid.Trim().ToString();
                    if (dispatchdispatchid.Substring(0, 1) == "-")
                    {
                        route.DispatchId = null;
                        route.CreatedDate = cstTime;//DateTime.Now.ToString()
                        //route.CreatedDate = DateTime.Now;
                        route.RecordStatus = "1";
                        route.RouteOrder = dispatch.routeorder;
                    }
                    else
                    {
                        route.DispatchId = dispatch.dispatchid;
                        route.CreatedDate = dispatch.createddate;
                        route.RouteOrder = 0;
                        //  route.RecordStatus = null;
                    }
                    route.LocationAddress = dispatch.address;
                    route.LocationName = dispatch.locationname;
                    route.LocationCity = dispatch.city;
                    route.LocationState = dispatch.state;
                    route.LocationZip = dispatch.zip;
                    route.Latitude = Convert.ToDouble(dispatch.latitude);
                    route.Longitude = Convert.ToDouble(dispatch.longitude);
                    route.DispatchNotes = dispatch.dispatchnotes;
                    route.UserId = dispatch.userid;
                    route.Customer = dispatch.customer;
                    route.APINumber = dispatch.api;
                    route.WellName = dispatch.wellname;
                    route.RigName = dispatch.rigname;
                    route.WellId = dispatch.wellid;
                    route.RigId = dispatch.rigid;

                    //   route.ModifiedDate = DateTime.Now;
                    route.ModifiedDate = cstTime;
                    route.CurrentRouterOrder = RouteOrder;
                    route.ScheduledArrival = dispatch.scheduledArrivalDate;

                    //History Details Object
                    routeDetails.dispatchid = dispatch.dispatchid;
                    routeDetails.customer = dispatch.customer;
                    routeDetails.locationname = dispatch.locationname;
                    routeDetails.address = dispatch.address;
                    routeDetails.city = dispatch.city;
                    routeDetails.state = dispatch.state;
                    routeDetails.zip = dispatch.zip;
                    routeDetails.latitude = dispatch.latitude;
                    routeDetails.longitude = dispatch.longitude;
                    routeDetails.apinumber = dispatch.api;
                    routeDetails.wellname = dispatch.wellname;
                    routeDetails.rigname = dispatch.rigname;
                    routeDetails.wellid = dispatch.wellid;
                    routeDetails.rigid = dispatch.rigid;
                    //
                    //routeDetails.changedrouterorder = dispatch.routeorder;                    
                    objDispatch.Add(route);
                    routesList.Add(routeDetails);
                }
                routesList2 = await commonBusiness.CreateDispatchRoute_V2(objDispatch, routesList);


                //-------------DispatchRoutesHistory Tables
                DispatchRoutesHistoryModel objChanges = new DispatchRoutesHistoryModel();
                objChanges.userid = Userid;
                objChanges.dispatchfrom = "A";
                objChanges.dispatchnotes = dispatchroutes[0].dispatchnotes;
                objChanges.routes = routesList2;
                objChanges.historyId = HistoryId;

                var historyStatus = await commonBusiness.CreateDispatchRoutesHistory(objChanges);

                //return HistoryId.ToString();
                return Json(HistoryId);

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch AddNewDispatch_V2", User.Identity.Name);
                return Json(customErrorHandler);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> DeleteDispatch_V2([FromBody] dynamic dispatchrecord, string HistoryHeadId)
        {
            try
            {
                DispatchRoutesModel[] dispatchroutes = JsonConvert.DeserializeObject<DispatchRoutesModel[]>(Convert.ToString(dispatchrecord));
                var status = true;
                //  dispatchroutes = JsonConvert.DeserializeObject<DispatchRoutesModel[]>(Convert.ToString(dispatchrecord));


                string Userid = "";
                //HistoryId = Guid.NewGuid().ToString("H");
                // string HistoryId = TempData["HistoryId"].ToString();
                string HistoryId = HistoryHeadId;

                Userid = dispatchroutes[0].userid;


                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //--------------
                dynamic fromval;
                dynamic toval;

                int RouteOrder = 0;
                List<DispatchRoutesHistoryDetailsModel> routesList2 = new List<DispatchRoutesHistoryDetailsModel>();
                List<DispatchRoutes> objDispatch = new List<DispatchRoutes>();
                List<DispatchRoutesHistoryDetailsModel> routesList = new List<DispatchRoutesHistoryDetailsModel>();
                foreach (var dispatch in dispatchroutes)
                {
                    //RouteOrder = RouteOrder + 1;
                    DispatchRoutes route = new DispatchRoutes();
                    DispatchRoutesHistoryDetailsModel routeDetails = new DispatchRoutesHistoryDetailsModel();
                    DateTime timeUtc = (TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local));
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                    if (dispatch.dispatchid == null)
                    {
                        route.DispatchId = null;
                        route.CreatedDate = cstTime;//DateTime.Now.ToString()
                    }
                    else
                    {
                        route.DispatchId = dispatch.dispatchid;
                        route.CreatedDate = dispatch.createddate;
                    }
                    route.LocationAddress = dispatch.address;
                    route.LocationName = dispatch.locationname;
                    route.LocationCity = dispatch.city;
                    route.LocationState = dispatch.state;
                    route.LocationZip = dispatch.zip;
                    route.Latitude = Convert.ToDouble(dispatch.latitude);
                    route.Longitude = Convert.ToDouble(dispatch.longitude);
                    route.DispatchNotes = dispatch.dispatchnotes;
                    route.UserId = dispatch.userid;
                    route.Customer = dispatch.customer;
                    route.APINumber = dispatch.api;
                    route.WellName = dispatch.wellname;
                    route.RigName = dispatch.rigname;
                    route.WellId = dispatch.wellid;
                    route.RigId = dispatch.rigid;
                    route.RecordStatus = "0";
                    route.ModifiedDate = cstTime;
                    route.CurrentRouterOrder = 0;
                    route.ScheduledArrival = dispatch.scheduledArrivalDate;

                    //History Details Object
                    routeDetails.dispatchid = dispatch.dispatchid;
                    routeDetails.customer = dispatch.customer;
                    routeDetails.locationname = dispatch.locationname;
                    routeDetails.address = dispatch.address;
                    routeDetails.city = dispatch.city;
                    routeDetails.state = dispatch.state;
                    routeDetails.zip = dispatch.zip;
                    routeDetails.latitude = dispatch.latitude;
                    routeDetails.longitude = dispatch.longitude;
                    routeDetails.apinumber = dispatch.api;
                    routeDetails.wellname = dispatch.wellname;
                    routeDetails.rigname = dispatch.rigname;
                    routeDetails.wellid = dispatch.wellid;
                    routeDetails.rigid = dispatch.rigid;
                    // routeDetails.routerorder = dispatch.routerorder;
                    //routeDetails.changedrouterorder = dispatch.routeorder;                    
                    objDispatch.Add(route);
                    routesList.Add(routeDetails);
                }
                routesList2 = await commonBusiness.DeleteDispatchRoute_V2(objDispatch, routesList);

                //-------------DispatchRoutesHistory Tables
                DispatchRoutesHistoryModel objChanges = new DispatchRoutesHistoryModel();
                objChanges.userid = Userid;
                objChanges.dispatchfrom = "A";
                objChanges.dispatchnotes = dispatchroutes[0].dispatchnotes;
                objChanges.routes = routesList2;
                objChanges.historyId = HistoryId;

                var historyStatus = await commonBusiness.CreateDispatchRoutesHistory_Deleted(objChanges);
                return status;
            }



            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch DeleteDispatch_V2", User.Identity.Name);
                return true;
            }

        }

        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //public async Task<bool> DeleteDispatch_V23([FromBody] dynamic dispatchrecord)
        //{
        //    try
        //    {


        //        string jsonString = Convert.ToString(dispatchrecord);
        //        var record = JObject.Parse(jsonString);

        //        var dispatchid = record["dispatchid"].ToString();
        //        var userid = record["userid"].ToString();


        //        var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
        //        var status = true;
        //        foreach (var dispatch in dispatchrecord)
        //        {
        //            DispatchRoutes objDispatch = new DispatchRoutes();

        //            objDispatch.DispatchId = dispatch.dispatchid;
        //            objDispatch.UserId = dispatch.userid;
        //            objDispatch.RecordStatus = "0";

        //            objDispatch.ModifiedDate = DateTime.Now;
        //             status = commonBusiness.DeleteDispatchRoute_V2(objDispatch);
        //        }

        //        return status;
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "Dispatch AddDispatchUser", User.Identity.Name);
        //        return true;
        //    }

        //}


          [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetUserLocationData(string userId)
        {

            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            UserCurrentLocation userLocation = new UserCurrentLocation();
            location locationData = new location();
            string Url = "";
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "user_location/";
                    response = await client.GetAsync(Url + userId).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        userLocation = JsonConvert.DeserializeObject<UserCurrentLocation>(response.Content.ReadAsStringAsync().Result);
                    }
                    return Json(response.Content.ReadAsStringAsync().Result);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch AddDispatchUser", User.Identity.Name);
                return Json(locationData);
            }

        }
          [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetUserCurrentLocation(string userId)
        {

            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            UserCurrentLocation userLocation = new UserCurrentLocation();
            location locationData = new location();
            string Url = "";
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "user_location/";
                    response = await client.GetAsync(Url + userId).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        userLocation = JsonConvert.DeserializeObject<UserCurrentLocation>(response.Content.ReadAsStringAsync().Result);
                    }

                    if (userLocation.result == "success")
                    {
                        locationData = userLocation.location;

                    }
                    else if (userLocation.result == "failure")
                    {
                        locationData.message = userLocation.message;
                    }
                }
                return Json(userLocation.location);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch AddDispatchUser", User.Identity.Name);
                return Json(locationData);
            }
        }

        [Microsoft.AspNetCore.Mvc.AcceptVerbs("Post")]
        public async Task<IActionResult> DispatchDataDestroy(string dispatchId, string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(dispatchId))
                {
                    var itemToDelete = (from Ds in db.DispatchRoutes
                                        where Ds.DispatchId == dispatchId
                                        select Ds).SingleOrDefault();
                    if (itemToDelete != null)
                    {
                        db.DispatchRoutes.Remove(itemToDelete);
                        await db.SaveChangesAsync();
                    }

                    var notifystatus = await SendDispatchRoutes(userId, "");

                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellDataDestroy", User.Identity.Name);
                //_logger.LogInformation(ex.Message);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            return Json(null);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> UpdateDispatchNotes([FromBody] DispatchRoutesModel dispatch)
        {
            try
            {
                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                DispatchRoutes objDispatch = new DispatchRoutes();
                objDispatch.DispatchNotes = dispatch.dispatchnotes;
                objDispatch.DispatchId = dispatch.dispatchid;
                objDispatch.UserId = dispatch.userid;

                var status = commonBusiness.UpdateDispatch(objDispatch);
             //   var notifystatus = await SendDispatchRoutes(dispatch.userid, dispatch.dispatchnotes);
                return status;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch UpdateDispatchUser", User.Identity.Name);
                return true;
            }

        }

          [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetUserCurrentRoutes(string userId)
        {

            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            UserRoutes userLocations = new UserRoutes();
            location locationData = new location();
            string Url = "";
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "user_location/";
                    response = await client.GetAsync(Url + userId + "/1").ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        userLocations = JsonConvert.DeserializeObject<UserRoutes>(response.Content.ReadAsStringAsync().Result);
                    }

                    if (userLocations.result == "success")
                    {
                        locationData = userLocations.location;

                    }
                    else if (userLocations.result == "failure")
                    {
                        locationData.message = userLocations.message;
                    }
                }
                return Json(userLocations);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetUserCurrentRoutes", User.Identity.Name);
                return Json(locationData);
            }
        }

          [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetUserCurrentRoutes_ETA(string userId)
        {

            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            UserRoutes userLocations = new UserRoutes();
            location locationData = new location();
            string Url = "";
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    // Url = GetUrl + "user_location/";
                    // response = await client.GetAsync(Url + userId + "/1").ConfigureAwait(true);
                    Url = GetUrl + "user_location/9d6d8aae-a871-4834-9111-bbd5fa2c9314/1 ";
                    response = await client.GetAsync(Url).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        userLocations = JsonConvert.DeserializeObject<UserRoutes>(response.Content.ReadAsStringAsync().Result);
                    }

                    if (userLocations.result == "success")
                    {
                        locationData = userLocations.location;

                    }
                    else if (userLocations.result == "failure")
                    {
                        locationData.message = userLocations.message;
                    }
                }
                return Json(userLocations);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetUserCurrentRoutes", User.Identity.Name);
                return Json(locationData);
            }
        }

        public async Task<bool> SendDispatchRoutes(string userId, string dispatchNotes)
        {

            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            string Url = "";
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var dispatchRoute = await commonBusiness.GetActiveDispatchRoutes(userId, dispatchNotes);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "/user_destinations";
                    var stringContent = new StringContent(JsonConvert.SerializeObject(dispatchRoute), Encoding.UTF8, "application/json");
                    response = await client.PostAsync(Url, stringContent).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch SendDispatchRoutes", User.Identity.Name);
                return false;
            }

        }

        public async Task<bool> SendDispatchRoutes_V2(string userId, string dispatchNotes)
        {

            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            string Url = "";
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var dispatchRoute = await commonBusiness.GetActiveDispatchRoutes_V2(userId, dispatchNotes);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "/user_destinations";
                    var stringContent = new StringContent(JsonConvert.SerializeObject(dispatchRoute), Encoding.UTF8, "application/json");
                    response = await client.PostAsync(Url, stringContent).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch SendDispatchRoutes", User.Identity.Name);
                return false;
            }

        }

          [Microsoft.AspNetCore.Mvc.HttpGet]
        public string GetActiveUserNotes(string userId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                string notes = commonBusiness.GetActiveUserNotes(userId);

                return JsonConvert.SerializeObject(new { data = notes });

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetActiveUserNotes", User.Identity.Name);
                return JsonConvert.SerializeObject(new { data = "" });
            }
        }
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> user_destinations([FromBody] DispatchNotification notification)
        {
            Console.WriteLine(notification.user_key.ToString());
            return true;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> RefreshRoutes()
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                //WellAI.Advisor.BLL.Business.DispatchBusiness dispatch = new WellAI.Advisor.BLL.Business.DispatchBusiness(db, _roleManager, _userManager, _configuration);
                //var result = _userManager.Users.Where(x => x.TenantId == HttpContext.GetMultiTenantContext().TenantInfo.Id && (x.IsDispatchUser != true)).ToList();
                //ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //IDispatchRepository dispatchBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
                //List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();
                //List<WellIdentityUser> usersList = commonBusiness.GetComponentPermissionUsers("RouterUser", HttpContext.GetMultiTenantContext().TenantInfo.Id);


                WellAI.Advisor.BLL.Business.DispatchBusiness dispatch = new WellAI.Advisor.BLL.Business.DispatchBusiness(db, _roleManager, _userManager, _configuration);
                var result = _userManager.Users.Where(x => x.TenantId == tenantId && (x.IsDispatchUser != true)).ToList();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                IDispatchRepository dispatchBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
                List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();
                List<WellIdentityUser> usersList = commonBusiness.GetComponentPermissionUsers("RouterUser", tenantId);

                foreach (var user in usersList)
                {
                    UserViewSRVModel userViewModel = new UserViewSRVModel();
                    userViewModel.UserID = user.Id;
                    userViewModel.UserName = user.FirstName + " " + user.LastName ?? "";
                    userViewModel.JobTitle = user.JobTitle;
                    userViewModel.PhoneNumber = user.Mobile;
                    userViewModel.Email = user.Email;

                    string dispatchAPIUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
                    UserRoutes userRoutes = await dispatch.GetUserCurrentRoutes(user.Id, dispatchAPIUrl);
                    if (userRoutes != null)
                    {
                        if (userRoutes.message != "User Not Found")
                        {
                            await dispatchBusiness.UpdateUserRoutes(user.Id, userRoutes);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch RefreshRoutes", User.Identity.Name);
                return true;
            }

        }


        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetUserDestinations(string userId)
        {
            userdestinations destinations = new userdestinations();
            CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
            try
            {
                var result = objComBusiness.GetDispatchRoutes(userId, false).Result;

                if (result != null)
                {
                    destinations.type = "FeatureCollection";

                    List<features> featuresList = new List<features>();
                    foreach (var item in result)
                    {
                        features feature = new features();
                        feature.type = "Feature";
                        geometry geometry = new geometry();
                        geometry.type = "Point";
                        float[] coordinates = new float[2];
                        coordinates[0] = Convert.ToSingle(item.longitude); coordinates[1] = Convert.ToSingle(item.latitude);
                        geometry.coordinates = coordinates;

                        properties properties = new properties();
                        properties.title = item.username;
                        properties.description = item.locationname;

                        feature.geometry = geometry;
                        feature.properties = properties;
                        featuresList.Add(feature);
                    }
                    if (featuresList.Count > 0)
                    {
                        destinations.features = featuresList.ToArray();
                    }
                }
                return Json(destinations);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch return Json(destinations);", User.Identity.Name);
                return Json(destinations);

            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> UpdateDispatchRouteOrder([FromBody] DispatchRouteOrderModel dispatchList)
        {
            try
            {
                var commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var status = await commonBusiness.UpdateDispatchRouteOrders(dispatchList);

                var notifystatus = await SendDispatchRoutes(dispatchList.DispatchRoutesModel[0].userid, "");
                return status;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch UpdateDispatchUser", User.Identity.Name);
                return true;
            }

        }



          [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetUserRoutes()
        {

            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            //UserRoutes userLocations = new UserRoutes();
            UserCurrentLocation locationData = new UserCurrentLocation();
            string Url = "";
            List<UserCurrentLocationdetails> locations = new List<UserCurrentLocationdetails>();
            List<UsersOptions> luseroptions = new List<UsersOptions>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                string userId = WellAIAppContext.Current.Session.GetString("UserId");

                string TenantIds = WellAIAppContext.Current.Session.GetString("TenantId");

                //var UID = db.Users.Where(x => x.TenantId == TenantId)
                //     .Select(x => x.Id).Distinct().ToArray();




                //var result = db.DispatchRoutes
                //     .Select(x => x.UserId).Distinct().ToArray();


                var result = (from u in db.Users
                              join d in db.DispatchRoutes on u.Id equals d.UserId
                              where u.TenantId == TenantIds
                              select u.Id).Distinct().ToArray();

                var UserIds = string.Join(",", result);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "users_location/";
                    response = await client.GetAsync(Url + UserIds).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        // locationData = JsonConvert.DeserializeObject<UserCurrentLocation>(response.Content.ReadAsStringAsync().Result);
                        string mycontent = response.Content.ReadAsStringAsync().Result;
                        locations = JsonConvert.DeserializeObject<UserCurrentLocationdetails[]>(mycontent).ToList();

                        UsersOptions userOption = new UsersOptions();
                        foreach (var loc in locations)
                        {
                            if (loc.message != "User Not Found")
                            {
                                var userResult = db.Users.Where(x => x.Id == loc.user_key).FirstOrDefault();
                                userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                                if (loc.location != null)
                                {
                                    userOption.Locations = loc.location.latitude.ToString() + "," + loc.location.longitude.ToString();
                                }
                                userResult.UsersOptions = JsonConvert.SerializeObject(userOption);
                                loc.username = userResult.FirstName + ' ' + userResult.LastName;
                                loc.city = userResult.City;
                                loc.state = userResult.State;
                                loc.area = userResult.Address;
                                db.SaveChanges();
                            }
                        }
                    }

                    return Json(locations);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetUserCurrentRoutes", User.Identity.Name);
                return Json(locationData);
            }
        }


        //  [Microsoft.AspNetCore.Mvc.HttpGet]
        //public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetUserRoutes()
        //{

        //    var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
        //    //UserRoutes userLocations = new UserRoutes();
        //    UserCurrentLocation locationData = new UserCurrentLocation();
        //    string Url = "";
        //    List<UserCurrentLocation> locations = new List<UserCurrentLocation>();
        //    //   List<UsersRoutesdetails> locations = new List<UsersRoutesdetails>();
        //    //  UsersRoutesdetails locations = new UsersRoutesdetails();
        //   // List<UsersRoutesdetails> locations = new List<UsersRoutesdetails>();
        //    List<UsersOptions> luseroptions = new List<UsersOptions>();
        //    try
        //    {
        //        ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

        //        var result = db.DispatchRoutes
        //              .Select(x => x.UserId).Distinct().ToArray();

        //        var UserIds = string.Join(",", result);
        //        //UserIds = "9d6d8aae-a871-4834-9111-bbd5fa2c9314";
        //        using (var client = new HttpClient())
        //        {
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            HttpResponseMessage response = new HttpResponseMessage();
        //            Url = GetUrl + "users_location/";
        //            response = await client.GetAsync(Url + UserIds ).ConfigureAwait(true);
        //            if (response.IsSuccessStatusCode)
        //            {
        //               // locationData = JsonConvert.DeserializeObject<UserCurrentLocation>(response.Content.ReadAsStringAsync().Result);
        //                var mycontent =  response.Content.ReadAsStringAsync().Result;
        //                // locations = JsonConvert.DeserializeObject<UserCurrentLocation[]>(mycontent).ToList();

        //             //   locations = JsonConvert.DeserializeObject<UsersRoutesdetails[]>(ud).ToList();

        //                var temp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(mycontent);

        //                temp.Descendants()
        //                 .OfType<JProperty>()
        //                 .Where(attr => attr.Name.StartsWith("q"))
        //                 .ToList() // you should call ToList because you're about to changing the result, which is not possible if it is IEnumerable
        //                 .ForEach(attr => attr.Remove());
        //                // string t1 = temp.Parent.ToString();
        //                //  string temp2 = temp.Root.ToString();
        //                //  var ud= JsonConvert.SerializeObject(temp2);


        //                //UserCurrentLocation locations_ind = new UserCurrentLocation();
        //                // JObject results = JObject.Parse(temp.ToString());
        //                //// foreach (var result2 in results["0"])
        //                // //{
        //                //     locations_ind.result = results["0"].ToString();
        //                //     locations_ind.user_key = results["3"].ToString();
        //                //     locations_ind.message = results["2"].ToString();

        //                // var locationdetails= JsonConvert.SerializeObject(results["1"]); 
        //                // locations_ind.location = JsonConvert.DeserializeObject<location>(locationdetails);

        //                //     locations.Add(locations_ind);
        //                // //  }
        //                UserCurrentLocation locations_ind = new UserCurrentLocation();
        //                JObject results = JObject.Parse(temp.ToString());
        //                for (int i= 0; results.Count>i;i++)
        //                {
        //                    // var locationdata = JsonConvert.SerializeObject(results[i].ToString());
        //                    var locationdata = results[i.ToString()].ToString();
        //                    locations_ind = JsonConvert.DeserializeObject<UserCurrentLocation>(locationdata);

        //                }
        //                locations.Add(locations_ind);

        //                  UsersOptions userOption = new UsersOptions();

        //                    foreach (var loc in locations)
        //                {
        //                    if (loc.message != "User Not Found")
        //                    {
        //                        var userResult = db.Users.Where(x => x.Id == loc.user_key).FirstOrDefault();
        //                        userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
        //                        if (loc.location != null)
        //                        {
        //                            userOption.Locations = loc.location.latitude.ToString() + "," + loc.location.longitude.ToString();
        //                        }
        //                        userResult.UsersOptions = JsonConvert.SerializeObject(userOption);
        //                        db.SaveChanges();
        //                    }
        //                }
        //            }

        //            return Json(locations);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, "Dispatch GetUserCurrentRoutes", User.Identity.Name);
        //        return Json(locationData);
        //    }
        //}


        public IActionResult Dashboard()
        {
            try
            {
                List<StatusViewModel> status = new List<StatusViewModel>();
                List<AuctionBidStatusViewModel> bidstatus = new List<AuctionBidStatusViewModel>();

                var bidstatusItem = new AuctionBidStatusViewModel
                {
                    AuctionBidStatusName = "--Select--",
                    AuctionBidStatus = "null2"
                };
                bidstatus.Add(bidstatusItem);

                bidstatusItem = new AuctionBidStatusViewModel
                {
                    AuctionBidStatusName = "Completed",
                    AuctionBidStatus = "Completed"
                };
                bidstatus.Add(bidstatusItem);

                bidstatusItem = new AuctionBidStatusViewModel
                {
                    AuctionBidStatusName = "Active",
                    AuctionBidStatus = "Active"
                };
                bidstatus.Add(bidstatusItem);



                var statusItem = new StatusViewModel
                {
                    StatusName = "--Select--",
                    DriverStatus = "null2"
                };
                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "ACTIVE",
                    DriverStatus = "ACTIVE"
                };
                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "INACTIVE",
                    DriverStatus = "INACTIVE"
                };
                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "ON-ROUTE",
                    DriverStatus = "ON-ROUTE"
                };

                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "ON-SITE",
                    DriverStatus = "ON-SITE"
                };

                status.Add(statusItem);

                statusItem = new StatusViewModel
                {
                    StatusName = "OVERDUE",
                    DriverStatus = "OVERDUE"
                };

                status.Add(statusItem);


                ViewData["BidStatus"] = bidstatus;
                ViewData["DispatchStatus"] = status;

             //   if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";


                    return LocalRedirect(returnUrl);
                }
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch Index", User.Identity.Name);
                string returnUrl = @"/Identity/Account/Login";
                return LocalRedirect(returnUrl);
            }
        }


        public IActionResult GetDriverStatusSummary([DataSourceRequest] DataSourceRequest request, string valueFrom, string valueTo)
        {
            List<DispatchDashboardModel> driverStatus = new List<DispatchDashboardModel>();
            //try
            //{
            //    //DispatchDashboardModel dispatch = new DispatchDashboardModel();
            //    //data = _singleton.auctionProposalBusiness.GetAuctionsProposalListForSRV(tenantid, operId, servrepo); //GetBidsData(valueFrom, valueTo);

            //    DispatchDashboardModel model = new DispatchDashboardModel(1, "ACTIVE", "Adams", "Operator #001",
            //                                                              "R C GLIKINSON #1", "RIG 1", "2022-07-21 11:30 AM", "2022-07-21 10:00 AM",
            //                                                              "Yes", "Service Notes");
            //    driverStatus.Add(model);

            //    model = new DispatchDashboardModel(2, "INACTIVE", "Bob Jones", "Operator #002",
            //                                                               "R C GLIKINSON #2", "RIG 2", "2022-07-22 11:30 AM", "2022-07-22 10:00 AM",
            //                                                               "Yes", "Service Notes");
            //    driverStatus.Add(model);


            //    model = new DispatchDashboardModel(3, "ON-ROUTE", "John Walker", "Operator #003",
            //                                                                "R C GLIKINSON #3", "RIG 3", "2022-07-22 11:30 AM", "2022-07-22 11:30 AM",
            //                                                                "No", "Service Notes");
            //    driverStatus.Add(model);

            //    model = new DispatchDashboardModel(4, "ON-SITE", "James", "",
            //                                                                "", "", "", "",
            //                                                                "No", "");
            //    driverStatus.Add(model);

            //    model = new DispatchDashboardModel(5, "OVERDUE", "Mike", "Operator #004",
            //                                                           "R C GLIKINSON #4", "RIG 4", "2022-07-22 12:30 PM", "2022-07-22 11:30 AM",
            //                                                           "Yes", "Service Notes");
            //    driverStatus.Add(model);

            //}
            //catch (Exception ex)
            //{
            //    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
            //    customErrorHandler.WriteError(ex, " ", User.Identity.Name);
            //    string returnUrl = @"/Dashboard/Error";
            //    //return LocalRedirect(returnUrl);
            //}
            ////return Json(driverStatus.ToDataSourceResult(request));
            return Json(driverStatus.ToDataSourceResult(request));
        }

        //public Task<IActionResult> GetDrivesStatusSummary([DataSourceRequest] DataSourceRequest request)
        public IActionResult GetDrivesStatusSummary([DataSourceRequest] DataSourceRequest request)
        {
            List<DispatchDashboardModel> driverStatus = new List<DispatchDashboardModel>();
            try
            {
                //DispatchDashboardModel dispatch = new DispatchDashboardModel();
                //data = _singleton.auctionProposalBusiness.GetAuctionsProposalListForSRV(tenantid, operId, servrepo); //GetBidsData(valueFrom, valueTo);

                //DispatchDashboardModel model = new DispatchDashboardModel("1234", "ACTIVE");
                //driverStatus.Add(model);


                DispatchDashboardModel model = new DispatchDashboardModel();

                model.UserId = 1;
                model.DriverStatus = "ACTIVE";
                model.DriverName = "Adams";
                model.Customer = "Operator #001";
                model.DestinationWell = "R C GLIKINSON #1";
                model.DestinationWell = "RIG 1";
                model.ETA = "2022-07-21 11:30 AM";
                model.ScheduledArrival = "2022-07-21 10:00 AM";
                model.MultiDestination = "Yes";
                model.Notes = "Service Notes";
                driverStatus.Add(model);

                //model = new DispatchDashboardModel(2, "INACTIVE", "Bob Jones", "Operator #002",
                //                                                           "R C GLIKINSON #2", "RIG 2", "2022-07-22 11:30 AM", "2022-07-22 10:00 AM",
                //                                                           "Yes", "Service Notes");
                //driverStatus.Add(model);


                //model = new DispatchDashboardModel(3, "ON-ROUTE", "John Walker", "Operator #003",
                //                                                            "R C GLIKINSON #3", "RIG 3", "2022-07-22 11:30 AM", "2022-07-22 11:30 AM",
                //                                                            "No", "Service Notes");
                //driverStatus.Add(model);

                //model = new DispatchDashboardModel(4, "ON-SITE", "James", "",
                //                                                            "", "", "", "",
                //                                                            "No", "");
                //driverStatus.Add(model);

                //model = new DispatchDashboardModel(5, "OVERDUE", "Mike", "Operator #004",
                //                                                       "R C GLIKINSON #4", "RIG 4", "2022-07-22 12:30 PM", "2022-07-22 11:30 AM",
                //                                                       "Yes", "Service Notes");
                //driverStatus.Add(model);

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, " ", User.Identity.Name);
                //string returnUrl = @"/Dashboard/Error";
                ////return LocalRedirect(returnUrl);
            }
            //return Json(driverStatus.ToDataSourceResult(request));
            return Json(driverStatus.ToDataSourceResult(request));
        }



        //public IActionResult GetDispatchDetails([DataSourceRequest] DataSourceRequest request)

        //{
        //    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
        //    List<DispatchRoutes> driverStatus = new List<DispatchRoutes>();
        //    DispatchRoutes model = new DispatchRoutes();
        //   // DispatchRepository objComBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
        //    try
        //    {


        //        DispatchRepository objComBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
        //        var result = objComBusiness.GetDispatchDetails("b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90");






        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
        //        customErrorHandler.WriteError(ex, " ", User.Identity.Name);
        //        //string returnUrl = @"/Dashboard/Error";
        //        //return LocalRedirect(returnUrl);
        //    }
        //    //return Json(driverStatus.ToDataSourceResult(request));
        //    return Json(driverStatus.ToDataSourceResult(request));
        //}


        public IActionResult GetDispatchDetails([DataSourceRequest] DataSourceRequest request, string valueFrom, string valueTo)
        {
            // ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            // IDispatchBusiness ObjDispatchBusiness = new DispatchBusiness(db, _roleManager, _userManager, _configuration);
            //  List<DispatchRoutesViewModel> driverStatus = new List<DispatchRoutesViewModel>();
            //    DispatchRoutesViewModel DispatchRoutes = new DispatchRoutesViewModel();
            //  string userId = WellAIAppContext.Current.Session.GetString("UserId");
            string TenantId = WellAIAppContext.Current.Session.GetString("TenantId");

            DispatchBusiness objComBusiness = new DispatchBusiness(db, _roleManager, _userManager, _configuration);
            var result = objComBusiness.GetDispatchDetailsList(TenantId);
            try
            {
                //List<DispatchRoutesViewModel> driverStatus = new List<DispatchRoutesViewModel>();
                //  DispatchBusiness objComBusiness = new DispatchBusiness(db, _roleManager, _userManager, _configuration);

                // var DispatchRoutes = objComBusiness.GetDispatchDetailsList("UserId");

                //  IDispatchRepository ObjDispatchRepository = new DispatchRepository(db, _roleManager, _userManager, _configuration);
                // var driverStatus = driverStatus.GetDispatchDetailsList("b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90");

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, " ", User.Identity.Name);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult GetDispatchStatusCount()
        {
            // ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            // IDispatchBusiness ObjDispatchBusiness = new DispatchBusiness(db, _roleManager, _userManager, _configuration);
            //  List<DispatchRoutesViewModel> driverStatus = new List<DispatchRoutesViewModel>();
            //    DispatchRoutesViewModel DispatchRoutes = new DispatchRoutesViewModel();
            //   string userId = WellAIAppContext.Current.Session.GetString("UserId");
            string TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            DispatchBusiness objComBusiness = new DispatchBusiness(db, _roleManager, _userManager, _configuration);
            var result = objComBusiness.GetDispatchStatuscount(TenantId);
            try
            {
                //List<DispatchRoutesViewModel> driverStatus = new List<DispatchRoutesViewModel>();
                //  DispatchBusiness objComBusiness = new DispatchBusiness(db, _roleManager, _userManager, _configuration);

                // var DispatchRoutes = objComBusiness.GetDispatchDetailsList("UserId");

                //  IDispatchRepository ObjDispatchRepository = new DispatchRepository(db, _roleManager, _userManager, _configuration);
                // var driverStatus = driverStatus.GetDispatchDetailsList("b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90");

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, " ", User.Identity.Name);
                //string returnUrl = @"/Dashboard/Error";
                //return LocalRedirect(returnUrl);
            }
            return Json(result);
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult ForeignKeyColumn_UpdateAsync([DataSourceRequest] DataSourceRequest request,
           [Microsoft.AspNetCore.Mvc.Bind(Prefix = "models")] IEnumerable<WellAI.Advisor.Model.ServiceCompany.Models.DispatchDashboardModel> products)
        {

            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    //productService.Update(product);
                    //product.DriverStatus = pr
                }
            }
            return Json(products.ToDataSourceResult(request, ModelState));
        }


        public class StatusViewModel
        {
            public string? DriverStatus { get; set; }
            public string? StatusName { get; set; }
        }

        public class AuctionBidStatusViewModel
        {
            public string? AuctionBidStatusName { get; set; }
            public string? AuctionBidStatus { get; set; }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<bool> deletedispatchrouts_V2([FromBody] string userId)
        {
            try
            {
                var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrlDelete"];
                string Url = "";

                DispatchDelete Deleteid = new DispatchDelete();
                Deleteid.user_key = userId;

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl;
                    var stringContent = new StringContent(JsonConvert.SerializeObject(Deleteid), Encoding.UTF8, "application/json");
                    response = await client.PostAsync(Url, stringContent).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {

                        if (!string.IsNullOrEmpty(userId))
                        {
                            var itemToDelete = (from Ds in db.DispatchRoutes
                                                where Ds.UserId == userId
                                                select Ds).ToList();
                            if (itemToDelete != null)
                            {
                                db.DispatchRoutes.RemoveRange(itemToDelete);
                                await db.SaveChangesAsync();
                            }

                            var userstatus = (from Ds in db.Users
                                              where Ds.Id == userId
                                              select Ds).SingleOrDefault();

                            if (userstatus != null)
                            {
                                userstatus.UserRouteStatus = "ACTIVE";
                                await db.SaveChangesAsync();
                            }
                        }


                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch DeleteDispatchUser", User.Identity.Name);
                return true;
            }

        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> GetSimulationMap([FromBody] destinationssimulationrequest destinations)
        {
            //userdestinations destinations = new userdestinations();
            CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            string Url = "";
            userdestinations destination = new userdestinations();
            MapSimulationObject simulationObject = new MapSimulationObject();
            try
            {
                if (destinations == null)
                {
                    return Json(destination);
                }


                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "/simulator";
                    //var stringContent = new StringContent(JsonConvert.SerializeObject(destinations), Encoding.UTF8, "application/json");

                    var formVariables = new List<KeyValuePair<string, string>>();
                    formVariables.Add(new KeyValuePair<string, string>("origin", destinations.origin));

                    for (int i = 0; i <= 4; i++)
                    {
                        if (i < destinations.destinationsarray.Count)
                        {
                            formVariables.Add(new KeyValuePair<string, string>("destinations[" + i + "][type]", destinations.destinationsarray[i].type));
                            formVariables.Add(new KeyValuePair<string, string>("destinations[" + i + "][id]", destinations.destinationsarray[i].id));
                            if (destinations.destinationsarray[i].type == "location")
                            {
                                formVariables.Add(new KeyValuePair<string, string>("destinations[" + i + "][address]", destinations.destinationsarray[i].id));

                            }
                            else
                            {
                                formVariables.Add(new KeyValuePair<string, string>("destinations[" + i + "][id]", destinations.destinationsarray[i].id));

                            }
                        }
                    }
                    formVariables.Add(new KeyValuePair<string, string>("priority", "travel_time"));

                    var formContent = new FormUrlEncodedContent(formVariables);

                    response = await client.PostAsync(Url, formContent).ConfigureAwait(true);

                    if (response.IsSuccessStatusCode)
                    {
                        // locationData = JsonConvert.DeserializeObject<UserCurrentLocation>(response.Content.ReadAsStringAsync().Result);
                        string mycontent = response.Content.ReadAsStringAsync().Result;
                        //locations = JsonConvert.DeserializeObject<UserCurrentLocation[]>(mycontent).ToList();
                        simulationObject = JsonConvert.DeserializeObject<MapSimulationObject>(mycontent);
                    }
                    //return Json(locations);
                }

                return Json(simulationObject);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetSimulationMap", User.Identity.Name);
                return Json(simulationObject);

            }
        }


     

        /// <summary>
        /// Share Driver Location to Operator
        /// </summary>
        /// <param name="Userdetails"></param>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult> ShareDriverLocationToOperator([FromBody] OperatorWithServiceDriverInfo operatorAndDriverInfo)
        {
            bool userStatus = false;
            try
            {
                var commonBusiness1 = new CommonBusiness(db, _roleManager, _userManager);
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var serviceUser = await _userManager.FindByIdAsync(operatorAndDriverInfo.userId);
                var serviceCompany = await commonBusiness.GetCorporateProfileByUserId(operatorAndDriverInfo.userId);

                var operatorTenant = await commonBusiness.GetCorporateProfileByTenant(operatorAndDriverInfo.operatorId);
                var operatorUser = await _userManager.FindByIdAsync(operatorAndDriverInfo.userId);

                var activityId = operatorAndDriverInfo.activityId ?? 0;

                if (ModelState.IsValid)
                {
                    EmailHandler emailHandler = new EmailHandler();
                    //  var callbackUrl = "https://" + Request.Host.Value + "/ProviderLocator/" + Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToString(activityId))); //Url.Page("Account/ResetPassword", pageHandler: null, values: new { area = "Identity", code },
                    var callbackUrl = "https://" + Request.Host.Value + "/ProviderLocator/" + Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToString(operatorAndDriverInfo.userId))); //Url.Page("Account/ResetPassword", pageHandler: null, values: new { area = "Identity", code },

                    //dynamic email 
                    // var flagStatus = await emailHandler.SendEmailAsync(operatorUser.UserName, operatorUser.Email, "Well AI - Service Provider Driver Location", $"Dear Operator, <br> Please click on  <a href='" + callbackUrl + "'>link</a> to see the driver's current location. Thanks, <br> " + serviceCompany.Name);
                    //tetsing email 
                     var flagStatus = await emailHandler.SendEmailAsync(operatorUser.UserName, "tuoperatortest@yopmail.com", "Well AI - Service Provider Driver Location", $"Dear Operator, <br> Please click on  <a href='" + callbackUrl + "'>link</a> to see the driver's current location. Thanks. <br> Note: Make sure you login to Well AI Advisor.<br>Regars,<br>" + serviceCompany.Name);
                 //   var flagStatus = await emailHandler.SendEmailAsync(operatorUser.UserName, "murugesan.c@techunity.com", "Well AI - Service Provider Driver Location", $"Dear Operator, <br> Please click on  <a href='" + callbackUrl + "'>link</a> to see the driver's current location. Thanks. <br> Note: Make sure you login to Well AI Advisor.<br>Regars,<br>" + serviceCompany.Name);

                    //update operator id at DispatchRoutes
                    if (flagStatus == true)
                    {
                        List<DispatchRoutes> objDispatch = new List<DispatchRoutes>();


                        DispatchRoutes route = new DispatchRoutes();
                        route.OperatorId = operatorAndDriverInfo.operatorId;
                        route.UserId = operatorAndDriverInfo.userId;
                        route.ActivityId = operatorAndDriverInfo.activityId;
                        route.IsLocationShared = true;
                        objDispatch.Add(route);
                        var status = await commonBusiness1.UpdateOperatorId(objDispatch);


                    }


                    userStatus = true;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch Service ShareDriverLocationToOperator", User.Identity.Name);
            }
            return Json(userStatus);
        }

    }
}
