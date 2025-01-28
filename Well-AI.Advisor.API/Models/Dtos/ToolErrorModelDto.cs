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
	public class OperatingConditionDto
	{
		[Key]
		public int OperatingConditionId { get; set; }
		public string Parameter { get; set; }
		public string Value { get; set; }
		public string Uid { get; set; }
		public MinDto Min { get; set; }
		public MaxDto Max { get; set; }
	}

	public class UseErrorTermSetDto
	{
		[Key]
		public int UseErrorTermSetId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class TermDto
	{
		[Key]
		public int TermId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class ToolErrorValueDto
	{
		[Key]
		public int ValueId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class ErrorTermValueDto
	{
		[Key]
		public int ErrorTermValueId { get; set; }
		public TermDto Term { get; set; }
		public string Prop { get; set; }
		public ToolErrorValueDto Value { get; set; }
		public string Uid { get; set; }
		
		public string  Bias { get; set; }
		public string  Comment { get; set; }
	}

	public class ToolErrorModelCommonDataDto
	{
		[Key]
		public int ToolErrorModelCommonDataId { get; set; }
		public string DTimLastChange { get; set; }
	}
	public class ToolErrorModelDto
	{
		[Key]
		public int ToolErrorModelId { get; set; }
		public string Name { get; set; }
		public AuthorizationDto Authorization { get; set; }
		public OperatingConditionDto OperatingCondition { get; set; }
		public UseErrorTermSetDto UseErrorTermSet { get; set; }
		public List<ErrorTermValueDto> ErrorTermValue { get; set; }
		public string Uid { get; set; }
		public List<OperatingIntervalDto> OperatingInterval { get; set; }
		public ModelParametersDto ModelParameters { get; set; }
		public ToolErrorModelCommonDataDto CommonData { get; set; }
	}

	public class StartDto
	{
		[Key]
		public int StartId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class EndDto
	{
		[Key]
		public int EndId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class OperatingIntervalDto
	{
		[Key]
		public int OperatingIntervalId { get; set; }
		public string Mode { get; set; }
		public StartDto Start { get; set; }
		public EndDto End { get; set; }
		public string Uid { get; set; }
		public SpeedDto Speed { get; set; }
	}

	public class SpeedDto
	{
		[Key]
		public int SpeedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class GyroInitializationDto
	{
		[Key]
		public int GyroInitializationId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class GyroReinitializationDistanceDto
	{
		[Key]
		public int GyroReinitializationDistanceId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ModelParametersDto
	{
		[Key]
		public int ModelParametersId { get; set; }
		public string MisalignmentMode { get; set; }
		public GyroInitializationDto GyroInitialization { get; set; }
		public GyroReinitializationDistanceDto GyroReinitializationDistance { get; set; }
		public string NoiseReductionFactor { get; set; }
		public string Switching { get; set; }
	}

	public class MinDto
	{
		[Key]
		public int MinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxDto
	{
		[Key]
		public int MaxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ToolErrorModelsDto
	{
		public List<ToolErrorModelDto> ToolErrorModel { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Witsml { get; set; }
		public string Xmlns { get; set; }
	}


}
