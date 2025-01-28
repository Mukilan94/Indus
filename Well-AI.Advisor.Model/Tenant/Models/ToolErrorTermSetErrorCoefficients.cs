using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorTermSetErrorCoefficients
    {
        [Key]
        public int ErrorCoefficientId { get; set; }
        public string Azi { get; set; }
        public string Uid { get; set; }
        public string Inc { get; set; }
        public string Depth { get; set; }
        public string Tvd { get; set; }
        public int? ErrorTermId { get; set; }

        [ForeignKey(nameof(ErrorTermId))]
        [InverseProperty(nameof(ToolErrorTermSetErrorTerms.ToolErrorTermSetErrorCoefficients))]
        public virtual ToolErrorTermSetErrorTerms ErrorTerm { get; set; }
    }
}
