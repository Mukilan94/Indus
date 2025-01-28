using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMeanDrafts
    {
        public OpsReportMeanDrafts()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int MeanDraftId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MeanDraft")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
