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
	
	public class MdMn {
		[Key]
		public int MdMnId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MdMx {
		[Key]
		public int MdMxId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class MagDeclUsed {
		[Key]
		public int MagDeclUsedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class GridCorUsed {
		[Key]
		public int GridCorUsedId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class AziVertSect {
		[Key]
		public int AziVertSectId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DispNsVertSectOrig {
		[Key]
		public int DispNsVertSectOrigId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class DispEwVertSectOrig {
		[Key]
		public int DispEwVertSectOrigId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	
	public class Easting {
		[Key]
		public int EastingId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TrajectoryNorthing {
		[Key]
		public int NorthingId { get; set; }
		public string Uom { get; set; }
		
		public string Text { get; set; }
	}

	public class TrajectoryWellCRS
	{
		[Key]
		public string UidRef { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryLongitude
	{            
		[Key]
		public int LongitudeId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryLatitude
	{
		[Key]

		public int LatitudeId { get; set; }

		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryLocation
	{
		[Key]
		public string Uid { get; set; }
		public TrajectoryWellCRS WellCRS { get; set; }
		public TrajectoryLatitude Latitude { get; set; }
		public TrajectoryLongitude Longitude { get; set; }
	
		public Easting Easting { get; set; }
		public TrajectoryNorthing Northing { get; set; }
	}


	public class TrajectoryMd
	{
		[Key]
		public int MdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryTvd
	{
		[Key]
		public int TvdId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryIncl
	{
		[Key]
		public int InclId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryAzi
	{
		[Key]
		public int AziId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMtf
	{
		[Key]
		public int MtfId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryGtf
	{
		[Key]
		public int GtfId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryDispNs
	{
		[Key]
		public int DispNsId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryDispEw
	{
		[Key]
		public int DispEwId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryVertSect
	{
		[Key]
		public int VertSectId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryDls
	{
		[Key]
		public int DlsId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryRateTurn
	{
		[Key]
		public int RateTurnId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryRateBuild
	{
		[Key]
		public int RateBuildId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMdDelta
	{
		[Key]
		public int MdDeltaId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryTvdDelta
	{
		[Key]
		public int TvdDeltaId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryGravTotalUncert
	{
		[Key]
		public int GravTotalUncertId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryDipAngleUncert
	{
		[Key]
		public int DipAngleUncertId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMagTotalUncert
	{
		[Key]
		public int MagTotalUncertId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryGravAxialRaw
	{
		[Key]
		public int GravAxialRawId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryGravTran1Raw
	{
		[Key]
		public int GravTran1RawId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryGravTran2Raw
	{
		[Key]
		public int GravTran2RawId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMagAxialRaw
	{
		[Key]
		public int MagAxialRawId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMagTran1Raw
	{
		[Key]
		public int MagTran1RawId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMagTran2Raw
	{
		[Key]
		public int MagTran2RawId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryRawData
	{
		[Key]
		public int RawDataId { get; set; }
		public TrajectoryGravAxialRaw GravAxialRaw { get; set; }
		public TrajectoryGravTran1Raw GravTran1Raw { get; set; }
		public TrajectoryGravTran2Raw GravTran2Raw { get; set; }
		public TrajectoryMagAxialRaw MagAxialRaw { get; set; }
		public TrajectoryMagTran1Raw MagTran1Raw { get; set; }
		public TrajectoryMagTran2Raw MagTran2Raw { get; set; }
	}

	public class TrajectoryGravAxialAccelCor
	{
		[Key]
		public int GravAxialAccelCorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryGravTran1AccelCor
	{
		[Key]
		public int GravTran1AccelCorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryGravTran2AccelCor
	{
		[Key]
		public int GravTran2AccelCorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMagAxialDrlstrCor
	{
		[Key]
		public int MagAxialDrlstrCorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMagTran1DrlstrCor
	{
		[Key]
		public int MagTran1DrlstrCorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMagTran2DrlstrCor
	{
		[Key]
		public int MagTran2DrlstrCorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectorySagIncCor
	{
		[Key]
		public int SagIncCorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectorySagAziCor
	{
		[Key]
		public int SagAziCorId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryStnMagDeclUsed
	{
		[Key]
		public int StnMagDeclUsedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryStnGridCorUsed
	{
		[Key]
		public int StnGridCorUsedId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryDirSensorOffset
	{
		[Key]
		public int DirSensorOffsetId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class TrajectoryCorUsed
	{
		[Key]
		public int TrajectoryCorUsedId { get; set; }
		public TrajectoryGravAxialAccelCor GravAxialAccelCor { get; set; }
		public TrajectoryGravTran1AccelCor GravTran1AccelCor { get; set; }
		public TrajectoryGravTran2AccelCor GravTran2AccelCor { get; set; }
		public TrajectoryMagAxialDrlstrCor MagAxialDrlstrCor { get; set; }
		public TrajectoryMagTran1DrlstrCor MagTran1DrlstrCor { get; set; }
		public TrajectoryMagTran2DrlstrCor MagTran2DrlstrCor { get; set; }
		public TrajectorySagIncCor SagIncCor { get; set; }
		public TrajectorySagAziCor SagAziCor { get; set; }
		public TrajectoryStnMagDeclUsed StnMagDeclUsed { get; set; }
		public TrajectoryStnGridCorUsed StnGridCorUsed { get; set; }
		public TrajectoryDirSensorOffset DirSensorOffset { get; set; }
	}

	public class TrajectoryMagTotalFieldCalc
	{
		[Key]
		public int MagTotalFieldCalcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryMagDipAngleCalc
	{
		[Key]
		public int MagDipAngleCalcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryGravTotalFieldCalc
	{
		[Key]
		public int GravTotalFieldCalcId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class TrajectoryValid
	{
		[Key]
		public int ValidId { get; set; }
		public TrajectoryMagTotalFieldCalc MagTotalFieldCalc { get; set; }
		public TrajectoryMagDipAngleCalc MagDipAngleCalc { get; set; }
		public TrajectoryGravTotalFieldCalc GravTotalFieldCalc { get; set; }
	}

	public class TrajectoryVarianceNN
	{
		[Key]
		public int VarianceNNId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryVarianceNE
	{
		[Key]
		public int VarianceNEId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryVarianceNVert
	{
		[Key]
		public int VarianceNVertId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryVarianceEE
	{
		[Key]
		public int VarianceEEId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryVarianceEVert
	{
		[Key]
		public int VarianceEVertId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryVarianceVertVert
	{
		[Key]
		public int VarianceVertVertId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryBiasN
	{
		[Key]
		public int BiasNDtId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryBiasE
	{
		[Key]
		public int BiasEId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}

	public class TrajectoryBiasVert
	{
		[Key]
		public int BiasVertId { get; set; }
		public string Uom { get; set; }

		public string Text { get; set; }
	}


	public class TrajectoryMatrixCov
	{
		[Key]
		public int MatrixCovId { get; set; }
		public TrajectoryVarianceNN VarianceNN { get; set; }
		public TrajectoryVarianceNE VarianceNE { get; set; }
		public TrajectoryVarianceNVert VarianceNVert { get; set; }
		public TrajectoryVarianceEE VarianceEE { get; set; }
		public TrajectoryVarianceEVert VarianceEVert { get; set; }
		public TrajectoryVarianceVertVert VarianceVertVert { get; set; }
		public TrajectoryBiasN BiasN { get; set; }
		public TrajectoryBiasE BiasE { get; set; }
		public TrajectoryBiasVert BiasVert { get; set; }
	}

	public class TrajectoryStation {
		[Key]
		public int TrajectoryStationId { get; set; }
		public string DTimStn { get; set; }
		public string TypeTrajStation { get; set; }
		public TrajectoryMd Md { get; set; }
		public TrajectoryTvd Tvd { get; set; }
		public TrajectoryIncl Incl { get; set; }
		public TrajectoryAzi Azi { get; set; }
		public TrajectoryMtf Mtf { get; set; }
		public TrajectoryGtf Gtf { get; set; }
		public TrajectoryDispNs DispNs { get; set; }
		public TrajectoryDispEw DispEw { get; set; }
		public TrajectoryVertSect VertSect { get; set; }
		public TrajectoryDls Dls { get; set; }
		public TrajectoryRateTurn RateTurn { get; set; }
		public TrajectoryRateBuild RateBuild { get; set; }
		public TrajectoryMdDelta MdDelta { get; set; }
		public TrajectoryTvdDelta TvdDelta { get; set; }
		public string ModelToolError { get; set; }
		public TrajectoryGravTotalUncert GravTotalUncert { get; set; }
		public TrajectoryDipAngleUncert DipAngleUncert { get; set; }
		public TrajectoryMagTotalUncert MagTotalUncert { get; set; }
		public string GravAccelCorUsed { get; set; }
		public string MagXAxialCorUsed { get; set; }
		public string SagCorUsed { get; set; }
		public string MagDrlstrCorUsed { get; set; }
		public string StatusTrajStation { get; set; }
		public TrajectoryRawData RawData { get; set; }
		public TrajectoryCorUsed CorUsed { get; set; }
		public TrajectoryValid Valid { get; set; }
		public TrajectoryMatrixCov MatrixCov { get; set; }
		public List<TrajectoryLocation> Location { get; set; }
		public string Uid { get; set; }
	}

	public class Trajectory {
		[Key]
		public int TrajectoryId { get; set; }
		public string NameWell { get; set; }
		public string NameWellbore { get; set; }
		public string Name { get; set; }
		public string DTimTrajStart { get; set; }
		public string DTimTrajEnd { get; set; }
		public MdMn MdMn { get; set; }
		public MdMx MdMx { get; set; }
		public string ServiceCompany { get; set; }
		public MagDeclUsed MagDeclUsed { get; set; }
		public GridCorUsed GridCorUsed { get; set; }
		public AziVertSect AziVertSect { get; set; }
		public DispNsVertSectOrig DispNsVertSectOrig { get; set; }
		public DispEwVertSectOrig DispEwVertSectOrig { get; set; }
		public string Definitive { get; set; }
		public string Memory { get; set; }
		public string FinalTraj { get; set; }
		public string AziRef { get; set; }
		public TrajectoryStation TrajectoryStation { get; set; }
		public TrajectoryCommonData CommonData { get; set; }
		public string Uid { get; set; }
		public string UidWellbore { get; set; }
		public string UidWell { get; set; }
	}
	public class TrajectoryCommonData
	{
		[Key]

		public int TrajectoryCommonDataId { get; set; }
		public string ItemState { get; set; }

		public string Comments { get; set; }
	}
	 

}
