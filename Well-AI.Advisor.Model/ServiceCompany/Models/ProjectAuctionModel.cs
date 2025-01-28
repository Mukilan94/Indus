using System;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class ProjectAuctionModel
    {
        [ScaffoldColumn(false)]
        public string AuctionID
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
        public int ProjectsStartedLastMonthCount { get; set; }
        public double ProjectsStartedLastMonthValue { get; set; }
        public DateTime ProjectsStartedLastMonthDate { get; set; }
        public int ProjectsStartedThisMonthCount { get; set; }
        public double ProjectsStartedThisMonthValue { get; set; }
        public DateTime ProjectsStartedThisMonthDate { get; set; }

        public int ActiveBidsCount { get; set; }
        public double ActiveBidsValue { get; set; }
        public DateTime ActiveBidsSinceDate { get; set; }

        public int AwardedBidsLastMonthCount { get; set; }
        public double AwardedBidsLastMonthValue { get; set; }
        public DateTime AwardedBidsLastMonthDate { get; set; }
        public int AwardedBidsThisMonthCount { get; set; }
        public double AwardedBidsThisMonthValue { get; set; }
        public DateTime AwardedBidsThisMonthDate { get; set; }

        public List<AuctionBidViewModel> Bids;
    }

    public class AuctionBidViewModel
    {
        [Display(Name = "Proposal Id")]
        public string ProposalId { get; set; }
        [Display(Name = "Auction Status")]
        public string AuctionBidStatusName { get; set; }
        public int? AuctionBidStatus { get; set; }
        public DateTime Created { get; set; }
        [Display(Name = "Operator")]
        public string TenantID { get; set; }

        [Display(Name = "Well")]
        public string WellName { get; set; }
        [Display(Name = "Rig")]
        public string RigName { get; set; }
        [Display(Name = "Job")]
        public string JobId { get; set; }
        [Display(Name = "Start")]
        public DateTime AuctionStart { get; set; }
        [Display(Name = "End")]
        public DateTime AuctionEnd { get; set; }
        [Display(Name = "Title")]
        public string Subject { get; set; }
        [Display(Name = "Summary")]
        public string Summary { get; set; }
        [Display(Name ="Description")]
        public string Body { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        [Display(Name = "Project Start Time")]
        public DateTime? ProjectStartTime { get; set; }
        [Display(Name = "Duration")]
        public double? ProjectDuration { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }
        public int? BidStatusId { get; set; }
        [Display(Name ="Attachement")]
        public string BidStatusName { get; set; }
        public string AuthorId { get; set; }
        public string BidID { get; set; }
        public string BidAttachmentUrl { get; set; }
        public int? Depth { get; set; }

        [Display(Name = "Number")] 
        public string AuctionNumber { get; set; }

        [MaxLength(300)]
        public string BidSummary { get; set; }

        public int Bids { get; set; }

        public string JobName { get; set; }
        [Display(Name = "Modified Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifyDate { get; set; }
        public string SRVTenantId { get; set; }

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
