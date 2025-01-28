using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorTermSetErrorTerms
    {
        public ToolErrorTermSetErrorTerms()
        {
            ToolErrorTermSetErrorCoefficients = new HashSet<ToolErrorTermSetErrorCoefficients>();
        }

        [Key]
        public int ErrorTermId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string MeasureClass { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }
        public string OperatingMode { get; set; }
        public int? ToolErrorTermSetId { get; set; }

        [ForeignKey(nameof(ToolErrorTermSetId))]
        [InverseProperty(nameof(ToolErrorTermSets.ToolErrorTermSetErrorTerms))]
        public virtual ToolErrorTermSets ToolErrorTermSet { get; set; }
        [InverseProperty("ErrorTerm")]
        public virtual ICollection<ToolErrorTermSetErrorCoefficients> ToolErrorTermSetErrorCoefficients { get; set; }
    }
}
