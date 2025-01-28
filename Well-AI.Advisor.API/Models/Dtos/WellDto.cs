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
	public class WellPcInterestDto
	{
		[Key]
		public int PcInterestId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	
	public class WellDatumNameDto
	{
		[Key]
		public int DatumNameId { get; set; }
		public string Code { get; set; }
		public string NamingSystem { get; set; }

		public string Text { get; set; }
	}

	public class WellGroundElevationDto
	{
		[Key]
		public int GroundElevationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellWaterDepthDto
	{
		[Key]
		public int WaterDepthId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellGeodeticCRSDto
	{
		[Key]
		public int GeodeticCRSId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class WellCRSDto
	{
		[Key]
		public int WellCRSUid { get; set; }
		public string Uid { get; set; }
		public string UidRef { get; set; }
		public string Name { get; set; }
		public WellGeodeticCRSDto GeodeticCRS { get; set; }
		public string Description { get; set; }
		public WellMapProjectionCRSDto MapProjectionCRS { get; set; }
		public WellLocalCRSDto LocalCRS { get; set; }
		public string Text { get; set; }

	}

	public class WellEastingDto
	{
		[Key]
		public int EastingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellNorthingDto
	{
		[Key]
		public int NorthingId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	

	public class WellLatitudeDto
	{
		[Key]
		public int LatitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellLongitudeDto
	{
		[Key]
		public int LongitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellLocationDto
	{
		[Key]
		public int LocationId { get; set; }
		public WellCRSDto WellCRS { get; set; }
		public WellEastingDto Easting { get; set; }
		public WellNorthingDto Northing { get; set; }
		public string Uid { get; set; }
		public WellLocalXDto LocalX { get; set; }
		public WellLocalYDto LocalY { get; set; }
		public string Description { get; set; }
		public WellLatitudeDto Latitude { get; set; }
		public WellLongitudeDto Longitude { get; set; }
	}

	public class WellLocalXDto
	{
		[Key]
		public int LocalXId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellLocalYDto
	{
		[Key]
		public int LocalYId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class WellElevationDto
	{
		[Key]
		public int ElevationId { get; set; }
		public string Uom { get; set; }
		public string Datum { get; set; }

		public string Text { get; set; }
	}
	public class WellReferencePointDto
	{
		[Key]
		public int ReferencePointId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public List<WellLocationDto> Location { get; set; }
		public string Uid { get; set; }
		public WellElevationDto Elevation { get; set; }
		public WellMeasuredDepthDto MeasuredDepth { get; set; }
	}

	public class WellMeasuredDepthDto
	{
		[Key]
		public int MeasuredDepthId { get; set; }
		public string Uom { get; set; }
		public string Datum { get; set; }

		public string Text { get; set; }
	}

	public class WellMapProjectionCRSDto
	{
		[Key]
		public int MapProjectionCRSId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class WellYAxisAzimuthDto
	{
		[Key]
		public int YAxisAzimuthId { get; set; }
		public string Uom { get; set; }
		public string NorthDirection { get; set; }

		public string Text { get; set; }
	}

	public class WellLocalCRSDto
	{
		[Key]
		public int LocalCRSId { get; set; }
		public string UsesWellAsOrigin { get; set; }
		public WellYAxisAzimuthDto YAxisAzimuth { get; set; }
		public string XRotationCounterClockwise { get; set; }
	}

	public class WellDefaultDatumDto
	{
		[Key]
		public int DefaultDatumId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}


	public class WellCommonDataDto
	{
		[Key]
		public int CommonDataId { get; set; }
		public string DTimCreation { get; set; }
		public string DTimLastChange { get; set; }
		public string ItemState { get; set; }
		public string Comments { get; set; }
		public WellDefaultDatumDto DefaultDatum { get; set; }
	}

	public class WellheadElevationDto
	{
		[Key]
		public int ElevationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class WellDatumDto
	{
		[Key]
		public int DatumId { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public WellElevationDto Elevation { get; set; }
		public string Uid { get; set; }
		public WellDatumNameDto DatumName { get; set; }
	}

	public class WellDto
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
		public WellPcInterestDto PcInterest { get; set; }
		public string NumAPI { get; set; }
		public string StatusWell { get; set; }
		public string PurposeWell { get; set; }
		public string DTimSpud { get; set; }
		public string DTimPa { get; set; }
		public WellheadElevationDto WellheadElevation { get; set; }
		public List<WellDatumDto> WellDatum { get; set; }
		public WellGroundElevationDto GroundElevation { get; set; }
		public WellWaterDepthDto WaterDepth { get; set; }
		public WellLocationDto WellLocation { get; set; }
		public List<WellReferencePointDto> ReferencePoint { get; set; }
		public List<WellCRSDto> WellCRS { get; set; }
		public WellCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
	}
 

}
