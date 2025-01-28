using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{
    public class SubscriptionSummary
    {
        public int customerId { get; set; }
        public string planCode { get; set; }
        public string name { get; set; }
        public object description { get; set; }
        public string reference { get; set; }
        public string interval { get; set; }
        public int numberOfInterval { get; set; }
        public string subscriptionStatus { get; set; }
        public int monthlyRecurringRevenue { get; set; }
        public int netMonthlyRecurringRevenue { get; set; }
        public DateTime lastBillingDate { get; set; }
        public DateTime nextBillingDate { get; set; }
        public string expiryDate { get; set; }
        public DateTime scheduledActivationTimestamp { get; set; }
        public DateTime cancellationTimestamp { get; set; }
        public DateTime suspendedTimestamp { get; set; }
        public DateTime createdTimestamp { get; set; }
        public DateTime modifiedTimestamp { get; set; }
        public DateTime activatedTimestamp { get; set; }
        public DateTime provisionedTimestamp { get; set; }
        public DateTime nextPeriodStartDate { get; set; }
        public DateTime contractStartTimestamp { get; set; }
        public DateTime contractEndTimestamp { get; set; }
        public int remainingInterval { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }
}
