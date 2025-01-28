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
    public class WbGeometryRepository : IWbGeometryRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public WbGeometryRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool WbGeometryExists(string uid)
        {
            bool value = _db.WbGeometrys.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateWbGeometry(WbGeometry wbGeometry)
        {
            MdBottom(wbGeometry);
            GapAir(wbGeometry);
            DepthWaterMean(wbGeometry);
            MdTop(wbGeometry);
            TvdTop(wbGeometry);
            IdSection(wbGeometry);
            OdSection(wbGeometry);
            WtPerLen(wbGeometry);
            DiaDrift(wbGeometry);
            WbGeometrySection(wbGeometry);
            TvdBottom(wbGeometry);
            CommonData(wbGeometry);
            _db.WbGeometrys.Add(wbGeometry);
            return Save();
        }

       

        public bool DeleteWbGeometry(WbGeometry wbGeometry)
        {
            _db.WbGeometrys.Remove(wbGeometry);
            return Save();
        }

        public WbGeometry GetWbGeometryDetail(string Uid)
        {
            return _db.WbGeometrys.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<WbGeometry> GetWbGeometryDetails()
        {
            return _db.WbGeometrys.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateWbGeometry(WbGeometry wbGeometry)
        {
            try
            {
                UpdateMdBottom(wbGeometry);
                UpdateGapAir(wbGeometry);
                UpdateDepthWaterMean(wbGeometry);
                UpdateMdTop(wbGeometry);
                UpdateTvdTop(wbGeometry);
                UpdateIdSection(wbGeometry);
                UpdateOdSection(wbGeometry);
                UpdateWtPerLen(wbGeometry);
                UpdateDiaDrift(wbGeometry);
                UpdateWbGeometrySection(wbGeometry);
                UpdateTvdBottom(wbGeometry);
                UpdateCommonData(wbGeometry);

                _db.WbGeometrys.Update(wbGeometry);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "WbGeometryRewpository UpdateWbGeometry", null);
                return Save();
            }
        }

        #region Insert WebGeomentry
        private void MdBottom(WbGeometry wbGeometry)
        {
            if (wbGeometry.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryMdBottom>(wbGeometry.MdBottom);
                _db.WbGeometryMdBottom.Add(obj);
            }
        }
        private void GapAir(WbGeometry wbGeometry)
        {
            if (wbGeometry.GapAir.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryGapAir>(wbGeometry.GapAir);
                _db.WbGeometryGapAir.Add(obj);
            }
        }
        private void DepthWaterMean(WbGeometry wbGeometry)
        {
            if (wbGeometry.DepthWaterMean.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryDepthWaterMean>(wbGeometry.DepthWaterMean);
                _db.WbGeometryDepthWaterMean.Add(obj);
            }
        }
        private void MdTop(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.MdTop.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryMdTop>(wbGeometry.WbGeometrySection.MdTop);
                _db.WbGeometryMdTop.Add(obj);
            }
        }
        private void TvdTop(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryTvdTop>(wbGeometry.WbGeometrySection.TvdTop);
                _db.WbGeometryTvdTop.Add(obj);
            }
        }

        private void TvdBottom(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryTvdBottom>(wbGeometry.WbGeometrySection.TvdBottom);
                _db.WbGeometryTvdBottom.Add(obj);
            }
        }

        private void IdSection(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.IdSection.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryIdSection>(wbGeometry.WbGeometrySection.IdSection);
                _db.WbGeometryIdSection.Add(obj);
            }
        }
        private void OdSection(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.OdSection.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryOdSection>(wbGeometry.WbGeometrySection.OdSection);
                _db.WbGeometryOdSection.Add(obj);
            }
        }
        private void WtPerLen(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.WtPerLen.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryWtPerLen>(wbGeometry.WbGeometrySection.WtPerLen);
                _db.WbGeometryWtPerLen.Add(obj);
            }
        }

        private void DiaDrift(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.DiaDrift.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryDiaDrift>(wbGeometry.WbGeometrySection.DiaDrift);
                _db.WbGeometryDiaDrift.Add(obj);
            }
        }
        private void CommonData(WbGeometry wbGeometry)
        {
            if (wbGeometry.CommonData.ItemState != null)
            {
                var obj = _mapper.Map<WbGeometryCommonData>(wbGeometry.CommonData);
                _db.WbGeometryCommonData.Add(obj);
            }
        }
        private void WbGeometrySection(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.Uid != null)
            {
                var obj = _mapper.Map<WbGeometrySection>(wbGeometry.WbGeometrySection);
                _db.WbGeometrySection.Add(obj);
            }
        }

        #endregion


        #region Update WebGeomentry
        private void UpdateMdBottom(WbGeometry wbGeometry)
        {
            if (wbGeometry.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryMdBottom>(wbGeometry.MdBottom);
                _db.WbGeometryMdBottom.Update(obj);
            }
        }
        private void UpdateGapAir(WbGeometry wbGeometry)
        {
            if (wbGeometry.GapAir.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryGapAir>(wbGeometry.GapAir);
                _db.WbGeometryGapAir.Update(obj);
            }
        }
        private void UpdateDepthWaterMean(WbGeometry wbGeometry)
        {
            if (wbGeometry.DepthWaterMean.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryDepthWaterMean>(wbGeometry.DepthWaterMean);
                _db.WbGeometryDepthWaterMean.Update(obj);
            }
        }
        private void UpdateMdTop(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.MdTop.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryMdTop>(wbGeometry.WbGeometrySection.MdTop);
                _db.WbGeometryMdTop.Update(obj);
            }
        }
        private void UpdateTvdTop(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryTvdTop>(wbGeometry.WbGeometrySection.TvdTop);
                _db.WbGeometryTvdTop.Update(obj);
            }
        }

        private void UpdateTvdBottom(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryTvdBottom>(wbGeometry.WbGeometrySection.TvdBottom);
                _db.WbGeometryTvdBottom.Update(obj);
            }
        }

        private void UpdateIdSection(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.IdSection.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryIdSection>(wbGeometry.WbGeometrySection.IdSection);
                _db.WbGeometryIdSection.Update(obj);
            }
        }
        private void UpdateOdSection(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.OdSection.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryOdSection>(wbGeometry.WbGeometrySection.OdSection);
                _db.WbGeometryOdSection.Update(obj);
            }
        }
        private void UpdateWtPerLen(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.WtPerLen.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryWtPerLen>(wbGeometry.WbGeometrySection.WtPerLen);
                _db.WbGeometryWtPerLen.Update(obj);
            }
        }

        private void UpdateDiaDrift(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.DiaDrift.Uom != null)
            {
                var obj = _mapper.Map<WbGeometryDiaDrift>(wbGeometry.WbGeometrySection.DiaDrift);
                _db.WbGeometryDiaDrift.Update(obj);
            }
        }
        private void UpdateCommonData(WbGeometry wbGeometry)
        {
            if (wbGeometry.CommonData.ItemState != null)
            {
                var obj = _mapper.Map<WbGeometryCommonData>(wbGeometry.CommonData);
                _db.WbGeometryCommonData.Update(obj);
            }
        }
        private void UpdateWbGeometrySection(WbGeometry wbGeometry)
        {
            if (wbGeometry.WbGeometrySection.Uid != null)
            {
                var obj = _mapper.Map<WbGeometrySection>(wbGeometry.WbGeometrySection);
                _db.WbGeometrySection.Update(obj);
            }
        }

        #endregion
    }
}
