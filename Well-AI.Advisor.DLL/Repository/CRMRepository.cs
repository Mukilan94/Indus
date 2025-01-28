using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.IRepository;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Tenant.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.OperatingCompany.Models;
using PaymentMethod = WellAI.Advisor.DLL.Entity.PaymentMethod;
using SubscriptionViewModel = WellAI.Advisor.Model.ServiceCompany.Models.SubscriptionViewModel;
using ActivityViewModel = WellAI.Advisor.Model.ServiceCompany.Models.ActivityViewModel;
using TaskSchedule = WellAI.Advisor.Model.ServiceCompany.Models.TaskSchedule;

namespace WellAI.Advisor.DLL.Repository
{
    public class CRMRepository : ICRMRepository
    {
        private readonly TenantServiceDbContext _servicedb;
        private readonly HttpContext _httpcontext;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext _db;

        public CRMRepository(TenantServiceDbContext servicedb, HttpContext httpcontext, UserManager<WellIdentityUser> userManager, WebAIAdvisorContext db)
        {
            _servicedb = servicedb;
            _httpcontext = httpcontext;
            _userManager = userManager;
            _db = db;
        }

        protected SqlConnection GetSqlConnection(string connectionstring)
        {
            return new SqlConnection(connectionstring);
        }
       
    }
}
