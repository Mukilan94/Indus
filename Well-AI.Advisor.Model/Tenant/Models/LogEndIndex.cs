using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class LogEndIndex
    {
        public LogEndIndex()
        {
            Logs = new HashSet<Logs>();
        }

        [Key]
        public int LogEndIndexId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndIndexLogEndIndex")]
        public virtual ICollection<Logs> Logs { get; set; }
    }
}
