using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportVolLostTrippingSurfs
    {
        public OpsReportVolLostTrippingSurfs()
        {
            OpsReportMudLossess = new HashSet<OpsReportMudLossess>();
        }

        [Key]
        public int VolLostTrippingSurfId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolLostTrippingSurf")]
        public virtual ICollection<OpsReportMudLossess> OpsReportMudLossess { get; set; }
    }
}
