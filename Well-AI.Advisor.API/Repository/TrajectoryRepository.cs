using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Controllers;
using Well_AI.Advisor.API.Data;
using Well_AI.Advisor.API.Models;
using Well_AI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.API.Repository.IRepository;

namespace Well_AI.Advisor.API.Repository
{
    public class TrajectoryRepository : ITrajectoryRepository
    {
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public TrajectoryRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            string tenantId = httpContextAccessor.HttpContext.User.Identity.Name;
            _tenantRepository = tenantRepository;
            var options = _tenantRepository.SetDbContext(tenantId);
            db = new WellAIAdvisiorContext(options);
            _db = db;
            _wdb = wdb;
        }
        public bool TrajectoryExists(string uid)
        {
            bool value = _db.Trajectorys.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateTrajectory(Trajectory trajectory)
        {
            try
            {
                MdMn(trajectory);
                MdMx(trajectory);
                MagDeclUsed(trajectory);
                GridCorUsed(trajectory);
                AziVertSect(trajectory);
                DispNsVertSectOrig(trajectory);
                DispEwVertSectOrig(trajectory);
                Md(trajectory);
                Tvd(trajectory);
                Incl(trajectory);
                Azi(trajectory);
                Mtf(trajectory);
                Gtf(trajectory);
                DispNs(trajectory);
                DispEw(trajectory);
                VertSect(trajectory);
                Dls(trajectory);
                RateTurn(trajectory);
                RateBuild(trajectory);
                MdDelta(trajectory);
                TvdDelta(trajectory);
                GravTotalUncert(trajectory);
                DipAngleUncert(trajectory);
                MagTotalUncert(trajectory);
                GravAxialRaw(trajectory);
                GravTran1Raw(trajectory);
                MagAxialRaw(trajectory);
                MagTran1Raw(trajectory);
                MagTran2Raw(trajectory);
                GravTran2Raw(trajectory);
                RawData(trajectory);
                GravAxialAccelCor(trajectory);
                GravTran1AccelCor(trajectory);
                GravTran2AccelCor(trajectory);
                MagAxialDrlstrCor(trajectory);
                MagTran2DrlstrCor(trajectory);
                SagIncCor(trajectory);
                SagAziCor(trajectory);
                StnMagDeclUsed(trajectory);
                StnGridCorUsed(trajectory);
                DirSensorOffset(trajectory);
                MagTran1DrlstrCor(trajectory);
                MagTran2DrlstrCor(trajectory);
                CorUsed(trajectory);
                MagTotalFieldCalc(trajectory);
                MagDipAngleCalc(trajectory);
                GravTotalFieldCalc(trajectory);
                Valid(trajectory);
                VarianceNN(trajectory);
                VarianceNE(trajectory);
                VarianceNVert(trajectory);
                VarianceEE(trajectory);
                VarianceEVert(trajectory);
                VarianceVertVert(trajectory);
                BiasN(trajectory);
                BiasE(trajectory);
                BiasVert(trajectory);
                MatrixCov(trajectory);
                WellCRS(trajectory);
                Latitude(trajectory);
                Longitude(trajectory);
                Location(trajectory);
                Easting(trajectory);
                Northing(trajectory);
                Location(trajectory);
                TrajectoryStation(trajectory);
                CommonData(trajectory);
                _db.Trajectorys.Add(trajectory);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "TrajectoryRepository CreateTrajectory", null);
                return Save();
            }
        }

       

        public bool DeleteTrajectory(Trajectory trajectory)
        {
            _db.Trajectorys.Remove(trajectory);
            return Save();
        }

