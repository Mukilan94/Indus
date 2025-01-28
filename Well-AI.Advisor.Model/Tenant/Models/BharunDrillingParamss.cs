using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class BharunDrillingParamss
    {
        public BharunDrillingParamss()
        {
            Bharuns = new HashSet<Bharuns>();
        }

        [Key]
        public string Uid { get; set; }
        [Column("ETimOpBitUom")]
        public string EtimOpBitUom { get; set; }
        public string MdHoleStartUom { get; set; }
        public string MdHoleStopUom { get; set; }
        public string TubularUidRef { get; set; }
        public string HkldRotUom { get; set; }
        public string OverPullUom { get; set; }
        public string SlackOffUom { get; set; }
        public string HkldUpUom { get; set; }
        public string HkldDnUom { get; set; }
        public string TqOnBotAvUom { get; set; }
        public string TqOnBotMxUom { get; set; }
        public string TqOnBotMnUom { get; set; }
        public string TqOffBotAvUom { get; set; }
        public string TqDhAvUom { get; set; }
        public string WtAboveJarUom { get; set; }
        public string WtBelowJarUom { get; set; }
        public string WtMudUom { get; set; }
        public string FlowratePumpUom { get; set; }
        public string PowBitUom { get; set; }
        public string VelNozzleAvUom { get; set; }
        public string PresDropBitUom { get; set; }
        [Column("CTimHoldUom")]
        public string CtimHoldUom { get; set; }
        [Column("CTimSteeringUom")]
        public string CtimSteeringUom { get; set; }
        [Column("CTimDrillRotUom")]
        public string CtimDrillRotUom { get; set; }
        [Column("CTimDrillSlidUom")]
        public string CtimDrillSlidUom { get; set; }
        [Column("CTimCircUom")]
        public string CtimCircUom { get; set; }
        [Column("CTimReamUom")]
        public string CtimReamUom { get; set; }
        public string DistDrillRotUom { get; set; }
        public string DistDrillSlidUom { get; set; }
        public string DistReamUom { get; set; }
        public string DistHoldUom { get; set; }
        public string DistSteeringUom { get; set; }
        public int? RpmAvBharunRpmAvId { get; set; }
        public string RpmMxUom { get; set; }
        public string RpmMnUom { get; set; }
        public string RpmAvDhUom { get; set; }
        public int? RopAvBharunRopAvId { get; set; }
        public int? RopMxBharunRopMxId { get; set; }
        public int? RopMnBharunRopMnId { get; set; }
        public int? WobAvBharunWobAvId { get; set; }
        public string WobMxUom { get; set; }
        public string WobMnUom { get; set; }
        public string WobAvDhUom { get; set; }
        public string ReasonTrip { get; set; }
        public string ObjectiveBha { get; set; }
        public string AziTopUom { get; set; }
        public string AziBottomUom { get; set; }
        public string InclStartUom { get; set; }
        public string InclMxUom { get; set; }
        public string InclMnUom { get; set; }
        public string InclStopUom { get; set; }
        public string TempMudDhMxUom { get; set; }
        public string PresPumpAvUom { get; set; }
        public string FlowrateBitUom { get; set; }
        public string Comments { get; set; }

        [ForeignKey(nameof(AziBottomUom))]
        [InverseProperty(nameof(BharunAziBottoms.BharunDrillingParamss))]
        public virtual BharunAziBottoms AziBottomUomNavigation { get; set; }
        [ForeignKey(nameof(AziTopUom))]
        [InverseProperty(nameof(BharunAziTops.BharunDrillingParamss))]
        public virtual BharunAziTops AziTopUomNavigation { get; set; }
        [ForeignKey(nameof(CtimCircUom))]
        [InverseProperty(nameof(BharunCtimCircs.BharunDrillingParamss))]
        public virtual BharunCtimCircs CtimCircUomNavigation { get; set; }
        [ForeignKey(nameof(CtimDrillRotUom))]
        [InverseProperty(nameof(BharunCtimDrillRots.BharunDrillingParamss))]
        public virtual BharunCtimDrillRots CtimDrillRotUomNavigation { get; set; }
        [ForeignKey(nameof(CtimDrillSlidUom))]
        [InverseProperty(nameof(BharunCtimDrillSlids.BharunDrillingParamss))]
        public virtual BharunCtimDrillSlids CtimDrillSlidUomNavigation { get; set; }
        [ForeignKey(nameof(CtimHoldUom))]
        [InverseProperty(nameof(BharunCtimHolds.BharunDrillingParamss))]
        public virtual BharunCtimHolds CtimHoldUomNavigation { get; set; }
        [ForeignKey(nameof(CtimReamUom))]
        [InverseProperty(nameof(BharunCtimReams.BharunDrillingParamss))]
        public virtual BharunCtimReams CtimReamUomNavigation { get; set; }
        [ForeignKey(nameof(CtimSteeringUom))]
        [InverseProperty(nameof(BharunCtimSteerings.BharunDrillingParamss))]
        public virtual BharunCtimSteerings CtimSteeringUomNavigation { get; set; }
        [ForeignKey(nameof(DistDrillRotUom))]
        [InverseProperty(nameof(BharunDistDrillRots.BharunDrillingParamss))]
        public virtual BharunDistDrillRots DistDrillRotUomNavigation { get; set; }
        [ForeignKey(nameof(DistDrillSlidUom))]
        [InverseProperty(nameof(BharunDistDrillSlids.BharunDrillingParamss))]
        public virtual BharunDistDrillSlids DistDrillSlidUomNavigation { get; set; }
        [ForeignKey(nameof(DistHoldUom))]
        [InverseProperty(nameof(BharunDistHolds.BharunDrillingParamss))]
        public virtual BharunDistHolds DistHoldUomNavigation { get; set; }
        [ForeignKey(nameof(DistReamUom))]
        [InverseProperty(nameof(BharunDistReams.BharunDrillingParamss))]
        public virtual BharunDistReams DistReamUomNavigation { get; set; }
        [ForeignKey(nameof(DistSteeringUom))]
        [InverseProperty(nameof(BharunDistSteerings.BharunDrillingParamss))]
        public virtual BharunDistSteerings DistSteeringUomNavigation { get; set; }
        [ForeignKey(nameof(EtimOpBitUom))]
        [InverseProperty(nameof(BharunEtimOpBits.BharunDrillingParamss))]
        public virtual BharunEtimOpBits EtimOpBitUomNavigation { get; set; }
        [ForeignKey(nameof(FlowrateBitUom))]
        [InverseProperty(nameof(BharunFlowrateBits.BharunDrillingParamss))]
        public virtual BharunFlowrateBits FlowrateBitUomNavigation { get; set; }
        [ForeignKey(nameof(FlowratePumpUom))]
        [InverseProperty(nameof(BharunFlowratePumps.BharunDrillingParamss))]
        public virtual BharunFlowratePumps FlowratePumpUomNavigation { get; set; }
        [ForeignKey(nameof(HkldDnUom))]
        [InverseProperty(nameof(BharunHkldDns.BharunDrillingParamss))]
        public virtual BharunHkldDns HkldDnUomNavigation { get; set; }
        [ForeignKey(nameof(HkldRotUom))]
        [InverseProperty(nameof(BharunHkldRots.BharunDrillingParamss))]
        public virtual BharunHkldRots HkldRotUomNavigation { get; set; }
        [ForeignKey(nameof(HkldUpUom))]
        [InverseProperty(nameof(BharunHkldUps.BharunDrillingParamss))]
        public virtual BharunHkldUps HkldUpUomNavigation { get; set; }
        [ForeignKey(nameof(InclMnUom))]
        [InverseProperty(nameof(BharunInclMns.BharunDrillingParamss))]
        public virtual BharunInclMns InclMnUomNavigation { get; set; }
        [ForeignKey(nameof(InclMxUom))]
        [InverseProperty(nameof(BharunInclMxs.BharunDrillingParamss))]
        public virtual BharunInclMxs InclMxUomNavigation { get; set; }
        [ForeignKey(nameof(InclStartUom))]
        [InverseProperty(nameof(BharunInclStarts.BharunDrillingParamss))]
        public virtual BharunInclStarts InclStartUomNavigation { get; set; }
        [ForeignKey(nameof(InclStopUom))]
        [InverseProperty(nameof(BharunInclStops.BharunDrillingParamss))]
        public virtual BharunInclStops InclStopUomNavigation { get; set; }
        [ForeignKey(nameof(MdHoleStartUom))]
        [InverseProperty(nameof(BharunMdHoleStarts.BharunDrillingParamss))]
        public virtual BharunMdHoleStarts MdHoleStartUomNavigation { get; set; }
        [ForeignKey(nameof(MdHoleStopUom))]
        [InverseProperty(nameof(BharunMdHoleStops.BharunDrillingParamss))]
        public virtual BharunMdHoleStops MdHoleStopUomNavigation { get; set; }
        [ForeignKey(nameof(OverPullUom))]
        [InverseProperty(nameof(BharunOverPulls.BharunDrillingParamss))]
        public virtual BharunOverPulls OverPullUomNavigation { get; set; }
        [ForeignKey(nameof(PowBitUom))]
        [InverseProperty(nameof(BharunPowBits.BharunDrillingParamss))]
        public virtual BharunPowBits PowBitUomNavigation { get; set; }
        [ForeignKey(nameof(PresDropBitUom))]
        [InverseProperty(nameof(BharunPresDropBits.BharunDrillingParamss))]
        public virtual BharunPresDropBits PresDropBitUomNavigation { get; set; }
        [ForeignKey(nameof(PresPumpAvUom))]
        [InverseProperty(nameof(BharunPresPumpAvs.BharunDrillingParamss))]
        public virtual BharunPresPumpAvs PresPumpAvUomNavigation { get; set; }
        [ForeignKey(nameof(RopAvBharunRopAvId))]
        [InverseProperty(nameof(BharunRopAvs.BharunDrillingParamss))]
        public virtual BharunRopAvs RopAvBharunRopAv { get; set; }
        [ForeignKey(nameof(RopMnBharunRopMnId))]
        [InverseProperty(nameof(BharunRopMns.BharunDrillingParamss))]
        public virtual BharunRopMns RopMnBharunRopMn { get; set; }
        [ForeignKey(nameof(RopMxBharunRopMxId))]
        [InverseProperty(nameof(BharunRopMxs.BharunDrillingParamss))]
        public virtual BharunRopMxs RopMxBharunRopMx { get; set; }
        [ForeignKey(nameof(RpmAvBharunRpmAvId))]
        [InverseProperty(nameof(BharunRpmAvs.BharunDrillingParamss))]
        public virtual BharunRpmAvs RpmAvBharunRpmAv { get; set; }
        [ForeignKey(nameof(RpmAvDhUom))]
        [InverseProperty(nameof(BharunRpmAvDhs.BharunDrillingParamss))]
        public virtual BharunRpmAvDhs RpmAvDhUomNavigation { get; set; }
        [ForeignKey(nameof(RpmMnUom))]
        [InverseProperty(nameof(BharunRpmMns.BharunDrillingParamss))]
        public virtual BharunRpmMns RpmMnUomNavigation { get; set; }
        [ForeignKey(nameof(RpmMxUom))]
        [InverseProperty(nameof(BharunRpmMxs.BharunDrillingParamss))]
        public virtual BharunRpmMxs RpmMxUomNavigation { get; set; }
        [ForeignKey(nameof(SlackOffUom))]
        [InverseProperty(nameof(BharunSlackOff.BharunDrillingParamss))]
        public virtual BharunSlackOff SlackOffUomNavigation { get; set; }
        [ForeignKey(nameof(TempMudDhMxUom))]
        [InverseProperty(nameof(BharunTempMudDhMxs.BharunDrillingParamss))]
        public virtual BharunTempMudDhMxs TempMudDhMxUomNavigation { get; set; }
        [ForeignKey(nameof(TqDhAvUom))]
        [InverseProperty(nameof(BharunTqDhAvs.BharunDrillingParamss))]
        public virtual BharunTqDhAvs TqDhAvUomNavigation { get; set; }
        [ForeignKey(nameof(TqOffBotAvUom))]
        [InverseProperty(nameof(BharunTqOffBotAvs.BharunDrillingParamss))]
        public virtual BharunTqOffBotAvs TqOffBotAvUomNavigation { get; set; }
        [ForeignKey(nameof(TqOnBotAvUom))]
        [InverseProperty(nameof(BharunTqOnBotAvs.BharunDrillingParamss))]
        public virtual BharunTqOnBotAvs TqOnBotAvUomNavigation { get; set; }
        [ForeignKey(nameof(TqOnBotMnUom))]
        [InverseProperty(nameof(BharunTqOnBotMns.BharunDrillingParamss))]
        public virtual BharunTqOnBotMns TqOnBotMnUomNavigation { get; set; }
        [ForeignKey(nameof(TqOnBotMxUom))]
        [InverseProperty(nameof(BharunTqOnBotMxs.BharunDrillingParamss))]
        public virtual BharunTqOnBotMxs TqOnBotMxUomNavigation { get; set; }
        [ForeignKey(nameof(TubularUidRef))]
        [InverseProperty(nameof(BharunTubulars.BharunDrillingParamss))]
        public virtual BharunTubulars TubularUidRefNavigation { get; set; }
        [ForeignKey(nameof(VelNozzleAvUom))]
        [InverseProperty(nameof(BharunVelNozzleAvs.BharunDrillingParamss))]
        public virtual BharunVelNozzleAvs VelNozzleAvUomNavigation { get; set; }
        [ForeignKey(nameof(WobAvBharunWobAvId))]
        [InverseProperty(nameof(BharunWobAvs.BharunDrillingParamss))]
        public virtual BharunWobAvs WobAvBharunWobAv { get; set; }
        [ForeignKey(nameof(WobAvDhUom))]
        [InverseProperty(nameof(BharunWobAvDhs.BharunDrillingParamss))]
        public virtual BharunWobAvDhs WobAvDhUomNavigation { get; set; }
        [ForeignKey(nameof(WobMnUom))]
        [InverseProperty(nameof(BharunWobMns.BharunDrillingParamss))]
        public virtual BharunWobMns WobMnUomNavigation { get; set; }
        [ForeignKey(nameof(WobMxUom))]
        [InverseProperty(nameof(BharunWobMxs.BharunDrillingParamss))]
        public virtual BharunWobMxs WobMxUomNavigation { get; set; }
        [ForeignKey(nameof(WtAboveJarUom))]
        [InverseProperty(nameof(BharunWtAboveJars.BharunDrillingParamss))]
        public virtual BharunWtAboveJars WtAboveJarUomNavigation { get; set; }
        [ForeignKey(nameof(WtBelowJarUom))]
        [InverseProperty(nameof(BharunWtBelowJars.BharunDrillingParamss))]
        public virtual BharunWtBelowJars WtBelowJarUomNavigation { get; set; }
        [ForeignKey(nameof(WtMudUom))]
        [InverseProperty(nameof(BharunWtMuds.BharunDrillingParamss))]
        public virtual BharunWtMuds WtMudUomNavigation { get; set; }
        [InverseProperty("DrillingParamsU")]
        public virtual ICollection<Bharuns> Bharuns { get; set; }
    }
}
