using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmProjects
    {
        public int ProjectId { get; set; }
        public int InstanceId { get; set; }
        public string Name { get; set; }
        public bool IsClosed { get; set; }
        public int? LatestCommentId { get; set; }
        public int? AssignedToUserId { get; set; }
        public decimal? Value { get; set; }
        public string ValueCurrency { get; set; }
    }
}
