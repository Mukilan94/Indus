using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.API.Repository
{
    public class DispatchRepository : IDispatchRepository
    {

        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _wdb;

        private readonly SignInManager<WellIdentityUser> _signInManager;
        private UserManager<WellIdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public DispatchRepository(IMapper mapper, WebAIAdvisorContext wdb,
            SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager)
        {
            _mapper = mapper;
            _wdb = wdb;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public  async Task<DispatchRoutesResponse> GetDispatchRoutes(DispatchRoutesRequest Request)
        {
            DispatchRoutesResponse reponse = new DispatchRoutesResponse();
            List<DispatchRoutesModel> dispatchRoutsList = new List<DispatchRoutesModel>();
            ICommonBusiness commonBusiness = new CommonBusiness(_wdb, _roleManager, _userManager);

            var userObj = await commonBusiness.GetUserById(Request.UserId);
            if (userObj == null)
            {
                reponse.message = "User not found";
                reponse.result = "failure";
            }              
            else
                {

                  var dispatchListObj = await commonBusiness.GetDispatchRoutes(Request.UserId,true);

                  
                foreach(var item in dispatchListObj)
                {
                    DispatchRoutesModel dispatchObj = new DispatchRoutesModel();
                    dispatchObj.dispatchid = item.dispatchid;
                    dispatchObj.userid = item.userid;
                    dispatchObj.customer = item.customer;
                    dispatchObj.locationname = item.locationname;
                    dispatchObj.address = item.address;
                    dispatchObj.city = item.city;
                    dispatchObj.state = item.state;
                    dispatchObj.zip = item.zip;
                    dispatchObj.latitude = item.latitude;
                    dispatchObj.longitude = item.longitude;
                    dispatchObj.dispatchnotes = item.dispatchnotes;
                    dispatchObj.routeorder = item.routeorder;
                    dispatchRoutsList.Add(dispatchObj);
                }

                if (dispatchRoutsList.Count == 0) {
                    reponse.message = "No Dispatch routes found";                 
                }
                reponse.result = "success";

                reponse.dispatchroutes = dispatchRoutsList;
            }
            //_wdb.WellIdentityUser.Any(x=>x.Email==request.email_address && x.PasswordHash == request.)
            //    _db.Rigs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return reponse;
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

        //_wdb.WellIdentityUser.Any(x=>x.Email==request.email_address && x.PasswordHash == request.)
        //    _db.Rigs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
    }
     
    }
    

