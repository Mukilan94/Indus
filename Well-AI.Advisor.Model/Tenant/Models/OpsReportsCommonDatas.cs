using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportsCommonDatas
    {
        public OpsReportsCommonDatas()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int OpsReportsCommonDataid { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataOpsReportsCommonData")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
