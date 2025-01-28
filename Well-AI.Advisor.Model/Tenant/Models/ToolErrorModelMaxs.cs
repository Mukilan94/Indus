using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelMaxs
    {
        public ToolErrorModelMaxs()
        {
            ToolErrorModelOperatingConditions = new HashSet<ToolErrorModelOperatingConditions>();
        }

        [Key]
        public int MaxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Max")]
        public virtual ICollection<ToolErrorModelOperatingConditions> ToolErrorModelOperatingConditions { get; set; }
    }
}
