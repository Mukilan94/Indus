using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmTaskComments
    {
        public int CommentId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }

        public virtual CrmTasks Task { get; set; }
        public virtual CrmUsers User { get; set; }
    }
}
