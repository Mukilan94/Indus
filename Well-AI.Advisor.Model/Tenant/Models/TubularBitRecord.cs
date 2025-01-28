using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularBitRecord
    {
        public TubularBitRecord()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int BitId { get; set; }
        public string NumBit { get; set; }
        public int? DiaBitId { get; set; }
        public int? DiaPassThruId { get; set; }
        public int? DiaPilotId { get; set; }
        public string Manufacturer { get; set; }
        public string TypeBit { get; set; }
        public int? CostId { get; set; }
        [Column("CodeIADC")]
        public string CodeIadc { get; set; }
        public string CondInitInner { get; set; }
        public string CondInitOuter { get; set; }
        public string CondInitDull { get; set; }
        public string CondInitLocation { get; set; }
        public string CondInitBearing { get; set; }
        public string CondInitGauge { get; set; }
        public string CondInitOther { get; set; }
        public string CondInitReason { get; set; }
        public string CondFinalInner { get; set; }
        public string CondFinalOuter { get; set; }
        public string CondFinalDull { get; set; }
        public string CondFinalLocation { get; set; }
        public string CondFinalBearing { get; set; }
        public string CondFinalGauge { get; set; }
        public string CondFinalOther { get; set; }
        public string CondFinalReason { get; set; }
        public string Drive { get; set; }
        public string BitClass { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CostId))]
        [InverseProperty(nameof(TubularCosts.TubularBitRecord))]
        public virtual TubularCosts Cost { get; set; }
        [ForeignKey(nameof(DiaBitId))]
        [InverseProperty(nameof(TubularDiaBits.TubularBitRecord))]
        public virtual TubularDiaBits DiaBit { get; set; }
        [ForeignKey(nameof(DiaPassThruId))]
        [InverseProperty(nameof(TubularDiaPassThrus.TubularBitRecord))]
        public virtual TubularDiaPassThrus DiaPassThru { get; set; }
        [ForeignKey(nameof(DiaPilotId))]
        [InverseProperty(nameof(TubularDiaPilot.TubularBitRecord))]
        public virtual TubularDiaPilot DiaPilot { get; set; }
        [InverseProperty("BitRecordBit")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
