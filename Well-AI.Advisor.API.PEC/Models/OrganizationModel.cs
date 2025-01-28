using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.PEC.Models
{
    public class OrganizationModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public object logoUrl { get; set; }
        public object description { get; set; }
        public DateTime subscriptionExpirationDate { get; set; }
        public string verticalId { get; set; }
        public string primaryAddress { get; set; }
        public string secondaryAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
        public string phoneNumber { get; set; }
        public string phoneNumberExtension { get; set; }
        public string emailAddress { get; set; }
        public bool isActive { get; set; }
        public IList<string> features { get; set; }
    }
  
}
