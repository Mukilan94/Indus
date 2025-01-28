using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;


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
        public int Accounts { get; set; }
        public int OnlineContacts { get; set; }
        public string LastContact { get; set; }
        public int ScheduledAppointments { get; set; }
        public int UpcomingServices { get; set; }
        public int BidsWon { get; set; }
        public int OpenFieldTickets { get; set; }

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
        public double Value { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
    }
}
