using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("AuctionBids")]
    public class AuctionBids
    {
        [Key]
        [StringLength(40)]
        public string BidID {get;set;}
        [StringLength(40)]
        public string ProposalId { get; set; }
        [StringLength(40)]
        public string TenantID { get; set; }
        [StringLength(40)]
        public string AuthorId { get; set; }
        public DateTime BidDate { get; set; }
        public DateTime BidTime { get; set; }
        public decimal BidAmount { get; set; }
        [StringLength(1)]
        public int? BidStatus { get; set; }
        public string BidSummary { get; set; }

    }
}
