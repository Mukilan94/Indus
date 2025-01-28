using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCores
    {
        [Key]
        public int ConvCoreId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public int? MdCoreTopId { get; set; }
        public int? MdCoreBottomId { get; set; }
        [Column("DTimCoreStart")]
        public string DtimCoreStart { get; set; }
        [Column("DTimCoreEnd")]
        public string DtimCoreEnd { get; set; }
        public string CoreReference { get; set; }
        public string CoringContractor { get; set; }
        public string AnalysisContractor { get; set; }
        public string CoreBarrel { get; set; }
        public string InnerBarrelUsed { get; set; }
        public string InnerBarrelType { get; set; }
        public int? LenBarrelId { get; set; }
        public string CoreBitType { get; set; }
        public int? DiaBitId { get; set; }
        public int? DiaCoreId { get; set; }
        public int? LenCoredId { get; set; }
        public int? LenRecoveredId { get; set; }
        public int? RecoverPcLenRecoveredId { get; set; }
        public int? InclHoleId { get; set; }
        public string CoreOrientation { get; set; }
        public string CoreMethod { get; set; }
        public string CoreTreatmentMethod { get; set; }
        public string CoreFluidUsed { get; set; }
        public string NameFormation { get; set; }
        public int? GeologyIntervalId { get; set; }
        public string CoreDescription { get; set; }
        public int? CommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataId))]
        [InverseProperty(nameof(ConvCoreCommonDatas.ConvCores))]
        public virtual ConvCoreCommonDatas CommonData { get; set; }
        [ForeignKey(nameof(DiaBitId))]
        [InverseProperty(nameof(ConvCoreDiaBits.ConvCores))]
        public virtual ConvCoreDiaBits DiaBit { get; set; }
        [ForeignKey(nameof(DiaCoreId))]
        [InverseProperty(nameof(ConvCoreDiaCores.ConvCores))]
        public virtual ConvCoreDiaCores DiaCore { get; set; }
        [ForeignKey(nameof(GeologyIntervalId))]
        [InverseProperty(nameof(ConvCoreGeologyIntervals.ConvCores))]
        public virtual ConvCoreGeologyIntervals GeologyInterval { get; set; }
        [ForeignKey(nameof(InclHoleId))]
        [InverseProperty(nameof(ConvCoreInclHoles.ConvCores))]
        public virtual ConvCoreInclHoles InclHole { get; set; }
        [ForeignKey(nameof(LenBarrelId))]
        [InverseProperty(nameof(ConvCoreLenBarrels.ConvCores))]
        public virtual ConvCoreLenBarrels LenBarrel { get; set; }
        [ForeignKey(nameof(LenCoredId))]
        [InverseProperty(nameof(ConvCoreLenCoreds.ConvCores))]
        public virtual ConvCoreLenCoreds LenCored { get; set; }
        [ForeignKey(nameof(LenRecoveredId))]
        [InverseProperty(nameof(ConvCoreLenRecovereds.ConvCores))]
        public virtual ConvCoreLenRecovereds LenRecovered { get; set; }
        [ForeignKey(nameof(MdCoreBottomId))]
        [InverseProperty(nameof(ConvCoreMdCoreBottoms.ConvCores))]
        public virtual ConvCoreMdCoreBottoms MdCoreBottom { get; set; }
        [ForeignKey(nameof(MdCoreTopId))]
        [InverseProperty(nameof(ConvCoreMdCoreTops.ConvCores))]
        public virtual ConvCoreMdCoreTops MdCoreTop { get; set; }
        [ForeignKey(nameof(RecoverPcLenRecoveredId))]
        [InverseProperty(nameof(ConvCoreRecoverPcs.ConvCores))]
        public virtual ConvCoreRecoverPcs RecoverPcLenRecovered { get; set; }
    }
}
