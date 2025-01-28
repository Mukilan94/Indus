using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportDrillingParams
    {
        public OpsReportDrillingParams()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public string Uid { get; set; }
        [Column("ETimOpBitId")]
        public int? EtimOpBitId { get; set; }
        public int? MdHoleStartId { get; set; }
        public int? MdHoleStopId { get; set; }
        public string TubularUidRef { get; set; }
        public int? HkldRotId { get; set; }
        public int? OverPullId { get; set; }
        public int? SlackOffId { get; set; }
        public int? HkldUpId { get; set; }
        public int? HkldDnId { get; set; }
        public int? TqOnBotAvId { get; set; }
        public int? TqOnBotMxId { get; set; }
        public int? TqOnBotMnId { get; set; }
        public int? TqOffBotAvId { get; set; }
        public int? TqDhAvId { get; set; }
        public int? WtAboveJarId { get; set; }
        public int? WtBelowJarId { get; set; }
        public string WtMudUom { get; set; }
        public int? FlowratePumpId { get; set; }
        public int? PowBitId { get; set; }
        public int? VelNozzleAvId { get; set; }
        public int? PresDropBitId { get; set; }
        [Column("CTimHoldId")]
        public int? CtimHoldId { get; set; }
        [Column("CTimSteeringId")]
        public int? CtimSteeringId { get; set; }
        [Column("CTimDrillRotId")]
        public int? CtimDrillRotId { get; set; }
        [Column("CTimDrillSlidId")]
        public int? CtimDrillSlidId { get; set; }
        [Column("CTimCircId")]
        public int? CtimCircId { get; set; }
        [Column("CTimReamId")]
        public int? CtimReamId { get; set; }
        public int? DistDrillRotId { get; set; }
        public int? DistDrillSlidId { get; set; }
        public int? DistReamId { get; set; }
        public int? DistHoldId { get; set; }
        public int? DistSteeringId { get; set; }
        public int? RpmAvId { get; set; }
        public int? RpmMxId { get; set; }
        public int? RpmMnId { get; set; }
        public int? RpmAvDhId { get; set; }
        public int? RopAvId { get; set; }
        public int? RopMxId { get; set; }
        public int? RopMnId { get; set; }
        public int? WobAvId { get; set; }
        public int? WobMxId { get; set; }
        public int? WobMnId { get; set; }
        public int? WobAvDhId { get; set; }
        public string ReasonTrip { get; set; }
        public string ObjectiveBha { get; set; }
        public int? AziTopId { get; set; }
        public int? AziBottomId { get; set; }
        public int? InclStartId { get; set; }
        public int? InclMxId { get; set; }
        public int? InclMnId { get; set; }
        public int? InclStopId { get; set; }
        public int? TempMudDhMxId { get; set; }
        public int? PresPumpAvId { get; set; }
        public int? FlowrateBitId { get; set; }
        public string Comments { get; set; }

        [ForeignKey(nameof(AziBottomId))]
        [InverseProperty(nameof(OpsReportAziBottoms.OpsReportDrillingParams))]
        public virtual OpsReportAziBottoms AziBottom { get; set; }
        [ForeignKey(nameof(AziTopId))]
        [InverseProperty(nameof(OpsReportAziTops.OpsReportDrillingParams))]
        public virtual OpsReportAziTops AziTop { get; set; }
        [ForeignKey(nameof(CtimCircId))]
        [InverseProperty(nameof(OpsReportCtimCircs.OpsReportDrillingParams))]
        public virtual OpsReportCtimCircs CtimCirc { get; set; }
        [ForeignKey(nameof(CtimDrillRotId))]
        [InverseProperty(nameof(OpsReportCtimDrillRots.OpsReportDrillingParams))]
        public virtual OpsReportCtimDrillRots CtimDrillRot { get; set; }
        [ForeignKey(nameof(CtimDrillSlidId))]
        [InverseProperty(nameof(OpsReportCtimDrillSlids.OpsReportDrillingParams))]
        public virtual OpsReportCtimDrillSlids CtimDrillSlid { get; set; }
        [ForeignKey(nameof(CtimHoldId))]
        [InverseProperty(nameof(OpsReportCtimHolds.OpsReportDrillingParams))]
        public virtual OpsReportCtimHolds CtimHold { get; set; }
        [ForeignKey(nameof(CtimReamId))]
        [InverseProperty(nameof(OpsReportCtimReams.OpsReportDrillingParams))]
        public virtual OpsReportCtimReams CtimReam { get; set; }
        [ForeignKey(nameof(CtimSteeringId))]
        [InverseProperty(nameof(OpsReportCtimSteerings.OpsReportDrillingParams))]
        public virtual OpsReportCtimSteerings CtimSteering { get; set; }
        [ForeignKey(nameof(DistDrillRotId))]
        [InverseProperty(nameof(OpsReportDistDrillRots.OpsReportDrillingParams))]
        public virtual OpsReportDistDrillRots DistDrillRot { get; set; }
        [ForeignKey(nameof(DistDrillSlidId))]
        [InverseProperty(nameof(OpsReportDistDrillSlids.OpsReportDrillingParams))]
        public virtual OpsReportDistDrillSlids DistDrillSlid { get; set; }
        [ForeignKey(nameof(DistHoldId))]
        [InverseProperty(nameof(OpsReportDistHolds.OpsReportDrillingParams))]
        public virtual OpsReportDistHolds DistHold { get; set; }
        [ForeignKey(nameof(DistReamId))]
        [InverseProperty(nameof(OpsReportDistReams.OpsReportDrillingParams))]
        public virtual OpsReportDistReams DistReam { get; set; }
        [ForeignKey(nameof(DistSteeringId))]
        [InverseProperty(nameof(OpsReportDistSteerings.OpsReportDrillingParams))]
        public virtual OpsReportDistSteerings DistSteering { get; set; }
        [ForeignKey(nameof(EtimOpBitId))]
        [InverseProperty(nameof(OpsReportEtimOpBits.OpsReportDrillingParams))]
        public virtual OpsReportEtimOpBits EtimOpBit { get; set; }
        [ForeignKey(nameof(FlowrateBitId))]
        [InverseProperty(nameof(OpsReportFlowrateBits.OpsReportDrillingParams))]
        public virtual OpsReportFlowrateBits FlowrateBit { get; set; }
        [ForeignKey(nameof(FlowratePumpId))]
        [InverseProperty(nameof(OpsReportFlowratePumps.OpsReportDrillingParams))]
        public virtual OpsReportFlowratePumps FlowratePump { get; set; }
        [ForeignKey(nameof(HkldDnId))]
        [InverseProperty(nameof(OpsReportHkldDns.OpsReportDrillingParams))]
        public virtual OpsReportHkldDns HkldDn { get; set; }
        [ForeignKey(nameof(HkldRotId))]
        [InverseProperty(nameof(OpsReportHkldRots.OpsReportDrillingParams))]
        public virtual OpsReportHkldRots HkldRot { get; set; }
        [ForeignKey(nameof(HkldUpId))]
        [InverseProperty(nameof(OpsReportHkldUps.OpsReportDrillingParams))]
        public virtual OpsReportHkldUps HkldUp { get; set; }
        [ForeignKey(nameof(InclMnId))]
        [InverseProperty(nameof(OpsReportInclMns.OpsReportDrillingParams))]
        public virtual OpsReportInclMns InclMn { get; set; }
        [ForeignKey(nameof(InclMxId))]
        [InverseProperty(nameof(OpsReportInclMxs.OpsReportDrillingParams))]
        public virtual OpsReportInclMxs InclMx { get; set; }
        [ForeignKey(nameof(InclStartId))]
        [InverseProperty(nameof(OpsReportInclStarts.OpsReportDrillingParams))]
        public virtual OpsReportInclStarts InclStart { get; set; }
        [ForeignKey(nameof(InclStopId))]
        [InverseProperty(nameof(OpsReportInclStops.OpsReportDrillingParams))]
        public virtual OpsReportInclStops InclStop { get; set; }
        [ForeignKey(nameof(MdHoleStartId))]
        [InverseProperty(nameof(OpsReportMdHoleStarts.OpsReportDrillingParams))]
        public virtual OpsReportMdHoleStarts MdHoleStart { get; set; }
        [ForeignKey(nameof(MdHoleStopId))]
        [InverseProperty(nameof(OpsReportMdHoleStops.OpsReportDrillingParams))]
        public virtual OpsReportMdHoleStops MdHoleStop { get; set; }
        [ForeignKey(nameof(OverPullId))]
        [InverseProperty(nameof(OpsReportOverPulls.OpsReportDrillingParams))]
        public virtual OpsReportOverPulls OverPull { get; set; }
        [ForeignKey(nameof(PowBitId))]
        [InverseProperty(nameof(OpsReportPowBits.OpsReportDrillingParams))]
        public virtual OpsReportPowBits PowBit { get; set; }
        [ForeignKey(nameof(PresDropBitId))]
        [InverseProperty(nameof(OpsReportPresDropBits.OpsReportDrillingParams))]
        public virtual OpsReportPresDropBits PresDropBit { get; set; }
        [ForeignKey(nameof(PresPumpAvId))]
        [InverseProperty(nameof(OpsReportPresPumpAvs.OpsReportDrillingParams))]
        public virtual OpsReportPresPumpAvs PresPumpAv { get; set; }
        [ForeignKey(nameof(RopAvId))]
        [InverseProperty(nameof(OpsReportRopAvs.OpsReportDrillingParams))]
        public virtual OpsReportRopAvs RopAv { get; set; }
        [ForeignKey(nameof(RopMnId))]
        [InverseProperty(nameof(OpsReportRopMns.OpsReportDrillingParams))]
        public virtual OpsReportRopMns RopMn { get; set; }
        [ForeignKey(nameof(RopMxId))]
        [InverseProperty(nameof(OpsReportRopMxs.OpsReportDrillingParams))]
        public virtual OpsReportRopMxs RopMx { get; set; }
        [ForeignKey(nameof(RpmAvId))]
        [InverseProperty(nameof(OpsReportRpmAvs.OpsReportDrillingParams))]
        public virtual OpsReportRpmAvs RpmAv { get; set; }
        [ForeignKey(nameof(RpmAvDhId))]
        [InverseProperty(nameof(OpsReportRpmAvDhs.OpsReportDrillingParams))]
        public virtual OpsReportRpmAvDhs RpmAvDh { get; set; }
        [ForeignKey(nameof(RpmMnId))]
        [InverseProperty(nameof(OpsReportRpmMns.OpsReportDrillingParams))]
        public virtual OpsReportRpmMns RpmMn { get; set; }
        [ForeignKey(nameof(RpmMxId))]
        [InverseProperty(nameof(OpsReportRpmMxs.OpsReportDrillingParams))]
        public virtual OpsReportRpmMxs RpmMx { get; set; }
        [ForeignKey(nameof(SlackOffId))]
        [InverseProperty(nameof(OpsReportSlackOffs.OpsReportDrillingParams))]
        public virtual OpsReportSlackOffs SlackOff { get; set; }
        [ForeignKey(nameof(TempMudDhMxId))]
        [InverseProperty(nameof(OpsReportTempMudDhMxs.OpsReportDrillingParams))]
        public virtual OpsReportTempMudDhMxs TempMudDhMx { get; set; }
        [ForeignKey(nameof(TqDhAvId))]
        [InverseProperty(nameof(OpsReportTqDhAvs.OpsReportDrillingParams))]
        public virtual OpsReportTqDhAvs TqDhAv { get; set; }
        [ForeignKey(nameof(TqOffBotAvId))]
        [InverseProperty(nameof(OpsReportTqOffBotAvs.OpsReportDrillingParams))]
        public virtual OpsReportTqOffBotAvs TqOffBotAv { get; set; }
        [ForeignKey(nameof(TqOnBotAvId))]
        [InverseProperty(nameof(OpsReportTqOnBotAvs.OpsReportDrillingParams))]
        public virtual OpsReportTqOnBotAvs TqOnBotAv { get; set; }
        [ForeignKey(nameof(TqOnBotMnId))]
        [InverseProperty(nameof(OpsReportTqOnBotMns.OpsReportDrillingParams))]
        public virtual OpsReportTqOnBotMns TqOnBotMn { get; set; }
        [ForeignKey(nameof(TqOnBotMxId))]
        [InverseProperty(nameof(OpsReportTqOnBotMxs.OpsReportDrillingParams))]
        public virtual OpsReportTqOnBotMxs TqOnBotMx { get; set; }
        [ForeignKey(nameof(TubularUidRef))]
        [InverseProperty(nameof(OpsReportTubulars.OpsReportDrillingParams))]
        public virtual OpsReportTubulars TubularUidRefNavigation { get; set; }
        [ForeignKey(nameof(VelNozzleAvId))]
        [InverseProperty(nameof(OpsReportVelNozzleAvs.OpsReportDrillingParams))]
        public virtual OpsReportVelNozzleAvs VelNozzleAv { get; set; }
        [ForeignKey(nameof(WobAvId))]
        [InverseProperty(nameof(OpsReportWobAvs.OpsReportDrillingParams))]
        public virtual OpsReportWobAvs WobAv { get; set; }
        [ForeignKey(nameof(WobAvDhId))]
        [InverseProperty(nameof(OpsReportWobAvDhs.OpsReportDrillingParams))]
        public virtual OpsReportWobAvDhs WobAvDh { get; set; }
        [ForeignKey(nameof(WobMnId))]
        [InverseProperty(nameof(OpsReportWobMns.OpsReportDrillingParams))]
        public virtual OpsReportWobMns WobMn { get; set; }
        [ForeignKey(nameof(WobMxId))]
        [InverseProperty(nameof(OpsReportWobMxs.OpsReportDrillingParams))]
        public virtual OpsReportWobMxs WobMx { get; set; }
        [ForeignKey(nameof(WtAboveJarId))]
        [InverseProperty(nameof(OpsReportWtAboveJars.OpsReportDrillingParams))]
        public virtual OpsReportWtAboveJars WtAboveJar { get; set; }
        [ForeignKey(nameof(WtBelowJarId))]
        [InverseProperty(nameof(OpsReportWtBelowJars.OpsReportDrillingParams))]
        public virtual OpsReportWtBelowJars WtBelowJar { get; set; }
        [ForeignKey(nameof(WtMudUom))]
        [InverseProperty(nameof(OpsReportWtMuds.OpsReportDrillingParams))]
        public virtual OpsReportWtMuds WtMudUomNavigation { get; set; }
        [InverseProperty("DrillingParamsU")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
