using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelTerms
    {
        public ToolErrorModelTerms()
        {
            ToolErrorModelErrorTermValues = new HashSet<ToolErrorModelErrorTermValues>();
        }

        [Key]
        public int TermId { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("Term")]
        public virtual ICollection<ToolErrorModelErrorTermValues> ToolErrorModelErrorTermValues { get; set; }
    }
}
