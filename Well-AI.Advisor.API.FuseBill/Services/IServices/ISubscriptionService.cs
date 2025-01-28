using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.FuseBill.Models.Subscription;

namespace Well_AI.Advisor.API.FuseBill.Services.IServices
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetSubscriptionListAsync();
        Task<List<SubscriptionSummary>> GetSubscriptionSummaryListAsync();

        Task<List<Subscription>> GetSubscriptionListByCustomerAsync(int customerId);

        Task<Subscription> GetSubscriptionDetailsAsync(int SubscriptionId);

        Task<Subscription> CreateSubscriptionAsync(Subscription subscription);

        Task<Subscription> UpdateSubscriptionAsync(Subscription subscription);

        Task<SubscriptionProduct> GetSubscriptionProductAsync(int subscriptionProductId);

        Task<SubscriptionProduct> UpdateSubscriptionProductAsync(SubscriptionProduct subscriptionProduct);

        Task<SubscriptionProduct> UpdateSubscriptionProductQuantityAsync(int subscriptionProductId,double deltaQuantity);
    }
}
