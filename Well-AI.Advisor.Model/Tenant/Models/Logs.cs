using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Logs
    {
        public Logs()
        {
            LogCurveInfos = new HashSet<LogCurveInfos>();
            LogParams = new HashSet<LogParams>();
        }

        [Key]
        public int LogId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string ServiceCompany { get; set; }
        public string RunNumber { get; set; }
        public string CreationDate { get; set; }
        public string Description { get; set; }
        public string IndexType { get; set; }
        public int? StartIndexLogStartIndexId { get; set; }
        public int? EndIndexLogEndIndexId { get; set; }
        public int? StepIncrementId { get; set; }
        public string Direction { get; set; }
        public string IndexCurve { get; set; }
        public string NullValue { get; set; }
        public int? LogDataId { get; set; }
        public int? CommonDataLogCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataLogCommonDataId))]
        [InverseProperty(nameof(LogCommonDatas.Logs))]
        public virtual LogCommonDatas CommonDataLogCommonData { get; set; }
        [ForeignKey(nameof(EndIndexLogEndIndexId))]
        [InverseProperty(nameof(LogEndIndex.Logs))]
        public virtual LogEndIndex EndIndexLogEndIndex { get; set; }
        [ForeignKey(nameof(LogDataId))]
        [InverseProperty(nameof(LogDatas.Logs))]
        public virtual LogDatas LogData { get; set; }
        [ForeignKey(nameof(StartIndexLogStartIndexId))]
        [InverseProperty(nameof(LogStartIndex.Logs))]
        public virtual LogStartIndex StartIndexLogStartIndex { get; set; }
        [ForeignKey(nameof(StepIncrementId))]
        [InverseProperty(nameof(LogStepIncrements.Logs))]
        public virtual LogStepIncrements StepIncrement { get; set; }
        [InverseProperty("Log")]
        public virtual ICollection<LogCurveInfos> LogCurveInfos { get; set; }
        [InverseProperty("Log")]
        public virtual ICollection<LogParams> LogParams { get; set; }
    }
}
