using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{
    public class PlanFrequency
    {
        public int planRevisionId { get; set; }
        public int numberOfIntervals { get; set; }
        public string interval { get; set; }
        public int numberOfSubscriptions { get; set; }
        public string status { get; set; }
        public IList<object> setupFees { get; set; }
        public IList<object> charges { get; set; }
        public bool isProrated { get; set; }
        public object prorationGranularity { get; set; }
        public int planFrequencyUniqueId { get; set; }
        public object remainingInterval { get; set; }
        public int invoiceInAdvance { get; set; }
        public object salesforceId { get; set; }
        public int id { get; set; }
        public object uri { get; set; }
    }
}
