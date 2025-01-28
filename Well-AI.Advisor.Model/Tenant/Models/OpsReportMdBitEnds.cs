using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMdBitEnds
    {
        public OpsReportMdBitEnds()
        {
            OpsReportActivitys = new HashSet<OpsReportActivitys>();
        }

        [Key]
        public int MdBitEndId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdBitEnd")]
        public virtual ICollection<OpsReportActivitys> OpsReportActivitys { get; set; }
    }
}
