using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Function.MailQueue.Models
{
    public class UserSessionViewModel
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SessionTimeStamp { get; set; }
        public int SessionInterval { get; set; }
    }

    public class StaffUserSessionViewModel
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SessionTimeStamp { get; set; }
        public int SessionInterval { get; set; }
    }
}
