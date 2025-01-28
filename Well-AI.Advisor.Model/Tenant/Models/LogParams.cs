using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class LogParams
    {
        [Key]
        public int LogParamId { get; set; }
        public string Uid { get; set; }
        public string Uom { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }
        public string Text { get; set; }
        public int? LogId { get; set; }

        [ForeignKey(nameof(LogId))]
        [InverseProperty(nameof(Logs.LogParams))]
        public virtual Logs Log { get; set; }
    }
}
