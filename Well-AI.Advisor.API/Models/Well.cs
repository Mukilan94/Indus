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

	public class WellPcInterest
	{
		[Key]
		public int PcInterestId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellDatumName
	{
		[Key]
		public int DatumNameId { get; set; }
		public string Code { get; set; }
		public string NamingSystem { get; set; }

		public string Text { get; set; }
	}

	public class WellGroundElevation
	{
		[Key]
		public int GroundElevationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellWaterDepth
	{
		[Key]
		public int WaterDepthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellGeodeticCRS
	{
		[Key]
		public int GeodeticCRSId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class WellCRS
	{
		[Key]
		public int WellCRSUid { get; set; }
		public string Uid { get; set; }
		public string UidRef { get; set; }
		public string Name { get; set; }
		public WellGeodeticCRS GeodeticCRS { get; set; }
		public string Description { get; set; }
		public WellMapProjectionCRS MapProjectionCRS { get; set; }
		public WellLocalCRS LocalCRS { get; set; }
		public string Text { get; set; }

	}

	public class WellEasting
	{
		[Key]
		public int EastingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellNorthing
	{
		[Key]
		public int NorthingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellLatitude
	{
		[Key]
		public int LatitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellLongitude
	{
		[Key]
		public int LongitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellLocation
	{
		[Key]
		public int LocationId { get; set; }
		public WellCRS WellCRS { get; set; }
		public WellEasting Easting { get; set; }
		public WellNorthing Northing { get; set; }
		public string Uid { get; set; }
		public WellLocalX LocalX { get; set; }
		public WellLocalY LocalY { get; set; }
		public string Description { get; set; }
		public WellLatitude Latitude { get; set; }
		public WellLongitude Longitude { get; set; }
	}

	public class WellLocalX
	{
		[Key]
		public int LocalXId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellLocalY
	{
		[Key]
		public int LocalYId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class WellElevation
	{
		[Key]
		public int ElevationId { get; set; }
		public string Uom { get; set; }
		public string Datum { get; set; }

		public string Text { get; set; }
	}
	public class WellReferencePoint
	{
		[Key]
		public int ReferencePointId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public List<WellLocation> Location { get; set; }
		public string Uid { get; set; }
		public WellElevation Elevation { get; set; }
		public WellMeasuredDepth MeasuredDepth { get; set; }
	}

	public class WellMeasuredDepth
	{
		[Key]
		public int MeasuredDepthId { get; set; }
		public string Uom { get; set; }
		public string Datum { get; set; }

		public string Text { get; set; }
	}

	public class WellMapProjectionCRS
	{
		[Key]
		public int MapProjectionCRSId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class WellYAxisAzimuth
	{
		[Key]
		public int YAxisAzimuthId { get; set; }
		public string Uom { get; set; }
		public string NorthDirection { get; set; }

		public string Text { get; set; }
	}

	public class WellLocalCRS
	{
		[Key]
		public int LocalCRSId { get; set; }
		public string UsesWellAsOrigin { get; set; }
		public WellYAxisAzimuth YAxisAzimuth { get; set; }
		public string XRotationCounterClockwise { get; set; }
	}

	public class WellDefaultDatum
	{
		[Key]
		public int DefaultDatumId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}


	public class WellCommonData
	{
		[Key]
		public int CommonDataId { get; set; }
		public string DTimCreation { get; set; }
		public string DTimLastChange { get; set; }
		public string ItemState { get; set; }
		public string Comments { get; set; }
		public WellDefaultDatum DefaultDatum { get; set; }
	}

	public class WellheadElevation
	{
		[Key]
		public int ElevationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class WellDatum
	{
		[Key]
		public int DatumId { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public WellElevation Elevation { get; set; }
		public string Uid { get; set; }
		public WellDatumName DatumName { get; set; }
	}

	public class Well
	{
		[Key]
		public int WellId { get; set; }
		public string Name { get; set; }
		public string NameLegal { get; set; }
		public string NumLicense { get; set; }
		public string NumGovt { get; set; }
		public string DTimLicense { get; set; }
		public string Field { get; set; }
		public string Country { get; set; }
		public string State { get; set; }
		public string County { get; set; }
		public string Region { get; set; }
		public string District { get; set; }
		public string Block { get; set; }
		public string TimeZone { get; set; }
		public string Operator { get; set; }
		public string OperatorDiv { get; set; }
		public WellPcInterest PcInterest { get; set; }
		public string NumAPI { get; set; }
		public string StatusWell { get; set; }
		public string PurposeWell { get; set; }
		public string DTimSpud { get; set; }
		public string DTimPa { get; set; }
		public WellheadElevation WellheadElevation { get; set; }
		public List<WellDatum> WellDatum { get; set; }
		public WellGroundElevation GroundElevation { get; set; }
		public WellWaterDepth WaterDepth { get; set; }
		public WellLocation WellLocation { get; set; }
		public List<WellReferencePoint> ReferencePoint { get; set; }
		public List<WellCRS> WellCRS { get; set; }
		public WellCommonData CommonData { get; set; }
		public string Uid { get; set; }
	}
 

}
