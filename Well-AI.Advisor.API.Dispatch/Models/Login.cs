using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Dispatch.Models
{
    /// <summary>
    /// Service Company User Profile for Dispatch
    /// </summary>
    public class UserProfile
    {
        public string user_key { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company { get; set; }
        public string companyId { get; set; }
        public string phone { get; set; }
        public string email_address { get; set; }
    }

    public class LoginReponse
    {
        public string result { get; set; }
        public string message { get; set; }
        public UserProfile userinfo { get; set; }
    }
    public class LoginRequest
    {
        public string email_address { get; set; }
        public string password { get; set; }
    }
}
