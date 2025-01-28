using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularMwdTool
    {
        public TubularMwdTool()
        {
            TubularComponent = new HashSet<TubularComponent>();
            TubularSensor = new HashSet<TubularSensor>();
        }

        [Key]
        public int MwdToolId { get; set; }
        public int? FlowrateMnId { get; set; }
        public int? FlowrateMxId { get; set; }
        public int? TempMxId { get; set; }
        public int? IdEquvId { get; set; }

        [ForeignKey(nameof(FlowrateMnId))]
        [InverseProperty(nameof(TubularFlowrateMn.TubularMwdTool))]
        public virtual TubularFlowrateMn FlowrateMn { get; set; }
        [ForeignKey(nameof(FlowrateMxId))]
        [InverseProperty(nameof(TubularFlowrateMx.TubularMwdTool))]
        public virtual TubularFlowrateMx FlowrateMx { get; set; }
        [ForeignKey(nameof(IdEquvId))]
        [InverseProperty(nameof(TubularIdEquv.TubularMwdTool))]
        public virtual TubularIdEquv IdEquv { get; set; }
        [ForeignKey(nameof(TempMxId))]
        [InverseProperty(nameof(TubularTempMx.TubularMwdTool))]
        public virtual TubularTempMx TempMx { get; set; }
        [InverseProperty("MwdTool")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
        [InverseProperty("TubularMwdToolMwdTool")]
        public virtual ICollection<TubularSensor> TubularSensor { get; set; }
    }
}
