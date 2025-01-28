using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMagTotalFieldCalcs
    {
        public OpsReportMagTotalFieldCalcs()
        {
            OpsReportValids = new HashSet<OpsReportValids>();
        }

        [Key]
        public int MagTotalFieldCalcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagTotalFieldCalc")]
        public virtual ICollection<OpsReportValids> OpsReportValids { get; set; }
    }
}
