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
	

	public class WbGeometryGapAir {
		[Key]
		public int GapAirId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WbGeometryDepthWaterMean {
		[Key]
		public int DepthWaterMeanId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	
	public class WbGeometryIdSection {
		[Key]
		public int IdSectionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WbGeometryOdSection {
		[Key]
		public int OdSectionId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WbGeometryWtPerLen {
		[Key]
		public int WtPerLenId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WbGeometryDiaDrift {
		[Key]
		public int DiaDriftId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WbGeometryMdBottom
	{
		[Key]
		public int DiaDriftId { get; set; }
		public int MdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryMdTop
	{
		[Key]
		public int MdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryTvdTop
	{
		[Key]
		public int TvdTopId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WbGeometryTvdBottom
	{
		[Key]
		public int TvdBottomId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	

	

	public class WbGeometrySection {
		[Key]
		public int WbGeometrySectionId { get; set; }
		public string TypeHoleCasing { get; set; }
		public WbGeometryMdTop MdTop { get; set; }
		public WbGeometryMdBottom MdBottom { get; set; }
		public WbGeometryTvdTop TvdTop { get; set; }
		public WbGeometryTvdBottom TvdBottom { get; set; }
		public WbGeometryIdSection IdSection { get; set; }
		public WbGeometryOdSection OdSection { get; set; }
		public WbGeometryWtPerLen WtPerLen { get; set; }
		public string Grade { get; set; }
		public string CurveConductor { get; set; }
		public WbGeometryDiaDrift DiaDrift { get; set; }
		public string FactFric { get; set; }
		public string Uid { get; set; }
	}

	

	public class WbGeometry {
		[Key]
		public int WbGeometryId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string DTimReport { get; set; }
		public WbGeometryMdBottom MdBottom { get; set; }
		public WbGeometryGapAir GapAir { get; set; }
		public WbGeometryDepthWaterMean DepthWaterMean { get; set; }
		public WbGeometrySection WbGeometrySection { get; set; }
		public WbGeometryCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	public class WbGeometryCommonData
	{
		[Key]

		public int WbGeometryCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
 

}
