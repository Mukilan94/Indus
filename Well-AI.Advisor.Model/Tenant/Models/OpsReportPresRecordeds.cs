using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPresRecordeds
    {
        public OpsReportPresRecordeds()
        {
            OpsReportScrs = new HashSet<OpsReportScrs>();
        }

        [Key]
        public int PresRecordedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresRecorded")]
        public virtual ICollection<OpsReportScrs> OpsReportScrs { get; set; }
    }
}
