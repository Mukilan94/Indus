using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class AuctionBidderDetailsViewModel
    {
        
        public string ProposalId { get; set; }
        public string AuthorId { get; set; }
        public string BidId { get; set; }
        [Display(Name =("Bid Amount"))]
        public decimal BidAmount { get; set; }
        
        [Display(Name =("Bid Description"))]
        public string BidDescription { get; set; }
        [Display(Name = ("Company"))]
        public string ServiceCompany { get; set; }
        public string ServiceTenantId { get; set; }
        public string OperTenantId { get; set; }
        [Display(Name = ("Name"))]
        public string BidderName { get; set; }
        [Display(Name = ("Mobile"))]
        public string BidderMobile { get; set; }
        [Display(Name = ("Bidder Email"))]
        public string BidderEmail { get; set; }
        [Display(Name = ("Bid Status"))]
        public int? BidStatusId { get; set; }
        public string BidStatusName { get; set; }
        [Display(Name = ("Bid Date Time"))]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime BidDate { get; set; }
        public string Title { get; set; }
        public List<AuctionProposalAttachmentViewModel> AuctionProposalAttachments { get; set; }
    }
}
