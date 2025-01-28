using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class ProjectAuctionModel
    {
        [ScaffoldColumn(false)]
        public string AuctionID { get; set; }

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

        public List<AuctionProposalViewModel> Bids;
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
    public class AddAuctionProposalViewModel: AuctionProposalViewModel
    {
        public List<IFormFile> files { get; set; }
    }
    public class AuctionProposalViewModel
    {
        [Display(Name = "Id")]
        public string ProposalId { get; set; }
        [Display(Name = "Auction Status")]
        public string AuctionBidStatusName { get; set; }
        public int AuctionBidStatusOrder { get; set; }
        [Display(Name = "Status")]
        public int AuctionBidStatusId { get; set; }

        [Display(Name = "Created Date")]
        public DateTime Created { get; set; }
        [Display(Name = "Operator Name")]
        public string TenantID { get; set; }
        [Display(Name = "Created By")]
        public string AuthorId { get; set; }
        [Display(Name = "Well")]
        public string WellId { get; set; }
        [Display(Name = "Well")]
        public string WellName { get; set; }
        
        [Display(Name = "Rig")]
        public string RigId { get; set; }
        [Display(Name = "Rig Name")]
        public string RigName { get; set; }

        [Display(Name ="Service")]
        [Required(ErrorMessage ="Required")]
        public string JobId { get; set; }
        [Display(Name = "Job Start Date")]
        //[DataType(DataType.Date)] 
        [DataType(DataType.DateTime)]
        public DateTime AuctionStart { get; set; }
        [Display(Name = "Bid Close")]
        [DataType(DataType.DateTime)] 
        public DateTime AuctionEnd { get; set; }
        [Display(Name = "Title")]
        public string Subject { get; set; }
        [Display(Name = "Summary")]
        public string Summary { get; set; }
        [Required(ErrorMessage ="Required")]
        public string Body { get; set; }
        [Display(Name = "Bidding Start Date")]
        [DataType(DataType.DateTime)] 
        public DateTime ProjectStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ProjectStart { get { return (ProjectStartDate.Date); } }

        [Display(Name = "Bidding Duration(hrs)")]

        [Required(ErrorMessage ="Required")]
        public double ProjectDuration { get; set; }
        [Display(Name = "Number")]
        public string AuctionNumber { get; set; }
        [Display(Name = "Service Company")]
        public string SRVTenantId { get; set; }
        [Display(Name = "Assign To Vendor")]
        public bool? IsPrivate { get; set; }
        public int Bids { get; set; }
        public List<AuctionBidderDetailsViewModel> AuctionBidderDetailsViewModels { get; set; }
        [Display(Name = "Category")]
        public string ServiceCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string JobName { get; set; }
        public decimal MinBidsValue { get; set; }
        public decimal MaxBidsValue { get; set; }
        public decimal BidsAmount { get; set; }
        public int? Depth { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifyDate { get; set; }
        [Display(Name ="Assign Vendor")]
        public string VendorName { get; set; }

    }
}
