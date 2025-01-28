using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmComments
    {
        public int CommentId { get; set; }
        public int ContactId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime CommentDate { get; set; }
        public int? UserId { get; set; }
        
        public string Body { get; set; }
        public int CommentType { get; set; }
        public int InstanceId { get; set; }

    }
}
