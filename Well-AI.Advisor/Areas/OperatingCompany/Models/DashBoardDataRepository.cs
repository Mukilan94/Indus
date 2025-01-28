using System.Collections.Generic;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public partial class DashBoardDataRepository
    {
        public static IList<Rig> Rigs()
        {
            return new Rig[] {
                
            };
        }
    }

    public class DashboardModel
    {
        public int Accounts { get; set; }
        public int OnlineContacts { get; set; }
        public string LastContact { get; set; }
        public int ScheduledAppointments { get; set; }
        public int UpcomingServices { get; set; }
        public int BidsWon { get; set; }
        public int OpenFieldTickets { get; set; }

        public List<Rig> DepthData;
        public List<Rig> TimeData;
    }
    public class Operator
    {
        public int OperId { get; set; }
        public string Title { get; set; }
    }
    public class Rig
    {
        public double Value { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
    }
}
