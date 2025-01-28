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
	public class MudLogStartMdDto
	{
		[Key]
		public int StartMdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEndMdDto
	{
		[Key]
		public int EndMdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
 



	public class MudLogCommonTimeDto
	{
		[Key]
		public int CommonTimeId { get; set; }
		public string DTimCreation { get; set; }
		public string DTimLastChange { get; set; }
	}

	public class MudLogMdTopDto
	{
		[Key]
		[Required]
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMdBottomDto
	{
		[Key]
		public int MdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class MudLogParameterDto
	{
		[Key]
		public int ParameterId { get; set; }
		public string Type { get; set; }
		public MudLogMdTopDto MdTop { get; set; }
		public MudLogMdBottomDto MdBottom { get; set; }
		public string Text { get; set; }
		public MudLogCommonTimeDto CommonTime { get; set; }
		public string Uid { get; set; }
		public MudLogForceDto Force { get; set; }
	}

	public class MudLogForceDto
	{
		[Key]
		public int ForceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogTvdTopDto
	{
		[Key]
		public int TvdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogTvdBaseDto
	{
		[Key]
		public int TvdBaseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogRopAvDto
	{
		[Key]
		public int RopAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogRopMnDto
	{
		[Key]
		public int RopMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogRopMxDto
	{
		[Key]
		public int RopMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogWobAvDto
	{
		[Key]
		public int WobAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogTqAvDto
	{
		[Key]
		public int TqAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogRpmAvDto
	{
		[Key]
		public int RpmAvId { get; set; }

		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogWtMudAvDto
	{
		[Key]
		public int WtMudAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEcdTdAvDto
	{
		[Key]
		public int EcdTdAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogDensShaleDto
	{
		[Key]
		public int DensShaleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogAbundanceDto
	{
		[Key]
		public int AbundanceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogQualifierDto
	{
		[Key]
		public int QualifierId { get; set; }
		public string Type { get; set; }
		public MudLogAbundanceDto Abundance { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class MudLogLithologyDto
	{
		[Key]
		public int LithologyId { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public string Color { get; set; }
		public string Texture { get; set; }
		public string Hardness { get; set; }
		public string SizeGrain { get; set; }
		public string Roundness { get; set; }
		public string Sorting { get; set; }
		public string MatrixCement { get; set; }
		public string PorosityVisible { get; set; }
		public string Permeability { get; set; }
		public MudLogDensShaleDto DensShale { get; set; }
		public MudLogQualifierDto Qualifier { get; set; }
		public string Uid { get; set; }
	}

	public class MudLogStainPcDto
	{
		[Key]
		public int StainPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNatFlorPcDto
	{
		[Key]
		public int NatFlorPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogShowDto
	{
		[Key]
		public int ShowId { get; set; }
		public string ShowRat { get; set; }
		public string StainColor { get; set; }
		public string StainDistr { get; set; }
		public MudLogStainPcDto StainPc { get; set; }
		public string NatFlorColor { get; set; }
		public MudLogNatFlorPcDto NatFlorPc { get; set; }
		public string NatFlorLevel { get; set; }
		public string NatFlorDesc { get; set; }
		public string CutColor { get; set; }
		public string CutSpeed { get; set; }
		public string CutStrength { get; set; }
		public string CutForm { get; set; }
		public string CutLevel { get; set; }
		public string CutFlorColor { get; set; }
		public string CutFlorSpeed { get; set; }
		public string CutFlorStrength { get; set; }
		public string CutFlorForm { get; set; }
		public string CutFlorLevel { get; set; }
		public string ResidueColor { get; set; }
		public string ShowDesc { get; set; }
		public string ImpregnatedLitho { get; set; }
		public string Odor { get; set; }
	}

	public class MudLogWtMudInDto
	{
		[Key]
		public int WtMudInId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogWtMudOutDto
	{
		[Key]
		public int WtMudOutId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogETimChromCycleDto
	{
		[Key]
		public int ETimChromCycleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMethAvDto
	{
		[Key]
		public int MethAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMethMnDto
	{
		[Key]
		public int MethMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMethMxDto
	{
		[Key]
		public int MethMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEthAvDto
	{
		[Key]
		public int EthAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEthMnDto
	{
		[Key]
		public int EthMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEthMxDto
	{
		[Key]
		public int EthMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogPropAvDto
	{
		[Key]
		public int PropAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogPropMnDto
	{
		[Key]
		public int PropMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogPropMxDto
	{
		[Key]
		public int PropMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIbutAvDto
	{
		[Key]
		public int IbutAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIbutMnDto
	{
		[Key]
		public int IbutMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIbutMxDto
	{
		[Key]
		public int IbutMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNbutAvDto
	{
		[Key]
		public int NbutAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNbutMnDto
	{
		[Key]
		public int NbutMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNbutMxDto
	{
		[Key]
		public int NbutMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIpentAvDto
	{
		[Key]
		public int IpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIpentMnDto
	{
		[Key]
		public int IpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIpentMxDto
	{
		[Key]
		public int IpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNpentAvDto
	{
		[Key]
		public int NpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNpentMnDto
	{
		[Key]
		public int NpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNpentMxDto
	{
		[Key]
		public int NpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEpentAvDto
	{
		[Key]
		public int EpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEpentMnDto
	{
		[Key]
		public int EpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEpentMxDto
	{
		[Key]
		public int EpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIhexAvDto
	{
		[Key]
		public int IhexAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIhexMnDto
	{
		[Key]
		public int IhexMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIhexMxDto
	{
		[Key]
		public int IhexMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNhexAvDto
	{
		[Key]
		public int NhexAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNhexMnDto
	{
		[Key]
		public int NhexMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNhexMxDto
	{
		[Key]
		public int NhexMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCo2AvDto
	{
		[Key]
		public int Co2AvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCo2MnDto
	{
		[Key]
		public int Co2MnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCo2MxDto
	{
		[Key]
		public int Co2MxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogH2sAvDto
	{
		[Key]
		public int H2sAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogH2sMnDto
	{
		[Key]
		public int H2sMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogH2sMxDto
	{
		[Key]
		public int H2sMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogAcetyleneDto
	{
		[Key]
		public int AcetyleneId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogChromatographDto
	{
		[Key]
		public int ChromatographId { get; set; }
		public string DTim { get; set; }
		public MudLogMdTopDto MdTop { get; set; }
		public MudLogMdBottomDto MdBottom { get; set; }
		public MudLogWtMudInDto WtMudIn { get; set; }
		public MudLogWtMudOutDto WtMudOut { get; set; }
		public string ChromType { get; set; }
		public MudLogETimChromCycleDto ETimChromCycle { get; set; }
		public string ChromIntRpt { get; set; }
		public MudLogMethAvDto MethAv { get; set; }
		public MudLogMethMnDto MethMn { get; set; }
		public MudLogMethMxDto MethMx { get; set; }
		public MudLogEthAvDto EthAv { get; set; }
		public MudLogEthMnDto EthMn { get; set; }
		public MudLogEthMxDto EthMx { get; set; }
		public MudLogPropAvDto PropAv { get; set; }
		public MudLogPropMnDto PropMn { get; set; }
		public MudLogPropMxDto PropMx { get; set; }
		public MudLogIbutAvDto IbutAv { get; set; }
		public MudLogIbutMnDto IbutMn { get; set; }
		public MudLogIbutMxDto IbutMx { get; set; }
		public MudLogNbutAvDto NbutAv { get; set; }
		public MudLogNbutMnDto NbutMn { get; set; }
		public MudLogNbutMxDto NbutMx { get; set; }
		public MudLogIpentAvDto IpentAv { get; set; }
		public MudLogIpentMnDto IpentMn { get; set; }
		public MudLogIpentMxDto IpentMx { get; set; }
		public MudLogNpentAvDto NpentAv { get; set; }
		public MudLogNpentMnDto NpentMn { get; set; }
		public MudLogNpentMxDto NpentMx { get; set; }
		public MudLogEpentAvDto EpentAv { get; set; }
		public MudLogEpentMnDto EpentMn { get; set; }
		public MudLogEpentMxDto EpentMx { get; set; }
		public MudLogIhexAvDto IhexAv { get; set; }
		public MudLogIhexMnDto IhexMn { get; set; }
		public MudLogIhexMxDto IhexMx { get; set; }
		public MudLogNhexAvDto NhexAv { get; set; }
		public MudLogNhexMnDto NhexMn { get; set; }
		public MudLogNhexMxDto NhexMx { get; set; }
		public MudLogCo2AvDto Co2Av { get; set; }
		public MudLogCo2MnDto Co2Mn { get; set; }
		public MudLogCo2MxDto Co2Mx { get; set; }
		public MudLogH2sAvDto H2sAv { get; set; }
		public MudLogH2sMnDto H2sMn { get; set; }
		public MudLogH2sMxDto H2sMx { get; set; }
		public MudLogAcetyleneDto Acetylene { get; set; }
	}

	public class MudLogGasAvDto
	{
		[Key]
		public int GasAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasPeakDto
	{
		[Key]
		public int GasPeakId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasBackgndDto
	{
		[Key]
		public int GasBackgndId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasConAvDto
	{
		[Key]
		public int GasConAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasConMxDto
	{
		[Key]
		public int GasConMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasTripDto
	{
		[Key]
		public int GasTripId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMudGasDto
	{
		[Key]
		public int MudGasId { get; set; }
		public MudLogGasAvDto GasAv { get; set; }
		public MudLogGasPeakDto GasPeak { get; set; }
		public string GasPeakType { get; set; }
		public MudLogGasBackgndDto GasBackgnd { get; set; }
		public MudLogGasConAvDto GasConAv { get; set; }
		public MudLogGasConMxDto GasConMx { get; set; }
		public MudLogGasTripDto GasTrip { get; set; }
	}

	public class MudLogDensBulkDto
	{
		[Key]
		public int DensBulkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCalciteDto
	{
		[Key]
		public int CalciteId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogDolomiteDto
	{
		[Key]
		public int DolomiteId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCecDto
	{
		[Key]
		public int CecId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCalcStabDto
	{
		[Key]
		public int CalcStabId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogLithostratigraphicDto
	{
		[Key]
		public int LithostratigraphicId { get; set; }
		public string Kind { get; set; }

		public string Text { get; set; }
	}

	public class MudLogChronostratigraphicDto
	{
		[Key]
		public int ChronostratigraphicId { get; set; }
		public string Kind { get; set; }

		public string Text { get; set; }
	}

	public class MudLogSizeMnDto
	{
		[Key]
		public int SizeMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogSizeMxDto
	{
		[Key]
		public int SizeMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogLenPlugDto
	{
		[Key]
		public int LenPlugId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGeologyIntervalDto
	{
		[Key]
		public int GeologyIntervalId { get; set; }
		public string TypeLithology { get; set; }
		public MudLogMdTopDto MdTop { get; set; }
		public MudLogMdBottomDto MdBottom { get; set; }
		public string DTim { get; set; }
		public MudLogTvdTopDto TvdTop { get; set; }
		public MudLogTvdBaseDto TvdBase { get; set; }
		public MudLogRopAvDto RopAv { get; set; }
		public MudLogRopMnDto RopMn { get; set; }
		public MudLogRopMxDto RopMx { get; set; }
		public MudLogWobAvDto WobAv { get; set; }
		public MudLogTqAvDto TqAv { get; set; }
		public MudLogRpmAvDto RpmAv { get; set; }
		public MudLogWtMudAvDto WtMudAv { get; set; }
		public MudLogEcdTdAvDto EcdTdAv { get; set; }
		public string DxcAv { get; set; }
		public MudLogLithologyDto Lithology { get; set; }
		public MudLogShowDto Show { get; set; }
		public MudLogChromatographDto Chromatograph { get; set; }
		public MudLogMudGasDto MudGas { get; set; }
		public MudLogDensBulkDto DensBulk { get; set; }
		public MudLogDensShaleDto DensShale { get; set; }
		public MudLogCalciteDto Calcite { get; set; }
		public MudLogDolomiteDto Dolomite { get; set; }
		public MudLogCecDto Cec { get; set; }
		public MudLogCalcStabDto CalcStab { get; set; }
		public MudLogLithostratigraphicDto Lithostratigraphic { get; set; }
		public List<MudLogChronostratigraphicDto> Chronostratigraphic { get; set; }
		public MudLogSizeMnDto SizeMn { get; set; }
		public MudLogSizeMxDto SizeMx { get; set; }
		public MudLogLenPlugDto LenPlug { get; set; }
		public string Description { get; set; }
		public string CuttingFluid { get; set; }
		public string CleaningMethod { get; set; }
		public string DryingMethod { get; set; }
		public MudLogCommonTimeDto CommonTime { get; set; }
		public string Uid { get; set; }
	}
 

	public class MudLogDto
	{
		[Key]
		public int MudLogId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string ObjectGrowing { get; set; }
		public string DTim { get; set; }
		public string MudLogCompany { get; set; }
		public string MudLogEngineers { get; set; }
		public MudLogStartMdDto StartMd { get; set; }
		public MudLogEndMdDto EndMd { get; set; }
		public List<MudLogParameterDto> Parameter { get; set; }
		public MudLogGeologyIntervalDto GeologyInterval { get; set; }
		public MudLogCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class MudLogCommonDataDto
	{
		[Key]
		public int MudLogCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	public class MudLogsDto
	{
		[Key]
		public int MudLogsId { get; set; }
	 
		public MudLogDto MudLog { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}

}
