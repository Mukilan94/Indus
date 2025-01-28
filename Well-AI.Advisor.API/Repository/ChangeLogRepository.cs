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
    public class ChangeLogRepository : IChangeLogRepository

    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;

        public ChangeLogRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool ChangeLogExists(string uid)
        {
            bool value = _db.ChangeLogs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateChangeLog(ChangeLog changeLog)
        {
            try
            {
                #region Insert method 
                
                ChangeHistory(changeLog);
                StartIndex(changeLog);
                EndIndex(changeLog);
                ChangeLogCommonData(changeLog);
                _db.ChangeLogs.Add(changeLog);
                return Save();
                #endregion Insert method 
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "ChangeLogRepository CreateChangeLog", null);
                return Save();
            }
        }

        public bool DeleteChangeLog(ChangeLog changeLog)
        {
            _db.ChangeLogs.Remove(changeLog);
            return Save();
        }

        public ChangeLog GetChangeLogDetail(string Uid)
        {
            return _db.ChangeLogs.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<ChangeLog> GetChangeLogDetails()
        {
            return _db.ChangeLogs.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateChangeLog(ChangeLog changeLog)
        {
            try
            {
                _db.ChangeLogs.Update(changeLog);
                UpdateChangeHistory(changeLog);
                UpdateStartIndex(changeLog);
                UpdateEndIndex(changeLog);
                UpdateChangeLogCommonData(changeLog);

                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "ChangeLogRepository UpdateChangeLog", null);
                return Save();
            }
        }

        #region Create ChangeLog Method
        private void ChangeHistory(ChangeLog changeLog)
        {
            foreach (var item in changeLog.ChangeHistory)
            {

                if (item.ChangeType != null)
                {
                    var obj = _mapper.Map<ChangeHistory>(item);
                    _db.ChangeLogChangeHistory.Add(obj);
                }
            }
        }

        private void StartIndex(ChangeLog changeLog)
        {
            foreach (var item in changeLog.ChangeHistory)
            {

                if (item.StartIndex != null)
                {
                    var obj = _mapper.Map<StartIndex>(item.StartIndex);
                    _db.ChangeLogStartIndexs.Add(obj);
                }
            }
        }

        private void EndIndex(ChangeLog changeLog)
        {
            foreach (var item in changeLog.ChangeHistory)
            {

                if (item.EndIndex != null)
                {
                    var obj = _mapper.Map<EndIndex>(item.EndIndex);
                    _db.ChangeLogEndIndexs.Add(obj);
                }
            }
        }

        private void ChangeLogCommonData(ChangeLog changeLog)
        {
           

                if (changeLog.CommonData.DTimCreation != null)
                {
                    var obj = _mapper.Map<ChangeLogCommonData>(changeLog.CommonData);
                    _db.ChangeLogCommonData.Add(obj);
                }
            }


        #endregion Create ChangeLog Method
        #region Update ChangeLog Method
        private void UpdateChangeHistory(ChangeLog changeLog)
        {
            foreach (var item in changeLog.ChangeHistory)
            {

                if (item.ChangeType != null)
                {
                    var obj = _mapper.Map<ChangeHistory>(item);
                    _db.ChangeLogChangeHistory.Update(obj);
                }
            }
        }

        private void UpdateStartIndex(ChangeLog changeLog)
        {
            foreach (var item in changeLog.ChangeHistory)
            {

                if (item.StartIndex.Uom != null)
                {
                    var obj = _mapper.Map<StartIndex>(item.StartIndex);
                    _db.ChangeLogStartIndexs.Update(obj);
                }
            }
        }

        private void UpdateEndIndex(ChangeLog changeLog)
        {
            foreach (var item in changeLog.ChangeHistory)
            {

                if (item.EndIndex.Uom != null)
                {
                    var obj = _mapper.Map<EndIndex>(item.EndIndex);
                    _db.ChangeLogEndIndexs.Update(obj);
                }
            }
        }

        private void UpdateChangeLogCommonData(ChangeLog changeLog)
        {


            if (changeLog.CommonData.DTimCreation != null)
            {
                var obj = _mapper.Map<ChangeLogCommonData>(changeLog.CommonData);
                _db.ChangeLogCommonData.Update(obj);
            }
        }
    }

    #endregion Create ChangeLog Method
}

  


