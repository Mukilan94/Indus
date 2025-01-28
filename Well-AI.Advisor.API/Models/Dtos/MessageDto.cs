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

  
	public class MessageParamDto
	{
		public string Name { get; set; }
		public string Index { get; set; }

		public string Text { get; set; }
	}

	public class MessageCommonDataDto
	{
		public string ItemState { get; set; }
		public string Comments { get; set; }
	}

	public class MessageMdDto
	{
		[Key]
		[Required]
		public int MdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MessageMdBitDto
	{
		[Key]
		[Required]
		public int MdBitId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class MessageDto {
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string DTim { get; set; }
		public string ActivityCode { get; set; }
		public MessageMdDto Md { get; set; }
		public MessageMdBitDto MdBit { get; set; }
		public string TypeMessage { get; set; }
		public string MessageText { get; set; }
		public MessageParamDto Param { get; set; }
		public string Severity { get; set; }
		public string WarnProbability { get; set; }
		public MessageCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
 

}
