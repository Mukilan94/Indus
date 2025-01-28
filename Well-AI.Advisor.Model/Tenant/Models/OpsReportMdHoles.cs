using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMdHoles
    {
        public OpsReportMdHoles()
        {
            OpsReportShakerOps = new HashSet<OpsReportShakerOps>();
        }

        [Key]
        public int MdHoleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdHole")]
        public virtual ICollection<OpsReportShakerOps> OpsReportShakerOps { get; set; }
    }
}
