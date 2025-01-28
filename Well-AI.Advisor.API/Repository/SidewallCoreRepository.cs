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
    public class SidewallCoreRepository : ISidewallCoreRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;

        public SidewallCoreRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool SidewallCoreExists(string uid)
        {
            bool value = _db.SidewallCores.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateSidewallCore(SidewallCore sidewallCore)
        {
            try
            {
                MdToolReference(sidewallCore);
                MdCore(sidewallCore);
                DiaHole(sidewallCore);
                DiaPlug(sidewallCore);
                Md(sidewallCore);
                LithPc(sidewallCore);
                DensShale(sidewallCore);
                Abundance(sidewallCore);
                Qualifier(sidewallCore);
                Lithology(sidewallCore);
                StainPc(sidewallCore);
                NatFlorPc(sidewallCore);
                Show(sidewallCore);
                SwcSample(sidewallCore);
                SidewallCoresCommonData(sidewallCore);
                _db.SidewallCores.Add(sidewallCore);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "SidewallCoreRepository CreateSidewallCore", null);
                return Save();
            }
        }

        

        public bool DeleteSidewallCore(SidewallCore sidewallCore)
        {
            _db.SidewallCores.Remove(sidewallCore);
            return Save();
        }

        public SidewallCore GetSidewallCoreDetail(string Uid)
        {
            return _db.SidewallCores.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<SidewallCore> GetSidewallCoreDetails()
        {
            return _db.SidewallCores.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateSidewallCore(SidewallCore sidewallCore)
        {
            try
            {
                UpdateMdToolReference(sidewallCore);
                UpdateMdCore(sidewallCore);
                UpdateDiaHole(sidewallCore);
                UpdateDiaPlug(sidewallCore);
                UpdateMd(sidewallCore);
                UpdateLithPc(sidewallCore);
                UpdateDensShale(sidewallCore);
                UpdateAbundance(sidewallCore);
                UpdateQualifier(sidewallCore);
                UpdateLithology(sidewallCore);
                UpdateStainPc(sidewallCore);
                UpdateNatFlorPc(sidewallCore);
                UpdateShow(sidewallCore);
                UpdateSwcSample(sidewallCore);
                UpdateSidewallCoresCommonData(sidewallCore);
                _db.SidewallCores.Update(sidewallCore);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "SidewallCoreRepository UpdateSidewallCore", null);
                return Save();
            }
        }

        #region Insert Sidewallcore
        private void MdToolReference(SidewallCore sidewallCore)
        {
            if (sidewallCore.MdToolReference.Uom != null)
            {
                var obj = _mapper.Map<MdToolReference>(sidewallCore.MdToolReference);
                _db.SideWallMdToolReference.Add(obj);
            }
        }
       
        private void MdCore(SidewallCore sidewallCore)
        {
            if (sidewallCore.MdCore.Uom != null)
            {
                var obj = _mapper.Map<MdCore>(sidewallCore.MdCore);
                _db.SideWallMdCore.Add(obj);
            }
        }

        private void DiaHole(SidewallCore sidewallCore)
        {
            if (sidewallCore.DiaHole.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreDiaHole>(sidewallCore.DiaHole);
                _db.SideWallCoreDiaHole.Add(obj);
            }
        }
        private void DiaPlug(SidewallCore sidewallCore)
        {
            if (sidewallCore.DiaPlug.Uom != null)
            {
                var obj = _mapper.Map<DiaPlug>(sidewallCore.DiaPlug);
                _db.DiaPlug.Add(obj);
            }
        }
        private void Md(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Md.Uom != null)
            {
                var obj = _mapper.Map<SideWallMd>(sidewallCore.SwcSample.Md);
                _db.SideWallMd.Add(obj);
            }
        }
        private void LithPc(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.LithPc.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreLithPc>(sidewallCore.SwcSample.Lithology.LithPc);
                _db.SideWallLithPcs.Add(obj);
            }
        }
        private void DensShale(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.DensShale.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreDensShale>(sidewallCore.SwcSample.Lithology.DensShale);
                _db.SideWallCoreDensShale.Add(obj);
            }
        }

        private void Abundance(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.Qualifier.Abundance.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreAbundance>(sidewallCore.SwcSample.Lithology.Qualifier.Abundance);
                _db.SideWallCoreAbundance.Add(obj);
            }
        }

        private void StainPc(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Show.StainPc.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreStainPc>(sidewallCore.SwcSample.Show.StainPc);
                _db.SideWallCoreStainPc.Add(obj);
            }
        }
        private void NatFlorPc(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Show.NatFlorPc.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreNatFlorPc>(sidewallCore.SwcSample.Show.NatFlorPc);
                _db.SideWallCoreNatFlorPc.Add(obj);
            }
        }
        private void Show(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Show != null)
            {
                var obj = _mapper.Map<SideWallCoreShow>(sidewallCore.SwcSample.Show);
                _db.SideWallCoreShow.Add(obj);
            }
        }
        private void SwcSample(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Uid != null)
            {
                var obj = _mapper.Map<SideWallCoreSwcSample>(sidewallCore.SwcSample);
                _db.SideWallCoreSwcSample.Add(obj);
            }
        }
        private void SidewallCoresCommonData(SidewallCore sidewallCore)
        {
            if (sidewallCore.CommonData != null)
            {
                var obj = _mapper.Map<SideWallCoreCommonData>(sidewallCore.CommonData);
                _db.SideWallCoreCommonData.Add(obj);
            }
        }

        private void Qualifier(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.Qualifier.Uid != null)
            {
                var obj = _mapper.Map<SideWallCoreQualifier>(sidewallCore.SwcSample.Lithology.Qualifier);
                _db.SideWallCoreQualifier.Add(obj);
            }
        }
       
        private void Lithology(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.Uid != null)
            {
                var obj = _mapper.Map<SideWallCoreLithology>(sidewallCore.SwcSample.Lithology);
                _db.SideWallCoreLithology.Add(obj);
            }
        }

        #endregion Insert Sidewallcore

        #region Update Sidewallcore
        private void UpdateMdToolReference(SidewallCore sidewallCore)
        {
            if (sidewallCore.MdToolReference.Uom != null)
            {
                var obj = _mapper.Map<MdToolReference>(sidewallCore.MdToolReference);
                _db.SideWallMdToolReference.Update(obj);
            }
        }

        private void UpdateMdCore(SidewallCore sidewallCore)
        {
            if (sidewallCore.MdCore.Uom != null)
            {
                var obj = _mapper.Map<MdCore>(sidewallCore.MdCore);
                _db.SideWallMdCore.Update(obj);
            }
        }

        private void UpdateDiaHole(SidewallCore sidewallCore)
        {
            if (sidewallCore.DiaHole.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreDiaHole>(sidewallCore.DiaHole);
                _db.SideWallCoreDiaHole.Update(obj);
            }
        }
        private void UpdateDiaPlug(SidewallCore sidewallCore)
        {
            if (sidewallCore.DiaPlug.Uom != null)
            {
                var obj = _mapper.Map<DiaPlug>(sidewallCore.DiaPlug);
                _db.DiaPlug.Update(obj);
            }
        }
        private void UpdateMd(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Md.Uom != null)
            {
                var obj = _mapper.Map<SideWallMd>(sidewallCore.SwcSample.Md);
                _db.SideWallMd.Update(obj);
            }
        }
        private void UpdateLithPc(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.LithPc.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreLithPc>(sidewallCore.SwcSample.Lithology.LithPc);
                _db.SideWallLithPcs.Update(obj);
            }
        }
        private void UpdateDensShale(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.DensShale.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreDensShale>(sidewallCore.SwcSample.Lithology.DensShale);
                _db.SideWallCoreDensShale.Update(obj);
            }
        }

        private void UpdateAbundance(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.Qualifier.Abundance.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreAbundance>(sidewallCore.SwcSample.Lithology.Qualifier.Abundance);
                _db.SideWallCoreAbundance.Update(obj);
            }
        }

        private void UpdateStainPc(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Show.StainPc.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreStainPc>(sidewallCore.SwcSample.Show.StainPc);
                _db.SideWallCoreStainPc.Update(obj);
            }
        }
        private void UpdateNatFlorPc(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Show.NatFlorPc.Uom != null)
            {
                var obj = _mapper.Map<SideWallCoreNatFlorPc>(sidewallCore.SwcSample.Show.NatFlorPc);
                _db.SideWallCoreNatFlorPc.Update(obj);
            }
        }
        private void UpdateShow(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Show != null)
            {
                var obj = _mapper.Map<SideWallCoreShow>(sidewallCore.SwcSample.Show);
                _db.SideWallCoreShow.Update(obj);
            }
        }
        private void UpdateSwcSample(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Uid != null)
            {
                var obj = _mapper.Map<SideWallCoreSwcSample>(sidewallCore.SwcSample);
                _db.SideWallCoreSwcSample.Update(obj);
            }
        }
        private void UpdateSidewallCoresCommonData(SidewallCore sidewallCore)
        {
            if (sidewallCore.CommonData != null)
            {
                var obj = _mapper.Map<SideWallCoreCommonData>(sidewallCore.CommonData);
                _db.SideWallCoreCommonData.Update(obj);
            }
        }

        private void UpdateQualifier(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.Qualifier.Uid != null)
            {
                var obj = _mapper.Map<SideWallCoreQualifier>(sidewallCore.SwcSample.Lithology.Qualifier);
                _db.SideWallCoreQualifier.Update(obj);
            }
        }

        private void UpdateLithology(SidewallCore sidewallCore)
        {
            if (sidewallCore.SwcSample.Lithology.Uid != null)
            {
                var obj = _mapper.Map<SideWallCoreLithology>(sidewallCore.SwcSample.Lithology);
                _db.SideWallCoreLithology.Update(obj);
            }
        }
        #endregion Update Sidewallcore
    }
}
