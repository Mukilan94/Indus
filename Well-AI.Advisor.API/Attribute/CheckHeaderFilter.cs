using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Well_AI.Advisor.API.Attribute
{
   
    public class CheckHeaderFilter 
    {
       

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Microsoft.Extensions.Primitives.StringValues authTokens;
            context.HttpContext.Request.Headers.TryGetValue("authToken", out authTokens);
            var _token = authTokens.FirstOrDefault();
        }
    }
}
