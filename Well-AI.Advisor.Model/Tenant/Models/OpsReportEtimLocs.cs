using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportETimLocs")]
    public partial class OpsReportEtimLocs
    {
        public OpsReportEtimLocs()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        [Column("ETimLocId")]
        public int EtimLocId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimLoc")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
