using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Well_AI_Advisior.API.Authorize.Net.Services;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;

namespace Well_AI_Advisior.API.Authorize.Net
{
        
        public static class Bootstrapper
        {

            public static IConfiguration configuration { get; }
            public static void UseServices(this IServiceCollection services)
            {
                services.AddScoped<IAuthenticationService, AuthenticationService>();
                services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();
                services.AddScoped<IRecurringBillingService, RecurringBillingService>();
          
        }      
    }
        
}
