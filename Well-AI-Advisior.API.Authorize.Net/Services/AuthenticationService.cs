using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using Well_AI_Advisior.API.Authorize.Net.Model;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;

namespace Well_AI_Advisior.API.Authorize.Net.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly string _environmentType;
        public AuthenticationService()
        {
            //_environmentType = ConfigurationManager.AppSettings["AuthorizeNetEnvironmentType"];
        }

        public ConfigurationModel AuthenticateApi()
        {
            WellAIAdvisiorApiAuthorizeContext _db = new WellAIAdvisiorApiAuthorizeContext();
            try
            {
                var apiConfig = _db.Configuration.Where(x => x.ConstantName == "Authorize.NetKey").FirstOrDefault();

                return apiConfig;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
