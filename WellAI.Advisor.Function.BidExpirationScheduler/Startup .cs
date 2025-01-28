using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WellAI.Advisor.Function.BidExpirationScheduler;
using WellAI.Advisor.Function.BidExpirationScheduler.Data;
using WellAI.Advisor.Function.BidExpirationScheduler.Repository;
using WellAI.Advisor.Function.BidExpirationScheduler.Repository.IRepository;

[assembly:FunctionsStartup(typeof(BidExpirationScheduler.Startup))]
namespace BidExpirationScheduler
{
    public class Startup : FunctionsStartup
    {
       
        public override void Configure(IFunctionsHostBuilder builder)
        {
           
           string connectionString = Environment.GetEnvironmentVariable("sqldb_connection");
            
            //Phase II Changes - 03/30/2021
            //builder.Services.AddDbContext<WellAIDataContext>(
            //   options => options.UseSqlServer(connectionString), option => option.EnableRetryOnFailure()));
            builder.Services.AddDbContext<WellAIDataContext>(
              options => options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));
            builder.Services.AddScoped<ICommonRepository, CommonRepository>();

        }
}
}

