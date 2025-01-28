using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.Common
{
    public class ClientContactModel
    {
        public string UserId { get; set; }
        public string TenantId { get; set; }
        public string ContactId { get; set; }
        public string UserName { get; set; }
        public string UserLogoPath { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }
        public string CompanyWebsite { get; set; }
    }
}
