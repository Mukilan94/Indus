using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularFlowrateMn
    {
        public TubularFlowrateMn()
        {
            TubularMotor = new HashSet<TubularMotor>();
            TubularMwdTool = new HashSet<TubularMwdTool>();
        }

        [Key]
        public int FlowrateMnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FlowrateMn")]
        public virtual ICollection<TubularMotor> TubularMotor { get; set; }
        [InverseProperty("FlowrateMn")]
        public virtual ICollection<TubularMwdTool> TubularMwdTool { get; set; }
    }
}
