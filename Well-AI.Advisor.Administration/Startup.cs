using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using WellAI.Advisor;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using Well_AI.Advisor.Communication;
using WellAI.Advisor.Model.TwilioCredentials;
using WellAI.Advisor.Tenant;
using Well_AI.Advisor.Administration.Hubs;

namespace Well_AI.Advisor.Administration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Phase II Changes - 01/12/2021
            services.AddMvc()
             .AddSessionStateTempDataProvider();
            services.AddSession();

            services.AddMultiTenant()
                .WithStore<TenantConfigurationStore>(ServiceLifetime.Singleton)
                .WithDelegateStrategy(async context =>
                {
                    var tenantId = "";
                    var tenantClaim = ((HttpContext)context).User.FindFirst("TenantId");
                    if (tenantClaim != null)
                        tenantId = tenantClaim.Value;
                    return await Task.FromResult(tenantId);
                });

            
             services.AddDistributedMemoryCache();

            // Add framework services.
            services
                .AddControllersWithViews()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddMvc(mvcOtions => mvcOtions.EnableEndpointRouting = false)
                            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddIdentity<WellIdentityUser, IdentityRole>(options =>{
            }).AddEntityFrameworkStores<WebAIAdvisorContext>().AddDefaultTokenProviders();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //Phase II Changes - 03/30/2021
            services.AddDbContext<WebAIAdvisorContext>(option => option.UseSqlServer(Configuration.GetConnectionString("WebAIAdvisorContextConnection"), option => option.EnableRetryOnFailure()));
            services.AddDbContext<WellAIStaffContext>(option => option.UseSqlServer(Configuration.GetConnectionString("WebAIAdvisorContextConnection"), option => option.EnableRetryOnFailure()));

            IdentityBuilder builder = services.AddIdentityCore<StaffWellIdentityUser>(opt =>
             {
                 opt.Password.RequireDigit = false;
                 opt.Password.RequiredLength = 4;
                 opt.Password.RequireNonAlphanumeric = false;
                 opt.Password.RequireLowercase = false;

             });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<WellAIStaffContext>();
            builder.AddRoleValidator<RoleValidator<IdentityRole>>();
            builder.AddRoleManager<RoleManager<IdentityRole>>();
            builder.AddSignInManager<SignInManager<StaffWellIdentityUser>>();
            builder.AddDefaultTokenProviders();


            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<ISingletonAdministration, SingletonAdministration>();
            services.AddTransient<ICommunication, Well_AI.Advisor.Communication.Communication>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddControllers().AddNewtonsoftJson();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages().AddNewtonsoftJson();

            services.AddDbContext<TenantOperatingDbContext>();
            services.AddDbContext<TenantServiceDbContext>();
            // Add Kendo UI services to the services container

            services.AddKendo();
            services.Configure<TwilioCredentials>(Configuration.GetSection("TwilioCredentials"));
            services.AddSignalR().AddAzureSignalR();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.Configure<TwilioAccountDetails>(Configuration.GetSection("TwilioAccountDetails"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            //Phase II Changes - 01/12/2021
            app.UseSession();
            app.UseAuthorization();
            WellAIAppContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notificationHub");
            });

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=account}/{action=login}/{id?}");
                endpoints.MapControllerRoute(
                    name: "home",
                    pattern: "{controller=home}/{action=index}/{id?}");

            });

        }
    }
}
