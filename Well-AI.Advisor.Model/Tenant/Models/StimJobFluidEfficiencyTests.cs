using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobFluidEfficiencyTests
    {
        public StimJobFluidEfficiencyTests()
        {
            StimJobPdatSessions = new HashSet<StimJobPdatSessions>();
        }

        [Key]
        public int FluidEfficiencyTestId { get; set; }
        [Column("DTimStart")]
        public string DtimStart { get; set; }
        [Column("DTimEnd")]
        public string DtimEnd { get; set; }
        public int? EndPdlDurationId { get; set; }
        public int? FractureCloseDurationId { get; set; }
        public int? FractureClosePresId { get; set; }
        public int? FractureExtensionPresId { get; set; }
        public int? NetPresId { get; set; }
        public int? PorePresId { get; set; }
        public int? PseudoRadialPresId { get; set; }
        public int? FractureLengthId { get; set; }
        public int? FractureWidthId { get; set; }
        public int? FluidEfficiencyId { get; set; }
        public string PdlCoef { get; set; }
        public int? ResidualPermeabilityId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(EndPdlDurationId))]
        [InverseProperty(nameof(StimJobEndPdlDurations.StimJobFluidEfficiencyTests))]
        public virtual StimJobEndPdlDurations EndPdlDuration { get; set; }
        [ForeignKey(nameof(FluidEfficiencyId))]
        [InverseProperty(nameof(StimJobFluidEfficiencys.StimJobFluidEfficiencyTests))]
        public virtual StimJobFluidEfficiencys FluidEfficiency { get; set; }
        [ForeignKey(nameof(FractureCloseDurationId))]
        [InverseProperty(nameof(StimJobFractureCloseDurations.StimJobFluidEfficiencyTests))]
        public virtual StimJobFractureCloseDurations FractureCloseDuration { get; set; }
        [ForeignKey(nameof(FractureClosePresId))]
        [InverseProperty(nameof(StimJobFractureClosePress.StimJobFluidEfficiencyTests))]
        public virtual StimJobFractureClosePress FractureClosePres { get; set; }
        [ForeignKey(nameof(FractureExtensionPresId))]
        [InverseProperty(nameof(StimJobFractureExtensionPress.StimJobFluidEfficiencyTests))]
        public virtual StimJobFractureExtensionPress FractureExtensionPres { get; set; }
        [ForeignKey(nameof(FractureLengthId))]
        [InverseProperty(nameof(StimJobFractureLengths.StimJobFluidEfficiencyTests))]
        public virtual StimJobFractureLengths FractureLength { get; set; }
        [ForeignKey(nameof(FractureWidthId))]
        [InverseProperty(nameof(StimJobFractureWidths.StimJobFluidEfficiencyTests))]
        public virtual StimJobFractureWidths FractureWidth { get; set; }
        [ForeignKey(nameof(NetPresId))]
        [InverseProperty(nameof(StimJobNetPress.StimJobFluidEfficiencyTests))]
        public virtual StimJobNetPress NetPres { get; set; }
        [ForeignKey(nameof(PorePresId))]
        [InverseProperty(nameof(StimJobPorePress.StimJobFluidEfficiencyTests))]
        public virtual StimJobPorePress PorePres { get; set; }
        [ForeignKey(nameof(PseudoRadialPresId))]
        [InverseProperty(nameof(StimJobPseudoRadialPress.StimJobFluidEfficiencyTests))]
        public virtual StimJobPseudoRadialPress PseudoRadialPres { get; set; }
        [ForeignKey(nameof(ResidualPermeabilityId))]
        [InverseProperty(nameof(StimJobResidualPermeabilitys.StimJobFluidEfficiencyTests))]
        public virtual StimJobResidualPermeabilitys ResidualPermeability { get; set; }
        [InverseProperty("FluidEfficiencyTest")]
        public virtual ICollection<StimJobPdatSessions> StimJobPdatSessions { get; set; }
    }
}
