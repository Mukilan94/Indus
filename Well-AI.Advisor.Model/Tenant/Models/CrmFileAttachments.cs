using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmFileAttachments
    {
        public int FileId { get; set; }
        public int? CommentId { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
        public string GoogleDriveUrl { get; set; }
    }
}
