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
	public class ObjectGroupParam
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

	public class ObjectGroupObjectReference
	{
		[Key]
		public int ObjectReferenceId { get; set; }
		public string UidRef { get; set; }
		public string Object { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupSequence1
	{
		[Key]
		public int Sequence1Id { get; set; }
		public string Description { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupSequence2
	{
		[Key]
		public int Sequence2Id { get; set; }
		public string Description { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupSequence3
	{
		[Key]
		public int Sequence3Id { get; set; }
		public string Description { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupRangeMin
	{
		[Key]
		public int RangeMinId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupRangeMax
	{
		[Key]
		public int RangeMaxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupReferenceDepth
	{
		[Key]
		public int ReferenceDepthId { get; set; }
		public string Uom { get; set; }
		public string Datum { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupValue
	{
		[Key]
		public int ValueId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class ObjectGroupMd
	{
		[Key]
		public int MdId { get; set; }
		public string Uom { get; set; }
		public string Datum { get; set; }

		public string Text { get; set; }
	}


	public class ObjectGroupExtensionNameValue
	{
		[Key]
		public int ExtensionNameValueId { get; set; }
		public string Name { get; set; }
		public ObjectGroupValue Value { get; set; }
		public string DataType { get; set; }
		public string DTim { get; set; }
		public ObjectGroupMd Md { get; set; }
		public string Index { get; set; }
		public string MeasureClass { get; set; }
		public string Description { get; set; }
		public string Uid { get; set; }
	}

	public class ObjectGroupMemberObject
	{
		[Key]
		public int MemberObjectId { get; set; }
		public ObjectGroupObjectReference ObjectReference { get; set; }
		public string IndexType { get; set; }
		public ObjectGroupSequence1 Sequence1 { get; set; }
		public ObjectGroupSequence2 Sequence2 { get; set; }
		public ObjectGroupSequence3 Sequence3 { get; set; }
		public ObjectGroupRangeMin RangeMin { get; set; }
		public ObjectGroupRangeMax RangeMax { get; set; }
		public string RangeDateTimeMin { get; set; }
		public string RangeDateTimeMax { get; set; }
		public string MnemonicList { get; set; }
		public ObjectGroupReferenceDepth ReferenceDepth { get; set; }
		public string ReferenceDateTime { get; set; }
		public ObjectGroupParam Param { get; set; }
		public ObjectGroupExtensionNameValue ExtensionNameValue { get; set; }
		public string Uid { get; set; }
	}

	public class ObjectGroupAcquisitionTimeZone
	{
		[Key]
		public int AcquisitionTimeZoneId { get; set; }
		public string DTim { get; set; }

		public string Text { get; set; }
	}


	public class ObjectGroupDefaultDatum
	{
		[Key]

		public int DefaultDatumId { get; set; }
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class ObjectGroupCommonData
	{
		[Key]

		public int ObjectGroupCommonDataId { get; set; }
		public string SourceName { get; set; }
		public string DTimCreation { get; set; }
		public string DTimLastChange { get; set; }
		public string ItemState { get; set; }
		public string ServiceCategory { get; set; }
		public string Comments { get; set; }
		public ObjectGroupAcquisitionTimeZone AcquisitionTimeZone { get; set; }
		public ObjectGroupDefaultDatum DefaultDatum { get; set; }
		public string PrivateGroupOnly { get; set; }
		public string ExtensionAny { get; set; }
		public ObjectGroupExtensionNameValue ExtensionNameValue { get; set; }
	}

	public class ObjectGroup
	{
		[Key]
		public int ObjectGroupId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string GroupType { get; set; }
		public string Sequence { get; set; }
		public string Description { get; set; }
		public ObjectGroupParam Param { get; set; }
		public ObjectGroupMemberObject MemberObject { get; set; }
		public ObjectGroupCommonData CommonData { get; set; }
		public string CustomData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	 

}
