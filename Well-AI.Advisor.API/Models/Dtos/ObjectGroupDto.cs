  
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

	public class ObjectGroupParamDto
	{
		[Key]

		public int ParamId { get; set; }
		public string Uid { get; set; }
		public string Description { get; set; }
		public string Uom { get; set; }
		public string Name { get; set; }
		public string Index { get; set; }
		public string Text { get; set; }
	}

	public class ObjectGroupObjectReferenceDto
	{
		[Key]
		public int ObjectReferenceId { get; set; }
		public string UidRef { get; set; }
		public string Object { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupSequence1Dto
	{
		[Key]
		public int Sequence1Id { get; set; }
		public string Description { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupSequence2Dto
	{
		[Key]
		public int Sequence2Id { get; set; }
		public string Description { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupSequence3Dto
	{
		[Key]
		public int Sequence3Id { get; set; }
		public string Description { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupRangeMinDto
	{
		[Key]
		public int RangeMinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupRangeMaxDto
	{
		[Key]
		public int RangeMaxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupReferenceDepthDto
	{
		[Key]
		public int ReferenceDepthId { get; set; }
		public string Uom { get; set; }
		public string Datum { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupValueDto
	{
		[Key]
		public int ValueId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class ObjectGroupMdDto
	{
		[Key]
		public int MdId { get; set; }
		public string Uom { get; set; }
		public string Datum { get; set; }

		public string Text { get; set; }
	}


	public class ObjectGroupExtensionNameValueDto
	{
		[Key]
		public int ExtensionNameValueId { get; set; }
		public string Name { get; set; }
		public ObjectGroupValueDto Value { get; set; }
		public string DataType { get; set; }
		public string DTim { get; set; }
		public ObjectGroupMdDto Md { get; set; }
		public string Index { get; set; }
		public string MeasureClass { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class ObjectGroupMemberObjectDto
	{
		[Key]
		public int MemberObjectId { get; set; }
		public ObjectGroupObjectReferenceDto ObjectReference { get; set; }
		public string IndexType { get; set; }
		public ObjectGroupSequence1Dto Sequence1 { get; set; }
		public ObjectGroupSequence2Dto Sequence2 { get; set; }
		public ObjectGroupSequence3Dto Sequence3 { get; set; }
		public ObjectGroupRangeMinDto RangeMin { get; set; }
		public ObjectGroupRangeMaxDto RangeMax { get; set; }
		public string RangeDateTimeMin { get; set; }
		public string RangeDateTimeMax { get; set; }
		public string MnemonicList { get; set; }
		public ObjectGroupReferenceDepthDto ReferenceDepth { get; set; }
		public string ReferenceDateTime { get; set; }
		public ObjectGroupParamDto Param { get; set; }
		public ObjectGroupExtensionNameValueDto ExtensionNameValue { get; set; }
		public string Uid { get; set; }
	}

	public class ObjectGroupAcquisitionTimeZoneDto
	{
		[Key]
		public int AcquisitionTimeZoneId { get; set; }
		public string DTim { get; set; }

		public string Text { get; set; }
	}


	public class ObjectGroupDefaultDatumDto
	{
		[Key]

		public int DefaultDatumId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupCommonDataDto
	{
		[Key]

		public int ObjectGroupCommonDataId { get; set; }
		public string SourceName { get; set; }
		public string DTimCreation { get; set; }
		public string DTimLastChange { get; set; }
		public string ItemState { get; set; }
		public string ServiceCategory { get; set; }
		public string Comments { get; set; }
		public ObjectGroupAcquisitionTimeZoneDto AcquisitionTimeZone { get; set; }
		public ObjectGroupDefaultDatumDto DefaultDatum { get; set; }
		public string PrivateGroupOnly { get; set; }
		public string ExtensionAny { get; set; }
		public ObjectGroupExtensionNameValueDto ExtensionNameValue { get; set; }
	}

	public class ObjectGroupDto
	{
		[Key]
		public int ObjectGroupId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string GroupType { get; set; }
		public string Sequence { get; set; }
		public string Description { get; set; }
		public ObjectGroupParamDto Param { get; set; }
		public ObjectGroupMemberObjectDto MemberObject { get; set; }
		public ObjectGroupCommonDataDto CommonData { get; set; }
		public string CustomData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
 

}


