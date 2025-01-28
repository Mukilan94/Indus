using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Data;
using WellAI.Advisor.API.Models;
using WellAI.Advisor.API.Repository.IRepository;

namespace WellAI.Advisor.API.Repository
{
    public class DrillingConnectionRepository : IDrillingConnectionRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public DrillingConnectionRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            string tenantId = httpContextAccessor.HttpContext.User.Identity.Name;
            _tenantRepository = tenantRepository;
            var options = _tenantRepository.SetDbContext(tenantId);
            db = new WellAIAdvisiorContext(options);
            _db = db;
        }


        public bool CreateDrillingConnection(DrillingConnection drillingConnection)
        {
            _db.erdos_DrillingConnections.Add(drillingConnection);
            return Save();
        }

        public bool DrillingConnectionExists(string WellId)
        {
            bool value = _db.erdos_DrillingConnections.Any(x => x.WELLID.ToLower().Trim() == WellId.ToLower().Trim());
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
