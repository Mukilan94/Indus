using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Mapper
{
    public class TrajectoryMappings : Profile
    {
        public TrajectoryMappings()
        {

            CreateMap<MdMn, MdMnDto>().ReverseMap();
            CreateMap<Easting, EastingDto>().ReverseMap();
            CreateMap<TrajectoryMd, TrajectoryMdDto>().ReverseMap();
            CreateMap<TrajectoryTvd, TrajectoryTvdDto>().ReverseMap();
            CreateMap<TrajectoryIncl, TrajectoryInclDto>().ReverseMap();
            CreateMap<TrajectoryAzi, TrajectoryAziDto>().ReverseMap();
            CreateMap<TrajectoryMtf, TrajectoryMtfDto>().ReverseMap();
            CreateMap<TrajectoryGtf, TrajectoryGtfDto>().ReverseMap();
            CreateMap<TrajectoryDispNs, TrajectoryDispNsDto>().ReverseMap();
            CreateMap<TrajectoryDispEw, TrajectoryDispEwDto>().ReverseMap();
            CreateMap<TrajectoryVertSect, TrajectoryVertSectDto>().ReverseMap();
            CreateMap<TrajectoryDls, TrajectoryDlsDto>().ReverseMap();
            CreateMap<TrajectoryRateTurn, TrajectoryRateTurnDto>().ReverseMap();
            CreateMap<TrajectoryRateBuild, TrajectoryRateBuildDto>().ReverseMap();
            CreateMap<TrajectoryMdDelta, TrajectoryMdDeltaDto>().ReverseMap();
            CreateMap<TrajectoryTvdDelta, TrajectoryTvdDeltaDto>().ReverseMap();
            CreateMap<TrajectoryGravTotalUncert, TrajectoryGravTotalUncertDto>().ReverseMap();
            CreateMap<TrajectoryDipAngleUncert, TrajectoryDipAngleUncertDto>().ReverseMap();
            CreateMap<TrajectoryMagTotalUncert, TrajectoryMagTotalUncertDto>().ReverseMap();
            CreateMap<TrajectoryGravAxialRaw, TrajectoryGravAxialRawDto>().ReverseMap();
            CreateMap<TrajectoryGravTran1Raw, TrajectoryGravTran1RawDto>().ReverseMap();
            CreateMap<TrajectoryGravTran2Raw, TrajectoryGravTran2RawDto>().ReverseMap();
            CreateMap<TrajectoryMagAxialRaw, TrajectoryMagAxialRawDto>().ReverseMap();
            CreateMap<TrajectoryMagTran1Raw, TrajectoryMagTran1RawDto>().ReverseMap();
            CreateMap<TrajectoryMagTran2Raw, TrajectoryMagTran2RawDto>().ReverseMap();
            CreateMap<TrajectoryRawData, TrajectoryRawDataDto>().ReverseMap();
            CreateMap<TrajectoryGravAxialAccelCor, TrajectoryGravAxialAccelCorDto>().ReverseMap();
            CreateMap<TrajectoryGravTran1AccelCor, TrajectoryGravTran1AccelCorDto>().ReverseMap();
            CreateMap<TrajectoryGravTran2AccelCor, TrajectoryGravTran2AccelCorDto>().ReverseMap();
            CreateMap<TrajectoryMagAxialDrlstrCor, TrajectoryMagAxialDrlstrCorDto>().ReverseMap();
            CreateMap<TrajectoryMagTran1DrlstrCor, TrajectoryMagTran1DrlstrCorDto>().ReverseMap();
            CreateMap<TrajectoryMagTran2DrlstrCor, TrajectoryMagTran2DrlstrCorDto>().ReverseMap();
            CreateMap<TrajectorySagIncCor, TrajectorySagIncCorDto>().ReverseMap();
            CreateMap<TrajectorySagAziCor, TrajectorySagAziCorDto>().ReverseMap();
            CreateMap<TrajectoryStnMagDeclUsed, TrajectoryStnMagDeclUsedDto>().ReverseMap();
            CreateMap<TrajectoryStnGridCorUsed, TrajectoryStnGridCorUsedDto>().ReverseMap();
            CreateMap<TrajectoryDirSensorOffset, TrajectoryDirSensorOffsetDto>().ReverseMap();
            CreateMap<TrajectoryCorUsed, TrajectoryCorUsedDto>().ReverseMap();
            CreateMap<TrajectoryMagTotalFieldCalc, TrajectoryMagTotalFieldCalcDto>().ReverseMap();
            CreateMap<TrajectoryMagDipAngleCalc, TrajectoryMagDipAngleCalcDto>().ReverseMap();
            CreateMap<TrajectoryGravTotalFieldCalc, TrajectoryGravTotalFieldCalcDto>().ReverseMap();
            CreateMap<TrajectoryValid, TrajectoryValidDto>().ReverseMap();
            CreateMap<TrajectoryVarianceNN, TrajectoryVarianceNNDto>().ReverseMap();
            CreateMap<TrajectoryVarianceNE, TrajectoryVarianceNEDto>().ReverseMap();
            CreateMap<TrajectoryVarianceNVert, TrajectoryVarianceNVertDto>().ReverseMap();
            CreateMap<TrajectoryVarianceEE, TrajectoryVarianceEEDto>().ReverseMap();
            CreateMap<TrajectoryVarianceEVert, TrajectoryVarianceEVertDto>().ReverseMap();
            CreateMap<TrajectoryVarianceVertVert, TrajectoryVarianceVertVertDto>().ReverseMap();
            CreateMap<TrajectoryBiasN, TrajectoryBiasNDto>().ReverseMap();
            CreateMap<TrajectoryBiasE, TrajectoryBiasEDto>().ReverseMap();
            CreateMap<TrajectoryBiasVert, TrajectoryBiasVertDto>().ReverseMap();
            CreateMap<TrajectoryMatrixCov, TrajectoryMatrixCovDto>().ReverseMap();
            CreateMap<MdMx, MdMxDto>().ReverseMap();
            CreateMap<MagDeclUsed, MagDeclUsedDto>().ReverseMap();
            CreateMap<GridCorUsed, GridCorUsedDto>().ReverseMap();
            CreateMap<AziVertSect, AziVertSectDto>().ReverseMap();
            CreateMap<DispNsVertSectOrig, DispNsVertSectOrigDto>().ReverseMap();
            CreateMap<DispEwVertSectOrig, DispEwVertSectOrigDto>().ReverseMap();
            CreateMap<TrajectoryStation, TrajectoryStationDto>().ReverseMap();
            CreateMap<TrajectoryLocation, TrajectoryLocationDto>().ReverseMap();
            CreateMap<TrajectoryNorthing, TrajectoryNorthingDto>().ReverseMap();
            CreateMap<TrajectoryLatitude, TrajectoryLatitudeDto>().ReverseMap();
            CreateMap<TrajectoryLongitude, TrajectoryLongitudeDto>().ReverseMap();

            CreateMap<TrajectoryWellCRS, TrajectoryWellCRSDto>().ReverseMap();
            CreateMap<TrajectoryCommonData, TrajectoryCommonDataDto>().ReverseMap();
            CreateMap<Trajectory, TrajectoryDto>().ReverseMap();

        }


    }
}
