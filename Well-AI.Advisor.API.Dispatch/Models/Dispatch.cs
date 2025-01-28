using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.API.Dispatch.Models
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
    //public class DispatchRoutesModel
    //{
    //    public string UserId { get; set; }
    //    public string Customer { get; set; }
    //    public string LocationName { get; set; }
    //    public string LocationAddress { get; set; }
    //    public string LocationCity { get; set; }
    //    public string LocationState { get; set; }
    //    public string LocationZip { get; set; }
    //    public string Latitude { get; set; }
    //    public string Longitude { get; set; }
    //    public string DispatchNotes { get; set; }
    //    public string RouteOrder { get; set; }
    //}
    public class DispatchRoutesResponse
    {
        public string result { get; set; }
        public string message { get; set; }
        public List<DispatchRoutesViewModel> dispatchroutes{ get; set; }
        
    }
    public class DispatchRoutesRequest
    {
        public string userid { get; set; }
        //public string email_address { get; set; }
        //public string password { get; set; }
    }
    //public class RecordDestinationChangesResponse
    //{
    //    public string result { get; set; }
    //    public string message { get; set; }
    //    public List<DispatchRoutesHistoryModel> dispatchroutes { get; set; }
    //}
    //public class RecordDestinationChangesRequest
    //{
    //    public string userid { get; set; }
        
    //}

}
