using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportControlIncidentInfo
    {
        public DrillReportControlIncidentInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int ControlIncidentInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdInflowId { get; set; }
        public int? TvdInflowId { get; set; }
        public string Phase { get; set; }
        public string ActivityCode { get; set; }
        public string DetailActivity { get; set; }
        [Column("ETimLostId")]
        public int? EtimLostId { get; set; }
        [Column("DTimRegained")]
        public string DtimRegained { get; set; }
        public int? DiaBitId { get; set; }
        public int? MdBitId { get; set; }
        public string WtMudUom { get; set; }
        public int? PorePressureId { get; set; }
        public int? DiaCsgLastId { get; set; }
        public int? MdCsgLastId { get; set; }
        public int? VolMudGainedId { get; set; }
        public int? PresShutInCasingId { get; set; }
        public int? PresShutInDrillId { get; set; }
        public string IncidentType { get; set; }
        public string KillingType { get; set; }
        public string Formation { get; set; }
        public int? TempBottomId { get; set; }
        public int? PresMaxChokeId { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(DiaBitId))]
        [InverseProperty(nameof(DrillReportDiaBit.DrillReportControlIncidentInfo))]
        public virtual DrillReportDiaBit DiaBit { get; set; }
        [ForeignKey(nameof(DiaCsgLastId))]
        [InverseProperty(nameof(DrillReportDiaCsgLast.DrillReportControlIncidentInfo))]
        public virtual DrillReportDiaCsgLast DiaCsgLast { get; set; }
        [ForeignKey(nameof(EtimLostId))]
        [InverseProperty(nameof(DrillReportEtimLost.DrillReportControlIncidentInfo))]
        public virtual DrillReportEtimLost EtimLost { get; set; }
        [ForeignKey(nameof(MdBitId))]
        [InverseProperty(nameof(DrillReportMdBit.DrillReportControlIncidentInfo))]
        public virtual DrillReportMdBit MdBit { get; set; }
        [ForeignKey(nameof(MdCsgLastId))]
        [InverseProperty(nameof(DrillReportMdCsgLast.DrillReportControlIncidentInfo))]
        public virtual DrillReportMdCsgLast MdCsgLast { get; set; }
        [ForeignKey(nameof(MdInflowId))]
        [InverseProperty(nameof(DrillReportMdInflow.DrillReportControlIncidentInfo))]
        public virtual DrillReportMdInflow MdInflow { get; set; }
        [ForeignKey(nameof(PorePressureId))]
        [InverseProperty(nameof(DrillReportPorePressure.DrillReportControlIncidentInfo))]
        public virtual DrillReportPorePressure PorePressure { get; set; }
        [ForeignKey(nameof(PresMaxChokeId))]
        [InverseProperty(nameof(DrillReportPresMaxChoke.DrillReportControlIncidentInfo))]
        public virtual DrillReportPresMaxChoke PresMaxChoke { get; set; }
        [ForeignKey(nameof(PresShutInCasingId))]
        [InverseProperty(nameof(DrillReportPresShutInCasing.DrillReportControlIncidentInfo))]
        public virtual DrillReportPresShutInCasing PresShutInCasing { get; set; }
        [ForeignKey(nameof(PresShutInDrillId))]
        [InverseProperty(nameof(DrillReportPresShutInDrill.DrillReportControlIncidentInfo))]
        public virtual DrillReportPresShutInDrill PresShutInDrill { get; set; }
        [ForeignKey(nameof(TempBottomId))]
        [InverseProperty(nameof(DrillReportTempBottom.DrillReportControlIncidentInfo))]
        public virtual DrillReportTempBottom TempBottom { get; set; }
        [ForeignKey(nameof(TvdInflowId))]
        [InverseProperty(nameof(DrillReportTvdInflow.DrillReportControlIncidentInfo))]
        public virtual DrillReportTvdInflow TvdInflow { get; set; }
        [ForeignKey(nameof(VolMudGainedId))]
        [InverseProperty(nameof(DrillReportVolMudGained.DrillReportControlIncidentInfo))]
        public virtual DrillReportVolMudGained VolMudGained { get; set; }
        [ForeignKey(nameof(WtMudUom))]
        [InverseProperty(nameof(DrillReportWtMuds.DrillReportControlIncidentInfo))]
        public virtual DrillReportWtMuds WtMudUomNavigation { get; set; }
        [InverseProperty("ControlIncidentInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
