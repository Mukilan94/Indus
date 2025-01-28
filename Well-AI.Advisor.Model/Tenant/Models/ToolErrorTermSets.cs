using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorTermSets
    {
        public ToolErrorTermSets()
        {
            ToolErrorTermSetErrorTerms = new HashSet<ToolErrorTermSetErrorTerms>();
        }

        [Key]
        public int ToolErrorTermSetId { get; set; }
        public string Name { get; set; }
        public int? AuthorizationId { get; set; }
        public int? NomenclatureId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(AuthorizationId))]
        [InverseProperty(nameof(ToolErrorTermSetAuthorizations.ToolErrorTermSets))]
        public virtual ToolErrorTermSetAuthorizations Authorization { get; set; }
        [ForeignKey(nameof(NomenclatureId))]
        [InverseProperty(nameof(ToolErrorTermSetNomenclatures.ToolErrorTermSets))]
        public virtual ToolErrorTermSetNomenclatures Nomenclature { get; set; }
        [InverseProperty("ToolErrorTermSet")]
        public virtual ICollection<ToolErrorTermSetErrorTerms> ToolErrorTermSetErrorTerms { get; set; }
    }
}
