using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WellAI.Advisor.DLL.Repository
{
    public class WellTenantRepository : IWellTenantRepository
    {
        private readonly WebAIAdvisorContext _db;
        public WellTenantRepository(WebAIAdvisorContext db)
        {
            _db = db;
        }

        public List<WellTenantInfo> GetAllTenants()
        {
            return _db.WellTenants.ToList();
        }

        public WellTenantInfo GetTenantById(string id)
        {
            return _db.WellTenants.FirstOrDefault(x => x.Id == id);
        }

        public bool CreateTenant(string tenantId, string connectionString, string name, string identifier, string id)
        {
            try
            {
                var ti = new WellTenantInfo
                {
                    TenId = Guid.NewGuid().ToString("D"),
                    Id = id,
                    Identifier = identifier,
                    Name = name,
                    ConnectionString = connectionString
                };
                _db.WellTenants.Add(ti);

                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, null, _db, null);
                customErrorHandler.WriteError(ex, "TwilioChat UpdateTwilioUserChannelLeaveStatus", null);
                return false;
            }
        }
    }
}
