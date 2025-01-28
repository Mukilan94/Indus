using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Areas.Identity;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.Extensions.Configuration;
using System.Linq;
using WellAI.Advisor.BLL;
using WellAI.Advisor.Model.OperatingCompany.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Security.Claims;
using WellAI.Advisor.Model.Administration;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class ChecklistTemplateController : BaseController
    {
        private readonly ILogger<ActivityViewController> _logger;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantServiceDbContext _servicedb;
        private readonly IConfiguration _configuration;
        private readonly ISingleton _singleton;
        private readonly TenantOperatingDbContext _operdb;
        public ChecklistTemplateController(TenantOperatingDbContext operdb, WebAIAdvisorContext dbContext, TenantServiceDbContext servicedb, SignInManager<WellIdentityUser> signInManager,
                                      RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<ActivityViewController> logger,
                                      IConfiguration configuration, ISingleton singleton
                                      )
            : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _servicedb = servicedb;
            _configuration = configuration;
            _singleton = singleton;
            _operdb = operdb;
        }

        public IActionResult Index()
        {
            try
            {


                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }


                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> ReadChecklistTemplateForDesign([DataSourceRequest] DataSourceRequest request, string welltype)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                List<ChecklistTaskTemplateModel> checklistTemplate = new List<ChecklistTaskTemplateModel>();
                checklistTemplate = await commonBusiness.ReadChecklistTemplate(welltype);

                if(checklistTemplate != null)
                {
                    foreach(var items in checklistTemplate)
                    {
                        if (items.ServiceDuration != null)
                        {
                            items.ServiceDurationDays = items.ServiceDuration != null ? Convert.ToString(items.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(items.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0])) : "00";
                            items.ServiceDurationHours = items.ServiceDuration != null ? Convert.ToString(items.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(items.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])) : "00";
                            items.ServiceDurationMinutes = items.ServiceDuration != null ? Convert.ToString(items.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(items.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2])) : "00";
                        }
                        else
                        {
                            items.ServiceDuration = "00:00:00";
                        }
                    }
                }
      
                return Json(checklistTemplate.ToDataSourceResult(request));

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Insurance_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;

            }
        }

        public IActionResult AddTemplate()
        {
            //ViewBag.WellTypeId = WellType;

            return PartialView("Template");
        }

        public async Task<IActionResult> ReadChecklistTemplatesList([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var checklistTemplate = await commonBusiness.ReadChecklistTemplateList(TenantId);

                return Json(checklistTemplate.ToDataSourceResult(request));

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ReadChecklistTemplatesList", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

        public IActionResult Details(string CheckListId)
        {
            try
            {
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                Model.OperatingCompany.Models.ChecklistTemplateModel Checklisttemplate = new Model.OperatingCompany.Models.ChecklistTemplateModel();

                Checklisttemplate = db.ChecklistTemplate.Where(x => x.CheckListTemplateId == CheckListId && x.TenantId == TenantId)
                                   .Select(y => new Model.OperatingCompany.Models.ChecklistTemplateModel
                                   {
                                       TemplateId = y.CheckListTemplateId,
                                       TemplateName = y.TemplateName,
                                       TenantId = y.TenantId,
                                       WellTypeId = y.WellTypeId,
                                       Checklist = y.Checklist,
                                       IsDefault = y.IsDefault,
                                       CreatedBy = y.CreatedBy,
                                       ModifiedBy = y.ModifiedBy,
                                       ModifiedDate = y.ModifiedDate,
                                       IsActive = y.IsActive,
                                       BopFrequency = y.BopFrequency,
                                       BOPFrequencyPermissionValue = "none"
                                   }).FirstOrDefault();

                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                if (Checklisttemplate != null)
                {
                    if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                    {
                        Checklisttemplate.BOPFrequencyPermissionValue = "block";
                    }
                    else
                    {

                        Checklisttemplate.BOPFrequencyPermissionValue = commonBusiness.GetComponentPermission("BOPFrequency", _userManager.GetUserId(User), TenantId) == true ? "block" : "none";
                    }
                }
                else
                {
                    Model.OperatingCompany.Models.ChecklistTemplateModel Checklisttemplate1 = new Model.OperatingCompany.Models.ChecklistTemplateModel();
                    Checklisttemplate1.BOPFrequencyPermissionValue = commonBusiness.GetComponentPermission("BOPFrequency", _userManager.GetUserId(User), TenantId) == true ? "block" : "none";

                    if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                    {
                        Checklisttemplate1.BOPFrequencyPermissionValue = "block";
                    }

                    return View(Checklisttemplate1);
                }

                ViewBag.CheckListId = CheckListId;

                return View(Checklisttemplate);

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ChecklistTemplateDetails", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }

        }

        public async Task<ActionResult> Read_ChecklistTemplate([DataSourceRequest] DataSourceRequest request, string CheckListId)
        {
            try
            {
                List<ChecklistTaskTemplateModel> ChecklistTemplate = new List<ChecklistTaskTemplateModel>();
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                ChecklistTemplate = await commonBusiness.GetChecklistTemplate(CheckListId, TenantId);

                return Json(ChecklistTemplate.ToDataSourceResult(request));

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ChecklistTemplateDetails", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetWellType()
        {
            try
            {
                var wellTypeData = db.WellType.Select(x => new Model.OperatingCompany.Models.WellTypeModel
                {
                    wellTypeId = x.welltype_id,
                    wellTypeName = x.welltype_name
                }).OrderBy(x => x.wellTypeName).ToList();

                return Json(wellTypeData);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IActionResult AddTasks(string templateId)
        {
            ViewBag.CheckListTemplateId = templateId;

            return PartialView("_AddTasks");
        }

        public async Task<ActionResult> Task_Read([DataSourceRequest] DataSourceRequest request, string checklistTemplateId)
        {
            try
            {
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var serviceCategory = await commonBusiness.GetTasks();

                if (checklistTemplateId != null)
                {

                    var TasksExits = await commonBusiness.GetChecklistTemplate(checklistTemplateId, TenantId);
                    var TaskId = TasksExits.Select(x => x.TaskId).ToList();

                    var TasksList = from c in serviceCategory
                                    where !TaskId.Contains(c.TaskId)
                                    select c;

                    TasksList.OrderBy(x => x.Name);
                    return Json(TasksList.ToDataSourceResult(request));
                }
                //serviceCategory.OrderBy(x => x.Name);
                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ChecklistTemplateDetails", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

        public async Task<IActionResult> ChangeChecklistDefault(string templateId,string wellTypeId,bool IsDefault)
        {
            try
            {
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");

                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);

                var result = await commonBusiness.ChangeChecklistDefaultForTenant(templateId, tenantId,wellTypeId, IsDefault);

                return Ok(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ChecklistTemplate ChangeChecklistDefault", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveAndUpdateChecklistTemplate([FromBody] WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplate ChecklistTemplate)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = await commonBusiness.SaveChecklistTemplate(ChecklistTemplate, TenantId, userId);
                return Json(new { TemplateId = result });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SaveChecklistTemplate", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceHours()
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = await commonBusiness.GetServiceHours();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetServiceHours", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetServiceMinutes()
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = await commonBusiness.GetServiceMinutes();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetServiceMinutes", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

        [HttpGet]
        public IActionResult GetServiceStage()
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = commonBusiness.GetServiceStage();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetServiceStage", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }

        }
        [HttpGet]
        public IActionResult GetCategoriesList()
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var result = commonBusiness.GetCategoriesList();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetCategoriesList", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
        [HttpGet]
        public IActionResult GetTaskDependencyList(string taskId)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //if (taskId != null)
                //{
                    var result = commonBusiness.GetTaskDependencyList(taskId);
                    return Json(result);
                //}
                //else
                //{
                //    return null;
                //}
                
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "GetTaskDependencyList", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }

        }

        [AcceptVerbs("Post")]
        public ActionResult Task_Create([DataSourceRequest] DataSourceRequest request, ChecklistModel input)
        {
            string category = ""; //db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
            string stagetype = "";// db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Id).FirstOrDefault(),
            string hours = "";
            string mins = "";
            if (input != null)
            {
                if (input.ExportToMaster)
                {
                    var TasksExits = db.Tasks.Where(x => x.Name == input.Name && x.IsSpecialServices == Convert.ToInt32(input.IsSpecialServices) && x.IsActive == true).FirstOrDefault();
                    if (TasksExits != null)
                    {
                        ModelState.AddModelError("Tasks", "This task is already exits");
                        return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                    }
                }

                category = db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                stagetype = db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault();
                hours = input.ServiceDurationHours ?? "00";
                mins = input.ServiceDurationMinutes ?? "00";
            }

            ChecklistTaskTemplateModel ChecklistTemplate = new ChecklistTaskTemplateModel
            {
                TaskId = Guid.NewGuid().ToString(),
                IsSpecialServices = input.IsSpecialServices,
                CategoryName = category,
                ServiceCategoryId = input.ServiceCategoryId,
                Name = input.Name,
                ServiceDuration = input.ServiceDurationDays + ":" + hours + ":" + mins,
                Depth = input.Depth,
                IsActiveCategory = input.IsActiveCategory,
                Description = input.Description,
                SeletedDependency = input.SeletedDependency == null ? "" : input.SeletedDependency.Replace(";", ","),
                StageType = input.StageType,
                StageTypeName = stagetype,
                LeadTime = input.LeadTime,
                Day = input.Day,
                ScheduleTime = input.ScheduleTime,
                ServiceDurationDays = input.ServiceDurationDays ?? "00",
                ServiceDurationHours = hours,
                ServiceDurationMinutes = mins,
                IsActive = input.IsActive,
                IsBiddable = input.IsBiddable,
                ExportToMaster = input.ExportToMaster,
                IsBenchMark = input.IsBenchMark,
                IsPreSpud = input.IsPreSpud
            };

            return Json(new[] { ChecklistTemplate }.ToDataSourceResult(request, ModelState));

        }


        [HttpPost]

        public IActionResult Task_Update([DataSourceRequest] DataSourceRequest request, ChecklistModel input)
        {
            try
            {
                string category = ""; //db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                string stagetype = "";// db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Id).FirstOrDefault(),
                string hours = "";
                string mins = "";
                string stagetypeId = "";
                if (input != null)
                {
                    category = db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                    stagetype = db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault();
                    hours = input.ServiceDurationHours ?? "00";
                    mins = input.ServiceDurationMinutes ?? "00";

                    if (input.StageType == null || input.StageType == "")
                    {
                        stagetypeId = db.Stages.Where(x => x.Name  == input.StageTypeName).Select(y => y.Id).FirstOrDefault();
                    }
                    else
                    {
                        stagetypeId = input.StageType;
                    }
                }

                ChecklistTaskTemplateModel ChecklistTemplate = new ChecklistTaskTemplateModel
                {
                    TaskId = input.TaskId,
                    IsSpecialServices = input.IsSpecialServices,
                    CategoryName = category,
                    ServiceCategoryId = input.ServiceCategoryId,
                    Name = input.Name,
                    ServiceDuration = input.ServiceDurationDays + ":" + hours + ":" + mins,
                    Depth = input.Depth,
                    IsActiveCategory = input.IsActiveCategory,
                    Description = input.Description,
                    SeletedDependency = input.SeletedDependency == null ? "" : input.SeletedDependency.Replace(";", ","),
                    StageType = stagetypeId,
                    StageTypeName = stagetype,
                    LeadTime = input.LeadTime,
                    Day = input.Day,
                    ScheduleTime = input.ScheduleTime,
                    ServiceDurationDays = input.ServiceDurationDays ?? "00",
                    ServiceDurationHours = hours,
                    ServiceDurationMinutes = mins,
                    IsActive = input.IsActive,
                    IsBiddable = input.IsBiddable,
                    ExportToMaster = input.ExportToMaster,
                    IsBenchMark = input.IsBenchMark,
                    IsPreSpud = input.IsPreSpud
                };

                return Json(new[] { ChecklistTemplate }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist Template Task_Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }




        private bool GetComponentsBasedOnRole()
        {
            var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            List<string> rolesName = (from rl in roles
                                      select rl.Value
                                 ).ToList();

            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
            var rolesResult = roleResult.Result;
            if (roleResult != null)
            {
                List<string> roleIds = (from rl in rolesResult
                                        select rl.Id
                                        ).ToList();
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "ChecklistTemplate", TenantId);
            }
            else
            {
                return false;
            }
        }

        [HttpGet]

        public JsonResult tasksExits(string taskId)
        {
            try
            {
                bool status = false;
                if (!string.IsNullOrEmpty(taskId))
                {
                    var Tasks = db.Tasks.Where(x => x.TaskId == taskId).FirstOrDefault();
                    if (Tasks != null)
                    {
                        status = true;
                    }
                }

                return Json(new { status });
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist Task exists ", User.Identity.Name);
                return null;
            }
        }

        [HttpPost]

        public async Task<IActionResult> ChecklistTemplateDelete(string templateId)
        {
            int result = 0;
            try
            {
                int IsTemplateusingByWell = db.WellRegister.Where(x => x.ChecklistTemplateId == templateId).Count();

                if (IsTemplateusingByWell == 0)
                {

                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    if (!string.IsNullOrEmpty(templateId))
                    {
                        result = await commonBusiness.DeleteChecklistTemplate(templateId);

                        return Json(new { result });
                    }
                }

                return Json(new {status = "failed", templateName = db.ChecklistTemplate.Where(x => x.CheckListTemplateId == templateId).Select(t => t.TemplateName).FirstOrDefault()});
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist Task exists ", User.Identity.Name);
                return Json(new { result });
            }
        }


        public IActionResult WellTypeChecklistExists(string WellDesignId,string TemplateId)
        {
            try
            {
                 var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                 var Checklist = db.ChecklistTemplate.Where(x => x.CheckListTemplateId == TemplateId && x.TenantId == TenantId && x.WellTypeId == WellDesignId).FirstOrDefault();                
                if(Checklist != null)
                {
                    if (Checklist.Checklist != null || Checklist.Checklist != null)
                    {
                        return Json(new { status = "Exists" });
                    }
                    else
                    {
                        return Json(new { status = "NotExists" });
                    }
                }
                else
                {
                    var ChecklistTemplate = db.WellType.Where(x => x.welltype_id == WellDesignId).FirstOrDefault();

                    if (ChecklistTemplate.DrillPlanChecklist != null)
                    {
                        return Json(new { status = "Exists" });
                    }
                    else
                    {
                        return Json(new { status = "NotExists" });
                    }
                }                
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellTypeChecklistExists ", User.Identity.Name);
                return null;
            }
        }

        public IActionResult ReadServiceTasks([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                var Tasks = commonBusiness.GetTasksList();
                List<Model.Administration.ChecklistTemplateModel> templateList = new List<Model.Administration.ChecklistTemplateModel>();

                if (Tasks != null)
                {
                    templateList = (from task in Tasks.Result
                                    select new Model.Administration.ChecklistTemplateModel
                                    {
                                        TaskOrder = 1,
                                        Name = task.Name,
                                        Description = task.Description,
                                        TaskId = task.TaskId,
                                        Day = task.Day,
                                        Depth = task.Depth,
                                        SeletedDependency = task.SeletedDependency == null ? "" : task.SeletedDependency.Replace(";", ","),
                                        LeadTime = task.LeadTime,
                                        ScheduleTime = (TimeSpan?)task.ScheduleTime,
                                        IsActive = task.IsActive,
                                        IsSpecialServices = task.IsSpecialServices == "0" ? "1" : task.IsSpecialServices.ToString(),
                                        IsBiddable = task.IsBiddable,
                                        StageType = task.StageType,
                                        ServiceCategoryId = task.ServiceCategoryId,
                                        StageTypeName = task.StageTypeName == null ? "N/A" : task.StageTypeName,
                                        CategoryName = task.CategoryName,
                                        ServiceDuration = task.ServiceDuration,

                                        ServiceDurationDays = task.ServiceDurationDays,
                                        ServiceDurationHours = task.ServiceDurationHours,
                                        ServiceDurationMinutes = task.ServiceDurationMinutes,
                                        //ServiceDurationDays = task.ServiceDurationDays != null ? Convert.ToString(task.ServiceDurationDays.Split(':', StringSplitOptions.RemoveEmptyEntries)[0]) : "00",
                                        //ServiceDurationHours = task.ServiceDurationHours != null ? Convert.ToString(task.ServiceDurationHours.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]) : "00",
                                        //ServiceDurationMinutes = task.ServiceDurationMinutes != null ? Convert.ToString(task.ServiceDurationMinutes.Split(':', StringSplitOptions.RemoveEmptyEntries)[2]) : "00",


                                        //ServiceDurationDays = task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length == 2 ? "00" : task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0],
                                        //ServiceDurationHours = task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length == 2 ? task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0] : task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1],
                                       // ServiceDurationMinutes = task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length == 2 ? task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1] : task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2],

                                        IsActiveCategory = task.IsActiveCategory
                                    }).ToList();

                    templateList = templateList.OrderBy(t => t.Name).ToList();
                }

                return Json(templateList.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ReadServiceTasks", User.Identity.Name);
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetDuration()
        {
            try
            {
                var Duration = new List<Duration>();
                Duration.Add(new Duration { Text = 7, Value = 7 });
                Duration.Add(new Duration { Text = 14, Value = 14 });
                Duration.Add(new Duration { Text = 21, Value = 21 });
                return Json(Duration);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ReadServiceTasks", User.Identity.Name);
                return null;
            }
        }
    }
}
