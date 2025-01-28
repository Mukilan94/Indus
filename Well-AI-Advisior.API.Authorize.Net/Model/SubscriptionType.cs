using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI_Advisior.API.Authorize.Net.Model
{
    public class SubscriptionType
    {
        public string name { get; set; }
        public PaymentSchedule paymentSchedule { get; set; }
        public decimal amount { get; set; }
        public decimal trialAmount { get; set; }
        public PaymentType payment { get; set; }
        public CustomerAddressType billTo { get; set; }
    }

    public class PaymentType
    {
        public object Item { get; set; }

        public string dataSource { get; set; }
    }
    public class PaymentSchedule
    {
        public Interval interval { get; set; }
        public string startDate { get; set; }
        public short totalOccurrences { get; set; }
        public short trialOccurrences { get; set; }
    }

    public class Interval
    {
        public short length { get; set; }
        public int unit { get; set; }
    }
}
