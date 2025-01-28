   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Well_AI.Advisor.API.Models
{


	
	public class Authorization {
		[Key]
		public int AuthorizationId { get; set; }
		public string Author { get; set; }
		public string Source { get; set; }
		public string Authority { get; set; }
		public string Status { get; set; }
		public string Version { get; set; }
		public string Comment { get; set; }
	}

	public class Function {
		[Key]
		public int FunctionId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}


	public class ToolErrorTermSetParameter
	{
		[Key]
		public string Uid { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		
	}
	public class Nomenclature {
		[Key]
		public int NomenclatureId { get; set; }
		public List<ToolErrorTermSetParameter> Parameter { get; set; }
		public List<Function> Function { get; set; }
		public Constant Constant { get; set; }
	}

	public class ErrorCoefficient {
		[Key]
		public int ErrorCoefficientId { get; set; }
		public string Azi { get; set; }
		public string Uid { get; set; }
		public string Inc { get; set; }
		public string Depth { get; set; }
		public string Tvd { get; set; }
	}

	public class ErrorTerm {
		[Key]
		public int ErrorTermId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string MeasureClass { get; set; }
		public string Label { get; set; }
		public string Description { get; set; }
		public List<ErrorCoefficient> ErrorCoefficient { get; set; }
		public string Uid { get; set; }
		
		public string OperatingMode { get; set; }
	}

	public class ToolErrorTermSet {
		[Key]
		public int ToolErrorTermSetId { get; set; }
		public string Name { get; set; }
		public Authorization Authorization { get; set; }
		public Nomenclature Nomenclature { get; set; }
		public List<ErrorTerm> ErrorTerm { get; set; }
		[XmlAttribute(AttributeName="uid")]
		public string Uid { get; set; }
	}

	public class Constant {
		[Key]
		public int ConstantId { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string Unit { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class ToolErrorTermSets {
		public List<ToolErrorTermSet> ToolErrorTermSet { get; set; }
		public string Version { get; set; }
		public string SchemaLocation { get; set; }
		public string Xsi { get; set; }
		public string Witsml { get; set; }
		public string Xmlns { get; set; }
	}

}
