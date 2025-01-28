using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMdHoleEnds
    {
        public OpsReportMdHoleEnds()
        {
            OpsReportActivitys = new HashSet<OpsReportActivitys>();
        }

        [Key]
        public int MdHoleEndId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdHoleEnd")]
        public virtual ICollection<OpsReportActivitys> OpsReportActivitys { get; set; }
    }
}
