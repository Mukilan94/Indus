using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Well_AI.Advisor.Communication;
using WellAI.Advisor.Areas.Identity;

namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    //[SessionTimeOut]
    public class CallController : TwilioController
    {
        private readonly ICommunication communication;
        public CallController(ICommunication communication)
        {
            this.communication = communication;
        }

        //[HttpPost]
        //public IActionResult Connect(PhoneCall phone)
        //{
        //    var response = communication.ConnectCall(phone.PhoneNumber);
        //    return TwiML(response);
        //}
        [HttpPost]
        public IActionResult Connect(string to, string callingDeviceIdentity)
        {
            var response = communication.ConnectCall(to, callingDeviceIdentity);
            //return TwiML(response);
            return Content(TwiML(response).ToString(), "text/xml");
        }
    }
    public class PhoneCall
    {
        public string PhoneNumber { get; set; }
    }
}
