using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

[assembly: HostingStartup(typeof(WellAI.Advisor.Areas.Identity.IdentityHostingStartup))]
namespace WellAI.Advisor.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WebAIAdvisorContext>(options =>
                    
                options.UseSqlServer(
                        context.Configuration.GetConnectionString("WebAIAdvisorContextConnection"), options => options.EnableRetryOnFailure()));

                services.AddIdentity<WellIdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddDefaultTokenProviders()
                    .AddDefaultUI()
                    .AddEntityFrameworkStores<WebAIAdvisorContext>()
                    .AddClaimsPrincipalFactory<WellUserClaimsPrincipalFactory>();

                services.AddMvc(mvcOtions => mvcOtions.EnableEndpointRouting = false)
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

                services.AddKendo();
            });
        }
    }
}