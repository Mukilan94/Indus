using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.TwilioCredentials
{
    public class TwilioCredentials
    {
        public string AccountSID { get; set; }
        public string AuthToken { get; set; }
        public string TwiMLApplicationSID { get; set; }
        public string PhoneNumber { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string ChatServiceSid { get; set; }
    }

    public static class Device
    {
        public static string Identity;
    }

    public class TwilioAccountDetails
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string TwimlAppSid { get; set; }
        public string CallerId { get; set; }
        public string ApiSid { get; set; }
        public string ApiSecret { get; set; }
    }
}
