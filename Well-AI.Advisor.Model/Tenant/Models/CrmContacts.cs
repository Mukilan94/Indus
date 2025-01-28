using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmContacts
    {
        public int ContactId { get; set; }
        public int InstanceId { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
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
        public bool IsLead { get; set; }
        public int LeadStatus { get; set; }
    }
}
