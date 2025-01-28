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
    public class FormationMarkerRepository : IFormationMarkerRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WebAIAdvisorContext _wdb;
        public FormationMarkerRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool FormationMarkerExists(string uid)
        {
            bool value = _db.FormationMarkers.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateFormationMarker(FormationMarker formationMarker)
        {
            try
            {
                MdPrognosed(formationMarker);
                TvdPrognosed(formationMarker);
                MdTopSample(formationMarker);
                TvdTopSample(formationMarker);
                ThicknessBed(formationMarker);
                ThicknessApparent(formationMarker);
                ThicknessPerpen(formationMarker);
                MdLogSample(formationMarker);
                TvdLogSample(formationMarker);
                Dip(formationMarker);
                DipDirection(formationMarker);
                Lithostratigraphic(formationMarker);
                Chronostratigraphic(formationMarker);
                CommonData(formationMarker);
                _db.FormationMarkers.Add(formationMarker);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "FormationMarkerRepository CreateFormationMarker", null);
                return Save();
            }
        }

        
        public bool DeleteFormationMarker(FormationMarker formationMarker)
        {
            _db.FormationMarkers.Remove(formationMarker);
            return Save();
        }

        public FormationMarker GetFormationMarkerDetail(string Uid)
        {
            return _db.FormationMarkers.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<FormationMarker> GetFormationMarkerDetails()
        {
            return _db.FormationMarkers.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateFormationMarker(FormationMarker formationMarker)
        {
            try
            {
                UpdateMdPrognosed(formationMarker);
                UpdateTvdPrognosed(formationMarker);
                UpdateMdTopSample(formationMarker);
                UpdateTvdTopSample(formationMarker);
                UpdateThicknessBed(formationMarker);
                UpdateThicknessApparent(formationMarker);
                UpdateThicknessPerpen(formationMarker);
                UpdateMdLogSample(formationMarker);
                UpdateTvdLogSample(formationMarker);
                UpdateDip(formationMarker);
                UpdateDipDirection(formationMarker);
                UpdateLithostratigraphic(formationMarker);
                UpdateChronostratigraphic(formationMarker);
                UpdateCommonData(formationMarker);
                _db.FormationMarkers.Update(formationMarker);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "FormationMarkerRepository UpdateFormationMarker", null);
                return Save();
            }
        }

        #region Insert FormationMarker
        private void MdPrognosed(FormationMarker formationMarker)
        {
            if(formationMarker.MdPrognosed.Uom !=null)
            {
                var obj = _mapper.Map<FormationMarkerMdPrognosed>(formationMarker.MdPrognosed);
                _db.FormationMarkerMdPrognoseds.Add(obj);
            }
        }
        private void TvdPrognosed(FormationMarker formationMarker)
        {
            if (formationMarker.TvdPrognosed.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerTvdPrognosed>(formationMarker.TvdPrognosed);
                _db.FormationMarkerTvdPrognoseds.Add(obj);
            }
        }
        private void MdTopSample(FormationMarker formationMarker)
        {
            if (formationMarker.MdTopSample.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerMdTopSample>(formationMarker.MdTopSample);
                _db.FormationMarkerMdTopSamples.Add(obj);
            }
        }
        private void TvdTopSample(FormationMarker formationMarker)
        {
            if (formationMarker.TvdTopSample.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerTvdTopSample>(formationMarker.TvdTopSample);
                _db.FormationMarkerTvdTopSamples.Add(obj);
            }
        }
        private void ThicknessBed(FormationMarker formationMarker)
        {
            if (formationMarker.ThicknessBed.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerThicknessBed>(formationMarker.ThicknessBed);
                _db.FormationMarkerThicknessBeds.Add(obj);
            }
        }
        private void ThicknessApparent(FormationMarker formationMarker)
        {
            if (formationMarker.ThicknessApparent.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerThicknessApparent>(formationMarker.ThicknessApparent);
                _db.FormationMarkerThicknessApparents.Add(obj);
            }
        }
        private void ThicknessPerpen(FormationMarker formationMarker)
        {
            if (formationMarker.ThicknessPerpen.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerThicknessPerpen>(formationMarker.ThicknessPerpen);
                _db.FormationMarkerThicknessPerpens.Add(obj);
            }
        }
        private void MdLogSample(FormationMarker formationMarker)
        {
            if (formationMarker.MdLogSample.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerMdLogSample>(formationMarker.MdLogSample);
                _db.FormationMarkerMdLogSamples.Add(obj);
            }
        } private void TvdLogSample(FormationMarker formationMarker)
        {
            if(formationMarker.TvdLogSample.Uom !=null)
            {
                var obj = _mapper.Map<FormationMarkerTvdLogSample>(formationMarker.TvdLogSample);
                _db.FormationMarkerTvdLogSamples.Add(obj);
            }
        }
        private void Dip(FormationMarker formationMarker)
        {
            if (formationMarker.Dip.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerDip>(formationMarker.Dip);
                _db.FormationMarkerDips.Add(obj);
            }
        }
        private void DipDirection(FormationMarker formationMarker)
        {
            if (formationMarker.DipDirection.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerDipDirection>(formationMarker.DipDirection);
                _db.FormationMarkerDipDirections.Add(obj);
            }
        }
        private void Lithostratigraphic(FormationMarker formationMarker)
        {
            if (formationMarker.Lithostratigraphic != null)
            {
                var obj = _mapper.Map<FormationMarkerLithostratigraphic>(formationMarker.Lithostratigraphic);
                _db.FormationMarkerLithostratigraphics.Add(obj);
            }
        }
        private void Chronostratigraphic(FormationMarker formationMarker)
        {
            if (formationMarker.Chronostratigraphic != null)
            {
                var obj = _mapper.Map<FormationMarkerChronostratigraphic>(formationMarker.Chronostratigraphic);
                _db.FormationMarkerChronostratigraphics.Add(obj);
            }
        }
        private void CommonData(FormationMarker formationMarker)
        {
            if (formationMarker.CommonData != null)
            {
                var obj = _mapper.Map<FormationMarkerCommonData>(formationMarker.CommonData);
                _db.FormationMarkerCommonDatas.Add(obj);
            }
        }
      

        #endregion Insert FormationMarker

        #region Update FormationMarker
          private void UpdateMdPrognosed(FormationMarker formationMarker)
        {
            if(formationMarker.MdPrognosed.Uom !=null)
            {
                var obj = _mapper.Map<FormationMarkerMdPrognosed>(formationMarker.MdPrognosed);
                _db.FormationMarkerMdPrognoseds.Update(obj);
            }
        }
        private void UpdateTvdPrognosed(FormationMarker formationMarker)
        {
            if (formationMarker.TvdPrognosed.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerTvdPrognosed>(formationMarker.TvdPrognosed);
                _db.FormationMarkerTvdPrognoseds.Update(obj);
            }
        }
        private void UpdateMdTopSample(FormationMarker formationMarker)
        {
            if (formationMarker.MdTopSample.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerMdTopSample>(formationMarker.MdTopSample);
                _db.FormationMarkerMdTopSamples.Update(obj);
            }
        }
        private void UpdateTvdTopSample(FormationMarker formationMarker)
        {
            if (formationMarker.TvdTopSample.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerTvdTopSample>(formationMarker.TvdTopSample);
                _db.FormationMarkerTvdTopSamples.Update(obj);
            }
        }
        private void UpdateThicknessBed(FormationMarker formationMarker)
        {
            if (formationMarker.ThicknessBed.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerThicknessBed>(formationMarker.ThicknessBed);
                _db.FormationMarkerThicknessBeds.Update(obj);
            }
        }
        private void UpdateThicknessApparent(FormationMarker formationMarker)
        {
            if (formationMarker.ThicknessApparent.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerThicknessApparent>(formationMarker.ThicknessApparent);
                _db.FormationMarkerThicknessApparents.Update(obj);
            }
        }
        private void UpdateThicknessPerpen(FormationMarker formationMarker)
        {
            if (formationMarker.ThicknessPerpen.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerThicknessPerpen>(formationMarker.ThicknessPerpen);
                _db.FormationMarkerThicknessPerpens.Update(obj);
            }
        }
        private void UpdateMdLogSample(FormationMarker formationMarker)
        {
            if (formationMarker.MdLogSample.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerMdLogSample>(formationMarker.MdLogSample);
                _db.FormationMarkerMdLogSamples.Update(obj);
            }
        } private void UpdateTvdLogSample(FormationMarker formationMarker)
        {
            if(formationMarker.TvdLogSample.Uom !=null)
            {
                var obj = _mapper.Map<FormationMarkerTvdLogSample>(formationMarker.TvdLogSample);
                _db.FormationMarkerTvdLogSamples.Update(obj);
            }
        }
        private void UpdateDip(FormationMarker formationMarker)
        {
            if (formationMarker.Dip.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerDip>(formationMarker.Dip);
                _db.FormationMarkerDips.Update(obj);
            }
        }
        private void UpdateDipDirection(FormationMarker formationMarker)
        {
            if (formationMarker.DipDirection.Uom != null)
            {
                var obj = _mapper.Map<FormationMarkerDipDirection>(formationMarker.DipDirection);
                _db.FormationMarkerDipDirections.Update(obj);
            }
        }
        private void UpdateLithostratigraphic(FormationMarker formationMarker)
        {
            if (formationMarker.Lithostratigraphic != null)
            {
                var obj = _mapper.Map<FormationMarkerLithostratigraphic>(formationMarker.Lithostratigraphic);
                _db.FormationMarkerLithostratigraphics.Update(obj);
            }
        }
        private void UpdateChronostratigraphic(FormationMarker formationMarker)
        {
            if (formationMarker.Chronostratigraphic != null)
            {
                var obj = _mapper.Map<FormationMarkerChronostratigraphic>(formationMarker.Chronostratigraphic);
                _db.FormationMarkerChronostratigraphics.Update(obj);
            }
        }
        private void UpdateCommonData(FormationMarker formationMarker)
        {
            if (formationMarker.CommonData != null)
            {
                var obj = _mapper.Map<FormationMarkerCommonData>(formationMarker.CommonData);
                _db.FormationMarkerCommonDatas.Update(obj);
            }
        }

        #endregion Update FormationMarker
    }
}
