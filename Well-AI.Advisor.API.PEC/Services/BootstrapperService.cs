using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Well_AI.Advisor.API.PEC.Repository;
using Well_AI.Advisor.API.PEC.Services.IServices;

namespace Well_AI.Advisor.API.PEC.Services
{
   public static class BootstrapperService
    {
        public static void UseServices(this IServiceCollection services)
        {
            services.AddHttpClient<IPecService, PecService>();
            services.AddHttpClient<IEmployeeQualification, EmployeeQualificationService>();
            services.AddHttpClient<ISafetyProgramEvaluationDocumentsService, SafetyProgramEvaluationDocumentsService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddHttpClient<IAuthenticationService, AuthonticateService>();
           

        }
    }
}
