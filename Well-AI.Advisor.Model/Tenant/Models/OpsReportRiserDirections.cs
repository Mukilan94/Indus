using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportRiserDirections
    {
        public OpsReportRiserDirections()
        {
            OpsReportRigResponses = new HashSet<OpsReportRigResponses>();
        }

        [Key]
        public int RiserDirectionId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RiserDirection")]
        public virtual ICollection<OpsReportRigResponses> OpsReportRigResponses { get; set; }
    }
}
