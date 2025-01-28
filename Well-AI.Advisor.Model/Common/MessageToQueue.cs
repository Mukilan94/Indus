using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.Common
{
    public class MessageToQueue
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string MsgSubject { get; set; }
        public string MsgBody { get; set; }
        public string MsgFooter { get; set; }
    }
}
