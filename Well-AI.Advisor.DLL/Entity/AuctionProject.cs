using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("AuctionProject")]
    public class AuctionProject
    {
        [Key]
        [Required]
        [StringLength(40)]
        public string Id { get; set; }
        [Required]
        [StringLength(40)]
        public string BidID {get;set;}
        [Required]
        [StringLength(40)]
        public string ProposalId { get; set; }
        [Required]
        [StringLength(40)]
        public string ServiceTenantID { get; set; }
        [Required]
        [StringLength(40)]
        public string OperTenantID { get; set; }
        [StringLength(40)]
        public string AuthorId { get; set; }
        public DateTime BidDate { get; set; }
        public DateTime BidTime { get; set; }
        public decimal BidAmount { get; set; }
        [StringLength(1)]
        public int BidStatus { get; set; }
        [StringLength(224)]
        public string Title { get; set; }
        public bool Active { get; set; }

    }
}
