using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMtfs
    {
        public OpsReportMtfs()
        {
            OpsReportTrajectoryStations = new HashSet<OpsReportTrajectoryStations>();
        }

        [Key]
        public int MtfId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Mtf")]
        public virtual ICollection<OpsReportTrajectoryStations> OpsReportTrajectoryStations { get; set; }
    }
}
