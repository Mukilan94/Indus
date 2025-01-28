using Microsoft.VisualStudio.TestTools.UnitTesting;
using Well_AI.Advisor.API.Samsara.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Samsara.Services.IServices;

namespace SamaraTests
{
    [TestClass()]
    public class VechicleModelTests
    {
        [TestMethod()]
        public async Task Get_All_Vechicles()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IVechicleService>();

            var tasks = await service.GetVechicleAsync();

            Assert.IsTrue(tasks.data.Count == 31);
            //Console.WriteLine("The VechicleList Count is equal to 31");
            //Console.ReadLine();
        }

        [TestMethod()]
        public async Task RetriveVechicles()
        {

            string id = "212014918388379";
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IVechicleService>();

            var tasks = await service.GetVechicleAsync(id);

            Assert.IsTrue(tasks.data.Count == 31);
            //Console.WriteLine("The VechicleList Count is equal to 31");
            //Console.ReadLine();
        }

        [TestMethod()]
        public async Task GetVechicleAsyncWithParam()
        {
            var services = new ServiceCollection();
            services.UseServices();
            int limit = 20;
            string direction = "After";
            string[] parentTagIds = { "212014918410262", "212014918428378" };
            string[] tagIds = { "1366236", "1366237" };
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IVechicleService>();

            var tasks = await service.GetVechicleAsync(limit, direction, parentTagIds, tagIds);


            Assert.IsNotNull(tasks);
        }

        [TestMethod()]
        public async Task GetMostRecentVechicle()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IVechicleService>();

            var tasks = await service.GetMostRecentVechicleLocationAsync();


            Assert.IsTrue(tasks.data.Count == 31);
        }

        [TestMethod()]
        public async Task GetMostRecentVechicleLocationAsync()
        {
            var services = new ServiceCollection();
            services.UseServices();
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IVechicleService>();
            var tasks = await service.GetMostRecentVechicleLocationAsync();
            Assert.IsNotNull(tasks);
        }

        [TestMethod()]
        public async Task GetMostRecentVechicleLocationAsyncWithParam()
        {
            var services = new ServiceCollection();
            services.UseServices();
            int limit = 20;
            string direction = "After";
            string[] parentTagIds = { "212014918410262", "212014918428378" };
            string[] tagIds = { "1366236", "1366237" };
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IVechicleService>();
            var tasks = await service.GetMostRecentVechicleLocationAsync(limit, direction, parentTagIds, tagIds);
            Assert.IsNotNull(tasks);
        }

        [TestMethod()]
        public async Task GetFllowFeedOfVechicleLocationWithParamAsync()
        {
            var services = new ServiceCollection();
            services.UseServices();

            string direction = "After";
            string[] parentTagIds = { "212014918410262", "212014918428378" };
            string[] tagIds = { "1366236", "1366237" };
            string[] vecghicleIds = { "1366236", "1366237" };
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IVechicleService>();
            var tasks = await service.GetFllowFeedOfVechicleLocationAsync(tagIds, direction, parentTagIds, vecghicleIds);
            Assert.IsNotNull(tasks);
        }

        [TestMethod()]
        public async Task GetHistoricalVechicleLocationAsync()
        {
            var services = new ServiceCollection();
            services.UseServices();
            string startTime = "2020-05-19T21:18:02Z";
            string endTime = "2020-05-19T23:18:02Z";
            string direction = "After";
            string[] parentTagIds = { "212014918410262", "212014918428378" };
            string[] tagIds = { "1366236", "1366237" };
            string[] vecghicleIds = { "1366236", "1366237" };
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IVechicleService>();
            var tasks = await service.GetHistoricalVechicleLocationAsync(startTime,endTime, tagIds, direction, parentTagIds, vecghicleIds);
            Assert.IsNotNull(tasks);
        }

        #region Future Implementation
        [TestMethod()]
        public async Task GetRecentVechicleStatus()
        {
            var services = new ServiceCollection();
            services.UseServices();
            string[] types = { "auxInput1", "batteryMilliVolts" };
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IVechicleService>();

            var tasks = await service.GetMostRecentVechicleStatusAsync(types);


            Assert.IsNotNull(tasks);
        }

        [TestMethod()]
        public async Task GetFollowFeedVechicleStatus()
        {
            var services = new ServiceCollection();
            services.UseServices();
            string types = "batteryMilliVolts";
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IVechicleService>();

            var tasks = await service.GetFllowFeedOfVechicleStatusAsync(types);


            Assert.IsNotNull(tasks);
        }

        [TestMethod()]
        public async Task GetFollowFeedVechicleStatus1()
        {
            var services = new ServiceCollection();
            services.UseServices();
            //string types = "batteryMilliVolts";
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IVechicleService>();

            var tasks = await service.GetFllowFeedOfVechicleLocationAsync();


            Assert.IsNotNull(tasks);
        }

        [TestMethod()]
        public async Task GetMostRecentVechicleStatusForBatteryMilliVoltAsync()
        {
            var services = new ServiceCollection();
            services.UseServices();
            string types = "batteryMilliVolts";
            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IVechicleService>();

            var tasks = await service.GetMostRecentVechicleStatusForBatteryMilliVoltAsync(types);


            Assert.IsNotNull(tasks);
        }

        #endregion
    }
}