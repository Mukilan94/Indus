using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class LogStepIncrements
    {
        public LogStepIncrements()
        {
            Logs = new HashSet<Logs>();
        }

        [Key]
        public int StepIncrementId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StepIncrement")]
        public virtual ICollection<Logs> Logs { get; set; }
    }
}
