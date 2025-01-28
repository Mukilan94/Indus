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
	public class ConvCoreMdCoreTopDto
	{
		[Key]
		[Required]

		public int MdCoreTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreMdCoreBottomDto
	{
		[Key]
		[Required]

		public int MdCoreBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreLenBarrelDto
	{
		[Key]
		[Required]
		public int LenBarrelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreDiaBitDto
	{
		[Key]
		[Required]
		public int DiaBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreDiaCoreDto
	{
		[Key]
		[Required]
		public int DiaCoreId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreLenCoredDto
	{
		[Key]
		[Required]
		public int LenCoredId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreLenRecoveredDto
	{
		[Key]
		[Required]
		public int LenRecoveredId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreRecoverPcDto
	{
		[Key]
		[Required]
		public int LenRecoveredId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreInclHoleDto
	{
		[Key]
		[Required]
		public int InclHoleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreMdTopDto
	{
		[Key]
		[Required]
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreMdBottomDto
	{
		[Key]
		[Required]
		public int MdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreTvdTopDto
	{
		[Key]
		[Required]
		public int TvdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreTvdBaseDto
	{
		[Key]
		[Required]

		public int TvdBaseId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreRopAvDto
	{
		[Key]
		[Required]

		public int RopAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreRopMnDto
	{
		[Key]
		[Required]
		public int RopMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreRopMxDto
	{
		[Key]
		[Required]
		public int RopMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreWobAvDto
	{
		[Key]
		[Required]
		public int WobAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreTqAvDto
	{
		[Key]
		[Required]
		public int TqAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreRpmAvDto
	{
		[Key]
		[Required]

		public int RpmAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreWtMudAvDto
	{
		[Key]
		[Required]
		public int WtMudAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreEcdTdAvDto
	{
		[Key]
		[Required]
		public int EcdTdAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreLithPcDto
	{
		[Key]
		[Required]
		public int LithPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreDensShaleDto
	{
		[Key]
		[Required]
		public int DensShaleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreQualifierDto
	{
		[Key]
		[Required]

		public int QualifierId { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class ConvCoreLithologyDto
	{
		[Key]
		[Required]
		public int LithologyId { get; set; }
		public string Type { get; set; }
		public string CodeLith { get; set; }
		public ConvCoreLithPcDto LithPc { get; set; }
		public string Description { get; set; }
		public string LithClass { get; set; }
		public string GrainType { get; set; }
		public string DunhamClass { get; set; }
		public string Color { get; set; }
		public string Texture { get; set; }
		public string Hardness { get; set; }
		public string SizeGrain { get; set; }
		public string Roundness { get; set; }
		public string Sorting { get; set; }
		public string MatrixCement { get; set; }
		public string PorosityVisible { get; set; }
		public string Permeability { get; set; }
		public ConvCoreDensShaleDto DensShale { get; set; }
		public List<ConvCoreQualifierDto> Qualifier { get; set; }
		public string Uid { get; set; }
	}

	public class ConvCoreStainPcDto
	{
		[Key]
		[Required]
		public int StainPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNatFlorPcDto
	{
		[Key]
		[Required]

		public int NatFlorPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreShowDto
	{
		[Key]
		[Required]
		public int ShowId { get; set; }
		public string ShowRat { get; set; }
		public string StainColor { get; set; }
		public string StainDistr { get; set; }
		public ConvCoreStainPcDto StainPc { get; set; }
		public string NatFlorColor { get; set; }
		public ConvCoreNatFlorPcDto NatFlorPc { get; set; }
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

	public class ConvCoreWtMudInDto
	{
		[Key]
		[Required]
		public int WtMudInId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreWtMudOutDto
	{
		[Key]
		[Required]
		public int WtMudOutId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreETimChromCycleDto
	{
		[Key]
		[Required]

		public int ETimChromCycleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreMethAvDto
	{
		[Key]
		[Required]

		public int MethAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreMethMnDto
	{
		[Key]
		[Required]
		public int MethMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreMethMxDto
	{
		[Key]
		[Required]
		public int MethMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreEthAvDto
	{
		[Key]
		[Required]
		public int EthAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreEthMnDto
	{
		[Key]
		[Required]
		public int EthMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreEthMxDto
	{
		[Key]
		[Required]
		public int EthMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCorePropAvDto
	{
		[Key]
		[Required]

		public int PropAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCorePropMnDto
	{
		[Key]
		[Required]

		public int PropMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCorePropMxDto
	{
		[Key]
		[Required]

		public int PropMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIbutAvDto
	{
		[Key]
		[Required]
		public int IbutAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIbutMnDto
	{
		[Key]
		[Required]
		public int IbutMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIbutMxDto
	{
		[Key]
		[Required]
		public int IbutMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNbutAvDto
	{
		[Key]
		[Required]
		public int NbutAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNbutMnDto
	{
		[Key]
		[Required]
		public int NbutMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNbutMxDto
	{
		[Key]
		[Required]

		public int NbutMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIpentAvDto
	{
		[Key]
		[Required]
		public int IpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIpentMnDto
	{
		[Key]
		[Required]
		public int IpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIpentMxDto
	{
		[Key]
		[Required]

		public int IpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNpentAvDto
	{
		[Key]
		[Required]

		public int NpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNpentMnDto
	{
		[Key]
		[Required]
		public int NpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNpentMxDto
	{
		[Key]
		[Required]

		public int NpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreEpentAvDto
	{
		[Key]
		[Required]

		public int EpentAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreEpentMnDto
	{
		[Key]
		[Required]

		public int EpentMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreEpentMxDto
	{
		[Key]
		[Required]
		public int EpentMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIhexAvDto
	{
		[Key]
		[Required]
		public int IhexAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIhexMnDto
	{
		[Key]
		[Required]
		public int IhexMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreIhexMxDto
	{
		[Key]
		[Required]
		public int IhexMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNhexAvDto
	{
		[Key]
		[Required]

		public int NhexAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNhexMnDto
	{
		[Key]
		[Required]
		public int NhexMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreNhexMxDto
	{
		[Key]
		[Required]
		public int NhexMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreCo2AvDto
	{
		[Key]
		[Required]
		public int Co2AvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreCo2MnDto
	{
		[Key]
		[Required]
		public int Co2MnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreCo2MxDto
	{
		[Key]
		[Required]

		public int Co2MxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreH2sAvDto
	{
		[Key]
		[Required]

		public int H2sAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreH2sMnDto
	{
		[Key]
		[Required]
		public int H2sMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreH2sMxDto
	{
		[Key]
		[Required]
		public int H2sMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreAcetyleneDto
	{
		[Key]
		[Required]

		public int AcetyleneId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreChromatographDto
	{
		[Key]
		[Required]

		public int ChromatographId { get; set; }
		public string DTim { get; set; }
		public ConvCoreMdTopDto MdTop { get; set; }
		public ConvCoreMdBottomDto MdBottom { get; set; }
		public ConvCoreWtMudInDto WtMudIn { get; set; }
		public ConvCoreWtMudOutDto WtMudOut { get; set; }
		public string ChromType { get; set; }
		public ConvCoreETimChromCycleDto ETimChromCycle { get; set; }
		public string ChromIntRpt { get; set; }
		public ConvCoreMethAvDto MethAv { get; set; }
		public ConvCoreMethMnDto MethMn { get; set; }
		public ConvCoreMethMxDto MethMx { get; set; }
		public ConvCoreEthAvDto EthAv { get; set; }
		public ConvCoreEthMnDto EthMn { get; set; }
		public ConvCoreEthMxDto EthMx { get; set; }
		public ConvCorePropAvDto PropAv { get; set; }
		public ConvCorePropMnDto PropMn { get; set; }
		public ConvCorePropMxDto PropMx { get; set; }
		public ConvCoreIbutAvDto IbutAv { get; set; }
		public ConvCoreIbutMnDto IbutMn { get; set; }
		public ConvCoreIbutMxDto IbutMx { get; set; }
		public ConvCoreNbutAvDto NbutAv { get; set; }
		public ConvCoreNbutMnDto NbutMn { get; set; }
		public ConvCoreNbutMxDto NbutMx { get; set; }
		public ConvCoreIpentAvDto IpentAv { get; set; }
		public ConvCoreIpentMnDto IpentMn { get; set; }
		public ConvCoreIpentMxDto IpentMx { get; set; }
		public ConvCoreNpentAvDto NpentAv { get; set; }
		public ConvCoreNpentMnDto NpentMn { get; set; }
		public ConvCoreNpentMxDto NpentMx { get; set; }
		public ConvCoreEpentAvDto EpentAv { get; set; }
		public ConvCoreEpentMnDto EpentMn { get; set; }
		public ConvCoreEpentMxDto EpentMx { get; set; }
		public ConvCoreIhexAvDto IhexAv { get; set; }
		public ConvCoreIhexMnDto IhexMn { get; set; }
		public ConvCoreIhexMxDto IhexMx { get; set; }
		public ConvCoreNhexAvDto NhexAv { get; set; }
		public ConvCoreNhexMnDto NhexMn { get; set; }
		public ConvCoreNhexMxDto NhexMx { get; set; }
		public ConvCoreCo2AvDto Co2Av { get; set; }
		public ConvCoreCo2MnDto Co2Mn { get; set; }
		public ConvCoreCo2MxDto Co2Mx { get; set; }
		public ConvCoreH2sAvDto H2sAv { get; set; }
		public ConvCoreH2sMnDto H2sMn { get; set; }
		public ConvCoreH2sMxDto H2sMx { get; set; }
		public ConvCoreAcetyleneDto Acetylene { get; set; }
	}

	public class ConvCoreGasAvDto
	{
		[Key]
		[Required]

		public int GasAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreGasPeakDto
	{
		[Key]
		[Required]

		public int GasPeakId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreGasBackgndDto
	{
		[Key]
		[Required]

		public int GasBackgndId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreGasConAvDto
	{
		[Key]
		[Required]
		public int GasConAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreGasConMxDto
	{
		[Key]
		[Required]

		public int GasConMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreGasTripDto
	{
		[Key]
		[Required]
		public int GasConTripId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreMudGasDto
	{
		[Key]
		[Required]
		public int MudGasId { get; set; }
		public ConvCoreGasAvDto GasAv { get; set; }
		public ConvCoreGasPeakDto GasPeak { get; set; }
		public string GasPeakType { get; set; }
		public ConvCoreGasBackgndDto GasBackgnd { get; set; }
		public ConvCoreGasConAvDto GasConAv { get; set; }
		public ConvCoreGasConMxDto GasConMx { get; set; }
		public ConvCoreGasTripDto GasTrip { get; set; }
	}

	public class ConvCoreDensBulkDto
	{
		[Key]
		[Required]
		public int DensBulkId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreCalciteDto
	{
		[Key]
		[Required]

		public int CalciteId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreDolomiteDto
	{
		[Key]
		[Required]

		public int DolomiteId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreCecDto
	{
		[Key]
		[Required]

		public int CecId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreCalcStabDto
	{
		[Key]
		[Required]

		public int CalcStabId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreSizeMnDto
	{
		[Key]
		[Required]
		public int SizeMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreSizeMxDto
	{
		[Key]
		[Required]

		public int SizeMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreLenPlugDto
	{
		[Key]
		[Required]

		public int LenPlugId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ConvCoreGeologyIntervalDto
	{
		[Key]
		[Required]
		public int GeologyIntervalId { get; set; }
		public string TypeLithology { get; set; }
		public ConvCoreMdTopDto MdTop { get; set; }
		public ConvCoreMdBottomDto MdBottom { get; set; }
		public string DTim { get; set; }
		public ConvCoreTvdTopDto TvdTop { get; set; }
		public ConvCoreTvdBaseDto TvdBase { get; set; }
		public ConvCoreRopAvDto RopAv { get; set; }
		public ConvCoreRopMnDto RopMn { get; set; }
		public ConvCoreRopMxDto RopMx { get; set; }
		public ConvCoreWobAvDto WobAv { get; set; }
		public ConvCoreTqAvDto TqAv { get; set; }
		public ConvCoreRpmAvDto RpmAv { get; set; }
		public ConvCoreWtMudAvDto WtMudAv { get; set; }
		public ConvCoreEcdTdAvDto EcdTdAv { get; set; }
		public string DxcAv { get; set; }
		public ConvCoreLithologyDto Lithology { get; set; }
		public ConvCoreShowDto Show { get; set; }
		public ConvCoreChromatographDto Chromatograph { get; set; }
		public ConvCoreMudGasDto MudGas { get; set; }
		public ConvCoreDensBulkDto DensBulk { get; set; }
		public ConvCoreDensShaleDto DensShale { get; set; }
		public ConvCoreCalciteDto Calcite { get; set; }
		public ConvCoreDolomiteDto Dolomite { get; set; }
		public ConvCoreCecDto Cec { get; set; }
		public ConvCoreCalcStabDto CalcStab { get; set; }
		public string NameFormation { get; set; }
		public string Lithostratigraphic { get; set; }
		public string Chronostratigraphic { get; set; }
		public ConvCoreSizeMnDto SizeMn { get; set; }
		public ConvCoreSizeMxDto SizeMx { get; set; }
		public ConvCoreLenPlugDto LenPlug { get; set; }
		public string Description { get; set; }
		public string CuttingFluid { get; set; }
		public string CleaningMethod { get; set; }
		public string DryingMethod { get; set; }
		public string Comments { get; set; }

		public string Uid { get; set; }
	}

	public class ConvCoreCommonDataDto
	{
		[Key]
		[Required]
		public int CommonDataId { get; set; }
		public string ItemState { get; set; }
		public string Comments { get; set; }
	}

	public class ConvCoreDto
	{
		[Key]
		[Required]

		public int ConvCoreId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public ConvCoreMdCoreTopDto MdCoreTop { get; set; }
		public ConvCoreMdCoreBottomDto MdCoreBottom { get; set; }
		public string DTimCoreStart { get; set; }
		public string DTimCoreEnd { get; set; }
		public string CoreReference { get; set; }
		public string CoringContractor { get; set; }
		public string AnalysisContractor { get; set; }
		public string CoreBarrel { get; set; }
		public string InnerBarrelUsed { get; set; }
		public string InnerBarrelType { get; set; }
		public ConvCoreLenBarrelDto LenBarrel { get; set; }
		public string CoreBitType { get; set; }
		public ConvCoreDiaBitDto DiaBit { get; set; }
		public ConvCoreDiaCoreDto DiaCore { get; set; }
		public ConvCoreLenCoredDto LenCored { get; set; }
		public ConvCoreLenRecoveredDto LenRecovered { get; set; }
		public ConvCoreRecoverPcDto RecoverPc { get; set; }
		public ConvCoreInclHoleDto InclHole { get; set; }
		public string CoreOrientation { get; set; }
		public string CoreMethod { get; set; }
		public string CoreTreatmentMethod { get; set; }
		public string CoreFluidUsed { get; set; }
		public string NameFormation { get; set; }
		public ConvCoreGeologyIntervalDto GeologyInterval { get; set; }
		public string CoreDescription { get; set; }
		public ConvCoreCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class ConvCores
	{
		public ConvCoreDto ConvCore { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}
}
