using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportProjectedXs
    {
        public OpsReportProjectedXs()
        {
            OpsReportLocations = new HashSet<OpsReportLocations>();
        }

        [Key]
        [Column("ProjectedXId")]
        public int ProjectedXid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ProjectedX")]
        public virtual ICollection<OpsReportLocations> OpsReportLocations { get; set; }
    }
}
