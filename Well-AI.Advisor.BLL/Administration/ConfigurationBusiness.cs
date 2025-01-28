using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Administration
{
    public interface IConfigurationBusiness
    {
        Task<List<ConfigurationViewModel>> GetConfigurations();
        Task<ConfigurationViewModel> AddConfiguration(ConfigurationViewModel input);
        Task<ConfigurationViewModel> UpdateConfiguration(ConfigurationViewModel input);
    }
    public class ConfigurationBusiness : IConfigurationBusiness
    {
        private readonly WebAIAdvisorContext db;
        public ConfigurationBusiness(WebAIAdvisorContext db)
        {
            this.db = db;
        }
        public Task<ConfigurationViewModel> AddConfiguration(ConfigurationViewModel input)
        {
            try
            {
                ConfigurationSetting configuration = new ConfigurationSetting
                {
                    ConstantName = input.ConstantName,
                    FriendlyName = input.FriendlyName,
                    Value = input.Value
                };
                db.ConfigurationSettings.Add(configuration);
                db.SaveChanges();
                return Task.FromResult(input);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "Configuration AddConfiguration", null);
                return null;
            }
        }

        public async Task<List<ConfigurationViewModel>> GetConfigurations()
        {
            try
            {
                var result = await db.ConfigurationSettings.Select(model => new ConfigurationViewModel { ConstantName = model.ConstantName, FriendlyName = model.FriendlyName, Value = model.Value, Index = model.Index }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "Configuration GetConfigurations", null);
                return null;
            }
        }

        public Task<ConfigurationViewModel> UpdateConfiguration(ConfigurationViewModel input)
        {
            try
            {
                var result = db.ConfigurationSettings.Find(input.Index);
                result.ConstantName = input.ConstantName;
                result.FriendlyName = input.FriendlyName;
                result.Value = input.Value;
                db.SaveChanges();
                return Task.FromResult(input);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "Configuration UpdateConfiguration", null);
                return null;
            }
        }
    }
}
