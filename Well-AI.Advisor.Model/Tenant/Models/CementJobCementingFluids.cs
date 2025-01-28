using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobCementingFluids
    {
        public CementJobCementingFluids()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int CementingFluidId { get; set; }
        public int FluidIndex { get; set; }
        public string TypeFluid { get; set; }
        public string DescFluid { get; set; }
        public string Purpose { get; set; }
        public string ClassSlurryDryBlend { get; set; }
        public int? MdFluidTopId { get; set; }
        public int? MdFluidBottomId { get; set; }
        public string SourceWater { get; set; }
        public int? VolWaterId { get; set; }
        public int? VolCementId { get; set; }
        public int? RatioMixWaterId { get; set; }
        public int? VolFluidId { get; set; }
        public int? CementPumpScheduleId { get; set; }
        public int? ExcessPcId { get; set; }
        public int? VolYieldId { get; set; }
        public int? DensityId { get; set; }
        public int? SolidVolumeFractionId { get; set; }
        public int? VolPumpedId { get; set; }
        public int? VolOtherId { get; set; }
        public string FluidRheologicalModel { get; set; }
        public int? VisId { get; set; }
        public int? YpJobYpId { get; set; }
        [Column("NId")]
        public int? Nid { get; set; }
        [Column("KId")]
        public int? Kid { get; set; }
        public int? Gel10SecReadingId { get; set; }
        public int? Gel10SecStrengthId { get; set; }
        public int? Gel1MinReadingId { get; set; }
        public int? Gel1MinStrengthId { get; set; }
        public int? Gel10MinReadingId { get; set; }
        public int? Gel10MinStrengthId { get; set; }
        public string TypeBaseFluid { get; set; }
        public int? DensBaseFluidId { get; set; }
        public string DryBlendName { get; set; }
        public string DryBlendDescription { get; set; }
        public int? MassDryBlendId { get; set; }
        public int? DensDryBlendId { get; set; }
        public int? MassSackDryBlendId { get; set; }
        public int? CementAdditiveId { get; set; }
        public string FoamUsed { get; set; }
        public string TypeGasFoam { get; set; }
        public int? VolGasFoamId { get; set; }
        public int? RatioConstGasMethodAvId { get; set; }
        public int? DensConstGasMethodId { get; set; }
        public int? RatioConstGasMethodStartId { get; set; }
        public int? RatioConstGasMethodEndId { get; set; }
        public int? DensConstGasFoamId { get; set; }
        [Column("ETimThickeningId")]
        public int? EtimThickeningId { get; set; }
        public int? TempThickeningId { get; set; }
        public int? PresTestThickeningId { get; set; }
        public int? ConsTestThickeningId { get; set; }
        public int? PcFreeWaterId { get; set; }
        public int? TempFreeWaterId { get; set; }
        public int? VolTestFluidLossId { get; set; }
        public int? TempFluidLossId { get; set; }
        public int? PresTestFluidLossId { get; set; }
        public int? TimeFluidLossId { get; set; }
        [Column("VolAPIFluidLossId")]
        public int? VolApifluidLossId { get; set; }
        [Column("ETimComprStren1Id")]
        public int? EtimComprStren1Id { get; set; }
        [Column("ETimComprStren2Id")]
        public int? EtimComprStren2Id { get; set; }
        public int? PresComprStren1Id { get; set; }
        public int? PresComprStren2Id { get; set; }
        public int? TempComprStren1Id { get; set; }
        public int? TempComprStren2Id { get; set; }
        public int? DensAtPresId { get; set; }
        public int? VolReservedId { get; set; }
        public int? VolTotSlurryId { get; set; }

        [ForeignKey(nameof(CementAdditiveId))]
        [InverseProperty(nameof(CementJobCementAdditives.CementJobCementingFluids))]
        public virtual CementJobCementAdditives CementAdditive { get; set; }
        [ForeignKey(nameof(CementPumpScheduleId))]
        [InverseProperty(nameof(CementJobCementPumpSchedules.CementJobCementingFluids))]
        public virtual CementJobCementPumpSchedules CementPumpSchedule { get; set; }
        [ForeignKey(nameof(ConsTestThickeningId))]
        [InverseProperty(nameof(CementJobConsTestThickenings.CementJobCementingFluids))]
        public virtual CementJobConsTestThickenings ConsTestThickening { get; set; }
        [ForeignKey(nameof(DensAtPresId))]
        [InverseProperty(nameof(CementJobDensAtPress.CementJobCementingFluids))]
        public virtual CementJobDensAtPress DensAtPres { get; set; }
        [ForeignKey(nameof(DensBaseFluidId))]
        [InverseProperty(nameof(CementJobDensBaseFluids.CementJobCementingFluids))]
        public virtual CementJobDensBaseFluids DensBaseFluid { get; set; }
        [ForeignKey(nameof(DensConstGasFoamId))]
        [InverseProperty(nameof(CementJobDensConstGasFoams.CementJobCementingFluids))]
        public virtual CementJobDensConstGasFoams DensConstGasFoam { get; set; }
        [ForeignKey(nameof(DensConstGasMethodId))]
        [InverseProperty(nameof(CementJobDensConstGasMethods.CementJobCementingFluids))]
        public virtual CementJobDensConstGasMethods DensConstGasMethod { get; set; }
        [ForeignKey(nameof(DensDryBlendId))]
        [InverseProperty(nameof(CementJobDensDryBlends.CementJobCementingFluids))]
        public virtual CementJobDensDryBlends DensDryBlend { get; set; }
        [ForeignKey(nameof(DensityId))]
        [InverseProperty(nameof(CementJobDensitys.CementJobCementingFluids))]
        public virtual CementJobDensitys Density { get; set; }
        [ForeignKey(nameof(EtimComprStren1Id))]
        [InverseProperty(nameof(CementJobEtimComprStren1s.CementJobCementingFluids))]
        public virtual CementJobEtimComprStren1s EtimComprStren1 { get; set; }
        [ForeignKey(nameof(EtimComprStren2Id))]
        [InverseProperty(nameof(CementJobEtimComprStren2s.CementJobCementingFluids))]
        public virtual CementJobEtimComprStren2s EtimComprStren2 { get; set; }
        [ForeignKey(nameof(EtimThickeningId))]
        [InverseProperty(nameof(CementJobEtimThickenings.CementJobCementingFluids))]
        public virtual CementJobEtimThickenings EtimThickening { get; set; }
        [ForeignKey(nameof(ExcessPcId))]
        [InverseProperty(nameof(CementJobExcessPcs.CementJobCementingFluids))]
        public virtual CementJobExcessPcs ExcessPc { get; set; }
        [ForeignKey(nameof(Gel10MinReadingId))]
        [InverseProperty(nameof(CementJobGel10MinReadings.CementJobCementingFluids))]
        public virtual CementJobGel10MinReadings Gel10MinReading { get; set; }
        [ForeignKey(nameof(Gel10MinStrengthId))]
        [InverseProperty(nameof(CementJobGel10MinStrengths.CementJobCementingFluids))]
        public virtual CementJobGel10MinStrengths Gel10MinStrength { get; set; }
        [ForeignKey(nameof(Gel10SecReadingId))]
        [InverseProperty(nameof(CementJobGel10SecReadings.CementJobCementingFluids))]
        public virtual CementJobGel10SecReadings Gel10SecReading { get; set; }
        [ForeignKey(nameof(Gel10SecStrengthId))]
        [InverseProperty(nameof(CementJobGel10SecStrengths.CementJobCementingFluids))]
        public virtual CementJobGel10SecStrengths Gel10SecStrength { get; set; }
        [ForeignKey(nameof(Gel1MinReadingId))]
        [InverseProperty(nameof(CementJobGel1MinReadings.CementJobCementingFluids))]
        public virtual CementJobGel1MinReadings Gel1MinReading { get; set; }
        [ForeignKey(nameof(Gel1MinStrengthId))]
        [InverseProperty(nameof(CementJobGel1MinStrengths.CementJobCementingFluids))]
        public virtual CementJobGel1MinStrengths Gel1MinStrength { get; set; }
        [ForeignKey(nameof(Kid))]
        [InverseProperty(nameof(CementJobKs.CementJobCementingFluids))]
        public virtual CementJobKs K { get; set; }
        [ForeignKey(nameof(MassDryBlendId))]
        [InverseProperty(nameof(CementJobMassDryBlends.CementJobCementingFluids))]
        public virtual CementJobMassDryBlends MassDryBlend { get; set; }
        [ForeignKey(nameof(MassSackDryBlendId))]
        [InverseProperty(nameof(CementJobMassSackDryBlends.CementJobCementingFluids))]
        public virtual CementJobMassSackDryBlends MassSackDryBlend { get; set; }
        [ForeignKey(nameof(MdFluidBottomId))]
        [InverseProperty(nameof(CementJobMdFluidBottoms.CementJobCementingFluids))]
        public virtual CementJobMdFluidBottoms MdFluidBottom { get; set; }
        [ForeignKey(nameof(MdFluidTopId))]
        [InverseProperty(nameof(CementJobMdFluidTops.CementJobCementingFluids))]
        public virtual CementJobMdFluidTops MdFluidTop { get; set; }
        [ForeignKey(nameof(Nid))]
        [InverseProperty(nameof(CementJobNs.CementJobCementingFluids))]
        public virtual CementJobNs N { get; set; }
        [ForeignKey(nameof(PcFreeWaterId))]
        [InverseProperty(nameof(CementJobPcFreeWaters.CementJobCementingFluids))]
        public virtual CementJobPcFreeWaters PcFreeWater { get; set; }
        [ForeignKey(nameof(PresComprStren1Id))]
        [InverseProperty(nameof(CementJobPresComprStren1s.CementJobCementingFluids))]
        public virtual CementJobPresComprStren1s PresComprStren1 { get; set; }
        [ForeignKey(nameof(PresComprStren2Id))]
        [InverseProperty(nameof(CementJobPresComprStren2s.CementJobCementingFluids))]
        public virtual CementJobPresComprStren2s PresComprStren2 { get; set; }
        [ForeignKey(nameof(PresTestFluidLossId))]
        [InverseProperty(nameof(CementJobPresTestFluidLosss.CementJobCementingFluids))]
        public virtual CementJobPresTestFluidLosss PresTestFluidLoss { get; set; }
        [ForeignKey(nameof(PresTestThickeningId))]
        [InverseProperty(nameof(CementJobPresTestThickenings.CementJobCementingFluids))]
        public virtual CementJobPresTestThickenings PresTestThickening { get; set; }
        [ForeignKey(nameof(RatioConstGasMethodAvId))]
        [InverseProperty(nameof(CementJobRatioConstGasMethodAvs.CementJobCementingFluids))]
        public virtual CementJobRatioConstGasMethodAvs RatioConstGasMethodAv { get; set; }
        [ForeignKey(nameof(RatioConstGasMethodEndId))]
        [InverseProperty(nameof(CementJobRatioConstGasMethodEnds.CementJobCementingFluids))]
        public virtual CementJobRatioConstGasMethodEnds RatioConstGasMethodEnd { get; set; }
        [ForeignKey(nameof(RatioConstGasMethodStartId))]
        [InverseProperty(nameof(CementJobRatioConstGasMethodStarts.CementJobCementingFluids))]
        public virtual CementJobRatioConstGasMethodStarts RatioConstGasMethodStart { get; set; }
        [ForeignKey(nameof(RatioMixWaterId))]
        [InverseProperty(nameof(CementJobRatioMixWaters.CementJobCementingFluids))]
        public virtual CementJobRatioMixWaters RatioMixWater { get; set; }
        [ForeignKey(nameof(SolidVolumeFractionId))]
        [InverseProperty(nameof(CementJobSolidVolumeFractions.CementJobCementingFluids))]
        public virtual CementJobSolidVolumeFractions SolidVolumeFraction { get; set; }
        [ForeignKey(nameof(TempComprStren1Id))]
        [InverseProperty(nameof(CementJobTempComprStren1s.CementJobCementingFluids))]
        public virtual CementJobTempComprStren1s TempComprStren1 { get; set; }
        [ForeignKey(nameof(TempComprStren2Id))]
        [InverseProperty(nameof(CementJobTempComprStren2s.CementJobCementingFluids))]
        public virtual CementJobTempComprStren2s TempComprStren2 { get; set; }
        [ForeignKey(nameof(TempFluidLossId))]
        [InverseProperty(nameof(CementJobTempFluidLosss.CementJobCementingFluids))]
        public virtual CementJobTempFluidLosss TempFluidLoss { get; set; }
        [ForeignKey(nameof(TempFreeWaterId))]
        [InverseProperty(nameof(CementJobTempFreeWaters.CementJobCementingFluids))]
        public virtual CementJobTempFreeWaters TempFreeWater { get; set; }
        [ForeignKey(nameof(TempThickeningId))]
        [InverseProperty(nameof(CementJobTempThickenings.CementJobCementingFluids))]
        public virtual CementJobTempThickenings TempThickening { get; set; }
        [ForeignKey(nameof(TimeFluidLossId))]
        [InverseProperty(nameof(CementJobTimeFluidLosss.CementJobCementingFluids))]
        public virtual CementJobTimeFluidLosss TimeFluidLoss { get; set; }
        [ForeignKey(nameof(VisId))]
        [InverseProperty(nameof(CementJobViss.CementJobCementingFluids))]
        public virtual CementJobViss Vis { get; set; }
        [ForeignKey(nameof(VolApifluidLossId))]
        [InverseProperty(nameof(CementJobVolApifluidLosss.CementJobCementingFluids))]
        public virtual CementJobVolApifluidLosss VolApifluidLoss { get; set; }
        [ForeignKey(nameof(VolCementId))]
        [InverseProperty(nameof(CementJobVolCements.CementJobCementingFluids))]
        public virtual CementJobVolCements VolCement { get; set; }
        [ForeignKey(nameof(VolFluidId))]
        [InverseProperty(nameof(CementJobVolFluids.CementJobCementingFluids))]
        public virtual CementJobVolFluids VolFluid { get; set; }
        [ForeignKey(nameof(VolGasFoamId))]
        [InverseProperty(nameof(CementJobVolGasFoams.CementJobCementingFluids))]
        public virtual CementJobVolGasFoams VolGasFoam { get; set; }
        [ForeignKey(nameof(VolOtherId))]
        [InverseProperty(nameof(CementJobVolOthers.CementJobCementingFluids))]
        public virtual CementJobVolOthers VolOther { get; set; }
        [ForeignKey(nameof(VolPumpedId))]
        [InverseProperty(nameof(CementJobVolPumpeds.CementJobCementingFluids))]
        public virtual CementJobVolPumpeds VolPumped { get; set; }
        [ForeignKey(nameof(VolReservedId))]
        [InverseProperty(nameof(CementJobVolReserveds.CementJobCementingFluids))]
        public virtual CementJobVolReserveds VolReserved { get; set; }
        [ForeignKey(nameof(VolTestFluidLossId))]
        [InverseProperty(nameof(CementJobVolTestFluidLosss.CementJobCementingFluids))]
        public virtual CementJobVolTestFluidLosss VolTestFluidLoss { get; set; }
        [ForeignKey(nameof(VolTotSlurryId))]
        [InverseProperty(nameof(CementJobVolTotSlurrys.CementJobCementingFluids))]
        public virtual CementJobVolTotSlurrys VolTotSlurry { get; set; }
        [ForeignKey(nameof(VolWaterId))]
        [InverseProperty(nameof(CementJobVolWaters.CementJobCementingFluids))]
        public virtual CementJobVolWaters VolWater { get; set; }
        [ForeignKey(nameof(VolYieldId))]
        [InverseProperty(nameof(CementJobVolYields.CementJobCementingFluids))]
        public virtual CementJobVolYields VolYield { get; set; }
        [ForeignKey(nameof(YpJobYpId))]
        [InverseProperty(nameof(CementJobYps.CementJobCementingFluids))]
        public virtual CementJobYps YpJobYp { get; set; }
        [InverseProperty("CementingFluid")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
