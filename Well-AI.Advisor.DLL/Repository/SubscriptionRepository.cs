using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.DLL.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public SubscriptionRepository(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public List<CrmSubscriptions> GetSubscriptions()
        {
            try
            {
                return db.CrmSubscriptions.ToList();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Subscription GetSubscriptions", null);
                return null;
            }
        }

        public List<SubscriptionPackage> GetSubscriptionPackages()
        {
            try
            {
                return db.SubscriptionPackage.Where(x => x.IsActive == true && x.AccountType == 2).OrderBy(x => x.PackageOrder).ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Subscription GetSubscriptionPackages", null);
                return null;
            }
        }

        public SubscriptionPackage GetSubscription(string userId)
        {
            try
            {
                return (from s in db.SubscriptionPackage
                        join u in db.CrmUserBasicDetail on s.PackageId.ToString() equals u.SubscriptionId
                        where u.UserId == userId
                        select s).FirstOrDefault();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Subscription GetSubscription", null);
                return null;
            }
        }

        public string GetCurrentSubscription(string userId)
        {
            try
            {
                return (from sub in db.CrmUserBasicDetail
                        where sub.UserId == userId
                        select sub.SubscriptionId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Subscription GetCurrentSubscription", null);
                return null;
            }
        }
        public List<SubscriptionPackage>GetNewSubscriptionPackages()//Dk
        {

            try
            {
                return db.SubscriptionPackage.ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "Subscription GetNewSubscriptionPackages", null);
                return null;
            }
        }
    }
}
