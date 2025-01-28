using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.BLL.Business
{
    public interface ISubscriptionBusiness
    {
        List<CrmSubscriptions> GetSubscriptions();
        SubscriptionPackage GetSubscription(string userId);
        List<SubscriptionPackage> GetSubscriptionPackages();
        string GetCurrentSubscription(string tenantId);
        public List<SubscriptionPackage> GetNewSubscriptionPackages();
    }
}
