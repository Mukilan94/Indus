using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmCompanies
    {
        public int CompanyId { get; set; }
        public int InstanceId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Comments { get; set; }
        public int? LatestCommentId { get; set; }
        public int? AssignedToUserId { get; set; }
        public string Fax { get; set; }
        public string Category { get; set; }
        public string Ein { get; set; }
        public string UserId { get; set; }
        public string TenantId { get; set; }
    }
}
