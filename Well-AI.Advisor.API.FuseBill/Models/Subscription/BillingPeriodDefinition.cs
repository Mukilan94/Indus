using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{
  

   

    public class Subscription
    {
        public int customerId { get; set; }
        public PlanFrequency planFrequency { get; set; }
        public string planCode { get; set; }
        public string planName { get; set; }
        public object planDescription { get; set; }
        public object planReference { get; set; }
        public string status { get; set; }
        public object reference { get; set; }
        public object subscriptionOverride { get; set; }
        public bool hasPostedInvoice { get; set; }
        public DateTime createdTimestamp { get; set; }
        public object activatedTimestamp { get; set; }
        public object provisionedTimestamp { get; set; }
        public object nextPeriodStartDate { get; set; }
        public object lastPeriodStartDate { get; set; }
        public object scheduledActivationTimestamp { get; set; }
        public IList<object> subscriptionProducts { get; set; }
        public object remainingInterval { get; set; }
        public object remainingIntervalPushOut { get; set; }
        public object openSubscriptionPeriodEndDate { get; set; }
        public object chargeDiscount { get; set; }
        public object setupFeeDiscount { get; set; }
        public object chargeDiscounts { get; set; }
        public object setupFeeDiscounts { get; set; }
        public object customFields { get; set; }
        public bool planAutoApplyChanges { get; set; }
        public bool autoApplyCatalogChanges { get; set; }
        public int monthlyRecurringRevenue { get; set; }
        public int netMonthlyRecurringRevenue { get; set; }
        public int amount { get; set; }
        public object contractStartTimestamp { get; set; }
        public object contractEndTimestamp { get; set; }
        public object expiredTimestamp { get; set; }
        public DateTime modifiedTimestamp { get; set; }
        public IList<object> coupons { get; set; }
        public object invoiceDay { get; set; }
        public object invoiceMonth { get; set; }
        public object canMigrate { get; set; }
        public object migrationDate { get; set; }
        public object scheduledMigrationDate { get; set; }
        public bool subscriptionHasRecurringEndOfPeriodCharge { get; set; }
        public object nextRechargeDate { get; set; }
        public int invoiceInAdvance { get; set; }
        public int billingPeriodDefinitionId { get; set; }
        public object salesforceId { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }

    public class BillingPeriodDefinition
    {
        public string interval { get; set; }
        public int numberOfIntervals { get; set; }
        public int invoiceDay { get; set; }
        public string billingPeriodType { get; set; }
        public object invoiceMonth { get; set; }
        public object mostRecentBillingPeriodStartDate { get; set; }
        public object mostRecentBillingPeriodEndDate { get; set; }
        public object nextRechargeDate { get; set; }
        public int numberOfBillingPeriods { get; set; }
        public int numberOfSubscriptions { get; set; }
        public IList<Subscription> subscriptions { get; set; }
        public bool canDelete { get; set; }
        public int invoiceInAdvance { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }
}
