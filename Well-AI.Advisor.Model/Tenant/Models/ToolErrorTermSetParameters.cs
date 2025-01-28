using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorTermSetParameters
    {
        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NomenclatureId { get; set; }

        [ForeignKey(nameof(NomenclatureId))]
        [InverseProperty(nameof(ToolErrorTermSetNomenclatures.ToolErrorTermSetParameters))]
        public virtual ToolErrorTermSetNomenclatures Nomenclature { get; set; }
    }
}
