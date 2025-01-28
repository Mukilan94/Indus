using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelOperatingConditions
    {
        public ToolErrorModelOperatingConditions()
        {
            ToolErrorModels = new HashSet<ToolErrorModels>();
        }

        [Key]
        public int OperatingConditionId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public string Uid { get; set; }
        public int? MinId { get; set; }
        public int? MaxId { get; set; }

        [ForeignKey(nameof(MaxId))]
        [InverseProperty(nameof(ToolErrorModelMaxs.ToolErrorModelOperatingConditions))]
        public virtual ToolErrorModelMaxs Max { get; set; }
        [ForeignKey(nameof(MinId))]
        [InverseProperty(nameof(ToolErrorModelMins.ToolErrorModelOperatingConditions))]
        public virtual ToolErrorModelMins Min { get; set; }
        [InverseProperty("OperatingCondition")]
        public virtual ICollection<ToolErrorModels> ToolErrorModels { get; set; }
    }
}
