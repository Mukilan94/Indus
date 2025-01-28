using System;
using System.Collections.Generic;
using System.Text;
using Well_AI.Advisor.API.PEC.Models;

namespace Well_AI.Advisor.API.PEC.Repository
{
   public interface IAuthenticationRepository
    {
       public  TenantConfiguration GetAuthonticationDetail(string tenantId);
        userDetailConfiguration GetAuthonticationUserDetail(string tenantId);
        
    }
}