        public Trajectory GetTrajectoryDetail(string Uid)
        {
            return _db.Trajectorys.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<Trajectory> GetTrajectoryDetails()
        {
            return _db.Trajectorys.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrajectory(Trajectory trajectory)
        {
            try
            {
                UpdateMdMn(trajectory);
                UpdateMdMx(trajectory);
                UpdateMagDeclUsed(trajectory);
                UpdateGridCorUsed(trajectory);
                UpdateAziVertSect(trajectory);
                UpdateDispNsVertSectOrig(trajectory);
                UpdateDispEwVertSectOrig(trajectory);
                UpdateMd(trajectory);
                UpdateTvd(trajectory);
                UpdateIncl(trajectory);
                UpdateAzi(trajectory);
                UpdateMtf(trajectory);
                UpdateGtf(trajectory);
                UpdateDispNs(trajectory);
                UpdateDispEw(trajectory);
                UpdateVertSect(trajectory);
                UpdateDls(trajectory);
                UpdateRateTurn(trajectory);
                UpdateRateBuild(trajectory);
                UpdateMdDelta(trajectory);
                UpdateTvdDelta(trajectory);
                UpdateGravTotalUncert(trajectory);
                UpdateDipAngleUncert(trajectory);
                UpdateMagTotalUncert(trajectory);
                UpdateGravAxialRaw(trajectory);
                UpdateGravTran1Raw(trajectory);
                UpdateMagAxialRaw(trajectory);
                UpdateMagTran1Raw(trajectory);
                UpdateMagTran2Raw(trajectory);
                UpdateGravTran2Raw(trajectory);
                UpdateRawData(trajectory);
                UpdateGravAxialAccelCor(trajectory);
                UpdateGravTran1AccelCor(trajectory);
                UpdateGravTran2AccelCor(trajectory);
                UpdateMagAxialDrlstrCor(trajectory);
                UpdateMagTran2DrlstrCor(trajectory);
                UpdateSagIncCor(trajectory);
                UpdateSagAziCor(trajectory);
                UpdateStnMagDeclUsed(trajectory);
                UpdateStnGridCorUsed(trajectory);
                UpdateDirSensorOffset(trajectory);
                UpdateMagTran1DrlstrCor(trajectory);
                UpdateMagTran2DrlstrCor(trajectory);
                UpdateCorUsed(trajectory);
                UpdateMagTotalFieldCalc(trajectory);
                UpdateMagDipAngleCalc(trajectory);
                UpdateGravTotalFieldCalc(trajectory);
                UpdateValid(trajectory);
                UpdateVarianceNN(trajectory);
                UpdateVarianceNE(trajectory);
                UpdateVarianceNVert(trajectory);
                UpdateVarianceEE(trajectory);
                UpdateVarianceEVert(trajectory);
                UpdateVarianceVertVert(trajectory);
                UpdateBiasN(trajectory);
                UpdateBiasE(trajectory);
                UpdateBiasVert(trajectory);
                UpdateMatrixCov(trajectory);
                UpdateWellCRS(trajectory);
                UpdateLatitude(trajectory);
                UpdateLongitude(trajectory);
                UpdateLocation(trajectory);
                UpdateEasting(trajectory);
                UpdateNorthing(trajectory);
                UpdateLocation(trajectory);
                UpdateTrajectoryStation(trajectory);
                UpdateCommonData(trajectory);
                _db.Trajectorys.Update(trajectory);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "TrajectoryRepository UpdateTrajectory", null);
                return Save();
            }
        }

        #region Insert Trajectory
        private void MdMn(Trajectory trajectory)
        {
            if (trajectory.MdMn.Uom != null)
            {
                var obj = _mapper.Map<MdMn>(trajectory.MdMn);
                _db.TrajectoryMdMns.Add(obj);
            }
        }
       

        private void MdMx(Trajectory trajectory)
        {
            if (trajectory.MdMx.Uom != null)
            {
                var obj = _mapper.Map<MdMx>(trajectory.MdMx);
                _db.TrajectoryMdMxs.Add(obj);
            }
        }

        private void MagDeclUsed(Trajectory trajectory)
        {
            if (trajectory.MagDeclUsed.Uom != null)
            {
                var obj = _mapper.Map<MagDeclUsed>(trajectory.MagDeclUsed);
                _db.TrajectoryMagDeclUseds.Add(obj);
            }
        }
        private void GridCorUsed(Trajectory trajectory)
        {
            if (trajectory.GridCorUsed.Uom != null)
            {
                var obj = _mapper.Map<GridCorUsed>(trajectory.GridCorUsed);
                _db.TrajectoryGridCorUseds.Add(obj);
            }
        }
        private void AziVertSect(Trajectory trajectory)
        {
            if (trajectory.AziVertSect.Uom != null)
            {
                var obj = _mapper.Map<AziVertSect>(trajectory.AziVertSect);
                _db.TrajectoryAziVertSects.Add(obj);
            }
        }
        private void DispNsVertSectOrig(Trajectory trajectory)
        {
            if (trajectory.DispNsVertSectOrig.Uom != null)
            {
                var obj = _mapper.Map<DispNsVertSectOrig>(trajectory.DispNsVertSectOrig);
                _db.TrajectoryDispNsVertSectOrigs.Add(obj);
            }
        }
        private void DispEwVertSectOrig(Trajectory trajectory)
        {
            if (trajectory.DispEwVertSectOrig.Uom != null)
            {
                var obj = _mapper.Map<DispEwVertSectOrig>(trajectory.DispEwVertSectOrig);
                _db.TrajectoryDispEwVertSectOrigs.Add(obj);
            }
        }
        private void Md(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Md.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMd>(trajectory.TrajectoryStation.Md);
                _db.TrajectoryMds.Add(obj);
            }
        }
        private void Tvd(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Tvd.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryTvd>(trajectory.TrajectoryStation.Tvd);
                _db.TrajectoryTvds.Add(obj);
            }
        }

        private void Incl(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Incl.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryIncl>(trajectory.TrajectoryStation.Incl);
                _db.TrajectoryIncls.Add(obj);
            }
        }

        private void Azi(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Azi.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryAzi>(trajectory.TrajectoryStation.Azi);
                _db.TrajectoryAzis.Add(obj);
            }
        }
        private void Mtf(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Mtf.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMtf>(trajectory.TrajectoryStation.Mtf);
                _db.TrajectoryMtfs.Add(obj);
            }
        }
        private void Gtf(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Gtf.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGtf>(trajectory.TrajectoryStation.Gtf);
                _db.TrajectoryGtfs.Add(obj);
            }
        }
        private void DispNs(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.DispNs.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDispNs>(trajectory.TrajectoryStation.DispNs);
                _db.TrajectoryDispNss.Add(obj);
            }
        }
        private void DispEw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.DispEw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDispEw>(trajectory.TrajectoryStation.DispEw);
                _db.TrajectoryDispEws.Add(obj);
            }
        }
        private void VertSect(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.VertSect.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVertSect>(trajectory.TrajectoryStation.VertSect);
                _db.TrajectoryVertSects.Add(obj);
            }
        }
        private void Dls(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Dls.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDls>(trajectory.TrajectoryStation.Dls);
                _db.TrajectoryDlss.Add(obj);
            }
        }
        private void RateTurn(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RateTurn.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryRateTurn>(trajectory.TrajectoryStation.RateTurn);
                _db.TrajectoryRateTurns.Add(obj);
            }
        }
        private void RateBuild(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RateBuild.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryRateBuild>(trajectory.TrajectoryStation.RateBuild);
                _db.TrajectoryRateBuilds.Add(obj);
            }
        }
        private void MdDelta(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MdDelta.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMdDelta>(trajectory.TrajectoryStation.MdDelta);
                _db.TrajectoryMdDeltas.Add(obj);
            }
        }
        private void TvdDelta(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.TvdDelta.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryTvdDelta>(trajectory.TrajectoryStation.TvdDelta);
                _db.TrajectoryTvdDeltas.Add(obj);
            }
        }
        private void GravTotalUncert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.GravTotalUncert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTotalUncert>(trajectory.TrajectoryStation.GravTotalUncert);
                _db.TrajectoryGravTotalUncerts.Add(obj);
            }
        }
        private void DipAngleUncert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.DipAngleUncert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDipAngleUncert>(trajectory.TrajectoryStation.DipAngleUncert);
                _db.TrajectoryDipAngleUncerts.Add(obj);
            }
        }
        private void MagTotalUncert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MagTotalUncert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTotalUncert>(trajectory.TrajectoryStation.MagTotalUncert);
                _db.TrajectoryMagTotalUncerts.Add(obj);
            }
        }
        private void GravAxialRaw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.GravAxialRaw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravAxialRaw>(trajectory.TrajectoryStation.RawData.GravAxialRaw);
                _db.TrajectoryGravAxialRaws.Add(obj);
            }
        }
        private void GravTran1Raw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.GravTran1Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran1Raw>(trajectory.TrajectoryStation.RawData.GravTran1Raw);
                _db.TrajectoryGravTran1Raws.Add(obj);
            }
        }
        private void GravTran2Raw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.GravTran2Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran2Raw>(trajectory.TrajectoryStation.RawData.GravTran2Raw);
                _db.TrajectoryGravTran2Raws.Add(obj);
            }
        }
       
        
        private void GravTran2Raws(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.GravTran2Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran2Raw>(trajectory.TrajectoryStation.RawData.GravTran2Raw);
                _db.TrajectoryGravTran2Raws.Add(obj);
            }
        }
        private void MagAxialRaw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.MagAxialRaw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagAxialRaw>(trajectory.TrajectoryStation.RawData.MagAxialRaw);
                _db.TrajectoryMagAxialRaws.Add(obj);
            }
        }
        private void MagTran1Raw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.MagTran1Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTran1Raw>(trajectory.TrajectoryStation.RawData.MagTran1Raw);
                _db.TrajectoryMagTran1Raws.Add(obj);
            }
        }
        private void MagTran2Raw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.MagTran2Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTran2Raw>(trajectory.TrajectoryStation.RawData.MagTran2Raw);
                _db.TrajectoryMagTran2Raws.Add(obj);
            }
        }

        private void RawData(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData != null)
            {
                var obj = _mapper.Map<TrajectoryRawData>(trajectory.TrajectoryStation.RawData);
                _db.TrajectoryRawDatas.Add(obj);
            }
        }
        private void GravAxialAccelCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.GravAxialAccelCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravAxialAccelCor>(trajectory.TrajectoryStation.CorUsed.GravAxialAccelCor);
                _db.TrajectoryGravAxialAccelCors.Add(obj);
            }
        }

        private void GravTran2AccelCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.GravTran2AccelCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran2AccelCor>(trajectory.TrajectoryStation.CorUsed.GravTran2AccelCor);
                _db.TrajectoryGravTran2AccelCors.Add(obj);
            }
        }

        private void MagAxialDrlstrCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.MagAxialDrlstrCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagAxialDrlstrCor>(trajectory.TrajectoryStation.CorUsed.MagAxialDrlstrCor);
                _db.TrajectoryMagAxialDrlstrCors.Add(obj);
            }
        }

        private void MagTran1DrlstrCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.MagTran1DrlstrCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTran1DrlstrCor>(trajectory.TrajectoryStation.CorUsed.MagTran1DrlstrCor);
                _db.TrajectoryMagTran1DrlstrCors.Add(obj);
            }
        }

        private void MagTran2DrlstrCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.MagTran2DrlstrCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTran2DrlstrCor>(trajectory.TrajectoryStation.CorUsed.MagTran2DrlstrCor);
                _db.TrajectoryMagTran2DrlstrCors.Add(obj);
            }
        }

        private void SagIncCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.SagIncCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectorySagIncCor>(trajectory.TrajectoryStation.CorUsed.SagIncCor);
                _db.TrajectorySagIncCors.Add(obj);
            }
        }

        private void SagAziCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.SagAziCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectorySagAziCor>(trajectory.TrajectoryStation.CorUsed.SagAziCor);
                _db.TrajectorySagAziCors.Add(obj);
            }
        }
        private void StnMagDeclUsed(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.StnMagDeclUsed.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryStnMagDeclUsed>(trajectory.TrajectoryStation.CorUsed.StnMagDeclUsed);
                _db.TrajectoryStnMagDeclUseds.Add(obj);
            }
        }
        private void StnGridCorUsed(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.StnGridCorUsed.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryStnGridCorUsed>(trajectory.TrajectoryStation.CorUsed.StnGridCorUsed);
                _db.TrajectoryStnGridCorUseds.Add(obj);
            }
        }
        private void DirSensorOffset(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.DirSensorOffset.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDirSensorOffset>(trajectory.TrajectoryStation.CorUsed.DirSensorOffset);
                _db.TrajectoryDirSensorOffsets.Add(obj);
            }
        }

        private void GravTran1AccelCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.GravTran1AccelCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran1AccelCor>(trajectory.TrajectoryStation.CorUsed.GravTran1AccelCor);
                _db.TrajectoryGravTran1AccelCors.Add(obj);
            }
        }
        private void CorUsed(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed != null)
            {
                var obj = _mapper.Map<TrajectoryCorUsed>(trajectory.TrajectoryStation.CorUsed);
                _db.TrajectoryCorUseds.Add(obj);
            }
        }
        private void MagTotalFieldCalc(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Valid.MagTotalFieldCalc.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTotalFieldCalc>(trajectory.TrajectoryStation.Valid.MagTotalFieldCalc);
                _db.TrajectoryMagTotalFieldCalcs.Add(obj);
            }
        }

        private void MagDipAngleCalc(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Valid.MagDipAngleCalc.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagDipAngleCalc>(trajectory.TrajectoryStation.Valid.MagDipAngleCalc);
                _db.TrajectoryMagDipAngleCalcs.Add(obj);
            }
        }
        private void GravTotalFieldCalc(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Valid.GravTotalFieldCalc.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTotalFieldCalc>(trajectory.TrajectoryStation.Valid.GravTotalFieldCalc);
                _db.TrajectoryGravTotalFieldCalcs.Add(obj);
            }
        }
        private void Valid(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Valid != null)
            {
                var obj = _mapper.Map<TrajectoryValid>(trajectory.TrajectoryStation.Valid);
                _db.TrajectoryValids.Add(obj);
            }
        }
        private void VarianceNN(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceNN.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceNN>(trajectory.TrajectoryStation.MatrixCov.VarianceNN);
                _db.TrajectoryVarianceNNs.Add(obj);
            }
        }

       

        private void VarianceNE(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceNE.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceNE>(trajectory.TrajectoryStation.MatrixCov.VarianceNE);
                _db.TrajectoryVarianceNEs.Add(obj);
            }
        }

        private void VarianceNVert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceNVert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceNVert>(trajectory.TrajectoryStation.MatrixCov.VarianceNVert);
                _db.TrajectoryVarianceNVerts.Add(obj);
            }
        }
        private void VarianceEE(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceEE.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceEE>(trajectory.TrajectoryStation.MatrixCov.VarianceEE);
                _db.TrajectoryVarianceEEs.Add(obj);
            }
        }
        private void VarianceEVert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceEVert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceEVert>(trajectory.TrajectoryStation.MatrixCov.VarianceEVert);
                _db.TrajectoryVarianceEVerts.Add(obj);
            }
        }
        private void VarianceVertVert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceVertVert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceVertVert>(trajectory.TrajectoryStation.MatrixCov.VarianceVertVert);
                _db.TrajectoryVarianceVertVerts.Add(obj);
            }
        }
        private void BiasN(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.BiasN.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryBiasN>(trajectory.TrajectoryStation.MatrixCov.BiasN);
                _db.TrajectoryBiasNs.Add(obj);
            }
        }
        private void BiasE(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.BiasE.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryBiasE>(trajectory.TrajectoryStation.MatrixCov.BiasE);
                _db.TrajectoryBiasEs.Add(obj);
            }
        }
        private void BiasVert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.BiasVert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryBiasVert>(trajectory.TrajectoryStation.MatrixCov.BiasVert);
                _db.TrajectoryBiasVerts.Add(obj);
            }
        }
        private void MatrixCov(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov != null)
            {
                var obj = _mapper.Map<TrajectoryMatrixCov>(trajectory.TrajectoryStation.MatrixCov);
                _db.TrajectoryMatrixCovs.Add(obj);
            }
        }
        private void WellCRS(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.WellCRS !=null && item.WellCRS.UidRef != null)
                {
                    var obj = _mapper.Map<TrajectoryWellCRS>(item.WellCRS);
                    _db.TrajectoryWellCRSs.Add(obj);
                }
            }
        }
        private void Latitude(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Latitude != null &&  item.Latitude.Uom != null)
                {
                    var obj = _mapper.Map<TrajectoryLatitude>(item.Latitude);
                    _db.TrajectoryLatitudes.Add(obj);
                }
            }
        }
        private void Longitude(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Longitude != null &&  item.Longitude.Uom != null)
                {
                    var obj = _mapper.Map<TrajectoryLongitude>(item.Longitude);
                    _db.TrajectoryLongitudes.Add(obj);
                }
            }
        }
        private void Easting(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Easting != null &&  item.Easting.Uom != null)
                {
                    var obj = _mapper.Map<Easting>(item.Easting);
                    _db.Add(obj);
                }
            }
        }
        private void Northing(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Northing != null && item.Northing.Uom != null)
                {
                    var obj = _mapper.Map<TrajectoryNorthing>(item.Northing);
                    _db.TrajectoryNorthings.Add(obj);
                }
            }
        }
        private void Location(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<TrajectoryLocation>(item);
                    _db.TrajectoryLocations.Add(obj);
                }
            }
        }
        private void TrajectoryStation(Trajectory trajectory)
        {
                if (trajectory.TrajectoryStation.Uid != null)
                {
                    var obj = _mapper.Map<TrajectoryStation>(trajectory.TrajectoryStation);
                    _db.TrajectoryStations.Add(obj);
                }
        }

        private void CommonData(Trajectory trajectory)
        {
            if (trajectory.CommonData!= null)
            {
                var obj = _mapper.Map<TrajectoryCommonData>(trajectory.CommonData);
                _db.TrajectoryCommonDatas.Add(obj);
            }
        }
        #endregion Insert Trajectory

        #region Update Trajectory
        private void UpdateMdMn(Trajectory trajectory)
        {
            if (trajectory.MdMn.Uom != null)
            {
                var obj = _mapper.Map<MdMn>(trajectory.MdMn);
                _db.TrajectoryMdMns.Update(obj);
            }
        }


        private void UpdateMdMx(Trajectory trajectory)
        {
            if (trajectory.MdMx.Uom != null)
            {
                var obj = _mapper.Map<MdMx>(trajectory.MdMx);
                _db.TrajectoryMdMxs.Update(obj);
            }
        }

        private void UpdateMagDeclUsed(Trajectory trajectory)
        {
            if (trajectory.MagDeclUsed.Uom != null)
            {
                var obj = _mapper.Map<MagDeclUsed>(trajectory.MagDeclUsed);
                _db.TrajectoryMagDeclUseds.Update(obj);
            }
        }
        private void UpdateGridCorUsed(Trajectory trajectory)
        {
            if (trajectory.GridCorUsed.Uom != null)
            {
                var obj = _mapper.Map<GridCorUsed>(trajectory.GridCorUsed);
                _db.TrajectoryGridCorUseds.Update(obj);
            }
        }
        private void UpdateAziVertSect(Trajectory trajectory)
        {
            if (trajectory.AziVertSect.Uom != null)
            {
                var obj = _mapper.Map<AziVertSect>(trajectory.AziVertSect);
                _db.TrajectoryAziVertSects.Update(obj);
            }
        }
        private void UpdateDispNsVertSectOrig(Trajectory trajectory)
        {
            if (trajectory.DispNsVertSectOrig.Uom != null)
            {
                var obj = _mapper.Map<DispNsVertSectOrig>(trajectory.DispNsVertSectOrig);
                _db.TrajectoryDispNsVertSectOrigs.Update(obj);
            }
        }
        private void UpdateDispEwVertSectOrig(Trajectory trajectory)
        {
            if (trajectory.DispEwVertSectOrig.Uom != null)
            {
                var obj = _mapper.Map<DispEwVertSectOrig>(trajectory.DispEwVertSectOrig);
                _db.TrajectoryDispEwVertSectOrigs.Update(obj);
            }
        }
        private void UpdateMd(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Md.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMd>(trajectory.TrajectoryStation.Md);
                _db.TrajectoryMds.Update(obj);
            }
        }
        private void UpdateTvd(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Tvd.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryTvd>(trajectory.TrajectoryStation.Tvd);
                _db.TrajectoryTvds.Update(obj);
            }
        }

        private void UpdateIncl(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Incl.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryIncl>(trajectory.TrajectoryStation.Incl);
                _db.TrajectoryIncls.Update(obj);
            }
        }

        private void UpdateAzi(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Azi.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryAzi>(trajectory.TrajectoryStation.Azi);
                _db.TrajectoryAzis.Update(obj);
            }
        }
        private void UpdateMtf(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Mtf.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMtf>(trajectory.TrajectoryStation.Mtf);
                _db.TrajectoryMtfs.Update(obj);
            }
        }
        private void UpdateGtf(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Gtf.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGtf>(trajectory.TrajectoryStation.Gtf);
                _db.TrajectoryGtfs.Update(obj);
            }
        }
        private void UpdateDispNs(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.DispNs.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDispNs>(trajectory.TrajectoryStation.DispNs);
                _db.TrajectoryDispNss.Update(obj);
            }
        }
        private void UpdateDispEw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.DispEw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDispEw>(trajectory.TrajectoryStation.DispEw);
                _db.TrajectoryDispEws.Update(obj);
            }
        }
        private void UpdateVertSect(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.VertSect.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVertSect>(trajectory.TrajectoryStation.VertSect);
                _db.TrajectoryVertSects.Update(obj);
            }
        }
        private void UpdateDls(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Dls.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDls>(trajectory.TrajectoryStation.Dls);
                _db.TrajectoryDlss.Update(obj);
            }
        }
        private void UpdateRateTurn(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RateTurn.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryRateTurn>(trajectory.TrajectoryStation.RateTurn);
                _db.TrajectoryRateTurns.Update(obj);
            }
        }
        private void UpdateRateBuild(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RateBuild.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryRateBuild>(trajectory.TrajectoryStation.RateBuild);
                _db.TrajectoryRateBuilds.Update(obj);
            }
        }
        private void UpdateMdDelta(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MdDelta.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMdDelta>(trajectory.TrajectoryStation.MdDelta);
                _db.TrajectoryMdDeltas.Update(obj);
            }
        }
        private void UpdateTvdDelta(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.TvdDelta.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryTvdDelta>(trajectory.TrajectoryStation.TvdDelta);
                _db.TrajectoryTvdDeltas.Update(obj);
            }
        }
        private void UpdateGravTotalUncert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.GravTotalUncert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTotalUncert>(trajectory.TrajectoryStation.GravTotalUncert);
                _db.TrajectoryGravTotalUncerts.Update(obj);
            }
        }
        private void UpdateDipAngleUncert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.DipAngleUncert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDipAngleUncert>(trajectory.TrajectoryStation.DipAngleUncert);
                _db.TrajectoryDipAngleUncerts.Update(obj);
            }
        }
        private void UpdateMagTotalUncert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MagTotalUncert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTotalUncert>(trajectory.TrajectoryStation.MagTotalUncert);
                _db.TrajectoryMagTotalUncerts.Update(obj);
            }
        }
        private void UpdateGravAxialRaw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.GravAxialRaw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravAxialRaw>(trajectory.TrajectoryStation.RawData.GravAxialRaw);
                _db.TrajectoryGravAxialRaws.Update(obj);
            }
        }
        private void UpdateGravTran1Raw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.GravTran1Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran1Raw>(trajectory.TrajectoryStation.RawData.GravTran1Raw);
                _db.TrajectoryGravTran1Raws.Update(obj);
            }
        }
        private void UpdateGravTran2Raw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.GravTran2Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran2Raw>(trajectory.TrajectoryStation.RawData.GravTran2Raw);
                _db.TrajectoryGravTran2Raws.Update(obj);
            }
        }


        private void UpdateGravTran2Raws(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.GravTran2Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran2Raw>(trajectory.TrajectoryStation.RawData.GravTran2Raw);
                _db.TrajectoryGravTran2Raws.Update(obj);
            }
        }
        private void UpdateMagAxialRaw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.MagAxialRaw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagAxialRaw>(trajectory.TrajectoryStation.RawData.MagAxialRaw);
                _db.TrajectoryMagAxialRaws.Update(obj);
            }
        }
        private void UpdateMagTran1Raw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.MagTran1Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTran1Raw>(trajectory.TrajectoryStation.RawData.MagTran1Raw);
                _db.TrajectoryMagTran1Raws.Update(obj);
            }
        }
        private void UpdateMagTran2Raw(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData.MagTran2Raw.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTran2Raw>(trajectory.TrajectoryStation.RawData.MagTran2Raw);
                _db.TrajectoryMagTran2Raws.Update(obj);
            }
        }

        private void UpdateRawData(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.RawData != null)
            {
                var obj = _mapper.Map<TrajectoryRawData>(trajectory.TrajectoryStation.RawData);
                _db.TrajectoryRawDatas.Update(obj);
            }
        }
        private void UpdateGravAxialAccelCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.GravAxialAccelCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravAxialAccelCor>(trajectory.TrajectoryStation.CorUsed.GravAxialAccelCor);
                _db.TrajectoryGravAxialAccelCors.Update(obj);
            }
        }

        private void UpdateGravTran2AccelCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.GravTran2AccelCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran2AccelCor>(trajectory.TrajectoryStation.CorUsed.GravTran2AccelCor);
                _db.TrajectoryGravTran2AccelCors.Update(obj);
            }
        }

        private void UpdateMagAxialDrlstrCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.MagAxialDrlstrCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagAxialDrlstrCor>(trajectory.TrajectoryStation.CorUsed.MagAxialDrlstrCor);
                _db.TrajectoryMagAxialDrlstrCors.Update(obj);
            }
        }

        private void UpdateMagTran1DrlstrCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.MagTran1DrlstrCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTran1DrlstrCor>(trajectory.TrajectoryStation.CorUsed.MagTran1DrlstrCor);
                _db.TrajectoryMagTran1DrlstrCors.Update(obj);
            }
        }

        private void UpdateMagTran2DrlstrCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.MagTran2DrlstrCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTran2DrlstrCor>(trajectory.TrajectoryStation.CorUsed.MagTran2DrlstrCor);
                _db.TrajectoryMagTran2DrlstrCors.Update(obj);
            }
        }

        private void UpdateSagIncCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.SagIncCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectorySagIncCor>(trajectory.TrajectoryStation.CorUsed.SagIncCor);
                _db.TrajectorySagIncCors.Update(obj);
            }
        }

        private void UpdateSagAziCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.SagAziCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectorySagAziCor>(trajectory.TrajectoryStation.CorUsed.SagAziCor);
                _db.TrajectorySagAziCors.Update(obj);
            }
        }
        private void UpdateStnMagDeclUsed(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.StnMagDeclUsed.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryStnMagDeclUsed>(trajectory.TrajectoryStation.CorUsed.StnMagDeclUsed);
                _db.TrajectoryStnMagDeclUseds.Update(obj);
            }
        }
        private void UpdateStnGridCorUsed(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.StnGridCorUsed.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryStnGridCorUsed>(trajectory.TrajectoryStation.CorUsed.StnGridCorUsed);
                _db.TrajectoryStnGridCorUseds.Update(obj);
            }
        }
        private void UpdateDirSensorOffset(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.DirSensorOffset.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryDirSensorOffset>(trajectory.TrajectoryStation.CorUsed.DirSensorOffset);
                _db.TrajectoryDirSensorOffsets.Update(obj);
            }
        }

        private void UpdateGravTran1AccelCor(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed.GravTran1AccelCor.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTran1AccelCor>(trajectory.TrajectoryStation.CorUsed.GravTran1AccelCor);
                _db.TrajectoryGravTran1AccelCors.Update(obj);
            }
        }
        private void UpdateCorUsed(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.CorUsed != null)
            {
                var obj = _mapper.Map<TrajectoryCorUsed>(trajectory.TrajectoryStation.CorUsed);
                _db.TrajectoryCorUseds.Update(obj);
            }
        }
        private void UpdateMagTotalFieldCalc(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Valid.MagTotalFieldCalc.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagTotalFieldCalc>(trajectory.TrajectoryStation.Valid.MagTotalFieldCalc);
                _db.TrajectoryMagTotalFieldCalcs.Update(obj);
            }
        }

        private void UpdateMagDipAngleCalc(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Valid.MagDipAngleCalc.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryMagDipAngleCalc>(trajectory.TrajectoryStation.Valid.MagDipAngleCalc);
                _db.TrajectoryMagDipAngleCalcs.Update(obj);
            }
        }
        private void UpdateGravTotalFieldCalc(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Valid.GravTotalFieldCalc.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryGravTotalFieldCalc>(trajectory.TrajectoryStation.Valid.GravTotalFieldCalc);
                _db.TrajectoryGravTotalFieldCalcs.Update(obj);
            }
        }
        private void UpdateValid(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Valid != null)
            {
                var obj = _mapper.Map<TrajectoryValid>(trajectory.TrajectoryStation.Valid);
                _db.TrajectoryValids.Update(obj);
            }
        }
        private void UpdateVarianceNN(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceNN.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceNN>(trajectory.TrajectoryStation.MatrixCov.VarianceNN);
                _db.TrajectoryVarianceNNs.Update(obj);
            }
        }



        private void UpdateVarianceNE(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceNE.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceNE>(trajectory.TrajectoryStation.MatrixCov.VarianceNE);
                _db.TrajectoryVarianceNEs.Update(obj);
            }
        }

        private void UpdateVarianceNVert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceNVert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceNVert>(trajectory.TrajectoryStation.MatrixCov.VarianceNVert);
                _db.TrajectoryVarianceNVerts.Update(obj);
            }
        }
        private void UpdateVarianceEE(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceEE.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceEE>(trajectory.TrajectoryStation.MatrixCov.VarianceEE);
                _db.TrajectoryVarianceEEs.Update(obj);
            }
        }
        private void UpdateVarianceEVert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceEVert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceEVert>(trajectory.TrajectoryStation.MatrixCov.VarianceEVert);
                _db.TrajectoryVarianceEVerts.Update(obj);
            }
        }
        private void UpdateVarianceVertVert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.VarianceVertVert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryVarianceVertVert>(trajectory.TrajectoryStation.MatrixCov.VarianceVertVert);
                _db.TrajectoryVarianceVertVerts.Update(obj);
            }
        }
        private void UpdateBiasN(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.BiasN.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryBiasN>(trajectory.TrajectoryStation.MatrixCov.BiasN);
                _db.TrajectoryBiasNs.Update(obj);
            }
        }
        private void UpdateBiasE(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.BiasE.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryBiasE>(trajectory.TrajectoryStation.MatrixCov.BiasE);
                _db.TrajectoryBiasEs.Update(obj);
            }
        }
        private void UpdateBiasVert(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov.BiasVert.Uom != null)
            {
                var obj = _mapper.Map<TrajectoryBiasVert>(trajectory.TrajectoryStation.MatrixCov.BiasVert);
                _db.TrajectoryBiasVerts.Update(obj);
            }
        }
        private void UpdateMatrixCov(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.MatrixCov != null)
            {
                var obj = _mapper.Map<TrajectoryMatrixCov>(trajectory.TrajectoryStation.MatrixCov);
                _db.TrajectoryMatrixCovs.Update(obj);
            }
        }
        private void UpdateWellCRS(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.WellCRS != null && item.WellCRS.UidRef != null)
                {
                    var obj = _mapper.Map<TrajectoryWellCRS>(item.WellCRS);
                    _db.TrajectoryWellCRSs.Update(obj);
                }
            }
        }
        private void UpdateLatitude(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Latitude != null && item.Latitude.Uom != null)
                {
                    var obj = _mapper.Map<TrajectoryLatitude>(item.Latitude);
                    _db.TrajectoryLatitudes.Update(obj);
                }
            }
        }
        private void UpdateLongitude(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Longitude != null && item.Longitude.Uom != null)
                {
                    var obj = _mapper.Map<TrajectoryLongitude>(item.Longitude);
                    _db.TrajectoryLongitudes.Update(obj);
                }
            }
        }
        private void UpdateEasting(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Easting != null && item.Easting.Uom != null)
                {
                    var obj = _mapper.Map<Easting>(item.Easting);
                    _db.TrajectoryEastings.Update(obj);
                }
            }
        }
        private void UpdateNorthing(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Northing != null && item.Northing.Uom != null)
                {
                    var obj = _mapper.Map<TrajectoryNorthing>(item.Northing);
                    _db.TrajectoryNorthings.Update(obj);
                }
            }
        }
        private void UpdateLocation(Trajectory trajectory)
        {
            foreach (var item in trajectory.TrajectoryStation.Location)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<TrajectoryLocation>(item);
                    _db.TrajectoryLocations.Update(obj);
                }
            }
        }
        private void UpdateTrajectoryStation(Trajectory trajectory)
        {
            if (trajectory.TrajectoryStation.Uid != null)
            {
                var obj = _mapper.Map<TrajectoryStation>(trajectory.TrajectoryStation);
                _db.TrajectoryStations.Update(obj);
            }
        }

        private void UpdateCommonData(Trajectory trajectory)
        {
            if (trajectory.CommonData != null)
            {
                var obj = _mapper.Map<TrajectoryCommonData>(trajectory.CommonData);
                _db.TrajectoryCommonDatas.Update(obj);
            }
        }
    
        #endregion Update Trajectory
    }
}
