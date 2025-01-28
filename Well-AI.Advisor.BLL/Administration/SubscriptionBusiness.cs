using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Administration
{
    public interface ISubscriptionBusiness
    {
        Task<List<SubscriptionViewModel>> GetSubscriptions();
        Task<SubscriptionViewModel> AddSubscription(SubscriptionViewModel input);
        Task<SubscriptionViewModel> UpdateSubscription(SubscriptionViewModel input);
    }


    public class SubscriptionBusiness : ISubscriptionBusiness
    {
        private readonly WebAIAdvisorContext db;
        public SubscriptionBusiness(WebAIAdvisorContext db)
        {
            this.db = db;
        }

        public async Task<SubscriptionViewModel> AddSubscription(SubscriptionViewModel input)
        {
            try
            {
                CrmSubscriptions crmSubscriptions = new CrmSubscriptions
                {
                    Description = input.Description,
                    Id = input.Id,
                    Name = input.Name,
                    Price = input.Price
                };
                db.CrmSubscriptions.Add(crmSubscriptions);
                await db.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "Subscription AddSubscription", null);
                return null;
            }
        }

        public async Task<List<SubscriptionViewModel>> GetSubscriptions()
        {
            try
            {
                return await db.CrmSubscriptions.Select(x => new SubscriptionViewModel
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "Subscription GetSubscriptions", null);
                return null;
            }
        }

        public async Task<SubscriptionViewModel> UpdateSubscription(SubscriptionViewModel input)
        {
            try
            {
                var result = db.CrmSubscriptions.Find(input.Id);
                result.Description = input.Description;
                result.Name = input.Name;
                result.Price = input.Price;
                await db.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "Subscription UpdateSubscription", null);
                return null;
            }
        }
    }

}
