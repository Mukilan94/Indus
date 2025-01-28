using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class ProjectAuctionModel
    {
        [ScaffoldColumn(false)]
        public int AuctionID
        {
            get;
            set;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Seller { get; set; }
        public string Status { get; set; }
        public string Attachment { get; set; }

        public string Location
        {
            get;
            set;
        }
    }

    public class AuctionBidsModel
    {
        public int ProjectsStartedLastMonthCount;
        public double ProjectsStartedLastMonthValue;
        public DateTime ProjectsStartedLastMonthDate;
        public int ProjectsStartedThisMonthCount;
        public double ProjectsStartedThisMonthValue;
        public DateTime ProjectsStartedThisMonthDate;

        public int ActiveBidsCount;
        public double ActiveBidsValue;
        public DateTime ActiveBidsSinceDate;

        public int AwardedBidsLastMonthCount;
        public double AwardedBidsLastMonthValue;
        public DateTime AwardedBidsLastMonthDate;
        public int AwardedBidsThisMonthCount;
        public double AwardedBidsThisMonthValue;
        public DateTime AwardedBidsThisMonthDate;

        public List<AuctionBid> Bids;
    }

    public class AuctionBid
    {
        public string ProcurementNumber { get; set; }
        public string ProcurementTitle { get; set; }
        public double EstimatedValue { get; set; }
        public string ProcurementCategory { get; set; }
        public string CurrentStage { get; set; }
        public string ProcurementOfficer { get; set; }
        public string Department { get; set; }
        public DateTime? Date { get; set; }
    }
}
