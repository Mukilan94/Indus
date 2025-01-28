using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReports
    {
        public OpsReports()
        {
            OpsReportDayCosts = new HashSet<OpsReportDayCosts>();
            OpsReportPersonnels = new HashSet<OpsReportPersonnels>();
            OpsReportPitVolumes = new HashSet<OpsReportPitVolumes>();
            OpsReportScrs = new HashSet<OpsReportScrs>();
        }

        [Key]
        public int OpsReportId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public int? RigOpsReportRigId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        [Column("ETimStartId")]
        public int? EtimStartId { get; set; }
        [Column("ETimSpudId")]
        public int? EtimSpudId { get; set; }
        [Column("ETimLocId")]
        public int? EtimLocId { get; set; }
        public int? MdReportId { get; set; }
        public int? TvdReportId { get; set; }
        public int? DistDrillId { get; set; }
        [Column("ETimDrillId")]
        public int? EtimDrillId { get; set; }
        public int? MdPlannedId { get; set; }
        public int? RopAvId { get; set; }
        public int? RopCurrentId { get; set; }
        public string Supervisor { get; set; }
        public string Engineer { get; set; }
        public string Geologist { get; set; }
        [Column("ETimDrillRotId")]
        public int? EtimDrillRotId { get; set; }
        [Column("ETimDrillSlidId")]
        public int? EtimDrillSlidId { get; set; }
        [Column("ETimCircId")]
        public int? EtimCircId { get; set; }
        [Column("ETimReamId")]
        public int? EtimReamId { get; set; }
        [Column("ETimHoldId")]
        public int? EtimHoldId { get; set; }
        [Column("ETimSteeringId")]
        public int? EtimSteeringId { get; set; }
        public int? DistDrillRotId { get; set; }
        public int? DistDrillSlidId { get; set; }
        public int? DistReamId { get; set; }
        public int? DistHoldId { get; set; }
        public int? DistSteeringId { get; set; }
        public string NumPob { get; set; }
        public string NumContract { get; set; }
        public string NumOperator { get; set; }
        public string NumService { get; set; }
        public string ActivityUid { get; set; }
        public string DrillingParamsUid { get; set; }
        public string TrajectoryStationUid { get; set; }
        public string FluidUid { get; set; }
        public int? MudVolumeId { get; set; }
        public int? MudInventoryId { get; set; }
        public int? BulkId { get; set; }
        public int? RigResponseId { get; set; }
        public int? PumpOpId { get; set; }
        public int? ShakerOpId { get; set; }
        public int? HseId { get; set; }
        public int? SupportCraftId { get; set; }
        public int? WeatherId { get; set; }
        [Column("NumAFE")]
        public string NumAfe { get; set; }
        public int? CostDayId { get; set; }
        public int? CostDayMudId { get; set; }
        public int? DiaHoleId { get; set; }
        public string ConditionHole { get; set; }
        public string Lithology { get; set; }
        public string NameFormation { get; set; }
        public int? DiaCsgLastId { get; set; }
        public int? MdCsgLastId { get; set; }
        public int? TvdCsgLastId { get; set; }
        public int? TvdLotId { get; set; }
        public int? PresLotEmwId { get; set; }
        public int? PresKickTolId { get; set; }
        public int? VolKickTolId { get; set; }
        public int? MaaspId { get; set; }
        public int? TubularId { get; set; }
        public string Sum24Hr { get; set; }
        public string Forecast24Hr { get; set; }
        public string StatusCurrent { get; set; }
        public int? CommonDataOpsReportsCommonDataid { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(ActivityUid))]
        [InverseProperty(nameof(OpsReportActivitys.OpsReports))]
        public virtual OpsReportActivitys ActivityU { get; set; }
        [ForeignKey(nameof(BulkId))]
        [InverseProperty(nameof(OpsReportBulks.OpsReports))]
        public virtual OpsReportBulks Bulk { get; set; }
        [ForeignKey(nameof(CommonDataOpsReportsCommonDataid))]
        [InverseProperty(nameof(OpsReportsCommonDatas.OpsReports))]
        public virtual OpsReportsCommonDatas CommonDataOpsReportsCommonData { get; set; }
        [ForeignKey(nameof(CostDayId))]
        [InverseProperty(nameof(OpsReportCostDays.OpsReports))]
        public virtual OpsReportCostDays CostDay { get; set; }
        [ForeignKey(nameof(CostDayMudId))]
        [InverseProperty(nameof(OpsReportCostDayMuds.OpsReports))]
        public virtual OpsReportCostDayMuds CostDayMud { get; set; }
        [ForeignKey(nameof(DiaCsgLastId))]
        [InverseProperty(nameof(OpsReportDiaCsgLasts.OpsReports))]
        public virtual OpsReportDiaCsgLasts DiaCsgLast { get; set; }
        [ForeignKey(nameof(DiaHoleId))]
        [InverseProperty(nameof(OpsReportDiaHoles.OpsReports))]
        public virtual OpsReportDiaHoles DiaHole { get; set; }
        [ForeignKey(nameof(DistDrillId))]
        [InverseProperty(nameof(OpsReportDistDrills.OpsReports))]
        public virtual OpsReportDistDrills DistDrill { get; set; }
        [ForeignKey(nameof(DistDrillRotId))]
        [InverseProperty(nameof(OpsReportDistDrillRots.OpsReports))]
        public virtual OpsReportDistDrillRots DistDrillRot { get; set; }
        [ForeignKey(nameof(DistDrillSlidId))]
        [InverseProperty(nameof(OpsReportDistDrillSlids.OpsReports))]
        public virtual OpsReportDistDrillSlids DistDrillSlid { get; set; }
        [ForeignKey(nameof(DistHoldId))]
        [InverseProperty(nameof(OpsReportDistHolds.OpsReports))]
        public virtual OpsReportDistHolds DistHold { get; set; }
        [ForeignKey(nameof(DistReamId))]
        [InverseProperty(nameof(OpsReportDistReams.OpsReports))]
        public virtual OpsReportDistReams DistReam { get; set; }
        [ForeignKey(nameof(DistSteeringId))]
        [InverseProperty(nameof(OpsReportDistSteerings.OpsReports))]
        public virtual OpsReportDistSteerings DistSteering { get; set; }
        [ForeignKey(nameof(DrillingParamsUid))]
        [InverseProperty(nameof(OpsReportDrillingParams.OpsReports))]
        public virtual OpsReportDrillingParams DrillingParamsU { get; set; }
        [ForeignKey(nameof(EtimCircId))]
        [InverseProperty(nameof(OpsReportEtimCircs.OpsReports))]
        public virtual OpsReportEtimCircs EtimCirc { get; set; }
        [ForeignKey(nameof(EtimDrillId))]
        [InverseProperty(nameof(OpsReportEtimDrills.OpsReports))]
        public virtual OpsReportEtimDrills EtimDrill { get; set; }
        [ForeignKey(nameof(EtimDrillRotId))]
        [InverseProperty(nameof(OpsReportEtimDrillRots.OpsReports))]
        public virtual OpsReportEtimDrillRots EtimDrillRot { get; set; }
        [ForeignKey(nameof(EtimDrillSlidId))]
        [InverseProperty(nameof(OpsReportEtimDrillSlids.OpsReports))]
        public virtual OpsReportEtimDrillSlids EtimDrillSlid { get; set; }
        [ForeignKey(nameof(EtimHoldId))]
        [InverseProperty(nameof(OpsReportEtimHolds.OpsReports))]
        public virtual OpsReportEtimHolds EtimHold { get; set; }
        [ForeignKey(nameof(EtimLocId))]
        [InverseProperty(nameof(OpsReportEtimLocs.OpsReports))]
        public virtual OpsReportEtimLocs EtimLoc { get; set; }
        [ForeignKey(nameof(EtimReamId))]
        [InverseProperty(nameof(OpsReportEtimReams.OpsReports))]
        public virtual OpsReportEtimReams EtimReam { get; set; }
        [ForeignKey(nameof(EtimSpudId))]
        [InverseProperty(nameof(OpsReportEtimSpuds.OpsReports))]
        public virtual OpsReportEtimSpuds EtimSpud { get; set; }
        [ForeignKey(nameof(EtimStartId))]
        [InverseProperty(nameof(OpsReportEtimStarts.OpsReports))]
        public virtual OpsReportEtimStarts EtimStart { get; set; }
        [ForeignKey(nameof(EtimSteeringId))]
        [InverseProperty(nameof(OpsReportEtimSteerings.OpsReports))]
        public virtual OpsReportEtimSteerings EtimSteering { get; set; }
        [ForeignKey(nameof(FluidUid))]
        [InverseProperty(nameof(OpsReportFluids.OpsReports))]
        public virtual OpsReportFluids FluidU { get; set; }
        [ForeignKey(nameof(HseId))]
        [InverseProperty(nameof(OpsReportHses.OpsReports))]
        public virtual OpsReportHses Hse { get; set; }
        [ForeignKey(nameof(MaaspId))]
        [InverseProperty(nameof(OpsReportMaasps.OpsReports))]
        public virtual OpsReportMaasps Maasp { get; set; }
        [ForeignKey(nameof(MdCsgLastId))]
        [InverseProperty(nameof(OpsReportMdCsgLasts.OpsReports))]
        public virtual OpsReportMdCsgLasts MdCsgLast { get; set; }
        [ForeignKey(nameof(MdPlannedId))]
        [InverseProperty(nameof(OpsReportMdPlanneds.OpsReports))]
        public virtual OpsReportMdPlanneds MdPlanned { get; set; }
        [ForeignKey(nameof(MdReportId))]
        [InverseProperty(nameof(OpsReportMdReports.OpsReports))]
        public virtual OpsReportMdReports MdReport { get; set; }
        [ForeignKey(nameof(MudInventoryId))]
        [InverseProperty(nameof(OpsReportMudInventorys.OpsReports))]
        public virtual OpsReportMudInventorys MudInventory { get; set; }
        [ForeignKey(nameof(MudVolumeId))]
        [InverseProperty(nameof(OpsReportMudVolumes.OpsReports))]
        public virtual OpsReportMudVolumes MudVolume { get; set; }
        [ForeignKey(nameof(PresKickTolId))]
        [InverseProperty(nameof(OpsReportPresKickTols.OpsReports))]
        public virtual OpsReportPresKickTols PresKickTol { get; set; }
        [ForeignKey(nameof(PresLotEmwId))]
        [InverseProperty(nameof(OpsReportPresLotEmws.OpsReports))]
        public virtual OpsReportPresLotEmws PresLotEmw { get; set; }
        [ForeignKey(nameof(PumpOpId))]
        [InverseProperty(nameof(OpsReportPumpOps.OpsReports))]
        public virtual OpsReportPumpOps PumpOp { get; set; }
        [ForeignKey(nameof(RigOpsReportRigId))]
        [InverseProperty(nameof(OpsReportRigs.OpsReports))]
        public virtual OpsReportRigs RigOpsReportRig { get; set; }
        [ForeignKey(nameof(RigResponseId))]
        [InverseProperty(nameof(OpsReportRigResponses.OpsReports))]
        public virtual OpsReportRigResponses RigResponse { get; set; }
        [ForeignKey(nameof(RopAvId))]
        [InverseProperty(nameof(OpsReportRopAvs.OpsReports))]
        public virtual OpsReportRopAvs RopAv { get; set; }
        [ForeignKey(nameof(RopCurrentId))]
        [InverseProperty(nameof(OpsReportRopCurrents.OpsReports))]
        public virtual OpsReportRopCurrents RopCurrent { get; set; }
        [ForeignKey(nameof(ShakerOpId))]
        [InverseProperty(nameof(OpsReportShakerOps.OpsReports))]
        public virtual OpsReportShakerOps ShakerOp { get; set; }
        [ForeignKey(nameof(SupportCraftId))]
        [InverseProperty(nameof(OpsReportSupportCrafts.OpsReports))]
        public virtual OpsReportSupportCrafts SupportCraft { get; set; }
        [ForeignKey(nameof(TrajectoryStationUid))]
        [InverseProperty(nameof(OpsReportTrajectoryStations.OpsReports))]
        public virtual OpsReportTrajectoryStations TrajectoryStationU { get; set; }
        [ForeignKey(nameof(TubularId))]
        [InverseProperty(nameof(Tubulars.OpsReports))]
        public virtual Tubulars Tubular { get; set; }
        [ForeignKey(nameof(TvdCsgLastId))]
        [InverseProperty(nameof(OpsReportTvdCsgLasts.OpsReports))]
        public virtual OpsReportTvdCsgLasts TvdCsgLast { get; set; }
        [ForeignKey(nameof(TvdLotId))]
        [InverseProperty(nameof(OpsReportTvdLots.OpsReports))]
        public virtual OpsReportTvdLots TvdLot { get; set; }
        [ForeignKey(nameof(TvdReportId))]
        [InverseProperty(nameof(OpsReportTvdReports.OpsReports))]
        public virtual OpsReportTvdReports TvdReport { get; set; }
        [ForeignKey(nameof(VolKickTolId))]
        [InverseProperty(nameof(OpsReportVolKickTols.OpsReports))]
        public virtual OpsReportVolKickTols VolKickTol { get; set; }
        [ForeignKey(nameof(WeatherId))]
        [InverseProperty(nameof(OpsReportWeathers.OpsReports))]
        public virtual OpsReportWeathers Weather { get; set; }
        [InverseProperty("OpsReport")]
        public virtual ICollection<OpsReportDayCosts> OpsReportDayCosts { get; set; }
        [InverseProperty("OpsReport")]
        public virtual ICollection<OpsReportPersonnels> OpsReportPersonnels { get; set; }
        [InverseProperty("OpsReport")]
        public virtual ICollection<OpsReportPitVolumes> OpsReportPitVolumes { get; set; }
        [InverseProperty("OpsReport")]
        public virtual ICollection<OpsReportScrs> OpsReportScrs { get; set; }
    }
}
