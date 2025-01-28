using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularMotor
    {
        public TubularMotor()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int MotorId { get; set; }
        public int? OffsetToolId { get; set; }
        public string PresLossFact { get; set; }
        public int? FlowrateMnId { get; set; }
        public int? FlowrateMxId { get; set; }
        public int? DiaRotorNozzleId { get; set; }
        public int? ClearanceBearBoxId { get; set; }
        public string LobesRotor { get; set; }
        public string LobesStator { get; set; }
        public string TypeBearing { get; set; }
        public int? TempOpMxId { get; set; }
        public string RotorCatcher { get; set; }
        public string DumpValve { get; set; }
        public int? DiaNozzleId { get; set; }
        public string Rotatable { get; set; }
        public int? BendSettingsMnId { get; set; }
        public int? BendSettingsMxId { get; set; }

        [ForeignKey(nameof(BendSettingsMnId))]
        [InverseProperty(nameof(TubularBendSettingsMn.TubularMotor))]
        public virtual TubularBendSettingsMn BendSettingsMn { get; set; }
        [ForeignKey(nameof(BendSettingsMxId))]
        [InverseProperty(nameof(TubularBendSettingsMx.TubularMotor))]
        public virtual TubularBendSettingsMx BendSettingsMx { get; set; }
        [ForeignKey(nameof(ClearanceBearBoxId))]
        [InverseProperty(nameof(TubularClearanceBearBox.TubularMotor))]
        public virtual TubularClearanceBearBox ClearanceBearBox { get; set; }
        [ForeignKey(nameof(DiaNozzleId))]
        [InverseProperty(nameof(TubularDiaNozzle.TubularMotor))]
        public virtual TubularDiaNozzle DiaNozzle { get; set; }
        [ForeignKey(nameof(DiaRotorNozzleId))]
        [InverseProperty(nameof(TubularDiaRotorNozzle.TubularMotor))]
        public virtual TubularDiaRotorNozzle DiaRotorNozzle { get; set; }
        [ForeignKey(nameof(FlowrateMnId))]
        [InverseProperty(nameof(TubularFlowrateMn.TubularMotor))]
        public virtual TubularFlowrateMn FlowrateMn { get; set; }
        [ForeignKey(nameof(FlowrateMxId))]
        [InverseProperty(nameof(TubularFlowrateMx.TubularMotor))]
        public virtual TubularFlowrateMx FlowrateMx { get; set; }
        [ForeignKey(nameof(OffsetToolId))]
        [InverseProperty(nameof(TubularOffsetTool.TubularMotor))]
        public virtual TubularOffsetTool OffsetTool { get; set; }
        [ForeignKey(nameof(TempOpMxId))]
        [InverseProperty(nameof(TubularTempOpMx.TubularMotor))]
        public virtual TubularTempOpMx TempOpMx { get; set; }
        [InverseProperty("Motor")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
