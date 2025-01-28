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

	public class ChangeHistoryDto
	{
		[Key]

		public int ChangeHistoryId { get; set; }
		public string DTimChange { get; set; }
		public string ChangeType { get; set; }
		public string ChangeInfo { get; set; }
		public StartIndexDto StartIndex { get; set; }
		public EndIndexDto EndIndex { get; set; }
		public string Mnemonics { get; set; }
	}

	public class StartIndexDto
	{
		[Key]

		[Required]
		public string Uom { get; set; }
		public string Text { get; set; }
	}

	public class EndIndexDto
	{
		[Key]

		[Required]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ChangeLogCommonDataDto
	{
		[Key]
		public int CommonDataId { get; set; }
		public DateTime DTimCreation { get; set; }

		public DateTime DTimLastChange { get; set; }
	}

	public class ChangeLogDto
	{
		[Key]
		[Required]
		public string Uid { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string NameObject { get; set; }
		public string ObjectType { get; set; }
		public string LastChangeType { get; set; }
		public string LastChangeInfo { get; set; }
		public List<ChangeHistoryDto> ChangeHistory { get; set; }
		public ChangeLogCommonDataDto CommonData { get; set; }
		
		public string UidObject { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	 

}
