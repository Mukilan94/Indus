using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.Identity;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;

namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    //[SessionTimeOut]
    public class ServiceCategoryController : BaseController
    {
        //Phase II - Clear Warning
        private new readonly WebAIAdvisorContext db;        
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly UserManager<StaffWellIdentityUser> _staffUserManager;
        //ServiceCategory
        public ServiceCategoryController(WebAIAdvisorContext db,ISingletonAdministration _singleton, UserManager<WellIdentityUser> userManager,
           RoleManager<IdentityRole> roleManager, SignInManager<StaffWellIdentityUser> signInManager, UserManager<StaffWellIdentityUser> staffUserManager) :base(_singleton,signInManager, db)
        {
            this.db= db;
            _userManager = userManager;
            _roleManager = roleManager;
            _staffUserManager = staffUserManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ServiceCategory_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var serviceCategory = _singleton.serviceCategoryBusiness.GetServiceCategories().Result;

                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ServiceCategory ServiceCategory_Read", User.Identity.Name);
                
                return null;
            }
        }

        public ActionResult ServiceCategoryByParentId_Read([DataSourceRequest] DataSourceRequest request,string ParentId)
        {
            try
            {
                var serviceCategory = _singleton.serviceCategoryBusiness.GetServiceSubCategories(ParentId).Result;

                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ServiceCategory ServiceCategoryByParentId_Read", User.Identity.Name);
                
                return null;
            }
        }


        [AcceptVerbs("Post")]
        public async Task<IActionResult> ServiceCategory_Destroy([DataSourceRequest] DataSourceRequest request, ServiceCategoryModel input)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                input.CreatedBy = UserId;

                ServiceCategory service = new ServiceCategory()
                {
                    ServiceCategoryId = input.ServiceCategoryId,
                    CreatedDate = DateTime.UtcNow,
                    Description = input.Description,
                    Name = input.Name,
                    IsActive = false,
                    ParentId = input.ParentId,
                    CreatedBy = input.CreatedBy,
                };
                db.serviceCategories.Update(service);
                await db.SaveChangesAsync();

                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ServiceCategory ServiceCategory_Destroy", User.Identity.Name);
                
                return null;
            }
        }


        [HttpPost]
        public async Task<IActionResult> ServiceCategory_Delete(string ServiceCategoryId)
        {
            try
            {
                bool isUsed = false;

                var Tasks = (from t in db.Tasks
                             join ct in db.CategoryTasks on t.TaskId equals ct.TaskId
                             join sc in db.serviceCategories on ct.ServiceCategoryId equals sc.ServiceCategoryId
                             where ct.ServiceCategoryId == ServiceCategoryId && ct.IsActive == true
                             select t).ToList();

                    if(Tasks.Count == 0)
                    {
                         var drillPlan = db.DrillPlanDetails.Where(x => x.CategoryId == ServiceCategoryId).Count();
                        if (drillPlan > 0)
                        {
                          isUsed = true;
                        }
                        else if (drillPlan == 0)
                            {
                                var Checklist = db.ChecklistTemplate.ToList();
                                
                                foreach(var tasks in Checklist)
                                {
                                    var ChecklistTemplateList = JsonConvert.DeserializeObject<WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateTaskListModel>(tasks.Checklist);
                                    
                                    if(ChecklistTemplateList.count > 0)
                                    {
                                        var TemplateList = ChecklistTemplateList.checklist.Where(x => x.ServiceCategoryId == ServiceCategoryId).Count();
                                        if(TemplateList > 0)
                                        {
                                            isUsed = true;
                                            break;
                                        }
                                    }
                                }
                            }
                    }
                    else
                    {
                        isUsed = true;
                    }

                if (isUsed == true)
                {
                    return Json(new { isUsed = true });
                }
                else
                {
                    var Categories = db.serviceCategories.Where(x => x.ServiceCategoryId == ServiceCategoryId).FirstOrDefault();
                    db.serviceCategories.Remove(Categories);
                    await db.SaveChangesAsync();
                }

                return Json(new {  });
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ServiceCategory ServiceCategory_Destroy", User.Identity.Name);
                return null;
            }
        }



        public IActionResult AddParentServiceCategory([DataSourceRequest] DataSourceRequest request, ServiceCategoryModel input)
        {
            string result = string.Empty;
            try
            {
                string Id = Guid.NewGuid().ToString();
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value; 
                input.ServiceCategoryId = Id;
                input.ParentId = Id;
                input.CreatedBy = UserId;
                result= _singleton.serviceCategoryBusiness.AddServiceCategory(input).Result;
                if (result != "True")
                {
                    ModelState.AddModelError("Name", result);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ServiceCategory AddParentServiceCategory", User.Identity.Name);
                
                return null;
            }
            return Json(new[] { result }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult GetParentServiceCategories()
        {
            List<ServiceCategoryModel> result = new List<ServiceCategoryModel>();
            try
            {  
                result = _singleton.serviceCategoryBusiness.GetParentServiceCategories().Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ServiceCategory GetParentServiceCategories", User.Identity.Name);
                
                return null;
            }
            return Json(result);
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> ServiceCategory_Create([DataSourceRequest] DataSourceRequest request, ServiceCategoryModel input)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                input.ServiceCategoryId = Id;
                input.CreatedBy = UserId;
                var result = await _singleton.serviceCategoryBusiness.AddServiceCategory(input);
                if (result != "True")
                {
                    ModelState.AddModelError("Name", result);
                }

                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ServiceCategory ServiceCategory_Create", User.Identity.Name);
                
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ServiceCategory_Update([DataSourceRequest] DataSourceRequest request, ServiceCategoryModel input)
        {
            try
            {
                var result = await _singleton.serviceCategoryBusiness.UpdateServiceCategory(input);
                if (result != "True")
                {
                    ModelState.AddModelError("Name", result);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _staffUserManager, db, Guid.Parse(_staffUserManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ServiceCategory ServiceCategory_Update", User.Identity.Name);
                
                return null;
            }
        }
    }
}