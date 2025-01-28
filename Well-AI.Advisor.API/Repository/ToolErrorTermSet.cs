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
    public class ToolErrorTermSetRepository : IToolErrorTermSetRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public ToolErrorTermSetRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool ToolErrorTermSetExists(string uid)
        {
            bool value = _db.ToolErrorTermSets.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateToolErrorTermSet(ToolErrorTermSet toolErrorTermSet)
        {
            try
            {
                Authorization(toolErrorTermSet);
                Parameter(toolErrorTermSet);
                Function(toolErrorTermSet);
                Nomenclature(toolErrorTermSet);
                ErrorCoefficient(toolErrorTermSet);
                ErrorTerm(toolErrorTermSet);
                Constant(toolErrorTermSet);
                _db.ToolErrorTermSets.Add(toolErrorTermSet);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "ToolErrorTermSet CreateToolErrorTermSet", null);
                return Save();
            }
        }

        public bool DeleteToolErrorTermSet(ToolErrorTermSet toolErrorTermSet)
        {
            _db.ToolErrorTermSets.Remove(toolErrorTermSet);
            return Save();
        }

        public ToolErrorTermSet GetToolErrorTermSetDetail(string Uid)
        {
            return _db.ToolErrorTermSets.FirstOrDefault(x => x.Uid == Uid);

        }

        public ICollection<ToolErrorTermSet> GetToolErrorTermSetDetails()
        {
            return _db.ToolErrorTermSets.OrderBy(x => x.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateToolErrorTermSet(ToolErrorTermSet toolErrorTermSet)
        {
            try
            {
                UpdateAuthorization(toolErrorTermSet);
                UpdateParameter(toolErrorTermSet);
                UpdateFunction(toolErrorTermSet);
                UpdateNomenclature(toolErrorTermSet);
                UpdateErrorCoefficient(toolErrorTermSet);
                UpdateErrorTerm(toolErrorTermSet);
                UpdateConstant(toolErrorTermSet);
                _db.ToolErrorTermSets.Update(toolErrorTermSet);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "ToolErrorTermSet UpdateToolError", null);
                return Save();
            }
        }

        #region Insert ToolErrorTermSet
        private void Authorization(ToolErrorTermSet toolErrorTermSet)
        {
            if (toolErrorTermSet.Authorization.Author != null)
            {
                var obj = _mapper.Map<Authorization>(toolErrorTermSet.Authorization);
                _db.ToolErrorTermSetAuthorizations.Add(obj);
            }
        }
        private void Function(ToolErrorTermSet toolErrorTermSet)
        {
            foreach (var item in toolErrorTermSet.Nomenclature.Function)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<Function>(item);
                    _db.ToolErrorTermSetFunctions.Add(obj);
                }
            }
        }

        private void Parameter(ToolErrorTermSet toolErrorTermSet)
        {
            foreach (var item in toolErrorTermSet.Nomenclature.Parameter)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<ToolErrorTermSetParameter>(item);
                    _db.ToolErrorTermSetParameters.Add(obj);
                }
            }
        }
        private void Constant(ToolErrorTermSet toolErrorTermSet)
        {
            if (toolErrorTermSet.Nomenclature.Constant.Uid != null)
            {
                var obj = _mapper.Map<Constant>(toolErrorTermSet.Nomenclature.Constant);
                _db.ToolErrorTermSetConstants.Add(obj);
            }
        }
        private void Nomenclature(ToolErrorTermSet toolErrorTermSet)
        {
            if (toolErrorTermSet.Nomenclature != null)
            {
                var obj = _mapper.Map<Nomenclature>(toolErrorTermSet.Nomenclature);
                _db.ToolErrorTermSetNomenclatures.Add(obj);
            }

        }

        private void ErrorCoefficient(ToolErrorTermSet toolErrorTermSet)
        {
            foreach (var item in toolErrorTermSet.ErrorTerm)
            {
                foreach (var subItem in item.ErrorCoefficient)
                {
                    if (subItem.Uid != null)
                    {
                        var obj = _mapper.Map<ErrorCoefficient>(subItem);
                        _db.ToolErrorTermSetErrorCoefficients.Add(obj);
                    }
                }
            }
        }
        private void ErrorTerm(ToolErrorTermSet toolErrorTermSet)
        {
            foreach (var item in toolErrorTermSet.ErrorTerm)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<ErrorTerm>(item);
                    _db.ToolErrorTermSetErrorTerms.Add(obj);
                }
            }
        }

        #endregion

        #region Update ToolErrorTermSet
        private void UpdateAuthorization(ToolErrorTermSet toolErrorTermSet)
        {
            if (toolErrorTermSet.Authorization.Author != null)
            {
                var obj = _mapper.Map<Authorization>(toolErrorTermSet.Authorization);
                _db.ToolErrorTermSetAuthorizations.Update(obj);
            }
        }
        private void UpdateFunction(ToolErrorTermSet toolErrorTermSet)
        {
            foreach (var item in toolErrorTermSet.Nomenclature.Function)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<Function>(item);
                    _db.ToolErrorTermSetFunctions.Update(obj);
                }
            }
        }

        private void UpdateParameter(ToolErrorTermSet toolErrorTermSet)
        {
            foreach (var item in toolErrorTermSet.Nomenclature.Parameter)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<ToolErrorTermSetParameter>(item);
                    _db.ToolErrorTermSetParameters.Update(obj);
                }
            }
        }
        private void UpdateConstant(ToolErrorTermSet toolErrorTermSet)
        {
            if (toolErrorTermSet.Nomenclature.Constant.Uid != null)
            {
                var obj = _mapper.Map<Constant>(toolErrorTermSet.Nomenclature.Constant);
                _db.ToolErrorTermSetConstants.Update(obj);
            }
        }
        private void UpdateNomenclature(ToolErrorTermSet toolErrorTermSet)
        {
            if (toolErrorTermSet.Nomenclature != null)
            {
                var obj = _mapper.Map<Nomenclature>(toolErrorTermSet.Nomenclature);
                _db.ToolErrorTermSetNomenclatures.Update(obj);
            }

        }

        private void UpdateErrorCoefficient(ToolErrorTermSet toolErrorTermSet)
        {
            foreach (var item in toolErrorTermSet.ErrorTerm)
            {
                foreach (var subItem in item.ErrorCoefficient)
                {
                    if (subItem.Uid != null)
                    {
                        var obj = _mapper.Map<ErrorCoefficient>(subItem);
                        _db.ToolErrorTermSetErrorCoefficients.Update(obj);
                    }
                }
            }
        }
        private void UpdateErrorTerm(ToolErrorTermSet toolErrorTermSet)
        {
            foreach (var item in toolErrorTermSet.ErrorTerm)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<ErrorTerm>(item);
                    _db.ToolErrorTermSetErrorTerms.Update(obj);
                }
            }
        }

        #endregion
    }
}
