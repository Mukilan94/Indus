using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelEnds
    {
        public ToolErrorModelEnds()
        {
            ToolErrorModelOperatingIntervals = new HashSet<ToolErrorModelOperatingIntervals>();
        }

        [Key]
        public int EndId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("End")]
        public virtual ICollection<ToolErrorModelOperatingIntervals> ToolErrorModelOperatingIntervals { get; set; }
    }
}
