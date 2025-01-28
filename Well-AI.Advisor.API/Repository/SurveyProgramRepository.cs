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
    public class SurveyProgramRepository : ISurveyProgramRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public SurveyProgramRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool SurveyProgramExists(string uid)
        {
            bool value = _db.SurveyPrograms.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateSurveyProgram(SurveyProgram surveyProgram)
        {
            try
            {
                MdStart(surveyProgram);
                MdEnd(surveyProgram);
                FrequencyMx(surveyProgram);
                SurveySection(surveyProgram);
                SurveyProgramCommonData(surveyProgram);
                _db.SurveyPrograms.Add(surveyProgram);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "SurveyProgramRepository CreateSurveyProgram", null);
                return Save();
            }
        }

       

        public bool DeleteSurveyProgram(SurveyProgram surveyProgram)
        {
            _db.SurveyPrograms.Remove(surveyProgram);
            return Save();
        }

        public SurveyProgram GetSurveyProgramDetail(string Uid)
        {
            return _db.SurveyPrograms.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<SurveyProgram> GetSurveyProgramDetails()
        {
            return _db.SurveyPrograms.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateSurveyProgram(SurveyProgram surveyProgram)
        {
            try
            {
                UpdateMdStart(surveyProgram);
                UpdateMdEnd(surveyProgram);
                UpdateFrequencyMx(surveyProgram);
                UpdateSurveySection(surveyProgram);
                UpdateSurveyProgramCommonData(surveyProgram);
                _db.SurveyPrograms.Update(surveyProgram);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "SurveyProgramRepository UpdateSurveyProgram", null);
                return Save();
            }
        }

        #region Insert SurveyProgram
        private void MdStart(SurveyProgram surveyProgram)
        {
            foreach (var item in surveyProgram.SurveySection)
            {
                if (item.MdStart.Uom != null)
                {
                    var obj = _mapper.Map<MdStart>(item.MdStart);
                    _db.SurveyProgramMdStart.Add(obj);
                }
            }
        }
        private void MdEnd(SurveyProgram surveyProgram)
        {
            foreach (var item in surveyProgram.SurveySection)
            {
                if (item.MdEnd.Uom != null)
                {
                    var obj = _mapper.Map<MdEnd>(item.MdEnd);
                    _db.SurveyProgramMdEnd.Add(obj);
                }
            }
        }
        private void FrequencyMx(SurveyProgram surveyProgram)
        {
            foreach (var item in surveyProgram.SurveySection)
            {
                if (item.FrequencyMx.Uom != null)
                {
                    var obj = _mapper.Map<FrequencyMx>(item.FrequencyMx);
                    _db.SurveyProgramFrequencyMx.Add(obj);
                }
            }
        }
        private void SurveySection(SurveyProgram surveyProgram)
        {
            foreach (var item in surveyProgram.SurveySection)
            {
                if (item!= null)
                {
                    var obj = _mapper.Map<SurveySection>(item);
                    _db.SurveyProgramSurveySection.Add(obj);
                }
            }
        }
        private void SurveyProgramCommonData(SurveyProgram surveyProgram)
        {
                if (surveyProgram.CommonData != null)
                {
                    var obj = _mapper.Map<SurveyProgramCommonData>(surveyProgram.CommonData);
                    _db.SurveyProgramCommonData.Add(obj);
                }
        }
        #endregion Insert SurveyProgram

        #region Update SurveyProgram
        private void UpdateMdStart(SurveyProgram surveyProgram)
        {
            foreach (var item in surveyProgram.SurveySection)
            {
                if (item.MdStart.Uom != null)
                {
                    var obj = _mapper.Map<MdStart>(item.MdStart);
                    _db.SurveyProgramMdStart.Update(obj);
                }
            }
        }
        private void UpdateMdEnd(SurveyProgram surveyProgram)
        {
            foreach (var item in surveyProgram.SurveySection)
            {
                if (item.MdEnd.Uom != null)
                {
                    var obj = _mapper.Map<MdEnd>(item.MdEnd);
                    _db.SurveyProgramMdEnd.Update(obj);
                }
            }
        }
        private void UpdateFrequencyMx(SurveyProgram surveyProgram)
        {
            foreach (var item in surveyProgram.SurveySection)
            {
                if (item.FrequencyMx.Uom != null)
                {
                    var obj = _mapper.Map<FrequencyMx>(item.FrequencyMx);
                    _db.SurveyProgramFrequencyMx.Update(obj);
                }
            }
        }
        private void UpdateSurveySection(SurveyProgram surveyProgram)
        {
            foreach (var item in surveyProgram.SurveySection)
            {
                if (item != null)
                {
                    var obj = _mapper.Map<SurveySection>(item);
                    _db.SurveyProgramSurveySection.Update(obj);
                }
            }
        }
        private void UpdateSurveyProgramCommonData(SurveyProgram surveyProgram)
        {
            if (surveyProgram.CommonData != null)
            {
                var obj = _mapper.Map<SurveyProgramCommonData>(surveyProgram.CommonData);
                _db.SurveyProgramCommonData.Update(obj);
            }
        }
        #endregion Update SurveyProgram
    }
}
