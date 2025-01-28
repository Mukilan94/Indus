using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellAI.Advisor.Model.Tenant.Models;


namespace WellAI.Advisor.DLL.Entity
{
    [Table("DispatchRoutes")]
   // [Table("DispatchRoutes")]
    public class DispatchRoutes
    { 
        [Key]
        public string DispatchId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; }
        public string? Customer { get; set; }
        public string? LocationName { get; set; }
        public string? LocationAddress { get; set; }
        public string? LocationCity { get; set; }
        public string? LocationState { get; set; }
        public string? LocationZip { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? DispatchNotes { get; set; }
        public int RouteOrder { get; set; }
        public string? APINumber { get; set; }
        public string? WellName { get; set; }
        public string? RigName { get; set; }
        public string? WellId { get; set; }
        public string? RigId { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string? RecordStatus { get; set; }
        public int? CurrentRouterOrder { get; set; }
        //public int? ChangedRouterOrder { get; set; }

        public bool? IsLocationShared { get; set; }
        public Int64? ActivityId { get; set; }
        public string? OperatorId { get; set; }
        public DateTime? ScheduledArrival { get; set; }
        public string? RouteStatus { get; set; }
        public DateTime? ETA { get; set; }
        
        //public DateTime? ModifiedDate { get; set; }
        //public bool IsFromAdvisor { get; set; }
        //public bool IsModified { get; set; }


    }
   
}
