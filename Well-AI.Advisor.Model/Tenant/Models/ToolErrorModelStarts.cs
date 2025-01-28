using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelStarts
    {
        public ToolErrorModelStarts()
        {
            ToolErrorModelOperatingIntervals = new HashSet<ToolErrorModelOperatingIntervals>();
        }

        [Key]
        public int StartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Start")]
        public virtual ICollection<ToolErrorModelOperatingIntervals> ToolErrorModelOperatingIntervals { get; set; }
    }
}
