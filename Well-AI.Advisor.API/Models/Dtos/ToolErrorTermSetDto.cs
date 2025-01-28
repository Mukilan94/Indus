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

	public class AuthorizationDto
	{
		[Key]
		public int AuthorizationId { get; set; }
		public string Author { get; set; }
		public string Source { get; set; }
		public string Authority { get; set; }
		public string Status { get; set; }
		public string Version { get; set; }
		public string Comment { get; set; }
	}

	public class FunctionDto
	{
		[Key]
		public int FunctionId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class ToolErrorTermSetParameterDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class NomenclatureDto
	{
		[Key]
		public int NomenclatureId { get; set; }
		public List<ToolErrorTermSetParameterDto> Parameter { get; set; }
		public List<FunctionDto> Function { get; set; }
		public ConstantDto Constant { get; set; }
	}

	public class ErrorCoefficientDto
	{
		[Key]
		public int ErrorCoefficientId { get; set; }
		public string Azi { get; set; }
		public string Uid { get; set; }
		public string Inc { get; set; }
		public string Depth { get; set; }
		public string Tvd { get; set; }
	}

	public class ErrorTermDto
	{
		[Key]
		public int ErrorTermId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string MeasureClass { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public List<ErrorCoefficientDto> ErrorCoefficient { get; set; }
		public string Uid { get; set; }
		public string OperatingMode { get; set; }
	}

	public class ToolErrorTermSetDto
	{
		[Key]
		public int ToolErrorTermSetId { get; set; }
		public string Name { get; set; }
		public AuthorizationDto Authorization { get; set; }
		public NomenclatureDto Nomenclature { get; set; }
		public List<ErrorTermDto> ErrorTerm { get; set; }
		[XmlAttribute(AttributeName = "uid")]
		public string Uid { get; set; }
	}

	public class ConstantDto
	{
		[Key]
		public int ConstantId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string Unit { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class ToolErrorTermSetsDto
	{
		public List<ToolErrorTermSetDto> ToolErrorTermSet { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Witsml { get; set; }
		public string Xmlns { get; set; }
	}

}
