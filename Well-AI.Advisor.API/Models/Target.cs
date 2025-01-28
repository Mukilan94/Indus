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
	
	public class DispNsCenter {
		[Key]
		public int DispNsCenterId { get; set; }
		
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DispEwCenter {
		[Key]
		public int DispEwCenterId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	
	public class DispNsOffset {
		[Key]
		public int DispNsOffsetId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DispEwOffset {
		[Key]
		public int DispEwOffsetId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ThickAbove {
		[Key]
		public int ThickAboveId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ThickBelow {
		[Key]
		public int ThickBelowId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}


	public class Strike {
		[Key]
		public int StrikeId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class Rotation {
		[Key]
		public int RotationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class LenMajorAxis {
		[Key]
		public int LenMajorAxisId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class WidMinorAxis {
		[Key]
		public int WidMinorAxisId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DispNsSectOrig {
		[Key]
		public int DispNsSectOrigId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DispEwSectOrig {
		[Key]
		public int DispEwSectOrigId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	
	

	public class TargetLocation
	{
		[Key]
		public int TargetLocationId { get; set; }
		public TargetWellCRS WellCRS { get; set; }
		public TargetLatitude Latitude { get; set; }
		public TargetLongitude Longitude { get; set; }
		public string Uid { get; set; }
		public TargetProjectedX ProjectedX { get; set; }
		public TargetProjectedY ProjectedY { get; set; }
	}

	public class TargetProjectedX {
		[Key]
		public int ProjectedXId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TargetProjectedY {
		[Key]
		public int ProjectedYId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class LenRadius {
		[Key]
		public int LenRadiusId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AngleArc {
		[Key]
		public int AngleArcId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TargetSection {
		[Key]
		public int TargetSectionId { get; set; }
		public string SectNumber { get; set; }
		public string TypeTargetSectionScope { get; set; }
		public LenRadius LenRadius { get; set; }
		public AngleArc AngleArc { get; set; }
		public ThickAbove ThickAbove { get; set; }
		public ThickBelow ThickBelow { get; set; }
		public List<TargetLocation> Location { get; set; }
		public string Uid { get; set; }
	}

	public class TargetTvd
	{
		[Key]
		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TargetDip
	{
		[Key]
		public int DipId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TargetWellCRS
	{
		[Key]
		public int WellCRSId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class TargetLatitude
	{
		[Key]
		public int LatitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TargetLongitude
	{
		[Key]
		public int LongitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class Target {
		[Key]
		public int TargetId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public DispNsCenter DispNsCenter { get; set; }
		public DispEwCenter DispEwCenter { get; set; }
		public TargetTvd Tvd { get; set; }
		public DispNsOffset DispNsOffset { get; set; }
		public DispEwOffset DispEwOffset { get; set; }
		public ThickAbove ThickAbove { get; set; }
		public ThickBelow ThickBelow { get; set; }
		public TargetDip Dip { get; set; }
		public Strike Strike { get; set; }
		public Rotation Rotation { get; set; }
		public LenMajorAxis LenMajorAxis { get; set; }
		public WidMinorAxis WidMinorAxis { get; set; }
		public string TypeTargetScope { get; set; }
		public DispNsSectOrig DispNsSectOrig { get; set; }
		public DispEwSectOrig DispEwSectOrig { get; set; }
		public string AziRef { get; set; }
		public string CatTarg { get; set; }
		public List<TargetLocation> Location { get; set; }
		public List<TargetSection> TargetSection { get; set; }
		public TargetCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	public class TargetCommonData
	{
		[Key]
		public int TargetCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	 

}
