using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPenetrationLeg1s
    {
        public OpsReportPenetrationLeg1s()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int PenetrationLeg1Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PenetrationLeg1")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
