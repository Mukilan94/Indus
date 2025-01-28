using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMagTran1Raws
    {
        public OpsReportMagTran1Raws()
        {
            OpsReportRawDatas = new HashSet<OpsReportRawDatas>();
        }

        [Key]
        public int MagTran1RawId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagTran1Raw")]
        public virtual ICollection<OpsReportRawDatas> OpsReportRawDatas { get; set; }
    }
}
