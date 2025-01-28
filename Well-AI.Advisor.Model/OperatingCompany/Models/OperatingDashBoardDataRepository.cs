using System.Collections.Generic;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public partial class OperatingDashBoardDataRepository
    {
        public static IList<OperatingRig> Rigs()
        {
            return new OperatingRig[] {
                
            };
        }
    }

    public class OperatingDashboardModel
    {
        public int Rigs { get; set; }
        public string AwardedBidsVal { get; set; }
        public int AwardedBids { get; set; }
        public string OpenBidsVal { get; set; }
        public string OpenBidsCount { get; set; }

        public int ComplianceAlertCount { get; set; }
        public int Recommendations { get; set; }
        public int OpenFieldTickets { get; set; }

        public List<OperatingRig> DepthData;

        public List<OperatingRig> TimeData;
    }
    public class Operator
    {
        public int OperId { get; set; }
        public string Title { get; set; }
    }
    public class OperatingRig
    {
        public float Value { get; set; }
        public int Value2 { get; set; }

        public string Category { get; set; }
        public string Color { get; set; }
        public string WellId { get; set; }
        public string WellName { get; set; }
        public string RigName { get; set; }
        public string RigId { get; set; }

    }
}
