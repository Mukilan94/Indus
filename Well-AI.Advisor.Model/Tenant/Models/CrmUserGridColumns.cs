using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmUserGridColumns
    {
        public int UserId { get; set; }
        public bool ShowEmailColumn { get; set; }
        public bool ShowPhoneColumn { get; set; }
        public bool ShowFaxColumn { get; set; }
        public bool ShowAddressColumn { get; set; }
        public bool ShowCityColumn { get; set; }
        public bool ShowStateColumn { get; set; }
        public bool ShowCountryColumn { get; set; }
        public bool ShowLastNoteColumn { get; set; }
        public bool ShowAssigneeColumn { get; set; }
        public string CustomFieldsToShowInContacts { get; set; }
        public string CustomFieldsToShowInCompanies { get; set; }
    }
}
