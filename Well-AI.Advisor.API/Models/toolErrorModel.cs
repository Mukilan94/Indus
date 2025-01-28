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
	

	public class OperatingCondition {
		[Key]
		public int OperatingConditionId { get; set; }
		public string Parameter { get; set; }
		public string Value { get; set; }
		public string Uid { get; set; }
		public Min Min { get; set; }
		public Max Max { get; set; }
	}

	public class ToolErrorValue
	{
		[Key]
		public int ValueId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class UseErrorTermSet {
		[Key]
		public int UseErrorTermSetId { get; set; }
		public string UidRef { get; set; }
		
		public string Text { get; set; }
	}

	public class Term {
		[Key]
		public int TermId { get; set; }
		public string UidRef { get; set; }
		
		public string Text { get; set; }
	}

	
	public class ErrorTermValue {
		[Key]
		public int ErrorTermValueId { get; set; }
		public Term Term { get; set; }
		public string Prop { get; set; }
		public ToolErrorValue Value { get; set; }
		public string Uid { get; set; }
		public string  Bias { get; set; }
		public string  Comment { get; set; }
	}

	public class ToolErrorModel {
		[Key]
		public int ToolErrorModelId { get; set; }
		public string Name { get; set; }
		public ToolErrorModelAuthorization Authorization { get; set; }
		public OperatingCondition OperatingCondition { get; set; }
		public UseErrorTermSet UseErrorTermSet { get; set; }
		public List<ErrorTermValue> ErrorTermValue { get; set; }
		public string Uid { get; set; }
		public List<OperatingInterval> OperatingInterval { get; set; }
		public ModelParameters ModelParameters { get; set; }
		public ToolErrorModelCommonData CommonData { get; set; }
	}

	public class ToolErrorModelCommonData
	{
		[Key]
		public int ToolErrorModelCommonDataId { get; set; }
		public string DTimLastChange { get; set; }
	}

	
	public class Start {
		[Key]
		public int StartId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class End {
		[Key]
		public int EndId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class OperatingInterval {
		[Key]
		public int OperatingIntervalId { get; set; }
		public string Mode { get; set; }
		public Start Start { get; set; }
		public End End { get; set; }
		public string Uid { get; set; }
		public Speed Speed { get; set; }
	}

	public class Speed {
		[Key]
		public int SpeedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class GyroInitialization {
		[Key]
		public int GyroInitializationId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class GyroReinitializationDistance {
		[Key]
		public int GyroReinitializationDistanceId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class ModelParameters {
		[Key]
		public int ModelParametersId { get; set; }
		public string MisalignmentMode { get; set; }
		public GyroInitialization GyroInitialization { get; set; }
		public GyroReinitializationDistance GyroReinitializationDistance { get; set; }
		public string NoiseReductionFactor { get; set; }
		public string Switching { get; set; }
	}

	public class ToolErrorModelAuthorization
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


	public class Min {
		[Key]
		public int MinId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class Max {
		[Key]
		public int MaxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}
 
}
