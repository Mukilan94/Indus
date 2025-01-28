using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Dispatch.Models
{
    public class RouteAcceptedRequest
    {
        public string user_key { get; set; }
        public string routeaccepted { get; set; }
    }
    public class RouteAcceptedResponse
    {
        [Required]
        public string result { get; set; }
        public string message { get; set; }
    }

    
}