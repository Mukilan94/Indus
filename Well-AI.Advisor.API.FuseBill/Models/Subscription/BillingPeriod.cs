using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{
    public class BillingPeriod
    {
        public DateTime currentBillingPeriodStartDate { get; set; }
        public DateTime currentBillingPeriodEndDate { get; set; }
        public DateTime nextRechargeDate { get; set; }
        public int invoiceDay { get; set; }
        public object invoiceMonth { get; set; }
        public string cycle { get; set; }
        public string billingPeriodType { get; set; }
        public DateTime createdDate { get; set; }
        public int numberOfIntervals { get; set; }
        public int numberOfBillingPeriods { get; set; }
        public int invoiceInAdvance { get; set; }
        public object subscriptions { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }
}
