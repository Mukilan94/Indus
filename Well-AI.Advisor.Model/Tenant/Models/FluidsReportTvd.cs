using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportTvd
    {
        public FluidsReportTvd()
        {
            FluidsReports = new HashSet<FluidsReports>();
        }

        [Key]
        public int TvdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Tvd")]
        public virtual ICollection<FluidsReports> FluidsReports { get; set; }
    }
}
