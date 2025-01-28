using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportScrs
    {
        [Key]
        public int ScrId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? PumpId { get; set; }
        public string TypeScr { get; set; }
        public int? RateStrokeId { get; set; }
        public int? PresRecordedId { get; set; }
        public int? MdBitId { get; set; }
        public string Uid { get; set; }
        public int? OpsReportId { get; set; }

        [ForeignKey(nameof(MdBitId))]
        [InverseProperty(nameof(OpsReportMdBits.OpsReportScrs))]
        public virtual OpsReportMdBits MdBit { get; set; }
        [ForeignKey(nameof(OpsReportId))]
        [InverseProperty(nameof(OpsReports.OpsReportScrs))]
        public virtual OpsReports OpsReport { get; set; }
        [ForeignKey(nameof(PresRecordedId))]
        [InverseProperty(nameof(OpsReportPresRecordeds.OpsReportScrs))]
        public virtual OpsReportPresRecordeds PresRecorded { get; set; }
        [ForeignKey(nameof(PumpId))]
        [InverseProperty(nameof(OpsReportPumps.OpsReportScrs))]
        public virtual OpsReportPumps Pump { get; set; }
        [ForeignKey(nameof(RateStrokeId))]
        [InverseProperty(nameof(OpsReportRateStrokes.OpsReportScrs))]
        public virtual OpsReportRateStrokes RateStroke { get; set; }
    }
}
