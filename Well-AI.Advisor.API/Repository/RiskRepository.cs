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
    public class RiskRepository : IRiskRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public RiskRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool RiskExists(string uid)
        {
            bool value = _db.Risks.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateRisk(Risk risk)
        {
            try
            {
                ObjectReference(risk);
                MdHoleStart(risk);
                MdHoleEnd(risk);
                TvdHoleStart(risk);
                TvdHoleEnd(risk);
                DiaHole(risk);
                _db.Risks.Add(risk);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "RiskRepository CreateRisk", null);
                return Save();
            }
        }

        public bool DeleteRisk(Risk risk)
        {
            _db.Risks.Remove(risk);
            return Save();
        }

        public Risk GetRiskDetail(string Uid)
        {
            return _db.Risks.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<Risk> GetRiskDetails()
        {
            return _db.Risks.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateRisk(Risk risk)
        {
            try
            {
                UpdateObjectReference(risk);
                UpdateMdHoleStart(risk);
                UpdateMdHoleEnd(risk);
                UpdateTvdHoleStart(risk);
                UpdateTvdHoleEnd(risk);
                UpdateDiaHole(risk);
                _db.Risks.Update(risk);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "RiskRepository UpdateRisk", null);
                return Save();
            }
        }

        #region Insert Risk
        private void ObjectReference(Risk risk)
        {
            if (risk.ObjectReference.UidRef != null)
            {
                var obj = _mapper.Map<RiskObjectReference>(risk.ObjectReference);
                _db.RiskObjectReferences.Add(obj);
            }
        }
        private void MdHoleStart(Risk risk)
        {
            if (risk.MdHoleStart.Uom != null)
            {
                var obj = _mapper.Map<RiskMdHoleStart>(risk.MdHoleStart);
                _db.RiskMdHoleStarts.Add(obj);
            }
        }
        private void MdHoleEnd(Risk risk)
        {
            if (risk.MdHoleEnd.Uom != null)
            {
                var obj = _mapper.Map<RiskMdHoleEnd>(risk.MdHoleEnd);
                _db.RiskMdHoleEnds.Add(obj);
            }
        }
        private void TvdHoleStart(Risk risk)
        {
            if (risk.TvdHoleStart.Uom != null)
            {
                var obj = _mapper.Map<RiskTvdHoleStart>(risk.TvdHoleStart);
                _db.RiskTvdHoleStarts.Add(obj);
            }
        }

        private void TvdHoleEnd(Risk risk)
        {
            if (risk.TvdHoleEnd.Uom != null)
            {
                var obj = _mapper.Map<RiskTvdHoleEnd>(risk.TvdHoleEnd);
                _db.RiskTvdHoleEnds.Add(obj);
            }
        }

        private void DiaHole(Risk risk)
        {
            if (risk.DiaHole.Uom != null)
            {
                var obj = _mapper.Map<RiskDiaHole>(risk.DiaHole);
                _db.RiskDiaHole.Add(obj);
            }
        }
        #endregion Insert Risk

        #region Update Risk
        private void UpdateObjectReference(Risk risk)
        {
            if (risk.ObjectReference.UidRef != null)
            {
                var obj = _mapper.Map<RiskObjectReference>(risk.ObjectReference);
                _db.RiskObjectReferences.Update(obj);
            }
        }
        private void UpdateMdHoleStart(Risk risk)
        {
            if (risk.MdHoleStart.Uom != null)
            {
                var obj = _mapper.Map<RiskMdHoleStart>(risk.MdHoleStart);
                _db.RiskMdHoleStarts.Update(obj);
            }
        }
        private void UpdateMdHoleEnd(Risk risk)
        {
            if (risk.MdHoleEnd.Uom != null)
            {
                var obj = _mapper.Map<RiskMdHoleEnd>(risk.MdHoleEnd);
                _db.RiskMdHoleEnds.Update(obj);
            }
        }
        private void UpdateTvdHoleStart(Risk risk)
        {
            if (risk.TvdHoleStart.Uom != null)
            {
                var obj = _mapper.Map<RiskTvdHoleStart>(risk.TvdHoleStart);
                _db.RiskTvdHoleStarts.Update(obj);
            }
        }

        private void UpdateTvdHoleEnd(Risk risk)
        {
            if (risk.TvdHoleEnd.Uom != null)
            {
                var obj = _mapper.Map<RiskTvdHoleEnd>(risk.TvdHoleEnd);
                _db.RiskTvdHoleEnds.Update(obj);
            }
        }
        private void UpdateDiaHole(Risk risk)
        {
            if (risk.DiaHole.Uom != null)
            {
                var obj = _mapper.Map<RiskDiaHole>(risk.DiaHole);
                _db.RiskDiaHole.Update(obj);
            }
        }
        #endregion Update Risk
    }
}
