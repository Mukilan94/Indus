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
    public class WellBoreRepository : IWellBoreRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public WellBoreRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool WellBoreExists(string uid)
        {
            bool value = _db.WellBores.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateWellBore(WellBore wellBore)
        {
            try
            {
                Md(wellBore);
                Tvd(wellBore);
                MdKickoff(wellBore);
                TvdKickoff(wellBore);
                MdPlanned(wellBore);
                TvdPlanned(wellBore);
                MdSubSeaPlanned(wellBore);
                TvdSubSeaPlanned(wellBore);
                CommonData(wellBore);
              
                _db.WellBores.Add(wellBore);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "WellboreRepository CreateWellBore", null);
                return Save();
            }
        }

        public bool DeleteWellBore(WellBore wellBore)
        {
            _db.WellBores.Remove(wellBore);
            return Save();
        }

        public WellBore GetWellBoreDetail(string Uid)
        {
            return _db.WellBores.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<WellBore> GetWellBoreDetails()
        {
            return _db.WellBores.OrderBy(x => x.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateWellBore(WellBore wellBore)
        {
            try
            {
                UpdateMd(wellBore);
                UpdateTvd(wellBore);
                UpdateMdKickoff(wellBore);
                UpdateTvdKickoff(wellBore);
                UpdateMdPlanned(wellBore);
                UpdateTvdPlanned(wellBore);
                UpdateMdSubSeaPlanned(wellBore);
                UpdateTvdSubSeaPlanned(wellBore);
                UpdateCommonData(wellBore);
              
                _db.WellBores.Update(wellBore);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "WellboreRepository UpdateWellBore", null);
                return Save();
            }
        }

        #region Insert Wellbore
        private void Md(WellBore wellBore)
        {
            if(wellBore.Md.Uom!=null)
            {
                var obj = _mapper.Map<WellboreMd>(wellBore.Md);
                _db.WellboreMd.Add(obj);
            }
            
        }
        private void Tvd(WellBore wellBore)
        {
            if (wellBore.Tvd.Uom != null)
            {
                var obj = _mapper.Map<WellboreTvd>(wellBore.Tvd);
                _db.WellboreTvd.Add(obj);
            }

        }
        private void MdKickoff(WellBore wellBore)
        {
            if (wellBore.MdKickoff.Uom != null)
            {
                var obj = _mapper.Map<WellboreMdKickoff>(wellBore.MdKickoff);
                _db.WellboreMdKickoff.Add(obj);
            }

        }
        private void TvdKickoff(WellBore wellBore)
        {
            if (wellBore.TvdKickoff.Uom != null)
            {
                var obj = _mapper.Map<TvdKickoff>(wellBore.TvdKickoff);
                _db.WellboreTvdKickoff.Add(obj);
            }

        }
        private void MdPlanned(WellBore wellBore)
        {
            if (wellBore.MdPlanned.Uom != null)
            {
                var obj = _mapper.Map<WellboreMdPlanned>(wellBore.MdPlanned);
                _db.WellboreMdPlanned.Add(obj);
            }

        }
        private void TvdPlanned(WellBore wellBore)
        {
            if (wellBore.TvdPlanned.Uom != null)
            {
                var obj = _mapper.Map<TvdPlanned>(wellBore.TvdPlanned);
                _db.WellboreTvdPlanned.Add(obj);
            }

        }
        private void MdSubSeaPlanned(WellBore wellBore)
        {
            if (wellBore.MdSubSeaPlanned.Uom != null)
            {
                var obj = _mapper.Map<MdSubSeaPlanned>(wellBore.MdSubSeaPlanned);
                _db.WellboreMdSubSeaPlanned.Add(obj);
            }

        }
        private void TvdSubSeaPlanned(WellBore wellBore)
        {
            if (wellBore.TvdSubSeaPlanned.Uom != null)
            {
                var obj = _mapper.Map<TvdSubSeaPlanned>(wellBore.TvdSubSeaPlanned);
                _db.WellboreTvdSubSeaPlanned.Add(obj);
            }

        }
        private void DayTarget(WellBore wellBore)
        {
            if (wellBore.DayTarget.Uom != null)
            {
                var obj = _mapper.Map<DayTarget>(wellBore.DayTarget);
                _db.WellboreDayTarget.Add(obj);
            }

        }
        private void CommonData(WellBore wellBore)
        {
                var obj = _mapper.Map<WellBoreCommonData>(wellBore.CommonData);
                _db.WellBoreCommonData.Add(obj);
        }
       

        #endregion Wellbore

        #region Update Wellbore
        private void UpdateMd(WellBore wellBore)
        {
            if (wellBore.Md.Uom != null)
            {
                var obj = _mapper.Map<WellboreMd>(wellBore.Md);
                _db.WellboreMd.Update(obj);
            }

        }
        private void UpdateTvd(WellBore wellBore)
        {
            if (wellBore.Tvd.Uom != null)
            {
                var obj = _mapper.Map<WellboreTvd>(wellBore.Tvd);
                _db.WellboreTvd.Update(obj);
            }

        }
        private void UpdateMdKickoff(WellBore wellBore)
        {
            if (wellBore.MdKickoff.Uom != null)
            {
                var obj = _mapper.Map<WellboreMdKickoff>(wellBore.MdKickoff);
                _db.WellboreMdKickoff.Update(obj);
            }

        }
        private void UpdateTvdKickoff(WellBore wellBore)
        {
            if (wellBore.TvdKickoff.Uom != null)
            {
                var obj = _mapper.Map<TvdKickoff>(wellBore.TvdKickoff);
                _db.WellboreTvdKickoff.Update(obj);
            }

        }
        private void UpdateMdPlanned(WellBore wellBore)
        {
            if (wellBore.MdPlanned.Uom != null)
            {
                var obj = _mapper.Map<WellboreMdPlanned>(wellBore.MdPlanned);
                _db.WellboreMdPlanned.Update(obj);
            }

        }
        private void UpdateTvdPlanned(WellBore wellBore)
        {
            if (wellBore.TvdPlanned.Uom != null)
            {
                var obj = _mapper.Map<TvdPlanned>(wellBore.TvdPlanned);
                _db.WellboreTvdPlanned.Update(obj);
            }

        }
        private void UpdateMdSubSeaPlanned(WellBore wellBore)
        {
            if (wellBore.MdSubSeaPlanned.Uom != null)
            {
                var obj = _mapper.Map<MdSubSeaPlanned>(wellBore.MdSubSeaPlanned);
                _db.WellboreMdSubSeaPlanned.Update(obj);
            }

        }
        private void UpdateTvdSubSeaPlanned(WellBore wellBore)
        {
            if (wellBore.TvdSubSeaPlanned.Uom != null)
            {
                var obj = _mapper.Map<TvdSubSeaPlanned>(wellBore.TvdSubSeaPlanned);
                _db.WellboreTvdSubSeaPlanned.Update(obj);
            }

        }
        private void UpdateDayTarget(WellBore wellBore)
        {
            if (wellBore.DayTarget.Uom != null)
            {
                var obj = _mapper.Map<DayTarget>(wellBore.DayTarget);
                _db.WellboreDayTarget.Update(obj);
            }

        }
        private void UpdateCommonData(WellBore wellBore)
        {
            var obj = _mapper.Map<WellBoreCommonData>(wellBore.CommonData);
            _db.WellBoreCommonData.Update(obj);
        }
 
        #endregion Update Wellbore
    }
}
