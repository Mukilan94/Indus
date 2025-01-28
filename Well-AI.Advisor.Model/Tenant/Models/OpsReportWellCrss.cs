using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("OpsReportWellCRSs")]
    public partial class OpsReportWellCrss
    {
        public OpsReportWellCrss()
        {
            OpsReportLocations = new HashSet<OpsReportLocations>();
        }

        [Key]
        [Column("WellCRSId")]
        public int WellCrsid { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("WellCrs")]
        public virtual ICollection<OpsReportLocations> OpsReportLocations { get; set; }
    }
}
