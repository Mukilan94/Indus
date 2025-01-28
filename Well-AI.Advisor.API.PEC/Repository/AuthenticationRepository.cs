using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Well_AI.Advisor.API.PEC.Data;
using Well_AI.Advisor.API.PEC.Models;
using Well_AI.Advisor.API.PEC.Repository;
using Well_AI.Advisor.API.PEC.Services;
using Well_AI.Advisor.API.PEC.Services.IServices;
using Well_AI.Advisor.Log.Error;

namespace Well_AI.Advisor.API.PEC.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public AuthenticationRepository(WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
        {
            _wdb = wdb;
        }
        public TenantConfiguration GetAuthonticationDetail(string TenantId)
        {
            WellAIAdvisiorApiPecContext _db = new WellAIAdvisiorApiPecContext();
            try
            {
                var apiConfig = _db.vw_PecConfiguration.Where(x => x.TenantId == TenantId).FirstOrDefault();

                return apiConfig;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(null, null, _wdb, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "AuthenticationRepository GetAuthonticationDetail", null);
                return null;
            }

        }

        public userDetailConfiguration GetAuthonticationUserDetail(string tenantId)
        {
            WellAIAdvisiorApiPecContext _db = new WellAIAdvisiorApiPecContext();
            
            try
            {
                var query = (from AspnetUSers in _db.vw_PecuserDetailConfiguration
                             join crmCompanies in _db.vw_PecdbCrmCompanies on AspnetUSers.Id equals crmCompanies.userId
                             where crmCompanies.TenantId.Equals(tenantId)
                             select new { AspnetUSers }).FirstOrDefault();
                
                return query.AspnetUSers;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(null, null, _wdb, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "AuthenticationRepository GetAuthonticationUserDetail", null);
                return null;
            }

        }
    }

}
