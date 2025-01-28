using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportWeathers
    {
        public OpsReportWeathers()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int WeatherId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string Agency { get; set; }
        public int? BarometricPressureId { get; set; }
        public string BeaufortScaleNumber { get; set; }
        public int? TempSurfaceMnId { get; set; }
        public int? TempSurfaceMxId { get; set; }
        public int? TempWindChillId { get; set; }
        public int? TempseaId { get; set; }
        public int? VisibilityId { get; set; }
        public int? AziWaveId { get; set; }
        public int? HtWaveId { get; set; }
        public int? PeriodWaveId { get; set; }
        public int? AziWindId { get; set; }
        public int? VelWindId { get; set; }
        public string TypePrecip { get; set; }
        public int? AmtPrecipId { get; set; }
        public string CoverCloud { get; set; }
        public int? CeilingCloudId { get; set; }
        public int? CurrentSeaId { get; set; }
        public int? AziCurrentSeaId { get; set; }
        public string Comments { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AmtPrecipId))]
        [InverseProperty(nameof(OpsReportAmtPrecips.OpsReportWeathers))]
        public virtual OpsReportAmtPrecips AmtPrecip { get; set; }
        [ForeignKey(nameof(AziCurrentSeaId))]
        [InverseProperty(nameof(OpsReportAziCurrentSeas.OpsReportWeathers))]
        public virtual OpsReportAziCurrentSeas AziCurrentSea { get; set; }
        [ForeignKey(nameof(AziWaveId))]
        [InverseProperty(nameof(OpsReportAziWaves.OpsReportWeathers))]
        public virtual OpsReportAziWaves AziWave { get; set; }
        [ForeignKey(nameof(AziWindId))]
        [InverseProperty(nameof(OpsReportAziWinds.OpsReportWeathers))]
        public virtual OpsReportAziWinds AziWind { get; set; }
        [ForeignKey(nameof(BarometricPressureId))]
        [InverseProperty(nameof(OpsReportBarometricPressures.OpsReportWeathers))]
        public virtual OpsReportBarometricPressures BarometricPressure { get; set; }
        [ForeignKey(nameof(CeilingCloudId))]
        [InverseProperty(nameof(OpsReportCeilingClouds.OpsReportWeathers))]
        public virtual OpsReportCeilingClouds CeilingCloud { get; set; }
        [ForeignKey(nameof(CurrentSeaId))]
        [InverseProperty(nameof(OpsReportCurrentSeas.OpsReportWeathers))]
        public virtual OpsReportCurrentSeas CurrentSea { get; set; }
        [ForeignKey(nameof(HtWaveId))]
        [InverseProperty(nameof(OpsReportHtWaves.OpsReportWeathers))]
        public virtual OpsReportHtWaves HtWave { get; set; }
        [ForeignKey(nameof(PeriodWaveId))]
        [InverseProperty(nameof(OpsReportPeriodWaves.OpsReportWeathers))]
        public virtual OpsReportPeriodWaves PeriodWave { get; set; }
        [ForeignKey(nameof(TempSurfaceMnId))]
        [InverseProperty(nameof(OpsReportTempSurfaceMns.OpsReportWeathers))]
        public virtual OpsReportTempSurfaceMns TempSurfaceMn { get; set; }
        [ForeignKey(nameof(TempSurfaceMxId))]
        [InverseProperty(nameof(OpsReportTempSurfaceMxs.OpsReportWeathers))]
        public virtual OpsReportTempSurfaceMxs TempSurfaceMx { get; set; }
        [ForeignKey(nameof(TempWindChillId))]
        [InverseProperty(nameof(OpsReportTempWindChills.OpsReportWeathers))]
        public virtual OpsReportTempWindChills TempWindChill { get; set; }
        [ForeignKey(nameof(TempseaId))]
        [InverseProperty(nameof(OpsReportTempseas.OpsReportWeathers))]
        public virtual OpsReportTempseas Tempsea { get; set; }
        [ForeignKey(nameof(VelWindId))]
        [InverseProperty(nameof(OpsReportVelWinds.OpsReportWeathers))]
        public virtual OpsReportVelWinds VelWind { get; set; }
        [ForeignKey(nameof(VisibilityId))]
        [InverseProperty(nameof(OpsReportVisibilitys.OpsReportWeathers))]
        public virtual OpsReportVisibilitys Visibility { get; set; }
        [InverseProperty("Weather")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
