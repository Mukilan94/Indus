using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobReservoirIntervals
    {
        public StimJobReservoirIntervals()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int ReservoirIntervalId { get; set; }
        public int? MdLithTopId { get; set; }
        public int? MdLithBottomId { get; set; }
        public int? LithFormationPermeabilityId { get; set; }
        public int? LithYoungsModulusId { get; set; }
        public int? LithPorePresId { get; set; }
        public int? LithNetPayThicknessId { get; set; }
        public string LithName { get; set; }
        public int? MdGrossPayTopId { get; set; }
        public int? MdGrossPayBottomId { get; set; }
        public int? GrossPayThicknessId { get; set; }
        public int? NetPayThicknessId { get; set; }
        public int? NetPayPorePresId { get; set; }
        public int? NetPayFluidCompressibilityId { get; set; }
        public int? NetPayFluidViscosityId { get; set; }
        public string NetPayName { get; set; }
        public int? NetPayFormationPermeabilityId { get; set; }
        public int? LithPoissonsRatioId { get; set; }
        public int? NetPayFormationPorosityId { get; set; }
        public int? FormationPermeabilityId { get; set; }
        public int? FormationPorosityId { get; set; }
        public string NameFormation { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(FormationPermeabilityId))]
        [InverseProperty(nameof(StimJobFormationPermeabilitys.StimJobReservoirIntervals))]
        public virtual StimJobFormationPermeabilitys FormationPermeability { get; set; }
        [ForeignKey(nameof(FormationPorosityId))]
        [InverseProperty(nameof(StimJobFormationPorositys.StimJobReservoirIntervals))]
        public virtual StimJobFormationPorositys FormationPorosity { get; set; }
        [ForeignKey(nameof(GrossPayThicknessId))]
        [InverseProperty(nameof(StimJobGrossPayThicknesss.StimJobReservoirIntervals))]
        public virtual StimJobGrossPayThicknesss GrossPayThickness { get; set; }
        [ForeignKey(nameof(LithFormationPermeabilityId))]
        [InverseProperty(nameof(StimJobLithFormationPermeabilitys.StimJobReservoirIntervals))]
        public virtual StimJobLithFormationPermeabilitys LithFormationPermeability { get; set; }
        [ForeignKey(nameof(LithNetPayThicknessId))]
        [InverseProperty(nameof(StimJobLithNetPayThicknesss.StimJobReservoirIntervals))]
        public virtual StimJobLithNetPayThicknesss LithNetPayThickness { get; set; }
        [ForeignKey(nameof(LithPoissonsRatioId))]
        [InverseProperty(nameof(StimJobLithPoissonsRatios.StimJobReservoirIntervals))]
        public virtual StimJobLithPoissonsRatios LithPoissonsRatio { get; set; }
        [ForeignKey(nameof(LithPorePresId))]
        [InverseProperty(nameof(StimJobLithPorePress.StimJobReservoirIntervals))]
        public virtual StimJobLithPorePress LithPorePres { get; set; }
        [ForeignKey(nameof(LithYoungsModulusId))]
        [InverseProperty(nameof(StimJobLithYoungsModuluss.StimJobReservoirIntervals))]
        public virtual StimJobLithYoungsModuluss LithYoungsModulus { get; set; }
        [ForeignKey(nameof(MdGrossPayBottomId))]
        [InverseProperty(nameof(StimJobMdGrossPayBottoms.StimJobReservoirIntervals))]
        public virtual StimJobMdGrossPayBottoms MdGrossPayBottom { get; set; }
        [ForeignKey(nameof(MdGrossPayTopId))]
        [InverseProperty(nameof(StimJobMdGrossPayTops.StimJobReservoirIntervals))]
        public virtual StimJobMdGrossPayTops MdGrossPayTop { get; set; }
        [ForeignKey(nameof(MdLithBottomId))]
        [InverseProperty(nameof(StimJobMdLithBottoms.StimJobReservoirIntervals))]
        public virtual StimJobMdLithBottoms MdLithBottom { get; set; }
        [ForeignKey(nameof(MdLithTopId))]
        [InverseProperty(nameof(StimJobMdLithTops.StimJobReservoirIntervals))]
        public virtual StimJobMdLithTops MdLithTop { get; set; }
        [ForeignKey(nameof(NetPayFluidCompressibilityId))]
        [InverseProperty(nameof(StimJobNetPayFluidCompressibilitys.StimJobReservoirIntervals))]
        public virtual StimJobNetPayFluidCompressibilitys NetPayFluidCompressibility { get; set; }
        [ForeignKey(nameof(NetPayFluidViscosityId))]
        [InverseProperty(nameof(StimJobNetPayFluidViscositys.StimJobReservoirIntervals))]
        public virtual StimJobNetPayFluidViscositys NetPayFluidViscosity { get; set; }
        [ForeignKey(nameof(NetPayFormationPermeabilityId))]
        [InverseProperty(nameof(StimJobNetPayFormationPermeabilitys.StimJobReservoirIntervals))]
        public virtual StimJobNetPayFormationPermeabilitys NetPayFormationPermeability { get; set; }
        [ForeignKey(nameof(NetPayFormationPorosityId))]
        [InverseProperty(nameof(StimJobNetPayFormationPorositys.StimJobReservoirIntervals))]
        public virtual StimJobNetPayFormationPorositys NetPayFormationPorosity { get; set; }
        [ForeignKey(nameof(NetPayPorePresId))]
        [InverseProperty(nameof(StimJobNetPayPorePress.StimJobReservoirIntervals))]
        public virtual StimJobNetPayPorePress NetPayPorePres { get; set; }
        [ForeignKey(nameof(NetPayThicknessId))]
        [InverseProperty(nameof(StimJobNetPayThicknesss.StimJobReservoirIntervals))]
        public virtual StimJobNetPayThicknesss NetPayThickness { get; set; }
        [InverseProperty("ReservoirInterval")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
