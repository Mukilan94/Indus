using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportTempRheom
    {
        public FluidsReportTempRheom()
        {
            FluidsReportRheometer = new HashSet<FluidsReportRheometer>();
        }

        [Key]
        public int TempRheomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempRheom")]
        public virtual ICollection<FluidsReportRheometer> FluidsReportRheometer { get; set; }
    }
}
