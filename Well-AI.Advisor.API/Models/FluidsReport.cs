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
	public class FluidsReportVisFunnel
	{
		[Key]
		[Required]

		public int VisFunnelId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportGel30Min
	{
		[Key]
		[Required]

		public int Gel30MinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportFilterCakeLtlp
	{
		[Key]
		[Required]

		public int FilterCakeLtlpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportFiltrateLtlp
	{
		[Key]
		[Required]
		public int FiltrateLtlpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTempHthp
	{
		[Key]
		[Required]

		public int TempHthpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPresHthp
	{
		[Key]
		[Required]
		public int PresHthpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportFiltrateHthp
	{
		[Key]
		[Required]
		public int FiltrateHthpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportFilterCakeHthp
	{
		[Key]
		[Required]
		public int FilterCakeHthpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolidsPc
	{
		[Key]
		[Required]

		public int SolidsPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportWaterPc
	{
		[Key]
		[Required]
		public int WaterPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportOilPc
	{
		[Key]
		[Required]
		public int OilPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSandPc
	{
		[Key]
		[Required]
		public int SandPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolidsLowGravPc
	{
		[Key]
		[Required]
		public int SolidsLowGravPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolidsCalcPc
	{
		[Key]
		[Required]
		public int SolidsCalcPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportBaritePc
	{
		[Key]
		[Required]
		public int BaritePcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportLcm
	{
		[Key]
		[Required]
		public int LcmId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportMbt
	{
		[Key]
		[Required]
		public int MbtId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTempPh
	{
		[Key]
		[Required]
		public int TempPhId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPm
	{
		[Key]
		[Required]

		public int PmId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPmFiltrate
	{
		[Key]
		[Required]
		public int PmFiltrateId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportMf
	{
		[Key]
		[Required]
		public int MfId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportAlkalinityP1
	{
		[Key]
		[Required]
		public int AlkalinityP1Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportAlkalinityP2
	{
		[Key]
		[Required]
		public int AlkalinityP2Id { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class FluidsReportCalcium
	{
		[Key]
		[Required]
		public int CalciumId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportMagnesium
	{
		[Key]
		[Required]
		public int MagnesiumId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPotassium
	{
		[Key]
		[Required]
		public int PotassiumId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTempRheom
	{
		[Key]
		[Required]
		public int TempRheomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPresRheom
	{
		[Key]
		[Required]
		public int PresRheomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportRheometer
	{
		[Key]
		[Required]
		public int RheometerId { get; set; }
		public FluidsReportTempRheom TempRheom { get; set; }
		public FluidsReportPresRheom PresRheom { get; set; }
		public string Vis3Rpm { get; set; }
		public string Vis6Rpm { get; set; }
		public string Vis100Rpm { get; set; }
		public string Vis200Rpm { get; set; }
		public string Vis300Rpm { get; set; }
		public string Vis600Rpm { get; set; }
		public string Uid { get; set; }
	}

	public class FluidsReportBrinePc
	{
		[Key]
		[Required]
		public int BrinePcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportLime
	{
		[Key]
		[Required]
		public int LimeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportElectStab
	{
		[Key]
		[Required]
		public int ElectStabId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportCalciumChloride
	{
		[Key]
		[Required]
		public int CalciumChlorideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolidsHiGravPc
	{
		[Key]
		[Required]
		public int SolidsHiGravPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPolymer
	{
		[Key]
		[Required]
		public int PolymerId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSolCorPc
	{
		[Key]
		[Required]
		public int SolCorPcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportOilCtg
	{
		[Key]
		[Required]
		public int OilCtgId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportHardnessCa
	{
		[Key]
		[Required]
		public int HardnessCaId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportSulfide
	{
		[Key]
		[Required]
		public int SulfideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportCommonData
	{
		[Key]
		public int CommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}

	public class FluidsReportDensity
	{
		[Key]
		public int DensityId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTempVis
	{
		[Key]
		public int TempVisId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportPv
	{
		[Key]
		public int ReportPvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportYp
	{
		[Key]
		public int ReportYpId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportGel10Sec
	{
		[Key]
		public int Gel10SecId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportGel10Min
	{
		[Key]
		public int Gel10MinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportChloride
	{
		[Key]
		public int ChlorideId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class FluidsReportFluid
	{
		[Key]
		public string Uid { get; set; }
		public string Type { get; set; }
		public string LocationSample { get; set; }
		public FluidsReportDensity Density { get; set; }
		public FluidsReportVisFunnel VisFunnel { get; set; }
		public FluidsReportTempVis TempVis { get; set; }
		public FluidsReportPv Pv { get; set; }
		public FluidsReportYp Yp { get; set; }
		public FluidsReportGel10Sec Gel10Sec { get; set; }
		public FluidsReportGel10Min Gel10Min { get; set; }
		public FluidsReportGel30Min Gel30Min { get; set; }
		public FluidsReportFilterCakeLtlp FilterCakeLtlp { get; set; }
		public FluidsReportFiltrateLtlp FiltrateLtlp { get; set; }
		public FluidsReportTempHthp TempHthp { get; set; }
		public FluidsReportPresHthp PresHthp { get; set; }
		public FluidsReportFiltrateHthp FiltrateHthp { get; set; }
		public FluidsReportFilterCakeHthp FilterCakeHthp { get; set; }
		public FluidsReportSolidsPc SolidsPc { get; set; }
		public FluidsReportWaterPc WaterPc { get; set; }
		public FluidsReportOilPc OilPc { get; set; }
		public FluidsReportSandPc SandPc { get; set; }
		public FluidsReportSolidsLowGravPc SolidsLowGravPc { get; set; }
		public FluidsReportSolidsCalcPc SolidsCalcPc { get; set; }
		public FluidsReportBaritePc BaritePc { get; set; }
		public FluidsReportLcm Lcm { get; set; }
		public FluidsReportMbt Mbt { get; set; }
		public string Ph { get; set; }
		public FluidsReportTempPh TempPh { get; set; }
		public FluidsReportPm Pm { get; set; }
		public FluidsReportPmFiltrate PmFiltrate { get; set; }
		public FluidsReportMf Mf { get; set; }
		public FluidsReportAlkalinityP1 AlkalinityP1 { get; set; }
		public FluidsReportAlkalinityP2 AlkalinityP2 { get; set; }
		public FluidsReportChloride Chloride { get; set; }
		public FluidsReportCalcium Calcium { get; set; }
		public FluidsReportMagnesium Magnesium { get; set; }
		public FluidsReportPotassium Potassium { get; set; }
		public List<FluidsReportRheometer> Rheometer { get; set; }
		public FluidsReportBrinePc BrinePc { get; set; }
		public FluidsReportLime Lime { get; set; }
		public FluidsReportElectStab ElectStab { get; set; }
		public FluidsReportCalciumChloride CalciumChloride { get; set; }
		public string Company { get; set; }
		public string Engineer { get; set; }
		public string Asg { get; set; }
		public FluidsReportSolidsHiGravPc SolidsHiGravPc { get; set; }
		public FluidsReportPolymer Polymer { get; set; }
		public string PolyType { get; set; }
		public FluidsReportSolCorPc SolCorPc { get; set; }
		public FluidsReportOilCtg OilCtg { get; set; }
		public FluidsReportHardnessCa HardnessCa { get; set; }
		public FluidsReportSulfide Sulfide { get; set; }
		public string Comments { get; set; }
		
	}

	public class FluidsReportMd
	{
		[Key]
		[Required]
		public int MdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FluidsReportTvd
	{
		[Key]
		[Required]
		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class FluidsReport
	{
		[Key]
		[Required]
		public int FluidsReportId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string DTim { get; set; }
		public FluidsReportMd Md { get; set; }
		public FluidsReportTvd Tvd { get; set; }
		public string NumReport { get; set; }
		public FluidsReportFluid Fluid { get; set; }
		public FluidsReportCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class FluidsReports
	{

		public FluidsReport FluidsReport { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}


}
