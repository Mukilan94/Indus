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


	public class WbGeometryGapAirDto
	{
		[Key]
		public int GapAirId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryDepthWaterMeanDto
	{
		[Key]
		public int DepthWaterMeanId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class WbGeometryIdSectionDto
	{
		[Key]
		public int IdSectionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryOdSectionDto
	{
		[Key]
		public int OdSectionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryWtPerLenDto
	{
		[Key]
		public int WtPerLenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryDiaDriftDto
	{
		[Key]
		public int DiaDriftId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryMdBottomDto
	{
		[Key]
		public int DiaDriftId { get; set; }
		public int MdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryMdTopDto
	{
		[Key]
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryTvdTopDto
	{
		[Key]
		public int TvdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryTvdBottomDto
	{
		[Key]
		public int TvdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}





	public class WbGeometrySectionDto
	{
		[Key]
		public int WbGeometrySectionId { get; set; }
		public string TypeHoleCasing { get; set; }
		public WbGeometryMdTopDto MdTop { get; set; }
		public WbGeometryMdBottomDto MdBottom { get; set; }
		public WbGeometryTvdTopDto TvdTop { get; set; }
		public WbGeometryTvdBottomDto TvdBottom { get; set; }
		public WbGeometryIdSectionDto IdSection { get; set; }
		public WbGeometryOdSectionDto OdSection { get; set; }
		public WbGeometryWtPerLenDto WtPerLen { get; set; }
		public string Grade { get; set; }
		public string CurveConductor { get; set; }
		public WbGeometryDiaDriftDto DiaDrift { get; set; }
		public string FactFric { get; set; }
		public string Uid { get; set; }
	}



	public class WbGeometryDto
	{
		[Key]
		public int WbGeometryId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string DTimReport { get; set; }
		public WbGeometryMdBottomDto MdBottom { get; set; }
		public WbGeometryGapAirDto GapAir { get; set; }
		public WbGeometryDepthWaterMeanDto DepthWaterMean { get; set; }
		public WbGeometrySectionDto WbGeometrySection { get; set; }
		public WbGeometryCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	public class WbGeometryCommonDataDto
	{
		[Key]

		public int WbGeometryCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
 
}
