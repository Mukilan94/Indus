using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportLoadLeg4s
    {
        public OpsReportLoadLeg4s()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int LoadLeg4Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("LoadLeg4")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
