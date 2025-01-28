using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Controllers;
using Well_AI.Advisor.API.Data;
using WellAI.Advisor.API.Models;
using WellAI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.DLL.Data;

namespace WellAI.Advisor.API.Repository
{
    public class DrillingDepthBasedRepository : IDrillingDepthBasedRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly WellAIAdvisiorContext _baseDb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        string tenantId = string.Empty;
        public DrillingDepthBasedRepository(WellAIAdvisiorContext db, WellAIAdvisiorContext baseDb, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            tenantId = httpContextAccessor.HttpContext.User.Identity.Name;
            _tenantRepository = tenantRepository;
            _baseDb = baseDb;
            var options = _tenantRepository.SetDbContext(tenantId);
            db = new WellAIAdvisiorContext(options);
            _db = db;
            _wdb = wdb;
        }


        public bool CreateDrillingDepthBased(DRILLINGDEPTHBASED drillingDepthBased)
        {
            _db.erdos_DrillingDepthBased.Add(drillingDepthBased);
            return Save();
        }

        public Guid DrillingDepthBasedExists(string WellId)
        {
            Guid WellDepthID = _baseDb.WellDepthDataStage.Where(x => x.WELLID.ToLower().Trim() == WellId.ToLower().Trim()).Select(x => x.WellDepthID).FirstOrDefault(); ;
            return WellDepthID;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool SaveWellDepthData(DRILLINGDEPTHBASED drillingDepthBased)
        {

            try
            {
                string wellId = drillingDepthBased.WELLID;
                WellDepthDataViewModel viewmodel = new WellDepthDataViewModel();
                Guid wellDepthId = DrillingDepthBasedExists(wellId);
                if (wellDepthId.Equals(Guid.Empty))
                {
                    viewmodel.WellDepthID = new System.Guid();
                    viewmodel.WELLID = drillingDepthBased.WELLID;
                    viewmodel.RECID = drillingDepthBased.RECID;
                    viewmodel.DATE = drillingDepthBased.DATE;
                    viewmodel.TIME = drillingDepthBased.TIME;
                    viewmodel.TENANTID = tenantId;
                    viewmodel.DEPTMEAS = drillingDepthBased.DEPTMEAS;
                    viewmodel.DEPTVERT = drillingDepthBased.DEPTVERT;
                    viewmodel.IsProcessed = false;
                    _baseDb.WellDepthDataStage.Add(viewmodel);
                    _baseDb.SaveChanges();
                }
                else
                {

                    viewmodel.WellDepthID = wellDepthId;
                    viewmodel.WELLID = drillingDepthBased.WELLID;
                    viewmodel.RECID = drillingDepthBased.RECID;
                    viewmodel.DATE = drillingDepthBased.DATE;
                    viewmodel.TIME = drillingDepthBased.TIME;
                    viewmodel.TENANTID = tenantId;
                    viewmodel.DEPTMEAS = drillingDepthBased.DEPTMEAS;
                    viewmodel.DEPTVERT = drillingDepthBased.DEPTVERT;
                    _baseDb.WellDepthDataStage.Update(viewmodel);
                    _baseDb.SaveChanges();

                }

                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "DrillingDepthBase SaveWellDepthData", null);
                return false;
            }
        }
 
    }
}
