using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportHses
    {
        public OpsReportHses()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public int HseId { get; set; }
        public int? DaysIncFreeId { get; set; }
        public int? IncidentId { get; set; }
        public string LastCsgPresTest { get; set; }
        public int? PresLastCsgId { get; set; }
        public string LastBopPresTest { get; set; }
        public string NextBopPresTest { get; set; }
        public int? PresStdPipeId { get; set; }
        public int? PresKellyHoseId { get; set; }
        public int? PresDiverterId { get; set; }
        public int? PresAnnularId { get; set; }
        public int? PresRamsId { get; set; }
        public int? PresChokeLineId { get; set; }
        public int? PresChokeManId { get; set; }
        public string LastFireBoatDrill { get; set; }
        public string LastAbandonDrill { get; set; }
        public string LastRigInspection { get; set; }
        public string LastSafetyMeeting { get; set; }
        public string LastSafetyInspection { get; set; }
        public string LastTripDrill { get; set; }
        public string LastDiverterDrill { get; set; }
        public string LastBopDrill { get; set; }
        public string RegAgencyInsp { get; set; }
        public string NonComplianceIssued { get; set; }
        public string NumStopCards { get; set; }
        public int? FluidDischargedId { get; set; }
        public int? VolCtgDischargedId { get; set; }
        public int? VolOilCtgDischargeId { get; set; }
        public int? WasteDischargedId { get; set; }
        public string Comments { get; set; }

        [ForeignKey(nameof(DaysIncFreeId))]
        [InverseProperty(nameof(OpsReportDaysIncFrees.OpsReportHses))]
        public virtual OpsReportDaysIncFrees DaysIncFree { get; set; }
        [ForeignKey(nameof(FluidDischargedId))]
        [InverseProperty(nameof(OpsReportFluidDischargeds.OpsReportHses))]
        public virtual OpsReportFluidDischargeds FluidDischarged { get; set; }
        [ForeignKey(nameof(IncidentId))]
        [InverseProperty(nameof(OpsReportIncidents.OpsReportHses))]
        public virtual OpsReportIncidents Incident { get; set; }
        [ForeignKey(nameof(PresAnnularId))]
        [InverseProperty(nameof(OpsReportPresAnnulars.OpsReportHses))]
        public virtual OpsReportPresAnnulars PresAnnular { get; set; }
        [ForeignKey(nameof(PresChokeLineId))]
        [InverseProperty(nameof(OpsReportPresChokeLines.OpsReportHses))]
        public virtual OpsReportPresChokeLines PresChokeLine { get; set; }
        [ForeignKey(nameof(PresChokeManId))]
        [InverseProperty(nameof(OpsReportPresChokeMans.OpsReportHses))]
        public virtual OpsReportPresChokeMans PresChokeMan { get; set; }
        [ForeignKey(nameof(PresDiverterId))]
        [InverseProperty(nameof(OpsReportPresDiverters.OpsReportHses))]
        public virtual OpsReportPresDiverters PresDiverter { get; set; }
        [ForeignKey(nameof(PresKellyHoseId))]
        [InverseProperty(nameof(OpsReportPresKellyHoses.OpsReportHses))]
        public virtual OpsReportPresKellyHoses PresKellyHose { get; set; }
        [ForeignKey(nameof(PresLastCsgId))]
        [InverseProperty(nameof(OpsReportPresLastCsgs.OpsReportHses))]
        public virtual OpsReportPresLastCsgs PresLastCsg { get; set; }
        [ForeignKey(nameof(PresRamsId))]
        [InverseProperty(nameof(OpsReportPresRamss.OpsReportHses))]
        public virtual OpsReportPresRamss PresRams { get; set; }
        [ForeignKey(nameof(PresStdPipeId))]
        [InverseProperty(nameof(OpsReportPresStdPipes.OpsReportHses))]
        public virtual OpsReportPresStdPipes PresStdPipe { get; set; }
        [ForeignKey(nameof(VolCtgDischargedId))]
        [InverseProperty(nameof(OpsReportVolCtgDischargeds.OpsReportHses))]
        public virtual OpsReportVolCtgDischargeds VolCtgDischarged { get; set; }
        [ForeignKey(nameof(VolOilCtgDischargeId))]
        [InverseProperty(nameof(OpsReportVolOilCtgDischarges.OpsReportHses))]
        public virtual OpsReportVolOilCtgDischarges VolOilCtgDischarge { get; set; }
        [ForeignKey(nameof(WasteDischargedId))]
        [InverseProperty(nameof(OpsReportWasteDischargeds.OpsReportHses))]
        public virtual OpsReportWasteDischargeds WasteDischarged { get; set; }
        [InverseProperty("Hse")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
