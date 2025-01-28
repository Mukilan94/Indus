using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorTermSetNomenclatures
    {
        public ToolErrorTermSetNomenclatures()
        {
            ToolErrorTermSetFunctions = new HashSet<ToolErrorTermSetFunctions>();
            ToolErrorTermSetParameters = new HashSet<ToolErrorTermSetParameters>();
            ToolErrorTermSets = new HashSet<ToolErrorTermSets>();
        }

        [Key]
        public int NomenclatureId { get; set; }
        public int? ConstantId { get; set; }

        [ForeignKey(nameof(ConstantId))]
        [InverseProperty(nameof(ToolErrorTermSetConstants.ToolErrorTermSetNomenclatures))]
        public virtual ToolErrorTermSetConstants Constant { get; set; }
        [InverseProperty("Nomenclature")]
        public virtual ICollection<ToolErrorTermSetFunctions> ToolErrorTermSetFunctions { get; set; }
        [InverseProperty("Nomenclature")]
        public virtual ICollection<ToolErrorTermSetParameters> ToolErrorTermSetParameters { get; set; }
        [InverseProperty("Nomenclature")]
        public virtual ICollection<ToolErrorTermSets> ToolErrorTermSets { get; set; }
    }
}
