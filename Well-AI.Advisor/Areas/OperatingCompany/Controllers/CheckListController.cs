using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Identity;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.BLL.IBusiness;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using WellAI.Advisor.DLL.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.DLL.Entity;
using Finbuckle.MultiTenant;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class CheckListController : BaseController
    {
        private readonly ILogger<CheckListController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantOperatingDbContext _tenantdb;

        public CheckListController(
            UserManager<WellIdentityUser> userManager,
           SignInManager<WellIdentityUser> signInManager,
           RoleManager<IdentityRole> roleManager,
           WebAIAdvisorContext dbContext,
           TenantOperatingDbContext tenantdb,
           ILogger<CheckListController> logger) : base(userManager, dbContext)
        {
            _logger = logger;
            _tenantdb = tenantdb;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            db = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                //checking invalid user//
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/OperatingDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                PopulateVendors(tenantId);
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                string wellCheckLists = string.Empty;
                if (wellId == "00000000-0000-0000-0000-000000000000")
                {
                    var wellCheckList = (from wellcheck in db.WellCheckList
                                         join rig in db.rig_register on wellcheck.RigId equals rig.Rig_id
                                         join well in db.WellRegister on wellcheck.WellId equals well.well_id
                                         where wellcheck.TenantID.Equals(tenantId) && well.Prediction == true && rig.isActive == true
                                         select new { wellcheck.CheckList })
                                   .ToList();
                    List<ProjectWellCheckListModel> projectWellCheckListModels = new List<ProjectWellCheckListModel>();
                    foreach (var item in wellCheckList)
                    {
                        var checkListDetail = JsonConvert.DeserializeObject<List<ProjectWellCheckListModel>>(item.CheckList);
                        projectWellCheckListModels.AddRange(checkListDetail);
                    }
                    ViewBag.TotalTasks = projectWellCheckListModels.Where(x => x.Type == 1).Count();
                    ViewBag.TotalService = projectWellCheckListModels.Where(x => x.Type == 2).Count();
                    ViewBag.TotalSpecial = projectWellCheckListModels.Where(x => x.Type == 3).Count();
                    ViewBag.TotalSupply = projectWellCheckListModels.Where(x => x.Type == 4).Count();
                    return View();
                }
                else
                {
                    var wellCheckList = (from wellcheck in db.WellCheckList
                                         where wellcheck.TenantID.Equals(tenantId) && (wellcheck.RigId == wellId)
                                         select new { wellcheck })
                                   .FirstOrDefault();
                    if (wellCheckList != null)
                    {
                        wellCheckLists = wellCheckList.wellcheck.CheckList;
                    }
                }
                if (!string.IsNullOrEmpty(wellCheckLists))
                {
                    var checkListDetail = JsonConvert.DeserializeObject<List<ProjectWellCheckListModel>>(wellCheckLists);

                    ViewBag.TotalTasks = checkListDetail.Where(x => x.Type == 1).Count();
                    ViewBag.TotalService = checkListDetail.Where(x => x.Type == 2).Count();
                    ViewBag.TotalSpecial = checkListDetail.Where(x => x.Type == 3).Count();
                    ViewBag.TotalSupply = checkListDetail.Where(x => x.Type == 4).Count();
                }
                else
                {
                    ViewBag.TotalTasks = 0;
                    ViewBag.TotalService = 0;
                    ViewBag.TotalSpecial = 0;
                    ViewBag.TotalSupply = 0;
                }
                var RigCookies = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(RigCookies) ? DLL.Constants.NoSpecificWellFilterKey : RigCookies.ToString();
                var Rigs = await GetRigsDataAsync(RigId);
                ViewData["Rigs"] = Rigs;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return View();
        }

        public async Task<IActionResult> Counts()
        {
            try
            {
                var CheclistCount = new CheclistCounts();
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                PopulateVendors(tenantId);
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                string wellCheckLists = string.Empty;
                if (wellId == "00000000-0000-0000-0000-000000000000")
                {
                    var wellCheckList = (from wellcheck in db.WellCheckList
                                         join rig in db.rig_register on wellcheck.RigId equals rig.Rig_id
                                         join well in db.WellRegister on wellcheck.WellId equals well.well_id
                                         where wellcheck.TenantID.Equals(tenantId) && well.Prediction == true && rig.isActive == true
                                         select new { wellcheck.CheckList })
                                   .ToList();
                    List<ProjectWellCheckListModel> projectWellCheckListModels = new List<ProjectWellCheckListModel>();
                    foreach (var item in wellCheckList)
                    {
                        var checkListDetail = JsonConvert.DeserializeObject<List<ProjectWellCheckListModel>>(item.CheckList);
                        projectWellCheckListModels.AddRange(checkListDetail);
                    }
                    CheclistCount = new CheclistCounts {
                        TotalTasks = projectWellCheckListModels.Where(x => x.Type == 1).Count(),
                        TotalService = projectWellCheckListModels.Where(x => x.Type == 2).Count(),
                        TotalSpecial = projectWellCheckListModels.Where(x => x.Type == 3).Count(),
                        TotalSupply = projectWellCheckListModels.Where(x => x.Type == 4).Count()
                    };

                    return await Task.FromResult(Json(CheclistCount));
                }
                else
                {
                    var wellCheckList = (from wellcheck in db.WellCheckList
                                         where wellcheck.TenantID.Equals(tenantId) && (wellcheck.RigId == wellId)
                                         select new { wellcheck })
                                   .FirstOrDefault();
                    if (wellCheckList != null)
                    {
                        wellCheckLists = wellCheckList.wellcheck.CheckList;
                    }
                }

                if (!string.IsNullOrEmpty(wellCheckLists))
                {
                    var checkListDetail = JsonConvert.DeserializeObject<List<ProjectWellCheckListModel>>(wellCheckLists);
                    CheclistCount = new CheclistCounts {
                        TotalTasks = checkListDetail.Where(x => x.Type == 1).Count(),
                        TotalService = checkListDetail.Where(x => x.Type == 2).Count(),
                        TotalSpecial = checkListDetail.Where(x => x.Type == 3).Count(),
                        TotalSupply = checkListDetail.Where(x => x.Type == 4).Count()
                    };
                }
                else
                {
                    CheclistCount = new CheclistCounts {
                        TotalTasks = 0,
                        TotalService = 0,
                        TotalSpecial = 0,
                        TotalSupply = 0
                    };
                }
                return Json(CheclistCount);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist Counts", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return null;
            }
        }

#nullable enable

        public JsonResult GetWelllisByRig(string text, string? RigId)
        {
            try
            {
                var userwell = JsonConvert.DeserializeObject<WellIdentityUser>(
                    WellAIAppContext.Current.Session.GetString(WellAI.Advisor.DLL.Constants.SessionUserWellIdentity));
                var result = (from Well in db.WellRegister
                              where Well.RigID == RigId && Well.customer_id == userwell.TenantId && Well.Prediction == true
                              select new {
                                  WellId = Well.well_id,
                                  Name = Well.wellname,
                              }).ToList();

                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CheckList GetWelllisByRig", User.Identity.Name);
                return Json("");
            }
        }

        public async Task<IActionResult> EditTask()
        {
            try
            {
                var RigCookies = Request.Cookies["wellfilterlayout"];
                var RigId = string.IsNullOrEmpty(RigCookies) ? DLL.Constants.NoSpecificWellFilterKey : RigCookies.ToString();
                var stages = await GetStagesDataAsync();
                var rigs = await GetRigsDataAsync(RigId);
                var auctionProposalBusiness = new AuctionProposalBusiness(db, _userManager);
                var categories = await auctionProposalBusiness.GetServiceCategorys();
                if (RigId == "00000000-0000-0000-0000-000000000000")
                {
                    rigs.Insert(0, new RigViewModel { RigId = DLL.Constants.NoSpecificWellFilterKey, RigName = "Select Rig" });
                }
                else
                {
                }
                stages.Insert(0, new Stage { Id = DLL.Constants.NoSpecificWellFilterKey, Name = "Select Stage" });
                categories.Insert(0, new ServiceCategory { ServiceCategoryId = DLL.Constants.NoSpecificWellFilterKey, Name = "Select Category" });
                ViewData["Rigs"] = rigs;
                ViewData["stages"] = stages;
                ViewData["categories"] = categories;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist EditTask", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return PartialView("_EditTask");
        }

        private async Task<List<Stage>> GetStagesDataAsync()
        {
            try
            {
                var stages = db.Stages.OrderBy(x => x.Name).ToList();
                return await Task.FromResult(stages);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist GetStagesDataAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                List<Stage> Stage = new List<Stage>();
                return Stage;
            }
        }

        private async Task<List<RigViewModel>> GetRigsDataAsync(string RigId)
        {
            try
            {
                var CheckRigFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                var userId = WellAIAppContext.Current.Session.GetString(DLL.Constants.SessionNotExpireKey);
                var userWells = db.UserRigs.Where(x => x.UserId == userId).ToList();
                var Rigs = new List<RigViewModel>();
                if (userWells.Count > 0)
                {
                    Rigs = (from uwell in userWells
                            join Rig in db.rig_register on uwell.RigID equals Rig.Rig_id
                            where Rig.isActive.Equals(true) && (Rig.Rig_id == RigId && !CheckRigFilter || CheckRigFilter)
                            select new RigViewModel {
                                RigId = Rig.Rig_id,
                                RigName = Rig.Rig_Name
                            }).OrderBy(x => x.RigName).ToList();
                }
                else
                {
                    var operId = WellAIAppContext.Current.Session.GetString("TenantId");
                    Rigs = db.rig_register.Where(x => x.TenantID == operId && x.isActive == true && (x.Rig_id == RigId && !CheckRigFilter || CheckRigFilter))
                        .Select(x => new RigViewModel {
                            RigId = x.Rig_id,
                            RigName = x.Rig_Name
                        }).OrderBy(x => x.RigName).ToList();
                }
                return await Task.FromResult(Rigs);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist GetRigsDataAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                List<RigViewModel> Rigs = new List<RigViewModel>();
                return Rigs;
            }
        }

        private void PopulateVendors(string tenantId)
        {
            try
            {
                var operrepo = new OperatingTenantRepository(_tenantdb);
                var providerDirectoryId = operrepo.GetProviderDirectoryId(tenantId).Result;
                var corpProfiles = db.CorporateProfile.Where(x => providerDirectoryId.Contains(x.TenantId)).Select(x => new VendorViewModel { Vendor = x.TenantId, VendorName = x.Name }).OrderBy(e => e.VendorName).ToList();
                var defaultval = new VendorViewModel {
                    VendorName = "--Select--",
                    Vendor = "null2"
                };
                corpProfiles.Insert(0, defaultval);
                ViewData["vendors"] = corpProfiles;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "Checklist PopulateVendors", User.Identity.Name);
                _logger.LogInformation(ex.Message);
            }
        }

        public async Task<IActionResult> CheckList_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var wellIdCookie = Request.Cookies["wellfilterlayout"];
                var wellId = string.IsNullOrEmpty(wellIdCookie) ? DLL.Constants.NoSpecificWellFilterKey : wellIdCookie.ToString();
                var wellCheckListDetail = await projectBusiness.GetWellCheckListForProjects(tenantId, wellId);
                return Json(wellCheckListDetail.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CheckList CheckList_Read", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CheckList_UpdateAsync([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<ProjectWellCheckListModel> checkListModels)
        {
            try
            {
                IEnumerable<ProjectWellCheckListModel> result = new List<ProjectWellCheckListModel>();
                result = checkListModels;
                if (checkListModels != null)
                {
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                    result = await projectBusiness.UpdateWellCheckStatusListForProjects(tenantId, checkListModels);
                }
                return Json(result.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CheckList CheckList_UpdateAsync", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckList_Create([DataSourceRequest] DataSourceRequest request, CheckListTaskModel input)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                input.TaskId = Id;
                input.CreatedBy = UserId;
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                await projectBusiness.CreateCheckListItem(tenantId, input);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CheckList CheckList_Create", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public IActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProjectCheckList(ProjectViewModel input, IFormCollection form)
        {
            try
            {
                string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var ids = string.IsNullOrEmpty(input.CheckIds) ? new string[] { } : input.CheckIds.Split(';', StringSplitOptions.RemoveEmptyEntries);
                var checkIds = ids.Length > 0 ? ids.ToList() : new List<string>();
                var updateResult = await projectBusiness.UpdateWellCheckStatusListForProjects(tenantId, checkIds);
                if (string.IsNullOrEmpty(updateResult))
                    return RedirectToAction("Index", new RouteValueDictionary(new { controller = "CheckList", action = "Index" }));
                else
                    throw new Exception(updateResult);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CheckList", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        public JsonResult GetServiceStage()
        {
            try
            {
                var StageType = db.ServiceStages.Select(x => new Model.OperatingCompany.Models.ServiceStage {
                    Stage_id = x.StageId,
                    Stage_Type = x.Stage_Type
                }).ToList();
                return Json(StageType);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CheckList GetServiceStage", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json("");
            }
        }

        public JsonResult GetCategoriesList(string text)
        {
            try
            {
                var result = db.serviceCategories.Where(x => x.ServiceCategoryId == x.ParentId && x.IsActive == true).Select(x => new Model.OperatingCompany.Models.ServiceCategoryModel {
                    ServiceCategoryId = x.ServiceCategoryId,
                    Name = x.Name
                }).ToList();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CheckList GetCategoriesList", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return Json("");
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
                return rolePermissionBusiness.GetComponentBasedOnRolesList(roleIds, "CheckListView", TenantId);
            }
            else
            {
                return false;
            }
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> CheckList_DestroyItem(string wellCheckListId, string wellId)
        {
            if (!string.IsNullOrEmpty(wellCheckListId) && !string.IsNullOrEmpty(wellId))
            {
                try
                {
                    string tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                    IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                    var ids = wellCheckListId.Split(":");
                    var wellCheckListId1 = ids[0];
                    var taskId = ids[1];
                    await projectBusiness.RemoveCheckListItem(tenantId, wellId, wellCheckListId1, taskId);
                    return Json(Guid.NewGuid());
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "CheckList CheckList_DestroyItem", User.Identity.Name);
                    _logger.LogInformation(ex.Message);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
            }
            return Json("");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> EnsureAndGetWellCheckList(string id)
        {
            try
            {
                string wellId = id;
                if (wellId == null)
                {
                    return RedirectToAction("Index");
                }
                Response.Cookies.Append("wellfilterlayout", wellId);
                var wellName = db.WellRegister.Where(x => x.well_id == id).FirstOrDefault();
                if (wellName != null)
                {
                    TempData["wellNameFilter"] = wellName.wellname;
                }
                string tenantId = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                List<WellCheckListDetailModel> wellCheckListDetail = new List<WellCheckListDetailModel>();
                if (wellId == "00000000-0000-0000-0000-000000000000")
                {
                    return RedirectToAction("Index");
                }
                var wellCheckLists = db.WellCheckList.FirstOrDefault(x => x.TenantID == tenantId && x.WellId == wellId);
                if (wellCheckLists != null)
                {
                    wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(wellCheckLists.CheckList);
                }
                else
                {
                    var taskList = await db.Tasks.Where(x => x.IsActive == true).ToListAsync();
                    wellCheckListDetail = await (from task in db.Tasks
                                                 join st in db.Stages on task.StageType equals st.Id into st1
                                                 from st in st1.DefaultIfEmpty()
                                                 join ct in db.CategoryTasks on task.TaskId equals ct.TaskId
                                                 join Category in db.serviceCategories on ct.ServiceCategoryId equals Category.ServiceCategoryId
                                                 where task.IsActive == true
                                                 select new WellCheckListDetailModel {
                                                     WellTaskId = task.TaskId,
                                                     WellTaskName = task.Name,
                                                     Day = task.Day,
                                                     Depth = task.Depth,
                                                     Duration = task.Duration,
                                                     Time = (System.TimeSpan?)task.ScheduleTime,
                                                     Type = task.IsSpecialServices,
                                                     IsBiddable = task.IsBiddable,
                                                     StageType = st.Name,
                                                     ServiceCategory = Category.Name
                                                 }).ToListAsync();
                    wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(JsonConvert.SerializeObject(wellCheckListDetail));
                    WellCheckList wellCheckList = new WellCheckList {
                        CheckList = JsonConvert.SerializeObject(wellCheckListDetail),
                        TenantID = tenantId,
                        WellId = wellId,
                        WellChecklistId = Guid.NewGuid().ToString(),
                        RigId = wellName == null ? null : wellName.RigID
                    };
                    if (wellId != null)
                    {
                        db.WellCheckList.Add(wellCheckList);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CheckList EnsureAndGetWellCheckList", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }

    public class VendorViewModel
    {
        public string? Vendor { get; set; }
        public string? VendorName { get; set; }
    }
}