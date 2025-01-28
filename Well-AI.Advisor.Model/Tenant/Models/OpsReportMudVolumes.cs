using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMudVolumes
    {
        public OpsReportMudVolumes()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int MudVolumeId { get; set; }
        public int? VolTotMudStartId { get; set; }
        public int? VolMudDumpedId { get; set; }
        public int? VolMudReceivedId { get; set; }
        public int? VolMudReturnedId { get; set; }
        public int? MudLossesId { get; set; }
        public int? VolMudBuiltId { get; set; }
        public int? VolMudStringId { get; set; }
        public int? VolMudCasingId { get; set; }
        public int? VolMudHoleId { get; set; }
        public int? VolMudRiserId { get; set; }
        public int? VolTotMudEndId { get; set; }

        [ForeignKey(nameof(MudLossesId))]
        [InverseProperty(nameof(OpsReportMudLossess.OpsReportMudVolumes))]
        public virtual OpsReportMudLossess MudLosses { get; set; }
        [ForeignKey(nameof(VolMudBuiltId))]
        [InverseProperty(nameof(OpsReportVolMudBuilts.OpsReportMudVolumes))]
        public virtual OpsReportVolMudBuilts VolMudBuilt { get; set; }
        [ForeignKey(nameof(VolMudCasingId))]
        [InverseProperty(nameof(OpsReportVolMudCasings.OpsReportMudVolumes))]
        public virtual OpsReportVolMudCasings VolMudCasing { get; set; }
        [ForeignKey(nameof(VolMudDumpedId))]
        [InverseProperty(nameof(OpsReportVolMudDumpeds.OpsReportMudVolumes))]
        public virtual OpsReportVolMudDumpeds VolMudDumped { get; set; }
        [ForeignKey(nameof(VolMudHoleId))]
        [InverseProperty(nameof(OpsReportVolMudHoles.OpsReportMudVolumes))]
        public virtual OpsReportVolMudHoles VolMudHole { get; set; }
        [ForeignKey(nameof(VolMudReceivedId))]
        [InverseProperty(nameof(OpsReportVolMudReceiveds.OpsReportMudVolumes))]
        public virtual OpsReportVolMudReceiveds VolMudReceived { get; set; }
        [ForeignKey(nameof(VolMudReturnedId))]
        [InverseProperty(nameof(OpsReportVolMudReturneds.OpsReportMudVolumes))]
        public virtual OpsReportVolMudReturneds VolMudReturned { get; set; }
        [ForeignKey(nameof(VolMudRiserId))]
        [InverseProperty(nameof(OpsReportVolMudRisers.OpsReportMudVolumes))]
        public virtual OpsReportVolMudRisers VolMudRiser { get; set; }
        [ForeignKey(nameof(VolMudStringId))]
        [InverseProperty(nameof(OpsReportVolMudStrings.OpsReportMudVolumes))]
        public virtual OpsReportVolMudStrings VolMudString { get; set; }
        [ForeignKey(nameof(VolTotMudEndId))]
        [InverseProperty(nameof(OpsReportVolTotMudEnds.OpsReportMudVolumes))]
        public virtual OpsReportVolTotMudEnds VolTotMudEnd { get; set; }
        [ForeignKey(nameof(VolTotMudStartId))]
        [InverseProperty(nameof(OpsReportVolTotMudStarts.OpsReportMudVolumes))]
        public virtual OpsReportVolTotMudStarts VolTotMudStart { get; set; }
        [InverseProperty("MudVolume")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
