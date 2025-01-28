using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class LogSensorOffsets
    {
        public LogSensorOffsets()
        {
            LogCurveInfos = new HashSet<LogCurveInfos>();
        }

        [Key]
        public int SensorOffsetId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SensorOffset")]
        public virtual ICollection<LogCurveInfos> LogCurveInfos { get; set; }
    }
}
