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


	public class FormationMarkerMdPrognosed
	{
		[Key]
		public int MdPrognosedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerTvdPrognosed
	{
		[Key]
		public int TvdPrognosedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerMdTopSample
	{
		[Key]
		public int MdTopSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerTvdTopSample
	{
		[Key]
		public int TvdTopSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerThicknessBed
	{
		[Key]
		public int ThicknessBedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerThicknessApparent
	{
		[Key]
		public int ThicknessApparentId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerThicknessPerpen
	{
		[Key]
		public int ThicknessPerpenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerMdLogSample
	{
		[Key]
		public int MdLogSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerTvdLogSample
	{
		[Key]
		public int TvdLogSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerDip
	{
		[Key]
		public int DipId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerDipDirection
	{
		[Key]
		public int DipDirectionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerCommonData
	{
		[Key]
		public int FormationMarkerCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}


	public class FormationMarkerLithostratigraphic
	{
		[Key]
		public int LithostratigraphicId { get; set; }
		public string Kind { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerChronostratigraphic
	{
		[Key]
		public int ChronostratigraphicId { get; set; }
		public string Kind { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarker
	{
		[Key]
		public int FormationMarkerId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public FormationMarkerMdPrognosed MdPrognosed { get; set; }
		public FormationMarkerTvdPrognosed TvdPrognosed { get; set; }
		public FormationMarkerMdTopSample MdTopSample { get; set; }
		public FormationMarkerTvdTopSample TvdTopSample { get; set; }
		public FormationMarkerThicknessBed ThicknessBed { get; set; }
		public FormationMarkerThicknessApparent ThicknessApparent { get; set; }
		public FormationMarkerThicknessPerpen ThicknessPerpen { get; set; }
		public FormationMarkerMdLogSample MdLogSample { get; set; }
		public FormationMarkerTvdLogSample TvdLogSample { get; set; }
		public FormationMarkerDip Dip { get; set; }
		public FormationMarkerDipDirection DipDirection { get; set; }
		public FormationMarkerLithostratigraphic Lithostratigraphic { get; set; }
		public FormationMarkerChronostratigraphic Chronostratigraphic { get; set; }
		public string Description { get; set; }
		public FormationMarkerCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
 

}
