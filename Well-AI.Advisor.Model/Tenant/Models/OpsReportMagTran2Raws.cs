using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMagTran2Raws
    {
        public OpsReportMagTran2Raws()
        {
            OpsReportRawDatas = new HashSet<OpsReportRawDatas>();
        }

        [Key]
        public int MagTran2RawId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagTran2Raw")]
        public virtual ICollection<OpsReportRawDatas> OpsReportRawDatas { get; set; }
    }
}
