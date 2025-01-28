using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPenetrationLeg2s
    {
        public OpsReportPenetrationLeg2s()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int PenetrationLeg2Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PenetrationLeg2")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
