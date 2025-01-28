using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.API.Models
{
    public class UserLocationReponse
    {
        public string result { get; set; }
        public string message { get; set; }
        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }

    }
    public class UserLocationRequest
    {
        public string UserId { get; set; }
 
    }
    public class DispatchRoutesResponse
    {
        public string result { get; set; }
        public string message { get; set; }
        public List<DispatchRoutesModel> dispatchroutes{ get; set; }
    }
    public class DispatchRoutesRequest
    {
        public string UserId { get; set; }
       
    }
}
