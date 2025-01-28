using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularFlowrateMx
    {
        public TubularFlowrateMx()
        {
            TubularMotor = new HashSet<TubularMotor>();
            TubularMwdTool = new HashSet<TubularMwdTool>();
        }

        [Key]
        public int FlowrateMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FlowrateMx")]
        public virtual ICollection<TubularMotor> TubularMotor { get; set; }
        [InverseProperty("FlowrateMx")]
        public virtual ICollection<TubularMwdTool> TubularMwdTool { get; set; }
    }
}
