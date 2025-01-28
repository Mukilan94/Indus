using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportGravTran1Raws
    {
        public OpsReportGravTran1Raws()
        {
            OpsReportRawDatas = new HashSet<OpsReportRawDatas>();
        }

        [Key]
        public int GravTran1RawId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GravTran1Raw")]
        public virtual ICollection<OpsReportRawDatas> OpsReportRawDatas { get; set; }
    }
}
