using Microsoft.VisualStudio.TestTools.UnitTesting;
using Well_AI.Advisor.API.PEC.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Well_AI.Advisor.API.PEC.Services.IServices;
using System.Threading.Tasks;

namespace Well_AI.Advisor.API.PEC.Services.Tests
{
    [TestClass()]
    public class PECTests
    {
        [TestMethod()]
        public async Task AuthonticateAPi()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IAuthenticationService>();
            string tenantID = "12cd4433f-ed45-4404-8bd4-10abff288f07";
            var tasks = await service.AuthenticateAsync(tenantID);

            Assert.IsTrue(tasks.access_token != null);
         
        }

        [TestMethod()]
        public async Task GetUserInfoAsyc()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IPecService>();
            string tenantID = "12cd433f-ed45-4404-8bd4-10abff288f07";
            var tasks = await service.GetUserInfoAsyc(tenantID);

            Assert.IsNotNull(tasks.userId);
        }

        [TestMethod()]
        public async Task GetOrganizationDetailsAsyc()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IPecService>();
            string OrganizationId = "282acaa1-5b38-4831-99ec-a83e0030ed9f";
            string tenantID = "12cd433f-ed45-4404-8bd4-10abff288f07";
            var tasks = await service.GetOrganizationDetailsAsyc(OrganizationId, tenantID);

            Assert.IsNotNull(tasks.id);
        }

        [TestMethod()]
        public async Task GetOrganizationRankingAsyc()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IPecService>();
            string OrganizationId = "282acaa1-5b38-4831-99ec-a83e0030ed9f";
            string tenantID = "12cd433f-ed45-4404-8bd4-10abff288f07";
            var tasks = await service.GetOrganizationRankingAsyc(OrganizationId, tenantID);

            Assert.IsNotNull(tasks.organization.id);
        }
    }
}