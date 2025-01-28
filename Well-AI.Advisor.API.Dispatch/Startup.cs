using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using Well_AI.Advisor.API;

using Well_AI.Advisor.API.Dispatch.Repository;
using Well_AI.Advisor.API.Dispatch.Repository.IRepository;
using WellAI.Advisor.API.Dispatch.Repository;
using WellAI.Advisor.API.Dispatch.Repository.IRepository;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using Well_AI.Advisor.API.Dispatch.Data;
using WellAI.Advisor.API.Dispatch.Helpers;
using WellAI.Advisor.API.Dispatch;
using WellAI.Advisor.API.Dispatch.Mapper;

namespace WellAI.Advisor.API
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
            services.AddCors();
            services.AddControllers();
            string securityKey = Configuration["AppSettings:SecretKey"]; ;
            var symentricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var appSettingsSections = Configuration.GetSection("AppSettings");

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddDbContext<ApplicationDbContext>
            //    (options => options.UseSqlServer(Configuration.GetConnectionString("WebAIAdvisorContextConnection")));
            services.AddDbContext<WellAIAdvisiorContext>
             (options => options.UseSqlServer(Configuration.GetConnectionString("WebAIAdvisorContextConnection"), options => options.EnableRetryOnFailure()));
            services.AddDbContext<WebAIAdvisorContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("WebAIAdvisorContextConnection"), options => options.EnableRetryOnFailure()));

            services.AddIdentity<WellIdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<WebAIAdvisorContext>()
                    .AddClaimsPrincipalFactory<WellUserClaimsPrincipalFactory>();

           
            services.AddScoped<IWellSubscriptionRepository, WellSubscriptionRepository>();           
            //For Dispatch Login
            services.AddScoped<ILoginRepository, LoginRepository>();

            services.AddScoped<IDispatchRepository, DispatchRepository>();           
            //services.AddAutoMapper(typeof(DispatchRoutesMappings));
            services.AddAutoMapper(typeof(LoginMappings));
            services.AddAutoMapper(typeof(RouterAcceptedMappings));            
            services.Configure<AppSettings>(appSettingsSections);

            var appSettings = appSettingsSections.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.SaveToken = true;
                  x.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(key),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });

            //services.AddMultiTenant()
            //     .WithStore<TenantConfigurationStore>(ServiceLifetime.Singleton)
            //     .WithDelegateStrategy(async context =>
            //     {
            //         var sessiondata = ((HttpContext)context).Session;
            //         byte[] val;
            //         var tenantId = "";
            //         if (sessiondata.TryGetValue(Constants.TenantIdKey, out val))
            //             tenantId = Encoding.UTF8.GetString(val);

            //         return await Task.FromResult(tenantId);
            //     });



            // Register the Swagger generator, defining 1 or more Swagger documents
            ConfigureSwaggr(services);
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();

           
        }

        private static void AddAuthentication(IServiceCollection services, SymmetricSecurityKey symentricSecurityKey)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x => {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = symentricSecurityKey,
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
        }

        private static void ConfigureSwaggr(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.

            app.UseSwaggerUI(c =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                    c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                        desc.GroupName.ToUpperInvariant());
                c.RoutePrefix = "";
                //c.SwaggerEndpoint("/swagger/WellAIAPI/swagger.json", "Well AI Adviser API V1");
                //c.RoutePrefix = "";
            });


            app.UseHttpsRedirection();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            

            app.UseRouting();
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();
            
            app.UseAuthorization();
            app.UseSession();
            WellAIApiApContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
