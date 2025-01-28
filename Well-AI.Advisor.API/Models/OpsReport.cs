/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Models
{

    public class OpsReportETimStart
    {
        [Key]
        public int ETimStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimSpud
    {
        [Key]
        public int ETimSpudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimLoc
    {
        [Key]
        public int ETimLocId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdReport
    {
        [Key]
        public int MdReportId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdReport
    {
        [Key]
        public int TvdReportId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }



    public class OpsReportETimDrill
    {
        [Key]
        public int ETimDrillId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdPlanned
    {
        [Key]
        public int MdPlannedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimDrillRot
    {
        [Key]
        public int ETimDrillRotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimDrillSlid
    {
        [Key]
        public int ETimDrillSlidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimCirc
    {
        [Key]
        public int ETimCircId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimReam
    {
        [Key]
        public int ETimReamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimHold
    {
        [Key]
        public int ETimHoldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimSteering
    {
        [Key]
        public int ETimSteeringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportDuration
    {
        [Key]
        public int DurationId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportMdBitStart
    {
        [Key]
        public int MdBitStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdBitEnd
    {
        [Key]
        public int MdBitEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportCostPerItem
    {
        [Key]
        public int CostPerItemId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCostAmount
    {
        [Key]
        public int CostAmountId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDayCost
    {
        [Key]
        public int DayCostId { get; set; }
        public string NumAFE { get; set; }
        public string CostGroup { get; set; }
        public string CostClass { get; set; }
        public string CostCode { get; set; }
        public string CostSubCode { get; set; }
        public string CostItemDescription { get; set; }
        public OpsReportCostPerItem CostPerItem { get; set; }
        public string ItemKind { get; set; }
        public string ItemSize { get; set; }
        public string QtyItem { get; set; }
        public OpsReportCostAmount CostAmount { get; set; }
        public string NumInvoice { get; set; }
        public string NumPO { get; set; }
        public string NumTicket { get; set; }
        public string IsCarryOver { get; set; }
        public string IsRental { get; set; }
        public string NumSerial { get; set; }
        public string NameVendor { get; set; }
        public string NumVendor { get; set; }
        public string Pool { get; set; }
        public string Estimated { get; set; }
        public string Uid { get; set; }
    }



    public class OpsReportMtf
    {
        [Key]
        public int MtfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGtf
    {
        [Key]
        public int GtfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDispNs
    {
        [Key]
        public int DispNsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDispEw
    {
        [Key]
        public int DispEwId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVertSect
    {
        [Key]
        public int VertSectId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDls
    {
        [Key]
        public int DlsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRateTurn
    {
        [Key]
        public int RateTurnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRateBuild
    {
        [Key]
        public int RateBuildId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdDelta
    {
        [Key]
        public int MdDeltaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdDelta
    {
        [Key]
        public int TvdDeltaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTotalUncert
    {
        [Key]
        public int GravTotalUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDipAngleUncert
    {
        [Key]
        public int DipAngleUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTotalUncert
    {
        [Key]
        public int MagTotalUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravAxialRaw
    {
        [Key]
        public int GravAxialRawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTran1Raw
    {
        [Key]
        public int GravTran1RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTran2Raw
    {
        [Key]
        public int GravTran2RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagAxialRaw
    {
        [Key]
        public int MagAxialRawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTran1Raw
    {

        [Key]
        public int MagTran1RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTran2Raw
    {
        [Key]
        public int MagTran2RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRawData
    {
        [Key]
        public int RawDataId { get; set; }
        public OpsReportGravAxialRaw GravAxialRaw { get; set; }
        public OpsReportGravTran1Raw GravTran1Raw { get; set; }
        public OpsReportGravTran2Raw GravTran2Raw { get; set; }
        public OpsReportMagAxialRaw MagAxialRaw { get; set; }
        public OpsReportMagTran1Raw MagTran1Raw { get; set; }
        public OpsReportMagTran2Raw MagTran2Raw { get; set; }
    }

    public class OpsReportGravAxialAccelCor
    {
        [Key]
        public int GravAxialAccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTran1AccelCor
    {
        [Key]
        public int GravTran1AccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTran2AccelCor
    {
        [Key]
        public int GravTran2AccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagAxialDrlstrCor
    {
        [Key]
        public int MagAxialDrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTran1DrlstrCor
    {
        [Key]
        public int MagTran1DrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTran2DrlstrCor
    {
        [Key]
        public int MagTran2DrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSagIncCor
    {
        [Key]
        public int SagIncCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSagAziCor
    {
        [Key]
        public int SagAziCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportStnMagDeclUsed
    {
        [Key]
        public int StnMagDeclUsedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportStnGridCorUsed
    {
        [Key]
        public int StnGridCorUsedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDirSensorOffset
    {
        [Key]
        public int DirSensorOffsetId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCorUsed
    {
        [Key]
        public int CorUsedId { get; set; }
        public OpsReportGravAxialAccelCor GravAxialAccelCor { get; set; }
        public OpsReportGravTran1AccelCor GravTran1AccelCor { get; set; }
        public OpsReportGravTran2AccelCor GravTran2AccelCor { get; set; }
        public OpsReportMagAxialDrlstrCor MagAxialDrlstrCor { get; set; }
        public OpsReportMagTran1DrlstrCor MagTran1DrlstrCor { get; set; }
        public OpsReportMagTran2DrlstrCor MagTran2DrlstrCor { get; set; }
        public OpsReportSagIncCor SagIncCor { get; set; }
        public OpsReportSagAziCor SagAziCor { get; set; }
        public OpsReportStnMagDeclUsed StnMagDeclUsed { get; set; }
        public OpsReportStnGridCorUsed StnGridCorUsed { get; set; }
        public OpsReportDirSensorOffset DirSensorOffset { get; set; }
    }

    public class OpsReportMagTotalFieldCalc
    {
        [Key]
        public int MagTotalFieldCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagDipAngleCalc
    {
        [Key]
        public int MagDipAngleCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTotalFieldCalc
    {
        [Key]
        public int GravTotalFieldCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportValid
    {
        [Key]
        public int ValidId { get; set; }
        public OpsReportMagTotalFieldCalc MagTotalFieldCalc { get; set; }
        public OpsReportMagDipAngleCalc MagDipAngleCalc { get; set; }
        public OpsReportGravTotalFieldCalc GravTotalFieldCalc { get; set; }
    }

    public class OpsReportVarianceNN
    {
        [Key]
        public int VarianceNNId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceNE
    {
        [Key]
        public int OpsReportsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceNVert
    {
        [Key]
        public int VarianceNVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceEE
    {
        [Key]
        public int VarianceEEId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceEVert
    {
        [Key]
        public int VarianceEVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceVertVert
    {
        [Key]
        public int VarianceVertVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBiasN
    {
        [Key]
        public int BiasNId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBiasE
    {
        [Key]
        public int BiasEId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBiasVert
    {
        [Key]
        public int BiasVertId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMatrixCov
    {
        [Key]
        public int MatrixCovId { get; set; }
        public OpsReportVarianceNN VarianceNN { get; set; }
        public OpsReportVarianceNE VarianceNE { get; set; }
        public OpsReportVarianceNVert VarianceNVert { get; set; }
        public OpsReportVarianceEE VarianceEE { get; set; }
        public OpsReportVarianceEVert VarianceEVert { get; set; }
        public OpsReportVarianceVertVert VarianceVertVert { get; set; }
        public OpsReportBiasN BiasN { get; set; }
        public OpsReportBiasE BiasE { get; set; }
        public OpsReportBiasVert BiasVert { get; set; }
    }


    public class OpsReportPump
    {
        [Key]
        public int PumpId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRateStroke
    {
        [Key]
        public int RateStrokeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresRecorded
    {
        [Key]
        public int PresRecordedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdBit
    {
        [Key]
        public int MdBitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportScr
    {
        [Key]
        public int ScrId { get; set; }
        public string DTim { get; set; }
        public OpsReportPump Pump { get; set; }
        public string TypeScr { get; set; }
        public OpsReportRateStroke RateStroke { get; set; }
        public OpsReportPresRecorded PresRecorded { get; set; }
        public OpsReportMdBit MdBit { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportPit
    {
        [Key]
        public int PitId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolPit
    {
        [Key]
        public int VolPitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDensFluid
    {
        [Key]
        public int DensFluidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPitVolume
    {
        [Key]
        public int PitVolumeId { get; set; }
        public OpsReportPit Pit { get; set; }
        public string DTim { get; set; }
        public OpsReportVolPit VolPit { get; set; }
        public OpsReportDensFluid DensFluid { get; set; }
        public string DescFluid { get; set; }
        public OpsReportVisFunnel VisFunnel { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportVolTotMudStart
    {
        [Key]
        public int VolTotMudStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudDumped
    {
        [Key]
        public int VolMudDumpedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudReceived
    {
        [Key]
        public int VolMudReceivedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudReturned
    {
        [Key]
        public int VolMudReturnedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostShakerSurf
    {
        [Key]
        public int VolLostShakerSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostMudCleanerSurf
    {
        [Key]
        public int VolLostMudCleanerSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostPitsSurf
    {
        [Key]
        public int VolLostPitsSurfId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostTrippingSurf
    {
        [Key]
        public int VolLostTrippingSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostOtherSurf
    {
        [Key]
        public int VolLostOtherSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolTotMudLostSurf
    {
        [Key]
        public int VolTotMudLostSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostCircHole
    {
        [Key]
        public int VolLostCircHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostCsgHole
    {
        [Key]
        public int VolLostCsgHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostCmtHole
    {
        [Key]
        public int VolLostCmtHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostBhdCsgHole
    {
        [Key]
        public int VolLostBhdCsgHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostAbandonHole
    {
        [Key]
        public int VolLostAbandonHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostOtherHole
    {
        [Key]
        public int VolLostOtherHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolTotMudLostHole
    {
        [Key]
        public int VolTotMudLostHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMudLosses
    {
        [Key]
        public int MudLossesId { get; set; }
        public OpsReportVolLostShakerSurf VolLostShakerSurf { get; set; }
        public OpsReportVolLostMudCleanerSurf VolLostMudCleanerSurf { get; set; }
        public OpsReportVolLostPitsSurf VolLostPitsSurf { get; set; }
        public OpsReportVolLostTrippingSurf VolLostTrippingSurf { get; set; }
        public OpsReportVolLostOtherSurf VolLostOtherSurf { get; set; }
        public OpsReportVolTotMudLostSurf VolTotMudLostSurf { get; set; }
        public OpsReportVolLostCircHole VolLostCircHole { get; set; }
        public OpsReportVolLostCsgHole VolLostCsgHole { get; set; }
        public OpsReportVolLostCmtHole VolLostCmtHole { get; set; }
        public OpsReportVolLostBhdCsgHole VolLostBhdCsgHole { get; set; }
        public OpsReportVolLostAbandonHole VolLostAbandonHole { get; set; }
        public OpsReportVolLostOtherHole VolLostOtherHole { get; set; }
        public OpsReportVolTotMudLostHole VolTotMudLostHole { get; set; }
    }

    public class OpsReportVolMudBuilt
    {
        [Key]
        public int VolMudBuiltId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudString
    {
        [Key]
        public int VolMudStringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudCasing
    {
        [Key]
        public int VolMudCasingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudHole
    {
        [Key]
        public int VolMudHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudRiser
    {
        [Key]
        public int VolMudRiserId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolTotMudEnd
    {
        [Key]
        public int VolTotMudEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMudVolume
    {
        [Key]
        public int MudVolumeId { get; set; }
        public OpsReportVolTotMudStart VolTotMudStart { get; set; }
        public OpsReportVolMudDumped VolMudDumped { get; set; }
        public OpsReportVolMudReceived VolMudReceived { get; set; }
        public OpsReportVolMudReturned VolMudReturned { get; set; }
        public OpsReportMudLosses MudLosses { get; set; }
        public OpsReportVolMudBuilt VolMudBuilt { get; set; }
        public OpsReportVolMudString VolMudString { get; set; }
        public OpsReportVolMudCasing VolMudCasing { get; set; }
        public OpsReportVolMudHole VolMudHole { get; set; }
        public OpsReportVolMudRiser VolMudRiser { get; set; }
        public OpsReportVolTotMudEnd VolTotMudEnd { get; set; }
    }

    public class OpsReportItemWtPerUnit
    {
        [Key]
        public int ItemWtPerUnitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPricePerUnit
    {
        [Key]
        public int PricePerUnitId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCostItem
    {
        [Key]
        public int CostItemId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMudInventory
    {
        [Key]
        public int MudInventoryId { get; set; }
        public string Name { get; set; }
        public OpsReportItemWtPerUnit ItemWtPerUnit { get; set; }
        public OpsReportPricePerUnit PricePerUnit { get; set; }
        public string QtyStart { get; set; }
        public string QtyAdjustment { get; set; }
        public string QtyReceived { get; set; }
        public string QtyReturned { get; set; }
        public string QtyUsed { get; set; }
        public OpsReportCostItem CostItem { get; set; }
        public string QtyOnLocation { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportItemVolPerUnit
    {
        [Key]
        public int ItemVolPerUnitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBulk
    {
        [Key]
        public int BulkId { get; set; }
        public string Name { get; set; }
        public OpsReportItemVolPerUnit ItemVolPerUnit { get; set; }
        public OpsReportPricePerUnit PricePerUnit { get; set; }
        public string QtyStart { get; set; }
        public string QtyAdjustment { get; set; }
        public string QtyReceived { get; set; }
        public string QtyReturned { get; set; }
        public string QtyUsed { get; set; }
        public OpsReportCostItem CostItem { get; set; }
        public string QtyOnLocation { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportAnchorTension
    {
        [Key]
        public int AnchorTensionId { get; set; }
        public string Uom { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAnchorAngle
    {
        [Key]
        public int AnchorAngleId { get; set; }
        public string Uom { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigHeading
    {
        [Key]
        public int RigHeadingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigHeave
    {
        [Key]
        public int RigHeaveId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigPitchAngle
    {
        [Key]
        public int RigPitchAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigRollAngle
    {
        [Key]
        public int RigRollAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRiserAngle
    {
        [Key]
        public int RiserAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRiserDirection
    {
        [Key]
        public int RiserDirectionId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRiserTension
    {
        [Key]
        public int RiserTensionId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVariableDeckLoad
    {
        [Key]
        public int VariableDeckLoadId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTotalDeckLoad
    {
        [Key]
        public int TotalDeckLoadId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGuideBaseAngle
    {
        [Key]
        public int GuideBaseAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBallJointAngle
    {
        [Key]
        public int BallJointAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBallJointDirection
    {
        [Key]
        public int BallJointDirectionId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportOffsetRig
    {
        [Key]
        public int OffsetRigId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLoadLeg1
    {
        [Key]
        public int LoadLeg1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLoadLeg2
    {
        [Key]
        public int LoadLeg2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLoadLeg3
    {
        [Key]
        public int LoadLeg3Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLoadLeg4
    {
        [Key]
        public int LoadLeg4Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPenetrationLeg1
    {
        [Key]
        public int PenetrationLeg1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPenetrationLeg2
    {
        [Key]
        public int PenetrationLeg2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPenetrationLeg3
    {
        [Key]
        public int PenetrationLeg3Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPenetrationLeg4
    {
        [Key]
        public int PenetrationLeg4Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDispRig
    {
        [Key]
        public int DispRigId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMeanDraft
    {
        [Key]
        public int MeanDraftId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigResponse
    {
        [Key]
        public int RigResponseId { get; set; }
        public List<OpsReportAnchorTension> AnchorTension { get; set; }
        public List<OpsReportAnchorAngle> AnchorAngle { get; set; }
        public OpsReportRigHeading RigHeading { get; set; }
        public OpsReportRigHeave RigHeave { get; set; }
        public OpsReportRigPitchAngle RigPitchAngle { get; set; }
        public OpsReportRigRollAngle RigRollAngle { get; set; }
        public OpsReportRiserAngle RiserAngle { get; set; }
        public OpsReportRiserDirection RiserDirection { get; set; }
        public OpsReportRiserTension RiserTension { get; set; }
        public OpsReportVariableDeckLoad VariableDeckLoad { get; set; }
        public OpsReportTotalDeckLoad TotalDeckLoad { get; set; }
        public OpsReportGuideBaseAngle GuideBaseAngle { get; set; }
        public OpsReportBallJointAngle BallJointAngle { get; set; }
        public OpsReportBallJointDirection BallJointDirection { get; set; }
        public OpsReportOffsetRig OffsetRig { get; set; }
        public OpsReportLoadLeg1 LoadLeg1 { get; set; }
        public OpsReportLoadLeg2 LoadLeg2 { get; set; }
        public OpsReportLoadLeg3 LoadLeg3 { get; set; }
        public OpsReportLoadLeg4 LoadLeg4 { get; set; }
        public OpsReportPenetrationLeg1 PenetrationLeg1 { get; set; }
        public OpsReportPenetrationLeg2 PenetrationLeg2 { get; set; }
        public OpsReportPenetrationLeg3 PenetrationLeg3 { get; set; }
        public OpsReportPenetrationLeg4 PenetrationLeg4 { get; set; }
        public OpsReportDispRig DispRig { get; set; }
        public OpsReportMeanDraft MeanDraft { get; set; }
    }

    public class OpsReportIdLiner
    {
        [Key]
        public int IdLinerId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLenStroke
    {
        [Key]
        public int LenStrokeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPressure
    {
        [Key]
        public int PressureId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPcEfficiency
    {
        [Key]
        public int PcEfficiencyId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPumpOutput
    {
        [Key]
        public int PumpOutputId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPumpOp
    {
        [Key]
        public int PumpOpId { get; set; }
        public string DTim { get; set; }
        public OpsReportPump Pump { get; set; }
        public string TypeOperation { get; set; }
        public OpsReportIdLiner IdLiner { get; set; }
        public OpsReportLenStroke LenStroke { get; set; }
        public OpsReportRateStroke RateStroke { get; set; }
        public OpsReportPressure Pressure { get; set; }
        public OpsReportPcEfficiency PcEfficiency { get; set; }
        public OpsReportPumpOutput PumpOutput { get; set; }
        public OpsReportMdBit MdBit { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportShaker
    {
        [Key]
        public int ShakerId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportHoursRun
    {
        [Key]
        public int HoursRunId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPcScreenCovered
    {
        [Key]
        public int PcScreenCoveredId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMeshX
    {
        [Key]
        public int MeshXId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMeshY
    {
        [Key]
        public int MeshYId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCutPoint
    {
        [Key]
        public int CutPointId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportShakerScreen
    {
        [Key]
        public int ShakerScreenId { get; set; }
        public string DTimStart { get; set; }
        public string DTimEnd { get; set; }
        public string NumDeck { get; set; }
        public OpsReportMeshX MeshX { get; set; }
        public OpsReportMeshY MeshY { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public OpsReportCutPoint CutPoint { get; set; }
    }

    public class OpsReportMdHole
    {
        [Key]
        public int MdHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportShakerOp
    {
        [Key]
        public int ShakerOpId { get; set; }
        public OpsReportShaker Shaker { get; set; }
        public OpsReportMdHole MdHole { get; set; }
        public string DTim { get; set; }
        public OpsReportHoursRun HoursRun { get; set; }
        public OpsReportPcScreenCovered PcScreenCovered { get; set; }
        public OpsReportShakerScreen ShakerScreen { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportDaysIncFree
    {
        [Key]
        public int DaysIncFreeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimLostGross
    {
        [Key]
        public int ETimLostGrossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCostLostGross
    {
        [Key]
        public int CostLostGrossId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportIncident
    {
        [Key]
        public int IncidentId { get; set; }
        public string DTim { get; set; }
        public string Reporter { get; set; }
        public string NumMinorInjury { get; set; }
        public string NumMajorInjury { get; set; }
        public string NumFatality { get; set; }
        public string IsNearMiss { get; set; }
        public string DescLocation { get; set; }
        public string DescAccident { get; set; }
        public string RemedialActionDesc { get; set; }
        public string CauseDesc { get; set; }
        public OpsReportETimLostGross ETimLostGross { get; set; }
        public OpsReportCostLostGross CostLostGross { get; set; }
        public string ResponsibleCompany { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportPresLastCsg
    {
        [Key]
        public int PresLastCsgId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresStdPipe
    {
        [Key]
        public int PresStdPipeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresKellyHose
    {
        [Key]
        public int PresKellyHoseId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresDiverter
    {
        [Key]
        public int PresDiverterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresAnnular
    {
        [Key]
        public int PresAnnularId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresRams
    {
        [Key]
        public int PresRamsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresChokeLine
    {
        [Key]
        public int PresChokeLineId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresChokeMan
    {
        [Key]
        public int PresChokeManId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFluidDischarged
    {
        [Key]
        public int FluidDischargedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolCtgDischarged
    {
        [Key]
        public int VolCtgDischargedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolOilCtgDischarge
    {
        [Key]
        public int VolOilCtgDischargeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWasteDischarged
    {
        [Key]
        public int WasteDischargedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHse
    {
        [Key]
        public int HseId { get; set; }
        public OpsReportDaysIncFree DaysIncFree { get; set; }
        public OpsReportIncident Incident { get; set; }
        public string LastCsgPresTest { get; set; }
        public OpsReportPresLastCsg PresLastCsg { get; set; }
        public string LastBopPresTest { get; set; }
        public string NextBopPresTest { get; set; }
        public OpsReportPresStdPipe PresStdPipe { get; set; }
        public OpsReportPresKellyHose PresKellyHose { get; set; }
        public OpsReportPresDiverter PresDiverter { get; set; }
        public OpsReportPresAnnular PresAnnular { get; set; }
        public OpsReportPresRams PresRams { get; set; }
        public OpsReportPresChokeLine PresChokeLine { get; set; }
        public OpsReportPresChokeMan PresChokeMan { get; set; }
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
        public OpsReportFluidDischarged FluidDischarged { get; set; }
        public OpsReportVolCtgDischarged VolCtgDischarged { get; set; }
        public OpsReportVolOilCtgDischarge VolOilCtgDischarge { get; set; }
        public OpsReportWasteDischarged WasteDischarged { get; set; }
        public string Comments { get; set; }
    }

    public class OpsReportTotalTime
    {
        [Key]
        public int TotalTimeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPersonnel
    {
        [Key]
        public int PersonnelId { get; set; }
        public string Company { get; set; }
        public string TypeService { get; set; }
        public string NumPeople { get; set; }
        public OpsReportTotalTime TotalTime { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportSupportCraft
    {
        [Key]
        public int SupportCraftId { get; set; }
        public string Name { get; set; }
        public string TypeSuppCraft { get; set; }
        public string DTimArrived { get; set; }
        public string DTimDeparted { get; set; }
        public string Comments { get; set; }
       
        public string Uid { get; set; }
    }

    public class OpsReportBarometricPressure
    {
        [Key]
        public int BarometricPressureId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempSurfaceMn
    {
        [Key]
        public int TempSurfaceMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempSurfaceMx
    {
        [Key]
        public int TempSurfaceMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempWindChill
    {
        [Key]
        public int TempWindChillId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempsea
    {
        [Key]
        public int TempseaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVisibility
    {
        [Key]
        public int VisibilityId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziWave
    {
        [Key]
        public int AziWaveId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHtWave
    {
        [Key]
        public int HtWaveId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPeriodWave
    {
        [Key]
        public int PeriodWaveId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziWind
    {
        [Key]
        public int AziWindId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVelWind
    {
        [Key]
        public int VelWindId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAmtPrecip
    {
        [Key]
        public int AmtPrecipId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCeilingCloud
    {
        [Key]
        public int CeilingCloudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCurrentSea
    {
        [Key]
        public int CurrentSeaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziCurrentSea
    {
        [Key]
        public int AziCurrentSeaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWeather
    {
        [Key]
        public int WeatherId { get; set; }
        public string DTim { get; set; }
        public string Agency { get; set; }
        public OpsReportBarometricPressure BarometricPressure { get; set; }
        public string BeaufortScaleNumber { get; set; }
        public OpsReportTempSurfaceMn TempSurfaceMn { get; set; }
        public OpsReportTempSurfaceMx TempSurfaceMx { get; set; }
        public OpsReportTempWindChill TempWindChill { get; set; }
        public OpsReportTempsea Tempsea { get; set; }
        public OpsReportVisibility Visibility { get; set; }
        public OpsReportAziWave AziWave { get; set; }
        public OpsReportHtWave HtWave { get; set; }
        public OpsReportPeriodWave PeriodWave { get; set; }
        public OpsReportAziWind AziWind { get; set; }
        public OpsReportVelWind VelWind { get; set; }
        public string TypePrecip { get; set; }
        public OpsReportAmtPrecip AmtPrecip { get; set; }
        public string CoverCloud { get; set; }
        public OpsReportCeilingCloud CeilingCloud { get; set; }
        public OpsReportCurrentSea CurrentSea { get; set; }
        public OpsReportAziCurrentSea AziCurrentSea { get; set; }
        public string Comments { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportCostDay
    {
        [Key]
        public int CostDayId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCostDayMud
    {
        [Key]
        public int CostDayMudId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

   

    public class OpsReportTvdCsgLast
    {
        [Key]
        public int TvdCsgLastId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdLot
    {
        [Key]
        public int TvdLotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresLotEmw
    {
        [Key]
        public int PresLotEmwId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresKickTol
    {
        [Key]
        public int PresKickTolId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolKickTol
    {
        [Key]
        public int VolKickTolId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMaasp
    {
        [Key]
        public int MaaspId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDensity
    {
        [Key]
        public int DensityId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVisFunnel
    {
        [Key]
        public int VisFunnelId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempVis
    {
        [Key]
        public int TempVisId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPv
    {
        [Key]
        public int ReportPvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportYp
    {
        [Key]
        public int YpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGel10Sec
    {
        [Key]
        public int Gel10SecId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGel10Min
    {
        [Key]
        public int Gel10MinId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGel30Min
    {
        [Key]
        public int Gel30MinId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFilterCakeLtlp
    {
        [Key]
        public int FilterCakeLtlpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFiltrateLtlp
    {
        [Key]
        public int FiltrateLtlpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempHthp
    {
        [Key]
        public int TempHthpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresHthp
    {
        [Key]
        public int PresHthpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFiltrateHthp
    {
        [Key]
        public int FiltrateHthpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFilterCakeHthp
    {
        [Key]
        public int FilterCakeHthpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolidsPc
    {
        [Key]
        public int SolidsPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWaterPc
    {
        [Key]
        public int WaterPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportOilPc
    {
        [Key]
        public int OilPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSandPc
    {
        [Key]
        public int SandPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolidsLowGravPc
    {
        [Key]
        public int SolidsLowGravPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolidsCalcPc
    {
        [Key]
        public int SolidsCalcPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBaritePc
    {
        [Key]
        public int BaritePcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLcm
    {
        [Key]
        public int LcmId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMbt
    {
        [Key]
        public int MbtId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempPh
    {
        [Key]
        public int TempPhId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPm
    {
        [Key]
        public int PmId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPmFiltrate
    {
        [Key]
        public int PmFiltrateId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMf
    {
        [Key]
        public int MfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAlkalinityP1
    {
        [Key]
        public int AlkalinityP1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAlkalinityP2
    {
        [Key]
        public int AlkalinityP2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportChloride
    {
        [Key]
        public int ChlorideId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCalcium
    {
        [Key]
        public int MagnesiumId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagnesium
    {
        [Key]
        public int MagnesiumId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempRheom
    {
        [Key]
        public int TempRheomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresRheom
    {
        [Key]
        public int PresRheomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRheometer
    {
        [Key]
        public int RheometerId { get; set; }
        public OpsReportTempRheom TempRheom { get; set; }
        public OpsReportPresRheom PresRheom { get; set; }
        public string Vis3Rpm { get; set; }
        public string Vis6Rpm { get; set; }
        public string Vis100Rpm { get; set; }
        public string Vis200Rpm { get; set; }
        public string Vis300Rpm { get; set; }
        public string Vis600Rpm { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportBrinePc
    {
        [Key]
        public int BrinePcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLime
    {
        [Key]
        public int LimeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportElectStab
    {
        [Key]
        public int ElectStabId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCalciumChloride
    {
        [Key]
        public int CalciumChlorideId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolidsHiGravPc
    {
        [Key]
        public int SolidsHiGravPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPolymer
    {
        [Key]
        public int PolymerId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolCorPc
    {
        [Key]
        public int SolCorPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportOilCtg
    {
        [Key]
        public int OilCtgId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHardnessCa
    {
        [Key]
        public int HardnessCaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSulfide
    {
        [Key]
        public int SulfideId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFluid
    {
        [Key]
        public string Uid { get; set; }
        public string Type { get; set; }
        public string LocationSample { get; set; }
        public OpsReportDensity Density { get; set; }
        public OpsReportVisFunnel VisFunnel { get; set; }
        public OpsReportTempVis TempVis { get; set; }
        public OpsReportPv Pv { get; set; }
        public OpsReportYp Yp { get; set; }
        public OpsReportGel10Sec Gel10Sec { get; set; }
        public OpsReportGel10Min Gel10Min { get; set; }
        public OpsReportGel30Min Gel30Min { get; set; }
        public OpsReportFilterCakeLtlp FilterCakeLtlp { get; set; }
        public OpsReportFiltrateLtlp FiltrateLtlp { get; set; }
        public OpsReportTempHthp TempHthp { get; set; }
        public OpsReportPresHthp PresHthp { get; set; }
        public OpsReportFiltrateHthp FiltrateHthp { get; set; }
        public OpsReportFilterCakeHthp FilterCakeHthp { get; set; }
        public OpsReportSolidsPc SolidsPc { get; set; }
        public OpsReportWaterPc WaterPc { get; set; }
        public OpsReportOilPc OilPc { get; set; }
        public OpsReportSandPc SandPc { get; set; }
        public OpsReportSolidsLowGravPc SolidsLowGravPc { get; set; }
        public OpsReportSolidsCalcPc SolidsCalcPc { get; set; }
        public OpsReportBaritePc BaritePc { get; set; }
        public OpsReportLcm Lcm { get; set; }
        public OpsReportMbt Mbt { get; set; }
        public string Ph { get; set; }
        public OpsReportTempPh TempPh { get; set; }
        public OpsReportPm Pm { get; set; }
        public OpsReportPmFiltrate PmFiltrate { get; set; }
        public OpsReportMf Mf { get; set; }
        public OpsReportAlkalinityP1 AlkalinityP1 { get; set; }
        public OpsReportAlkalinityP2 AlkalinityP2 { get; set; }
        public OpsReportChloride Chloride { get; set; }
        public OpsReportCalcium Calcium { get; set; }
        public OpsReportMagnesium Magnesium { get; set; }
        public List<OpsReportRheometer> Rheometer { get; set; }
        public OpsReportBrinePc BrinePc { get; set; }
        public OpsReportLime Lime { get; set; }
        public OpsReportElectStab ElectStab { get; set; }
        public OpsReportCalciumChloride CalciumChloride { get; set; }
        public string Company { get; set; }
        public string Engineer { get; set; }
        public string Asg { get; set; }
        public OpsReportSolidsHiGravPc SolidsHiGravPc { get; set; }
        public OpsReportPolymer Polymer { get; set; }
        public string PolyType { get; set; }
        public OpsReportSolCorPc SolCorPc { get; set; }
        public OpsReportOilCtg OilCtg { get; set; }
        public OpsReportHardnessCa HardnessCa { get; set; }
        public OpsReportSulfide Sulfide { get; set; }
        public string Comments { get; set; }
       
    }
    public class OpsReportRig
    {
        [Key]
        public int OpsReportRigId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdHoleStart
    {
        [Key]
        public int MdHoleStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdHoleStart
    {
        [Key]
        public int TvdHoleStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdHoleEnd
    {
        [Key]
        public int MdHoleEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdHoleEnd
    {
        [Key]
        public int TvdHoleEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportActivity
    {
        [Key]
        public string Uid { get; set; }
        public string DTimStart { get; set; }
        public string DTimEnd { get; set; }
        public OpsReportDuration Duration { get; set; }
        public string Phase { get; set; }
        public string ActivityCode { get; set; }
        public string DetailActivity { get; set; }
        public string TypeActivityClass { get; set; }
        public OpsReportMdHoleStart MdHoleStart { get; set; }
        public OpsReportTvdHoleStart TvdHoleStart { get; set; }
        public OpsReportMdHoleEnd MdHoleEnd { get; set; }
        public OpsReportTvdHoleEnd TvdHoleEnd { get; set; }
        public OpsReportMdBitStart MdBitStart { get; set; }
        public OpsReportMdBitEnd MdBitEnd { get; set; }
        public string State { get; set; }
        public string Operator { get; set; }
        public string Optimum { get; set; }
        public string Productive { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }
        
    }


    public class OpsReportTubular
    {
        [Key]
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWtMud
    {
        [Key]
        [Required]

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimOpBit
    {
        [Key]
        [Required]
        public int ETimOpBitId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdHoleStop
    {
        [Key]
        [Required]
        public int MdHoleStopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }



    public class OpsReportHkldRot
    {
        [Key]
        [Required]
        public int HkldRotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportOverPull
    {
        [Key]
        [Required]
        public int OverPullId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSlackOff
    {
        [Key]
        [Required]
        public int SlackOffId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHkldUp
    {
        [Key]
        [Required]
        public int HkldUpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHkldDn
    {
        [Key]
        [Required]
        public int HkldDnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqOnBotAv
    {
        [Key]
        [Required]
        public int TqOnBotAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqOnBotMx
    {
        [Key]
        [Required]
        public int TqOnBotMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqOnBotMn
    {
        [Key]
        [Required]
        public int TqOnBotMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqOffBotAv
    {
        [Key]
        [Required]
        public int TqOffBotAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqDhAv
    {
        [Key]
        [Required]
        public int TqDhAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWtAboveJar
    {
        [Key]
        [Required]
        public int WtAboveJarId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWtBelowJar
    {
        [Key]
        [Required]
        public int WtBelowJarId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportFlowratePump
    {
        [Key]
        [Required]
        public int FlowratePumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPowBit
    {
        [Key]
        [Required]
        public int PowBitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVelNozzleAv
    {
        [Key]
        [Required]
        public int VelNozzleAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresDropBit
    {
        [Key]
        [Required]
        public int PresDropBitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimHold
    {
        [Key]
        [Required]
        public int CTimHoldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimSteering
    {
        [Key]
        [Required]
        public int CTimSteeringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimDrillRot
    {
        [Key]
        [Required]
        public int CTimDrillRotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimDrillSlid
    {
        [Key]
        [Required]
        public int CTimDrillSlidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimCirc
    {
        [Key]
        [Required]
        public int CTimCircId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimReam
    {
        [Key]
        [Required]
        public int CTimReamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistDrillRot
    {
        [Key]
        [Required]
        public int DistDrillRotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistDrillSlid
    {
        [Key]
        [Required]
        public int DistDrillSlidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistReam
    {
        [Key]
        [Required]
        public int DistReamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistHold
    {

        [Key]
        [Required]
        public int DistHoldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistSteering
    {
        [Key]
        [Required]
        public int DistSteeringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRpmAv
    {
        [Key]
        [Required]
        public int RpmAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRpmMx
    {
        [Key]
        [Required]
        public int RpmMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRpmMn
    {
        [Key]
        [Required]
        public int RpmMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRpmAvDh
    {
        [Key]
        [Required]
        public int RpmAvDhId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRopMx
    {
        [Key]
        [Required]
        public int RopMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRopMn
    {
        [Key]
        [Required]
        public int RopMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWobAv
    {
        [Key]
        [Required]
        public int WobAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWobMx
    {
        [Key]
        [Required]
        public int WobMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRopAv
    {
        [Key]
        [Required]
        public int RopAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWobMn
    {
        [Key]
        [Required]
        public int WobMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWobAvDh
    {
        [Key]
        [Required]
        public int WobAvDhId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziTop
    {
        [Key]
        [Required]
        public int AziTopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziBottom
    {
        [Key]
        [Required]
        public int AziBottomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclStart
    {
        [Key]
        [Required]
        public int InclStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclMx
    {
        [Key]
        [Required]
        public int InclMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclMn
    {
        [Key]
        [Required]
        public int  InclMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclStop
    {
        [Key]
        [Required]
        public int InclStopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempMudDhMx
    {
        [Key]
        [Required]
        public int TempMudDhMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresPumpAv
    {
        [Key]
        [Required]
        public int PresPumpAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFlowrateBit
    {
        [Key]
        [Required]
        public int FlowrateBitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDrillingParam
    {
        [Key]
        [Required]
        public string Uid { get; set; }
        public OpsReportETimOpBit ETimOpBit { get; set; }

        public OpsReportMdHoleStart MdHoleStart { get; set; }

        public OpsReportMdHoleStop MdHoleStop { get; set; }

        public OpsReportTubular Tubular { get; set; }

        public OpsReportHkldRot HkldRot { get; set; }

        public OpsReportOverPull OverPull { get; set; }

        public OpsReportSlackOff SlackOff { get; set; }

        public OpsReportHkldUp HkldUp { get; set; }

        public OpsReportHkldDn HkldDn { get; set; }

        public OpsReportTqOnBotAv TqOnBotAv { get; set; }

        public OpsReportTqOnBotMx TqOnBotMx { get; set; }

        public OpsReportTqOnBotMn TqOnBotMn { get; set; }

        public OpsReportTqOffBotAv TqOffBotAv { get; set; }

        public OpsReportTqDhAv TqDhAv { get; set; }

        public OpsReportWtAboveJar WtAboveJar { get; set; }

        public OpsReportWtBelowJar WtBelowJar { get; set; }

        public OpsReportWtMud WtMud { get; set; }

        public OpsReportFlowratePump FlowratePump { get; set; }

        public OpsReportPowBit PowBit { get; set; }

        public OpsReportVelNozzleAv VelNozzleAv { get; set; }

        public OpsReportPresDropBit PresDropBit { get; set; }

        public OpsReportCTimHold CTimHold { get; set; }

        public OpsReportCTimSteering CTimSteering { get; set; }

        public OpsReportCTimDrillRot CTimDrillRot { get; set; }

        public OpsReportCTimDrillSlid CTimDrillSlid { get; set; }

        public OpsReportCTimCirc CTimCirc { get; set; }

        public OpsReportCTimReam CTimReam { get; set; }

        public OpsReportDistDrillRot DistDrillRot { get; set; }

        public OpsReportDistDrillSlid DistDrillSlid { get; set; }

        public OpsReportDistReam DistReam { get; set; }

        public OpsReportDistHold DistHold { get; set; }

        public OpsReportDistSteering DistSteering { get; set; }

        public OpsReportRpmAv RpmAv { get; set; }

        public OpsReportRpmMx RpmMx { get; set; }

        public OpsReportRpmMn RpmMn { get; set; }

        public OpsReportRpmAvDh RpmAvDh { get; set; }

        public OpsReportRopAv RopAv { get; set; }

        public OpsReportRopMx RopMx { get; set; }

        public OpsReportRopMn RopMn { get; set; }

        public OpsReportWobAv WobAv { get; set; }

        public OpsReportWobMx WobMx { get; set; }

        public OpsReportWobMn WobMn { get; set; }

        public OpsReportWobAvDh WobAvDh { get; set; }

        public string ReasonTrip { get; set; }

        public string ObjectiveBha { get; set; }

        public OpsReportAziTop AziTop { get; set; }

        public OpsReportAziBottom AziBottom { get; set; }

        public OpsReportInclStart InclStart { get; set; }

        public OpsReportInclMx InclMx { get; set; }

        public OpsReportInclMn InclMn { get; set; }

        public OpsReportInclStop InclStop { get; set; }

        public OpsReportTempMudDhMx TempMudDhMx { get; set; }

        public OpsReportPresPumpAv PresPumpAv { get; set; }

        public OpsReportFlowrateBit FlowrateBit { get; set; }

        public string Comments { get; set; }
    }

    public class OpsReportWellCRS
    {
        [Key]
        public int WellCRSId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLatitude
    {
        [Key]
        public int LatitudeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLongitude
    {
        [Key]
        public int LongitudeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportProjectedX
    {
        [Key]
        public int ProjectedXId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportProjectedY
    {
        [Key]
        public int ProjectedYId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLocation
    {
        [Key]
        public string Uid { get; set; }
        public OpsReportWellCRS WellCRS { get; set; }
        public OpsReportLatitude Latitude { get; set; }
        public OpsReportLongitude Longitude { get; set; }
        public OpsReportProjectedX ProjectedX { get; set; }
        public OpsReportProjectedY ProjectedY { get; set; }
    }

    public class OpsReportMd
    {
        [Key]
        public int MdId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvd
    {
        [Key]
        public int TvdId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportIncl
    {
        [Key]
        public int InclId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAzi
    {
        [Key]
        public int AziId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTrajectoryStation
    {
        [Key]
        public string Uid { get; set; }
        public string DTimStn { get; set; }
        public string TypeTrajStation { get; set; }
        public OpsReportMd Md { get; set; }
        public OpsReportTvd Tvd { get; set; }
        public OpsReportIncl Incl { get; set; }
        public OpsReportAzi Azi { get; set; }
        public OpsReportMtf Mtf { get; set; }
        public OpsReportGtf Gtf { get; set; }
        public OpsReportDispNs DispNs { get; set; }
        public OpsReportDispEw DispEw { get; set; }
        public OpsReportVertSect VertSect { get; set; }
        public OpsReportDls Dls { get; set; }
        public OpsReportRateTurn RateTurn { get; set; }
        public OpsReportRateBuild RateBuild { get; set; }
        public OpsReportMdDelta MdDelta { get; set; }
        public OpsReportTvdDelta TvdDelta { get; set; }
        public string ModelToolError { get; set; }
        public OpsReportGravTotalUncert GravTotalUncert { get; set; }
        public OpsReportDipAngleUncert DipAngleUncert { get; set; }
        public OpsReportMagTotalUncert MagTotalUncert { get; set; }
        public string GravAccelCorUsed { get; set; }
        public string MagXAxialCorUsed { get; set; }
        public string SagCorUsed { get; set; }
        public string MagDrlstrCorUsed { get; set; }
        public string StatusTrajStation { get; set; }
        public OpsReportRawData RawData { get; set; }
        public OpsReportCorUsed CorUsed { get; set; }
        public OpsReportValid Valid { get; set; }
        public OpsReportMatrixCov MatrixCov { get; set; }
        public List<OpsReportLocation> Location { get; set; }
       
    }

    public class OpsReportDistDrill
    {
        [Key]
        public int DistDrillId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRopCurrent
    {
        [Key]
        public int RopCurrentId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDiaHole
    {
        [Key]
        public int DiaHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDiaCsgLast
    {
        [Key]
        public int DiaCsgLastId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdCsgLast
    {
        [Key]
        public int MdCsgLastId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReport
    {
        [Key]
        public int OpsReportId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public OpsReportRig Rig { get; set; }
        public string DTim { get; set; }
        public OpsReportETimStart ETimStart { get; set; }
        public OpsReportETimSpud ETimSpud { get; set; }
        public OpsReportETimLoc ETimLoc { get; set; }
        public OpsReportMdReport MdReport { get; set; }
        public OpsReportTvdReport TvdReport { get; set; }
        public OpsReportDistDrill DistDrill { get; set; }
        public OpsReportETimDrill ETimDrill { get; set; }
        public OpsReportMdPlanned MdPlanned { get; set; }
        public OpsReportRopAv RopAv { get; set; }
        public OpsReportRopCurrent RopCurrent { get; set; }
        public string Supervisor { get; set; }
        public string Engineer { get; set; }
        public string Geologist { get; set; }
        public OpsReportETimDrillRot ETimDrillRot { get; set; }
        public OpsReportETimDrillSlid ETimDrillSlid { get; set; }
        public OpsReportETimCirc ETimCirc { get; set; }
        public OpsReportETimReam ETimReam { get; set; }
        public OpsReportETimHold ETimHold { get; set; }
        public OpsReportETimSteering ETimSteering { get; set; }
        public OpsReportDistDrillRot DistDrillRot { get; set; }
        public OpsReportDistDrillSlid DistDrillSlid { get; set; }
        public OpsReportDistReam DistReam { get; set; }
        public OpsReportDistHold DistHold { get; set; }
        public OpsReportDistSteering DistSteering { get; set; }
        public string NumPob { get; set; }
        public string NumContract { get; set; }
        public string NumOperator { get; set; }
        public string NumService { get; set; }
        public OpsReportActivity Activity { get; set; }
        public OpsReportDrillingParam DrillingParams { get; set; }
        public List<OpsReportDayCost> DayCost { get; set; }
        public OpsReportTrajectoryStation TrajectoryStation { get; set; }
        public OpsReportFluid Fluid { get; set; }
        public List<OpsReportScr> Scr { get; set; }
        public List<OpsReportPitVolume> PitVolume { get; set; }
        public OpsReportMudVolume MudVolume { get; set; }
        public OpsReportMudInventory MudInventory { get; set; }
        public OpsReportBulk Bulk { get; set; }
        public OpsReportRigResponse RigResponse { get; set; }
        public OpsReportPumpOp PumpOp { get; set; }
        public OpsReportShakerOp ShakerOp { get; set; }
        public OpsReportHse Hse { get; set; }
        public List<OpsReportPersonnel> Personnel { get; set; }
        public OpsReportSupportCraft SupportCraft { get; set; }
        public OpsReportWeather Weather { get; set; }
        public string NumAFE { get; set; }
        public OpsReportCostDay CostDay { get; set; }
        public OpsReportCostDayMud CostDayMud { get; set; }
        public OpsReportDiaHole DiaHole { get; set; }
        public string ConditionHole { get; set; }
        public string Lithology { get; set; }
        public string NameFormation { get; set; }
        public OpsReportDiaCsgLast DiaCsgLast { get; set; }
        public OpsReportMdCsgLast MdCsgLast { get; set; }
        public OpsReportTvdCsgLast TvdCsgLast { get; set; }
        public OpsReportTvdLot TvdLot { get; set; }
        public OpsReportPresLotEmw PresLotEmw { get; set; }
        public OpsReportPresKickTol PresKickTol { get; set; }
        public OpsReportVolKickTol VolKickTol { get; set; }
        public OpsReportMaasp Maasp { get; set; }
        public Tubular Tubular { get; set; }
        public string Sum24Hr { get; set; }
        public string Forecast24Hr { get; set; }
        public string StatusCurrent { get; set; }
        public OpsReportsCommonData CommonData { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }
    }

    public class OpsReportsCommonData
    {
        [Key]
        public int OpsReportsCommonDataid { get; set; }
        public string ItemState { get; set; }

        public string Comments { get; set; }
    }
    
}
