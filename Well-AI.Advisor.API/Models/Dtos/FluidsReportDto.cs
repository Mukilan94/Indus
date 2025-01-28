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

	public class FluidsReportVisFunnelDto
	{
		[Key]
		[Required]

		public int VisFunnelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportGel30MinDto
	{
		[Key]
		[Required]

		public int Gel30MinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportFilterCakeLtlpDto
	{
		[Key]
		[Required]

		public int FilterCakeLtlpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportFiltrateLtlpDto
	{
		[Key]
		[Required]
		public int FiltrateLtlpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTempHthpDto
	{
		[Key]
		[Required]

		public int TempHthpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPresHthpDto
	{
		[Key]
		[Required]
		public int PresHthpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportFiltrateHthpDto
	{
		[Key]
		[Required]
		public int FiltrateHthpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportFilterCakeHthpDto
	{
		[Key]
		[Required]
		public int FilterCakeHthpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolidsPcDto
	{
		[Key]
		[Required]

		public int SolidsPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportWaterPcDto
	{
		[Key]
		[Required]
		public int WaterPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportOilPcDto
	{
		[Key]
		[Required]
		public int OilPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSandPcDto
	{
		[Key]
		[Required]
		public int SandPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolidsLowGravPcDto
	{
		[Key]
		[Required]
		public int SolidsLowGravPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolidsCalcPcDto
	{
		[Key]
		[Required]
		public int SolidsCalcPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportBaritePcDto
	{
		[Key]
		[Required]
		public int BaritePcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportLcmDto
	{
		[Key]
		[Required]
		public int LcmId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportMbtDto
	{
		[Key]
		[Required]
		public int MbtId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTempPhDto
	{
		[Key]
		[Required]
		public int TempPhId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPmDto
	{
		[Key]
		[Required]

		public int PmId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPmFiltrateDto
	{
		[Key]
		[Required]
		public int PmFiltrateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportMfDto
	{
		[Key]
		[Required]
		public int MfId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportAlkalinityP1Dto
	{
		[Key]
		[Required]
		public int AlkalinityP1Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportAlkalinityP2Dto
	{
		[Key]
		[Required]
		public int AlkalinityP2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class FluidsReportCalciumDto
	{
		[Key]
		[Required]
		public int CalciumId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportMagnesiumDto
	{
		[Key]
		[Required]
		public int MagnesiumId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPotassiumDto
	{
		[Key]
		[Required]
		public int PotassiumId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTempRheomDto
	{
		[Key]
		[Required]
		public int TempRheomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPresRheomDto
	{
		[Key]
		[Required]
		public int PresRheomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportRheometerDto
	{
		[Key]
		[Required]
		public int RheometerId { get; set; }
		public FluidsReportTempRheomDto TempRheom { get; set; }
		public FluidsReportPresRheomDto PresRheom { get; set; }
		public string Vis3Rpm { get; set; }
		public string Vis6Rpm { get; set; }
		public string Vis100Rpm { get; set; }
		public string Vis200Rpm { get; set; }
		public string Vis300Rpm { get; set; }
		public string Vis600Rpm { get; set; }
		public string Uid { get; set; }
	}

	public class FluidsReportBrinePcDto
	{
		[Key]
		[Required]
		public int BrinePcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportLimeDto
	{
		[Key]
		[Required]
		public int LimeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportElectStabDto
	{
		[Key]
		[Required]
		public int ElectStabId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportCalciumChlorideDto
	{
		[Key]
		[Required]
		public int CalciumChlorideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolidsHiGravPcDto
	{
		[Key]
		[Required]
		public int SolidsHiGravPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPolymerDto
	{
		[Key]
		[Required]
		public int PolymerId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolCorPcDto
	{
		[Key]
		[Required]
		public int SolCorPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportOilCtgDto
	{
		[Key]
		[Required]
		public int OilCtgId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportHardnessCaDto
	{
		[Key]
		[Required]
		public int HardnessCaId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSulfideDto
	{
		[Key]
		[Required]
		public int SulfideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportCommonDataDto
	{
		[Key]
		public int CommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}

	public class FluidsReportDensityDto
	{
		[Key]
		public int DensityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTempVisDto
	{
		[Key]
		public int TempVisId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPvDto
	{
		[Key]
		public int ReportPvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportYpDto
	{
		[Key]
		public int ReportYpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportGel10SecDto
	{
		[Key]
		public int Gel10SecId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportGel10MinDto
	{
		[Key]
		public int Gel10MinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportChlorideDto
	{
		[Key]
		public int ChlorideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class FluidsReportFluidDto
	{
		[Key]
		public string Uid { get; set; }
		public string Type { get; set; }
		public string LocationSample { get; set; }
		public FluidsReportDensityDto Density { get; set; }
		public FluidsReportVisFunnelDto VisFunnel { get; set; }
		public FluidsReportTempVisDto TempVis { get; set; }
		public FluidsReportPvDto Pv { get; set; }
		public FluidsReportYpDto Yp { get; set; }
		public FluidsReportGel10SecDto Gel10Sec { get; set; }
		public FluidsReportGel10MinDto Gel10Min { get; set; }
		public FluidsReportGel30MinDto Gel30Min { get; set; }
		public FluidsReportFilterCakeLtlpDto FilterCakeLtlp { get; set; }
		public FluidsReportFiltrateLtlpDto FiltrateLtlp { get; set; }
		public FluidsReportTempHthpDto TempHthp { get; set; }
		public FluidsReportPresHthpDto PresHthp { get; set; }
		public FluidsReportFiltrateHthpDto FiltrateHthp { get; set; }
		public FluidsReportFilterCakeHthpDto FilterCakeHthp { get; set; }
		public FluidsReportSolidsPcDto SolidsPc { get; set; }
		public FluidsReportWaterPcDto WaterPc { get; set; }
		public FluidsReportOilPcDto OilPc { get; set; }
		public FluidsReportSandPcDto SandPc { get; set; }
		public FluidsReportSolidsLowGravPcDto SolidsLowGravPc { get; set; }
		public FluidsReportSolidsCalcPcDto SolidsCalcPc { get; set; }
		public FluidsReportBaritePcDto BaritePc { get; set; }
		public FluidsReportLcmDto Lcm { get; set; }
		public FluidsReportMbtDto Mbt { get; set; }
		public string Ph { get; set; }
		public FluidsReportTempPhDto TempPh { get; set; }
		public FluidsReportPmDto Pm { get; set; }
		public FluidsReportPmFiltrateDto PmFiltrate { get; set; }
		public FluidsReportMfDto Mf { get; set; }
		public FluidsReportAlkalinityP1Dto AlkalinityP1 { get; set; }
		public FluidsReportAlkalinityP2Dto AlkalinityP2 { get; set; }
		public FluidsReportChlorideDto Chloride { get; set; }
		public FluidsReportCalciumDto Calcium { get; set; }
		public FluidsReportMagnesiumDto Magnesium { get; set; }
		public FluidsReportPotassiumDto Potassium { get; set; }
		public List<FluidsReportRheometerDto> Rheometer { get; set; }
		public FluidsReportBrinePcDto BrinePc { get; set; }
		public FluidsReportLimeDto Lime { get; set; }
		public FluidsReportElectStabDto ElectStab { get; set; }
		public FluidsReportCalciumChlorideDto CalciumChloride { get; set; }
		public string Company { get; set; }
		public string Engineer { get; set; }
		public string Asg { get; set; }
		public FluidsReportSolidsHiGravPcDto SolidsHiGravPc { get; set; }
		public FluidsReportPolymerDto Polymer { get; set; }
		public string PolyType { get; set; }
		public FluidsReportSolCorPcDto SolCorPc { get; set; }
		public FluidsReportOilCtgDto OilCtg { get; set; }
		public FluidsReportHardnessCaDto HardnessCa { get; set; }
		public FluidsReportSulfideDto Sulfide { get; set; }
		public string Comments { get; set; }

	}

	public class FluidsReportMdDto
	{
		[Key]
		[Required]
		public int MdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTvdDto
	{
		[Key]
		[Required]
		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class FluidsReportDto
	{
		[Key]
		[Required]
		public int FluidsReportId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string DTim { get; set; }
		public FluidsReportMdDto Md { get; set; }
		public FluidsReportTvdDto Tvd { get; set; }
		public string NumReport { get; set; }
		public FluidsReportFluidDto Fluid { get; set; }
		public FluidsReportCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class FluidsReports
	{

		public FluidsReportDto FluidsReport { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}


}
