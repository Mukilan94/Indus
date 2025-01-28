using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportTvdCsgLasts
    {
        public OpsReportTvdCsgLasts()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int TvdCsgLastId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdCsgLast")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
