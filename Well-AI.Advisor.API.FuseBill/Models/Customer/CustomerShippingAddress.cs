using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models
{
   public  class CustomerShippingAddress
    {
        public int customerAddressPreferenceId { get; set; }
        public string companyName { get; set; }
        public string line1 { get; set; }
        public object line2 { get; set; }
        public int countryId { get; set; }
        public string country { get; set; }
        public int stateId { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string postalZip { get; set; }
        public string addressType { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }
}
