using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPumpOps
    {
        public OpsReportPumpOps()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int PumpOpId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? PumpId { get; set; }
        public string TypeOperation { get; set; }
        public int? IdLinerId { get; set; }
        public int? LenStrokeId { get; set; }
        public int? RateStrokeId { get; set; }
        public int? PressureId { get; set; }
        public int? PcEfficiencyId { get; set; }
        public int? PumpOutputId { get; set; }
        public int? MdBitId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(IdLinerId))]
        [InverseProperty(nameof(OpsReportIdLiners.OpsReportPumpOps))]
        public virtual OpsReportIdLiners IdLiner { get; set; }
        [ForeignKey(nameof(LenStrokeId))]
        [InverseProperty(nameof(OpsReportLenStrokes.OpsReportPumpOps))]
        public virtual OpsReportLenStrokes LenStroke { get; set; }
        [ForeignKey(nameof(MdBitId))]
        [InverseProperty(nameof(OpsReportMdBits.OpsReportPumpOps))]
        public virtual OpsReportMdBits MdBit { get; set; }
        [ForeignKey(nameof(PcEfficiencyId))]
        [InverseProperty(nameof(OpsReportPcEfficiencys.OpsReportPumpOps))]
        public virtual OpsReportPcEfficiencys PcEfficiency { get; set; }
        [ForeignKey(nameof(PressureId))]
        [InverseProperty(nameof(OpsReportPressures.OpsReportPumpOps))]
        public virtual OpsReportPressures Pressure { get; set; }
        [ForeignKey(nameof(PumpId))]
        [InverseProperty(nameof(OpsReportPumps.OpsReportPumpOps))]
        public virtual OpsReportPumps Pump { get; set; }
        [ForeignKey(nameof(PumpOutputId))]
        [InverseProperty(nameof(OpsReportPumpOutputs.OpsReportPumpOps))]
        public virtual OpsReportPumpOutputs PumpOutput { get; set; }
        [ForeignKey(nameof(RateStrokeId))]
        [InverseProperty(nameof(OpsReportRateStrokes.OpsReportPumpOps))]
        public virtual OpsReportRateStrokes RateStroke { get; set; }
        [InverseProperty("PumpOp")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
