using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmTasks
    {
        public CrmTasks()
        {
            CrmTaskComments = new HashSet<CrmTaskComments>();
        }

        public int TaskId { get; set; }
        public int? ContactId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime? DueDate { get; set; }
        public string TaskText { get; set; }
        public bool IsCompleted { get; set; }
        public bool SendReminder { get; set; }
        public int? AssignedToUserId { get; set; }

        public virtual ICollection<CrmTaskComments> CrmTaskComments { get; set; }
    }
}
