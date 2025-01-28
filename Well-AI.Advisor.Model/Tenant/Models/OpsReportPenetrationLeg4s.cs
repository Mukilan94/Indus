using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPenetrationLeg4s
    {
        public OpsReportPenetrationLeg4s()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int PenetrationLeg4Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PenetrationLeg4")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
