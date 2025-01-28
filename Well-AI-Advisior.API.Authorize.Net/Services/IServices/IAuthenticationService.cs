using System;
using System.Collections.Generic;
using System.Text;
using Well_AI_Advisior.API.Authorize.Net.Model;

namespace Well_AI_Advisior.API.Authorize.Net.Services.IServices
{
   public interface IAuthenticationService
    {
        ConfigurationModel AuthenticateApi();
    }
}
