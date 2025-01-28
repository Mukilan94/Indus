using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreGeologyIntervals
    {
        public ConvCoreGeologyIntervals()
        {
            ConvCores = new HashSet<ConvCores>();
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
        public string NameFormation { get; set; }
        public string Lithostratigraphic { get; set; }
        public string Chronostratigraphic { get; set; }
        public int? SizeMnId { get; set; }
        public int? SizeMxId { get; set; }
        public int? LenPlugId { get; set; }
        public string Description { get; set; }
        public string CuttingFluid { get; set; }
        public string CleaningMethod { get; set; }
        public string DryingMethod { get; set; }
        public string Comments { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(CalcStabId))]
        [InverseProperty(nameof(ConvCoreCalcStabs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreCalcStabs CalcStab { get; set; }
        [ForeignKey(nameof(CalciteId))]
        [InverseProperty(nameof(ConvCoreCalcites.ConvCoreGeologyIntervals))]
        public virtual ConvCoreCalcites Calcite { get; set; }
        [ForeignKey(nameof(CecId))]
        [InverseProperty(nameof(ConvCoreCecs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreCecs Cec { get; set; }
        [ForeignKey(nameof(ChromatographId))]
        [InverseProperty(nameof(ConvCoreChromatographs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreChromatographs Chromatograph { get; set; }
        [ForeignKey(nameof(DensBulkId))]
        [InverseProperty(nameof(ConvCoreDensBulks.ConvCoreGeologyIntervals))]
        public virtual ConvCoreDensBulks DensBulk { get; set; }
        [ForeignKey(nameof(DensShaleId))]
        [InverseProperty(nameof(ConvCoreDensShales.ConvCoreGeologyIntervals))]
        public virtual ConvCoreDensShales DensShale { get; set; }
        [ForeignKey(nameof(DolomiteId))]
        [InverseProperty(nameof(ConvCoreDolomites.ConvCoreGeologyIntervals))]
        public virtual ConvCoreDolomites Dolomite { get; set; }
        [ForeignKey(nameof(EcdTdAvId))]
        [InverseProperty(nameof(ConvCoreEcdTdAvs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreEcdTdAvs EcdTdAv { get; set; }
        [ForeignKey(nameof(LenPlugId))]
        [InverseProperty(nameof(ConvCoreLenPlugs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreLenPlugs LenPlug { get; set; }
        [ForeignKey(nameof(LithologyId))]
        [InverseProperty(nameof(ConvCoreLithologys.ConvCoreGeologyIntervals))]
        public virtual ConvCoreLithologys Lithology { get; set; }
        [ForeignKey(nameof(MdBottomId))]
        [InverseProperty(nameof(ConvCoreMdBottoms.ConvCoreGeologyIntervals))]
        public virtual ConvCoreMdBottoms MdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(ConvCoreMdTops.ConvCoreGeologyIntervals))]
        public virtual ConvCoreMdTops MdTop { get; set; }
        [ForeignKey(nameof(MudGasId))]
        [InverseProperty(nameof(ConvCoreMudGass.ConvCoreGeologyIntervals))]
        public virtual ConvCoreMudGass MudGas { get; set; }
        [ForeignKey(nameof(RopAvId))]
        [InverseProperty(nameof(ConvCoreRopAvs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreRopAvs RopAv { get; set; }
        [ForeignKey(nameof(RopMnId))]
        [InverseProperty(nameof(ConvCoreRopMns.ConvCoreGeologyIntervals))]
        public virtual ConvCoreRopMns RopMn { get; set; }
        [ForeignKey(nameof(RopMxId))]
        [InverseProperty(nameof(ConvCoreRopMxs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreRopMxs RopMx { get; set; }
        [ForeignKey(nameof(RpmAvId))]
        [InverseProperty(nameof(ConvCoreRpmAvs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreRpmAvs RpmAv { get; set; }
        [ForeignKey(nameof(ShowId))]
        [InverseProperty(nameof(ConvCoreShows.ConvCoreGeologyIntervals))]
        public virtual ConvCoreShows Show { get; set; }
        [ForeignKey(nameof(SizeMnId))]
        [InverseProperty(nameof(ConvCoreSizeMns.ConvCoreGeologyIntervals))]
        public virtual ConvCoreSizeMns SizeMn { get; set; }
        [ForeignKey(nameof(SizeMxId))]
        [InverseProperty(nameof(ConvCoreSizeMxs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreSizeMxs SizeMx { get; set; }
        [ForeignKey(nameof(TqAvId))]
        [InverseProperty(nameof(ConvCoreTqAvs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreTqAvs TqAv { get; set; }
        [ForeignKey(nameof(TvdBaseId))]
        [InverseProperty(nameof(ConvCoreTvdBases.ConvCoreGeologyIntervals))]
        public virtual ConvCoreTvdBases TvdBase { get; set; }
        [ForeignKey(nameof(TvdTopId))]
        [InverseProperty(nameof(ConvCoreTvdTops.ConvCoreGeologyIntervals))]
        public virtual ConvCoreTvdTops TvdTop { get; set; }
        [ForeignKey(nameof(WobAvId))]
        [InverseProperty(nameof(ConvCoreWobAvs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreWobAvs WobAv { get; set; }
        [ForeignKey(nameof(WtMudAvId))]
        [InverseProperty(nameof(ConvCoreWtMudAvs.ConvCoreGeologyIntervals))]
        public virtual ConvCoreWtMudAvs WtMudAv { get; set; }
        [InverseProperty("GeologyInterval")]
        public virtual ICollection<ConvCores> ConvCores { get; set; }
    }
}
