using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("AuctionBidAmountHistory")]
    public class AuctionBidAmountHistory
    {
        [Key]
        public string Id { get; set; }
        [StringLength(50)]
        public string AuctionBidId { get; set; }
        [StringLength(50)]
        public string AuthorId { get; set; }
        public DateTime BidDate { get; set; }
        public decimal BidAmount { get; set; }
        public string BidSummary { get; set; }
    }
}
