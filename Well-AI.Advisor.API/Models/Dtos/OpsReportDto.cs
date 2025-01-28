   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Dtos
{

    public class OpsReportETimStartDto
    {
        [Key]
        public int ETimStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimSpudDto
    {
        [Key]
        public int ETimSpudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimLocDto
    {
        [Key]
        public int ETimLocId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdReportDto
    {
        [Key]
        public int MdReportId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdReportDto
    {
        [Key]
        public int TvdReportId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }



    public class OpsReportETimDrillDto
    {
        [Key]
        public int ETimDrillId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdPlannedDto
    {
        [Key]
        public int MdPlannedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimDrillRotDto
    {
        [Key]
        public int ETimDrillRotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimDrillSlidDto
    {
        [Key]
        public int ETimDrillSlidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimCircDto
    {
        [Key]
        public int ETimCircId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimReamDto
    {
        [Key]
        public int ETimReamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimHoldDto
    {
        [Key]
        public int ETimHoldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimSteeringDto
    {
        [Key]
        public int ETimSteeringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportDurationDto
    {
        [Key]
        public int DurationId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportMdBitStartDto
    {
        [Key]
        public int MdBitStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdBitEndDto
    {
        [Key]
        public int MdBitEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportCostPerItemDto
    {
        [Key]
        public int CostPerItemId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCostAmountDto
    {
        [Key]
        public int CostAmountId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDayCostDto
    {
        [Key]
        public int DayCostId { get; set; }
        public string NumAFE { get; set; }
        public string CostGroup { get; set; }
        public string CostClass { get; set; }
        public string CostCode { get; set; }
        public string CostSubCode { get; set; }
        public string CostItemDescription { get; set; }
        public OpsReportCostPerItemDto CostPerItem { get; set; }
        public string ItemKind { get; set; }
        public string ItemSize { get; set; }
        public string QtyItem { get; set; }
        public OpsReportCostAmountDto CostAmount { get; set; }
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



    public class OpsReportMtfDto
    {
        [Key]
        public int MtfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGtfDto
    {
        [Key]
        public int GtfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDispNsDto
    {
        [Key]
        public int DispNsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDispEwDto
    {
        [Key]
        public int DispEwId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVertSectDto
    {
        [Key]
        public int VertSectId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDlsDto
    {
        [Key]
        public int DlsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRateTurnDto
    {
        [Key]
        public int RateTurnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRateBuildDto
    {
        [Key]
        public int RateBuildId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdDeltaDto
    {
        [Key]
        public int MdDeltaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdDeltaDto
    {
        [Key]
        public int TvdDeltaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTotalUncertDto
    {
        [Key]
        public int GravTotalUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDipAngleUncertDto
    {
        [Key]
        public int DipAngleUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTotalUncertDto
    {
        [Key]
        public int MagTotalUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravAxialRawDto
    {
        [Key]
        public int GravAxialRawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTran1RawDto
    {
        [Key]
        public int GravTran1RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTran2RawDto
    {
        [Key]
        public int GravTran2RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagAxialRawDto
    {
        [Key]
        public int MagAxialRawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTran1RawDto
    {

        [Key]
        public int MagTran1RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTran2RawDto
    {
        [Key]
        public int MagTran2RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRawDataDto
    {
        [Key]
        public int RawDataId { get; set; }
        public OpsReportGravAxialRawDto GravAxialRaw { get; set; }
        public OpsReportGravTran1RawDto GravTran1Raw { get; set; }
        public OpsReportGravTran2RawDto GravTran2Raw { get; set; }
        public OpsReportMagAxialRawDto MagAxialRaw { get; set; }
        public OpsReportMagTran1RawDto MagTran1Raw { get; set; }
        public OpsReportMagTran2RawDto MagTran2Raw { get; set; }
    }

    public class OpsReportGravAxialAccelCorDto
    {
        [Key]
        public int GravAxialAccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTran1AccelCorDto
    {
        [Key]
        public int GravTran1AccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTran2AccelCorDto
    {
        [Key]
        public int GravTran2AccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagAxialDrlstrCorDto
    {
        [Key]
        public int MagAxialDrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTran1DrlstrCorDto
    {
        [Key]
        public int MagTran1DrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagTran2DrlstrCorDto
    {
        [Key]
        public int MagTran2DrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSagIncCorDto
    {
        [Key]
        public int SagIncCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSagAziCorDto
    {
        [Key]
        public int SagAziCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportStnMagDeclUsedDto
    {
        [Key]
        public int StnMagDeclUsedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportStnGridCorUsedDto
    {
        [Key]
        public int StnGridCorUsedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDirSensorOffsetDto
    {
        [Key]
        public int DirSensorOffsetId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCorUsedDto
    {
        [Key]
        public int CorUsedId { get; set; }
        public OpsReportGravAxialAccelCorDto GravAxialAccelCor { get; set; }
        public OpsReportGravTran1AccelCorDto GravTran1AccelCor { get; set; }
        public OpsReportGravTran2AccelCorDto GravTran2AccelCor { get; set; }
        public OpsReportMagAxialDrlstrCorDto MagAxialDrlstrCor { get; set; }
        public OpsReportMagTran1DrlstrCorDto MagTran1DrlstrCor { get; set; }
        public OpsReportMagTran2DrlstrCorDto MagTran2DrlstrCor { get; set; }
        public OpsReportSagIncCorDto SagIncCor { get; set; }
        public OpsReportSagAziCorDto SagAziCor { get; set; }
        public OpsReportStnMagDeclUsedDto StnMagDeclUsed { get; set; }
        public OpsReportStnGridCorUsedDto StnGridCorUsed { get; set; }
        public OpsReportDirSensorOffsetDto DirSensorOffset { get; set; }
    }

    public class OpsReportMagTotalFieldCalcDto
    {
        [Key]
        public int MagTotalFieldCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagDipAngleCalcDto
    {
        [Key]
        public int MagDipAngleCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGravTotalFieldCalcDto
    {
        [Key]
        public int GravTotalFieldCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportValidDto
    {
        [Key]
        public int ValidId { get; set; }
        public OpsReportMagTotalFieldCalcDto MagTotalFieldCalc { get; set; }
        public OpsReportMagDipAngleCalcDto MagDipAngleCalc { get; set; }
        public OpsReportGravTotalFieldCalcDto GravTotalFieldCalc { get; set; }
    }

    public class OpsReportVarianceNNDto
    {
        [Key]
        public int VarianceNNId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceNEDto
    {
        [Key]
        public int OpsReportsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceNVertDto
    {
        [Key]
        public int VarianceNVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceEEDto
    {
        [Key]
        public int VarianceEEId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceEVertDto
    {
        [Key]
        public int VarianceEVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVarianceVertVertDto
    {
        [Key]
        public int VarianceVertVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBiasNDto
    {
        [Key]
        public int BiasNId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBiasEDto
    {
        [Key]
        public int BiasEId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBiasVertDto
    {
        [Key]
        public int BiasVertId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMatrixCovDto
    {
        [Key]
        public int MatrixCovId { get; set; }
        public OpsReportVarianceNNDto VarianceNN { get; set; }
        public OpsReportVarianceNEDto VarianceNE { get; set; }
        public OpsReportVarianceNVertDto VarianceNVert { get; set; }
        public OpsReportVarianceEEDto VarianceEE { get; set; }
        public OpsReportVarianceEVertDto VarianceEVert { get; set; }
        public OpsReportVarianceVertVertDto VarianceVertVert { get; set; }
        public OpsReportBiasNDto BiasN { get; set; }
        public OpsReportBiasEDto BiasE { get; set; }
        public OpsReportBiasVertDto BiasVert { get; set; }
    }


    public class OpsReportPumpDto
    {
        [Key]
        public int PumpId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRateStrokeDto
    {
        [Key]
        public int RateStrokeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresRecordedDto
    {
        [Key]
        public int PresRecordedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdBitDto
    {
        [Key]
        public int MdBitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportScrDto
    {
        [Key]
        public int ScrId { get; set; }
        public string DTim { get; set; }
        public OpsReportPumpDto Pump { get; set; }
        public string TypeScr { get; set; }
        public OpsReportRateStrokeDto RateStroke { get; set; }
        public OpsReportPresRecordedDto PresRecorded { get; set; }
        public OpsReportMdBitDto MdBit { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportPitDto
    {
        [Key]
        public int PitId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolPitDto
    {
        [Key]
        public int VolPitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDensFluidDto
    {
        [Key]
        public int DensFluidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPitVolumeDto
    {
        [Key]
        public int PitVolumeId { get; set; }
        public OpsReportPitDto Pit { get; set; }
        public string DTim { get; set; }
        public OpsReportVolPitDto VolPit { get; set; }
        public OpsReportDensFluidDto DensFluid { get; set; }
        public string DescFluid { get; set; }
        public OpsReportVisFunnelDto VisFunnel { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportVolTotMudStartDto
    {
        [Key]
        public int VolTotMudStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudDumpedDto
    {
        [Key]
        public int VolMudDumpedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudReceivedDto
    {
        [Key]
        public int VolMudReceivedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudReturnedDto
    {
        [Key]
        public int VolMudReturnedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostShakerSurfDto
    {
        [Key]
        public int VolLostShakerSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostMudCleanerSurfDto
    {
        [Key]
        public int VolLostMudCleanerSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostPitsSurfDto
    {
        [Key]
        public int VolLostPitsSurfId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostTrippingSurfDto
    {
        [Key]
        public int VolLostTrippingSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostOtherSurfDto
    {
        [Key]
        public int VolLostOtherSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolTotMudLostSurfDto
    {
        [Key]
        public int VolTotMudLostSurfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostCircHoleDto
    {
        [Key]
        public int VolLostCircHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostCsgHoleDto
    {
        [Key]
        public int VolLostCsgHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostCmtHoleDto
    {
        [Key]
        public int VolLostCmtHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostBhdCsgHoleDto
    {
        [Key]
        public int VolLostBhdCsgHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostAbandonHoleDto
    {
        [Key]
        public int VolLostAbandonHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolLostOtherHoleDto
    {
        [Key]
        public int VolLostOtherHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolTotMudLostHoleDto
    {
        [Key]
        public int VolTotMudLostHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMudLossesDto
    {
        [Key]
        public int MudLossesId { get; set; }
        public OpsReportVolLostShakerSurfDto VolLostShakerSurf { get; set; }
        public OpsReportVolLostMudCleanerSurfDto VolLostMudCleanerSurf { get; set; }
        public OpsReportVolLostPitsSurfDto VolLostPitsSurf { get; set; }
        public OpsReportVolLostTrippingSurfDto VolLostTrippingSurf { get; set; }
        public OpsReportVolLostOtherSurfDto VolLostOtherSurf { get; set; }
        public OpsReportVolTotMudLostSurfDto VolTotMudLostSurf { get; set; }
        public OpsReportVolLostCircHoleDto VolLostCircHole { get; set; }
        public OpsReportVolLostCsgHoleDto VolLostCsgHole { get; set; }
        public OpsReportVolLostCmtHoleDto VolLostCmtHole { get; set; }
        public OpsReportVolLostBhdCsgHoleDto VolLostBhdCsgHole { get; set; }
        public OpsReportVolLostAbandonHoleDto VolLostAbandonHole { get; set; }
        public OpsReportVolLostOtherHoleDto VolLostOtherHole { get; set; }
        public OpsReportVolTotMudLostHoleDto VolTotMudLostHole { get; set; }
    }

    public class OpsReportVolMudBuiltDto
    {
        [Key]
        public int VolMudBuiltId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudStringDto
    {
        [Key]
        public int VolMudStringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudCasingDto
    {
        [Key]
        public int VolMudCasingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudHoleDto
    {
        [Key]
        public int VolMudHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolMudRiserDto
    {
        [Key]
        public int VolMudRiserId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolTotMudEndDto
    {
        [Key]
        public int VolTotMudEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMudVolumeDto
    {
        [Key]
        public int MudVolumeId { get; set; }
        public OpsReportVolTotMudStartDto VolTotMudStart { get; set; }
        public OpsReportVolMudDumpedDto VolMudDumped { get; set; }
        public OpsReportVolMudReceivedDto VolMudReceived { get; set; }
        public OpsReportVolMudReturnedDto VolMudReturned { get; set; }
        public OpsReportMudLossesDto MudLosses { get; set; }
        public OpsReportVolMudBuiltDto VolMudBuilt { get; set; }
        public OpsReportVolMudStringDto VolMudString { get; set; }
        public OpsReportVolMudCasingDto VolMudCasing { get; set; }
        public OpsReportVolMudHoleDto VolMudHole { get; set; }
        public OpsReportVolMudRiserDto VolMudRiser { get; set; }
        public OpsReportVolTotMudEndDto VolTotMudEnd { get; set; }
    }

    public class OpsReportItemWtPerUnitDto
    {
        [Key]
        public int ItemWtPerUnitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPricePerUnitDto
    {
        [Key]
        public int PricePerUnitId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCostItemDto
    {
        [Key]
        public int CostItemId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMudInventoryDto
    {
        [Key]
        public int MudInventoryId { get; set; }
        public string Name { get; set; }
        public OpsReportItemWtPerUnitDto ItemWtPerUnit { get; set; }
        public OpsReportPricePerUnitDto PricePerUnit { get; set; }
        public string QtyStart { get; set; }
        public string QtyAdjustment { get; set; }
        public string QtyReceived { get; set; }
        public string QtyReturned { get; set; }
        public string QtyUsed { get; set; }
        public OpsReportCostItemDto CostItem { get; set; }
        public string QtyOnLocation { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportItemVolPerUnitDto
    {
        [Key]
        public int ItemVolPerUnitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBulkDto
    {
        [Key]
        public int BulkId { get; set; }
        public string Name { get; set; }
        public OpsReportItemVolPerUnitDto ItemVolPerUnit { get; set; }
        public OpsReportPricePerUnitDto PricePerUnit { get; set; }
        public string QtyStart { get; set; }
        public string QtyAdjustment { get; set; }
        public string QtyReceived { get; set; }
        public string QtyReturned { get; set; }
        public string QtyUsed { get; set; }
        public OpsReportCostItemDto CostItem { get; set; }
        public string QtyOnLocation { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportAnchorTensionDto
    {
        [Key]

        public string Uom { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAnchorAngleDto
    {
        [Key]
        public int AnchorAngleId { get; set; }
        public string Uom { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigHeadingDto
    {
        [Key]
        public int RigHeadingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigHeaveDto
    {
        [Key]
        public int RigHeaveId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigPitchAngleDto
    {
        [Key]
        public int RigPitchAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigRollAngleDto
    {
        [Key]
        public int RigRollAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRiserAngleDto
    {
        [Key]
        public int RiserAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRiserDirectionDto
    {
        [Key]
        public int RiserDirectionId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRiserTensionDto
    {
        [Key]
        public int RiserTensionId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVariableDeckLoadDto
    {
        [Key]
        public int VariableDeckLoadId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTotalDeckLoadDto
    {
        [Key]
        public int TotalDeckLoadId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGuideBaseAngleDto
    {
        [Key]
        public int GuideBaseAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBallJointAngleDto
    {
        [Key]
        public int BallJointAngleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBallJointDirectionDto
    {
        [Key]
        public int BallJointDirectionId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportOffsetRigDto
    {
        [Key]
        public int OffsetRigId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLoadLeg1Dto
    {
        [Key]
        public int LoadLeg1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLoadLeg2Dto
    {
        [Key]
        public int LoadLeg2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLoadLeg3Dto
    {
        [Key]
        public int LoadLeg3Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLoadLeg4Dto
    {
        [Key]
        public int LoadLeg4Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPenetrationLeg1Dto
    {
        [Key]
        public int PenetrationLeg1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPenetrationLeg2Dto
    {
        [Key]
        public int PenetrationLeg2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPenetrationLeg3Dto
    {
        [Key]
        public int PenetrationLeg3Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPenetrationLeg4Dto
    {
        [Key]
        public int PenetrationLeg4Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDispRigDto
    {
        [Key]
        public int DispRigId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMeanDraftDto
    {
        [Key]
        public int MeanDraftId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRigResponseDto
    {
        [Key]
        public int RigResponseId { get; set; }
        public List<OpsReportAnchorTensionDto> AnchorTension { get; set; }
        public List<OpsReportAnchorAngleDto> AnchorAngle { get; set; }
        public OpsReportRigHeadingDto RigHeading { get; set; }
        public OpsReportRigHeaveDto RigHeave { get; set; }
        public OpsReportRigPitchAngleDto RigPitchAngle { get; set; }
        public OpsReportRigRollAngleDto RigRollAngle { get; set; }
        public OpsReportRiserAngleDto RiserAngle { get; set; }
        public OpsReportRiserDirectionDto RiserDirection { get; set; }
        public OpsReportRiserTensionDto RiserTension { get; set; }
        public OpsReportVariableDeckLoadDto VariableDeckLoad { get; set; }
        public OpsReportTotalDeckLoadDto TotalDeckLoad { get; set; }
        public OpsReportGuideBaseAngleDto GuideBaseAngle { get; set; }
        public OpsReportBallJointAngleDto BallJointAngle { get; set; }
        public OpsReportBallJointDirectionDto BallJointDirection { get; set; }
        public OpsReportOffsetRigDto OffsetRig { get; set; }
        public OpsReportLoadLeg1Dto LoadLeg1 { get; set; }
        public OpsReportLoadLeg2Dto LoadLeg2 { get; set; }
        public OpsReportLoadLeg3Dto LoadLeg3 { get; set; }
        public OpsReportLoadLeg4Dto LoadLeg4 { get; set; }
        public OpsReportPenetrationLeg1Dto PenetrationLeg1 { get; set; }
        public OpsReportPenetrationLeg2Dto PenetrationLeg2 { get; set; }
        public OpsReportPenetrationLeg3Dto PenetrationLeg3 { get; set; }
        public OpsReportPenetrationLeg4Dto PenetrationLeg4 { get; set; }
        public OpsReportDispRigDto DispRig { get; set; }
        public OpsReportMeanDraftDto MeanDraft { get; set; }
    }

    public class OpsReportIdLinerDto
    {
        [Key]
        public int IdLinerId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLenStrokeDto
    {
        [Key]
        public int LenStrokeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPressureDto
    {
        [Key]
        public int PressureId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPcEfficiencyDto
    {
        [Key]
        public int PcEfficiencyId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPumpOutputDto
    {
        [Key]
        public int PumpOutputId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPumpOpDto
    {
        [Key]
        public int PumpOpId { get; set; }
        public string DTim { get; set; }
        public OpsReportPumpDto Pump { get; set; }
        public string TypeOperation { get; set; }
        public OpsReportIdLinerDto IdLiner { get; set; }
        public OpsReportLenStrokeDto LenStroke { get; set; }
        public OpsReportRateStrokeDto RateStroke { get; set; }
        public OpsReportPressureDto Pressure { get; set; }
        public OpsReportPcEfficiencyDto PcEfficiency { get; set; }
        public OpsReportPumpOutputDto PumpOutput { get; set; }
        public OpsReportMdBitDto MdBit { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportShakerDto
    {
        [Key]
        public int ShakerId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportHoursRunDto
    {
        [Key]
        public int HoursRunId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPcScreenCoveredDto
    {
        [Key]
        public int PcScreenCoveredId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMeshXDto
    {
        [Key]
        public int MeshXId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMeshYDto
    {
        [Key]
        public int MeshYId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCutPointDto
    {
        [Key]
        public int CutPointId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportShakerScreenDto
    {
        [Key]
        public int ShakerScreenId { get; set; }
        public string DTimStart { get; set; }
        public string DTimEnd { get; set; }
        public string NumDeck { get; set; }
        public OpsReportMeshXDto MeshX { get; set; }
        public OpsReportMeshYDto MeshY { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public OpsReportCutPointDto CutPoint { get; set; }
    }

    public class OpsReportMdHoleDto
    {
        [Key]
        public int MdHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportShakerOpDto
    {
        [Key]
        public int ShakerOpId { get; set; }
        public OpsReportShakerDto Shaker { get; set; }
        public OpsReportMdHoleDto MdHole { get; set; }
        public string DTim { get; set; }
        public OpsReportHoursRunDto HoursRun { get; set; }
        public OpsReportPcScreenCoveredDto PcScreenCovered { get; set; }
        public OpsReportShakerScreenDto ShakerScreen { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportDaysIncFreeDto
    {
        [Key]
        public int DaysIncFreeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimLostGrossDto
    {
        [Key]
        public int ETimLostGrossId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCostLostGrossDto
    {
        [Key]
        public int CostLostGrossId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportIncidentDto
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
        public OpsReportETimLostGrossDto ETimLostGross { get; set; }
        public OpsReportCostLostGrossDto CostLostGross { get; set; }
        public string ResponsibleCompany { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportPresLastCsgDto
    {
        [Key]
        public int PresLastCsgId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresStdPipeDto
    {
        [Key]
        public int PresStdPipeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresKellyHoseDto
    {
        [Key]
        public int PresKellyHoseId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresDiverterDto
    {
        [Key]
        public int PresDiverterId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresAnnularDto
    {
        [Key]
        public int PresAnnularId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresRamsDto
    {
        [Key]
        public int PresRamsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresChokeLineDto
    {
        [Key]
        public int PresChokeLineId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresChokeManDto
    {
        [Key]
        public int PresChokeManId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFluidDischargedDto
    {
        [Key]
        public int FluidDischargedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolCtgDischargedDto
    {
        [Key]
        public int VolCtgDischargedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolOilCtgDischargeDto
    {
        [Key]
        public int VolOilCtgDischargeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWasteDischargedDto
    {
        [Key]
        public int WasteDischargedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHseDto
    {
        [Key]
        public int HseId { get; set; }
        public OpsReportDaysIncFreeDto DaysIncFree { get; set; }
        public OpsReportIncidentDto Incident { get; set; }
        public string LastCsgPresTest { get; set; }
        public OpsReportPresLastCsgDto PresLastCsg { get; set; }
        public string LastBopPresTest { get; set; }
        public string NextBopPresTest { get; set; }
        public OpsReportPresStdPipeDto PresStdPipe { get; set; }
        public OpsReportPresKellyHoseDto PresKellyHose { get; set; }
        public OpsReportPresDiverterDto PresDiverter { get; set; }
        public OpsReportPresAnnularDto PresAnnular { get; set; }
        public OpsReportPresRamsDto PresRams { get; set; }
        public OpsReportPresChokeLineDto PresChokeLine { get; set; }
        public OpsReportPresChokeManDto PresChokeMan { get; set; }
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
        public OpsReportFluidDischargedDto FluidDischarged { get; set; }
        public OpsReportVolCtgDischargedDto VolCtgDischarged { get; set; }
        public OpsReportVolOilCtgDischargeDto VolOilCtgDischarge { get; set; }
        public OpsReportWasteDischargedDto WasteDischarged { get; set; }
        public string Comments { get; set; }
    }

    public class OpsReportTotalTimeDto
    {
        [Key]
        public int TotalTimeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPersonnelDto
    {
        [Key]
        public int PersonnelId { get; set; }
        public string Company { get; set; }
        public string TypeService { get; set; }
        public string NumPeople { get; set; }
        public OpsReportTotalTimeDto TotalTime { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportSupportCraftDto
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

    public class OpsReportBarometricPressureDto
    {
        [Key]
        public int BarometricPressureId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempSurfaceMnDto
    {
        [Key]
        public int TempSurfaceMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempSurfaceMxDto
    {
        [Key]
        public int TempSurfaceMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempWindChillDto
    {
        [Key]
        public int TempWindChillId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempseaDto
    {
        [Key]
        public int TempseaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVisibilityDto
    {
        [Key]
        public int VisibilityId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziWaveDto
    {
        [Key]
        public int AziWaveId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHtWaveDto
    {
        [Key]
        public int HtWaveId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPeriodWaveDto
    {
        [Key]
        public int PeriodWaveId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziWindDto
    {
        [Key]
        public int AziWindId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVelWindDto
    {
        [Key]
        public int VelWindId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAmtPrecipDto
    {
        [Key]
        public int AmtPrecipId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCeilingCloudDto
    {
        [Key]
        public int CeilingCloudId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCurrentSeaDto
    {
        [Key]
        public int CurrentSeaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziCurrentSeaDto
    {
        [Key]
        public int AziCurrentSeaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWeatherDto
    {
        [Key]
        public int WeatherId { get; set; }
        public string DTim { get; set; }
        public string Agency { get; set; }
        public OpsReportBarometricPressureDto BarometricPressure { get; set; }
        public string BeaufortScaleNumber { get; set; }
        public OpsReportTempSurfaceMnDto TempSurfaceMn { get; set; }
        public OpsReportTempSurfaceMxDto TempSurfaceMx { get; set; }
        public OpsReportTempWindChillDto TempWindChill { get; set; }
        public OpsReportTempseaDto Tempsea { get; set; }
        public OpsReportVisibilityDto Visibility { get; set; }
        public OpsReportAziWaveDto AziWave { get; set; }
        public OpsReportHtWaveDto HtWave { get; set; }
        public OpsReportPeriodWaveDto PeriodWave { get; set; }
        public OpsReportAziWindDto AziWind { get; set; }
        public OpsReportVelWindDto VelWind { get; set; }
        public string TypePrecip { get; set; }
        public OpsReportAmtPrecipDto AmtPrecip { get; set; }
        public string CoverCloud { get; set; }
        public OpsReportCeilingCloudDto CeilingCloud { get; set; }
        public OpsReportCurrentSeaDto CurrentSea { get; set; }
        public OpsReportAziCurrentSeaDto AziCurrentSea { get; set; }
        public string Comments { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportCostDayDto
    {
        [Key]
        public int CostDayId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCostDayMudDto
    {
        [Key]
        public int CostDayMudId { get; set; }
        public string Currency { get; set; }

        public string Text { get; set; }
    }



    public class OpsReportTvdCsgLastDto
    {
        [Key]
        public int TvdCsgLastId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdLotDto
    {
        [Key]
        public int TvdLotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresLotEmwDto
    {
        [Key]
        public int PresLotEmwId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresKickTolDto
    {
        [Key]
        public int PresKickTolId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVolKickTolDto
    {
        [Key]
        public int VolKickTolId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMaaspDto
    {
        [Key]
        public int MaaspId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDensityDto
    {
        [Key]
        public int DensityId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportVisFunnelDto
    {
        [Key]
        public int VisFunnelId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempVisDto
    {
        [Key]
        public int TempVisId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPvDto
    {
        [Key]
        public int ReportPvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportYpDto
    {
        [Key]
        public int YpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGel10SecDto
    {
        [Key]
        public int Gel10SecId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGel10MinDto
    {
        [Key]
        public int Gel10MinId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportGel30MinDto
    {
        [Key]
        public int Gel30MinId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFilterCakeLtlpDto
    {
        [Key]
        public int FilterCakeLtlpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFiltrateLtlpDto
    {
        [Key]
        public int FiltrateLtlpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempHthpDto
    {
        [Key]
        public int TempHthpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresHthpDto
    {
        [Key]
        public int PresHthpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFiltrateHthpDto
    {
        [Key]
        public int FiltrateHthpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFilterCakeHthpDto
    {
        [Key]
        public int FilterCakeHthpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolidsPcDto
    {
        [Key]
        public int SolidsPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWaterPcDto
    {
        [Key]
        public int WaterPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportOilPcDto
    {
        [Key]
        public int OilPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSandPcDto
    {
        [Key]
        public int SandPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolidsLowGravPcDto
    {
        [Key]
        public int SolidsLowGravPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolidsCalcPcDto
    {
        [Key]
        public int SolidsCalcPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportBaritePcDto
    {
        [Key]
        public int BaritePcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLcmDto
    {
        [Key]
        public int LcmId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMbtDto
    {
        [Key]
        public int MbtId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempPhDto
    {
        [Key]
        public int TempPhId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPmDto
    {
        [Key]
        public int PmId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPmFiltrateDto
    {
        [Key]
        public int PmFiltrateId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMfDto
    {
        [Key]
        public int MfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAlkalinityP1Dto
    {
        [Key]
        public int AlkalinityP1Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAlkalinityP2Dto
    {
        [Key]
        public int AlkalinityP2Id { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportChlorideDto
    {
        [Key]
        public int ChlorideId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCalciumDto
    {
        [Key]
        public int MagnesiumId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMagnesiumDto
    {
        [Key]
        public int MagnesiumId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempRheomDto
    {
        [Key]
        public int TempRheomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresRheomDto
    {
        [Key]
        public int PresRheomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRheometerDto
    {
        [Key]
        public int RheometerId { get; set; }
        public OpsReportTempRheomDto TempRheom { get; set; }
        public OpsReportPresRheomDto PresRheom { get; set; }
        public string Vis3Rpm { get; set; }
        public string Vis6Rpm { get; set; }
        public string Vis100Rpm { get; set; }
        public string Vis200Rpm { get; set; }
        public string Vis300Rpm { get; set; }
        public string Vis600Rpm { get; set; }
        public string Uid { get; set; }
    }

    public class OpsReportBrinePcDto
    {
        [Key]
        public int BrinePcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLimeDto
    {
        [Key]
        public int LimeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportElectStabDto
    {
        [Key]
        public int ElectStabId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCalciumChlorideDto
    {
        [Key]
        public int CalciumChlorideId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolidsHiGravPcDto
    {
        [Key]
        public int SolidsHiGravPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPolymerDto
    {
        [Key]
        public int PolymerId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSolCorPcDto
    {
        [Key]
        public int SolCorPcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportOilCtgDto
    {
        [Key]
        public int OilCtgId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHardnessCaDto
    {
        [Key]
        public int HardnessCaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSulfideDto
    {
        [Key]
        public int SulfideId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFluidDto
    {
        [Key]
        public string Uid { get; set; }
        public string Type { get; set; }
        public string LocationSample { get; set; }
        public OpsReportDensityDto Density { get; set; }
        public OpsReportVisFunnelDto VisFunnel { get; set; }
        public OpsReportTempVisDto TempVis { get; set; }
        public OpsReportPvDto Pv { get; set; }
        public OpsReportYpDto Yp { get; set; }
        public OpsReportGel10SecDto Gel10Sec { get; set; }
        public OpsReportGel10MinDto Gel10Min { get; set; }
        public OpsReportGel30MinDto Gel30Min { get; set; }
        public OpsReportFilterCakeLtlpDto FilterCakeLtlp { get; set; }
        public OpsReportFiltrateLtlpDto FiltrateLtlp { get; set; }
        public OpsReportTempHthpDto TempHthp { get; set; }
        public OpsReportPresHthpDto PresHthp { get; set; }
        public OpsReportFiltrateHthpDto FiltrateHthp { get; set; }
        public OpsReportFilterCakeHthpDto FilterCakeHthp { get; set; }
        public OpsReportSolidsPcDto SolidsPc { get; set; }
        public OpsReportWaterPcDto WaterPc { get; set; }
        public OpsReportOilPcDto OilPc { get; set; }
        public OpsReportSandPcDto SandPc { get; set; }
        public OpsReportSolidsLowGravPcDto SolidsLowGravPc { get; set; }
        public OpsReportSolidsCalcPcDto SolidsCalcPc { get; set; }
        public OpsReportBaritePcDto BaritePc { get; set; }
        public OpsReportLcmDto Lcm { get; set; }
        public OpsReportMbtDto Mbt { get; set; }
        public string Ph { get; set; }
        public OpsReportTempPhDto TempPh { get; set; }
        public OpsReportPmDto Pm { get; set; }
        public OpsReportPmFiltrateDto PmFiltrate { get; set; }
        public OpsReportMfDto Mf { get; set; }
        public OpsReportAlkalinityP1Dto AlkalinityP1 { get; set; }
        public OpsReportAlkalinityP2Dto AlkalinityP2 { get; set; }
        public OpsReportChlorideDto Chloride { get; set; }
        public OpsReportCalciumDto Calcium { get; set; }
        public OpsReportMagnesiumDto Magnesium { get; set; }
        public List<OpsReportRheometerDto> Rheometer { get; set; }
        public OpsReportBrinePcDto BrinePc { get; set; }
        public OpsReportLimeDto Lime { get; set; }
        public OpsReportElectStabDto ElectStab { get; set; }
        public OpsReportCalciumChlorideDto CalciumChloride { get; set; }
        public string Company { get; set; }
        public string Engineer { get; set; }
        public string Asg { get; set; }
        public OpsReportSolidsHiGravPcDto SolidsHiGravPc { get; set; }
        public OpsReportPolymerDto Polymer { get; set; }
        public string PolyType { get; set; }
        public OpsReportSolCorPcDto SolCorPc { get; set; }
        public OpsReportOilCtgDto OilCtg { get; set; }
        public OpsReportHardnessCaDto HardnessCa { get; set; }
        public OpsReportSulfideDto Sulfide { get; set; }
        public string Comments { get; set; }

    }
    public class OpsReportRigDto
    {
        [Key]
        public int OpsReportRigId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdHoleStartDto
    {
        [Key]
        public int MdHoleStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdHoleStartDto
    {
        [Key]
        public int TvdHoleStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdHoleEndDto
    {
        [Key]
        public int MdHoleEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdHoleEndDto
    {
        [Key]
        public int TvdHoleEndId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportActivityDto
    {
        [Key]
        public string Uid { get; set; }
        public string DTimStart { get; set; }
        public string DTimEnd { get; set; }
        public OpsReportDurationDto Duration { get; set; }
        public string Phase { get; set; }
        public string ActivityCode { get; set; }
        public string DetailActivity { get; set; }
        public string TypeActivityClass { get; set; }
        public OpsReportMdHoleStartDto MdHoleStart { get; set; }
        public OpsReportTvdHoleStartDto TvdHoleStart { get; set; }
        public OpsReportMdHoleEndDto MdHoleEnd { get; set; }
        public OpsReportTvdHoleEndDto TvdHoleEnd { get; set; }
        public OpsReportMdBitStartDto MdBitStart { get; set; }
        public OpsReportMdBitEndDto MdBitEnd { get; set; }
        public string State { get; set; }
        public string Operator { get; set; }
        public string Optimum { get; set; }
        public string Productive { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

    }


    public class OpsReportTubularDto
    {
        [Key]
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWtMudDto
    {
        [Key]
        [Required]

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportETimOpBitDto
    {
        [Key]
        [Required]
        public int ETimOpBitId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdHoleStopDto
    {
        [Key]
        [Required]
        public int MdHoleStopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }



    public class OpsReportHkldRotDto
    {
        [Key]
        [Required]
        public int HkldRotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportOverPullDto
    {
        [Key]
        [Required]
        public int OverPullId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportSlackOffDto
    {
        [Key]
        [Required]
        public int SlackOffId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHkldUpDto
    {
        [Key]
        [Required]
        public int HkldUpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportHkldDnDto
    {
        [Key]
        [Required]
        public int HkldDnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqOnBotAvDto
    {
        [Key]
        [Required]
        public int TqOnBotAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqOnBotMxDto
    {
        [Key]
        [Required]
        public int TqOnBotMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqOnBotMnDto
    {
        [Key]
        [Required]
        public int TqOnBotMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqOffBotAvDto
    {
        [Key]
        [Required]
        public int TqOffBotAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTqDhAvDto
    {
        [Key]
        [Required]
        public int TqDhAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWtAboveJarDto
    {
        [Key]
        [Required]
        public int WtAboveJarId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWtBelowJarDto
    {
        [Key]
        [Required]
        public int WtBelowJarId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class OpsReportFlowratePumpDto
    {
        [Key]
        [Required]
        public int FlowratePumpId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPowBitDto
    {
        [Key]
        [Required]
        public int PowBitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    
    public class OpsReportVelNozzleAvDto
    {
        [Key]
        [Required]
        public int VelNozzleAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    
    public class OpsReportPresDropBitDto
    {
        [Key]
        [Required]
        public int PresDropBitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

   
    public class OpsReportCTimHoldDto
    {
        [Key]
        [Required]
        public int CTimHoldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

   
    public class OpsReportCTimSteeringDto
    {
        [Key]
        [Required]
        public int CTimSteeringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

   
    public class OpsReportCTimDrillRotDto
    {
        [Key]
        [Required]
        public int CTimDrillRotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimDrillSlidDto
    {
        [Key]
        [Required]
        public int CTimDrillSlidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimCircDto
    {
        [Key]
        [Required]
        public int CTimCircId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportCTimReamDto
    {
        [Key]
        [Required]
        public int CTimReamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistDrillRotDto
    {
        [Key]
        [Required]
        public int DistDrillRotId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistDrillSlidDto
    {
        [Key]
        [Required]
        public int DistDrillSlidId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistReamDto
    {
        [Key]
        [Required]
        public int DistReamId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistHoldDto
    {

        [Key]
        [Required]
        public int DistHoldId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDistSteeringDto
    {
        [Key]
        [Required]
        public int DistSteeringId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRpmAvDto
    {
        [Key]
        [Required]
        public int RpmAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRpmMxDto
    {
        [Key]
        [Required]
        public int RpmMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRpmMnDto
    {
        [Key]
        [Required]
        public int RpmMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRpmAvDhDto
    {
        [Key]
        [Required]
        public int RpmAvDhId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRopMxDto
    {
        [Key]
        [Required]
        public int RopMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRopMnDto
    {
        [Key]
        [Required]
        public int RopMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWobAvDto
    {
        [Key]
        [Required]
        public int WobAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWobMxDto
    {
        [Key]
        [Required]
        public int WobMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRopAvDto
    {
        [Key]
        [Required]
        public int RopAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWobMnDto
    {
        [Key]
        [Required]
        public int WobMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportWobAvDhDto
    {
        [Key]
        [Required]
        public int WobAvDhId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziTopDto
    {
        [Key]
        [Required]
        public int AziTopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziBottomDto
    {
        [Key]
        [Required]
        public int AziBottomId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclStartDto
    {
        [Key]
        [Required]
        public int InclStartId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclMxDto
    {
        [Key]
        [Required]
        public int InclMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclMnDto
    {
        [Key]
        [Required]
        public int InclMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclStopDto
    {
        [Key]
        [Required]
        public int InclStopId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTempMudDhMxDto
    {
        [Key]
        [Required]
        public int TempMudDhMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportPresPumpAvDto
    {
        [Key]
        [Required]
        public int PresPumpAvId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportFlowrateBitDto
    {
        [Key]
        [Required]
        public int FlowrateBitId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDrillingParamDto
    {
        [Key]
        [Required]
        public string Uid { get; set; }
        public OpsReportETimOpBitDto ETimOpBit { get; set; }

        public OpsReportMdHoleStartDto MdHoleStart { get; set; }

        public OpsReportMdHoleStopDto MdHoleStop { get; set; }

        public OpsReportTubularDto Tubular { get; set; }

        public OpsReportHkldRotDto HkldRot { get; set; }

        public OpsReportOverPullDto OverPull { get; set; }

        public OpsReportSlackOffDto SlackOff { get; set; }

        public OpsReportHkldUpDto HkldUp { get; set; }

        public OpsReportHkldDnDto HkldDn { get; set; }

        public OpsReportTqOnBotAvDto TqOnBotAv { get; set; }

        public OpsReportTqOnBotMxDto TqOnBotMx { get; set; }

        public OpsReportTqOnBotMnDto TqOnBotMn { get; set; }

        public OpsReportTqOffBotAvDto TqOffBotAv { get; set; }

        public OpsReportTqDhAvDto TqDhAv { get; set; }

        public OpsReportWtAboveJarDto WtAboveJar { get; set; }

        public OpsReportWtBelowJarDto WtBelowJar { get; set; }

        public OpsReportWtMudDto WtMud { get; set; }

        public OpsReportFlowratePumpDto FlowratePump { get; set; }

        public OpsReportPowBitDto PowBit { get; set; }

        public OpsReportVelNozzleAvDto VelNozzleAv { get; set; }

        public OpsReportPresDropBitDto PresDropBit { get; set; }

        public OpsReportCTimHoldDto CTimHold { get; set; }

        public OpsReportCTimSteeringDto CTimSteering { get; set; }

        public OpsReportCTimDrillRotDto CTimDrillRot { get; set; }

        public OpsReportCTimDrillSlidDto CTimDrillSlid { get; set; }

        public OpsReportCTimCircDto CTimCirc { get; set; }

        public OpsReportCTimReamDto CTimReam { get; set; }

        public OpsReportDistDrillRotDto DistDrillRot { get; set; }

        public OpsReportDistDrillSlidDto DistDrillSlid { get; set; }

        public OpsReportDistReamDto DistReam { get; set; }

        public OpsReportDistHoldDto DistHold { get; set; }

        public OpsReportDistSteeringDto DistSteering { get; set; }

        public OpsReportRpmAvDto RpmAv { get; set; }

        public OpsReportRpmMxDto RpmMx { get; set; }

        public OpsReportRpmMnDto RpmMn { get; set; }

        public OpsReportRpmAvDhDto RpmAvDh { get; set; }

        public OpsReportRopAvDto RopAv { get; set; }

        public OpsReportRopMxDto RopMx { get; set; }

        public OpsReportRopMnDto RopMn { get; set; }

        public OpsReportWobAvDto WobAv { get; set; }

        public OpsReportWobMxDto WobMx { get; set; }

        public OpsReportWobMnDto WobMn { get; set; }

        public OpsReportWobAvDhDto WobAvDh { get; set; }

        public string ReasonTrip { get; set; }

        public string ObjectiveBha { get; set; }

        public OpsReportAziTopDto AziTop { get; set; }

        public OpsReportAziBottomDto AziBottom { get; set; }

        public OpsReportInclStartDto InclStart { get; set; }

        public OpsReportInclMxDto InclMx { get; set; }

        public OpsReportInclMnDto InclMn { get; set; }

        public OpsReportInclStopDto InclStop { get; set; }

        public OpsReportTempMudDhMxDto TempMudDhMx { get; set; }

        public OpsReportPresPumpAvDto PresPumpAv { get; set; }

        public OpsReportFlowrateBitDto FlowrateBit { get; set; }

        public string Comments { get; set; }
    }

    public class OpsReportWellCRSDto
    {
        [Key]
        public int WellCRSId { get; set; }
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLatitudeDto
    {
        [Key]
        public int LatitudeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLongitudeDto
    {
        [Key]
        public int LongitudeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportProjectedXDto
    {
        [Key]
        public int ProjectedXId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportProjectedYDto
    {
        [Key]
        public int ProjectedYId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportLocationDto
    {
        [Key]
        public string Uid { get; set; }
        public OpsReportWellCRSDto WellCRS { get; set; }
        public OpsReportLatitudeDto Latitude { get; set; }
        public OpsReportLongitudeDto Longitude { get; set; }
        public OpsReportProjectedXDto ProjectedX { get; set; }
        public OpsReportProjectedYDto ProjectedY { get; set; }
    }

    public class OpsReportMdDto
    {
        [Key]
        public int MdId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTvdDto
    {
        [Key]
        public int TvdId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportInclDto
    {
        [Key]
        public int InclId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportAziDto
    {
        [Key]
        public int AziId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportTrajectoryStationDto
    {
        [Key]
        public string Uid { get; set; }
        public string DTimStn { get; set; }
        public string TypeTrajStation { get; set; }
        public OpsReportMdDto Md { get; set; }
        public OpsReportTvdDto Tvd { get; set; }
        public OpsReportInclDto Incl { get; set; }
        public OpsReportAziDto Azi { get; set; }
        public OpsReportMtfDto Mtf { get; set; }
        public OpsReportGtfDto Gtf { get; set; }
        public OpsReportDispNsDto DispNs { get; set; }
        public OpsReportDispEwDto DispEw { get; set; }
        public OpsReportVertSectDto VertSect { get; set; }
        public OpsReportDlsDto Dls { get; set; }
        public OpsReportRateTurnDto RateTurn { get; set; }
        public OpsReportRateBuildDto RateBuild { get; set; }
        public OpsReportMdDeltaDto MdDelta { get; set; }
        public OpsReportTvdDeltaDto TvdDelta { get; set; }
        public string ModelToolError { get; set; }
        public OpsReportGravTotalUncertDto GravTotalUncert { get; set; }
        public OpsReportDipAngleUncertDto DipAngleUncert { get; set; }
        public OpsReportMagTotalUncertDto MagTotalUncert { get; set; }
        public string GravAccelCorUsed { get; set; }
        public string MagXAxialCorUsed { get; set; }
        public string SagCorUsed { get; set; }
        public string MagDrlstrCorUsed { get; set; }
        public string StatusTrajStation { get; set; }
        public OpsReportRawDataDto RawData { get; set; }
        public OpsReportCorUsedDto CorUsed { get; set; }
        public OpsReportValidDto Valid { get; set; }
        public OpsReportMatrixCovDto MatrixCov { get; set; }
        public List<OpsReportLocationDto> Location { get; set; }

    }

    public class OpsReportDistDrillDto
    {
        [Key]
        public int DistDrillId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportRopCurrentDto
    {
        [Key]
        public int RopCurrentId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDiaHoleDto
    {
        [Key]
        public int DiaHoleId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDiaCsgLastDto
    {
        [Key]
        public int DiaCsgLastId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportMdCsgLastDto
    {
        [Key]
        public int MdCsgLastId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class OpsReportDto
    {
        [Key]
        public int OpsReportId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public OpsReportETimCircDto ETimCirc { get; set; }
        public OpsReportDrillingParamDto OpsReportDrillingParam { get; set; }
        public OpsReportRigDto Rig { get; set; }
        public string DTim { get; set; }
        public OpsReportETimStartDto ETimStart { get; set; }
        public OpsReportETimSpudDto ETimSpud { get; set; }
        public OpsReportETimLocDto ETimLoc { get; set; }
        public OpsReportMdReportDto MdReport { get; set; }
        public OpsReportTvdReportDto TvdReport { get; set; }
        public OpsReportDistDrillDto DistDrill { get; set; }
        public OpsReportETimDrillDto ETimDrill { get; set; }
        public OpsReportMdPlannedDto MdPlanned { get; set; }
        public OpsReportRopAvDto RopAv { get; set; }
        public OpsReportRopCurrentDto RopCurrent { get; set; }
        public string Supervisor { get; set; }
        public string Engineer { get; set; }
        public string Geologist { get; set; }
        public OpsReportETimDrillRotDto ETimDrillRot { get; set; }
        public OpsReportETimDrillSlidDto ETimDrillSlid { get; set; }
        public OpsReportETimReamDto ETimReam { get; set; }
        public OpsReportETimHoldDto ETimHold { get; set; }
        public OpsReportETimSteeringDto ETimSteering { get; set; }
        public OpsReportDistDrillRotDto DistDrillRot { get; set; }
        public OpsReportDistDrillSlidDto DistDrillSlid { get; set; }
        public OpsReportDistReamDto DistReam { get; set; }
        public OpsReportDistHoldDto DistHold { get; set; }
        public OpsReportDistSteeringDto DistSteering { get; set; }
        public string NumPob { get; set; }
        public string NumContract { get; set; }
        public string NumOperator { get; set; }
        public string NumService { get; set; }
        public OpsReportActivityDto Activity { get; set; }
        public OpsReportDrillingParamDto DrillingParams { get; set; }
        public List<OpsReportDayCostDto> DayCost { get; set; }
        public OpsReportTrajectoryStationDto TrajectoryStation { get; set; }
        public OpsReportFluidDto Fluid { get; set; }
        public List<OpsReportScrDto> Scr { get; set; }
        public List<OpsReportPitVolumeDto> PitVolume { get; set; }
        public OpsReportMudVolumeDto MudVolume { get; set; }
        public OpsReportMudInventoryDto MudInventory { get; set; }
        public OpsReportBulkDto Bulk { get; set; }
        public OpsReportRigResponseDto RigResponse { get; set; }
        public OpsReportPumpOpDto PumpOp { get; set; }
        public OpsReportShakerOpDto ShakerOp { get; set; }
        public OpsReportHseDto Hse { get; set; }
        public List<OpsReportPersonnelDto> Personnel { get; set; }
        public OpsReportSupportCraftDto SupportCraft { get; set; }
        public OpsReportWeatherDto Weather { get; set; }
        public string NumAFE { get; set; }
        public OpsReportCostDayDto CostDay { get; set; }
        public OpsReportCostDayMudDto CostDayMud { get; set; }
        public OpsReportDiaHoleDto DiaHole { get; set; }
        public string ConditionHole { get; set; }
        public string Lithology { get; set; }
        public string NameFormation { get; set; }
        public OpsReportDiaCsgLastDto DiaCsgLast { get; set; }
        public OpsReportMdCsgLastDto MdCsgLast { get; set; }
        public OpsReportTvdCsgLastDto TvdCsgLast { get; set; }
        public OpsReportTvdLotDto TvdLot { get; set; }
        public OpsReportPresLotEmwDto PresLotEmw { get; set; }
        public OpsReportPresKickTolDto PresKickTol { get; set; }
        public OpsReportVolKickTolDto VolKickTol { get; set; }
        public OpsReportMaaspDto Maasp { get; set; }
        public TubularDto Tubular { get; set; }
        public string Sum24Hr { get; set; }
        public string Forecast24Hr { get; set; }
        public string StatusCurrent { get; set; }
        public OpsReportsCommonDataDto CommonData { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }
    }

    public class OpsReportsCommonDataDto
    {
        [Key]
        public int OpsReportsCommonDataid { get; set; }
        public string ItemState { get; set; }

        public string Comments { get; set; }
    }
    

}


