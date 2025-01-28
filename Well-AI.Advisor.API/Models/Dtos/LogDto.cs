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
	public class StepIncrementDto
	{
		[Key]
		public int StepIncrementId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LogParamDto
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

	public class MinIndexDto
	{
		[Key]
		public int MinIndexId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class MaxIndexDto
	{
		[Key]
		public int MaxIndexId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class SensorOffsetDto
	{
		[Key]
		public int SensorOffsetId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class LogCurveInfoDto
	{
		[Key]
		public int LogCurveInfoId { get; set; }
		public string Mnemonic { get; set; }
		public string ClassWitsml { get; set; }
		public string Unit { get; set; }
		public string MnemAlias { get; set; }
		public string NullValue { get; set; }
		public MinIndexDto MinIndex { get; set; }
		public MaxIndexDto MaxIndex { get; set; }
		public string CurveDescription { get; set; }
		public SensorOffsetDto SensorOffset { get; set; }
		public string TraceState { get; set; }
		public string TypeLogData { get; set; }
		public string Uid { get; set; }
	}

	public class LogDataDto
	{
		[Key]
		public int LogDataId { get; set; }
		public string MnemonicList { get; set; }
		public string UnitList { get; set; }
		public List<string> Data { get; set; }
	}

	public class LogCommonDataDto
	{
		public string DTimCreation { get; set; }
		public string DTimLastChange { get; set; }
		public string ItemState { get; set; }
		public string Comments { get; set; }
	}

	public class LogStartIndexDto
	{
		[Key]

		public int LogStartIndexId { get; set; }
		public string Uom { get; set; }
		public string Text { get; set; }
	}

	public class LogEndIndexDto
	{
		[Key]

		public int LogEndIndexId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}
	public class LogDto
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
		public LogStartIndexDto StartIndex { get; set; }
		public LogEndIndexDto EndIndex { get; set; }
		public StepIncrementDto StepIncrement { get; set; }
		public string Direction { get; set; }
		public string IndexCurve { get; set; }
		public string NullValue { get; set; }
		public List<LogParamDto> LogParam { get; set; }
		public List<LogCurveInfoDto> LogCurveInfo { get; set; }
		public LogDataDto LogData { get; set; }
		public LogCommonDataDto CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
 


}
