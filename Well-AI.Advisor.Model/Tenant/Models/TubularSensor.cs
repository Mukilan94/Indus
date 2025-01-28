using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularSensor
    {
        [Key]
        public int SensorId { get; set; }
        public string TypeMeasurement { get; set; }
        public int? OffsetBotId { get; set; }
        public string Comments { get; set; }
        public string Uid { get; set; }
        public int? TubularMwdToolMwdToolId { get; set; }

        [ForeignKey(nameof(OffsetBotId))]
        [InverseProperty(nameof(TubularOffsetBot.TubularSensor))]
        public virtual TubularOffsetBot OffsetBot { get; set; }
        [ForeignKey(nameof(TubularMwdToolMwdToolId))]
        [InverseProperty(nameof(TubularMwdTool.TubularSensor))]
        public virtual TubularMwdTool TubularMwdToolMwdTool { get; set; }
    }
}
