using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportFluid
    {
        public FluidsReportFluid()
        {
            FluidsReportRheometer = new HashSet<FluidsReportRheometer>();
            FluidsReports = new HashSet<FluidsReports>();
        }

        [Key]
        public string Uid { get; set; }
        public string Type { get; set; }
        public string LocationSample { get; set; }
        public int? DensityId { get; set; }
        public int? VisFunnelId { get; set; }
        public int? TempVisId { get; set; }
        public int? PvReportPvId { get; set; }
        public int? YpReportYpId { get; set; }
        public int? Gel10SecId { get; set; }
        public int? Gel10MinId { get; set; }
        public int? Gel30MinId { get; set; }
        public int? FilterCakeLtlpId { get; set; }
        public int? FiltrateLtlpId { get; set; }
        public int? TempHthpId { get; set; }
        public int? PresHthpId { get; set; }
        public int? FiltrateHthpId { get; set; }
        public int? FilterCakeHthpId { get; set; }
        public int? SolidsPcId { get; set; }
        public int? WaterPcId { get; set; }
        public int? OilPcId { get; set; }
        public int? SandPcId { get; set; }
        public int? SolidsLowGravPcId { get; set; }
        public int? SolidsCalcPcId { get; set; }
        public int? BaritePcId { get; set; }
        public int? LcmId { get; set; }
        public int? MbtId { get; set; }
        public string Ph { get; set; }
        public int? TempPhId { get; set; }
        public int? PmId { get; set; }
        public int? PmFiltrateId { get; set; }
        public int? MfId { get; set; }
        [Column("AlkalinityP1Id")]
        public int? AlkalinityP1id { get; set; }
        [Column("AlkalinityP2Id")]
        public int? AlkalinityP2id { get; set; }
        public int? ChlorideId { get; set; }
        public int? CalciumId { get; set; }
        public int? MagnesiumId { get; set; }
        public int? PotassiumId { get; set; }
        public int? BrinePcId { get; set; }
        public int? LimeId { get; set; }
        public int? ElectStabId { get; set; }
        public int? CalciumChlorideId { get; set; }
        public string Company { get; set; }
        public string Engineer { get; set; }
        public string Asg { get; set; }
        public int? SolidsHiGravPcId { get; set; }
        public int? PolymerId { get; set; }
        public string PolyType { get; set; }
        public int? SolCorPcId { get; set; }
        public int? OilCtgId { get; set; }
        public int? HardnessCaId { get; set; }
        public int? SulfideId { get; set; }
        public string Comments { get; set; }

        [ForeignKey(nameof(AlkalinityP1id))]
        [InverseProperty(nameof(FluidsReportAlkalinityP1.FluidsReportFluid))]
        public virtual FluidsReportAlkalinityP1 AlkalinityP1 { get; set; }
        [ForeignKey(nameof(AlkalinityP2id))]
        [InverseProperty(nameof(FluidsReportAlkalinityP2.FluidsReportFluid))]
        public virtual FluidsReportAlkalinityP2 AlkalinityP2 { get; set; }
        [ForeignKey(nameof(BaritePcId))]
        [InverseProperty(nameof(FluidsReportBaritePc.FluidsReportFluid))]
        public virtual FluidsReportBaritePc BaritePc { get; set; }
        [ForeignKey(nameof(BrinePcId))]
        [InverseProperty(nameof(FluidsReportBrinePc.FluidsReportFluid))]
        public virtual FluidsReportBrinePc BrinePc { get; set; }
        [ForeignKey(nameof(CalciumId))]
        [InverseProperty(nameof(FluidsReportCalcium.FluidsReportFluid))]
        public virtual FluidsReportCalcium Calcium { get; set; }
        [ForeignKey(nameof(CalciumChlorideId))]
        [InverseProperty(nameof(FluidsReportCalciumChloride.FluidsReportFluid))]
        public virtual FluidsReportCalciumChloride CalciumChloride { get; set; }
        [ForeignKey(nameof(ChlorideId))]
        [InverseProperty(nameof(FluidsReportChloride.FluidsReportFluid))]
        public virtual FluidsReportChloride Chloride { get; set; }
        [ForeignKey(nameof(DensityId))]
        [InverseProperty(nameof(FluidsReportDensity.FluidsReportFluid))]
        public virtual FluidsReportDensity Density { get; set; }
        [ForeignKey(nameof(ElectStabId))]
        [InverseProperty(nameof(FluidsReportElectStab.FluidsReportFluid))]
        public virtual FluidsReportElectStab ElectStab { get; set; }
        [ForeignKey(nameof(FilterCakeHthpId))]
        [InverseProperty(nameof(FluidsReportFilterCakeHthp.FluidsReportFluid))]
        public virtual FluidsReportFilterCakeHthp FilterCakeHthp { get; set; }
        [ForeignKey(nameof(FilterCakeLtlpId))]
        [InverseProperty(nameof(FluidsReportFilterCakeLtlp.FluidsReportFluid))]
        public virtual FluidsReportFilterCakeLtlp FilterCakeLtlp { get; set; }
        [ForeignKey(nameof(FiltrateHthpId))]
        [InverseProperty(nameof(FluidsReportFiltrateHthp.FluidsReportFluid))]
        public virtual FluidsReportFiltrateHthp FiltrateHthp { get; set; }
        [ForeignKey(nameof(FiltrateLtlpId))]
        [InverseProperty(nameof(FluidsReportFiltrateLtlp.FluidsReportFluid))]
        public virtual FluidsReportFiltrateLtlp FiltrateLtlp { get; set; }
        [ForeignKey(nameof(Gel10MinId))]
        [InverseProperty(nameof(FluidsReportGel10Min.FluidsReportFluid))]
        public virtual FluidsReportGel10Min Gel10Min { get; set; }
        [ForeignKey(nameof(Gel10SecId))]
        [InverseProperty(nameof(FluidsReportGel10Sec.FluidsReportFluid))]
        public virtual FluidsReportGel10Sec Gel10Sec { get; set; }
        [ForeignKey(nameof(Gel30MinId))]
        [InverseProperty(nameof(FluidsReportGel30Min.FluidsReportFluid))]
        public virtual FluidsReportGel30Min Gel30Min { get; set; }
        [ForeignKey(nameof(HardnessCaId))]
        [InverseProperty(nameof(FluidsReportHardnessCa.FluidsReportFluid))]
        public virtual FluidsReportHardnessCa HardnessCa { get; set; }
        [ForeignKey(nameof(LcmId))]
        [InverseProperty(nameof(FluidsReportLcm.FluidsReportFluid))]
        public virtual FluidsReportLcm Lcm { get; set; }
        [ForeignKey(nameof(LimeId))]
        [InverseProperty(nameof(FluidsReportLime.FluidsReportFluid))]
        public virtual FluidsReportLime Lime { get; set; }
        [ForeignKey(nameof(MagnesiumId))]
        [InverseProperty(nameof(FluidsReportMagnesium.FluidsReportFluid))]
        public virtual FluidsReportMagnesium Magnesium { get; set; }
        [ForeignKey(nameof(MbtId))]
        [InverseProperty(nameof(FluidsReportMbt.FluidsReportFluid))]
        public virtual FluidsReportMbt Mbt { get; set; }
        [ForeignKey(nameof(MfId))]
        [InverseProperty(nameof(FluidsReportMf.FluidsReportFluid))]
        public virtual FluidsReportMf Mf { get; set; }
        [ForeignKey(nameof(OilCtgId))]
        [InverseProperty(nameof(FluidsReportOilCtg.FluidsReportFluid))]
        public virtual FluidsReportOilCtg OilCtg { get; set; }
        [ForeignKey(nameof(OilPcId))]
        [InverseProperty(nameof(FluidsReportOilPc.FluidsReportFluid))]
        public virtual FluidsReportOilPc OilPc { get; set; }
        [ForeignKey(nameof(PmId))]
        [InverseProperty(nameof(FluidsReportPm.FluidsReportFluid))]
        public virtual FluidsReportPm Pm { get; set; }
        [ForeignKey(nameof(PmFiltrateId))]
        [InverseProperty(nameof(FluidsReportPmFiltrate.FluidsReportFluid))]
        public virtual FluidsReportPmFiltrate PmFiltrate { get; set; }
        [ForeignKey(nameof(PolymerId))]
        [InverseProperty(nameof(FluidsReportPolymer.FluidsReportFluid))]
        public virtual FluidsReportPolymer Polymer { get; set; }
        [ForeignKey(nameof(PotassiumId))]
        [InverseProperty(nameof(FluidsReportPotassium.FluidsReportFluid))]
        public virtual FluidsReportPotassium Potassium { get; set; }
        [ForeignKey(nameof(PresHthpId))]
        [InverseProperty(nameof(FluidsReportPresHthp.FluidsReportFluid))]
        public virtual FluidsReportPresHthp PresHthp { get; set; }
        [ForeignKey(nameof(PvReportPvId))]
        [InverseProperty(nameof(FluidsReportPv.FluidsReportFluid))]
        public virtual FluidsReportPv PvReportPv { get; set; }
        [ForeignKey(nameof(SandPcId))]
        [InverseProperty(nameof(FluidsReportSandPc.FluidsReportFluid))]
        public virtual FluidsReportSandPc SandPc { get; set; }
        [ForeignKey(nameof(SolCorPcId))]
        [InverseProperty(nameof(FluidsReportSolCorPc.FluidsReportFluid))]
        public virtual FluidsReportSolCorPc SolCorPc { get; set; }
        [ForeignKey(nameof(SolidsCalcPcId))]
        [InverseProperty(nameof(FluidsReportSolidsCalcPc.FluidsReportFluid))]
        public virtual FluidsReportSolidsCalcPc SolidsCalcPc { get; set; }
        [ForeignKey(nameof(SolidsHiGravPcId))]
        [InverseProperty(nameof(FluidsReportSolidsHiGravPc.FluidsReportFluid))]
        public virtual FluidsReportSolidsHiGravPc SolidsHiGravPc { get; set; }
        [ForeignKey(nameof(SolidsLowGravPcId))]
        [InverseProperty(nameof(FluidsReportSolidsLowGravPc.FluidsReportFluid))]
        public virtual FluidsReportSolidsLowGravPc SolidsLowGravPc { get; set; }
        [ForeignKey(nameof(SolidsPcId))]
        [InverseProperty(nameof(FluidsReportSolidsPc.FluidsReportFluid))]
        public virtual FluidsReportSolidsPc SolidsPc { get; set; }
        [ForeignKey(nameof(SulfideId))]
        [InverseProperty(nameof(FluidsReportSulfide.FluidsReportFluid))]
        public virtual FluidsReportSulfide Sulfide { get; set; }
        [ForeignKey(nameof(TempHthpId))]
        [InverseProperty(nameof(FluidsReportTempHthp.FluidsReportFluid))]
        public virtual FluidsReportTempHthp TempHthp { get; set; }
        [ForeignKey(nameof(TempPhId))]
        [InverseProperty(nameof(FluidsReportTempPh.FluidsReportFluid))]
        public virtual FluidsReportTempPh TempPh { get; set; }
        [ForeignKey(nameof(TempVisId))]
        [InverseProperty(nameof(FluidsReportTempVis.FluidsReportFluid))]
        public virtual FluidsReportTempVis TempVis { get; set; }
        [ForeignKey(nameof(VisFunnelId))]
        [InverseProperty(nameof(FluidsReportVisFunnel.FluidsReportFluid))]
        public virtual FluidsReportVisFunnel VisFunnel { get; set; }
        [ForeignKey(nameof(WaterPcId))]
        [InverseProperty(nameof(FluidsReportWaterPc.FluidsReportFluid))]
        public virtual FluidsReportWaterPc WaterPc { get; set; }
        [ForeignKey(nameof(YpReportYpId))]
        [InverseProperty(nameof(FluidsReportYp.FluidsReportFluid))]
        public virtual FluidsReportYp YpReportYp { get; set; }
        [InverseProperty("FluidsReportFluidU")]
        public virtual ICollection<FluidsReportRheometer> FluidsReportRheometer { get; set; }
        [InverseProperty("FluidU")]
        public virtual ICollection<FluidsReports> FluidsReports { get; set; }
    }
}
