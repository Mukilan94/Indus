using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Administration
{
    public interface IUsersManagerBusiness
    {
        Task<WellIdentityUser> FindByEmailAsync(string Email);
        Task<string> GeneratePasswordResetTokenAsync(WellIdentityUser user);
    }

    public class UsersManagerBusiness : IUsersManagerBusiness
    {
        private readonly WebAIAdvisorContext db;
        UserManager<WellIdentityUser> _userManager;
        protected readonly IMapper _mapper;
        public UsersManagerBusiness(WebAIAdvisorContext db, UserManager<WellIdentityUser> userManager, IMapper mapper)
        {
            this.db = db;
            _userManager = userManager;
            _mapper = mapper;

        }
        public async Task<WellIdentityUser> FindByEmailAsync(string Email)
        {
           // 
            var user = await _userManager.FindByEmailAsync(Email);
            return user;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(WellIdentityUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
    }
}
