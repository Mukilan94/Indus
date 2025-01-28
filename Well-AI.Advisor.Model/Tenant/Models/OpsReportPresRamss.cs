using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPresRamss
    {
        public OpsReportPresRamss()
        {
            OpsReportHses = new HashSet<OpsReportHses>();
        }

        [Key]
        public int PresRamsId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresRams")]
        public virtual ICollection<OpsReportHses> OpsReportHses { get; set; }
    }
}
