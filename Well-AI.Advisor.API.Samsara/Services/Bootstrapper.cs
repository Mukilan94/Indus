using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Well_AI.Advisor.API.Samsara.Services.IServices;

namespace Well_AI.Advisor.API.Samsara.Services
{
   public static class Bootstrapper
    {
        public static void UseServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddHttpClient<IVechicleService, VechicleService>();
        }
    }
}
