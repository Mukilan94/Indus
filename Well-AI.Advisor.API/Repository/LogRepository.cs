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
    public class LogRepository : ILogRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public LogRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool LogExists(string uid)
        {
            bool value = _db.Logs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateLog(Log log)
        {
            try
            {
                StartIndex(log);
                EndIndex(log);
                StepIncrement(log);
                LogParam(log);
                MinIndex(log);
                MaxIndex(log);
                SensorOffset(log);
                LogCurveInfo(log);
                LogData(log);
                CommonData(log);
                _db.Logs.Add(log);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "LogRepository CreateLog", null);
                return Save();
            }
        
        }


        public bool DeleteLog(Log log)
        {
            _db.Logs.Remove(log);
            return Save();
        }

        public Log GetLogDetail(string Uid)
        {
            return _db.Logs.FirstOrDefault(x => x.Uid == Uid);

        }

        public ICollection<Log> GetLogDetails()
        {
            return _db.Logs.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateLog(Log log)
        {
            try
            {
                UpdateStartIndex(log);
                UpdateEndIndex(log);
                UpdateStepIncrement(log);
                UpdateLogParam(log);
                UpdateMinIndex(log);
                UpdateMaxIndex(log);
                UpdateSensorOffset(log);
                UpdateLogCurveInfo(log);
                UpdateLogData(log);
                UpdateCommonData(log);
                _db.Logs.Update(log);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "LogRepository UpdateLog", null);
                return Save();
            }
        }

        #region Insert Log
        private void StartIndex(Log log)
        {
            if (log.StartIndex.Uom != null)
            {
                var obj = _mapper.Map<LogStartIndex>(log.StartIndex);
                _db.LogStartIndex.Add(obj);
            }
        }
        private void EndIndex(Log log)
        {
            if (log.EndIndex.Uom != null)
            {
                var obj = _mapper.Map<LogEndIndex>(log.EndIndex);
                _db.LogEndIndex.Add(obj);
            }
        }
        private void StepIncrement(Log log)
        {
            if (log.StepIncrement.Uom != null)
            {
                var obj = _mapper.Map<StepIncrement>(log.StepIncrement);
                _db.LogStepIncrements.Add(obj);
            }
        }
        private void LogParam(Log log)
        {
            foreach (var item in log.LogParam)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<LogParam>(item);
                    _db.LogParams.Add(obj);
                }
            }
        }
        private void MinIndex(Log log)
        {
            foreach (var item in log.LogCurveInfo)
            {
                if (item.MinIndex.Uom != null)
                {
                    var obj = _mapper.Map<MinIndex>(item.MinIndex);
                    _db.LogMinIndexs.Add(obj);
                }
            }
        }
        private void MaxIndex(Log log)
        {
            foreach (var item in log.LogCurveInfo)
            {
                if (item.MaxIndex.Uom != null)
                {
                    var obj = _mapper.Map<MaxIndex>(item.MaxIndex);
                    _db.LogMaxIndexs.Add(obj);
                }
            }
        }
        private void SensorOffset(Log log)
        {
            foreach (var item in log.LogCurveInfo)
            {
                if (item.SensorOffset.Uom != null)
                {
                    var obj = _mapper.Map<SensorOffset>(item.SensorOffset);
                    _db.LogSensorOffsets.Add(obj);
                }
            }
        }
        private void LogCurveInfo(Log log)
        {
            foreach (var item in log.LogCurveInfo)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<LogCurveInfo>(item);
                    _db.LogCurveInfos.Add(obj);
                }
            }
        }
        private void LogData(Log log)
        {
            if (log.LogData != null)
            {
                var obj = _mapper.Map<LogData>(log.LogData);
                _db.LogDatas.Add(obj);
            }
        }
        private void CommonData(Log log)
        {
            if (log.CommonData != null)
            {
                var obj = _mapper.Map<LogCommonData>(log.CommonData);
                _db.LogCommonDatas.Add(obj);
            }
        }
        #endregion Insert Log


        #region Update Log
        private void UpdateStartIndex(Log log)
        {
            if (log.StartIndex.Uom != null)
            {
                var obj = _mapper.Map<LogStartIndex>(log.StartIndex);
                _db.LogStartIndex.Update(obj);
            }
        }
        private void UpdateEndIndex(Log log)
        {
            if (log.EndIndex.Uom != null)
            {
                var obj = _mapper.Map<LogEndIndex>(log.EndIndex);
                _db.LogEndIndex.Update(obj);
            }
        }
        private void UpdateStepIncrement(Log log)
        {
            if (log.StepIncrement.Uom != null)
            {
                var obj = _mapper.Map<StepIncrement>(log.StepIncrement);
                _db.LogStepIncrements.Update(obj);
            }
        }
        private void UpdateLogParam(Log log)
        {
            foreach (var item in log.LogParam)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<LogParam>(item);
                    _db.LogParams.Update(obj);
                }
            }
        }
        private void UpdateMinIndex(Log log)
        {
            foreach (var item in log.LogCurveInfo)
            {
                if (item.MinIndex.Uom != null)
                {
                    var obj = _mapper.Map<MinIndex>(item.MinIndex);
                    _db.LogMinIndexs.Update(obj);
                }
            }
        }
        private void UpdateMaxIndex(Log log)
        {
            foreach (var item in log.LogCurveInfo)
            {
                if (item.MaxIndex.Uom != null)
                {
                    var obj = _mapper.Map<MaxIndex>(item.MaxIndex);
                    _db.LogMaxIndexs.Update(obj);
                }
            }
        }
        private void UpdateSensorOffset(Log log)
        {
            foreach (var item in log.LogCurveInfo)
            {
                if (item.SensorOffset.Uom != null)
                {
                    var obj = _mapper.Map<SensorOffset>(item.SensorOffset);
                    _db.LogSensorOffsets.Update(obj);
                }
            }
        }
        private void UpdateLogCurveInfo(Log log)
        {
            foreach (var item in log.LogCurveInfo)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<LogCurveInfo>(item);
                    _db.LogCurveInfos.Update(obj);
                }
            }
        }
        private void UpdateLogData(Log log)
        {
            if (log.LogData != null)
            {
                var obj = _mapper.Map<LogData>(log.LogData);
                _db.LogDatas.Update(obj);
            }
        }
        private void UpdateCommonData(Log log)
        {
            if (log.CommonData != null)
            {
                var obj = _mapper.Map<LogCommonData>(log.CommonData);
                _db.LogCommonDatas.Update(obj);
            }
        }
        #endregion Update Log
    }
}
