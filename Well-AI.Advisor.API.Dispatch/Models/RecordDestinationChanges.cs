using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.Model.ServiceCompany.Models;
using System.ComponentModel.DataAnnotations;




namespace WellAI.Advisor.API.Dispatch.Models
{
    public class RecordDestinationChangesRequest
    {
       
      //  public string user_key { get; set; }
       // public string recorddestinationchanges { get; set; }
        public DispatchRoutesHistoryModel destinationroutes { get; set; }
    }
    public class RecordDestinationChangesResponse
    {
        [Required]
        public string result { get; set; }
        public string message { get; set; }
    }
   
}
