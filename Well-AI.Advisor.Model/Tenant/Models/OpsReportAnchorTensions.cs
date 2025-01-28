using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportAnchorTensions
    {
        [Key]
        public int AnchorTensionId { get; set; }
        public string Uom { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }
        public string Text { get; set; }
        public int? OpsReportRigResponseRigResponseId { get; set; }

        [ForeignKey(nameof(OpsReportRigResponseRigResponseId))]
        [InverseProperty(nameof(OpsReportRigResponses.OpsReportAnchorTensions))]
        public virtual OpsReportRigResponses OpsReportRigResponseRigResponse { get; set; }
    }
}
