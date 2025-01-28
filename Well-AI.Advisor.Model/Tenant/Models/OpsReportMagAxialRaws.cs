using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMagAxialRaws
    {
        public OpsReportMagAxialRaws()
        {
            OpsReportRawDatas = new HashSet<OpsReportRawDatas>();
        }

        [Key]
        public int MagAxialRawId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagAxialRaw")]
        public virtual ICollection<OpsReportRawDatas> OpsReportRawDatas { get; set; }
    }
}
