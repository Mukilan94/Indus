using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
//using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Business
{
    public class SubscriptionBusiness : ISubscriptionBusiness
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public SubscriptionBusiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List<CrmSubscriptions> GetSubscriptions()
        {
            ISubscriptionRepository repostirory = new SubscriptionRepository(db, _roleManager, _userManager);
            return repostirory.GetSubscriptions();
        }

        public SubscriptionPackage GetSubscription(string userId)
        {
            ISubscriptionRepository repostirory = new SubscriptionRepository(db, _roleManager, _userManager);
            return repostirory.GetSubscription(userId);
        }

        public List<SubscriptionPackage> GetSubscriptionPackages()
        {
            ISubscriptionRepository repostirory = new SubscriptionRepository(db, _roleManager, _userManager);
            return repostirory.GetSubscriptionPackages();
        }

        public string GetCurrentSubscription(string tenantId)
        {
            ISubscriptionRepository repostirory = new SubscriptionRepository(db, _roleManager, _userManager);
            return repostirory.GetCurrentSubscription(tenantId);
        }
        public List<SubscriptionPackage>GetNewSubscriptionPackages()//dk
        {
            ISubscriptionRepository repostirory = new SubscriptionRepository(db, _roleManager, _userManager);
            return repostirory.GetNewSubscriptionPackages();
        }


    }
}
