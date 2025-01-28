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



	public class MessageParam
	{
		[Key]
		public string Index { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
	}

	public class MessageMd
	{
		[Key]
		[Required]
		public int MdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MessageMdBit
	{
		[Key]
		[Required]
		public int MdBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class Message {
		[Key]
		public int MessageId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string DTim { get; set; }
		public string ActivityCode { get; set; }
		public MessageMd Md { get; set; }
		public MessageMdBit MdBit { get; set; }
		public string TypeMessage { get; set; }
		public string MessageText { get; set; }
		public MessageParam Param { get; set; }
		public string Severity { get; set; }
		public string WarnProbability { get; set; }
		public MessageCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}

	public class MessageCommonData
	{
		[Key]
		public int MessageCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	 
}
