using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Controllers;
using Well_AI.Advisor.API.Data;
using Well_AI.Advisor.API.Models;
using Well_AI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.API.Models;
using WellAI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.API.Repository.IRepository
{

    public class LoginRepository : ILoginRepository
    {
  
        private readonly IMapper _mapper;
       private readonly WebAIAdvisorContext _wdb;

        private readonly SignInManager<WellIdentityUser> _signInManager;
        private UserManager<WellIdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
 
        public LoginRepository(IMapper mapper, WebAIAdvisorContext wdb, 
            SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager, 
            UserManager<WellIdentityUser> userManager)
        {
            _mapper = mapper;
            _wdb = wdb;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginReponse> Login(LoginRequest request)
        {
            LoginReponse reponse = new LoginReponse();
            UserProfile userinfo = new UserProfile();
            ICommonBusiness commonBusiness = new CommonBusiness(_wdb, _roleManager, _userManager);

            var userObj = await commonBusiness.GetUserByEmail(request.email_address);
            if (userObj == null)
            {
                reponse.message = "User not found";
                reponse.result = "failure";
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(request.email_address, request.password, false, false);

                if (!result.Succeeded)
                {
                    reponse.message = "Incorrect Password";
                    reponse.result = "failure";
                }
                else
                {
                    reponse.message = "";
                    reponse.result = "success";

                    userinfo.user_key = userObj.Id;
                    userinfo.first_name = userObj.FirstName;
                    userinfo.last_name = userObj.LastName;
                    userinfo.company = commonBusiness.GetCompanyDetailByTenant(userObj.TenantId).Name;
                    userinfo.companyId = userObj.TenantId;
                    userinfo.phone = userObj.PhoneNumber ?? userObj.Mobile;
                    reponse.userinfo = userinfo;
               }
            }
            
            //_wdb.WellIdentityUser.Any(x=>x.Email==request.email_address && x.PasswordHash == request.)
            //    _db.Rigs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return reponse;
        }
    }
}
