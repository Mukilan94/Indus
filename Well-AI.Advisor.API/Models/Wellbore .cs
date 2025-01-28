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
	

	public class TvdKickoff {
		[Key]

		public int TvdKickoffId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdPlanned {
		[Key]

		public int TvdPlannedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdSubSeaPlanned {
		[Key]

		public int MdSubSeaPlannedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TvdSubSeaPlanned {
		[Key]

		public int TvdSubSeaPlannedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DayTarget {
		[Key]

		public int DayTargetId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}


	public class WellboreMd
	{
		[Key]

		public int MdId { get; set; }
		public string Uom { get; set; }
		public string Text { get; set; }
	}

	public class WellboreTvd
	{
		[Key]

		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellboreMdKickoff
	{
		[Key]

		public int MdKickoffId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellboreMdPlanned
	{
		[Key]

		public int MdPlannedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class WellBore {
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
		public WellboreMd Md { get; set; }
		public WellboreTvd Tvd { get; set; }
		public WellboreMdKickoff MdKickoff { get; set; }
		public TvdKickoff TvdKickoff { get; set; }
		public WellboreMdPlanned MdPlanned { get; set; }
		public TvdPlanned TvdPlanned { get; set; }
		public MdSubSeaPlanned MdSubSeaPlanned { get; set; }
		public TvdSubSeaPlanned TvdSubSeaPlanned { get; set; }
		public DayTarget DayTarget { get; set; }
		public WellBoreCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWell { get; set; }
		 
	}

	public class WellBoreCommonData
	{
		[Key]

		public int WellBoreCommonDataId { get; set; }
		public DateTime DTimCreation { get; set; }
		public DateTime DTimLastChange { get; set; }
		public string ItemState { get; set; }
		public string Comments { get; set; }
	}

	public class ParentWellbore {
		[Key]
		public int ParentWellboreId { get; set; }
		public string UidRef { get; set; }
		
		public string Text { get; set; }
	}
 

}
