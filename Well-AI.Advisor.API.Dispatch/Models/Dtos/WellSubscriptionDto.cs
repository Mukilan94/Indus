using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models.Dtos
{
    public class WellSubscriptionDto
    {
        public string CustomerAccountIdentifier { get; set; }
        public string WorkstationIdentifier { get; set; }
        public string RegistrationToken { get; set; }
    }
}
