using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorTermSetFunctions
    {
        [Key]
        public int FunctionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }
        public int? NomenclatureId { get; set; }

        [ForeignKey(nameof(NomenclatureId))]
        [InverseProperty(nameof(ToolErrorTermSetNomenclatures.ToolErrorTermSetFunctions))]
        public virtual ToolErrorTermSetNomenclatures Nomenclature { get; set; }
    }
}
