using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WellAI.Advisor.Areas.Identity;


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Well_AI.Advisor.API.PEC.Services.IServices;
using WellAI.Advisor;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Well_AI.Advisor.API.PEC.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Well_AI.Advisor.Log.Error;
using Microsoft.Extensions.Logging;
using System.Net;
using Telerik.Web.PDF;
using WellAI.Advisor.Areas.Identity;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.DLL.IRepository;

namespace Well_AI.Advisor.Administration.Controllers
{  //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    // [SessionTimeOut]
    public class HomeController : BaseController
    {

        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
        //Phase II - Clear Warning

        //Phase II Changes-01/12/2021 - Add OperatingDBContext to Read Vendor Directory
        private TenantOperatingDbContext _tdbContext;
        private readonly TenantOperatingDbContext _operdb;
        private ISession _session;
        private TenantServiceDbContext _servicedb;
        //private readonly IConfiguration _configuration;
        public HomeController(IConfiguration _configuration, UserManager<WellIdentityUser> userManager,
          RoleManager<IdentityRole> roleManager,
           ISingletonAdministration _singleton, WebAIAdvisorContext db, TenantOperatingDbContext operdb, TenantOperatingDbContext tdbContext,
           TenantServiceDbContext servicedb, UserManager<StaffWellIdentityUser> staffUserManager, IConfiguration configuratio)
            : base(_configuration, _singleton, db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _operdb = operdb;
            _tdbContext = tdbContext;
            _session = WellAIAppContext.Current.Session;
            _servicedb = servicedb;
            _staffUserManager = staffUserManager;
            //_configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
