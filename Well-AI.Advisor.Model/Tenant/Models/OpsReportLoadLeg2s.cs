using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportLoadLeg2s
    {
        public OpsReportLoadLeg2s()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int LoadLeg2Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LoadLeg2")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
