using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportGravTotalUncerts
    {
        public OpsReportGravTotalUncerts()
        {
            OpsReportTrajectoryStations = new HashSet<OpsReportTrajectoryStations>();
        }

        [Key]
        public int GravTotalUncertId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GravTotalUncert")]
        public virtual ICollection<OpsReportTrajectoryStations> OpsReportTrajectoryStations { get; set; }
    }
}
