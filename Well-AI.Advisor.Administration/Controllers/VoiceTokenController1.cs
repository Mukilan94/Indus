using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.Model.TwilioCredentials;
using Microsoft.Extensions.Options;
using Faker;
using Faker.Extensions;
using Twilio.Jwt.AccessToken;

namespace WellAI.Advisor.Areas.OperatingCompany
{
    public class VoiceTokenController : Controller
    {
        private readonly TwilioAccountDetails _twilioAccountDetails;

        public VoiceTokenController(IOptions<TwilioAccountDetails> twilioAccountDetails)
        {
            _twilioAccountDetails = twilioAccountDetails.Value ?? throw new ArgumentException(nameof(twilioAccountDetails));
        }

        // GET: /token
        [HttpGet]
        public IActionResult Index()
        {

            var identity = Internet.UserName().AlphanumericOnly();
            Device.Identity = identity;

            var grant = new VoiceGrant();
            grant.OutgoingApplicationSid = _twilioAccountDetails.TwimlAppSid;
            grant.IncomingAllow = true;

            var grants = new HashSet<IGrant>
                {
                    { grant }
                };

            var token = new Token(
            _twilioAccountDetails.AccountSid,
            _twilioAccountDetails.ApiSid,
            _twilioAccountDetails.ApiSecret,
            identity,
            grants: grants).ToJwt();



            return Json(new { identity, token });
        }
    }
}
