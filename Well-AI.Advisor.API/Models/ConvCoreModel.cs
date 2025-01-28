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

    public class ConvCoreMdCoreTop {
		[Key]
		[Required]

		public int MdCoreTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreMdCoreBottom {
		[Key]
		[Required]

		public int MdCoreBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreLenBarrel {
		[Key]
		[Required]
		public int LenBarrelId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreDiaBit {
		[Key]
		[Required]
		public int DiaBitId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreDiaCore {
		[Key]
		[Required]
		public int DiaCoreId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreLenCored {
		[Key]
		[Required]
		public int LenCoredId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreLenRecovered {
		[Key]
		[Required]
		public int LenRecoveredId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreRecoverPc {
		[Key]
		[Required]
		public int LenRecoveredId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreInclHole {
		[Key]
		[Required]
		public int InclHoleId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreMdTop {
		[Key]
		[Required]
		public int MdTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreMdBottom {
		[Key]
		[Required]
		public int MdBottomId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreTvdTop {
		[Key]
		[Required]
		public int TvdTopId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreTvdBase {
		[Key]
		[Required]

		public int TvdBaseId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreRopAv {
		[Key]
		[Required]

		public int RopAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreRopMn {
		[Key]
		[Required]
		public int RopMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreRopMx {
		[Key]
		[Required]
		public int RopMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreWobAv {
		[Key]
		[Required]
		public int WobAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreTqAv {
		[Key]
		[Required]
		public int TqAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreRpmAv {
		[Key]
		[Required]

		public int RpmAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreWtMudAv {
		[Key]
		[Required]
		public int WtMudAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreEcdTdAv {
		[Key]
		[Required]
		public int EcdTdAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreLithPc {
		[Key]
		[Required]
		public int LithPcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreDensShale {
		[Key]
		[Required]
		public int DensShaleId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreQualifier {
		[Key]
		[Required]

		public int QualifierId { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class ConvCoreLithology {
		[Key]
		[Required]
		public int LithologyId { get; set; }
		public string Type { get; set; }
		public string CodeLith { get; set; }
		public ConvCoreLithPc LithPc { get; set; }
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
		public ConvCoreDensShale DensShale { get; set; }
		public List<ConvCoreQualifier> Qualifier { get; set; }
		public string Uid { get; set; }
	}

	public class ConvCoreStainPc {
		[Key]
		[Required]
		public int StainPcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNatFlorPc {
		[Key]
		[Required]

		public int NatFlorPcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreShow {
		[Key]
		[Required]
		public int ShowId { get; set; }
		public string ShowRat { get; set; }
		public string StainColor { get; set; }
		public string StainDistr { get; set; }
		public ConvCoreStainPc StainPc { get; set; }
		public string NatFlorColor { get; set; }
		public ConvCoreNatFlorPc NatFlorPc { get; set; }
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

	public class ConvCoreWtMudIn {
		[Key]
		[Required]
		public int WtMudInId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreWtMudOut {
		[Key]
		[Required]
		public int WtMudOutId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreETimChromCycle {
		[Key]
		[Required]

		public int ETimChromCycleId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreMethAv {
		[Key]
		[Required]

		public int MethAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreMethMn {
		[Key]
		[Required]
		public int MethMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreMethMx {
		[Key]
		[Required]
		public int MethMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreEthAv {
		[Key]
		[Required]
		public int EthAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreEthMn {
		[Key]
		[Required]
		public int EthMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreEthMx {
		[Key]
		[Required]
		public int EthMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCorePropAv {
		[Key]
		[Required]

		public int PropAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCorePropMn {
		[Key]
		[Required]

		public int PropMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCorePropMx {
		[Key]
		[Required]

		public int PropMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIbutAv {
		[Key]
		[Required]
		public int IbutAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIbutMn {
		[Key]
		[Required]
		public int IbutMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIbutMx {
		[Key]
		[Required]
		public int IbutMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNbutAv {
		[Key]
		[Required]
		public int NbutAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNbutMn {
		[Key]
		[Required]
		public int NbutMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNbutMx {
		[Key]
		[Required]

		public int NbutMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIpentAv {
		[Key]
		[Required]
		public int IpentAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIpentMn {
		[Key]
		[Required]
		public int IpentMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIpentMx {
		[Key]
		[Required]

		public int IpentMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNpentAv {
		[Key]
		[Required]

		public int NpentAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNpentMn {
		[Key]
		[Required]
		public int NpentMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNpentMx {
		[Key]
		[Required]

		public int NpentMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreEpentAv {
		[Key]
		[Required]

		public int EpentAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreEpentMn {
		[Key]
		[Required]

		public int EpentMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreEpentMx {
		[Key]
		[Required]
		public int EpentMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIhexAv {
		[Key]
		[Required]
		public int IhexAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIhexMn {
		[Key]
		[Required]
		public int IhexMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreIhexMx {
		[Key]
		[Required]
		public int IhexMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNhexAv {
		[Key]
		[Required]

		public int NhexAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNhexMn {
		[Key]
		[Required]
		public int NhexMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreNhexMx {
		[Key]
		[Required]
		public int NhexMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreCo2Av {
		[Key]
		[Required]
		public int Co2AvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreCo2Mn {
		[Key]
		[Required]
		public int Co2MnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreCo2Mx {
		[Key]
		[Required]

		public int Co2MxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreH2sAv {
		[Key]
		[Required]

		public int H2sAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreH2sMn {
		[Key]
		[Required]
		public int H2sMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreH2sMx {
		[Key]
		[Required]
		public int H2sMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreAcetylene {
		[Key]
		[Required]

		public int AcetyleneId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreChromatograph {
		[Key]
		[Required]

		public int ChromatographId { get; set; }
		public string DTim { get; set; }
		public ConvCoreMdTop MdTop { get; set; }
		public ConvCoreMdBottom MdBottom { get; set; }
		public ConvCoreWtMudIn WtMudIn { get; set; }
		public ConvCoreWtMudOut WtMudOut { get; set; }
		public string ChromType { get; set; }
		public ConvCoreETimChromCycle ETimChromCycle { get; set; }
		public string ChromIntRpt { get; set; }
		public ConvCoreMethAv MethAv { get; set; }
		public ConvCoreMethMn MethMn { get; set; }
		public ConvCoreMethMx MethMx { get; set; }
		public ConvCoreEthAv EthAv { get; set; }
		public ConvCoreEthMn EthMn { get; set; }
		public ConvCoreEthMx EthMx { get; set; }
		public ConvCorePropAv PropAv { get; set; }
		public ConvCorePropMn PropMn { get; set; }
		public ConvCorePropMx PropMx { get; set; }
		public ConvCoreIbutAv IbutAv { get; set; }
		public ConvCoreIbutMn IbutMn { get; set; }
		public ConvCoreIbutMx IbutMx { get; set; }
		public ConvCoreNbutAv NbutAv { get; set; }
		public ConvCoreNbutMn NbutMn { get; set; }
		public ConvCoreNbutMx NbutMx { get; set; }
		public ConvCoreIpentAv IpentAv { get; set; }
		public ConvCoreIpentMn IpentMn { get; set; }
		public ConvCoreIpentMx IpentMx { get; set; }
		public ConvCoreNpentAv NpentAv { get; set; }
		public ConvCoreNpentMn NpentMn { get; set; }
		public ConvCoreNpentMx NpentMx { get; set; }
		public ConvCoreEpentAv EpentAv { get; set; }
		public ConvCoreEpentMn EpentMn { get; set; }
		public ConvCoreEpentMx EpentMx { get; set; }
		public ConvCoreIhexAv IhexAv { get; set; }
		public ConvCoreIhexMn IhexMn { get; set; }
		public ConvCoreIhexMx IhexMx { get; set; }
		public ConvCoreNhexAv NhexAv { get; set; }
		public ConvCoreNhexMn NhexMn { get; set; }
		public ConvCoreNhexMx NhexMx { get; set; }
		public ConvCoreCo2Av Co2Av { get; set; }
		public ConvCoreCo2Mn Co2Mn { get; set; }
		public ConvCoreCo2Mx Co2Mx { get; set; }
		public ConvCoreH2sAv H2sAv { get; set; }
		public ConvCoreH2sMn H2sMn { get; set; }
		public ConvCoreH2sMx H2sMx { get; set; }
		public ConvCoreAcetylene Acetylene { get; set; }
	}

	public class ConvCoreGasAv {
		[Key]
		[Required]

		public int GasAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreGasPeak {
		[Key]
		[Required]

		public int GasPeakId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreGasBackgnd {
		[Key]
		[Required]

		public int GasBackgndId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreGasConAv {
		[Key]
		[Required]
		public int GasConAvId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreGasConMx {
		[Key]
		[Required]

		public int GasConMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreGasTrip {
		[Key]
		[Required]
		public int GasConTripId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreMudGas {
		[Key]
		[Required]
		public int MudGasId { get; set; }
		public ConvCoreGasAv GasAv { get; set; }
		public ConvCoreGasPeak GasPeak { get; set; }
		public string GasPeakType { get; set; }
		public ConvCoreGasBackgnd GasBackgnd { get; set; }
		public ConvCoreGasConAv GasConAv { get; set; }
		public ConvCoreGasConMx GasConMx { get; set; }
		public ConvCoreGasTrip GasTrip { get; set; }
	}

	public class ConvCoreDensBulk {
		[Key]
		[Required]
		public int DensBulkId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreCalcite {
		[Key]
		[Required]

		public int CalciteId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreDolomite {
		[Key]
		[Required]

		public int DolomiteId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreCec {
		[Key]
		[Required]

		public int CecId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreCalcStab {
		[Key]
		[Required]

		public int CalcStabId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreSizeMn {
		[Key]
		[Required]
		public int SizeMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreSizeMx {
		[Key]
		[Required]

		public int SizeMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreLenPlug {
		[Key]
		[Required]

		public int LenPlugId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ConvCoreGeologyInterval {
		[Key]
		[Required]
		public int GeologyIntervalId { get; set; }
		public string TypeLithology { get; set; }
		public ConvCoreMdTop MdTop { get; set; }
		public ConvCoreMdBottom MdBottom { get; set; }
		public string DTim { get; set; }
		public ConvCoreTvdTop TvdTop { get; set; }
		public ConvCoreTvdBase TvdBase { get; set; }
		public ConvCoreRopAv RopAv { get; set; }
		public ConvCoreRopMn RopMn { get; set; }
		public ConvCoreRopMx RopMx { get; set; }
		public ConvCoreWobAv WobAv { get; set; }
		public ConvCoreTqAv TqAv { get; set; }
		public ConvCoreRpmAv RpmAv { get; set; }
		public ConvCoreWtMudAv WtMudAv { get; set; }
		public ConvCoreEcdTdAv EcdTdAv { get; set; }
		public string DxcAv { get; set; }
		public ConvCoreLithology Lithology { get; set; }
		public ConvCoreShow Show { get; set; }
		public ConvCoreChromatograph Chromatograph { get; set; }
		public ConvCoreMudGas MudGas { get; set; }
		public ConvCoreDensBulk DensBulk { get; set; }
		public ConvCoreDensShale DensShale { get; set; }
		public ConvCoreCalcite Calcite { get; set; }
		public ConvCoreDolomite Dolomite { get; set; }
		public ConvCoreCec Cec { get; set; }
		public ConvCoreCalcStab CalcStab { get; set; }
		public string NameFormation { get; set; }
		public string Lithostratigraphic { get; set; }
		public string Chronostratigraphic { get; set; }
		public ConvCoreSizeMn SizeMn { get; set; }
		public ConvCoreSizeMx SizeMx { get; set; }
		public ConvCoreLenPlug LenPlug { get; set; }
		public string Description { get; set; }
		public string CuttingFluid { get; set; }
		public string CleaningMethod { get; set; }
		public string DryingMethod { get; set; }
		public string Comments { get; set; }
		
		public string Uid { get; set; }
	}

	public class ConvCoreCommonData {
		[Key]
		[Required]
		public int CommonDataId { get; set; }
		public string ItemState { get; set; }
		public string Comments { get; set; }
	}

	public class ConvCore {
		[Key]
		[Required]

		public int ConvCoreId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public ConvCoreMdCoreTop MdCoreTop { get; set; }
		public ConvCoreMdCoreBottom MdCoreBottom { get; set; }
		public string DTimCoreStart { get; set; }
		public string DTimCoreEnd { get; set; }
		public string CoreReference { get; set; }
		public string CoringContractor { get; set; }
		public string AnalysisContractor { get; set; }
		public string CoreBarrel { get; set; }
		public string InnerBarrelUsed { get; set; }
		public string InnerBarrelType { get; set; }
		public ConvCoreLenBarrel LenBarrel { get; set; }
		public string CoreBitType { get; set; }
		public ConvCoreDiaBit DiaBit { get; set; }
		public ConvCoreDiaCore DiaCore { get; set; }
		public ConvCoreLenCored LenCored { get; set; }
		public ConvCoreLenRecovered LenRecovered { get; set; }
		public ConvCoreRecoverPc RecoverPc { get; set; }
		public ConvCoreInclHole InclHole { get; set; }
		public string CoreOrientation { get; set; }
		public string CoreMethod { get; set; }
		public string CoreTreatmentMethod { get; set; }
		public string CoreFluidUsed { get; set; }
		public string NameFormation { get; set; }
		public ConvCoreGeologyInterval GeologyInterval { get; set; }
		public string CoreDescription { get; set; }
		public ConvCoreCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class ConvCores {
		public ConvCore ConvCore { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}

}
