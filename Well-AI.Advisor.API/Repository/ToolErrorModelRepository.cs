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
    public class ToolErrorModelRepository : IToolErrorModelRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public ToolErrorModelRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool ToolErrorModelExists(string uid)
        {
            bool value = _db.ToolErrorModels.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateToolErrorModel(ToolErrorModel toolErrorModel)
        {
            try
            {
                Authorization(toolErrorModel);
                OperatingCondition(toolErrorModel);
                UseErrorTermSet(toolErrorModel);
                Term(toolErrorModel);
                Value(toolErrorModel);
                ErrorTermValue(toolErrorModel);
                Start(toolErrorModel);
                End(toolErrorModel);
                OperatingInterval(toolErrorModel);
                Speed(toolErrorModel);
                GyroInitialization(toolErrorModel);
                GyroReinitializationDistance(toolErrorModel);
                ModelParameters(toolErrorModel);
                ToolErrorModelCommonData(toolErrorModel);
                Min(toolErrorModel);
                Max(toolErrorModel);
                _db.ToolErrorModels.Add(toolErrorModel);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "ToolErrorModelRepository CreateToolErrorModel", null);
                return Save();
            }
        }



        public bool DeleteToolErrorModel(ToolErrorModel toolErrorModel)
        {
            _db.ToolErrorModels.Remove(toolErrorModel);
            return Save();
        }

        public ToolErrorModel GetToolErrorModelDetail(string Uid)
        {
            return _db.ToolErrorModels.FirstOrDefault(x => x.Uid == Uid);

        }

        public ICollection<ToolErrorModel> GetToolErrorModelDetails()
        {
            return _db.ToolErrorModels.OrderBy(x => x.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateToolErrorModel(ToolErrorModel toolErrorModel)
        {
            try
            {
                UpdateAuthorization(toolErrorModel);
                UpdateOperatingCondition(toolErrorModel);
                UpdateUseErrorTermSet(toolErrorModel);
                UpdateTerm(toolErrorModel);
                UpdateValue(toolErrorModel);
                UpdateErrorTermValue(toolErrorModel);
                UpdateStart(toolErrorModel);
                UpdateEnd(toolErrorModel);
                UpdateOperatingInterval(toolErrorModel);
                UpdateSpeed(toolErrorModel);
                UpdateGyroInitialization(toolErrorModel);
                UpdateGyroReinitializationDistance(toolErrorModel);
                UpdateModelParameters(toolErrorModel);
                UpdateToolErrorModelCommonData(toolErrorModel);
                UpdateMin(toolErrorModel);
                UpdateMax(toolErrorModel);
                _db.ToolErrorModels.Update(toolErrorModel);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "ToolErrorModelRepository UpdateToolError", null);
                return Save();
            }
        }

        #region Insert ToolErrorModel
        private void Authorization(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.Authorization != null)
            {
                var obj = _mapper.Map<ToolErrorModelAuthorization>(toolErrorModel.Authorization);
                _db.ToolErrorModelAuthorizations.Add(obj);
            }
        }
        private void Min(ToolErrorModel toolErrorModel)
        {

            if (toolErrorModel.OperatingCondition.Min.Uom != null)
            {
                var obj = _mapper.Map<Min>(toolErrorModel.OperatingCondition.Min);
                _db.ToolErrorModelMins.Add(obj);
            }

        }

        private void Max(ToolErrorModel toolErrorModel)
        {

            if (toolErrorModel.OperatingCondition.Max.Uom != null)
            {
                var obj = _mapper.Map<Max>(toolErrorModel.OperatingCondition.Max);
                _db.ToolErrorModelMaxs.Add(obj);

            }
        }

        private void OperatingCondition(ToolErrorModel toolErrorModel)
        {
           
                if (toolErrorModel.OperatingCondition.Uid != null)
                {
                    var obj = _mapper.Map<OperatingCondition>(toolErrorModel.OperatingCondition);
                    _db.ToolErrorModelOperatingConditions.Add(obj);
                
            }
        }
        private void UseErrorTermSet(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.UseErrorTermSet.UidRef != null)
            {
                var obj = _mapper.Map<UseErrorTermSet>(toolErrorModel.UseErrorTermSet);
                _db.ToolErrorModelUseErrorTermSets.Add(obj);
            }
        }
        private void Term(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.ErrorTermValue)
            {
                if (item.Term.UidRef != null)
                {
                    var obj = _mapper.Map<Term>(item.Term);
                    _db.ToolErrorModelTerms.Add(obj);
                }
            }
        }
        private void Value(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.ErrorTermValue)
            {
                if (item.Value.Uom != null)
                {
                    var obj = _mapper.Map<ToolErrorValue>(item.Value);
                    _db.ToolErrorModelValues.Add(obj);
                }
            }
        }

        private void ErrorTermValue(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.ErrorTermValue)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<ErrorTermValue>(item);
                    _db.ToolErrorModelErrorTermValues.Add(obj);
                }
            }
        }
        private void Start(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.OperatingInterval)
            {
                if (item.Start.Uom != null)
                {
                    var obj = _mapper.Map<Start>(item.Start);
                    _db.ToolErrorModelStarts.Add(obj);
                }
            }
        }
        private void End(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.OperatingInterval)
            {
                if (item.End.Uom != null)
                {
                    var obj = _mapper.Map<End>(item.End);
                    _db.ToolErrorModelEnds.Add(obj);
                }
            }
        }
        private void Speed(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.OperatingInterval)
            {
                if (item.Speed.Uom != null)
                {
                    var obj = _mapper.Map<Speed>(item.Speed);
                    _db.ToolErrorModelSpeeds.Add(obj);
                }
            }
        }

        private void OperatingInterval(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.OperatingInterval)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<OperatingInterval>(item);
                    _db.ToolErrorModelOperatingIntervals.Add(obj);
                }
            }
        }

        private void GyroInitialization(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.ModelParameters.GyroInitialization.Uom != null)
            {
                var obj = _mapper.Map<GyroInitialization>(toolErrorModel.ModelParameters.GyroInitialization);
                _db.ToolErrorModelGyroInitializations.Add(obj);
            }
        }
        private void GyroReinitializationDistance(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.ModelParameters.GyroReinitializationDistance.Uom != null)
            {
                var obj = _mapper.Map<GyroReinitializationDistance>(toolErrorModel.ModelParameters.GyroReinitializationDistance);
                _db.ToolErrorModelGyroReinitializationDistances.Add(obj);
            }
        }
        private void ModelParameters(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.ModelParameters != null)
            {
                var obj = _mapper.Map<ModelParameters>(toolErrorModel.ModelParameters);
                _db.ToolErrorModelModelParameter.Add(obj);
            }
        }
        private void ToolErrorModelCommonData(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.CommonData != null)
            {
                var obj = _mapper.Map<ToolErrorModelCommonData>(toolErrorModel.CommonData);
                _db.ToolErrorModelCommonDatas.Add(obj);
            }
        }
        #endregion Insert ToolErrorModel

        #region Update ToolErrorModel
        private void UpdateAuthorization(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.Authorization != null)
            {
                var obj = _mapper.Map<ToolErrorModelAuthorization>(toolErrorModel.Authorization);
                _db.ToolErrorModelAuthorizations.Update(obj);
            }
        }
        private void UpdateMin(ToolErrorModel toolErrorModel)
        {
           
                if (toolErrorModel.OperatingCondition.Min.Uom != null)
                {
                    var obj = _mapper.Map<Min>(toolErrorModel.OperatingCondition.Min);
                    _db.ToolErrorModelMins.Update(obj);
                }
            
        }

        private void UpdateMax(ToolErrorModel toolErrorModel)
        {
           
                if (toolErrorModel.OperatingCondition.Max.Uom != null)
                {
                    var obj = _mapper.Map<Max>(toolErrorModel.OperatingCondition.Max);
                    _db.ToolErrorModelMaxs.Update(obj);
                }
            
        }

        private void UpdateOperatingCondition(ToolErrorModel toolErrorModel)
        {
         
                if (toolErrorModel.OperatingCondition.Uid != null)
                {
                    var obj = _mapper.Map<OperatingCondition>(toolErrorModel.OperatingCondition);
                    _db.ToolErrorModelOperatingConditions.Update(obj);
                }
            
        }
        private void UpdateUseErrorTermSet(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.UseErrorTermSet.UidRef != null)
            {
                var obj = _mapper.Map<UseErrorTermSet>(toolErrorModel.UseErrorTermSet);
                _db.ToolErrorModelUseErrorTermSets.Update(obj);
            }
        }
        private void UpdateTerm(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.ErrorTermValue)
            {
                if (item.Term.UidRef != null)
                {
                    var obj = _mapper.Map<Term>(item.Term);
                    _db.ToolErrorModelTerms.Update(obj);
                }
            }
        }
        private void UpdateValue(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.ErrorTermValue)
            {
                if (item.Value.Uom != null)
                {
                    var obj = _mapper.Map<ToolErrorValue>(item.Value);
                    _db.ToolErrorModelValues.Update(obj);
                }
            }
        }

        private void UpdateErrorTermValue(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.ErrorTermValue)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<ErrorTermValue>(item);
                    _db.ToolErrorModelErrorTermValues.Update(obj);
                }
            }
        }
        private void UpdateStart(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.OperatingInterval)
            {
                if (item.Start.Uom != null)
                {
                    var obj = _mapper.Map<Start>(item.Start);
                    _db.ToolErrorModelStarts.Update(obj);
                }
            }
        }
        private void UpdateEnd(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.OperatingInterval)
            {
                if (item.End.Uom != null)
                {
                    var obj = _mapper.Map<End>(item.End);
                    _db.ToolErrorModelEnds.Update(obj);
                }
            }
        }
        private void UpdateSpeed(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.OperatingInterval)
            {
                if (item.Speed.Uom != null)
                {
                    var obj = _mapper.Map<Speed>(item.Speed);
                    _db.ToolErrorModelSpeeds.Update(obj);
                }
            }
        }

        private void UpdateOperatingInterval(ToolErrorModel toolErrorModel)
        {
            foreach (var item in toolErrorModel.OperatingInterval)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<OperatingInterval>(item);
                    _db.ToolErrorModelOperatingIntervals.Update(obj);
                }
            }
        }

        private void UpdateGyroInitialization(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.ModelParameters.GyroInitialization.Uom != null)
            {
                var obj = _mapper.Map<GyroInitialization>(toolErrorModel.ModelParameters.GyroInitialization);
                _db.ToolErrorModelGyroInitializations.Update(obj);
            }
        }
        private void UpdateGyroReinitializationDistance(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.ModelParameters.GyroReinitializationDistance.Uom != null)
            {
                var obj = _mapper.Map<GyroReinitializationDistance>(toolErrorModel.ModelParameters.GyroReinitializationDistance);
                _db.ToolErrorModelGyroReinitializationDistances.Update(obj);
            }
        }
        private void UpdateModelParameters(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.ModelParameters != null)
            {
                var obj = _mapper.Map<ModelParameters>(toolErrorModel.ModelParameters);
                _db.ToolErrorModelModelParameter.Update(obj);
            }
        }
        private void UpdateToolErrorModelCommonData(ToolErrorModel toolErrorModel)
        {
            if (toolErrorModel.CommonData != null)
            {
                var obj = _mapper.Map<ToolErrorModelCommonData>(toolErrorModel.CommonData);
                _db.ToolErrorModelCommonDatas.Update(obj);
            }
        }
        #endregion Update ToolErrorModel
    }
}
