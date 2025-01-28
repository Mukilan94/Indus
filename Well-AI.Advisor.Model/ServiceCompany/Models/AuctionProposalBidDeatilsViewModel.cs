using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class AuctionProposalBidDeatilsViewModel
    {
        [Display(Name = "Proposal Id")]
        public string ProposalId { get; set; }
        [Display(Name = "Status")]
        public string AuctionBidStatusName { get; set; }
        public DateTime Created { get; set; }
        [Display(Name = "Operator")]
        public string TenantID { get; set; }

        [Display(Name = "Well")]
        public string WellName { get; set; }
        [Display(Name = "Job")]
        public string JobId { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start")]
        public DateTime AuctionStart { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End")]
        public DateTime AuctionEnd { get; set; }
        [Display(Name = "Project")]
        public string Subject { get; set; }
        [Display(Name = "Summary")]
        public string Summary { get; set; }
        [Display(Name = "Description")]
        public string Body { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        [Display(Name = "Project Start")]
        public DateTime? ProjectStartTime { get; set; }
        [Display(Name = "Duration")]
        public double? ProjectDuration { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Bid Amount")] 
        public decimal BidAmount { get; set; }
        public DateTime BidTime { get; set; }
        public int? BidStatusId { get; set; }
        [Display(Name = "Attachement")]
        public string BidStatusName { get; set; }
        public string AuthorId { get; set; }
        public string BidID { get; set; }
        public List<IFormFile> files { get; set; }

        [MaxLength(1000)]
        [Required(ErrorMessage = "Required")]
        [Display(Name ="Description")]
        public string BidSummary { get; set; }
        [Display(Name ="Attachment")]
        public string BidAttachmentUrl { get; set; }
        [Display(Name ="Number")]
        public string AuctionNumber { get; set; }
        [Display(Name ="Rig")]
        public string RigName { get; set; }
        public string JobName { get; set; }

        public DateTime BidDate { get; set; }
        public List<AuctionBidAmountHistoryViewModel> auctionBidAmountHistoryViewModels { get; set; }
        public List<AuctionProposalAttachmentViewModel> auctionProposalAttachments { get; set; }
        
    }
}
