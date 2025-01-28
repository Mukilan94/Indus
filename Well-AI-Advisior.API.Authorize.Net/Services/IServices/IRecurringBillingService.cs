using AuthorizeNet.Api.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI_Advisior.API.Authorize.Net.Model;

namespace Well_AI_Advisior.API.Authorize.Net.Services.IServices
{
    public interface IRecurringBillingService
    {
        SubscriptionResponse CreateSubscription(CreditCardType cctype, SubscriptionType subscriptionType, string accountType);
        ANetApiResponse GetSubscription(string subscriptionId, string accountType);
        SubscriptionResponse GetSubscriptionStatus(string subscriptionId, string accountType);
        ANetApiResponse UpdateSubscription(CreditCardType cctype, SubscriptionType subscriptionType, string subscriptionId, string accountType);
        ANetApiResponse CancelSubscription(string subscriptionId, string accountType);
        ANetApiResponse GetListOfSubscriptions(string accountType);
        SubscriptionResponse GetTransactionListRequest(string accountType);
        ANetApiResponse GetTransactionListRequestStatus(string accountType,string batchId);
        ANetApiResponse GetSettledBatchListStatus(string accountType ,DateTime FirsDate , DateTime LastDate);

        ANetApiResponse UpdateSubscriptionApi(CreditCardType cctype, SubscriptionType subscriptionType, string subscriptionId, string accountType);
    }
}
