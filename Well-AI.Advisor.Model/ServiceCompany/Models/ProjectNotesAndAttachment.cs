using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class ProjectNotesAndAttachment
    {
        [StringLength(40)]
        public string AttachmentId { get; set; }
        [Display(Name = ("File Name"))]
        [StringLength(150)]
        public string FileName { get; set; }
        public string TenantId { get; set; }
        public string ProposalId { get; set; }
        public string ProjectId { get; set; }
        public string AuthorId { get; set; }
        public DateTime DateUpload { get; set; }

    }
}
