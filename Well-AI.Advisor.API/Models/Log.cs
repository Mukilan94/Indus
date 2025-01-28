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


	public class StepIncrement
	{
		[Key]
		public int StepIncrementId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LogParam
	{
		[Key]
		public int LogParamId { get; set; }
		public string Uid { get; set; }
		public string Uom { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string Index { get; set; }

		public string Text { get; set; }
	}

	public class MinIndex
	{
		[Key]
		public int MinIndexId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxIndex
	{
		[Key]
		public int MaxIndexId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SensorOffset
	{
		[Key]
		public int SensorOffsetId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LogCurveInfo
	{
		[Key]
		public int LogCurveInfoId { get; set; }
		public string Mnemonic { get; set; }
		public string ClassWitsml { get; set; }
		public string Unit { get; set; }
		public string MnemAlias { get; set; }
		public string NullValue { get; set; }
		public MinIndex MinIndex { get; set; }
		public MaxIndex MaxIndex { get; set; }
		public string CurveDescription { get; set; }
		public SensorOffset SensorOffset { get; set; }
		public string TraceState { get; set; }
		public string TypeLogData { get; set; }
		public string Uid { get; set; }
	}

	public class LogData
	{
		[Key]
		public int LogDataId { get; set; }
		public string MnemonicList { get; set; }
		public string UnitList { get; set; }
		public string Data { get; set; }
	}

	public class LogStartIndex
	{
		[Key]

		public int LogStartIndexId  { get; set; }
		public string Uom { get; set; }
		public string Text { get; set; }
	}

	public class LogEndIndex
	{
		[Key]

		public int LogEndIndexId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class Log
	{
		[Key]
		public int LogId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string ServiceCompany { get; set; }
		public string RunNumber { get; set; }
		public string CreationDate { get; set; }
		public string Description { get; set; }
		public string IndexType { get; set; }
		public LogStartIndex StartIndex { get; set; }
		public LogEndIndex EndIndex { get; set; }
		public StepIncrement StepIncrement { get; set; }
		public string Direction { get; set; }
		public string IndexCurve { get; set; }
		public string NullValue { get; set; }
		public List<LogParam> LogParam { get; set; }
		public List<LogCurveInfo> LogCurveInfo { get; set; }
		public LogData LogData { get; set; }
		public LogCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	public class LogCommonData
	{
		[Key]
		public int LogCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	 

}
