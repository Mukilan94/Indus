using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorTermSetConstants
    {
        public ToolErrorTermSetConstants()
        {
            ToolErrorTermSetNomenclatures = new HashSet<ToolErrorTermSetNomenclatures>();
        }

        [Key]
        public int ConstantId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }

        [InverseProperty("Constant")]
        public virtual ICollection<ToolErrorTermSetNomenclatures> ToolErrorTermSetNomenclatures { get; set; }
    }
}
