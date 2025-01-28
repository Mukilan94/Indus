using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class AuctionProposalAttachmentViewModel
    {
        [StringLength(40)]
        public string AttachmentId { get; set; }
        [Display(Name = ("File Name"))]
        [StringLength(150)]
        public string FileName { get; set; }
        public string TableName { get; set; }
        public string TenantId { get; set; }
        public string ProposalId { get; set; }
        [Display(Name ="Date")]
        [DisplayFormat(DataFormatString ="{0:mm/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime? DateUploaded { get; set; }

    }

    public class AddProjectAttachmentViewModel
    {
        [StringLength(40)]
        public string AttachmentId { get; set; }
        public string ProjectId { get; set; }
        [Display(Name = ("File Name"))]
        [StringLength(150)]
        public string FileName { get; set; }

        public string TenantId { get; set; }
        public string ProposalId { get; set; }

        public IFormFile[] files { get; set; }
        public string AuthorId { get; set; }
        public string Note { get; set; }
        public string ProjectCode { get; set; }
        public string OperatorTenantId { get; set; }
    }
}
