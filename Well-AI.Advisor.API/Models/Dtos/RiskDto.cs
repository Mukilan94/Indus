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
	public class RiskMdHoleEndDto
	{
		[Key]
		public int MdHoleEndId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RiskTvdHoleStartDto
	{
		[Key]
		public int TvdHoleStartId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RiskTvdHoleEndDto
	{
		[Key]
		public int TvdHoleEndId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RiskDiaHoleDto
	{
		[Key]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RiskObjectReferenceDto
	{
		[Key]
		public int ObjectReferenceId { get; set; }
		public string UidRef { get; set; }
		public string Object { get; set; }

		public string Text { get; set; }
	}


	public class RiskMdHoleStartDto
	{

		[Key]
		public int MdHoleStartId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class RiskDto
	{
		[Key]
		public int RiskId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public RiskObjectReferenceDto ObjectReference { get; set; }
		public string Type { get; set; }
		public string Category { get; set; }
		public string SubCategory { get; set; }
		public RiskMdHoleStartDto MdHoleStart { get; set; }
		public RiskMdHoleEndDto  MdHoleEnd { get; set; }
		public RiskTvdHoleStartDto TvdHoleStart { get; set; }
		public RiskTvdHoleEndDto TvdHoleEnd { get; set; }
		public RiskDiaHoleDto DiaHole { get; set; }
		public string SeverityLevel { get; set; }
		public string ProbabilityLevel { get; set; }
		public string Summary { get; set; }
		public string Details { get; set; }
		public string Identification { get; set; }
		public string Mitigation { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class Risks
	{
		public RiskDto Risk { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Xmlns { get; set; }
	}


}
