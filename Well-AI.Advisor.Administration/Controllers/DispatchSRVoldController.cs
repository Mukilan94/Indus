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

namespace Well_AI.Advisor.Administration.Controllers
{
    public class DispatchSRVoldController : BaseController
    {
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
       
        private TenantOperatingDbContext _tdbContext;
        private readonly TenantOperatingDbContext _operdb;
        private ISession _session;
        private TenantServiceDbContext _servicedb;
        public DispatchSRVoldController(IConfiguration _configuration, UserManager<WellIdentityUser> userManager,
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

        public IActionResult LoadAdvisorUsers()
        {
            return PartialView("_AdvisorUsers");
        }


        public IActionResult LoadAssignDispatch(string userId)
        {
            ViewBag.UserId = userId;

            return PartialView("_AssignDispatch");
        }

        public IActionResult LoadDispatchNotes()
        {

            return PartialView("_DispatchNotes");
        }
        public async Task<IActionResult> GetDispatchList([DataSourceRequest] DataSourceRequest request, string userId)
        {
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            try
            {
                string tenantId = HttpContext.Session.GetString("AdminSessionCurrentTenantId");
                CommonBusiness objComBusiness = new CommonBusiness(db, _roleManager, _userManager);
                result =  objComBusiness.GetDispatchRoutes(userId, false).Result;
            }
            catch (System.Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchList", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
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
        public ActionResult AdvisorUsers_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                string tenantId = HttpContext.Session.GetString("AdminSessionCurrentTenantId");

                var result = _userManager.Users.Where(x => x.TenantId == tenantId && (x.IsDispatchUser != true)).ToList();
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
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

        [HttpGet]
        public async Task<ActionResult> DispatchRoutes_InitialRead([DataSourceRequest] DataSourceRequest request)
        {
            List<DispatchRoutesModel> routes = new List<DispatchRoutesModel>();
            return Json(  routes.ToDataSourceResult(request));
        }
        [HttpPost]
       
        public async Task<ActionResult> DispatchRoutes_Read(string user)
        {
            ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
            List<DispatchRoutesModel> routes = new List<DispatchRoutesModel>();
            List<GeoLocations> locationsList = new List<GeoLocations>();
            try
            {
                routes = await commonBusiness.GetDispatchRoutes(user, false);
                locationsList = (from wr in routes
                                 select new GeoLocations
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

        [HttpGet]
        public async Task<JsonResult> GetRigDetailsByNameAsync(string text)
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
                    response = await client.GetAsync(Url + text).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        rigResults = JsonConvert.DeserializeObject<RigApiData>(result);
                    }
                }
                if (rigResults.results != null)
                {
                    var rigDataResult = rigResults.results;


                    rigDataResult = rigDataResult.Where(ap => ap.rig.Contains(text)).ToList();

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
               
                return Json(rigData);
            }
        }

        [HttpPost]
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

        [HttpGet]
        public async Task<JsonResult> GetUserLocationData(string userId)
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
        [HttpGet]
        public async Task<JsonResult> GetUserCurrentLocation(string userId)
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

        [AcceptVerbs("Post")]
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
                customErrorHandler.WriteError(ex, "DispatchDataDestroy", User.Identity.Name);
             
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(null);
        }

        [HttpPost]
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
                var notifystatus = await SendDispatchRoutes(dispatch.userid, dispatch.dispatchnotes);
                return status;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Dispatch UpdateDispatchUser", User.Identity.Name);
                return true;
            }
          
        }

        [HttpGet]
        public async Task<JsonResult> GetUserCurrentRoutes(string userId)
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

        [HttpGet]
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
        [HttpPost]
        public async Task<bool> user_destinations([FromBody] DispatchNotification notification)
        {
            Console.WriteLine(notification.user_key.ToString());
            return true;
        }

        [HttpPost]
        public async Task<bool> RefreshRoutes()
        {
            try
            {
                string tenantId = HttpContext.Session.GetString("AdminSessionCurrentTenantId");
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
        [HttpGet]
        public async Task<JsonResult> GetUserDestinations(string userId)
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

        [HttpGet]
        public async Task<JsonResult> GetWellDetailsByApiNumberAsync(string text, string filterType)
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
                IDispatchRepository dispatchRepo = new DispatchRepository(db, _roleManager, _userManager, _configuration);
                ResResult = await dispatchRepo.GetWellDetailsByApiNumberAsync(text, filterType);
                if (ResResult.message != null && text != null)
                {
                    TempData["Error"] = ResResult.message;
                    return Json(WellData);
                }
                else
                {
                    WellData = ResResult.results;
                    return Json(WellData);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetWellDetailsByApiNumberAsync", User.Identity.Name);               
                return Json(WellData);
            }
        }

        public IActionResult LoadUserRoutes(string userId)
        {
            ViewBag.UserId = userId;

            return PartialView("_UserRoutes");
        }

        [HttpPost]
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
        public async Task<JsonResult> GetAllUserCurrentRoutes(string userId)
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


        [HttpGet]
        public async Task<JsonResult> GetUserRoutes()
        {

            var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
            //UserRoutes userLocations = new UserRoutes();
            UserCurrentLocation locationData = new UserCurrentLocation();
            string Url = "";
            List<UserCurrentLocation> locations = new List<UserCurrentLocation>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                   var result = db.DispatchRoutes
                     .Select(x => x.UserId).Distinct().ToArray();

                     var UserIds = string.Join(",", result);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "users_location/";
                    response = await client.GetAsync(Url + UserIds  ).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        // locationData = JsonConvert.DeserializeObject<UserCurrentLocation>(response.Content.ReadAsStringAsync().Result);
                        string mycontent = response.Content.ReadAsStringAsync().Result;
                        locations = JsonConvert.DeserializeObject<UserCurrentLocation[]>(mycontent).ToList();


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
                                db.SaveChangesAsync();
                            }
                        }

                    }

                    //if (userLocations.result == "success")
                    //{
                    //    locationData = userLocations.location;

                    //}
                    //else if (userLocations.result == "failure")
                    //{
                    //    locationData.message = userLocations.message;
                    //}
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

    }
}
