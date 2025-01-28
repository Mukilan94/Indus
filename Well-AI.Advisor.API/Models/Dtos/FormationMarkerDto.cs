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

	public class FormationMarkerMdPrognosedDto
	{
		[Key]
		public int MdPrognosedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerTvdPrognosedDto
	{
		[Key]
		public int TvdPrognosedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerMdTopSampleDto
	{
		[Key]
		public int MdTopSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerTvdTopSampleDto
	{
		[Key]
		public int TvdTopSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerThicknessBedDto
	{
		[Key]
		public int ThicknessBedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerThicknessApparentDto
	{
		[Key]
		public int ThicknessApparentId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerThicknessPerpenDto
	{
		[Key]
		public int ThicknessPerpenId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerMdLogSampleDto
	{
		[Key]
		public int MdLogSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerTvdLogSampleDto
	{
		[Key]
		public int TvdLogSampleId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerDipDto
	{
		[Key]
		public int DipId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerDipDirectionDto
	{
		[Key]
		public int DipDirectionId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerCommonDataDto
	{
		[Key]
		public int FormationMarkerCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}


	public class FormationMarkerLithostratigraphicDto
	{
		[Key]
		public int LithostratigraphicId { get; set; }
		public string Kind { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerChronostratigraphicDto
	{
		[Key]
		public int ChronostratigraphicId { get; set; }
		public string Kind { get; set; }

		public string Text { get; set; }
	}

	public class FormationMarkerDto
	{
		[Key]
		public int FormationMarkerId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public FormationMarkerMdPrognosedDto MdPrognosed { get; set; }
		public FormationMarkerTvdPrognosedDto TvdPrognosed { get; set; }
		public FormationMarkerMdTopSampleDto MdTopSample { get; set; }
		public FormationMarkerTvdTopSampleDto TvdTopSample { get; set; }
		public FormationMarkerThicknessBedDto ThicknessBed { get; set; }
		public FormationMarkerThicknessApparentDto ThicknessApparent { get; set; }
		public FormationMarkerThicknessPerpenDto ThicknessPerpen { get; set; }
		public FormationMarkerMdLogSampleDto MdLogSample { get; set; }
		public FormationMarkerTvdLogSampleDto TvdLogSample { get; set; }
		public FormationMarkerDipDto Dip { get; set; }
		public FormationMarkerDipDirectionDto DipDirection { get; set; }
		public FormationMarkerLithostratigraphicDto Lithostratigraphic { get; set; }
		public FormationMarkerChronostratigraphicDto Chronostratigraphic { get; set; }
		public string Description { get; set; }
		public FormationMarkerCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
 
}
