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
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.API.Repository
{
    public class ConvCoreRepository : IConvCoreRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;

        public ConvCoreRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool ConvCoreExists(string uid)
        {
            bool value = _db.ConvCores.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateConvCore(ConvCore convCore)
        {
            try
            {
                MdCoreTop(convCore);
                MdCoreBottom(convCore);
                DiaCore(convCore);
                LenCored(convCore);
                LenBarrel(convCore);
                InclHole(convCore);
                MdTop(convCore);
                MdBottom(convCore);
                TvdTop(convCore);
                TvdBase(convCore);
                RopAv(convCore);
                RopMn(convCore);
                RopMx(convCore);
                WobAv(convCore);
                TqAv(convCore);
                RpmAv(convCore);
                WtMudAv(convCore);
                EcdTdAv(convCore);
                LithPc(convCore);
                DensShale(convCore);
                Qualifier(convCore);
                Lithology(convCore);
                StainPc(convCore);
                NatFlorPc(convCore);
                Show(convCore);
                WtMudIn(convCore);
                WtMudOut(convCore);
                ETimChromCycle(convCore);
                MethAv(convCore);
                MethMn(convCore);
                MethMx(convCore);
                EthAv(convCore);
                EthMn(convCore);
                EthMx(convCore);
                PropAv(convCore);
                PropMn(convCore);
                PropMx(convCore);
                IbutAv(convCore);
                IbutMn(convCore);
                IbutMx(convCore);
                NbutAv(convCore);
                NbutMn(convCore);
                NbutMx(convCore);
                IpentAv(convCore);
                IpentMn(convCore);
                IpentMx(convCore);
                NpentAv(convCore);
                NpentMn(convCore);
                NpentMx(convCore);
                EpentAv(convCore);
                EpentMn(convCore);
                EpentMx(convCore);
                IhexAv(convCore);
                IhexMn(convCore);
                IhexMx(convCore);
                NhexAv(convCore);
                NhexMn(convCore);
                NhexMx(convCore);
                Co2Av(convCore);
                Co2Mn(convCore);
                Co2Mx(convCore);
                H2sAv(convCore);
                H2sMn(convCore);
                H2sMx(convCore);
                Acetylene(convCore);
                Chromatograph(convCore);
                GasAv(convCore);
                GasPeak(convCore);
                GasBackgnd(convCore);
                GasConAv(convCore);
                GasConMx(convCore);
                GasTrip(convCore);
                MudGas(convCore);
                DensBulk(convCore);
                Calcite(convCore);
                Dolomite(convCore);
                Cec(convCore);
                CalcStab(convCore);
                SizeMn(convCore);
                SizeMx(convCore);
                LenPlug(convCore);
                GeologyInterval(convCore);
                ConvCoreCommonData(convCore);
                DiaBit(convCore);
                RecoverPc(convCore);
                LenRecovered(convCore);
                _db.ConvCores.Add(convCore);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "ConvCore CreateConvCore", null);
                return Save();
            }
        }

        private void MdCoreTop(ConvCore convCore)
        {
            if (convCore.MdCoreTop.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreMdCoreTop>(convCore.MdCoreTop);
                _db.ConvCoreMdCoreTops.Add(obj);
            }
        }

        private void MdCoreBottom(ConvCore convCore)
        {
            if (convCore.MdCoreBottom.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreMdCoreBottom>(convCore.MdCoreBottom);
                _db.ConvCoreMdCoreBottoms.Add(obj);
            }
        }
        private void DiaCore(ConvCore convCore)
        {
            if (convCore.DiaCore.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreDiaCore>(convCore.DiaCore);
                _db.ConvCoreDiaCores.Add(obj);
            }
        }
        private void LenCored(ConvCore convCore)
        {
            if (convCore.LenCored.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreLenCored>(convCore.LenCored);
                _db.ConvCoreLenCoreds.Add(obj);
            }
        }
        private void LenBarrel(ConvCore convCore)
        {
            if (convCore.LenBarrel.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreLenBarrel>(convCore.LenBarrel);
                _db.ConvCoreLenBarrels.Add(obj);
            }
        }

        private void InclHole(ConvCore convCore)
        {
            if (convCore.InclHole.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreInclHole>(convCore.InclHole);
                _db.ConvCoreInclHoles.Add(obj);
            }
        }


        private void MdTop(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MdTop.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreMdTop>(convCore.GeologyInterval.MdTop);
                _db.ConvCoreMdTops.Add(obj);
            }
        }
        private void MdBottom(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreMdBottom>(convCore.GeologyInterval.MdBottom);
                _db.ConvCoreMdBottoms.Add(obj);
            }
        }
        private void TvdTop(ConvCore convCore)
        {
            if (convCore.GeologyInterval.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreTvdTop>(convCore.GeologyInterval.TvdTop);
                _db.ConvCoreTvdTops.Add(obj);
            }
        }
        private void TvdBase(ConvCore convCore)
        {
            if (convCore.GeologyInterval.TvdBase.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreTvdBase>(convCore.GeologyInterval.TvdBase);
                _db.ConvCoreTvdBases.Add(obj);
            }
        }
        private void RopAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.RopAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreRopAv>(convCore.GeologyInterval.RopAv);
                _db.ConvCoreRopAvs.Add(obj);
            }
        }
        private void RopMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.RopMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreRopMn>(convCore.GeologyInterval.RopMn);
                _db.ConvCoreRopMns.Add(obj);
            }
        }
        private void RopMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.RopMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreRopMx>(convCore.GeologyInterval.RopMx);
                _db.ConvCoreRopMxs.Add(obj);
            }
        }
        private void WobAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.WobAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreWobAv>(convCore.GeologyInterval.WobAv);
                _db.ConvCoreWobAvs.Add(obj);
            }
        }
        private void TqAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.TqAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreTqAv>(convCore.GeologyInterval.TqAv);
                _db.ConvCoreTqAvs.Add(obj);
            }
        }
        private void RpmAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.RpmAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreRpmAv>(convCore.GeologyInterval.RpmAv);
                _db.ConvCoreRpmAvs.Add(obj);
            }
        }
        private void WtMudAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.WtMudAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreWtMudAv>(convCore.GeologyInterval.WtMudAv);
                _db.ConvCoreWtMudAvs.Add(obj);
            }
        }
        private void EcdTdAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.EcdTdAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreEcdTdAv>(convCore.GeologyInterval.EcdTdAv);
                _db.ConvCoreEcdTdAvs.Add(obj);
            }
        }

        private void LithPc(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Lithology.LithPc.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreLithPc>(convCore.GeologyInterval.Lithology.LithPc);
                _db.ConvCoreLithPcs.Add(obj);
            }
        }

        private void DensShale(ConvCore convCore)
        {
            if (convCore.GeologyInterval.DensShale.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreDensShale>(convCore.GeologyInterval.DensShale);
                _db.ConvCoreDensShales.Add(obj);
            }
        }

        private void Qualifier(ConvCore convCore)
        {
            foreach (var item in convCore.GeologyInterval.Lithology.Qualifier)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<ConvCoreQualifier>(item);
                    _db.ConvCoreQualifiers.Add(obj);
                }
            }
        }

        private void DiaBit(ConvCore convCore)
        {
            if (convCore.DiaBit.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreDiaBit>(convCore.DiaBit);
                _db.ConvCoreDiaBits.Add(obj);
            }
        }
        private void ConvCoreCommonData(ConvCore convCore)
        {
            if (convCore.CommonData.Comments != null)
            {
                var obj = _mapper.Map<ConvCoreCommonData>(convCore.CommonData);
                _db.ConvCoreCommonDatas.Add(obj);
            }
        }

        private void RecoverPc(ConvCore convCore)
        {
            if (convCore.RecoverPc.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreRecoverPc>(convCore.RecoverPc);
                _db.ConvCoreRecoverPcs.Add(obj);
            }
        }

        private void LenRecovered(ConvCore convCore)
        {
            if (convCore.LenRecovered.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreLenRecovered>(convCore.LenRecovered);
                _db.ConvCoreLenRecovereds.Add(obj);
            }
        }
        private void Lithology(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Lithology.Uid != null)
            {
                var obj = _mapper.Map<ConvCoreLithology>(convCore.GeologyInterval.Lithology);
                _db.ConvCoreLithologys.Add(obj);
            }
        }

        private void StainPc(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Show.StainPc.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreStainPc>(convCore.GeologyInterval.Show.StainPc);
                _db.ConvCoreStainPcs.Add(obj);
            }
        }

        private void NatFlorPc(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Show.NatFlorPc.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNatFlorPc>(convCore.GeologyInterval.Show.NatFlorPc);
                _db.ConvCoreNatFlorPcs.Add(obj);
            }
        }

       
        private void Show(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Show.Odor != null)
            {
                var obj = _mapper.Map<ConvCoreShow>(convCore.GeologyInterval.Show);
                _db.ConvCoreShows.Add(obj);
            }
        }

        private void WtMudIn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.WtMudIn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreWtMudIn>(convCore.GeologyInterval.Chromatograph.WtMudIn);
                _db.ConvCoreWtMudIns.Add(obj);
            }
        }

        private void WtMudOut(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.WtMudOut.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreWtMudOut>(convCore.GeologyInterval.Chromatograph.WtMudOut);
                _db.ConvCoreWtMudOuts.Add(obj);
            }
        }

        private void ETimChromCycle(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.ETimChromCycle.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreETimChromCycle>(convCore.GeologyInterval.Chromatograph.ETimChromCycle);
                _db.ConvCoreETimChromCycles.Add(obj);
            }
        }
        private void MethAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.MethAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreMethAv>(convCore.GeologyInterval.Chromatograph.MethAv);
                _db.ConvCoreMethAvs.Add(obj);
            }
        }
        private void MethMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.MethMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreMethMn>(convCore.GeologyInterval.Chromatograph.MethMn);
                _db.ConvCoreMethMns.Add(obj);
            }
        }
        private void MethMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.MethMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreMethMx>(convCore.GeologyInterval.Chromatograph.MethMx);
                _db.ConvCoreMethMxs.Add(obj);
            }
        }
        private void EthAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.EthAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreEthAv>(convCore.GeologyInterval.Chromatograph.EthAv);
                _db.ConvCoreEthAvs.Add(obj);
            }
        }
        private void EthMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.EthMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreEthMn>(convCore.GeologyInterval.Chromatograph.EthMn);
                _db.ConvCoreEthMns.Add(obj);
            }
        }
        private void EthMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.EthMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreEthMx>(convCore.GeologyInterval.Chromatograph.EthMx);
                _db.ConvCoreEthMxs.Add(obj);
            }
        }
        private void PropAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.PropAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCorePropAv>(convCore.GeologyInterval.Chromatograph.PropAv);
                _db.ConvCorePropAvs.Add(obj);
            }
        }
        private void PropMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.PropMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCorePropMn>(convCore.GeologyInterval.Chromatograph.PropMn);
                _db.ConvCorePropMns.Add(obj);
            }
        }
        private void PropMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.PropMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCorePropMx>(convCore.GeologyInterval.Chromatograph.PropMx);
                _db.ConvCorePropMxs.Add(obj);
            }
        }
        private void IbutAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IbutAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIbutAv>(convCore.GeologyInterval.Chromatograph.IbutAv);
                _db.ConvCoreIbutAvs.Add(obj);
            }
        }
        private void IbutMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IbutMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIbutMn>(convCore.GeologyInterval.Chromatograph.IbutMn);
                _db.ConvCoreIbutMns.Add(obj);
            }
        }
        private void IbutMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IbutMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIbutMx>(convCore.GeologyInterval.Chromatograph.IbutMx);
                _db.ConvCoreIbutMxs.Add(obj);
            }
        }
        private void NbutAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NbutAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNbutAv>(convCore.GeologyInterval.Chromatograph.NbutAv);
                _db.ConvCoreNbutAvs.Add(obj);
            }
        }
        private void NbutMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NbutMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNbutMn>(convCore.GeologyInterval.Chromatograph.NbutMn);
                _db.ConvCoreNbutMns.Add(obj);
            }
        }
        private void NbutMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NbutMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNbutMx>(convCore.GeologyInterval.Chromatograph.NbutMx);
                _db.ConvCoreNbutMxs.Add(obj);
            }
        }
        private void IpentAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IpentAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIpentAv>(convCore.GeologyInterval.Chromatograph.IpentAv);
                _db.ConvCoreIpentAvs.Add(obj);
            }
        }
        private void IpentMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IpentMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIpentMn>(convCore.GeologyInterval.Chromatograph.IpentMn);
                _db.ConvCoreIpentMns.Add(obj);
            }
        }
        private void IpentMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IpentMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIpentMx>(convCore.GeologyInterval.Chromatograph.IpentMx);
                _db.ConvCoreIpentMxs.Add(obj);
            }
        }
        private void NpentAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NpentAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNpentAv>(convCore.GeologyInterval.Chromatograph.NpentAv);
                _db.ConvCoreNpentAvs.Add(obj);
            }
        }
        private void NpentMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NpentMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNpentMn>(convCore.GeologyInterval.Chromatograph.NpentMn);
                _db.ConvCoreNpentMns.Add(obj);
            }
        }
        private void NpentMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NpentMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNpentMx>(convCore.GeologyInterval.Chromatograph.NpentMx);
                _db.ConvCoreNpentMxs.Add(obj);
            }
        }
        private void EpentAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.EpentAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreEpentAv>(convCore.GeologyInterval.Chromatograph.EpentAv);
                _db.ConvCoreEpentAvs.Add(obj);
            }
        }
        private void EpentMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.EpentMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreEpentMn>(convCore.GeologyInterval.Chromatograph.EpentMn);
                _db.ConvCoreEpentMns.Add(obj);
            }
        }
        private void EpentMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.EpentMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreEpentMx>(convCore.GeologyInterval.Chromatograph.EpentMx);
                _db.ConvCoreEpentMxs.Add(obj);
            }
        }
        private void IhexAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IhexAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIhexAv>(convCore.GeologyInterval.Chromatograph.IhexAv);
                _db.ConvCoreIhexAvs.Add(obj);
            }
        }
        private void IhexMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IhexMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIhexMn>(convCore.GeologyInterval.Chromatograph.IhexMn);
                _db.ConvCoreIhexMns.Add(obj);
            }
        }
        private void IhexMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.IhexMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreIhexMx>(convCore.GeologyInterval.Chromatograph.IhexMx);
                _db.ConvCoreIhexMxs.Add(obj);
            }
        }
        private void NhexAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NhexAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNhexAv>(convCore.GeologyInterval.Chromatograph.NhexAv);
                _db.ConvCoreNhexAvs.Add(obj);
            }
        }
        private void NhexMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NhexMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNhexMn>(convCore.GeologyInterval.Chromatograph.NhexMn);
                _db.ConvCoreNhexMns.Add(obj);
            }
        }
        private void NhexMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.NhexMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreNhexMx>(convCore.GeologyInterval.Chromatograph.NhexMx);
                _db.ConvCoreNhexMxs.Add(obj);
            }
        }
        private void Co2Av(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.Co2Av.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreCo2Av>(convCore.GeologyInterval.Chromatograph.Co2Av);
                _db.ConvCoreCo2Avs.Add(obj);
            }
        }
        private void Co2Mn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.Co2Mn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreCo2Mn>(convCore.GeologyInterval.Chromatograph.Co2Mn);
                _db.ConvCoreCo2Mns.Add(obj);
            }
        }
        private void Co2Mx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.Co2Mx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreCo2Mx>(convCore.GeologyInterval.Chromatograph.Co2Mx);
                _db.ConvCoreCo2Mxs.Add(obj);
            }
        }
        private void H2sAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.H2sAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreH2sAv>(convCore.GeologyInterval.Chromatograph.H2sAv);
                _db.ConvCoreH2sAvs.Add(obj);
            }
        }
        private void H2sMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.H2sMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreH2sMn>(convCore.GeologyInterval.Chromatograph.H2sMn);
                _db.ConvCoreH2sMns.Add(obj);
            }
        }
        private void H2sMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.H2sMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreH2sMx>(convCore.GeologyInterval.Chromatograph.H2sMx);
                _db.ConvCoreH2sMxs.Add(obj);
            }
        }
        private void Acetylene(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.Acetylene.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreAcetylene>(convCore.GeologyInterval.Chromatograph.Acetylene);
                _db.ConvCoreAcetylenes.Add(obj);
            }
        }
        private void Chromatograph(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Chromatograph.DTim != null)
            {
                var obj = _mapper.Map<ConvCoreChromatograph>(convCore.GeologyInterval.Chromatograph);
                _db.ConvCoreChromatographs.Add(obj);
            }
        }
       
        private void GasAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MudGas.GasAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreGasAv>(convCore.GeologyInterval.MudGas.GasAv);
                _db.ConvCoreGasAvs.Add(obj);
            }
        }
        private void GasPeak(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MudGas.GasPeak.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreGasPeak>(convCore.GeologyInterval.MudGas.GasPeak);
                _db.ConvCoreGasPeaks.Add(obj);
            }
        }
        private void GasBackgnd(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MudGas.GasBackgnd.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreGasBackgnd>(convCore.GeologyInterval.MudGas.GasBackgnd);
                _db.ConvCoreGasBackgnds.Add(obj);
            }
        }
        private void GasConAv(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MudGas.GasConAv.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreGasConAv>(convCore.GeologyInterval.MudGas.GasConAv);
                _db.ConvCoreGasConAvs.Add(obj);
            }
        }
        private void GasConMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MudGas.GasConMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreGasConMx>(convCore.GeologyInterval.MudGas.GasConMx);
                _db.ConvCoreGasConMxs.Add(obj);
            }
        }
        private void GasTrip(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MudGas.GasTrip.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreGasTrip>(convCore.GeologyInterval.MudGas.GasTrip);
                _db.ConvCoreGasTrips.Add(obj);
            }
        }
        private void MudGas(ConvCore convCore)
        {
            if (convCore.GeologyInterval.MudGas != null)
            {
                var obj = _mapper.Map<ConvCoreMudGas>(convCore.GeologyInterval.MudGas);
                _db.ConvCoreMudGass.Add(obj);
            }
        }
        private void DensBulk(ConvCore convCore)
        {
            if (convCore.GeologyInterval.DensBulk.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreDensBulk>(convCore.GeologyInterval.DensBulk);
                _db.ConvCoreDensBulks.Add(obj);
            }
        }
        private void Calcite(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Calcite.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreCalcite>(convCore.GeologyInterval.Calcite);
                _db.ConvCoreCalcites.Add(obj);
            }
        }
        private void Dolomite(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Dolomite.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreDolomite>(convCore.GeologyInterval.Dolomite);
                _db.ConvCoreDolomites.Add(obj);
            }
        }
        private void Cec(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Cec.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreCec>(convCore.GeologyInterval.Cec);
                _db.ConvCoreCecs.Add(obj);
            }
        }
        private void CalcStab(ConvCore convCore)
        {
            if (convCore.GeologyInterval.CalcStab.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreCalcStab>(convCore.GeologyInterval.CalcStab);
                _db.ConvCoreCalcStabs.Add(obj);
            }
        }
        private void SizeMn(ConvCore convCore)
        {
            if (convCore.GeologyInterval.SizeMn.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreSizeMn>(convCore.GeologyInterval.SizeMn);
                _db.ConvCoreSizeMns.Add(obj);
            }
        }
        private void SizeMx(ConvCore convCore)
        {
            if (convCore.GeologyInterval.SizeMx.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreSizeMx>(convCore.GeologyInterval.SizeMx);
                _db.ConvCoreSizeMxs.Add(obj);
            }
        }
        private void LenPlug(ConvCore convCore)
        {
            if (convCore.GeologyInterval.LenPlug.Uom != null)
            {
                var obj = _mapper.Map<ConvCoreLenPlug>(convCore.GeologyInterval.LenPlug);
                _db.ConvCoreLenPlugs.Add(obj);
            }
        }

        private void GeologyInterval(ConvCore convCore)
        {
            if (convCore.GeologyInterval.Uid != null)
            {
                var obj = _mapper.Map<ConvCoreGeologyInterval>(convCore.GeologyInterval);
                _db.ConvCoreGeologyIntervals.Add(obj);
            }
        }
      
       

        public bool DeleteConvCore(ConvCore convCore)
        {
            _db.ConvCores.Remove(convCore);
            return Save();
        }

        public ConvCore GetConvCoreDetail(string Uid)
        {
            return _db.ConvCores.FirstOrDefault(x => x.Uid == Uid);

        }

        public ICollection<ConvCore> GetConvCoreDetails()
        {
            return _db.ConvCores.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateConvCore(ConvCore convCore)
        {
            _db.ConvCores.Update(convCore);
            return Save();
        }
    }
}
