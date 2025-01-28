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
	public class FileCreationInformation {
		[Key]
		public int FileCreationInformationId { get; set; }
		public string FileCreationDate { get; set; }
		public string FileCreator { get; set; }
	}

	public class MdStart {
		[Key]
		public int MdStartId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdEnd {
		[Key]
		public int MdEndId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class FrequencyMx {
		[Key]
		public int FrequencyMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class SurveySection {
		[Key]
		public int SurveySectionId { get; set; }
		public string Sequence { get; set; }
		public string Name { get; set; }
		public MdStart MdStart { get; set; }
		public MdEnd MdEnd { get; set; }
		public string NameSurveyCompany { get; set; }
		public string NameTool { get; set; }
		public string TypeTool { get; set; }
		public string ModelError { get; set; }
		public string Overwrite { get; set; }
		public FrequencyMx FrequencyMx { get; set; }
		public string ItemState { get; set; }
		public string Comments { get; set; }
		public string Uid { get; set; }
	}

	public class SurveyProgram {
		[Key]
		public int SurveyProgramId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string SurveyVer { get; set; }
		public string DTimTrajProg { get; set; }
		public string Engineer { get; set; }
		public string Final { get; set; }
		public List<SurveySection> SurveySection { get; set; }
		public SurveyProgramCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	public class SurveyProgramCommonData
	{
		[Key]
		public int SurveyProgramCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
 

}
