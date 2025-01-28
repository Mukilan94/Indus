using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelSpeeds
    {
        public ToolErrorModelSpeeds()
        {
            ToolErrorModelOperatingIntervals = new HashSet<ToolErrorModelOperatingIntervals>();
        }

        [Key]
        public int SpeedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Speed")]
        public virtual ICollection<ToolErrorModelOperatingIntervals> ToolErrorModelOperatingIntervals { get; set; }
    }
}
