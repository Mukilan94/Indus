using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("AuctionProposalAttachments")]
    public class AuctionProposalAttachments
    {
        [Key]
        [StringLength(40)]
        public string AttachmentId { get; set; }
        [Display(Name = ("File Name"))]
        [StringLength(150)] 
        public string FileName { get; set; }
        [Display(Name = ("File Patch"))]
        [StringLength(340)] 
        public string FilePatch { get; set; }
        [StringLength(40)] 
        public string ProposalID { get; set; }
        [Display(Name = ("Date Uploaded"))]
        public DateTime DateUploaded { get; set; }
        [Display(Name = ("Tenant"))]
        [StringLength(40)] 
        public string TenantID { get; set; }
    }
    [Table("AuctionProposalOperatorAttachments")]
    public class AuctionProposalOperatorAttachments
    {
        [Key]
        [StringLength(40)]
        public string AttachmentId { get; set; }
        [Display(Name = ("File Name"))]
        [StringLength(150)]
        public string FileName { get; set; }
        [Display(Name = ("File Patch"))]
        [StringLength(340)]
        public string FilePatch { get; set; }
        [StringLength(40)]
        public string ProposalID { get; set; }
        [Display(Name = ("Date Uploaded"))]
        public DateTime DateUploaded { get; set; }
        [Display(Name = ("Tenant"))]
        [StringLength(40)]
        public string TenantID { get; set; }
    }
}
