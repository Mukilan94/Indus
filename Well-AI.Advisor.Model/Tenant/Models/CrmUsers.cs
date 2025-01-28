using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmUsers
    {
        public CrmUsers()
        {
            CrmTaskComments = new HashSet<CrmTaskComments>();
        }

        public int UserId { get; set; }
        public int InstanceId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public bool Disabled { get; set; }
        public string EmailSignature { get; set; }
        public DateTime? LastSeen { get; set; }
        public string Ipaddress { get; set; }

        public virtual ICollection<CrmTaskComments> CrmTaskComments { get; set; }
    }
}
