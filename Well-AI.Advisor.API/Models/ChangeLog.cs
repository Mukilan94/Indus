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


	public class ChangeHistory
	{
		[Key]

		public int ChangeHistoryId { get; set; }
		public string DTimChange { get; set; }
		public string ChangeType { get; set; }
		public string ChangeInfo { get; set; }
		public StartIndex StartIndex { get; set; }
		public EndIndex EndIndex { get; set; }
		public string Mnemonics { get; set; }
	}

	public class StartIndex
	{
		[Key]

		[Required]
		public string Uom { get; set; }
		public string Text { get; set; }
	}

	public class EndIndex
	{
		[Key]

		[Required]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ChangeLogCommonData
	{
		[Key]
		public int CommonDataId { get; set; }
		public DateTime DTimCreation { get; set; }

		public DateTime DTimLastChange { get; set; }
	}

	public class ChangeLog
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
		public List<ChangeHistory> ChangeHistory { get; set; }
		public ChangeLogCommonData CommonData { get; set; }

		public string UidObject { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
 
}
