/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Well_AI.Advisor.API.Dtos
{

	public class BharunActDoglegDto
	{

		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunActDoglegMxDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunETimOpBitDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunMdHoleStartDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunMdHoleStopDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunHkldRotDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunHkldUpDto
	{

		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunHkldDnDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqOnBotAvDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqOnBotMxDto
	{

		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqOnBotMnDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqOffBotAvDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqDhAvDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWtAboveJarDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWtBelowJarDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWtMudDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunFlowratePumpDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunPowBitDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunVelNozzleAvDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunPresDropBitDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimHoldDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimSteeringDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimDrillRotDto
	{

		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimDrillSlidDto
	{


		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimCircDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimReamDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistDrillRotDto
	{

		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistDrillSlidDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistReamDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistHoldDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistSteeringDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class BharunRpmMxDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRpmMnDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRpmAvDhDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}




	public class BharunWobMxDto
	{


		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWobMnDto
	{

		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWobAvDhDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunAziTopDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunAziBottomDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunInclStartDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunInclMxDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunInclMnDto
	{

		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunInclStopDto
	{
		[Key]
		


		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTempMudDhMxDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunPresPumpAvDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunFlowrateBitDto
	{
		[Key]
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRpmAvDto
	{
		[Key]
		

		public int BharunRpmAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunRopAvDto
	{
		[Key]
		

		public int BharunRopAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRopMxDto
	{
		[Key]
		

		public int BharunRopMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRopMnDto
	{
		[Key]
		

		public int BharunRopMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWobAvDto
	{
		[Key]
		

		public int BharunWobAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunDrillingParamsDto
	{
		[Key]
		
		public string Uid { get; set; }
		public BharunETimOpBitDto ETimOpBit { get; set; }

		public BharunMdHoleStartDto MdHoleStart { get; set; }

		public BharunMdHoleStopDto MdHoleStop { get; set; }


		public BharunTubularDto Tubular { get; set; }

		public BharunHkldRotDto HkldRot { get; set; }

		public BharunOverPullDto OverPull { get; set; }

		public BharunSlackOffDto SlackOff { get; set; }

		public BharunHkldUpDto HkldUp { get; set; }

		public BharunHkldDnDto HkldDn { get; set; }

		public BharunTqOnBotAvDto TqOnBotAv { get; set; }

		public BharunTqOnBotMxDto TqOnBotMx { get; set; }

		public BharunTqOnBotMnDto TqOnBotMn { get; set; }

		public BharunTqOffBotAvDto TqOffBotAv { get; set; }

		public BharunTqDhAvDto TqDhAv { get; set; }

		public BharunWtAboveJarDto WtAboveJar { get; set; }

		public BharunWtBelowJarDto WtBelowJar { get; set; }

		public BharunWtMudDto WtMud { get; set; }

		public BharunFlowratePumpDto FlowratePump { get; set; }

		public BharunPowBitDto PowBit { get; set; }

		public BharunVelNozzleAvDto VelNozzleAv { get; set; }

		public BharunPresDropBitDto PresDropBit { get; set; }

		public BharunCTimHoldDto CTimHold { get; set; }

		public BharunCTimSteeringDto CTimSteering { get; set; }

		public BharunCTimDrillRotDto CTimDrillRot { get; set; }

		public BharunCTimDrillSlidDto CTimDrillSlid { get; set; }

		public BharunCTimCircDto CTimCirc { get; set; }

		public BharunCTimReamDto CTimReam { get; set; }

		public BharunDistDrillRotDto DistDrillRot { get; set; }

		public BharunDistDrillSlidDto DistDrillSlid { get; set; }

		public BharunDistReamDto DistReam { get; set; }

		public BharunDistHoldDto DistHold { get; set; }

		public BharunDistSteeringDto DistSteering { get; set; }

		public BharunRpmAvDto RpmAv { get; set; }

		public BharunRpmMxDto RpmMx { get; set; }

		public BharunRpmMnDto RpmMn { get; set; }

		public BharunRpmAvDhDto RpmAvDh { get; set; }

		public BharunRopAvDto RopAv { get; set; }

		public BharunRopMxDto RopMx { get; set; }

		public BharunRopMnDto RopMn { get; set; }

		public BharunWobAvDto WobAv { get; set; }

		public BharunWobMxDto WobMx { get; set; }

		public BharunWobMnDto WobMn { get; set; }

		public BharunWobAvDhDto WobAvDh { get; set; }

		public string ReasonTrip { get; set; }

		public string ObjectiveBha { get; set; }

		public BharunAziTopDto AziTop { get; set; }

		public BharunAziBottomDto AziBottom { get; set; }

		public BharunInclStartDto InclStart { get; set; }

		public BharunInclMxDto InclMx { get; set; }

		public BharunInclMnDto InclMn { get; set; }

		public BharunInclStopDto InclStop { get; set; }

		public BharunTempMudDhMxDto TempMudDhMx { get; set; }

		public BharunPresPumpAvDto PresPumpAv { get; set; }

		public BharunFlowrateBitDto FlowrateBit { get; set; }

		public string Comments { get; set; }
	}

	public class BharunTubularDto
	{
		[Key]
		

		public string UidRef { get; set; }

		public string Text { get; set; }
	}


	public class BharunCommonDataDto
	{
		[Key]

		public int CommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	public class BharunOverPullDto
	{
		[Key]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunSlackOffDto
	{
		[Key]
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDto
	{

		[Key]
		

		public string Uid { get; set; }
		public string NameWell { get; set; }

		public string NameWellbore { get; set; }

		public string Name { get; set; }

		public BharunTubularDto Tubular { get; set; }

		public DateTime DTimStart { get; set; }

		public DateTime DTimStop { get; set; }

		public DateTime DTimStartDrilling { get; set; }

		public DateTime DTimStopDrilling { get; set; }

		public BharunPlanDoglegDto PlanDogleg { get; set; }

		public BharunActDoglegDto ActDogleg { get; set; }

		public BharunActDoglegMxDto ActDoglegMx { get; set; }

		public string StatusBha { get; set; }

		public int NumBitRun { get; set; }

		public int NumStringRun { get; set; }

		public string ReasonTrip { get; set; }

		public string ObjectiveBha { get; set; }
		[BindProperty(Name = "drillingParams")]
		public BharunDrillingParamsDto DrillingParams { get; set; }

		[BindProperty(Name = "tubular")]
		public BharunCommonDataDto CommonData { get; set; }

		public string UidWell { get; set; }

		public string UidWellbore { get; set; }

	}


	public class BharunPlanDoglegDto
	{
		[Key]

		

		public string Uom { get; set; }

		public string Text { get; set; }
	}

	 
}
