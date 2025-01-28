using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Core;
using Twilio.TwiML;
using Well_AI.Advisor.Communication;
using WellAI.Advisor.Areas.Identity;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
 
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