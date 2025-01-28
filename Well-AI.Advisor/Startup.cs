using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using Well_AI.Advisor.API.Samsara.Services;
using Well_AI.Advisor.API.Samsara.Services.IServices;
using Well_AI.Advisor.Communication;
using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.BLL;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Hubs;
using WellAI.Advisor.Model.TwilioCredentials;
using WellAI.Advisor.Tenant;
using WellAI.Advisor.DLL.Data;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WellAI.Advisor
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

            services.AddMvc(mvcOtions => mvcOtions.EnableEndpointRouting = false)
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddKendo();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommunication, Communication>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IVechicleService, VechicleService>();
            services.AddTransient<ISingleton, Singleton>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContext<TenantOperatingDbContext>();
            services.AddDbContext<TenantServiceDbContext>();
            services.AddTransient<HttpClient>();
            services.AddOptions();
            services.Configure<TwilioAccountDetails>(Configuration.GetSection("TwilioAccountDetails"));
            services.Configure<TwilioCredentials>(Configuration.GetSection("TwilioCredentials"));
            services.AddSignalR().AddAzureSignalR();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            
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
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseSession();

            WellAIAppContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseMultiTenant();
            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notificationHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                    name: "default",
                    areaName: "identity",
                    template: "{controller=account}/{action=Login}");
                routes.MapAreaRoute(
                   name: "LogOut",
                   areaName: "identity",
                   template: "{controller=account}/{action=LogOut}");
                //Changed Dashboard to OperatingDashboard
                routes.MapAreaRoute(
                    name: "OperatingDashboard",
                    areaName: "OperatingCompany",
                    template: "{controller=OperatingDashboard}/{action=Index}");
                routes.MapAreaRoute(
                    name: "InDepthRigData",
                    areaName: "OperatingCompany",
                    template: "{controller=InDepthRigData}/{action=Index}");
                routes.MapAreaRoute(
                    name: "LoginAdvisor",
                    areaName: "OperatingCompany",
                    template: "{controller=LoginAdvisor}/{action=Index}");
                routes.MapAreaRoute(
                    name: "ProviderDirectoryProfile",
                    areaName: "OperatingCompany",
                    template: "{controller=ProviderDirectory}/{action=Index}/{id}");
                routes.MapAreaRoute(
                    name: "ProviderDirectory",
                    areaName: "OperatingCompany",
                    template: "{controller=ProviderDirectory}/{action=Index}");
                routes.MapAreaRoute(
                    name: "ProjectAuctions",
                    areaName: "OperatingCompany",
                    template: "{controller=ProjectAuctions}/{action=Index}");
                routes.MapAreaRoute(
                    name: "ActivityView",
                    areaName: "OperatingCompany",
                    template: "{controller=ActivityView}/{action=Index}");
                routes.MapAreaRoute(
                    name: "FieldTickets",
                    areaName: "OperatingCompany",
                    template: "{controller=FieldTickets}/{action=Index}");
                routes.MapAreaRoute(
                    name: "WellManager",
                    areaName: "OperatingCompany",
                    template: "{controller=WellManager}/{action=Index}");
                routes.MapAreaRoute(
                    name: "OngoingProjects",
                    areaName: "OperatingCompany",
                    template: "{controller=OngoingProjects}/{action=Index}");
                routes.MapAreaRoute(
                    name: "UpcomingProjects",
                    areaName: "OperatingCompany",
                    template: "{controller=UpcomingProjects}/{action=Index}");
                routes.MapAreaRoute(
                    name: "UpcomingProjectDetails",
                    areaName: "OperatingCompany",
                    template: "{controller=UpcomingProjects}/{action=Details}");
                routes.MapAreaRoute(
                    name: "OpenBids",
                    areaName: "OperatingCompany",
                    template: "{controller=OpenBids}/{action=Index}");
                routes.MapAreaRoute(
                    name: "TechnicianTracker",
                    areaName: "OperatingCompany",
                    template: "{controller=TechnicianTracker}/{action=Index}");
                routes.MapAreaRoute(
                    name: "WellData",
                    areaName: "OperatingCompany",
                    template: "{controller=WellData}/{action=Index}");
                routes.MapAreaRoute(
                    name: "DocumentManager",
                    areaName: "OperatingCompany",
                    template: "{controller=DocumentManager}/{action=Index}");
                routes.MapAreaRoute(
                    name: "WellMetrics",
                    areaName: "OperatingCompany",
                    template: "{controller=WellMetrics}/{action=Index}");
                routes.MapAreaRoute(
                    name: "SupportTickets",
                    areaName: "OperatingCompany",
                    template: "{controller=SupportTickets}/{action=Index}");
                routes.MapAreaRoute(
                    name: "Support",
                    areaName: "OperatingCompany",
                    template: "{controller=Support}/{action=Index}");
                routes.MapAreaRoute(
                    name: "Communication",
                    areaName: "OperatingCompany",
                    template: "{controller=Communication}/{action=Index}");
                routes.MapAreaRoute(
                    name: "VoiceTextVideoChat",
                    areaName: "OperatingCompany",
                    template: "{controller=VoiceTextVideoChat}/{action=Index}");
                routes.MapAreaRoute(
                    name: "History",
                    areaName: "OperatingCompany",
                    template: "{controller=History}/{action=Index}");
                routes.MapAreaRoute(
                    name: "Profile",
                    areaName: "OperatingCompany",
                    template: "{controller=Profile}/{action=Index}");
                routes.MapAreaRoute(
                    name: "CorporateProfile",
                    areaName: "OperatingCompany",
                    template: "{controller=CorporateProfile}/{action=Index}");
                routes.MapAreaRoute(
                    name: "EditProfile",
                    areaName: "OperatingCompany",
                    template: "{controller=EditProfile}/{action=Index}");
                routes.MapAreaRoute(
                    name: "PaymentMethods",
                    areaName: "OperatingCompany",
                    template: "{controller=PaymentMethods}/{action=Index}");
                routes.MapAreaRoute(
                    name: "BillingInvoiceHistorySRVNew",
                    areaName: "OperatingCompany",
                    template: "{controller=BillingInvoiceHistorySRVNew}/{action=Index}");
                routes.MapAreaRoute(
                    name: "UserManager",
                    areaName: "OperatingCompany",
                    template: "{controller=UserManager}/{action=Index}");
                routes.MapAreaRoute(
                    name: "AddUpdateUsers",
                    areaName: "OperatingCompany",
                    template: "{controller=AddUpdateUsers}/{action=Index}");
                routes.MapAreaRoute(
                    name: "ManageRoles",
                    areaName: "OperatingCompany",
                    template: "{controller=ManageRoles}/{action=Index}");
                routes.MapAreaRoute(
                    name: "ManagePermissions",
                    areaName: "OperatingCompany",
                    template: "{controller=ManagePermissions}/{action=Index}");
                routes.MapAreaRoute(
                    name: "DataManager",
                    areaName: "OperatingCompany",
                    template: "{controller=DataManager}/{action=Index}");

                routes.MapAreaRoute(
                    name: "Token",
                    areaName: "OperatingCompany",
                    template: "{controller=Token}/{action=GenerateCallToken}");
                routes.MapAreaRoute(
                   name: "Call",
                   areaName: "OperatingCompany",
                   template: "{controller=Call}/{action=Connect}");

                routes.MapAreaRoute(
                   name: "productsubscription",
                   areaName: "OperatingCompany",
                   template: "{controller=xxx}/{action=Index}");
                routes.MapAreaRoute(
                   name: "Configuration",
                   areaName: "OperatingCompany",
                   template: "{controller=Configuration}/{action=Index}");


                routes.MapAreaRoute(
                 name: "Voice",
                 areaName: "OperatingCompany",
                 template: "{controller=Voice}/{action=Index}");

                routes.MapAreaRoute(
                name: "VoiceToken",
                areaName: "OperatingCompany",
                template: "{controller=VoiceToken}/{action=Index}");

                //ServiceCompany
                routes.MapAreaRoute(
                    name: "ServiceDashboard",
                    areaName: "ServiceCompany",
                    template: "{controller=ServiceDashboard}/{action=Index}");
                routes.MapAreaRoute(
                    name: "InDepthRigDataSrv",
                    areaName: "ServiceCompany",
                    template: "{controller=InDepthRigDataSrv}/{action=Index}");
                routes.MapAreaRoute(
                    name: "Sales",
                    areaName: "ServiceCompany",
                    template: "{controller=Sales}/{action=Index}");
                routes.MapAreaRoute(
                    name: "AddUpdateUsersSRV",
                    areaName: "ServiceCompany",
                    template: "{controller=AddUpdateUsersSRV}/{action=Index}");

                routes.MapAreaRoute(
                    name: "SupportTicketsSRV",
                    areaName: "ServiceCompany",
                    template: "{controller=SupportTickets}/{action=Index}");
                routes.MapAreaRoute(
                    name: "BillingInvoiceHistorySRV",
                    areaName: "ServiceCompany",
                    template: "{controller=BillingInvoiceHistorySRV}/{action=Index}");

                routes.MapAreaRoute(
                    name: "serviceDeliveries",
                    areaName: "OperatingCompany",
                    template: "{controller=ServiceDeliveries}/{action=Index}");

                routes.MapAreaRoute(
                    name: "checkList",
                    areaName: "OperatingCompany",
                    template: "{controller=CheckList}/{action=Index}");

                routes.MapAreaRoute(
                    name: "programpermits",
                    areaName: "OperatingCompany",
                    template: "{controller=ProgramPermits}/{action=Index}");

                routes.MapAreaRoute(
                    name: "msas",
                    areaName: "OperatingCompany",
                    template: "{controller=MSAs}/{action=Index}");

                routes.MapAreaRoute(
                    name: "pad",
                    areaName: "OperatingCompany",
                    template: "{controller=Pad}/{action=Index}");
                routes.MapAreaRoute(
                    name: "apiConfiguration",
                    areaName: "OperatingCompany",
                    template: "{controller=ApiConfiguration}/{action=Index}");
                routes.MapAreaRoute(
                    name: "apiConfigurationSrv",
                    areaName: "ServiceCompany",
                    template: "{controller=ApiConfigurationSrv}/{action=Index}");

                //Registration
                routes.MapAreaRoute(
                   name: "registration",
                   areaName: "Registration",
                   template: "{controller=Registration}/{action=Index}");

                routes.MapAreaRoute(
                  name: "registrationlayout",
                  areaName: "Registration",
                  template: "{controller=Registration}/{action=RegistrationLayout}");


                routes.MapAreaRoute(
                  name: "SubscriptionSelector",
                  areaName: "Registration",
                  template: "{controller=Registration}/{action=SubscriptionSelector}");

                routes.MapAreaRoute(
                name: "CompanyDetails",
                areaName: "Registration",
                template: "{controller=Registration}/{action=CompanyDetails}");

                routes.MapAreaRoute(
                name: "CostSummary",
                areaName: "Registration",
                template: "{controller=Registration}/{action=CostSummary}");

                routes.MapAreaRoute(
                name: "Payment",
                areaName: "Registration",
                template: "{controller=Registration}/{action=Payment}");

                routes.MapAreaRoute(
                  name: "UpdateSubscription",
                  areaName: "Registration",
                  template: "{controller=Registration}/{action=UpdateSubscription}");
            });

           
        }
    }
}
