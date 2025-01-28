using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogGeologyInterval
    {
        public MudLogGeologyInterval()
        {
            MudLogChronostratigraphic = new HashSet<MudLogChronostratigraphic>();
            MudLogs = new HashSet<MudLogs>();
        }

        [Key]
        public int GeologyIntervalId { get; set; }
        public string TypeLithology { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? TvdTopId { get; set; }
        public int? TvdBaseId { get; set; }
        public int? RopAvId { get; set; }
        public int? RopMnId { get; set; }
        public int? RopMxId { get; set; }
        public int? WobAvId { get; set; }
        public int? TqAvId { get; set; }
        public int? RpmAvId { get; set; }
        public int? WtMudAvId { get; set; }
        public int? EcdTdAvId { get; set; }
        public string DxcAv { get; set; }
        public int? LithologyId { get; set; }
        public int? ShowId { get; set; }
        public int? ChromatographId { get; set; }
        public int? MudGasId { get; set; }
        public int? DensBulkId { get; set; }
        public int? DensShaleId { get; set; }
        public int? CalciteId { get; set; }
        public int? DolomiteId { get; set; }
        public int? CecId { get; set; }
        public int? CalcStabId { get; set; }
        public int? LithostratigraphicId { get; set; }
        public int? SizeMnId { get; set; }
        public int? SizeMxId { get; set; }
        public int? LenPlugId { get; set; }
        public string Description { get; set; }
        public string CuttingFluid { get; set; }
        public string CleaningMethod { get; set; }
        public string DryingMethod { get; set; }
        public int? CommonTimeId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CalcStabId))]
        [InverseProperty(nameof(MudLogCalcStab.MudLogGeologyInterval))]
        public virtual MudLogCalcStab CalcStab { get; set; }
        [ForeignKey(nameof(CalciteId))]
        [InverseProperty(nameof(MudLogCalcite.MudLogGeologyInterval))]
        public virtual MudLogCalcite Calcite { get; set; }
        [ForeignKey(nameof(CecId))]
        [InverseProperty(nameof(MudLogCec.MudLogGeologyInterval))]
        public virtual MudLogCec Cec { get; set; }
        [ForeignKey(nameof(ChromatographId))]
        [InverseProperty(nameof(MudLogChromatograph.MudLogGeologyInterval))]
        public virtual MudLogChromatograph Chromatograph { get; set; }
        [ForeignKey(nameof(CommonTimeId))]
        [InverseProperty(nameof(MudLogCommonTime.MudLogGeologyInterval))]
        public virtual MudLogCommonTime CommonTime { get; set; }
        [ForeignKey(nameof(DensBulkId))]
        [InverseProperty(nameof(MudLogDensBulk.MudLogGeologyInterval))]
        public virtual MudLogDensBulk DensBulk { get; set; }
        [ForeignKey(nameof(DensShaleId))]
        [InverseProperty(nameof(MudLogDensShale.MudLogGeologyInterval))]
        public virtual MudLogDensShale DensShale { get; set; }
        [ForeignKey(nameof(DolomiteId))]
        [InverseProperty(nameof(MudLogDolomite.MudLogGeologyInterval))]
        public virtual MudLogDolomite Dolomite { get; set; }
        [ForeignKey(nameof(EcdTdAvId))]
        [InverseProperty(nameof(MudLogEcdTdAv.MudLogGeologyInterval))]
        public virtual MudLogEcdTdAv EcdTdAv { get; set; }
        [ForeignKey(nameof(LenPlugId))]
        [InverseProperty(nameof(MudLogLenPlug.MudLogGeologyInterval))]
        public virtual MudLogLenPlug LenPlug { get; set; }
        [ForeignKey(nameof(LithologyId))]
        [InverseProperty(nameof(MudLogLithology.MudLogGeologyInterval))]
        public virtual MudLogLithology Lithology { get; set; }
        [ForeignKey(nameof(LithostratigraphicId))]
        [InverseProperty(nameof(MudLogLithostratigraphic.MudLogGeologyInterval))]
        public virtual MudLogLithostratigraphic Lithostratigraphic { get; set; }
        [ForeignKey(nameof(MdBottomId))]
        [InverseProperty(nameof(MudLogMdBottom.MudLogGeologyInterval))]
        public virtual MudLogMdBottom MdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(MudLogMdTop.MudLogGeologyInterval))]
        public virtual MudLogMdTop MdTop { get; set; }
        [ForeignKey(nameof(MudGasId))]
        [InverseProperty(nameof(MudLogMudGas.MudLogGeologyInterval))]
        public virtual MudLogMudGas MudGas { get; set; }
        [ForeignKey(nameof(RopAvId))]
        [InverseProperty(nameof(MudLogRopAv.MudLogGeologyInterval))]
        public virtual MudLogRopAv RopAv { get; set; }
        [ForeignKey(nameof(RopMnId))]
        [InverseProperty(nameof(MudLogRopMn.MudLogGeologyInterval))]
        public virtual MudLogRopMn RopMn { get; set; }
        [ForeignKey(nameof(RopMxId))]
        [InverseProperty(nameof(MudLogRopMx.MudLogGeologyInterval))]
        public virtual MudLogRopMx RopMx { get; set; }
        [ForeignKey(nameof(RpmAvId))]
        [InverseProperty(nameof(MudLogRpmAv.MudLogGeologyInterval))]
        public virtual MudLogRpmAv RpmAv { get; set; }
        [ForeignKey(nameof(ShowId))]
        [InverseProperty(nameof(MudLogShow.MudLogGeologyInterval))]
        public virtual MudLogShow Show { get; set; }
        [ForeignKey(nameof(SizeMnId))]
        [InverseProperty(nameof(MudLogSizeMn.MudLogGeologyInterval))]
        public virtual MudLogSizeMn SizeMn { get; set; }
        [ForeignKey(nameof(SizeMxId))]
        [InverseProperty(nameof(MudLogSizeMx.MudLogGeologyInterval))]
        public virtual MudLogSizeMx SizeMx { get; set; }
        [ForeignKey(nameof(TqAvId))]
        [InverseProperty(nameof(MudLogTqAv.MudLogGeologyInterval))]
        public virtual MudLogTqAv TqAv { get; set; }
        [ForeignKey(nameof(TvdBaseId))]
        [InverseProperty(nameof(MudLogTvdBase.MudLogGeologyInterval))]
        public virtual MudLogTvdBase TvdBase { get; set; }
        [ForeignKey(nameof(TvdTopId))]
        [InverseProperty(nameof(MudLogTvdTop.MudLogGeologyInterval))]
        public virtual MudLogTvdTop TvdTop { get; set; }
        [ForeignKey(nameof(WobAvId))]
        [InverseProperty(nameof(MudLogWobAv.MudLogGeologyInterval))]
        public virtual MudLogWobAv WobAv { get; set; }
        [ForeignKey(nameof(WtMudAvId))]
        [InverseProperty(nameof(MudLogWtMudAv.MudLogGeologyInterval))]
        public virtual MudLogWtMudAv WtMudAv { get; set; }
        [InverseProperty("MudLogGeologyIntervalGeologyInterval")]
        public virtual ICollection<MudLogChronostratigraphic> MudLogChronostratigraphic { get; set; }
        [InverseProperty("GeologyInterval")]
        public virtual ICollection<MudLogs> MudLogs { get; set; }
    }
}
