using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportStatusInfo
    {
        public DrillReportStatusInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int StatusInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public int? MdPlugTopId { get; set; }
        public int? DiaHoleId { get; set; }
        public int? MdDiaHoleStartId { get; set; }
        public int? DiaPilotId { get; set; }
        public int? MdDiaPilotPlanId { get; set; }
        public int? MdKickoffId { get; set; }
        public int? StrengthFormId { get; set; }
        public int? MdStrengthFormId { get; set; }
        public int? DiaCsgLastId { get; set; }
        public int? MdCsgLastId { get; set; }
        public string PresTestType { get; set; }
        public int? DistDrillId { get; set; }
        public string Sum24Hr { get; set; }
        public string Forecast24Hr { get; set; }
        public int? RopCurrentId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(DiaCsgLastId))]
        [InverseProperty(nameof(DrillReportDiaCsgLast.DrillReportStatusInfo))]
        public virtual DrillReportDiaCsgLast DiaCsgLast { get; set; }
        [ForeignKey(nameof(DiaHoleId))]
        [InverseProperty(nameof(DrillReportDiaHole.DrillReportStatusInfo))]
        public virtual DrillReportDiaHole DiaHole { get; set; }
        [ForeignKey(nameof(DiaPilotId))]
        [InverseProperty(nameof(DrillReportDiaPilot.DrillReportStatusInfo))]
        public virtual DrillReportDiaPilot DiaPilot { get; set; }
        [ForeignKey(nameof(DistDrillId))]
        [InverseProperty(nameof(DrillReportDistDrill.DrillReportStatusInfo))]
        public virtual DrillReportDistDrill DistDrill { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(DrillReportMd.DrillReportStatusInfo))]
        public virtual DrillReportMd Md { get; set; }
        [ForeignKey(nameof(MdCsgLastId))]
        [InverseProperty(nameof(DrillReportMdCsgLast.DrillReportStatusInfo))]
        public virtual DrillReportMdCsgLast MdCsgLast { get; set; }
        [ForeignKey(nameof(MdDiaHoleStartId))]
        [InverseProperty(nameof(DrillReportMdDiaHoleStart.DrillReportStatusInfo))]
        public virtual DrillReportMdDiaHoleStart MdDiaHoleStart { get; set; }
        [ForeignKey(nameof(MdDiaPilotPlanId))]
        [InverseProperty(nameof(DrillReportMdDiaPilotPlan.DrillReportStatusInfo))]
        public virtual DrillReportMdDiaPilotPlan MdDiaPilotPlan { get; set; }
        [ForeignKey(nameof(MdKickoffId))]
        [InverseProperty(nameof(DrillReportMdKickoff.DrillReportStatusInfo))]
        public virtual DrillReportMdKickoff MdKickoff { get; set; }
        [ForeignKey(nameof(MdPlugTopId))]
        [InverseProperty(nameof(DrillReportMdPlugTop.DrillReportStatusInfo))]
        public virtual DrillReportMdPlugTop MdPlugTop { get; set; }
        [ForeignKey(nameof(MdStrengthFormId))]
        [InverseProperty(nameof(DrillReportMdStrengthForm.DrillReportStatusInfo))]
        public virtual DrillReportMdStrengthForm MdStrengthForm { get; set; }
        [ForeignKey(nameof(RopCurrentId))]
        [InverseProperty(nameof(DrillReportRopCurrent.DrillReportStatusInfo))]
        public virtual DrillReportRopCurrent RopCurrent { get; set; }
        [ForeignKey(nameof(StrengthFormId))]
        [InverseProperty(nameof(DrillReportStrengthForm.DrillReportStatusInfo))]
        public virtual DrillReportStrengthForm StrengthForm { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(DrillReportTvd.DrillReportStatusInfo))]
        public virtual DrillReportTvd Tvd { get; set; }
        [InverseProperty("StatusInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
