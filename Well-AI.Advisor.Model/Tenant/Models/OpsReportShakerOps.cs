using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportShakerOps
    {
        public OpsReportShakerOps()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int ShakerOpId { get; set; }
        public int? ShakerId { get; set; }
        public int? MdHoleId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? HoursRunId { get; set; }
        public int? PcScreenCoveredId { get; set; }
        public int? ShakerScreenId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(HoursRunId))]
        [InverseProperty(nameof(OpsReportHoursRuns.OpsReportShakerOps))]
        public virtual OpsReportHoursRuns HoursRun { get; set; }
        [ForeignKey(nameof(MdHoleId))]
        [InverseProperty(nameof(OpsReportMdHoles.OpsReportShakerOps))]
        public virtual OpsReportMdHoles MdHole { get; set; }
        [ForeignKey(nameof(PcScreenCoveredId))]
        [InverseProperty(nameof(OpsReportPcScreenCovereds.OpsReportShakerOps))]
        public virtual OpsReportPcScreenCovereds PcScreenCovered { get; set; }
        [ForeignKey(nameof(ShakerId))]
        [InverseProperty(nameof(OpsReportShakers.OpsReportShakerOps))]
        public virtual OpsReportShakers Shaker { get; set; }
        [ForeignKey(nameof(ShakerScreenId))]
        [InverseProperty(nameof(OpsReportShakerScreens.OpsReportShakerOps))]
        public virtual OpsReportShakerScreens ShakerScreen { get; set; }
        [InverseProperty("ShakerOp")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
