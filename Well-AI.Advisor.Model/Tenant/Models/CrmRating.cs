using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmRating
    {
        public CrmRating()
        {
            CrmOpportunity = new HashSet<CrmOpportunity>();
        }

        public string RatingId { get; set; }
        public string ColorHex { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public string RatingName { get; set; }

        public virtual ICollection<CrmOpportunity> CrmOpportunity { get; set; }
    }
}
