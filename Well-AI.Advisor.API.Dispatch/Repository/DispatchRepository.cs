using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Dispatch.Repository.IRepository;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.API.Dispatch.Models;
using System.Globalization;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.DLL.Entity;
using Microsoft.Extensions.Configuration;

namespace WellAI.Advisor.API.Dispatch.Repository
{
    public class DispatchRepository : IDispatchRepository
    {

        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _wdb;

        private readonly SignInManager<WellIdentityUser> _signInManager;
        private UserManager<WellIdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;


        public DispatchRepository(IMapper mapper, WebAIAdvisorContext wdb,
            SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager, IConfiguration configuration = null)
        {
            _mapper = mapper;
            _wdb = wdb;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<DispatchRoutesResponse> GetDispatchRoutes(DispatchRoutesRequest Request)
        {
            DispatchRoutesResponse dispatchReponse = new DispatchRoutesResponse();
            List<DispatchRoutesViewModel> dispatchRoutsList = new List<DispatchRoutesViewModel>();
            ICommonBusiness commonBusiness = new CommonBusiness(_wdb, _roleManager, _userManager);

            try
            {
                var userObj = await commonBusiness.GetUserById(Request.userid);
                if (userObj == null)
                {
                    dispatchReponse.message = "User not found";
                    dispatchReponse.result = "failure";
                }
                else
                {

                    var dispatchListObj = await commonBusiness.GetDispatchRoutes(Request.userid,true);

                    foreach (var item in dispatchListObj)
                    {
                        DispatchRoutesViewModel dispatchObj = new DispatchRoutesViewModel();
                        dispatchObj.dispatchid = item.dispatchid;
                        dispatchObj.userid = item.userid;
                        dispatchObj.createddate = item.createddate == null ? "" : item.createddate.ToString("yyyy-dd-mm HH:mm", CultureInfo.CurrentCulture);
                        dispatchObj.customer = item.customer;
                        dispatchObj.locationname = item.locationname;
                        dispatchObj.address = item.address;
                        dispatchObj.city = item.city;
                        dispatchObj.state = item.state;
                        dispatchObj.zip = item.zip;
                        dispatchObj.latitude = Convert.ToDouble(item.latitude);
                        dispatchObj.longitude = Convert.ToDouble(item.longitude);
                        dispatchObj.dispatchnotes = item.dispatchnotes;
                        dispatchObj.routeorder = item.routeorder;
                        //dispatchObj.modifieddate = item.modifieddate.HasValue ? item.modifieddate?.ToString("yyyy-MM-dd hh:mm:ss") : "";
                        //dispatchObj.ismodified = item.ismodified;
                        dispatchObj.api = item.api ?? "";
                        dispatchObj.rigname = item.rigname ?? "";
                        dispatchObj.wellid = item.wellid ?? "";
                        dispatchObj.wellname = item.wellname ?? "";
                        dispatchObj.username = item.username;
                        dispatchRoutsList.Add(dispatchObj);
                    }

                    if (dispatchRoutsList.Count == 0)
                    {
                        dispatchReponse.message = "No Dispatch routes found";
                    }
                    else
                    {
                        dispatchReponse.message = "";
                    }
                    dispatchReponse.result = "success";

                    dispatchReponse.dispatchroutes = dispatchRoutsList;
                }
                //_wdb.WellIdentityUser.Any(x=>x.Email==request.email_address && x.PasswordHash == request.)
                //    _db.Rigs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            }
            catch (Exception e)
            {
                DispatchRoutesViewModel dispatchObj1 = new DispatchRoutesViewModel();
                dispatchReponse.result = "failure";
                dispatchReponse.message = e.Message;
                dispatchReponse.dispatchroutes = dispatchRoutsList;
            }


            return dispatchReponse;
        }

        public async Task<UserLocationReponse> UserLocation(string UserId)
        {
            UserLocationReponse reponse = new UserLocationReponse();
            UserProfile userinfo = new UserProfile();
            ICommonBusiness commonBusiness = new CommonBusiness(_wdb, _roleManager, _userManager);

            var userObj = await commonBusiness.GetUserById(UserId);
            if (userObj == null)
            {
                reponse.message = "User not found";
                reponse.result = "failure";
            }
            else
            {
                reponse.message = "";
                reponse.result = "success";
            }
            return reponse;
        }

        public async Task<RouteAcceptedResponse> RouteAccepted(RouteAcceptedRequest request)
        {
            RouteAcceptedResponse reponse = new RouteAcceptedResponse();

            ICommonBusiness commonBusiness = new CommonBusiness(_wdb, _roleManager, _userManager);
            WellAI.Advisor.BLL.Business.DispatchBusiness dispatch = new WellAI.Advisor.BLL.Business.DispatchBusiness(_wdb, _roleManager, _userManager,_configuration);

            var userObj = await commonBusiness.GetUserById(request.user_key);
            if (userObj == null)
            {
                reponse.message = "User not found";
                reponse.result = "failure";
            }
            else if (request.routeaccepted == "no")
            {
                //do not request routes
                reponse.message = "ok";
                reponse.result = "success";
            }
            else if(request.routeaccepted=="yes")
            {
                //logic to be implemented to update routes
                //request routes and update routes
                var dispatchAPIUrl = _configuration.GetSection("WellDataApi")["ApiUrl"];
                UserRoutes userRoutes = await dispatch.GetUserCurrentRoutes(request.user_key, dispatchAPIUrl);
                if (userRoutes != null)
                {
                    ////Delete all routes from DispatchRoutes table for the user
                    //var routes = _wdb.DispatchRoutes.Where(x => x.UserId == request.user_key).ToList();
                    //_wdb.DispatchRoutes.RemoveRange(routes);
                    //await _wdb.SaveChangesAsync();

                    ////Add the routes from userRoutes to DispatchRoutes table
                    //DispatchRoutes objDispatch = new DispatchRoutes();

                    //objDispatch.LocationAddress = "";
                    //objDispatch.LocationName = "";
                    //objDispatch.LocationCity = "";
                    //objDispatch.LocationState = "";
                    //objDispatch.LocationZip = "";
                    //objDispatch.Latitude = 0.00;
                    //objDispatch.Longitude = 0.00;
                    //objDispatch.DispatchNotes = "";
                    //objDispatch.RouteOrder = 1;
                    //objDispatch.UserId = request.user_key;
                    //objDispatch.CreatedDate = userRoutes.location.logged_dt;
                    //objDispatch.Customer = "";
                    //objDispatch.APINumber = "";
                    //objDispatch.WellName = userRoutes.location.activity.well_id != "" ? userRoutes.location.activity.destination :"";
                    //objDispatch.RigName = userRoutes.location.activity.rig_id != "" ? userRoutes.location.activity.rig_id : "";
                    //objDispatch.WellId = userRoutes.location.activity.well_id;
                    //objDispatch.RigId = userRoutes.location.activity.rig_id;
                    //commonBusiness.CreateDispatchRoute(objDispatch);
                    await dispatch.UpdateUserRoutes(request.user_key, userRoutes);
                }

                

                reponse.message = "ok";
                reponse.result = "success";
            }
            else 
            {
                //logic to be implemented to update routes
                reponse.message = "Invalid input";
                reponse.result = "failure";
            }


            //_wdb.WellIdentityUser.Any(x=>x.Email==request.email_address && x.PasswordHash == request.)
            //    _db.Rigs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return reponse;
        }



        public async Task<RecordDestinationChangesResponse> RecordDestinationChanges(RecordDestinationChangesRequest request)
        {
            RecordDestinationChangesResponse reponse = new RecordDestinationChangesResponse();

            ICommonBusiness commonBusiness = new CommonBusiness(_wdb, _roleManager, _userManager);
            var result = await commonBusiness.CreateDispatchRoutesHistory(request.destinationroutes);

            if(result != false)
            {
                reponse.result = "success";
                reponse.message = "Record saved";
            }
            else
            {
                reponse.message = "";
                reponse.result = "failure";
            }            
            return reponse;
        }
    }
     
    }
    

