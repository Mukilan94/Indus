using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.FuseBill.Models.Subscription;
using Well_AI.Advisor.API.FuseBill.Services.IServices;

namespace Well_AI.Advisor.API.FuseBill.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public Task<Subscription> GetSubscriptionDetailsAsync(int SubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Subscription>> GetSubscriptionListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Subscription>> GetSubscriptionListByCustomerAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<SubscriptionProduct> GetSubscriptionProductAsync(int subscriptionProductId)
        {
            throw new NotImplementedException();
        }

        public Task<List<SubscriptionSummary>> GetSubscriptionSummaryListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Subscription> UpdateSubscriptionAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public Task<SubscriptionProduct> UpdateSubscriptionProductAsync(SubscriptionProduct subscriptionProduct)
        {
            throw new NotImplementedException();
        }

        public Task<SubscriptionProduct> UpdateSubscriptionProductQuantityAsync(int subscriptionProductId, double deltaQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
