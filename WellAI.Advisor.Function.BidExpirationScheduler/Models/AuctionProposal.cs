using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.Function.BidExpirationScheduler.Models
{
    [Table("AuctionProposalViewDetail")]
    public class AuctionProposalViewModel
    {
        [Column("ProposalId")]
        [Key]
        [Required]
        [StringLength(40)]
        public string ProposalId { get; set; }
        public int BidStatusId { get; set; }
        public DateTime Created { get; set; }
        [StringLength(40)]
        public string TenantID { get; set; }
        [StringLength(40)]
        public string AuthorId { get; set; }
        [StringLength(40)]
        public string WellId { get; set; }
        [StringLength(40)]
        public string JobId { get; set; }
        public DateTime AuctionStart { get; set; }
        public DateTime AuctionEnd { get; set; }
        [StringLength(70)]
        public string Subject { get; set; }
        [StringLength(250)]
        public string Summary { get; set; }
        public string Body { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public double ProjectDuration { get; set; }
        public string AuctionNumber { get; set; }
        public string SRVTenantId { get; set; }
        public bool IsPrivate { get; set; }
        public string RigId { get; set; }
        public string Category { get; set; }
        public string WellName { get; set; }
        public string TaskName { get; set; }
        public string Rig_Name { get; set; }
    }

}
