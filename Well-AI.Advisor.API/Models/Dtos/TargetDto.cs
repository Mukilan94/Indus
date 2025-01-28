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
	public class DispNsCenterDto
	{
		[Key]
		public int DispNsCenterId { get; set; }

		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DispEwCenterDto
	{
		[Key]
		public int DispEwCenterId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class DispNsOffsetDto
	{
		[Key]
		public int DispNsOffsetId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DispEwOffsetDto
	{
		[Key]
		public int DispEwOffsetId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ThickAboveDto
	{
		[Key]
		public int ThickAboveId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ThickBelowDto
	{
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class StrikeDto
	{
		[Key]
		public int StrikeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RotationDto
	{
		[Key]
		public int RotationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LenMajorAxisDto
	{
		[Key]
		public int LenMajorAxisId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WidMinorAxisDto
	{
		[Key]
		public int WidMinorAxisId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DispNsSectOrigDto
	{
		[Key]
		public int DispNsSectOrigId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DispEwSectOrigDto
	{
		[Key]
		public int DispEwSectOrigId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class TargetLatitudeDto
	{
		[Key]
		public int LatitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TargetLongitudeDto
	{
		[Key]
		public int LongitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TargetWellCRSDto
	{
		[Key]
		public int WellCRSId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}
	public class TargetLocationDto
	{
		[Key]
		public int LocationId { get; set; }
		public TargetWellCRSDto WellCRS { get; set; }
		public TargetLatitudeDto Latitude { get; set; }
		public TargetLongitudeDto Longitude { get; set; }
		public string Uid { get; set; }
		public ProjectedXDto ProjectedX { get; set; }
		public ProjectedYDto ProjectedY { get; set; }
	}
	public class ProjectedXDto
	{
		[Key]
		public int ProjectedXId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ProjectedYDto
	{
		[Key]
		public int ProjectedYId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LenRadiusDto
	{
		[Key]
		public int LenRadiusId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class AngleArcDto
	{
		[Key]
		public int AngleArcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TargetSectionDto
	{
		[Key]
		public int TargetSectionId { get; set; }
		public string SectNumber { get; set; }
		public string TypeTargetSectionScope { get; set; }
		public LenRadiusDto LenRadius { get; set; }
		public AngleArcDto AngleArc { get; set; }
		public ThickAboveDto ThickAbove { get; set; }
		public ThickBelowDto ThickBelow { get; set; }
		public List<TargetLocationDto> Location { get; set; }
		public string Uid { get; set; }
	}

	public class TargetCommonDataDto
	{
		public string ItemState { get; set; }
		public string Comments { get; set; }
	}


	public class TargetTvdDto
	{
		[Key]
		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TargetDipDto
	{
		[Key]
		public int DipId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class TargetDto
	{
		[Key]
		public int TargetId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public DispNsCenterDto DispNsCenter { get; set; }
		public DispEwCenterDto DispEwCenter { get; set; }
		public TargetTvdDto Tvd { get; set; }
		public DispNsOffsetDto DispNsOffset { get; set; }
		public DispEwOffsetDto DispEwOffset { get; set; }
		public ThickAboveDto ThickAbove { get; set; }
		public ThickBelowDto ThickBelow { get; set; }
		public TargetDipDto Dip { get; set; }
		public StrikeDto Strike { get; set; }
		public RotationDto Rotation { get; set; }
		public LenMajorAxisDto LenMajorAxis { get; set; }
		public WidMinorAxisDto WidMinorAxis { get; set; }
		public string TypeTargetScope { get; set; }
		public DispNsSectOrigDto DispNsSectOrig { get; set; }
		public DispEwSectOrigDto DispEwSectOrig { get; set; }
		public string AziRef { get; set; }
		public string CatTarg { get; set; }
		public List<TargetLocationDto> Location { get; set; }
		public List<TargetSectionDto> TargetSection { get; set; }
		public TargetCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
 
}
