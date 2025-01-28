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
	public class TvdKickoffDto
	{
		[Key]

		public int TvdKickoffId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdPlannedDto
	{
		[Key]

		public int TvdPlannedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MdSubSeaPlannedDto
	{
		[Key]

		public int MdSubSeaPlannedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TvdSubSeaPlannedDto
	{
		[Key]

		public int TvdSubSeaPlannedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class DayTargetDto
	{
		[Key]

		public int DayTargetId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellBoreCommonDataDto
	{
		public int WellBoreCommonDataId { get; set; }
		public string DTimCreation { get; set; }
		public string DTimLastChange { get; set; }
		public string ItemState { get; set; }
		public string Comments { get; set; }
	}

	public class WellboreMdDto
	{
		[Key]

		public int MdId { get; set; }
		public string Uom { get; set; }
		public string Text { get; set; }
	}

	public class WellboreTvdDto
	{
		[Key]

		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellboreMdKickoffDto
	{
		[Key]

		public int MdKickoffId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellboreMdPlannedDto
	{
		[Key]

		public int MdPlannedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class WellBoreDto
	{
		[Key]

		public int WellboreId { get; set; }
		public string NameWell { get; set; }
		public string Name { get; set; }
		public string Number { get; set; }
		public string SuffixAPI { get; set; }
		public string NumGovt { get; set; }
		public string StatusWellbore { get; set; }
		public string PurposeWellbore { get; set; }
		public string TypeWellbore { get; set; }
		public string Shape { get; set; }
		public string DTimKickoff { get; set; }
		public WellboreMdDto Md { get; set; }
		public WellboreTvdDto Tvd { get; set; }
		public WellboreMdKickoffDto MdKickoff { get; set; }
		public TvdKickoffDto TvdKickoff { get; set; }
		public WellboreMdPlannedDto MdPlanned { get; set; }
		public TvdPlannedDto TvdPlanned { get; set; }
		public MdSubSeaPlannedDto MdSubSeaPlanned { get; set; }
		public TvdSubSeaPlannedDto TvdSubSeaPlanned { get; set; }
		public DayTargetDto DayTarget { get; set; }
		public WellBoreCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWell { get; set; }
		public ParentWellboreDto ParentWellbore { get; set; }
	}

	public class ParentWellboreDto
	{
		[Key]
		public int ParentWellboreId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}
 

}
