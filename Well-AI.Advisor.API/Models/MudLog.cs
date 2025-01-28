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
	public class MudLogStartMd
	{
		[Key]
		public int StartMdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEndMd
	{
		[Key]
		public int EndMdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
 

	public class MudLogCommonTime
	{
		[Key]
		public int CommonTimeId { get; set; }
		public string DTimCreation { get; set; }
		public string DTimLastChange { get; set; }
	}

	public class MudLogMdTop
	{
		[Key]
		[Required]
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMdBottom
	{
		[Key]
		public int MdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	

	public class MudLogParameter
	{
		[Key]
		public int ParameterId { get; set; }
		public string Type { get; set; }
		public MudLogMdTop MdTop { get; set; }
		public MudLogMdBottom MdBottom { get; set; }
		public string Text { get; set; }
		public MudLogCommonTime CommonTime { get; set; }
		public string Uid { get; set; }
		public MudLogForce Force { get; set; }
	}

	public class MudLogForce
	{
		[Key]
		public int ForceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogTvdTop
	{
		[Key]
		public int TvdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogTvdBase
	{
		[Key]
		public int TvdBaseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogRopAv
	{
		[Key]
		public int RopAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogRopMn
	{
		[Key]
		public int RopMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogRopMx
	{
		[Key]
		public int RopMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogWobAv
	{
		[Key]
		public int WobAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogTqAv
	{
		[Key]
		public int TqAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogRpmAv
	{
		[Key]
		public int RpmAvId { get; set; }

		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogWtMudAv
	{
		[Key]
		public int WtMudAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEcdTdAv
	{
		[Key]
		public int EcdTdAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogDensShale
	{
		[Key]
		public int DensShaleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogAbundance
	{
		[Key]
		public int AbundanceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogQualifier
	{
		[Key]
		public int QualifierId { get; set; }
		public string Type { get; set; }
		public MudLogAbundance Abundance { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class MudLogLithology
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
		public MudLogDensShale DensShale { get; set; }
		public MudLogQualifier Qualifier { get; set; }
		public string Uid { get; set; }
	}

	public class MudLogStainPc
	{
		[Key]
		public int StainPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNatFlorPc
	{
		[Key]
		public int NatFlorPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogShow
	{
		[Key]
		public int ShowId { get; set; }
		public string ShowRat { get; set; }
		public string StainColor { get; set; }
		public string StainDistr { get; set; }
		public MudLogStainPc StainPc { get; set; }
		public string NatFlorColor { get; set; }
		public MudLogNatFlorPc NatFlorPc { get; set; }
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

	public class MudLogWtMudIn
	{
		[Key]
		public int WtMudInId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogWtMudOut
	{
		[Key]
		public int WtMudOutId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogETimChromCycle
	{
		[Key]
		public int ETimChromCycleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMethAv
	{
		[Key]
		public int MethAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMethMn
	{
		[Key]
		public int MethMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMethMx
	{
		[Key]
		public int MethMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEthAv
	{
		[Key]
		public int EthAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEthMn
	{
		[Key]
		public int EthMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEthMx
	{
		[Key]
		public int EthMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogPropAv
	{
		[Key]
		public int PropAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogPropMn
	{
		[Key]
		public int PropMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogPropMx
	{
		[Key]
		public int PropMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIbutAv
	{
		[Key]
		public int IbutAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIbutMn
	{
		[Key]
		public int IbutMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIbutMx
	{
		[Key]
		public int IbutMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNbutAv
	{
		[Key]
		public int NbutAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNbutMn
	{
		[Key]
		public int NbutMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNbutMx
	{
		[Key]
		public int NbutMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIpentAv
	{
		[Key]
		public int IpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIpentMn
	{
		[Key]
		public int IpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIpentMx
	{
		[Key]
		public int IpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNpentAv
	{
		[Key]
		public int NpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNpentMn
	{
		[Key]
		public int NpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNpentMx
	{
		[Key]
		public int NpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEpentAv
	{
		[Key]
		public int EpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEpentMn
	{
		[Key]
		public int EpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogEpentMx
	{
		[Key]
		public int EpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIhexAv
	{
		[Key]
		public int IhexAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIhexMn
	{
		[Key]
		public int IhexMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogIhexMx
	{
		[Key]
		public int IhexMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNhexAv
	{
		[Key]
		public int NhexAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNhexMn
	{
		[Key]
		public int NhexMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogNhexMx
	{
		[Key]
		public int NhexMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCo2Av
	{
		[Key]
		public int Co2AvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCo2Mn
	{
		[Key]
		public int Co2MnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCo2Mx
	{
		[Key]
		public int Co2MxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogH2sAv
	{
		[Key]
		public int H2sAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogH2sMn
	{
		[Key]
		public int H2sMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogH2sMx
	{
		[Key]
		public int H2sMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogAcetylene
	{
		[Key]
		public int AcetyleneId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogChromatograph
	{
		[Key]
		public int ChromatographId { get; set; }
		public string DTim { get; set; }
		public MudLogMdTop MdTop { get; set; }
		public MudLogMdBottom MdBottom { get; set; }
		public MudLogWtMudIn WtMudIn { get; set; }
		public MudLogWtMudOut WtMudOut { get; set; }
		public string ChromType { get; set; }
		public MudLogETimChromCycle ETimChromCycle { get; set; }
		public string ChromIntRpt { get; set; }
		public MudLogMethAv MethAv { get; set; }
		public MudLogMethMn MethMn { get; set; }
		public MudLogMethMx MethMx { get; set; }
		public MudLogEthAv EthAv { get; set; }
		public MudLogEthMn EthMn { get; set; }
		public MudLogEthMx EthMx { get; set; }
		public MudLogPropAv PropAv { get; set; }
		public MudLogPropMn PropMn { get; set; }
		public MudLogPropMx PropMx { get; set; }
		public MudLogIbutAv IbutAv { get; set; }
		public MudLogIbutMn IbutMn { get; set; }
		public MudLogIbutMx IbutMx { get; set; }
		public MudLogNbutAv NbutAv { get; set; }
		public MudLogNbutMn NbutMn { get; set; }
		public MudLogNbutMx NbutMx { get; set; }
		public MudLogIpentAv IpentAv { get; set; }
		public MudLogIpentMn IpentMn { get; set; }
		public MudLogIpentMx IpentMx { get; set; }
		public MudLogNpentAv NpentAv { get; set; }
		public MudLogNpentMn NpentMn { get; set; }
		public MudLogNpentMx NpentMx { get; set; }
		public MudLogEpentAv EpentAv { get; set; }
		public MudLogEpentMn EpentMn { get; set; }
		public MudLogEpentMx EpentMx { get; set; }
		public MudLogIhexAv IhexAv { get; set; }
		public MudLogIhexMn IhexMn { get; set; }
		public MudLogIhexMx IhexMx { get; set; }
		public MudLogNhexAv NhexAv { get; set; }
		public MudLogNhexMn NhexMn { get; set; }
		public MudLogNhexMx NhexMx { get; set; }
		public MudLogCo2Av Co2Av { get; set; }
		public MudLogCo2Mn Co2Mn { get; set; }
		public MudLogCo2Mx Co2Mx { get; set; }
		public MudLogH2sAv H2sAv { get; set; }
		public MudLogH2sMn H2sMn { get; set; }
		public MudLogH2sMx H2sMx { get; set; }
		public MudLogAcetylene Acetylene { get; set; }
	}

	public class MudLogGasAv
	{
		[Key]
		public int GasAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasPeak
	{
		[Key]
		public int GasPeakId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasBackgnd
	{
		[Key]
		public int GasBackgndId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasConAv
	{
		[Key]
		public int GasConAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasConMx
	{
		[Key]
		public int GasConMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGasTrip
	{
		[Key]
		public int GasTripId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogMudGas
	{
		[Key]
		public int MudGasId { get; set; }
		public MudLogGasAv GasAv { get; set; }
		public MudLogGasPeak GasPeak { get; set; }
		public string GasPeakType { get; set; }
		public MudLogGasBackgnd GasBackgnd { get; set; }
		public MudLogGasConAv GasConAv { get; set; }
		public MudLogGasConMx GasConMx { get; set; }
		public MudLogGasTrip GasTrip { get; set; }
	}

	public class MudLogDensBulk
	{
		[Key]
		public int DensBulkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCalcite
	{
		[Key]
		public int CalciteId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogDolomite
	{
		[Key]
		public int DolomiteId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCec
	{
		[Key]
		public int CecId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogCalcStab
	{
		[Key]
		public int CalcStabId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogLithostratigraphic
	{
		[Key]
		public int LithostratigraphicId { get; set; }
		public string Kind { get; set; }

		public string Text { get; set; }
	}

	public class MudLogChronostratigraphic
	{
		[Key]
		public int ChronostratigraphicId { get; set; }
		public string Kind { get; set; }

		public string Text { get; set; }
	}

	public class MudLogSizeMn
	{
		[Key]
		public int SizeMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogSizeMx
	{
		[Key]
		public int SizeMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogLenPlug
	{
		[Key]
		public int LenPlugId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MudLogGeologyInterval
	{
		[Key]
		public int GeologyIntervalId { get; set; }
		public string TypeLithology { get; set; }
		public MudLogMdTop MdTop { get; set; }
		public MudLogMdBottom MdBottom { get; set; }
		public string DTim { get; set; }
		public MudLogTvdTop TvdTop { get; set; }
		public MudLogTvdBase TvdBase { get; set; }
		public MudLogRopAv RopAv { get; set; }
		public MudLogRopMn RopMn { get; set; }
		public MudLogRopMx RopMx { get; set; }
		public MudLogWobAv WobAv { get; set; }
		public MudLogTqAv TqAv { get; set; }
		public MudLogRpmAv RpmAv { get; set; }
		public MudLogWtMudAv WtMudAv { get; set; }
		public MudLogEcdTdAv EcdTdAv { get; set; }
		public string DxcAv { get; set; }
		public MudLogLithology Lithology { get; set; }
		public MudLogShow Show { get; set; }
		public MudLogChromatograph Chromatograph { get; set; }
		public MudLogMudGas MudGas { get; set; }
		public MudLogDensBulk DensBulk { get; set; }
		public MudLogDensShale DensShale { get; set; }
		public MudLogCalcite Calcite { get; set; }
		public MudLogDolomite Dolomite { get; set; }
		public MudLogCec Cec { get; set; }
		public MudLogCalcStab CalcStab { get; set; }
		public MudLogLithostratigraphic Lithostratigraphic { get; set; }
		public List<MudLogChronostratigraphic> Chronostratigraphic { get; set; }
		public MudLogSizeMn SizeMn { get; set; }
		public MudLogSizeMx SizeMx { get; set; }
		public MudLogLenPlug LenPlug { get; set; }
		public string Description { get; set; }
		public string CuttingFluid { get; set; }
		public string CleaningMethod { get; set; }
		public string DryingMethod { get; set; }
		public MudLogCommonTime CommonTime { get; set; }
		public string Uid { get; set; }
	}


	public class MudLog
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
		public MudLogStartMd StartMd { get; set; }
		public MudLogEndMd EndMd { get; set; }
		public List<MudLogParameter> Parameter { get; set; }
		public MudLogGeologyInterval GeologyInterval { get; set; }
		public MudLogCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class MudLogCommonData
	{
		[Key]
		public int MudLogCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	public class MudLogs
	{
		[Key]
		public int MudLogsId { get; set; }
		 
		public MudLog MudLog { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}
}
