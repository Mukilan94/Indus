using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class AuctionBidAmountHistoryViewModel
    {
        public string Id { get; set; }
        public string AuctionBidId { get; set; }
        public string AuthorId { get; set; }
        public DateTime BidDate { get; set; }
        public decimal BidAmount { get; set; }
        public string BidSummary { get; set; }
    }
}
