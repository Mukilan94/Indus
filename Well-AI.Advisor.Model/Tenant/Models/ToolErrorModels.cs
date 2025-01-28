using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModels
    {
        public ToolErrorModels()
        {
            ToolErrorModelErrorTermValues = new HashSet<ToolErrorModelErrorTermValues>();
            ToolErrorModelOperatingIntervals = new HashSet<ToolErrorModelOperatingIntervals>();
        }

        [Key]
        public int ToolErrorModelId { get; set; }
        public string Name { get; set; }
        public int? AuthorizationId { get; set; }
        public int? OperatingConditionId { get; set; }
        public int? UseErrorTermSetId { get; set; }
        public string Uid { get; set; }
        public int? ModelParametersId { get; set; }
        public int? CommonDataToolErrorModelCommonDataId { get; set; }

        [ForeignKey(nameof(AuthorizationId))]
        [InverseProperty(nameof(ToolErrorModelAuthorizations.ToolErrorModels))]
        public virtual ToolErrorModelAuthorizations Authorization { get; set; }
        [ForeignKey(nameof(CommonDataToolErrorModelCommonDataId))]
        [InverseProperty(nameof(ToolErrorModelCommonDatas.ToolErrorModels))]
        public virtual ToolErrorModelCommonDatas CommonDataToolErrorModelCommonData { get; set; }
        [ForeignKey(nameof(ModelParametersId))]
        [InverseProperty(nameof(ToolErrorModelModelParameter.ToolErrorModels))]
        public virtual ToolErrorModelModelParameter ModelParameters { get; set; }
        [ForeignKey(nameof(OperatingConditionId))]
        [InverseProperty(nameof(ToolErrorModelOperatingConditions.ToolErrorModels))]
        public virtual ToolErrorModelOperatingConditions OperatingCondition { get; set; }
        [ForeignKey(nameof(UseErrorTermSetId))]
        [InverseProperty(nameof(ToolErrorModelUseErrorTermSets.ToolErrorModels))]
        public virtual ToolErrorModelUseErrorTermSets UseErrorTermSet { get; set; }
        [InverseProperty("ToolErrorModel")]
        public virtual ICollection<ToolErrorModelErrorTermValues> ToolErrorModelErrorTermValues { get; set; }
        [InverseProperty("ToolErrorModel")]
        public virtual ICollection<ToolErrorModelOperatingIntervals> ToolErrorModelOperatingIntervals { get; set; }
    }
}
