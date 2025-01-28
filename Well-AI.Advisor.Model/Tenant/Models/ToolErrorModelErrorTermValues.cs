using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelErrorTermValues
    {
        [Key]
        public int ErrorTermValueId { get; set; }
        public int? TermId { get; set; }
        public string Prop { get; set; }
        public int? ValueId { get; set; }
        public string Uid { get; set; }
        public string Bias { get; set; }
        public string Comment { get; set; }
        public int? ToolErrorModelId { get; set; }

        [ForeignKey(nameof(TermId))]
        [InverseProperty(nameof(ToolErrorModelTerms.ToolErrorModelErrorTermValues))]
        public virtual ToolErrorModelTerms Term { get; set; }
        [ForeignKey(nameof(ToolErrorModelId))]
        [InverseProperty(nameof(ToolErrorModels.ToolErrorModelErrorTermValues))]
        public virtual ToolErrorModels ToolErrorModel { get; set; }
        [ForeignKey(nameof(ValueId))]
        [InverseProperty(nameof(ToolErrorModelValues.ToolErrorModelErrorTermValues))]
        public virtual ToolErrorModelValues Value { get; set; }
    }
}
