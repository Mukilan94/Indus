using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobJobEvents
    {
        [Key]
        public int JobEventId { get; set; }
        public string Number { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string Comment { get; set; }
        public string NumStage { get; set; }
        public string Uid { get; set; }
        public int? FlowPathId { get; set; }

        [ForeignKey(nameof(FlowPathId))]
        [InverseProperty(nameof(StimJobFlowPaths.StimJobJobEvents))]
        public virtual StimJobFlowPaths FlowPath { get; set; }
    }
}
