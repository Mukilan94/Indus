using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{
    public class CouponCode
    {
        public string code { get; set; }
        public int timesUsed { get; set; }
        public int? remainingUsages { get; set; }
    }

    public class Coupon
    {
        public string name { get; set; }
        public string description { get; set; }
        public DateTime eligibilityStartDate { get; set; }
        public DateTime eligibilityEndDate { get; set; }
        public string status { get; set; }
        public bool applyToAllPlans { get; set; }
        public IList<CouponCode> couponCodes { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }
}
