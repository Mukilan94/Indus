using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmContactImages
    {
        public int ContactId { get; set; }
        public string ImageFileName { get; set; }
        public byte[] ImageFileData { get; set; }
    }
}
