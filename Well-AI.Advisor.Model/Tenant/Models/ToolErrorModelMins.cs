using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelMins
    {
        public ToolErrorModelMins()
        {
            ToolErrorModelOperatingConditions = new HashSet<ToolErrorModelOperatingConditions>();
        }

        [Key]
        public int MinId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Min")]
        public virtual ICollection<ToolErrorModelOperatingConditions> ToolErrorModelOperatingConditions { get; set; }
    }
}
