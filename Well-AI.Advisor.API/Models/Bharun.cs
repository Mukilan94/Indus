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

namespace Well_AI.Advisor.API.Models
{

	public class BharunActDogleg
	{

		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunActDoglegMx
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunETimOpBit
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunMdHoleStart
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunMdHoleStop
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunHkldRot
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunHkldUp
	{

		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunHkldDn
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqOnBotAv
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqOnBotMx
	{

		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqOnBotMn
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqOffBotAv
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTqDhAv
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWtAboveJar
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWtBelowJar
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWtMud
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunFlowratePump
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunPowBit
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunVelNozzleAv
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunPresDropBit
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimHold
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimSteering
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimDrillRot
	{

		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimDrillSlid
	{


		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimCirc
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunCTimReam
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistDrillRot
	{

		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistDrillSlid
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistReam
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistHold
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunDistSteering
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}



	public class BharunRpmMx
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRpmMn
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRpmAvDh
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}




	public class BharunWobMx
	{


		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWobMn
	{

		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWobAvDh
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunAziTop
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunAziBottom
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunInclStart
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunInclMx
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunInclMn
	{

		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunInclStop
	{
		[Key]
		
		

		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunTempMudDhMx
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunPresPumpAv
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunFlowrateBit
	{
		[Key]
		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRpmAv
	{
		[Key]
		

		public int BharunRpmAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunRopAv
	{
		[Key]
		

		public int BharunRopAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRopMx
	{
		[Key]
		

		public int BharunRopMxId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunRopMn
	{
		[Key]
		

		public int BharunRopMnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class BharunWobAv
	{
		[Key]
		

		public int BharunWobAvId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunDrillingParams
	{
		[Key]
		
		public string Uid { get; set; }
		public BharunETimOpBit ETimOpBit { get; set; }

		public BharunMdHoleStart MdHoleStart { get; set; }

		public BharunMdHoleStop MdHoleStop { get; set; }

		
		public BharunTubular Tubular { get; set; }

		public BharunHkldRot HkldRot { get; set; }
	
		public BharunOverPull OverPull { get; set; }
		
		public BharunSlackOff SlackOff { get; set; }

		public BharunHkldUp HkldUp { get; set; }

		public BharunHkldDn HkldDn { get; set; }

		public BharunTqOnBotAv TqOnBotAv { get; set; }

		public BharunTqOnBotMx TqOnBotMx { get; set; }

		public BharunTqOnBotMn TqOnBotMn { get; set; }

		public BharunTqOffBotAv TqOffBotAv { get; set; }

		public BharunTqDhAv TqDhAv { get; set; }

		public BharunWtAboveJar WtAboveJar { get; set; }

		public BharunWtBelowJar WtBelowJar { get; set; }

		public BharunWtMud WtMud { get; set; }

		public BharunFlowratePump FlowratePump { get; set; }

		public BharunPowBit PowBit { get; set; }

		public BharunVelNozzleAv VelNozzleAv { get; set; }

		public BharunPresDropBit PresDropBit { get; set; }

		public BharunCTimHold CTimHold { get; set; }

		public BharunCTimSteering CTimSteering { get; set; }

		public BharunCTimDrillRot CTimDrillRot { get; set; }

		public BharunCTimDrillSlid CTimDrillSlid { get; set; }

		public BharunCTimCirc CTimCirc { get; set; }

		public BharunCTimReam CTimReam { get; set; }

		public BharunDistDrillRot DistDrillRot { get; set; }

		public BharunDistDrillSlid DistDrillSlid { get; set; }

		public BharunDistReam DistReam { get; set; }

		public BharunDistHold DistHold { get; set; }

		public BharunDistSteering DistSteering { get; set; }

		public BharunRpmAv RpmAv { get; set; }

		public BharunRpmMx RpmMx { get; set; }

		public BharunRpmMn RpmMn { get; set; }

		public BharunRpmAvDh RpmAvDh { get; set; }

		public BharunRopAv RopAv { get; set; }

		public BharunRopMx RopMx { get; set; }

		public BharunRopMn RopMn { get; set; }

		public BharunWobAv WobAv { get; set; }

		public BharunWobMx WobMx { get; set; }

		public BharunWobMn WobMn { get; set; }

		public BharunWobAvDh WobAvDh { get; set; }

		public string ReasonTrip { get; set; }

		public string ObjectiveBha { get; set; }

		public BharunAziTop AziTop { get; set; }

		public BharunAziBottom AziBottom { get; set; }

		public BharunInclStart InclStart { get; set; }

		public BharunInclMx InclMx { get; set; }

		public BharunInclMn InclMn { get; set; }

		public BharunInclStop InclStop { get; set; }

		public BharunTempMudDhMx TempMudDhMx { get; set; }

		public BharunPresPumpAv PresPumpAv { get; set; }

		public BharunFlowrateBit FlowrateBit { get; set; }

		public string Comments { get; set; }
	}

	public class BharunTubular
	{
		[Key]
		
		
		public string UidRef { get; set; }

		public string Text { get; set; }
	}


	public class BharunCommonData
	{
		[Key]
		
		public int CommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	public class BharunOverPull
	{
		[Key]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class BharunSlackOff
	{
		[Key]
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	
	public class Bharun
	{

		[Key]
		

		public string Uid { get; set; }
		public string NameWell { get; set; }

		public string NameWellbore { get; set; }

		public string Name { get; set; }

		public BharunTubular Tubular { get; set; }

		public DateTime DTimStart { get; set; }

		public DateTime DTimStop { get; set; }

		public DateTime DTimStartDrilling { get; set; }

		public DateTime DTimStopDrilling { get; set; }

		public BharunPlanDogleg PlanDogleg { get; set; }

		public BharunActDogleg ActDogleg { get; set; }

		public BharunActDoglegMx ActDoglegMx { get; set; }

		public string StatusBha { get; set; }

		public int NumBitRun { get; set; }

		public int NumStringRun { get; set; }

		public string ReasonTrip { get; set; }

		public string ObjectiveBha { get; set; }
		[BindProperty(Name="drillingParams")]
		public BharunDrillingParams DrillingParams { get; set; }

		[BindProperty(Name = "tubular")]
		public BharunCommonData CommonData { get; set; }

		public string UidWell { get; set; }

		public string UidWellbore { get; set; }

	}


	public class BharunPlanDogleg
	{
		[Key]

		
		
		public string Uom { get; set; }

		public string Text { get; set; }
	}
 

}
