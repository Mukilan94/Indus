using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelOperatingIntervals
    {
        [Key]
        public int OperatingIntervalId { get; set; }
        public string Mode { get; set; }
        public int? StartId { get; set; }
        public int? EndId { get; set; }
        public string Uid { get; set; }
        public int? SpeedId { get; set; }
        public int? ToolErrorModelId { get; set; }

        [ForeignKey(nameof(EndId))]
        [InverseProperty(nameof(ToolErrorModelEnds.ToolErrorModelOperatingIntervals))]
        public virtual ToolErrorModelEnds End { get; set; }
        [ForeignKey(nameof(SpeedId))]
        [InverseProperty(nameof(ToolErrorModelSpeeds.ToolErrorModelOperatingIntervals))]
        public virtual ToolErrorModelSpeeds Speed { get; set; }
        [ForeignKey(nameof(StartId))]
        [InverseProperty(nameof(ToolErrorModelStarts.ToolErrorModelOperatingIntervals))]
        public virtual ToolErrorModelStarts Start { get; set; }
        [ForeignKey(nameof(ToolErrorModelId))]
        [InverseProperty(nameof(ToolErrorModels.ToolErrorModelOperatingIntervals))]
        public virtual ToolErrorModels ToolErrorModel { get; set; }
    }
}
