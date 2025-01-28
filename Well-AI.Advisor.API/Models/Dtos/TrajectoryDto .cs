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
    public class MdMnDto
    {
        [Key]
        public int MdMnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class MdMxDto
    {
        [Key]
        public int MdMxId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class MagDeclUsedDto
    {
        [Key]
        public int MagDeclUsedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class GridCorUsedDto
    {
        [Key]
        public int GridCorUsedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class AziVertSectDto
    {
        [Key]
        public int AziVertSectId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class DispNsVertSectOrigDto
    {
        [Key]
        public int DispNsVertSectOrigId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class DispEwVertSectOrigDto
    {
        [Key]
        public int DispEwVertSectOrigId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class EastingDto
    {
        [Key]
        public int EastingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryNorthingDto
    {
        [Key]
        public int NorthingId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryWellCRSDto
    {
        [Key]
        public string UidRef { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryLongitudeDto
    {
        [Key]
        public int LongitudeId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryLatitudeDto
    {
        [Key]

        public int LatitudeId { get; set; }

        public string Uom { get; set; }

        public string Text { get; set; }
    }
    public class TrajectoryLocationDto
    {
        [Key]
        public string Uid { get; set; }
        public TrajectoryWellCRSDto WellCRS { get; set; }
        public TrajectoryLatitudeDto Latitude { get; set; }
        public TrajectoryLongitudeDto Longitude { get; set; }

        public EastingDto Easting { get; set; }
        public TrajectoryNorthingDto Northing { get; set; }
    }

    public class TrajectoryMdDto
    {
        [Key]
        public int MdId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryTvdDto
    {
        [Key]
        public int TvdId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryInclDto
    {
        [Key]
        public int InclId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryAziDto
    {
        [Key]
        public int AziId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMtfDto
    {
        [Key]
        public int MtfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryGtfDto
    {
        [Key]
        public int GtfId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryDispNsDto
    {
        [Key]
        public int DispNsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryDispEwDto
    {
        [Key]
        public int DispEwId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryVertSectDto
    {
        [Key]
        public int VertSectId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryDlsDto
    {
        [Key]
        public int DlsId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryRateTurnDto
    {
        [Key]
        public int RateTurnId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryRateBuildDto
    {
        [Key]
        public int RateBuildId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMdDeltaDto
    {
        [Key]
        public int MdDeltaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryTvdDeltaDto
    {
        [Key]
        public int TvdDeltaId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryGravTotalUncertDto
    {
        [Key]
        public int GravTotalUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryDipAngleUncertDto
    {
        [Key]
        public int DipAngleUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMagTotalUncertDto
    {
        [Key]
        public int MagTotalUncertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class TrajectoryGravAxialRawDto
    {
        [Key]
        public int GravAxialRawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryGravTran1RawDto
    {
        [Key]
        public int GravTran1RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryGravTran2RawDto
    {
        [Key]
        public int GravTran2RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMagAxialRawDto
    {
        [Key]
        public int MagAxialRawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMagTran1RawDto
    {
        [Key]
        public int MagTran1RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMagTran2RawDto
    {
        [Key]
        public int MagTran2RawId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryRawDataDto
    {
        [Key]
        public int RawDataId { get; set; }
        public TrajectoryGravAxialRawDto GravAxialRaw { get; set; }
        public TrajectoryGravTran1RawDto GravTran1Raw { get; set; }
        public TrajectoryGravTran2RawDto GravTran2Raw { get; set; }
        public TrajectoryMagAxialRawDto MagAxialRaw { get; set; }
        public TrajectoryMagTran1RawDto MagTran1Raw { get; set; }
        public TrajectoryMagTran2RawDto MagTran2Raw { get; set; }
    }

    public class TrajectoryGravAxialAccelCorDto
    {
        [Key]
        public int GravAxialAccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryGravTran1AccelCorDto
    {
        [Key]
        public int GravTran1AccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryGravTran2AccelCorDto
    {
        [Key]
        public int GravTran2AccelCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMagAxialDrlstrCorDto
    {
        [Key]
        public int MagAxialDrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMagTran1DrlstrCorDto
    {
        [Key]
        public int MagTran1DrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMagTran2DrlstrCorDto
    {
        [Key]
        public int MagTran2DrlstrCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectorySagIncCorDto
    {
        [Key]
        public int SagIncCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectorySagAziCorDto
    {
        [Key]
        public int SagAziCorId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryStnMagDeclUsedDto
    {
        [Key]
        public int StnMagDeclUsedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryStnGridCorUsedDto
    {
        [Key]
        public int StnGridCorUsedId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryDirSensorOffsetDto
    {
        [Key]
        public int DirSensorOffsetId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class TrajectoryCorUsedDto
    {
        [Key]
        public int TrajectoryCorUsedId { get; set; }
        public TrajectoryGravAxialAccelCorDto GravAxialAccelCor { get; set; }
        public TrajectoryGravTran1AccelCorDto GravTran1AccelCor { get; set; }
        public TrajectoryGravTran2AccelCorDto GravTran2AccelCor { get; set; }
        public TrajectoryMagAxialDrlstrCorDto MagAxialDrlstrCor { get; set; }
        public TrajectoryMagTran1DrlstrCorDto MagTran1DrlstrCor { get; set; }
        public TrajectoryMagTran2DrlstrCorDto MagTran2DrlstrCor { get; set; }
        public TrajectorySagIncCorDto SagIncCor { get; set; }
        public TrajectorySagAziCorDto SagAziCor { get; set; }
        public TrajectoryStnMagDeclUsedDto StnMagDeclUsed { get; set; }
        public TrajectoryStnGridCorUsedDto StnGridCorUsed { get; set; }
        public TrajectoryDirSensorOffsetDto DirSensorOffset { get; set; }
    }

    public class TrajectoryMagTotalFieldCalcDto
    {
        [Key]
        public int MagTotalFieldCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryMagDipAngleCalcDto
    {
        [Key]
        public int MagDipAngleCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryGravTotalFieldCalcDto
    {
        [Key]
        public int GravTotalFieldCalcId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class TrajectoryValidDto
    {
        [Key]
        public int ValidId { get; set; }
        public TrajectoryMagTotalFieldCalcDto MagTotalFieldCalc { get; set; }
        public TrajectoryMagDipAngleCalcDto MagDipAngleCalc { get; set; }
        public TrajectoryGravTotalFieldCalcDto GravTotalFieldCalc { get; set; }
    }

    public class TrajectoryVarianceNNDto
    {
        [Key]
        public int VarianceNNId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryVarianceNEDto
    {
        [Key]
        public int VarianceNEId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryVarianceNVertDto
    {
        [Key]
        public int VarianceNVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryVarianceEEDto
    {
        [Key]
        public int VarianceEEId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryVarianceEVertDto
    {
        [Key]
        public int VarianceEVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryVarianceVertVertDto
    {
        [Key]
        public int VarianceVertVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryBiasNDto
    {
        [Key]
        public int BiasNDtId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryBiasEDto
    {
        [Key]
        public int BiasEId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class TrajectoryBiasVertDto
    {
        [Key]
        public int BiasVertId { get; set; }
        public string Uom { get; set; }

        public string Text { get; set; }
    }


    public class TrajectoryMatrixCovDto
    {
        [Key]
        public int MatrixCovId { get; set; }
        public TrajectoryVarianceNNDto VarianceNN { get; set; }
        public TrajectoryVarianceNEDto VarianceNE { get; set; }
        public TrajectoryVarianceNVertDto VarianceNVert { get; set; }
        public TrajectoryVarianceEEDto VarianceEE { get; set; }
        public TrajectoryVarianceEVertDto VarianceEVert { get; set; }
        public TrajectoryVarianceVertVertDto VarianceVertVert { get; set; }
        public TrajectoryBiasNDto BiasN { get; set; }
        public TrajectoryBiasEDto BiasE { get; set; }
        public TrajectoryBiasVertDto BiasVert { get; set; }
    }
    public class TrajectoryStationDto
    {
        [Key]
        public int TrajectoryStationId { get; set; }
        public string DTimStn { get; set; }
        public string TypeTrajStation { get; set; }
        public TrajectoryMdDto Md { get; set; }
        public TrajectoryTvdDto Tvd { get; set; }
        public TrajectoryInclDto Incl { get; set; }
        public TrajectoryAziDto Azi { get; set; }
        public TrajectoryMtfDto Mtf { get; set; }
        public TrajectoryGtfDto Gtf { get; set; }
        public TrajectoryDispNsDto DispNs { get; set; }
        public TrajectoryDispEwDto DispEw { get; set; }
        public TrajectoryVertSectDto VertSect { get; set; }
        public TrajectoryDlsDto Dls { get; set; }
        public TrajectoryRateTurnDto RateTurn { get; set; }
        public TrajectoryRateBuildDto RateBuild { get; set; }
        public TrajectoryMdDeltaDto MdDelta { get; set; }
        public TrajectoryTvdDeltaDto TvdDelta { get; set; }
        public string ModelToolError { get; set; }
        public TrajectoryGravTotalUncertDto GravTotalUncert { get; set; }
        public TrajectoryDipAngleUncertDto DipAngleUncert { get; set; }
        public TrajectoryMagTotalUncertDto MagTotalUncert { get; set; }
        public string GravAccelCorUsed { get; set; }
        public string MagXAxialCorUsed { get; set; }
        public string SagCorUsed { get; set; }
        public string MagDrlstrCorUsed { get; set; }
        public string StatusTrajStation { get; set; }
        public TrajectoryRawDataDto RawData { get; set; }
        public TrajectoryCorUsedDto CorUsed { get; set; }
        public TrajectoryValidDto Valid { get; set; }
        public TrajectoryMatrixCovDto MatrixCov { get; set; }
        public List<TrajectoryLocationDto> Location { get; set; }
        public string Uid { get; set; }
    }

    public class TrajectoryDto
    {
        [Key]
        public int TrajectoryId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string DTimTrajStart { get; set; }
        public string DTimTrajEnd { get; set; }
        public MdMnDto MdMn { get; set; }
        public MdMxDto MdMx { get; set; }
        public string ServiceCompany { get; set; }
        public MagDeclUsedDto MagDeclUsed { get; set; }
        public GridCorUsedDto GridCorUsed { get; set; }
        public AziVertSectDto AziVertSect { get; set; }
        public DispNsVertSectOrigDto DispNsVertSectOrig { get; set; }
        public DispEwVertSectOrigDto DispEwVertSectOrig { get; set; }
        public string Definitive { get; set; }
        public string Memory { get; set; }
        public string FinalTraj { get; set; }
        public string AziRef { get; set; }
        public TrajectoryStationDto TrajectoryStation { get; set; }
        public TrajectoryCommonDataDto CommonData { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }
    }
    public class TrajectoryCommonDataDto
    {
        public string ItemState { get; set; }
        public string Comments { get; set; }
    }
    

}
