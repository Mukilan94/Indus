using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportValids
    {
        public OpsReportValids()
        {
            OpsReportTrajectoryStations = new HashSet<OpsReportTrajectoryStations>();
        }

        [Key]
        public int ValidId { get; set; }
        public int? MagTotalFieldCalcId { get; set; }
        public int? MagDipAngleCalcId { get; set; }
        public int? GravTotalFieldCalcId { get; set; }

        [ForeignKey(nameof(GravTotalFieldCalcId))]
        [InverseProperty(nameof(OpsReportGravTotalFieldCalcs.OpsReportValids))]
        public virtual OpsReportGravTotalFieldCalcs GravTotalFieldCalc { get; set; }
        [ForeignKey(nameof(MagDipAngleCalcId))]
        [InverseProperty(nameof(OpsReportMagDipAngleCalcs.OpsReportValids))]
        public virtual OpsReportMagDipAngleCalcs MagDipAngleCalc { get; set; }
        [ForeignKey(nameof(MagTotalFieldCalcId))]
        [InverseProperty(nameof(OpsReportMagTotalFieldCalcs.OpsReportValids))]
        public virtual OpsReportMagTotalFieldCalcs MagTotalFieldCalc { get; set; }
        [InverseProperty("Valid")]
        public virtual ICollection<OpsReportTrajectoryStations> OpsReportTrajectoryStations { get; set; }
    }
}
