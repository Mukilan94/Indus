using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class DispatchViewModel
    {
        public string user { get; set; }
        public string dispatchId { get; set; }
        public string userId { get; set; }
        public string location { get; set; }
        public string customer { get; set; }
        public string customerId { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string username { get; set; }
    }


    public class UserLocationModel
    {
        public string userId { get; set; }
        public string currentLocation { get; set; }
    }

    public class UserRouteModel
    {
        public string userId { get; set; }
        public string destinationSequence { get; set; }
        public string destinationName { get; set; }
        public string dispatchNotes { get; set; }
    }
    public class DispatchRoutesModel
    {
        public string dispatchid { get; set; }
        public string userid { get; set; }
        public DateTime createddate { get; set; }
        public string customer { get; set; }
        public string locationname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    
        public string dispatchnotes { get; set; }
        public int routeorder { get; set; }
        public string username { get; set; }
        public string api { get; set; }
        public string wellname { get; set; }
        public string rigname { get; set; }
        public string wellid { get; set; }
        public string rigid { get; set; }
        //public bool ismodified { get; set; }
        //public DateTime? modifieddate { get; set; }
        public UsersOptions usersoptions { get; set; }

        public string rigwellandlocation { get; set; }
        public int? currentrouterorder { get; set; }
        public string? recordstatus { get; set; }
        public string? modificationStatus { get; set; }

        public string? subscriptiontype { get; set; }
        public string? subscriptionstatus { get; set; }
        public bool? islocationshared { get; set; }
        public long? activityid { get; set; }
        public string operatorId { get; set; }
        public DateTime? scheduledArrivalDate { get; set; }

        public string? routstatus { get; set; }
        public DateTime? eta { get; set; }
    }

    
    public class DispatchRouteOrderModel
    {
        public List<DispatchRoutesModel> DispatchRoutesModel { get; set; }
    }


    public class DispatchUserStatusCount
    {
        public int ACTIVE { get; set; }
        public int INACTIVE { get; set; }
        public int ONROUTE { get; set; }
        public int ONSITE { get; set; }
        public int OVERDUE { get; set; }
    }
        public class DispatchRoutesViewModel
    {
        public string? dispatchid { get; set; }
        public string? userid { get; set; }
        public string? createddate { get; set; }
        public string? customer { get; set; }
        public string? locationname { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? zip { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string? dispatchnotes { get; set; }
        public int? routeorder { get; set; }
        public string? username { get; set; }
        public string? api { get; set; }
        public string? wellname { get; set; }
        public string? rigname { get; set; }
        public string? wellid { get; set; }
        public string? rigid { get; set; }
        public bool? ismodified { get; set; }
        public string? modifieddate { get; set; }
        public long? activityid { get; set; }
        public string? operatorId { get; set; }
        public DateTime? scheduledArrivalDate { get; set; }
        public string? routestatus { get; set; }
        public DateTime? eta { get; set; }
    }
    public class DispatchDelete
    {
        public string user_key { get; set; }

    }
    public class DispatchNotification
    {
        public string user_key { get; set; }
        public Destination[] destinations { get; set; }
        public bool optimize { get; set; }
        public string message { get; set; }
    }
    public class Destination
    {
        [System.ComponentModel.DisplayName("type")]
        public string type { get; set; }
        public long? id { get; set; }
        public string address { get; set; }
        public bool? last { get; set; }
    }
    /// <summary>
    /// Dispatch Routes History 
    /// </summary>
    public class DispatchRoutesHistoryModel
    {  public string historyId { get; set; }
    public string userid { get; set; }
/// <summary>
///   A - Advisor, R - Router
/// </summary>
        public string dispatchfrom { get; set; }
        public string dispatchnotes { get; set; }
        public List<DispatchRoutesHistoryDetailsModel> routes { get; set; }
        
    }

    public class DispatchRoutesHistoryDetailsModel
    {
        public string dispatchid { get; set; }
        public string customer { get; set; }
        public string locationname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string apinumber { get; set; }
        public string wellname { get; set; }
        public string rigname { get; set; }
        public string wellid { get; set; }
        public string rigid { get; set; }
        
    }
    public class OperatorWithServiceDriverInfo
    {
        public string userId { get; set; }
        public string operatorId { get; set; }
        public int? activityId { get; set; }
    }


}