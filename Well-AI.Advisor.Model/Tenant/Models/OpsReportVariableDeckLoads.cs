using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVariableDeckLoads
    {
        public OpsReportVariableDeckLoads()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int VariableDeckLoadId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VariableDeckLoad")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
