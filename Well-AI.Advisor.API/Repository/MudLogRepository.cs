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
    public class MudLogRepository : IMudLogRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public MudLogRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool MudLogExists(string uid)
        {
            bool value = _db.MudLogs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateMudLog(MudLog mudLog)
        {
            try
            {
                StartMd(mudLog);
                EndMd(mudLog);
                MdTop(mudLog);
                MdBottom(mudLog);
                CommonTime(mudLog);
                Parameter(mudLog);
                Force(mudLog);
                TvdTop(mudLog);
                TvdBase(mudLog);
                RopAv(mudLog);
                RopMn(mudLog);
                RopMx(mudLog);
                WobAv(mudLog);
                TqAv(mudLog);
                RpmAv(mudLog);
                WtMudAv(mudLog);
                EcdTdAv(mudLog);
                DensShale(mudLog);
                Abundance(mudLog);
                Lithology(mudLog);
                StainPc(mudLog);
                NatFlorPc(mudLog);
                Show(mudLog);
                WtMudIn(mudLog);
                WtMudOut(mudLog);
                ETimChromCycle(mudLog);
                MethAv(mudLog);
                MethMn(mudLog);
                MethMx(mudLog);
                EthAv(mudLog);
                EthMn(mudLog);
                EthMx(mudLog);
                PropAv(mudLog);
                PropMn(mudLog);
                PropMx(mudLog);
                IbutAv(mudLog);
                IbutMn(mudLog);
                IbutMx(mudLog);
                NbutAv(mudLog);
                NbutMn(mudLog);
                NbutMx(mudLog);
                IpentAv(mudLog);
                IpentMn(mudLog);
                IpentMx(mudLog);
                NpentAv(mudLog);
                NpentMn(mudLog);
                NpentMx(mudLog);
                EpentAv(mudLog);
                EpentMn(mudLog);
                EpentMx(mudLog);
                IhexAv(mudLog);
                IhexMn(mudLog);
                IhexMx(mudLog);
                NhexAv(mudLog);
                NhexMn(mudLog);
                NhexMx(mudLog);
                Co2Av(mudLog);
                Co2Mn(mudLog);
                Co2Mx(mudLog);
                H2sAv(mudLog);
                H2sMn(mudLog);
                H2sMx(mudLog);
                Acetylene(mudLog);
                Chromatograph(mudLog);
                GasAv(mudLog);
                GasPeak(mudLog);
                GasBackgnd(mudLog);
                GasConAv(mudLog);
                GasConMx(mudLog);
                GasTrip(mudLog);
                MudGas(mudLog);
                DensBulk(mudLog);
                Calcite(mudLog);
                Dolomite(mudLog);
                Cec(mudLog);
                CalcStab(mudLog);
                Lithostratigraphic(mudLog);
                Chronostratigraphic(mudLog);
                SizeMn(mudLog);
                SizeMx(mudLog);
                LenPlug(mudLog);
                GeologyInterval(mudLog);
                CommonData(mudLog);
                NhexMn(mudLog);
                _db.MudLogs.Add(mudLog);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "MudLogRepository CreateMudLog", null);
                return Save();
            }
        }

        public bool DeleteMudLog(MudLog mudLog)
        {
            _db.MudLogs.Remove(mudLog);
            return Save();
        }

        public MudLog GetMudLogDetail(string Uid)
        {
            return _db.MudLogs.FirstOrDefault(x => x.Uid == Uid);

        }

        public ICollection<MudLog> GetMudLogDetails()
        {
            return _db.MudLogs.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMudLog(MudLog mudLog)
        {
            try
            {
                UpdateStartMd(mudLog);
                UpdateEndMd(mudLog);
                UpdateMdTop(mudLog);
                UpdateMdBottom(mudLog);
                UpdateCommonTime(mudLog);
                UpdateParameter(mudLog);
                UpdateForce(mudLog);
                UpdateTvdTop(mudLog);
                UpdateTvdBase(mudLog);
                UpdateRopAv(mudLog);
                UpdateRopMn(mudLog);
                UpdateRopMx(mudLog);
                UpdateWobAv(mudLog);
                UpdateTqAv(mudLog);
                UpdateRpmAv(mudLog);
                UpdateWtMudAv(mudLog);
                UpdateEcdTdAv(mudLog);
                UpdateDensShale(mudLog);
                UpdateAbundance(mudLog);
                UpdateLithology(mudLog);
                UpdateStainPc(mudLog);
                UpdateNatFlorPc(mudLog);
                UpdateShow(mudLog);
                UpdateWtMudIn(mudLog);
                UpdateWtMudOut(mudLog);
                UpdateETimChromCycle(mudLog);
                UpdateMethAv(mudLog);
                UpdateMethMn(mudLog);
                UpdateMethMx(mudLog);
                UpdateEthAv(mudLog);
                UpdateEthMn(mudLog);
                UpdateEthMx(mudLog);
                UpdatePropAv(mudLog);
                UpdatePropMn(mudLog);
                UpdatePropMx(mudLog);
                UpdateIbutAv(mudLog);
                UpdateIbutMn(mudLog);
                UpdateIbutMx(mudLog);
                UpdateNbutAv(mudLog);
                UpdateNbutMn(mudLog);
                UpdateNbutMx(mudLog);
                UpdateIpentAv(mudLog);
                UpdateIpentMn(mudLog);
                UpdateIpentMx(mudLog);
                UpdateNpentAv(mudLog);
                UpdateNpentMn(mudLog);
                UpdateNpentMx(mudLog);
                UpdateEpentAv(mudLog);
                UpdateEpentMn(mudLog);
                UpdateEpentMx(mudLog);
                UpdateIhexAv(mudLog);
                UpdateIhexMn(mudLog);
                UpdateIhexMx(mudLog);
                UpdateNhexAv(mudLog);
                UpdateNhexMn(mudLog);
                UpdateNhexMx(mudLog);
                UpdateCo2Av(mudLog);
                UpdateCo2Mn(mudLog);
                UpdateCo2Mx(mudLog);
                UpdateH2sAv(mudLog);
                UpdateH2sMn(mudLog);
                UpdateH2sMx(mudLog);
                UpdateAcetylene(mudLog);
                UpdateChromatograph(mudLog);
                UpdateGasAv(mudLog);
                UpdateGasPeak(mudLog);
                UpdateGasBackgnd(mudLog);
                UpdateGasConAv(mudLog);
                UpdateGasConMx(mudLog);
                UpdateGasTrip(mudLog);
                UpdateMudGas(mudLog);
                UpdateDensBulk(mudLog);
                UpdateCalcite(mudLog);
                UpdateDolomite(mudLog);
                UpdateCec(mudLog);
                UpdateCalcStab(mudLog);
                UpdateLithostratigraphic(mudLog);
                UpdateChronostratigraphic(mudLog);
                UpdateSizeMn(mudLog);
                UpdateSizeMx(mudLog);
                UpdateLenPlug(mudLog);
                UpdateGeologyInterval(mudLog);
                UpdateCommonData(mudLog);
                UpdateNhexMn(mudLog);
                _db.MudLogs.Update(mudLog);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "MudLogRepository UpdateMudLog", null);
                return Save();
            }
        }

        #region Insert Mudlog
        private void StartMd(MudLog mudLog)
        {
            if (mudLog.StartMd.Uom != null)
            {
                var obj = _mapper.Map<MudLogStartMd>(mudLog.StartMd);
                _db.MudLogStartMd.Add(obj);
            }
        }
        private void EndMd(MudLog mudLog)
        {
            if (mudLog.EndMd.Uom != null)
            {
                var obj = _mapper.Map<MudLogEndMd>(mudLog.EndMd);
                _db.MudLogEndMd.Add(obj);
            }
        }
        private void MdTop(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.MdTop.Uom != null)
                {
                    var obj = _mapper.Map<MudLogMdTop>(item.MdTop);
                    _db.MudLogMdTop.Add(obj);
                }
            }
        }
        private void MdBottom(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.MdBottom.Uom != null)
                {
                    var obj = _mapper.Map<MudLogMdBottom>(item.MdBottom);
                    _db.MudLogMdBottom.Add(obj);
                }
            }
        }
        private void CommonTime(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.CommonTime != null)
                {
                    var obj = _mapper.Map<MudLogCommonTime>(item.CommonTime);
                    _db.MudLogCommonTime.Add(obj);
                }
            }
        }

        private void Force(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.Force != null && item.Force.Uom != null)
                {
                    var obj = _mapper.Map<MudLogForce>(item.Force);
                    _db.MudLogForce.Add(obj);
                }
            }
        }
        private void Parameter(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<MudLogParameter>(item);
                    _db.MudLogParameter.Add(obj);
                }
            }
        }
        private void TvdTop(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<MudLogTvdTop>(mudLog.GeologyInterval.TvdTop);
                _db.MudLogTvdTop.Add(obj);
            }
        }
        private void TvdBase(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.TvdBase.Uom != null)
            {
                var obj = _mapper.Map<MudLogTvdBase>(mudLog.GeologyInterval.TvdBase);
                _db.MudLogTvdBase.Add(obj);
            }
        }
        private void RopAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.RopAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogRopAv>(mudLog.GeologyInterval.RopAv);
                _db.MudLogRopAv.Add(obj);
            }
        }
        private void RopMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.RopMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogRopMn>(mudLog.GeologyInterval.RopMn);
                _db.MudLogRopMn.Add(obj);
            }
        }
        private void RopMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.RopMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogRopMx>(mudLog.GeologyInterval.RopMx);
                _db.MudLogRopMx.Add(obj);
            }
        }
        private void WobAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.WobAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogWobAv>(mudLog.GeologyInterval.WobAv);
                _db.MudLogWobAv.Add(obj);
            }
        }
        private void TqAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.TqAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogTqAv>(mudLog.GeologyInterval.TqAv);
                _db.MudLogTqAv.Add(obj);
            }
        }
        private void RpmAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.RpmAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogRpmAv>(mudLog.GeologyInterval.RpmAv);
                _db.MudLogRpmAv.Add(obj);
            }
        }
        private void WtMudAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.WtMudAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogWtMudAv>(mudLog.GeologyInterval.WtMudAv);
                _db.MudLogWtMudAv.Add(obj);
            }
        }
        private void EcdTdAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.EcdTdAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogEcdTdAv>(mudLog.GeologyInterval.EcdTdAv);
                _db.MudLogEcdTdAv.Add(obj);
            }
        }
        private void DensShale(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.DensShale.Uom != null)
            {
                var obj = _mapper.Map<MudLogDensShale>(mudLog.GeologyInterval.DensShale);
                _db.MudLogDensShale.Add(obj);
            }
        }
        private void Abundance(MudLog mudLog)
        {

            if (mudLog.GeologyInterval.Lithology.Qualifier.Abundance.Uom != null)
            {
                var obj = _mapper.Map<MudLogAbundance>(mudLog.GeologyInterval.Lithology.Qualifier.Abundance);
                _db.MudLogAbundance.Add(obj);
            }

        }
        private void Qualifier(MudLog mudLog)
        {

            if (mudLog.GeologyInterval.Lithology.Qualifier.Uid != null)
            {
                var obj = _mapper.Map<MudLogQualifier>(mudLog.GeologyInterval.Lithology.Qualifier);
                _db.MudLogQualifier.Add(obj);
            }
        }
        private void Lithology(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Lithology.Uid != null)
            {
                var obj = _mapper.Map<MudLogLithology>(mudLog.GeologyInterval.Lithology);
                _db.MudLogLithology.Add(obj);
            }
        }
        private void StainPc(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Show.StainPc.Uom != null)
            {
                var obj = _mapper.Map<MudLogStainPc>(mudLog.GeologyInterval.Show.StainPc);
                _db.MudLogStainPc.Add(obj);
            }
        }
        private void NatFlorPc(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Show.NatFlorPc.Uom != null)
            {
                var obj = _mapper.Map<MudLogNatFlorPc>(mudLog.GeologyInterval.Show.NatFlorPc);
                _db.MudLogNatFlorPc.Add(obj);
            }
        }
        private void Show(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Show != null)
            {
                var obj = _mapper.Map<MudLogShow>(mudLog.GeologyInterval.Show);
                _db.MudLogShow.Add(obj);
            }
        }
        private void WtMudIn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.WtMudIn.Uom != null)
            {
                var obj = _mapper.Map<MudLogWtMudIn>(mudLog.GeologyInterval.Chromatograph.WtMudIn);
                _db.MudLogWtMudIn.Add(obj);
            }
        }
        private void WtMudOut(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.WtMudOut.Uom != null)
            {
                var obj = _mapper.Map<MudLogWtMudOut>(mudLog.GeologyInterval.Chromatograph.WtMudOut);
                _db.MudLogWtMudOut.Add(obj);
            }
        }
        private void ETimChromCycle(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.ETimChromCycle.Uom != null)
            {
                var obj = _mapper.Map<MudLogETimChromCycle>(mudLog.GeologyInterval.Chromatograph.ETimChromCycle);
                _db.MudLogETimChromCycle.Add(obj);
            }
        }
        private void MethAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.MethAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogMethAv>(mudLog.GeologyInterval.Chromatograph.MethAv);
                _db.MudLogMethAv.Add(obj);
            }
        }
        private void MethMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.MethMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogMethMn>(mudLog.GeologyInterval.Chromatograph.MethMn);
                _db.MudLogMethMn.Add(obj);
            }
        }
        private void MethMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.MethMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogMethMx>(mudLog.GeologyInterval.Chromatograph.MethMx);
                _db.MudLogMethMx.Add(obj);
            }
        }
        private void EthAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EthAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogEthAv>(mudLog.GeologyInterval.Chromatograph.EthAv);
                _db.MudLogEthAv.Add(obj);
            }
        }
        private void EthMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EthMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogEthMn>(mudLog.GeologyInterval.Chromatograph.EthMn);
                _db.MudLogEthMn.Add(obj);
            }
        }
        private void EthMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EthMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogEthMx>(mudLog.GeologyInterval.Chromatograph.EthMx);
                _db.MudLogEthMx.Add(obj);
            }
        }
        private void PropAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.PropAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogPropAv>(mudLog.GeologyInterval.Chromatograph.PropAv);
                _db.MudLogPropAv.Add(obj);
            }
        }
        private void PropMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.PropMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogPropMn>(mudLog.GeologyInterval.Chromatograph.PropMn);
                _db.MudLogPropMn.Add(obj);
            }
        }
        private void PropMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.PropMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogPropMx>(mudLog.GeologyInterval.Chromatograph.PropMx);
                _db.MudLogPropMx.Add(obj);
            }
        }
        private void IbutAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IbutAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogIbutAv>(mudLog.GeologyInterval.Chromatograph.IbutAv);
                _db.MudLogIbutAv.Add(obj);
            }
        }
        private void IbutMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IbutMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogIbutMn>(mudLog.GeologyInterval.Chromatograph.IbutMn);
                _db.MudLogIbutMn.Add(obj);
            }
        }
        private void IbutMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IbutMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogIbutMx>(mudLog.GeologyInterval.Chromatograph.IbutMx);
                _db.MudLogIbutMx.Add(obj);
            }
        }
        private void NbutAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NbutAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogNbutAv>(mudLog.GeologyInterval.Chromatograph.NbutAv);
                _db.MudLogNbutAv.Add(obj);
            }
        }
        private void NbutMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NbutMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogNbutMn>(mudLog.GeologyInterval.Chromatograph.NbutMn);
                _db.MudLogNbutMn.Add(obj);
            }
        }
        private void NbutMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NbutMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogNbutMx>(mudLog.GeologyInterval.Chromatograph.NbutMx);
                _db.MudLogNbutMx.Add(obj);
            }
        }
        private void IpentAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IpentAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogIpentAv>(mudLog.GeologyInterval.Chromatograph.IpentAv);
                _db.MudLogIpentAv.Add(obj);
            }
        }
        private void IpentMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IpentMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogIpentMn>(mudLog.GeologyInterval.Chromatograph.IpentMn);
                _db.MudLogIpentMn.Add(obj);
            }
        }
        private void IpentMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IpentMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogIpentMx>(mudLog.GeologyInterval.Chromatograph.IpentMx);
                _db.MudLogIpentMx.Add(obj);
            }
        }
        private void NpentAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NpentAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogNpentAv>(mudLog.GeologyInterval.Chromatograph.NpentAv);
                _db.MudLogNpentAv.Add(obj);
            }
        }
        private void NpentMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NpentMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogNpentMn>(mudLog.GeologyInterval.Chromatograph.NpentMn);
                _db.MudLogNpentMn.Add(obj);
            }
        }
        private void NpentMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NpentMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogNpentMx>(mudLog.GeologyInterval.Chromatograph.NpentMx);
                _db.MudLogNpentMx.Add(obj);
            }
        }
        private void EpentAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EpentAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogEpentAv>(mudLog.GeologyInterval.Chromatograph.EpentAv);
                _db.MudLogEpentAv.Add(obj);
            }
        }
        private void EpentMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EpentMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogEpentMn>(mudLog.GeologyInterval.Chromatograph.EpentMn);
                _db.MudLogEpentMn.Add(obj);
            }
        }
        private void EpentMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EpentMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogEpentMx>(mudLog.GeologyInterval.Chromatograph.EpentMx);
                _db.MudLogEpentMx.Add(obj);
            }
        }
        private void IhexAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IhexAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogIhexAv>(mudLog.GeologyInterval.Chromatograph.IhexAv);
                _db.MudLogIhexAv.Add(obj);
            }
        }
        private void IhexMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IhexMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogIhexMn>(mudLog.GeologyInterval.Chromatograph.IhexMn);
                _db.MudLogIhexMn.Add(obj);
            }
        }
        private void IhexMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IhexMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogIhexMx>(mudLog.GeologyInterval.Chromatograph.IhexMx);
                _db.MudLogIhexMx.Add(obj);
            }
        }
        private void NhexAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NhexAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogNhexAv>(mudLog.GeologyInterval.Chromatograph.NhexAv);
                _db.MudLogNhexAv.Add(obj);
            }
        }
        private void NhexMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NhexMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogNhexMn>(mudLog.GeologyInterval.Chromatograph.NhexMn);
                _db.MudLogNhexMn.Add(obj);
            }
        }
        private void NhexMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NhexMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogNhexMx>(mudLog.GeologyInterval.Chromatograph.NhexMx);
                _db.MudLogNhexMx.Add(obj);
            }
        }
        private void Co2Av(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.Co2Av.Uom != null)
            {
                var obj = _mapper.Map<MudLogCo2Av>(mudLog.GeologyInterval.Chromatograph.Co2Av);
                _db.MudLogCo2Av.Add(obj);
            }
        }
        private void Co2Mn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.Co2Mn.Uom != null)
            {
                var obj = _mapper.Map<MudLogCo2Mn>(mudLog.GeologyInterval.Chromatograph.Co2Mn);
                _db.MudLogCo2Mn.Add(obj);
            }
        }
        private void Co2Mx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.Co2Mx.Uom != null)
            {
                var obj = _mapper.Map<MudLogCo2Mx>(mudLog.GeologyInterval.Chromatograph.Co2Mx);
                _db.MudLogCo2Mx.Add(obj);
            }
        }
        private void H2sAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.H2sAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogH2sAv>(mudLog.GeologyInterval.Chromatograph.H2sAv);
                _db.MudLogH2sAv.Add(obj);
            }
        }
        private void H2sMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.H2sMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogH2sMn>(mudLog.GeologyInterval.Chromatograph.H2sMn);
                _db.MudLogH2sMn.Add(obj);
            }
        }
        private void H2sMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.H2sMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogH2sMx>(mudLog.GeologyInterval.Chromatograph.H2sMx);
                _db.MudLogH2sMx.Add(obj);
            }
        }
        private void Acetylene(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.Acetylene.Uom != null)
            {
                var obj = _mapper.Map<MudLogAcetylene>(mudLog.GeologyInterval.Chromatograph.Acetylene);
                _db.MudLogAcetylene.Add(obj);
            }
        }
        private void Chromatograph(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph != null)
            {
                var obj = _mapper.Map<MudLogChromatograph>(mudLog.GeologyInterval.Chromatograph);
                _db.MudLogChromatograph.Add(obj);
            }
        }
        private void GasAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasAv>(mudLog.GeologyInterval.MudGas.GasAv);
                _db.MudLogGasAv.Add(obj);
            }
        }
        private void GasPeak(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasPeak.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasPeak>(mudLog.GeologyInterval.MudGas.GasPeak);
                _db.MudLogGasPeak.Add(obj);
            }
        }
        private void GasBackgnd(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasBackgnd.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasBackgnd>(mudLog.GeologyInterval.MudGas.GasBackgnd);
                _db.MudLogGasBackgnd.Add(obj);
            }
        }
        private void GasConAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasConAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasConAv>(mudLog.GeologyInterval.MudGas.GasConAv);
                _db.MudLogGasConAv.Add(obj);
            }
        }
        private void GasConMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasConMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasConMx>(mudLog.GeologyInterval.MudGas.GasConMx);
                _db.MudLogGasConMx.Add(obj);
            }
        }
        private void GasTrip(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasTrip.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasTrip>(mudLog.GeologyInterval.MudGas.GasTrip);
                _db.MudLogGasTrip.Add(obj);
            }
        }
        private void MudGas(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas != null)
            {
                var obj = _mapper.Map<MudLogMudGas>(mudLog.GeologyInterval.MudGas);
                _db.MudLogMudGas.Add(obj);
            }
        }
        private void DensBulk(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.DensBulk.Uom != null)
            {
                var obj = _mapper.Map<MudLogDensBulk>(mudLog.GeologyInterval.DensBulk);
                _db.MudLogDensBulk.Add(obj);
            }
        }
        private void Calcite(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Calcite.Uom != null)
            {
                var obj = _mapper.Map<MudLogCalcite>(mudLog.GeologyInterval.Calcite);
                _db.MudLogCalcite.Add(obj);
            }
        }
        private void Dolomite(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Dolomite.Uom != null)
            {
                var obj = _mapper.Map<MudLogDolomite>(mudLog.GeologyInterval.Dolomite);
                _db.MudLogDolomite.Add(obj);
            }
        }
        private void Cec(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Cec.Uom != null)
            {
                var obj = _mapper.Map<MudLogCec>(mudLog.GeologyInterval.Cec);
                _db.MudLogCec.Add(obj);
            }
        }
        private void CalcStab(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.CalcStab.Uom != null)
            {
                var obj = _mapper.Map<MudLogCalcStab>(mudLog.GeologyInterval.CalcStab);
                _db.MudLogCalcStab.Add(obj);
            }
        }
        private void Lithostratigraphic(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Lithostratigraphic != null)
            {
                var obj = _mapper.Map<MudLogLithostratigraphic>(mudLog.GeologyInterval.Lithostratigraphic);
                _db.MudLogLithostratigraphic.Add(obj);
            }
        }
        private void Chronostratigraphic(MudLog mudLog)
        {
            foreach (var item in mudLog.GeologyInterval.Chronostratigraphic)
            {
                if (item != null)
                {
                    var obj = _mapper.Map<MudLogChronostratigraphic>(item);
                    _db.MudLogChronostratigraphic.Add(obj);
                }
            }
        }

        private void SizeMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.SizeMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogSizeMn>(mudLog.GeologyInterval.SizeMn);
                _db.MudLogSizeMn.Add(obj);
            }
        }
        private void SizeMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.SizeMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogSizeMx>(mudLog.GeologyInterval.SizeMx);
                _db.MudLogSizeMx.Add(obj);
            }
        }
        private void LenPlug(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.LenPlug.Uom != null)
            {
                var obj = _mapper.Map<MudLogLenPlug>(mudLog.GeologyInterval.LenPlug);
                _db.MudLogLenPlug.Add(obj);
            }
        }
        private void GeologyInterval(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Uid != null)
            {
                var obj = _mapper.Map<MudLogGeologyInterval>(mudLog.GeologyInterval);
                _db.MudLogGeologyInterval.Add(obj);
            }
        }
        private void CommonData(MudLog mudLog)
        {
            if (mudLog.CommonData != null)
            {
                var obj = _mapper.Map<MudLogCommonData>(mudLog.CommonData);
                _db.MudLogCommonDatas.Add(obj);
            }
        }

        #endregion Insert Mudlog

        #region Update Mudlog
        private void UpdateStartMd(MudLog mudLog)
        {
            if (mudLog.StartMd.Uom != null)
            {
                var obj = _mapper.Map<MudLogStartMd>(mudLog.StartMd);
                _db.MudLogStartMd.Update(obj);
            }
        }
        private void UpdateEndMd(MudLog mudLog)
        {
            if (mudLog.EndMd.Uom != null)
            {
                var obj = _mapper.Map<MudLogEndMd>(mudLog.EndMd);
                _db.MudLogEndMd.Update(obj);
            }
        }
        private void UpdateMdTop(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.MdTop.Uom != null)
                {
                    var obj = _mapper.Map<MudLogMdTop>(item.MdTop);
                    _db.MudLogMdTop.Update(obj);
                }
            }
        }
        private void UpdateMdBottom(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.MdBottom.Uom != null)
                {
                    var obj = _mapper.Map<MudLogMdBottom>(item.MdBottom);
                    _db.MudLogMdBottom.Update(obj);
                }
            }
        }
        private void UpdateCommonTime(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.CommonTime != null)
                {
                    var obj = _mapper.Map<MudLogCommonTime>(item.CommonTime);
                    _db.MudLogCommonTime.Update(obj);
                }
            }
        }

        private void UpdateForce(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.Force != null && item.Force.Uom != null)
                {
                    var obj = _mapper.Map<MudLogForce>(item.Force);
                    _db.MudLogForce.Update(obj);
                }
            }
        }
        private void UpdateParameter(MudLog mudLog)
        {
            foreach (var item in mudLog.Parameter)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<MudLogParameter>(item);
                    _db.MudLogParameter.Update(obj);
                }
            }
        }
        private void UpdateTvdTop(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<MudLogTvdTop>(mudLog.GeologyInterval.TvdTop);
                _db.MudLogTvdTop.Update(obj);
            }
        }
        private void UpdateTvdBase(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.TvdBase.Uom != null)
            {
                var obj = _mapper.Map<MudLogTvdBase>(mudLog.GeologyInterval.TvdBase);
                _db.MudLogTvdBase.Update(obj);
            }
        }
        private void UpdateRopAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.RopAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogRopAv>(mudLog.GeologyInterval.RopAv);
                _db.MudLogRopAv.Update(obj);
            }
        }
        private void UpdateRopMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.RopMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogRopMn>(mudLog.GeologyInterval.RopMn);
                _db.MudLogRopMn.Update(obj);
            }
        }
        private void UpdateRopMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.RopMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogRopMx>(mudLog.GeologyInterval.RopMx);
                _db.MudLogRopMx.Update(obj);
            }
        }
        private void UpdateWobAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.WobAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogWobAv>(mudLog.GeologyInterval.WobAv);
                _db.MudLogWobAv.Update(obj);
            }
        }
        private void UpdateTqAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.TqAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogTqAv>(mudLog.GeologyInterval.TqAv);
                _db.MudLogTqAv.Update(obj);
            }
        }
        private void UpdateRpmAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.RpmAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogRpmAv>(mudLog.GeologyInterval.RpmAv);
                _db.MudLogRpmAv.Update(obj);
            }
        }
        private void UpdateWtMudAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.WtMudAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogWtMudAv>(mudLog.GeologyInterval.WtMudAv);
                _db.MudLogWtMudAv.Update(obj);
            }
        }
        private void UpdateEcdTdAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.EcdTdAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogEcdTdAv>(mudLog.GeologyInterval.EcdTdAv);
                _db.MudLogEcdTdAv.Update(obj);
            }
        }
        private void UpdateDensShale(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.DensShale.Uom != null)
            {
                var obj = _mapper.Map<MudLogDensShale>(mudLog.GeologyInterval.DensShale);
                _db.MudLogDensShale.Update(obj);
            }
        }
        private void UpdateAbundance(MudLog mudLog)
        {

            if (mudLog.GeologyInterval.Lithology.Qualifier.Abundance.Uom != null)
            {
                var obj = _mapper.Map<MudLogAbundance>(mudLog.GeologyInterval.Lithology.Qualifier.Abundance);
                _db.MudLogAbundance.Update(obj);
            }

        }
        private void UpdateQualifier(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Lithology.Qualifier.Uid != null)
            {
                var obj = _mapper.Map<MudLogQualifier>(mudLog.GeologyInterval.Lithology.Qualifier);
                _db.MudLogQualifier.Update(obj);
            }

        }
        private void UpdateLithology(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Lithology.Uid != null)
            {
                var obj = _mapper.Map<MudLogLithology>(mudLog.GeologyInterval.Lithology);
                _db.MudLogLithology.Update(obj);
            }
        }
        private void UpdateStainPc(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Show.StainPc.Uom != null)
            {
                var obj = _mapper.Map<MudLogStainPc>(mudLog.GeologyInterval.Show.StainPc);
                _db.MudLogStainPc.Update(obj);
            }
        }
        private void UpdateNatFlorPc(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Show.NatFlorPc.Uom != null)
            {
                var obj = _mapper.Map<MudLogNatFlorPc>(mudLog.GeologyInterval.Show.NatFlorPc);
                _db.MudLogNatFlorPc.Update(obj);
            }
        }
        private void UpdateShow(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Show != null)
            {
                var obj = _mapper.Map<MudLogShow>(mudLog.GeologyInterval.Show);
                _db.MudLogShow.Update(obj);
            }
        }
        private void UpdateWtMudIn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.WtMudIn.Uom != null)
            {
                var obj = _mapper.Map<MudLogWtMudIn>(mudLog.GeologyInterval.Chromatograph.WtMudIn);
                _db.MudLogWtMudIn.Update(obj);
            }
        }
        private void UpdateWtMudOut(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.WtMudOut.Uom != null)
            {
                var obj = _mapper.Map<MudLogWtMudOut>(mudLog.GeologyInterval.Chromatograph.WtMudOut);
                _db.MudLogWtMudOut.Update(obj);
            }
        }
        private void UpdateETimChromCycle(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.ETimChromCycle.Uom != null)
            {
                var obj = _mapper.Map<MudLogETimChromCycle>(mudLog.GeologyInterval.Chromatograph.ETimChromCycle);
                _db.MudLogETimChromCycle.Update(obj);
            }
        }
        private void UpdateMethAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.MethAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogMethAv>(mudLog.GeologyInterval.Chromatograph.MethAv);
                _db.MudLogMethAv.Update(obj);
            }
        }
        private void UpdateMethMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.MethMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogMethMn>(mudLog.GeologyInterval.Chromatograph.MethMn);
                _db.MudLogMethMn.Update(obj);
            }
        }
        private void UpdateMethMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.MethMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogMethMx>(mudLog.GeologyInterval.Chromatograph.MethMx);
                _db.MudLogMethMx.Update(obj);
            }
        }
        private void UpdateEthAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EthAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogEthAv>(mudLog.GeologyInterval.Chromatograph.EthAv);
                _db.MudLogEthAv.Update(obj);
            }
        }
        private void UpdateEthMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EthMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogEthMn>(mudLog.GeologyInterval.Chromatograph.EthMn);
                _db.MudLogEthMn.Update(obj);
            }
        }
        private void UpdateEthMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EthMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogEthMx>(mudLog.GeologyInterval.Chromatograph.EthMx);
                _db.MudLogEthMx.Update(obj);
            }
        }
        private void UpdatePropAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.PropAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogPropAv>(mudLog.GeologyInterval.Chromatograph.PropAv);
                _db.MudLogPropAv.Update(obj);
            }
        }
        private void UpdatePropMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.PropMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogPropMn>(mudLog.GeologyInterval.Chromatograph.PropMn);
                _db.MudLogPropMn.Update(obj);
            }
        }
        private void UpdatePropMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.PropMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogPropMx>(mudLog.GeologyInterval.Chromatograph.PropMx);
                _db.MudLogPropMx.Update(obj);
            }
        }
        private void UpdateIbutAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IbutAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogIbutAv>(mudLog.GeologyInterval.Chromatograph.IbutAv);
                _db.MudLogIbutAv.Update(obj);
            }
        }
        private void UpdateIbutMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IbutMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogIbutMn>(mudLog.GeologyInterval.Chromatograph.IbutMn);
                _db.MudLogIbutMn.Update(obj);
            }
        }
        private void UpdateIbutMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IbutMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogIbutMx>(mudLog.GeologyInterval.Chromatograph.IbutMx);
                _db.MudLogIbutMx.Update(obj);
            }
        }
        private void UpdateNbutAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NbutAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogNbutAv>(mudLog.GeologyInterval.Chromatograph.NbutAv);
                _db.MudLogNbutAv.Update(obj);
            }
        }
        private void UpdateNbutMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NbutMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogNbutMn>(mudLog.GeologyInterval.Chromatograph.NbutMn);
                _db.MudLogNbutMn.Update(obj);
            }
        }
        private void UpdateNbutMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NbutMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogNbutMx>(mudLog.GeologyInterval.Chromatograph.NbutMx);
                _db.MudLogNbutMx.Update(obj);
            }
        }
        private void UpdateIpentAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IpentAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogIpentAv>(mudLog.GeologyInterval.Chromatograph.IpentAv);
                _db.MudLogIpentAv.Update(obj);
            }
        }
        private void UpdateIpentMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IpentMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogIpentMn>(mudLog.GeologyInterval.Chromatograph.IpentMn);
                _db.MudLogIpentMn.Update(obj);
            }
        }
        private void UpdateIpentMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IpentMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogIpentMx>(mudLog.GeologyInterval.Chromatograph.IpentMx);
                _db.MudLogIpentMx.Update(obj);
            }
        }
        private void UpdateNpentAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NpentAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogNpentAv>(mudLog.GeologyInterval.Chromatograph.NpentAv);
                _db.MudLogNpentAv.Update(obj);
            }
        }
        private void UpdateNpentMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NpentMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogNpentMn>(mudLog.GeologyInterval.Chromatograph.NpentMn);
                _db.MudLogNpentMn.Update(obj);
            }
        }
        private void UpdateNpentMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NpentMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogNpentMx>(mudLog.GeologyInterval.Chromatograph.NpentMx);
                _db.MudLogNpentMx.Update(obj);
            }
        }
        private void UpdateEpentAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EpentAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogEpentAv>(mudLog.GeologyInterval.Chromatograph.EpentAv);
                _db.MudLogEpentAv.Update(obj);
            }
        }
        private void UpdateEpentMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EpentMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogEpentMn>(mudLog.GeologyInterval.Chromatograph.EpentMn);
                _db.MudLogEpentMn.Update(obj);
            }
        }
        private void UpdateEpentMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.EpentMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogEpentMx>(mudLog.GeologyInterval.Chromatograph.EpentMx);
                _db.MudLogEpentMx.Update(obj);
            }
        }
        private void UpdateIhexAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IhexAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogIhexAv>(mudLog.GeologyInterval.Chromatograph.IhexAv);
                _db.MudLogIhexAv.Update(obj);
            }
        }
        private void UpdateIhexMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IhexMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogIhexMn>(mudLog.GeologyInterval.Chromatograph.IhexMn);
                _db.MudLogIhexMn.Update(obj);
            }
        }
        private void UpdateIhexMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.IhexMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogIhexMx>(mudLog.GeologyInterval.Chromatograph.IhexMx);
                _db.MudLogIhexMx.Update(obj);
            }
        }
        private void UpdateNhexAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NhexAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogNhexAv>(mudLog.GeologyInterval.Chromatograph.NhexAv);
                _db.MudLogNhexAv.Update(obj);
            }
        }
        private void UpdateNhexMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NhexMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogNhexMn>(mudLog.GeologyInterval.Chromatograph.NhexMn);
                _db.MudLogNhexMn.Update(obj);
            }
        }
        private void UpdateNhexMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.NhexMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogNhexMx>(mudLog.GeologyInterval.Chromatograph.NhexMx);
                _db.MudLogNhexMx.Update(obj);
            }
        }
        private void UpdateCo2Av(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.Co2Av.Uom != null)
            {
                var obj = _mapper.Map<MudLogCo2Av>(mudLog.GeologyInterval.Chromatograph.Co2Av);
                _db.MudLogCo2Av.Update(obj);
            }
        }
        private void UpdateCo2Mn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.Co2Mn.Uom != null)
            {
                var obj = _mapper.Map<MudLogCo2Mn>(mudLog.GeologyInterval.Chromatograph.Co2Mn);
                _db.MudLogCo2Mn.Update(obj);
            }
        }
        private void UpdateCo2Mx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.Co2Mx.Uom != null)
            {
                var obj = _mapper.Map<MudLogCo2Mx>(mudLog.GeologyInterval.Chromatograph.Co2Mx);
                _db.MudLogCo2Mx.Update(obj);
            }
        }
        private void UpdateH2sAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.H2sAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogH2sAv>(mudLog.GeologyInterval.Chromatograph.H2sAv);
                _db.MudLogH2sAv.Update(obj);
            }
        }
        private void UpdateH2sMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.H2sMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogH2sMn>(mudLog.GeologyInterval.Chromatograph.H2sMn);
                _db.MudLogH2sMn.Update(obj);
            }
        }
        private void UpdateH2sMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.H2sMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogH2sMx>(mudLog.GeologyInterval.Chromatograph.H2sMx);
                _db.MudLogH2sMx.Update(obj);
            }
        }
        private void UpdateAcetylene(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph.Acetylene.Uom != null)
            {
                var obj = _mapper.Map<MudLogAcetylene>(mudLog.GeologyInterval.Chromatograph.Acetylene);
                _db.MudLogAcetylene.Update(obj);
            }
        }
        private void UpdateChromatograph(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chromatograph != null)
            {
                var obj = _mapper.Map<MudLogChromatograph>(mudLog.GeologyInterval.Chromatograph);
                _db.MudLogChromatograph.Update(obj);
            }
        }
        private void UpdateGasAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasAv>(mudLog.GeologyInterval.MudGas.GasAv);
                _db.MudLogGasAv.Update(obj);
            }
        }
        private void UpdateGasPeak(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasPeak.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasPeak>(mudLog.GeologyInterval.MudGas.GasPeak);
                _db.MudLogGasPeak.Update(obj);
            }
        }
        private void UpdateGasBackgnd(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasBackgnd.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasBackgnd>(mudLog.GeologyInterval.MudGas.GasBackgnd);
                _db.MudLogGasBackgnd.Update(obj);
            }
        }
        private void UpdateGasConAv(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasConAv.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasConAv>(mudLog.GeologyInterval.MudGas.GasConAv);
                _db.MudLogGasConAv.Update(obj);
            }
        }
        private void UpdateGasConMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasConMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasConMx>(mudLog.GeologyInterval.MudGas.GasConMx);
                _db.MudLogGasConMx.Update(obj);
            }
        }
        private void UpdateGasTrip(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas.GasTrip.Uom != null)
            {
                var obj = _mapper.Map<MudLogGasTrip>(mudLog.GeologyInterval.MudGas.GasTrip);
                _db.MudLogGasTrip.Update(obj);
            }
        }
        private void UpdateMudGas(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.MudGas != null)
            {
                var obj = _mapper.Map<MudLogMudGas>(mudLog.GeologyInterval.MudGas);
                _db.MudLogMudGas.Update(obj);
            }
        }
        private void UpdateDensBulk(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.DensBulk.Uom != null)
            {
                var obj = _mapper.Map<MudLogDensBulk>(mudLog.GeologyInterval.DensBulk);
                _db.MudLogDensBulk.Update(obj);
            }
        }
        private void UpdateCalcite(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Calcite.Uom != null)
            {
                var obj = _mapper.Map<MudLogCalcite>(mudLog.GeologyInterval.Calcite);
                _db.MudLogCalcite.Update(obj);
            }
        }
        private void UpdateDolomite(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Dolomite.Uom != null)
            {
                var obj = _mapper.Map<MudLogDolomite>(mudLog.GeologyInterval.Dolomite);
                _db.MudLogDolomite.Update(obj);
            }
        }
        private void UpdateCec(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Cec.Uom != null)
            {
                var obj = _mapper.Map<MudLogCec>(mudLog.GeologyInterval.Cec);
                _db.MudLogCec.Update(obj);
            }
        }
        private void UpdateCalcStab(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.CalcStab.Uom != null)
            {
                var obj = _mapper.Map<MudLogCalcStab>(mudLog.GeologyInterval.CalcStab);
                _db.MudLogCalcStab.Update(obj);
            }
        }
        private void UpdateLithostratigraphic(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Lithostratigraphic != null)
            {
                var obj = _mapper.Map<MudLogLithostratigraphic>(mudLog.GeologyInterval.Lithostratigraphic);
                _db.MudLogLithostratigraphic.Update(obj);
            }
        }
        private void UpdateChronostratigraphic(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Chronostratigraphic != null)
            {
                var obj = _mapper.Map<MudLogChronostratigraphic>(mudLog.GeologyInterval.Chronostratigraphic);
                _db.MudLogChronostratigraphic.Update(obj);
            }
        }
        private void UpdateSizeMn(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.SizeMn.Uom != null)
            {
                var obj = _mapper.Map<MudLogSizeMn>(mudLog.GeologyInterval.SizeMn);
                _db.MudLogSizeMn.Update(obj);
            }
        }
        private void UpdateSizeMx(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.SizeMx.Uom != null)
            {
                var obj = _mapper.Map<MudLogSizeMx>(mudLog.GeologyInterval.SizeMx);
                _db.MudLogSizeMx.Update(obj);
            }
        }
        private void UpdateLenPlug(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.LenPlug.Uom != null)
            {
                var obj = _mapper.Map<MudLogLenPlug>(mudLog.GeologyInterval.LenPlug);
                _db.MudLogLenPlug.Update(obj);
            }
        }
        private void UpdateGeologyInterval(MudLog mudLog)
        {
            if (mudLog.GeologyInterval.Uid != null)
            {
                var obj = _mapper.Map<MudLogGeologyInterval>(mudLog.GeologyInterval);
                _db.MudLogGeologyInterval.Update(obj);
            }
        }
        private void UpdateCommonData(MudLog mudLog)
        {
            if (mudLog.CommonData != null)
            {
                var obj = _mapper.Map<MudLogCommonData>(mudLog.CommonData);
                _db.MudLogCommonDatas.Update(obj);
            }
        }


        #endregion Update Mudlog

    }
}
