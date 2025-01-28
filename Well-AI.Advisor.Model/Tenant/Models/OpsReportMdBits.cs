using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMdBits
    {
        public OpsReportMdBits()
        {
            OpsReportPumpOps = new HashSet<OpsReportPumpOps>();
            OpsReportScrs = new HashSet<OpsReportScrs>();
        }

        [Key]
        public int MdBitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdBit")]
        public virtual ICollection<OpsReportPumpOps> OpsReportPumpOps { get; set; }
        [InverseProperty("MdBit")]
        public virtual ICollection<OpsReportScrs> OpsReportScrs { get; set; }
    }
}
