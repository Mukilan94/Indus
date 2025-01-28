using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public partial class ServiceDashBoardDataRepository
    {
        public static IList<ServiceRig> Rigs()
        {
            return new ServiceRig[] {

            };
        }
    }
    public class ServiceDashboardModel
    {
        public int Rigs { get; set; }
        public string AwardedBidsVal { get; set; }
        public int AwardedBids { get; set; }
        public string OpenBidsVal { get; set; }
        public int UpcomingServices { get; set; }
        public int Recommendations { get; set; }
        public int OpenFieldTickets { get; set; }
        public int UpcomingAppoinment { get;set; }


        public List<ServiceRig> DepthData;
        public List<ServiceRig> TimeData;
    }
    public class ServiceOperator
    {
        public int OperId { get; set; }
        public string Title { get; set; }
    }
    public class ServiceRig
    {
        public double? Value { get; set; }
        public int? Value2 { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string WellId { get; set; }
        public string OperatorTenantId { get; set; }
        public string WellName { get; set; }
        public string RigId { get; set; }
    }
   

}