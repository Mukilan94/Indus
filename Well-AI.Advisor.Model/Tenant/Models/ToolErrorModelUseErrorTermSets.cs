using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelUseErrorTermSets
    {
        public ToolErrorModelUseErrorTermSets()
        {
            ToolErrorModels = new HashSet<ToolErrorModels>();
        }

        [Key]
        public int UseErrorTermSetId { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("UseErrorTermSet")]
        public virtual ICollection<ToolErrorModels> ToolErrorModels { get; set; }
    }
}
