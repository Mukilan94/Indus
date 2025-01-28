using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.IRepository;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
//using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.DLL.Repository
{
    public class DispatchRepository:IDispatchRepository
    {
        private readonly WebAIAdvisorContext db;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public DispatchRepository(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, IConfiguration configuration)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<UserRoutes> GetUserCurrentRoutes(string userId,string dispatchApiUrl)
        {
            string result = "";
            var GetUrl = dispatchApiUrl;
            UserRoutes userLocations = new UserRoutes();
            location locationData = new location();
            string Url = "";
            try
            {
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
                        //locationData.message = "success";
                    }
                    else if (userLocations.result == "failure")
                    {
                        locationData.message = userLocations.message;
                    }
                }
                return userLocations;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetRoleList", null);
                return userLocations;
            }
        }

      

            public async Task<bool> UpdateUserRoutes(string userId, UserRoutes routes)
        {
            try
            {
                ICommonRepository common = new CommonRepository(db, _roleManager, _userManager);
                //Delete all routes from DispatchRoutes table for the user

                //    db.DispatchRoutes.RemoveRange(currentRoutes);
                // await db.SaveChangesAsync();


                var currentRoutes2 = db.DispatchRoutes.Where(x => x.UserId == userId && x.ScheduledArrival == null && x.RouteOrder != 0 && x.OperatorId == null).ToList();
                db.DispatchRoutes.RemoveRange(currentRoutes2);
                await db.SaveChangesAsync();
                var currentRoutes = db.DispatchRoutes.Where(x => x.UserId == userId && x.RecordStatus == "0").ToList();

                string notes = routes.dispatch_instructions;

                var userResult = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                if (userResult.UsersOptions != null)
                {
                    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                    if (notes != "")
                    {
                        if(userOption.CurrentStatus=="1")
                        {
                            db.DispatchRoutes.RemoveRange(currentRoutes);
                              await db.SaveChangesAsync();
                        }

                        userOption.DispatchNotes = notes;
                        userOption.IsDispatchUser = true;
                        userOption.CurrentStatus = "0";//0-Empty,1-Destinations Changing,2-Waiting For Approval (if Destinations sent to Driver)
                    }
                    userResult.UsersOptions = JsonConvert.SerializeObject(userOption);
                    await db.SaveChangesAsync();
                }

                //for (var tbl = 0; tbl < currentRoutes.Count(); tbl++)
                //{
                //    bool recstatus = false;
                //    if (routes.destinations != null)
                //    {
                //        if (routes.destinations.Length > 0)
                //        {
                //            foreach (var route in routes.destinations)
                //            {
                //                if (route.activity_id.ToString().Trim() == currentRoutes[tbl].ActivityId.ToString().Trim())
                //                {
                //                    recstatus = true;
                //                }
                //            }
                //            if (recstatus == false)
                //            {
                //                db.DispatchRoutes.RemoveRange(currentRoutes[tbl]);
                //                await db.SaveChangesAsync();
                //            }

                //        }
                //    }

                //}

                if (routes != null)
                {
                    if (routes.destinations != null)
                    {
                        if (routes.destinations.Length > 0)
                        {
                            int i = 0;
                            List<DispatchRoutes> rst ;
                            foreach (var route in routes.destinations)
                            {
                                //check well name
                                rst = db.DispatchRoutes.Where(x => x.UserId == userId && x.WellName == route.destination.Replace("Well: ", "")
                                && x.WellId == route.well_id).ToList();

                                if (rst.Count() == 0)
                                {
                                    rst = db.DispatchRoutes.Where(x => x.UserId == userId && x.RigName == route.destination.Replace("Rig: ", "")
                                     && x.RigId == route.rig_id).ToList();

                                    if (rst.Count() == 0)
                                    {

                                        rst = db.DispatchRoutes.Where(x => x.UserId == userId && x.LocationName == route.destination).ToList();


                                        if (rst.Count() == 0)
                                        {

                                            //   rst = db.DispatchRoutes.Where(x => x.UserId == userId && ( Convert.ToString( x.Latitude) +',' Convert.ToString() == route.destination.Replace("Coordinattes: ", "").Trim()).ToList();

                                            //    rst = db.DispatchRoutes.Where(x => x.UserId == userId && (Convert.ToString(x.Latitude) + ','+ Convert.ToString(x.Longitude))== route.destination.Replace("Coordinattes: ", "").Trim()).ToList();

                                            rst = db.DispatchRoutes.Where(x => x.UserId == userId && (x.Latitude + ',' + x.Longitude).ToString()
                                   == route.destination.Replace("Coordinattes: ", "").Trim()).ToList();

                                            if (rst.Count() == 0)
                                            {

                                                rst = db.DispatchRoutes.Where(x => x.UserId == userId && (x.ActivityId).ToString().Trim() == route.activity_id.ToString().Trim()).ToList();

                                            }


                                        }

                                        }



                                }     

                                //var rst1 = db.DispatchRoutes.Where(x => x.UserId == userId && x.Latitude == route.destination_coordinates.latitude &&
                                //x.Longitude == route.destination_coordinates.longitude && x.LocationName == route.destination
                                //&& x.WellId == route.well_id && x.RigId == route.rig_id
                                //&& x.WellName == route.destination.Replace("Rig: ", "") && x.RigName == route.destination.Replace("Rig: ", "")
                                //&& x.RouteOrder == 0).ToList();


                                //-------
                                if (rst.Count() == 0)
                                {
                                    //Add the routes from userRoutes to DispatchRoutes table
                                    DispatchRoutes objDispatch = new DispatchRoutes();

                                    if (route.well_id == "0" && route.rig_id == "0")
                                    {
                                        objDispatch.LocationName = route.destination;
                                        objDispatch.RigId = "0";
                                        objDispatch.WellId = "0";
                                        objDispatch.RigName = "";
                                        objDispatch.WellName = "";
                                    }
                                    if (route.well_id != "0")
                                    {
                                        objDispatch.WellId = route.well_id;
                                        objDispatch.WellName = route.destination.Replace("Well: ", "");
                                        objDispatch.RigId = "0";
                                        objDispatch.RigName = "";
                                        objDispatch.LocationName = route.destination;
                                    }
                                    if (route.rig_id != "0")
                                    {
                                        objDispatch.RigId = route.rig_id;
                                        objDispatch.RigName = route.destination.Replace("Rig: ", "");
                                        objDispatch.WellId = "0";
                                        objDispatch.WellName = "";
                                        objDispatch.LocationName = route.destination;
                                    }

                                    objDispatch.LocationCity = "";
                                    objDispatch.LocationState = "";
                                    objDispatch.LocationZip = "";
                                    objDispatch.ActivityId = route.activity_id;
                                    objDispatch.Latitude = route.destination_coordinates.latitude;
                                    objDispatch.Longitude = route.destination_coordinates.longitude;
                                    //objDispatch.DispatchNotes = "";
                                    objDispatch.RouteOrder = i + 1;
                                    objDispatch.UserId = userId;
                                    objDispatch.CreatedDate = routes.location.logged_dt;
                                    objDispatch.Customer = "";
                                    objDispatch.APINumber = "";
                                 
                                    if (route.eta_timestamp_UT != null )
                                    {
                                        objDispatch.ETA = route.eta_timestamp_UT;
                                    }
                                    else
                                    {
                                        objDispatch.ETA =  null;
                                    }
                                    
                                    //objDispatch.WellName = (routes.location.activity.well_id != "0" && routes.location.activity.well_id != "") ? routes.location.activity.destination : "";
                                    //objDispatch.RigName = (routes.location.activity.rig_id != "0" && routes.location.activity.rig_id != "") ? routes.location.activity.destination : "";
                                    objDispatch.CurrentRouterOrder = i + 1;
                                    //objDispatch.RigId = routes.location.activity.rig_id;
                                    common.CreateDispatchRoute_RefreshRout(objDispatch);
                                    /// common.CreateDispatchRoute(objDispatch);
                                   

                                }
                                else
                                {

                                 var   rst2 = db.DispatchRoutes.Where(x => x.UserId == userId && x.ActivityId.ToString().Trim() == route.activity_id.ToString().Trim()).ToList();

                                    if (rst2.Count() == 0)
                                    {

                                        db.DispatchRoutes.RemoveRange(rst);
                                        await db.SaveChangesAsync();

                                        //Add the routes from userRoutes to DispatchRoutes table
                                        DispatchRoutes objDispatch = new DispatchRoutes();

                                        if (route.well_id == "0" && route.rig_id == "0")
                                        {
                                            objDispatch.LocationName = route.destination;
                                            objDispatch.RigId = "0";
                                            objDispatch.WellId = "0";
                                            objDispatch.RigName = "";
                                            objDispatch.WellName = "";
                                        }
                                        if (route.well_id != "0")
                                        {
                                            objDispatch.WellId = route.well_id;
                                            objDispatch.WellName = route.destination.Replace("Well: ", "");
                                            objDispatch.RigId = "0";
                                            objDispatch.RigName = "";
                                            objDispatch.LocationName = route.destination;
                                        }
                                        if (route.rig_id != "0")
                                        {
                                            objDispatch.RigId = route.rig_id;
                                            objDispatch.RigName = route.destination.Replace("Rig: ", "");
                                            objDispatch.WellId = "0";
                                            objDispatch.WellName = "";
                                            objDispatch.LocationName = route.destination;
                                        }

                                        objDispatch.LocationCity = "";
                                        objDispatch.LocationState = "";
                                        objDispatch.LocationZip = "";
                                        objDispatch.ActivityId = route.activity_id;
                                        objDispatch.Latitude = route.destination_coordinates.latitude;
                                        objDispatch.Longitude = route.destination_coordinates.longitude;
                                        //objDispatch.DispatchNotes = "";
                                        objDispatch.RouteOrder = i + 1;
                                        objDispatch.UserId = userId;
                                        objDispatch.CreatedDate = routes.location.logged_dt;
                                        objDispatch.Customer = "";
                                        objDispatch.APINumber = "";
                                        if (route.eta_timestamp_UT != null)
                                        {
                                            objDispatch.ETA = route.eta_timestamp_UT;
                                        }
                                        else
                                        {
                                            objDispatch.ETA = null;
                                        }
                                        //objDispatch.WellName = (routes.location.activity.well_id != "0" && routes.location.activity.well_id != "") ? routes.location.activity.destination : "";
                                        //objDispatch.RigName = (routes.location.activity.rig_id != "0" && routes.location.activity.rig_id != "") ? routes.location.activity.destination : "";
                                        objDispatch.CurrentRouterOrder = i + 1;
                                        //objDispatch.RigId = routes.location.activity.rig_id;
                                        //  common.CreateDispatchRoute_RefreshRout_Update(objDispatch);
                                        common.CreateDispatchRoute_RefreshRout(objDispatch);
                                        /// common.CreateDispatchRoute(objDispatch);

                                    }
                                    else
                                    {

                                    }



                                }



                                i++;


                            }
                        }
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "DispatchReporsitory UpdateUserRoutes", null);
                return true;
            }
        }


        public async Task<bool> UpdateUserRoutes_old(string userId, UserRoutes routes)
        {
            try
            {
                ICommonRepository common = new CommonRepository(db, _roleManager, _userManager);
                //Delete all routes from DispatchRoutes table for the user
              
                //    db.DispatchRoutes.RemoveRange(currentRoutes);
                // await db.SaveChangesAsync();


                var currentRoutes2 = db.DispatchRoutes.Where(x => x.UserId == userId &&  x.ScheduledArrival == null).ToList();
                db.DispatchRoutes.RemoveRange(currentRoutes2);
                await db.SaveChangesAsync();
                var currentRoutes = db.DispatchRoutes.Where(x => x.UserId == userId).ToList();


                string notes = routes.dispatch_instructions;

                var userResult = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                if (userResult.UsersOptions != null)
                {
                    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                    if (notes != "")
                    {
                        userOption.DispatchNotes = notes;
                        userOption.IsDispatchUser = true;
                        userOption.CurrentStatus = "0";//0-Empty,1-Destinations Changing,2-Waiting For Approval (if Destinations sent to Driver)
                    }
                    userResult.UsersOptions = JsonConvert.SerializeObject(userOption);
                    await db.SaveChangesAsync();
                }

                for (var tbl = 0; tbl < currentRoutes.Count(); tbl++)
                {
                    bool recstatus = false;
                    if (routes.destinations != null)
                    {
                        if (routes.destinations.Length > 0)
                        {
                           
                            foreach (var route in routes.destinations)
                            {
                                if(route.activity_id.ToString().Trim() == currentRoutes[tbl].ActivityId.ToString().Trim())
                                {
                                    recstatus = true;
                                }
                            }
                            if(recstatus==false)
                            {
                                db.DispatchRoutes.RemoveRange(currentRoutes[tbl]);
                                await db.SaveChangesAsync();
                            }
                           
                        }
                    }
                  
                }
                            if (routes != null)
                {
                    if (routes.destinations != null)
                    {
                        if (routes.destinations.Length > 0)
                        {
                            int i = 0;
                            foreach (var route in routes.destinations)
                            {
                                //Add the routes from userRoutes to DispatchRoutes table
                                DispatchRoutes objDispatch = new DispatchRoutes();

                                if (route.well_id == "0" && route.rig_id == "0")
                                {
                                    objDispatch.LocationName = route.destination;
                                    objDispatch.RigId = "0";
                                    objDispatch.WellId = "0";
                                    objDispatch.RigName = "";
                                    objDispatch.WellName = "";
                                }
                                if (route.well_id != "0")
                                {
                                    objDispatch.WellId = route.well_id;
                                    objDispatch.WellName = route.destination.Replace("Well: ", "");
                                    objDispatch.RigId = "0";
                                    objDispatch.RigName = "";
                                    objDispatch.LocationName = route.destination;                                    
                                }
                                if (route.rig_id != "0")
                                {
                                    objDispatch.RigId = route.rig_id;
                                    objDispatch.RigName = route.destination.Replace("Rig: ", "");
                                    objDispatch.WellId = "0";
                                    objDispatch.WellName = "";
                                    objDispatch.LocationName = route.destination;                                    
                                }

                                objDispatch.LocationCity = "";
                                objDispatch.LocationState = "";
                                objDispatch.LocationZip = "";
                                objDispatch.ActivityId  = route.activity_id;
                                objDispatch.Latitude = route.destination_coordinates.latitude;
                                objDispatch.Longitude = route.destination_coordinates.longitude;
                                //objDispatch.DispatchNotes = "";
                                objDispatch.RouteOrder = i + 1;
                                objDispatch.UserId = userId;
                                objDispatch.CreatedDate = routes.location.logged_dt;
                                objDispatch.Customer = "";
                                objDispatch.APINumber = "";
                              
                                //objDispatch.WellName = (routes.location.activity.well_id != "0" && routes.location.activity.well_id != "") ? routes.location.activity.destination : "";
                                //objDispatch.RigName = (routes.location.activity.rig_id != "0" && routes.location.activity.rig_id != "") ? routes.location.activity.destination : "";
                                objDispatch.CurrentRouterOrder = i + 1;
                                //objDispatch.RigId = routes.location.activity.rig_id;
                                common.CreateDispatchRoute_RefreshRout(objDispatch);
                                /// common.CreateDispatchRoute(objDispatch);
                                i++;
                            }
                        }
                    }
                   
                }
                
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "DispatchReporsitory UpdateUserRoutes", null);
                return true;
            }
        }


        public async Task<WellApiData> GetWellDetailsByApiNumberAsync(string text, string filterType)
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
            WellApiData ResResult = new WellApiData();
            try
            {
                
                var GetUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
                Result data = new Result();

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

                    
                }
                return ResResult;

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "DispatchReporsitory UpdateUserRoutes", null);
                return ResResult;
            }
        }

        public async Task<UserRoutes> GetUserRoutes(string dispatchApiUrl)
        {
       
            var GetUrl = dispatchApiUrl;
            UserRoutes userLocations = new UserRoutes();
            location locationData = new location();
            string Url = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = new HttpResponseMessage();
                    Url = GetUrl + "user_location/";
                    //response = await client.GetAsync(Url + userId + "/1").ConfigureAwait(true);
                    response = await client.GetAsync(Url).ConfigureAwait(true);
                    if (response.IsSuccessStatusCode)
                    {
                        userLocations = JsonConvert.DeserializeObject<UserRoutes>(response.Content.ReadAsStringAsync().Result);
                    }

                    if (userLocations.result == "success")
                    {
                        locationData = userLocations.location;
                        //locationData.message = "success";
                    }
                    else if (userLocations.result == "failure")
                    {
                        locationData.message = userLocations.message;
                    }
                }
                return userLocations;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetRoleList", null);
                return userLocations;
            }
        }

        public List<DispatchRoutesViewModel> GetDispatchDetailsList(string TenantId)
        {
            DispatchRoutesViewModel dispatchRoutes = new DispatchRoutesViewModel();
            IDispatchRepository objDispatchBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            //  DispatchRoutesViewModel model = new DispatchRoutesViewModel();
            List<DispatchRoutesViewModel> driverStatus = new List<DispatchRoutesViewModel>();
            //  var result = objDispatchBusiness.GetDispatchDetailsList("b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90");
            try
            {

                List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();
                var users = (from u in _userManager.Users
                             join tu in db.TenantUsers on u.Id equals tu.UserId
                             where tu.TenantId == TenantId
                             select u).ToList();
                foreach (var user in users)
                {
                    UserViewSRVModel userViewModel = new UserViewSRVModel();

                    userViewModel.UserID = user.Id;
                    userViewModel.PhoneNumber = user.PhoneNumber;
                    userViewModel.Email = user.Email;
                    userViewModel.FirstName = user.FirstName ?? String.Empty;
                    userViewModel.MiddleName = user.MiddleName;
                    userViewModel.LastName = user.LastName;
                    userViewModel.IsPrimary = user.Primary.HasValue ? user.Primary.Value : false;
                    userViewModel.JobTitle = user.JobTitle;
                    userViewModel.Address = user.Address;
                    userViewModel.City = user.City;
                    userViewModel.AdditionalNotes = user.AdditionalNotes;
                    userViewModel.Zip = user.Zip;
                    userViewModel.Mobile = user.Mobile;
                    userViewModel.State = user.State;
                    userViewModel.UserName = String.Concat(user.FirstName ?? String.Empty, " ", user.LastName ?? String.Empty);


                    var userRoleNames = _userManager.GetRolesAsync(user).Result;
                    var tenantRoles = (from r in _roleManager.Roles
                                       join tr in db.TenantRoles on r.Id equals tr.RoleId
                                       where tr.TenantId == TenantId
                                       select r).ToList();

                    userViewModel.roles = new List<IdentityRole>();
                    userViewModel.SelectedRoles = "";

                    foreach (var tenantRole in tenantRoles)
                    {
                        if (userRoleNames.Contains(tenantRole.Name))
                        {
                            userViewModel.roles.Add(new IdentityRole { Id = tenantRole.Id, Name = tenantRole.Name });
                            userViewModel.SelectedRoles += tenantRole.Id + ";";
                        }
                    }


                    if (userRoleNames.Contains("Driver"))
                    {
                        userViewModel.IsActive = true;
                    }
                    else
                    {
                        userViewModel.IsActive = false;
                    }


                    userViewModel.ProfileImageName = user.ProfileImageName;
                    userViewModel.UserTenantId = user.TenantId;

                    userViewModelList.Add(userViewModel);
                }


                //var result = (from dr in db.DispatchRoutes
                //              join us in db.Users on dr.UserId equals us.Id

                //              // where dr.UserId == (userid) && dr.RouteOrder != 0  
                //              where (dr.RouteOrder != 0) && us.TenantId == (TenantId)
                //              orderby dr.RouteOrder ascending
                //              select new DispatchRoutesViewModel
                //              {
                //                  dispatchid = dr.DispatchId,
                //                  dispatchnotes = dr.DispatchNotes,
                //                  userid = dr.UserId,
                //                  customer = dr.Customer,
                //                  address = dr.LocationAddress,
                //                  locationname = dr.LocationName == "" ? dr.RigName == "" ? dr.WellName : dr.RigName : dr.LocationName,
                //                  city = dr.LocationCity,
                //                  state = dr.LocationState,
                //                  zip = dr.LocationZip,
                //                  routeorder = dr.RouteOrder,
                //                  username = us.FirstName + " " + us.LastName,
                //                  latitude = dr.Latitude,
                //                  longitude = dr.Longitude,
                //                  createddate = Convert.ToString(dr.CreatedDate),
                //                  api = dr.APINumber,
                //                  rigname = dr.RigName,
                //                  rigid = dr.RigId,
                //                  wellname = dr.WellName,
                //                  wellid = dr.WellId,
                //                  activityid = dr.ActivityId,
                //                  operatorId = dr.OperatorId,
                //                  routestatus = dr.RouteStatus ?? "ON-ROUTE",
                //                  scheduledArrivalDate = dr.ScheduledArrival,
                //                  eta = dr.ETA
                //              }
                //         ).ToList();

                //var _iViewModelList = userViewModelList;
                //var _Iresult = result;

                //var _iViewModelListCount = _iViewModelList.Count();
                //var _IresultCount = _Iresult.Count();

                //var _iViewModelListlist = _iViewModelList.ToList();
                //var _Iresultlist = _Iresult.ToList();

                //var result2 = from r2 in userViewModelList
                //              join r1 in result on r2.UserID equals r1.userid
                //              into r1obj
                //              from r3 in r1obj.DefaultIfEmpty()
                //              select new {  r2, r1obj };


                //foreach (var item in userViewModelList)
                //{

                //}

                List<DispatchRoutesViewModel> DispatchRoutesViewModelall = new List<DispatchRoutesViewModel>();

                //List<UserViewSRVModel> userViewModelListNew = new List<UserViewSRVModel>();
                //userViewModelListNew = (List<UserViewSRVModel>)userViewModelList.Where(x => x.IsActive == true);
                var userViewModelListNew = userViewModelList.Where(x => x.IsActive == true).ToList();



                for (int i = 0; userViewModelListNew.Count > i; i++)
                {

                    // var dispatch = result.Where(x => x.userid == userViewModelList[i].UserID);

                    UserViewSRVModel userViewModel = new UserViewSRVModel();
                    userViewModel.UserID = userViewModelListNew[i].UserID;
                    userViewModel.UserName = null;
                    userViewModel.JobTitle = null;
                    userViewModel.PhoneNumber = null;
                    userViewModel.Email = null;

                    //var dispatch1 = (from dr in db.DispatchRoutes
                    //                 join us in db.Users on dr.UserId equals us.Id

                    //                 // where dr.UserId == (userid) && dr.RouteOrder != 0  
                    //                 where (dr.RouteOrder != 0) && us.TenantId == (TenantId) &&
                    //                 dr.UserId == (userViewModelListNew[i].UserID)
                    //                 orderby dr.RouteOrder ascending
                    //                 select new DispatchRoutesViewModel
                    //                 {
                    //                     dispatchid = dr.DispatchId,
                    //                     dispatchnotes = dr.DispatchNotes,
                    //                     userid = dr.UserId,
                    //                     customer = dr.Customer,
                    //                     address = dr.LocationAddress,
                    //                     locationname = dr.LocationName == "" ? dr.RigName == "" ? dr.WellName : dr.RigName : dr.LocationName,
                    //                     city = dr.LocationCity,
                    //                     state = dr.LocationState,
                    //                     zip = dr.LocationZip,
                    //                     routeorder = dr.RouteOrder,
                    //                     username = us.FirstName + " " + us.LastName,
                    //                     latitude = dr.Latitude,
                    //                     longitude = dr.Longitude,
                    //                     createddate = Convert.ToString(dr.CreatedDate),
                    //                     api = dr.APINumber,
                    //                     rigname = dr.RigName,
                    //                     rigid = dr.RigId,
                    //                     wellname = dr.WellName,
                    //                     wellid = dr.WellId,
                    //                     activityid = dr.ActivityId,
                    //                     operatorId = dr.OperatorId,
                    //                     routestatus = dr.RouteStatus ?? "ON-ROUTE",
                    //                     scheduledArrivalDate = dr.ScheduledArrival,
                    //                     eta = dr.ETA
                    //                 }
                    //             ).Distinct().ToList();

                    var dispatch = (from us in db.Users    
                                    join dr in db.DispatchRoutes on us.Id equals dr.UserId into well
                                    from res in well.DefaultIfEmpty()
                                    where  us.TenantId == (TenantId) &&
                                    us.Id == (userViewModelListNew[i].UserID) && res.CurrentRouterOrder == '1'
                                    select new DispatchRoutesViewModel
                                    {
                                        userid = us.Id,
                                        //locationname = us.UserLocation,
                                        //locationname = res?.WellName ?? string.Empty,
                                        locationname = res.WellName ?? res.LocationName ,
                                        routestatus = us.UserRouteStatus ?? "ACTIVE",
                                        scheduledArrivalDate = us.UserScheduledArrival,
                                        eta = us.UserETA,
                                        username = us.FirstName + " " + us.LastName,
                                       dispatchnotes = "",
                                   }
                            ).Distinct().ToList();

                 
                    if (dispatch.Count != 0)
                    {
                        foreach (var item in dispatch)
                        {
                            DispatchRoutesViewModelall.Add(item);
                        }
                     //   DispatchRoutesViewModelall.Add(dispatch);
                    }
                    else
                    {
                        DispatchRoutesViewModel DispatchRoutesViewModelUseronly = new DispatchRoutesViewModel();
                        DispatchRoutesViewModelUseronly.dispatchid = "";
                        DispatchRoutesViewModelUseronly.dispatchnotes = "";
                        DispatchRoutesViewModelUseronly.userid = userViewModelListNew[i].UserID;
                        DispatchRoutesViewModelUseronly.customer = "";
                        DispatchRoutesViewModelUseronly.address = "";
                        DispatchRoutesViewModelUseronly.locationname = "";
                        DispatchRoutesViewModelUseronly.city = "";
                        DispatchRoutesViewModelUseronly.state = "";
                        DispatchRoutesViewModelUseronly.zip = "";
                        DispatchRoutesViewModelUseronly.routeorder = 0;
                        DispatchRoutesViewModelUseronly.username = userViewModelListNew[i].FirstName + " " + userViewModelListNew[i].LastName;
                        DispatchRoutesViewModelUseronly.latitude = 0;
                        DispatchRoutesViewModelUseronly.longitude = 0;
                        DispatchRoutesViewModelUseronly.createddate = "";
                        DispatchRoutesViewModelUseronly.api = "";
                        DispatchRoutesViewModelUseronly.rigname = "";
                        DispatchRoutesViewModelUseronly.rigid = "";
                        DispatchRoutesViewModelUseronly.wellname = "";
                        DispatchRoutesViewModelUseronly.wellid = "";
                        DispatchRoutesViewModelUseronly.activityid = 0;
                        DispatchRoutesViewModelUseronly.operatorId = "";
                        DispatchRoutesViewModelUseronly.routestatus = "ACTIVE";
                        DispatchRoutesViewModelUseronly.scheduledArrivalDate = null;
                        DispatchRoutesViewModelUseronly.eta = null;
                        DispatchRoutesViewModelall.Add(DispatchRoutesViewModelUseronly);
                    }


                }




                //var result = (from dr in db.DispatchRoutes
                //              join us in db.Users on dr.UserId equals us.Id

                //              // where dr.UserId == (userid) && dr.RouteOrder != 0  
                //              where (dr.RouteOrder != 0) && us.TenantId == (TenantId)
                //              orderby dr.RouteOrder ascending
                //              select new DispatchRoutesViewModel
                //              {
                //                  dispatchid = dr.DispatchId,
                //                  dispatchnotes = dr.DispatchNotes,
                //                  userid = dr.UserId,
                //                  customer = dr.Customer,
                //                  address = dr.LocationAddress,
                //                  locationname = dr.LocationName == "" ? dr.RigName == "" ? dr.WellName : dr.RigName : dr.LocationName,
                //                  city = dr.LocationCity,
                //                  state = dr.LocationState,
                //                  zip = dr.LocationZip,
                //                  routeorder = dr.RouteOrder,
                //                  username = us.FirstName + " " + us.LastName,
                //                  latitude = dr.Latitude,
                //                  longitude = dr.Longitude,
                //                  createddate = Convert.ToString(dr.CreatedDate),
                //                  api = dr.APINumber,
                //                  rigname = dr.RigName,
                //                  rigid = dr.RigId,
                //                  wellname = dr.WellName,
                //                  wellid = dr.WellId,
                //                  activityid = dr.ActivityId,
                //                  operatorId = dr.OperatorId,
                //                  routestatus = dr.RouteStatus ?? "ON-ROUTE",
                //                  scheduledArrivalDate = dr.ScheduledArrival,
                //                  eta = dr.ETA
                //              }
                //            ).ToList();


                //var dispatch = result.Where(x => x.UserId.ToString().ToUpper() == " b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90").FirstOrDefault();
                //model.DispatchId = dispatch.DispatchId;
                //model.WellName = dispatch.WellName;
                //model.RigName = dispatch.RigName;
                //model.DispatchNotes = dispatch.DispatchNotes;
                // driverStatus.Add(model);
                //DispatchRoutes.DispatchRoutesViewModel = dispatchRoutes;
                //return (driverStatus);
                return DispatchRoutesViewModelall;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchDetails", null);
                return null;
            }
        }


        public List<DispatchRoutesViewModel> GetDispatchDetailsList_active(string TenantId)
        {
            DispatchRoutesViewModel dispatchRoutes = new DispatchRoutesViewModel();
            IDispatchRepository objDispatchBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            //  DispatchRoutesViewModel model = new DispatchRoutesViewModel();
            List<DispatchRoutesViewModel> driverStatus = new List<DispatchRoutesViewModel>();
            //  var result = objDispatchBusiness.GetDispatchDetailsList("b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90");
            try
            {



                List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();
                var users = (from u in _userManager.Users
                             join tu in db.TenantUsers on u.Id equals tu.UserId
                             where tu.TenantId == TenantId
                             select u).ToList();
                foreach (var user in users)
                {
                    UserViewSRVModel userViewModel = new UserViewSRVModel();

                    userViewModel.UserID = user.Id;
                    userViewModel.PhoneNumber = user.PhoneNumber;
                    userViewModel.Email = user.Email;
                    userViewModel.FirstName = user.FirstName ?? String.Empty;
                    userViewModel.MiddleName = user.MiddleName;
                    userViewModel.LastName = user.LastName;
                    userViewModel.IsPrimary = user.Primary.HasValue ? user.Primary.Value : false;
                    userViewModel.JobTitle = user.JobTitle;
                    userViewModel.Address = user.Address;
                    userViewModel.City = user.City;
                    userViewModel.AdditionalNotes = user.AdditionalNotes;
                    userViewModel.Zip = user.Zip;
                    userViewModel.Mobile = user.Mobile;
                    userViewModel.State = user.State;
                    userViewModel.UserName = String.Concat(user.FirstName ?? String.Empty, " ", user.LastName ?? String.Empty);


                    var userRoleNames = _userManager.GetRolesAsync(user).Result;
                    var tenantRoles = (from r in _roleManager.Roles
                                       join tr in db.TenantRoles on r.Id equals tr.RoleId
                                       where tr.TenantId == TenantId
                                       select r).ToList();

                    userViewModel.roles = new List<IdentityRole>();
                    userViewModel.SelectedRoles = "";

                    foreach (var tenantRole in tenantRoles)
                    {
                        if (userRoleNames.Contains(tenantRole.Name))
                        {
                            userViewModel.roles.Add(new IdentityRole { Id = tenantRole.Id, Name = tenantRole.Name });
                            userViewModel.SelectedRoles += tenantRole.Id + ";";
                        }
                    }


                    if (userRoleNames.Contains("Driver"))
                    {
                        userViewModel.IsActive = true;
                    }
                    else
                    {
                        userViewModel.IsActive = false;
                    }


                    userViewModel.ProfileImageName = user.ProfileImageName;
                    userViewModel.UserTenantId = user.TenantId;

                    userViewModelList.Add(userViewModel);
                }


                //var result = (from dr in db.DispatchRoutes
                //              join us in db.Users on dr.UserId equals us.Id

                //              // where dr.UserId == (userid) && dr.RouteOrder != 0  
                //              where (dr.RouteOrder != 0) && us.TenantId == (TenantId)
                //              orderby dr.RouteOrder ascending
                //              select new DispatchRoutesViewModel
                //              {
                //                  dispatchid = dr.DispatchId,
                //                  dispatchnotes = dr.DispatchNotes,
                //                  userid = dr.UserId,
                //                  customer = dr.Customer,
                //                  address = dr.LocationAddress,
                //                  locationname = dr.LocationName == "" ? dr.RigName == "" ? dr.WellName : dr.RigName : dr.LocationName,
                //                  city = dr.LocationCity,
                //                  state = dr.LocationState,
                //                  zip = dr.LocationZip,
                //                  routeorder = dr.RouteOrder,
                //                  username = us.FirstName + " " + us.LastName,
                //                  latitude = dr.Latitude,
                //                  longitude = dr.Longitude,
                //                  createddate = Convert.ToString(dr.CreatedDate),
                //                  api = dr.APINumber,
                //                  rigname = dr.RigName,
                //                  rigid = dr.RigId,
                //                  wellname = dr.WellName,
                //                  wellid = dr.WellId,
                //                  activityid = dr.ActivityId,
                //                  operatorId = dr.OperatorId,
                //                  routestatus = dr.RouteStatus ?? "ON-ROUTE",
                //                  scheduledArrivalDate = dr.ScheduledArrival,
                //                  eta = dr.ETA
                //              }
                //         ).ToList();

                //var _iViewModelList = userViewModelList;
                //var _Iresult = result;

                //var _iViewModelListCount = _iViewModelList.Count();
                //var _IresultCount = _Iresult.Count();

                //var _iViewModelListlist = _iViewModelList.ToList();
                //var _Iresultlist = _Iresult.ToList();

                //var result2 = from r2 in userViewModelList
                //              join r1 in result on r2.UserID equals r1.userid
                //              into r1obj
                //              from r3 in r1obj.DefaultIfEmpty()
                //              select new {  r2, r1obj };


                //foreach (var item in userViewModelList)
                //{

                //}

                List<DispatchRoutesViewModel> DispatchRoutesViewModelall = new List<DispatchRoutesViewModel>();

                //List<UserViewSRVModel> userViewModelListNew = new List<UserViewSRVModel>();
                //userViewModelListNew = (List<UserViewSRVModel>)userViewModelList.Where(x => x.IsActive == true);
                var userViewModelListNew = userViewModelList.Where(x => x.IsActive == true).ToList();



                for (int i = 0; userViewModelListNew.Count > i; i++)
                {

                    // var dispatch = result.Where(x => x.userid == userViewModelList[i].UserID);

                    UserViewSRVModel userViewModel = new UserViewSRVModel();
                    userViewModel.UserID = userViewModelListNew[i].UserID;
                    userViewModel.UserName = null;
                    userViewModel.JobTitle = null;
                    userViewModel.PhoneNumber = null;
                    userViewModel.Email = null;


                    var dispatch = (from dr in db.DispatchRoutes
                                    join us in db.Users on dr.UserId equals us.Id

                                    // where dr.UserId == (userid) && dr.RouteOrder != 0  
                                    where (dr.RouteOrder != 0) && us.TenantId == (TenantId) &&
                                    dr.UserId == (userViewModelListNew[i].UserID) && dr.RouteStatus != "INACTIVE"
                                    orderby dr.RouteOrder ascending
                                    select new DispatchRoutesViewModel
                                    {
                                        dispatchid = dr.DispatchId,
                                        dispatchnotes = dr.DispatchNotes,
                                        userid = dr.UserId,
                                        customer = dr.Customer,
                                        address = dr.LocationAddress,
                                        locationname = dr.LocationName == "" ? dr.RigName == "" ? dr.WellName : dr.RigName : dr.LocationName,
                                        city = dr.LocationCity,
                                        state = dr.LocationState,
                                        zip = dr.LocationZip,
                                        routeorder = dr.RouteOrder,
                                        username = us.FirstName + " " + us.LastName,
                                        latitude = dr.Latitude,
                                        longitude = dr.Longitude,
                                        createddate = Convert.ToString(dr.CreatedDate),
                                        api = dr.APINumber,
                                        rigname = dr.RigName,
                                        rigid = dr.RigId,
                                        wellname = dr.WellName,
                                        wellid = dr.WellId,
                                        activityid = dr.ActivityId,
                                        operatorId = dr.OperatorId,
                                        routestatus = dr.RouteStatus ?? "ON-ROUTE",
                                        scheduledArrivalDate = dr.ScheduledArrival,
                                        eta = dr.ETA
                                    }
                                ).Distinct().ToList();

                    if (dispatch.Count != 0)
                    {
                        foreach (var item in dispatch)
                        {
                            DispatchRoutesViewModelall.Add(item);
                        }
                        //   DispatchRoutesViewModelall.Add(dispatch);
                    }
                    else
                    {
                        //DispatchRoutesViewModel DispatchRoutesViewModelUseronly = new DispatchRoutesViewModel();
                        //DispatchRoutesViewModelUseronly.dispatchid = "";
                        //DispatchRoutesViewModelUseronly.dispatchnotes = "";
                        //DispatchRoutesViewModelUseronly.userid = userViewModelListNew[i].UserID;
                        //DispatchRoutesViewModelUseronly.customer = "";
                        //DispatchRoutesViewModelUseronly.address = "";
                        //DispatchRoutesViewModelUseronly.locationname = "";
                        //DispatchRoutesViewModelUseronly.city = "";
                        //DispatchRoutesViewModelUseronly.state = "";
                        //DispatchRoutesViewModelUseronly.zip = "";
                        //DispatchRoutesViewModelUseronly.routeorder = 0;
                        //DispatchRoutesViewModelUseronly.username = userViewModelListNew[i].FirstName + " " + userViewModelListNew[i].LastName;
                        //DispatchRoutesViewModelUseronly.latitude = 0;
                        //DispatchRoutesViewModelUseronly.longitude = 0;
                        //DispatchRoutesViewModelUseronly.createddate = "";
                        //DispatchRoutesViewModelUseronly.api = "";
                        //DispatchRoutesViewModelUseronly.rigname = "";
                        //DispatchRoutesViewModelUseronly.rigid = "";
                        //DispatchRoutesViewModelUseronly.wellname = "";
                        //DispatchRoutesViewModelUseronly.wellid = "";
                        //DispatchRoutesViewModelUseronly.activityid = 0;
                        //DispatchRoutesViewModelUseronly.operatorId = "";
                        //DispatchRoutesViewModelUseronly.routestatus = "INACTIVE";
                        //DispatchRoutesViewModelUseronly.scheduledArrivalDate = null;
                        //DispatchRoutesViewModelUseronly.eta = null;
                        //DispatchRoutesViewModelall.Add(DispatchRoutesViewModelUseronly);
                    }

                }


                //var result = (from dr in db.DispatchRoutes
                //              join us in db.Users on dr.UserId equals us.Id

                //              // where dr.UserId == (userid) && dr.RouteOrder != 0  
                //              where (dr.RouteOrder != 0) && us.TenantId == (TenantId)
                //              orderby dr.RouteOrder ascending
                //              select new DispatchRoutesViewModel
                //              {
                //                  dispatchid = dr.DispatchId,
                //                  dispatchnotes = dr.DispatchNotes,
                //                  userid = dr.UserId,
                //                  customer = dr.Customer,
                //                  address = dr.LocationAddress,
                //                  locationname = dr.LocationName == "" ? dr.RigName == "" ? dr.WellName : dr.RigName : dr.LocationName,
                //                  city = dr.LocationCity,
                //                  state = dr.LocationState,
                //                  zip = dr.LocationZip,
                //                  routeorder = dr.RouteOrder,
                //                  username = us.FirstName + " " + us.LastName,
                //                  latitude = dr.Latitude,
                //                  longitude = dr.Longitude,
                //                  createddate = Convert.ToString(dr.CreatedDate),
                //                  api = dr.APINumber,
                //                  rigname = dr.RigName,
                //                  rigid = dr.RigId,
                //                  wellname = dr.WellName,
                //                  wellid = dr.WellId,
                //                  activityid = dr.ActivityId,
                //                  operatorId = dr.OperatorId,
                //                  routestatus = dr.RouteStatus ?? "ON-ROUTE",
                //                  scheduledArrivalDate = dr.ScheduledArrival,
                //                  eta = dr.ETA
                //              }
                //            ).ToList();


                //var dispatch = result.Where(x => x.UserId.ToString().ToUpper() == " b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90").FirstOrDefault();
                //model.DispatchId = dispatch.DispatchId;
                //model.WellName = dispatch.WellName;
                //model.RigName = dispatch.RigName;
                //model.DispatchNotes = dispatch.DispatchNotes;
                // driverStatus.Add(model);
                //DispatchRoutes.DispatchRoutesViewModel = dispatchRoutes;
                //return (driverStatus);
                return DispatchRoutesViewModelall;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchDetails", null);
                return null;
            }
        }

        public List<DispatchRoutesViewModel> GetDispatchDetailsListold2(string TenantId)
        {
            DispatchRoutesViewModel dispatchRoutes = new DispatchRoutesViewModel();
            IDispatchRepository objDispatchBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
            //  DispatchRoutesViewModel model = new DispatchRoutesViewModel();
            List<DispatchRoutesViewModel> driverStatus = new List<DispatchRoutesViewModel>();
            //  var result = objDispatchBusiness.GetDispatchDetailsList("b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90");
            try
            {

                List<UserViewSRVModel> userViewModelList22 = new List<UserViewSRVModel>();

                var result = (from dr in db.DispatchRoutes
                              join us in db.Users on dr.UserId equals us.Id

                              // where dr.UserId == (userid) && dr.RouteOrder != 0  
                              where (dr.RouteOrder != 0) && us.TenantId == (TenantId)
                              orderby dr.RouteOrder ascending
                              select new DispatchRoutesViewModel
                              {
                                  dispatchid = dr.DispatchId,
                                  dispatchnotes = dr.DispatchNotes,
                                  userid = dr.UserId,
                                  customer = dr.Customer,
                                  address = dr.LocationAddress,
                                  locationname = dr.LocationName == "" ? dr.RigName == "" ? dr.WellName : dr.RigName : dr.LocationName,
                                  city = dr.LocationCity,
                                  state = dr.LocationState,
                                  zip = dr.LocationZip,
                                  routeorder = dr.RouteOrder,
                                  username = us.FirstName + " " + us.LastName,
                                  latitude = dr.Latitude,
                                  longitude = dr.Longitude,
                                  createddate = Convert.ToString(dr.CreatedDate),
                                  api = dr.APINumber,
                                  rigname = dr.RigName,
                                  rigid = dr.RigId,
                                  wellname = dr.WellName,
                                  wellid = dr.WellId,
                                  activityid = dr.ActivityId,
                                  operatorId = dr.OperatorId,
                                  routestatus = dr.RouteStatus ?? "ON-ROUTE",
                                  scheduledArrivalDate = dr.ScheduledArrival,
                                  eta = dr.ETA
                              }
                            ).ToList();


                //var dispatch = result.Where(x => x.UserId.ToString().ToUpper() == " b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90").FirstOrDefault();
                //model.DispatchId = dispatch.DispatchId;
                //model.WellName = dispatch.WellName;
                //model.RigName = dispatch.RigName;
                //model.DispatchNotes = dispatch.DispatchNotes;
                // driverStatus.Add(model);
                //DispatchRoutes.DispatchRoutesViewModel = dispatchRoutes;
                //return (driverStatus);
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchDetails", null);
                return null;
            }
        }

        //public List<DispatchRoutesViewModel> GetDispatchDetailsList(string TenantId)
        //{
        //    DispatchRoutesViewModel dispatchRoutes = new DispatchRoutesViewModel();
        //    IDispatchRepository objDispatchBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
        //    DispatchRoutesViewModel model = new DispatchRoutesViewModel();
        //    List<DispatchRoutesViewModel> driverStatus = new List<DispatchRoutesViewModel>();
        //    var result = objDispatchBusiness.GetDispatchDetailsList("b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90");
        //    try
        //    {

        //        List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();
        //        var users = (from u in _userManager.Users
        //                     join tu in db.TenantUsers on u.Id equals tu.UserId
        //                     where tu.TenantId == TenantId
        //                     select u).ToList();
        //        foreach (var user in users)
        //        {
        //            UserViewSRVModel userViewModel = new UserViewSRVModel();

        //            userViewModel.UserID = user.Id;
        //            userViewModel.PhoneNumber = user.PhoneNumber;
        //            userViewModel.Email = user.Email;
        //            userViewModel.FirstName = user.FirstName ?? String.Empty;
        //            userViewModel.MiddleName = user.MiddleName;
        //            userViewModel.LastName = user.LastName;
        //            userViewModel.IsPrimary = user.Primary.HasValue ? user.Primary.Value : false;
        //            userViewModel.JobTitle = user.JobTitle;
        //            userViewModel.Address = user.Address;
        //            userViewModel.City = user.City;
        //            userViewModel.AdditionalNotes = user.AdditionalNotes;
        //            userViewModel.Zip = user.Zip;
        //            userViewModel.Mobile = user.Mobile;
        //            userViewModel.State = user.State;
        //            userViewModel.UserName = String.Concat(user.FirstName ?? String.Empty, " ", user.LastName ?? String.Empty);


        //            var userRoleNames = _userManager.GetRolesAsync(user).Result;
        //            var tenantRoles = (from r in _roleManager.Roles
        //                               join tr in db.TenantRoles on r.Id equals tr.RoleId
        //                               where tr.TenantId == TenantId
        //                               select r).ToList();

        //            userViewModel.roles = new List<IdentityRole>();
        //            userViewModel.SelectedRoles = "";

        //            foreach (var tenantRole in tenantRoles)
        //            {
        //                if (userRoleNames.Contains(tenantRole.Name))
        //                {
        //                    userViewModel.roles.Add(new IdentityRole { Id = tenantRole.Id, Name = tenantRole.Name });
        //                    userViewModel.SelectedRoles += tenantRole.Id + ";";
        //                }
        //            }


        //            if (userRoleNames.Contains("Service Manager"))
        //            {
        //                userViewModel.IsActive = true;
        //            }
        //            else
        //            {
        //                userViewModel.IsActive = false;
        //            }



        //            userViewModel.ProfileImageName = user.ProfileImageName;
        //            userViewModel.UserTenantId = user.TenantId;

        //            userViewModelList.Add(userViewModel);
        //        }

        //       // rom dr in db.DispatchRoutes in gj.DefaultIfEmpty()

        //        var result = (from um in userViewModelList
        //                      join us in db.Users on um.UserID equals us.Id
        //                      join dr in db.DispatchRoutes on us.Id equals dr.UserId
        //                      into umobj
        //                      from dr in umobj.DefaultIfEmpty()
        //                          // where dr.UserId == (userid) && dr.RouteOrder != 0  
        //                      where us.TenantId == (TenantId)
        //                      // orderby dr.RouteOrder ascending

        //                      select new DispatchRoutesViewModel
        //                      {
        //                          dispatchid = dr.DispatchId,
        //                          dispatchnotes = dr.DispatchNotes,
        //                          userid = um.UserID,
        //                          customer = um.UserName,
        //                          address = dr.LocationAddress,
        //                          //locationname = dr.LocationName == null ? dr.RigName == null ? dr.WellName : dr.RigName : dr.LocationName,
        //                          //city = dr.LocationCity ,
        //                          //state = dr.LocationState ,
        //                          //zip = dr.LocationZip ,
        //                          //routeorder = dr.RouteOrder ,
        //                          //username = us.FirstName + " " + us.LastName ,
        //                          //latitude = dr.Latitude ,
        //                          //longitude = dr.Longitude ,
        //                          //createddate =dr.CreatedDate.ToString()??null,
        //                          //api = dr.APINumber ,
        //                          //rigname = dr.RigName ,
        //                          //rigid = dr.RigId ,
        //                          //wellname = dr.WellName ,
        //                          //wellid = dr.WellId ,
        //                          //activityid =dr.ActivityId ,
        //                          //operatorId = dr.OperatorId ,
        //                          //routestatus = dr.RouteStatus ?? "ON-ROUTE",
        //                          //scheduledArrivalDate =dr.ScheduledArrival ,
        //                          //eta = dr.ETA 
        //                      }
        //                    ).ToList();


        //        var dispatch = result.Where(x => x.UserId.ToString().ToUpper() == " b0e2cd7f-86f1-48b5-ac1b-0cb3f4d96b90").FirstOrDefault();
        //        model.DispatchId = dispatch.DispatchId;
        //        model.WellName = dispatch.WellName;
        //        model.RigName = dispatch.RigName;
        //        model.DispatchNotes = dispatch.DispatchNotes;
        //        driverStatus.Add(model);
        //        DispatchRoutes.DispatchRoutesViewModel = dispatchRoutes;
        //        return (driverStatus);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
        //        customErrorHandler.WriteError(ex, "Dispatch GetDispatchDetails", null);
        //        return null;
        //    }
        //}

        public DispatchUserStatusCount GetDispatchStatuscount(string TenantId)
        {
            DispatchRoutesViewModel dispatchRoutes = new DispatchRoutesViewModel();
            IDispatchRepository objDispatchBusiness = new DispatchRepository(db, _roleManager, _userManager, _configuration);
          
            try
            {

                int result_ACTIVE = (from us in db.Users                                     
                                     where us.TenantId == (TenantId)  && us.UserRouteStatus == "ACTIVE"
                                     select us).Count();

                int result_NACTIVE = (from us in db.Users
                                     where us.TenantId == (TenantId) && us.UserRouteStatus == "INACTIVE"
                                      select us).Count();


                int result_ONSITE = (from us in db.Users
                                      where us.TenantId == (TenantId) && us.UserRouteStatus == "ON-SITE"
                                     select us).Count();



                int result_ONROUTE = (from us in db.Users
                                     where us.TenantId == (TenantId) && us.UserRouteStatus == "ON-ROUTE"
                                      select us).Count();

                int result_OVERDUE = (from us in db.Users
                                      where us.TenantId == (TenantId) && us.UserRouteStatus == "OVERDUE"
                                      select us).Count();



                


                //int result_ACTIVE = (from dr in db.DispatchRoutes
                //                     join us in db.Users on dr.UserId equals us.Id
                //                     where   us.TenantId == (TenantId) && dr.RouteOrder != 0 && dr.RouteStatus == "ACTIVE"

                //                     select dr).Count();
                //int result_NACTIVE = (from dr in db.DispatchRoutes
                //                      join us in db.Users on dr.UserId equals us.Id
                //                      where   us.TenantId == (TenantId) &&  dr.RouteOrder != 0 && dr.RouteStatus == "INACTIVE"

                //                      select dr).Count();


                //int result_ONSITE = (from dr in db.DispatchRoutes
                //                     join us in db.Users on dr.UserId equals us.Id

                //                     where us.TenantId == (TenantId) && dr.RouteOrder != 0 && dr.RouteStatus == "ON-SITE"
                //                     select dr).Count();


                //int result_ONROUTE = (from dr in db.DispatchRoutes
                //                      join us in db.Users on dr.UserId equals us.Id
                //                      where    us.TenantId == (TenantId) &&  dr.RouteOrder != 0 &&
                //                      (dr.RouteStatus == "ON-ROUTE" || dr.RouteStatus == null
                //                      || dr.RouteStatus == "")

                //                      select dr).Count();

                //int result_OVERDUE = (from dr in db.DispatchRoutes
                //                      join us in db.Users on dr.UserId equals us.Id
                //                      where  us.TenantId == (TenantId) &&  dr.RouteOrder != 0 && dr.RouteStatus == "OVERDUE"

                //                      select dr).Count();

                //int result_ACTIVE = db.DispatchRoutes.Where(x => x.RouteStatus == "ACTIVE" && x.RouteOrder != 0).Count();
                //int result_NACTIVE = db.DispatchRoutes.Where(x => x.RouteStatus == "INACTIVE" && x.RouteOrder != 0).Count();
                //int result_ONROUTE = db.DispatchRoutes.Where(x => (x.RouteStatus == "ON-ROUTE" || x.RouteStatus == null
                //|| x.RouteStatus == "") && x.RouteOrder != 0).Count();
                //int result_ONSITE = db.DispatchRoutes.Where(x => x.RouteStatus == "ON-SITE" && x.RouteOrder != 0).Count();
                //int result_OVERDUE = db.DispatchRoutes.Where(x => x.RouteStatus == "OVERDUE" && x.RouteOrder != 0).Count();


                DispatchUserStatusCount record = new DispatchUserStatusCount();
                record.ACTIVE = result_ACTIVE;
                record.INACTIVE = result_NACTIVE;
                record.ONROUTE = result_ONROUTE;
                record.ONSITE = result_ONSITE;
                record.OVERDUE = result_OVERDUE;

                return record;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Dispatch GetDispatchDetails", null);
                return null;
            }
        }

    }
}
