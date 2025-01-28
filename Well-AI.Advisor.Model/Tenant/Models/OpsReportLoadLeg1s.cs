using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportLoadLeg1s
    {
        public OpsReportLoadLeg1s()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int LoadLeg1Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LoadLeg1")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
