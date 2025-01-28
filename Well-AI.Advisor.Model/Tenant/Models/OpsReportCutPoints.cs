using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportCutPoints
    {
        public OpsReportCutPoints()
        {
            OpsReportShakerScreens = new HashSet<OpsReportShakerScreens>();
        }

        [Key]
        public int CutPointId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CutPoint")]
        public virtual ICollection<OpsReportShakerScreens> OpsReportShakerScreens { get; set; }
    }
}
