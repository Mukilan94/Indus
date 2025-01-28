using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportFluids
    {
        public OpsReportFluids()
        {
            OpsReportRheometers = new HashSet<OpsReportRheometers>();
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public string Uid { get; set; }
        public string Type { get; set; }
        public string LocationSample { get; set; }
        public int? DensityId { get; set; }
        public int? VisFunnelId { get; set; }
        public int? TempVisId { get; set; }
        public int? PvReportPvId { get; set; }
        public int? YpId { get; set; }
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
        public int? CalciumMagnesiumId { get; set; }
        public int? MagnesiumId { get; set; }
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
        [InverseProperty(nameof(OpsReportAlkalinityP1s.OpsReportFluids))]
        public virtual OpsReportAlkalinityP1s AlkalinityP1 { get; set; }
        [ForeignKey(nameof(AlkalinityP2id))]
        [InverseProperty(nameof(OpsReportAlkalinityP2s.OpsReportFluids))]
        public virtual OpsReportAlkalinityP2s AlkalinityP2 { get; set; }
        [ForeignKey(nameof(BaritePcId))]
        [InverseProperty(nameof(OpsReportBaritePcs.OpsReportFluids))]
        public virtual OpsReportBaritePcs BaritePc { get; set; }
        [ForeignKey(nameof(BrinePcId))]
        [InverseProperty(nameof(OpsReportBrinePcs.OpsReportFluids))]
        public virtual OpsReportBrinePcs BrinePc { get; set; }
        [ForeignKey(nameof(CalciumChlorideId))]
        [InverseProperty(nameof(OpsReportCalciumChlorides.OpsReportFluids))]
        public virtual OpsReportCalciumChlorides CalciumChloride { get; set; }
        [ForeignKey(nameof(CalciumMagnesiumId))]
        [InverseProperty(nameof(OpsReportCalciums.OpsReportFluids))]
        public virtual OpsReportCalciums CalciumMagnesium { get; set; }
        [ForeignKey(nameof(ChlorideId))]
        [InverseProperty(nameof(OpsReportChlorides.OpsReportFluids))]
        public virtual OpsReportChlorides Chloride { get; set; }
        [ForeignKey(nameof(DensityId))]
        [InverseProperty(nameof(OpsReportDensitys.OpsReportFluids))]
        public virtual OpsReportDensitys Density { get; set; }
        [ForeignKey(nameof(ElectStabId))]
        [InverseProperty(nameof(OpsReportElectStabs.OpsReportFluids))]
        public virtual OpsReportElectStabs ElectStab { get; set; }
        [ForeignKey(nameof(FilterCakeHthpId))]
        [InverseProperty(nameof(OpsReportFilterCakeHthps.OpsReportFluids))]
        public virtual OpsReportFilterCakeHthps FilterCakeHthp { get; set; }
        [ForeignKey(nameof(FilterCakeLtlpId))]
        [InverseProperty(nameof(OpsReportFilterCakeLtlps.OpsReportFluids))]
        public virtual OpsReportFilterCakeLtlps FilterCakeLtlp { get; set; }
        [ForeignKey(nameof(FiltrateHthpId))]
        [InverseProperty(nameof(OpsReportFiltrateHthps.OpsReportFluids))]
        public virtual OpsReportFiltrateHthps FiltrateHthp { get; set; }
        [ForeignKey(nameof(FiltrateLtlpId))]
        [InverseProperty(nameof(OpsReportFiltrateLtlps.OpsReportFluids))]
        public virtual OpsReportFiltrateLtlps FiltrateLtlp { get; set; }
        [ForeignKey(nameof(Gel10MinId))]
        [InverseProperty(nameof(OpsReportGel10Mins.OpsReportFluids))]
        public virtual OpsReportGel10Mins Gel10Min { get; set; }
        [ForeignKey(nameof(Gel10SecId))]
        [InverseProperty(nameof(OpsReportGel10Secs.OpsReportFluids))]
        public virtual OpsReportGel10Secs Gel10Sec { get; set; }
        [ForeignKey(nameof(Gel30MinId))]
        [InverseProperty(nameof(OpsReportGel30Mins.OpsReportFluids))]
        public virtual OpsReportGel30Mins Gel30Min { get; set; }
        [ForeignKey(nameof(HardnessCaId))]
        [InverseProperty(nameof(OpsReportHardnessCas.OpsReportFluids))]
        public virtual OpsReportHardnessCas HardnessCa { get; set; }
        [ForeignKey(nameof(LcmId))]
        [InverseProperty(nameof(OpsReportLcms.OpsReportFluids))]
        public virtual OpsReportLcms Lcm { get; set; }
        [ForeignKey(nameof(LimeId))]
        [InverseProperty(nameof(OpsReportLimes.OpsReportFluids))]
        public virtual OpsReportLimes Lime { get; set; }
        [ForeignKey(nameof(MagnesiumId))]
        [InverseProperty(nameof(OpsReportMagnesiums.OpsReportFluids))]
        public virtual OpsReportMagnesiums Magnesium { get; set; }
        [ForeignKey(nameof(MbtId))]
        [InverseProperty(nameof(OpsReportMbts.OpsReportFluids))]
        public virtual OpsReportMbts Mbt { get; set; }
        [ForeignKey(nameof(MfId))]
        [InverseProperty(nameof(OpsReportMfs.OpsReportFluids))]
        public virtual OpsReportMfs Mf { get; set; }
        [ForeignKey(nameof(OilCtgId))]
        [InverseProperty(nameof(OpsReportOilCtgs.OpsReportFluids))]
        public virtual OpsReportOilCtgs OilCtg { get; set; }
        [ForeignKey(nameof(OilPcId))]
        [InverseProperty(nameof(OpsReportOilPcs.OpsReportFluids))]
        public virtual OpsReportOilPcs OilPc { get; set; }
        [ForeignKey(nameof(PmId))]
        [InverseProperty(nameof(OpsReportPms.OpsReportFluids))]
        public virtual OpsReportPms Pm { get; set; }
        [ForeignKey(nameof(PmFiltrateId))]
        [InverseProperty(nameof(OpsReportPmFiltrates.OpsReportFluids))]
        public virtual OpsReportPmFiltrates PmFiltrate { get; set; }
        [ForeignKey(nameof(PolymerId))]
        [InverseProperty(nameof(OpsReportPolymers.OpsReportFluids))]
        public virtual OpsReportPolymers Polymer { get; set; }
        [ForeignKey(nameof(PresHthpId))]
        [InverseProperty(nameof(OpsReportPresHthps.OpsReportFluids))]
        public virtual OpsReportPresHthps PresHthp { get; set; }
        [ForeignKey(nameof(PvReportPvId))]
        [InverseProperty(nameof(OpsReportPvs.OpsReportFluids))]
        public virtual OpsReportPvs PvReportPv { get; set; }
        [ForeignKey(nameof(SandPcId))]
        [InverseProperty(nameof(OpsReportSandPcs.OpsReportFluids))]
        public virtual OpsReportSandPcs SandPc { get; set; }
        [ForeignKey(nameof(SolCorPcId))]
        [InverseProperty(nameof(OpsReportSolCorPcs.OpsReportFluids))]
        public virtual OpsReportSolCorPcs SolCorPc { get; set; }
        [ForeignKey(nameof(SolidsCalcPcId))]
        [InverseProperty(nameof(OpsReportSolidsCalcPcs.OpsReportFluids))]
        public virtual OpsReportSolidsCalcPcs SolidsCalcPc { get; set; }
        [ForeignKey(nameof(SolidsHiGravPcId))]
        [InverseProperty(nameof(OpsReportSolidsHiGravPcs.OpsReportFluids))]
        public virtual OpsReportSolidsHiGravPcs SolidsHiGravPc { get; set; }
        [ForeignKey(nameof(SolidsLowGravPcId))]
        [InverseProperty(nameof(OpsReportSolidsLowGravPcs.OpsReportFluids))]
        public virtual OpsReportSolidsLowGravPcs SolidsLowGravPc { get; set; }
        [ForeignKey(nameof(SolidsPcId))]
        [InverseProperty(nameof(OpsReportSolidsPcs.OpsReportFluids))]
        public virtual OpsReportSolidsPcs SolidsPc { get; set; }
        [ForeignKey(nameof(SulfideId))]
        [InverseProperty(nameof(OpsReportSulfides.OpsReportFluids))]
        public virtual OpsReportSulfides Sulfide { get; set; }
        [ForeignKey(nameof(TempHthpId))]
        [InverseProperty(nameof(OpsReportTempHthps.OpsReportFluids))]
        public virtual OpsReportTempHthps TempHthp { get; set; }
        [ForeignKey(nameof(TempPhId))]
        [InverseProperty(nameof(OpsReportTempPhs.OpsReportFluids))]
        public virtual OpsReportTempPhs TempPh { get; set; }
        [ForeignKey(nameof(TempVisId))]
        [InverseProperty(nameof(OpsReportTempViss.OpsReportFluids))]
        public virtual OpsReportTempViss TempVis { get; set; }
        [ForeignKey(nameof(VisFunnelId))]
        [InverseProperty(nameof(OpsReportVisFunnels.OpsReportFluids))]
        public virtual OpsReportVisFunnels VisFunnel { get; set; }
        [ForeignKey(nameof(WaterPcId))]
        [InverseProperty(nameof(OpsReportWaterPcs.OpsReportFluids))]
        public virtual OpsReportWaterPcs WaterPc { get; set; }
        [ForeignKey(nameof(YpId))]
        [InverseProperty(nameof(OpsReportYps.OpsReportFluids))]
        public virtual OpsReportYps Yp { get; set; }
        [InverseProperty("OpsReportFluidU")]
        public virtual ICollection<OpsReportRheometers> OpsReportRheometers { get; set; }
        [InverseProperty("FluidU")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
