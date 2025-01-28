using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class LogCurveInfos
    {
        [Key]
        public int LogCurveInfoId { get; set; }
        public string Mnemonic { get; set; }
        public string ClassWitsml { get; set; }
        public string Unit { get; set; }
        public string MnemAlias { get; set; }
        public string NullValue { get; set; }
        public int? MinIndexId { get; set; }
        public int? MaxIndexId { get; set; }
        public string CurveDescription { get; set; }
        public int? SensorOffsetId { get; set; }
        public string TraceState { get; set; }
        public string TypeLogData { get; set; }
        public string Uid { get; set; }
        public int? LogId { get; set; }

        [ForeignKey(nameof(LogId))]
        [InverseProperty(nameof(Logs.LogCurveInfos))]
        public virtual Logs Log { get; set; }
        [ForeignKey(nameof(MaxIndexId))]
        [InverseProperty(nameof(LogMaxIndexs.LogCurveInfos))]
        public virtual LogMaxIndexs MaxIndex { get; set; }
        [ForeignKey(nameof(MinIndexId))]
        [InverseProperty(nameof(LogMinIndexs.LogCurveInfos))]
        public virtual LogMinIndexs MinIndex { get; set; }
        [ForeignKey(nameof(SensorOffsetId))]
        [InverseProperty(nameof(LogSensorOffsets.LogCurveInfos))]
        public virtual LogSensorOffsets SensorOffset { get; set; }
    }
}
