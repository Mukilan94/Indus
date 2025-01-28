
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;
//DWOP
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using System.Globalization;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using System.Security.Claims;

namespace Well_AI.Advisor.Administration.Controllers
{
    public class CheckListTemplateController : Controller
    {
        //DWOP
        private enum EnumImporTaksColumn
        {
            TaskName = 0,
            TaskDescription = 1,
            Day = 2,
            ScheduleTime = 3,
            Depth = 4,
            LeadTime = 5,
            SpecialServices = 6,
            Biddable = 7,
            StageType = 8,
            ServiceCategory = 9,
            ServiceDuration = 10,
            Dependency = 11
        }

        private new readonly WebAIAdvisorContext db;
        private readonly WebAIAdvisorContext dbcontext;
        protected readonly ISingletonAdministration _singleton;
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<StaffWellIdentityUser> _userManager;

        public CheckListTemplateController(WebAIAdvisorContext _dbcontext, ISingletonAdministration singleton, RoleManager<IdentityRole> roleManager,
            UserManager<StaffWellIdentityUser> userManager)
        {
            dbcontext = _dbcontext;
            _singleton = singleton;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            try
            {
                var WellType = dbcontext.WellType.Select(x => new WellTypeModel
                {
                    WellTypeId = x.welltype_id,
                    WellTypeName = x.welltype_name
                }).OrderBy(x => x.WellTypeName).ToList();

                ViewData["WellTypes"] = WellType;
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate Index", User.Identity.Name);
                return null;
            }
        }

        public IActionResult AddTasks(string WellType)
        {
            ViewBag.WellTypeId = WellType;

            return PartialView("_AddTasks");
        }

        [HttpPost]
        public IActionResult SaveTasks([FromBody] List<ChecklistTemplateModel> tasksList)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

                var Result = _singleton.wellTaskBusiness.SaveWellDesignChecklist(tasksList, userId);
                return Json(new { status = 1, id = Result.Result });
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate SaveTasks", User.Identity.Name);
                return null;
            }
        }

        public ActionResult FilterMenuCustomization_Stages()
        {
            List<StageFilter> stages = new List<StageFilter>();
            StageFilter stageFilter = new StageFilter();
            stageFilter.StageTypeName = "1 : Mob";
            stages.Add(stageFilter);

            stageFilter = new StageFilter();
            stageFilter.StageTypeName = "2 : Surface";
            stages.Add(stageFilter);

            stageFilter = new StageFilter();
            stageFilter.StageTypeName = "3 : 1st intermediate";
            stages.Add(stageFilter);

            stageFilter = new StageFilter();
            stageFilter.StageTypeName = "4 : 2nd intermediate";
            stages.Add(stageFilter);

            stageFilter = new StageFilter();
            stageFilter.StageTypeName = "5 : Production";
            stages.Add(stageFilter);

            stageFilter = new StageFilter();
            stageFilter.StageTypeName = "6 : Demob";
            stages.Add(stageFilter);

            stageFilter = new StageFilter();
            stageFilter.StageTypeName = "7 : Miscellaneous";
            stages.Add(stageFilter);
            //var stageList = db.Stages.OrderBy(s=>s.Name);
            //var stages = (List<Stage>)(stages.Select(e => e.Name).Distinct());
            return Json(stages.Select(e => e.StageTypeName).Distinct());
        }
        public async Task<IActionResult> ReadCheckListTemplate([DataSourceRequest] DataSourceRequest request, string welltype)
        {
            try
            {
                List<ChecklistTemplateModel> checklistTemplate = new List<ChecklistTemplateModel>();
                checklistTemplate = await _singleton.wellTaskBusiness.ReadChecklistTemplate(welltype);

                if (checklistTemplate == null)
                {
                    checklistTemplate = new List<ChecklistTemplateModel>();
                }
                return Json(checklistTemplate.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate ReadCheckListTemplate", User.Identity.Name);
                return null;
            }
        }

        public ActionResult Task_Read([DataSourceRequest] DataSourceRequest request, string WellType)
        {
            try
            {
                var serviceCategory = _singleton.wellTaskBusiness.GetTasks1().Result;
                if (WellType != null)
                {
                    var Tasks = dbcontext.WellType.Where(x => x.welltype_id == WellType).FirstOrDefault();

                    if (Tasks.DrillPlanChecklist != null)
                    {
                        var TasksExits = JsonConvert.DeserializeObject<List<ChecklistTemplateModel>>(Tasks.DrillPlanChecklist);

                        var TaskId = TasksExits.Select(x => x.TaskId).ToList();

                        var TasksList = from c in serviceCategory
                                        where !TaskId.Contains(c.TaskId)
                                        select c;

                        TasksList.OrderBy(x => x.Name);
                        return Json(TasksList.ToDataSourceResult(request));
                    }
                }
                serviceCategory.OrderBy(x => x.Name);
                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate Task_Read", User.Identity.Name);
                return null;
            }
        }

        public PartialViewResult AddWellType(WellTypeModel input)
        {
            if (input.WellTypeId != null)
            {
                var wellTypeData = dbcontext.WellType.Where(y => y.welltype_id == input.WellTypeId).Select(x => new WellTypeModel
                {
                    WellTypeId = x.welltype_id,
                    WellTypeName = x.welltype_name
                }).FirstOrDefault();

                return PartialView("_AddWellType", wellTypeData);

            }
            return PartialView("_AddWellType");
        }



        [HttpPost]
        public IActionResult DeleteWellDesign(string WellDesignId)
        {
            try
            {
                if (!string.IsNullOrEmpty(WellDesignId))
                {
                    int IswellDesignExits = dbcontext.WellRegister.Where(x => x.welltype_id == WellDesignId).Count();
                    int Ischecklisttemplate = dbcontext.ChecklistTemplate.Where(x => x.WellTypeId == WellDesignId).Count();
                    if (IswellDesignExits == 0 && Ischecklisttemplate == 0)
                    {

                        var WellTypeExits = dbcontext.WellType.Where(x => x.welltype_id == WellDesignId).FirstOrDefault();
                        if (WellTypeExits != null)
                        {
                            dbcontext.WellType.Remove(WellTypeExits);
                            dbcontext.SaveChanges();
                        }

                        return Json(new { Status = "Success" });
                    }
                }

                return Json(new { Status = "Failed", WellDesignName = dbcontext.WellType.Where(y => y.welltype_id == WellDesignId).Select(x => x.welltype_name).FirstOrDefault() });
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate DeleteWellDesign", User.Identity.Name);
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetWellType()
        {
            try
            {
                var wellTypeData = dbcontext.WellType.Select(x => new WellTypeModel
                {
                    WellTypeId = x.welltype_id,
                    WellTypeName = x.welltype_name
                }).OrderBy(x => x.WellTypeName).ToList();

                return Json(wellTypeData);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate GetWellType", User.Identity.Name);
                return null;
            }
        }
        public IActionResult Checklist_Delete(string TaskId, string WellDesignId)
        {
            try
            {
                var ChecklistTemplate = dbcontext.WellType.Where(x => x.welltype_id == WellDesignId).FirstOrDefault();
                var GettasksList = JsonConvert.DeserializeObject<List<ChecklistTemplateModel>>(ChecklistTemplate.DrillPlanChecklist);
                var Task = GettasksList.Where(x => x.TaskId == TaskId).FirstOrDefault();
                GettasksList.Remove(Task);
                ChecklistTemplate.DrillPlanChecklist = JsonConvert.SerializeObject(GettasksList);
                dbcontext.WellType.Update(ChecklistTemplate);
                dbcontext.SaveChanges();

                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate Checklist_Delete", User.Identity.Name);
                return null;
            }
        }

        public IActionResult WellTypeChecklistExists(string WellDesignId)
        {
            try
            {
                var ChecklistTemplate = dbcontext.WellType.Where(x => x.welltype_id == WellDesignId).FirstOrDefault();

                if (ChecklistTemplate.DrillPlanChecklist != null)
                {
                    return Json(new { status = "Exists" });
                }
                else
                {
                    return Json(new { status = "NotExists" });
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate WellTypeChecklistExists", User.Identity.Name);
                return null;
            }
        }

        public IActionResult WellTypeNameExists(string TypeName)
        {
            try
            {
                var typeName = dbcontext.WellType.Where(x => x.welltype_name == TypeName).FirstOrDefault();

                if (typeName != null && typeName.ToString() != "")
                {
                    return Json(new { status = "Exists" });
                }
                else
                {
                    return Json(new { status = "NotExists" });
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate WellTypeNameExists", User.Identity.Name);
                return null;
            }
        }


        [AcceptVerbs("Post")]
        public ActionResult Task_Create([DataSourceRequest] DataSourceRequest request, TaskModel input)
        {
            string category = ""; //db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
            string stagetype = "";// db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Id).FirstOrDefault(),
            string hours = "";
            string mins = "";
            string days = "";
            if (input != null)
            {
                if (input.ExportToMaster)
                {
                    var TasksExits = dbcontext.Tasks.Where(x => x.Name == input.Name && x.IsSpecialServices == Convert.ToInt32(input.IsSpecialServices) && x.IsActive == true).FirstOrDefault();
                    if (TasksExits != null)
                    {
                        ModelState.AddModelError("Tasks", "This task is already exits");
                        return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                    }
                }

                category = dbcontext.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                stagetype = dbcontext.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault();
                hours = input.ServiceDurationHours ?? "00";
                mins = input.ServiceDurationMinutes ?? "00";
                days = input.ServiceDurationDays ?? "00";
            }
            ChecklistTemplateModel ChecklistTemplate = new ChecklistTemplateModel
            {
                TaskId = Guid.NewGuid().ToString(),
                IsSpecialServices = input.IsSpecialServices,
                CategoryName = category,
                ServiceCategoryId = input.ServiceCategoryId,
                Name = input.Name,
                ServiceDuration = days + ":" + hours + ":" + mins,
                Depth = input.Depth,
                IsActiveCategory = input.IsActiveCategory,
                Description = input.Description,
                SeletedDependency = input.SeletedDependency != null ? input.SeletedDependency.Replace(";", ",") : null,
                StageType = input.StageType,
                StageTypeName = stagetype,
                LeadTime = input.LeadTime,
                Day = input.Day,
                ScheduleTime = input.ScheduleTime,
                ServiceDurationDays = days,
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

        public IActionResult Task_Update([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.Administration.TaskModel input)
        {
            try
            {
                string category = ""; //db.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                string stagetype = "";// db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Id).FirstOrDefault(),
                string hours = "";
                string mins = "";
                string days = "";
                if (input != null)
                {
                    category = dbcontext.serviceCategories.Where(x => x.ServiceCategoryId == input.ServiceCategoryId).Select(y => y.Name).FirstOrDefault();
                    stagetype = dbcontext.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault();
                    hours = input.ServiceDurationHours ?? "00";
                    mins = input.ServiceDurationMinutes ?? "00";
                    days = input.ServiceDurationDays ?? "00";

                }

                ChecklistTemplateModel ChecklistTemplate = new ChecklistTemplateModel
                {
                    TaskId = input.TaskId,
                    IsSpecialServices = input.IsSpecialServices,
                    CategoryName = category,
                    ServiceCategoryId = input.ServiceCategoryId,
                    Name = input.Name,
                    ServiceDuration = days + ":" + hours + ":" + mins,
                    Depth = input.Depth,
                    IsActiveCategory = input.IsActiveCategory,
                    Description = input.Description,
                    SeletedDependency = input.SeletedDependency != null ? input.SeletedDependency.Replace(";", ",") : null,
                    StageType = input.StageType,
                    StageTypeName = stagetype,
                    LeadTime = input.LeadTime,
                    Day = input.Day,
                    ScheduleTime = input.ScheduleTime,
                    ServiceDurationDays = days,
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
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate Task_Update", User.Identity.Name);
                return null;
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
                    var Tasks = dbcontext.Tasks.Where(x => x.TaskId == taskId).FirstOrDefault();
                    if (taskId != null)
                    {
                        status = true;
                    }
                }

                return Json(new { status });
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "ChecklistTemplate tasksExits", User.Identity.Name);
                return null;
            }
        }

        //#region Import
        ////DWOP
        //[HttpPost]
        //public async Task<IActionResult> ImportTasks(IFormCollection form)
        //{
        //    try
        //    {
        //        List<Tasks> importedTasksList = new List<Tasks>();
        //        List<CategoryTask> categoryTasksList = new List<CategoryTask>();
        //        List<TaskModel> ServiceTasks = new List<TaskModel>();

        //        string duplicateServices = "";

        //        if (form.Files.Count > 0)
        //        {
        //            string fileName = form.Files[0].FileName;

        //            string[] fileInfo = fileName.Split(".");
        //            string fileType = "";
        //            if (fileInfo.Length > 0)
        //            {
        //                fileType = fileInfo[fileInfo.Length - 1];
        //            }

        //            var inputStream = form.Files[0].OpenReadStream();
        //            Workbook workbook = new Workbook();
        //            if (fileType == "xlsx")
        //            {
        //                XlsxFormatProvider formatProvider = new XlsxFormatProvider();

        //                workbook = formatProvider.Import(inputStream);
        //            }
        //            else if (fileType == "csv")
        //            {
        //                IWorkbookFormatProvider formatCSVProvider = new CsvFormatProvider();

        //                workbook = formatCSVProvider.Import(inputStream);

        //            }

        //            workbook.ActiveWorksheet = workbook.Worksheets[0];

        //            Worksheet worksheet = workbook.ActiveWorksheet;


        //            CellRange usedCellRange = worksheet.UsedCellRange;
        //            for (int rowIndex = usedCellRange.FromIndex.RowIndex; rowIndex <= usedCellRange.ToIndex.RowIndex; rowIndex++)
        //            {
        //                Tasks input = new Tasks();
        //                CategoryTask categoryTask = new CategoryTask();
        //                TaskModel inputModel = new TaskModel();
        //                string Id = Guid.NewGuid().ToString();
        //                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //                input.TaskId = Id;
        //                input.CreatedBy = UserId;
        //                input.Dependency = "";
        //                input.CreatedDate = DateTime.UtcNow;
        //                input.IsActive = true;
        //                input.Day = null;
        //                input.Depth = null;
        //                input.Duration = null;
        //                input.LeadTime = null;
        //                input.ScheduleTime = null;
        //                input.IsSpecialServices = 1;
        //                input.IsBiddable = false;
        //                input.StageType = null;

        //                if (rowIndex > 0)
        //                {
        //                    for (int columnIndex = usedCellRange.FromIndex.ColumnIndex; columnIndex <= usedCellRange.ToIndex.ColumnIndex; columnIndex++)
        //                    {
        //                        if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.TaskName))
        //                        {
        //                            input.Name = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString();
        //                            if (input.Name == "")
        //                            {
        //                                continue;
        //                            }
        //                        }
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.TaskDescription))
        //                            input.Description = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString();
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.Day))
        //                            input.Day = Convert.ToInt16(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString());
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.ScheduleTime))
        //                        {
        //                            input.ScheduleTime = DateTime.ParseExact(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString().Replace("\"", ""), "h:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
        //                        }
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.Depth))
        //                            input.Depth = Convert.ToInt32(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString());
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.LeadTime))
        //                            input.LeadTime = Convert.ToInt32(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString());
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.SpecialServices))
        //                        {
        //                            if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() == "Task")
        //                                input.IsSpecialServices = 1;
        //                            else if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() == "Service")
        //                                input.IsSpecialServices = 2;
        //                            else if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() == "Special Service")
        //                                input.IsSpecialServices = 3;
        //                            else if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() == "Supply")
        //                                input.IsSpecialServices = 4;
        //                        }
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.Biddable))
        //                            input.IsBiddable = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString().ToUpper() == "YES" ? true : false;
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.StageType))
        //                        {
        //                            var stageType = db.Stages.Where(x => x.Name == worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString()).Select(y => y.Id).FirstOrDefault();
        //                            if (stageType != "")
        //                            {
        //                                input.StageType = stageType;
        //                            }
        //                        }
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.ServiceCategory))
        //                        {
        //                            if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
        //                            {
        //                                var serviceCategoryId = db.serviceCategories.Where(x => x.Name == worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString()).Select(y => y.ServiceCategoryId).FirstOrDefault();
        //                                if (serviceCategoryId != "")
        //                                {
        //                                    //input.ServiceCategoryId = serviceCategoryId;

        //                                    categoryTask = new CategoryTask
        //                                    {
        //                                        CategoryTaskId = Guid.NewGuid().ToString(),
        //                                        ServiceCategoryId = serviceCategoryId,
        //                                        TaskId = input.TaskId,
        //                                        CreatedBy = input.CreatedBy,
        //                                        CreatedDate = DateTime.UtcNow,
        //                                        IsActive = true
        //                                    };
        //                                }
        //                            }

        //                        }
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.ServiceDuration))
        //                            input.ServiceDuration = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString();
        //                        else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.Dependency))
        //                        {
        //                            string[] arrDependencies = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString().Split("$$");
        //                            string dependencyTaskValues = "";
        //                            if (arrDependencies.Length > 0)
        //                            {
        //                                for (int dIndex = 0; dIndex < arrDependencies.Length; dIndex++)
        //                                {
        //                                    var dependencyTaskId = db.Tasks.Where(x => x.Name == arrDependencies[dIndex]).Select(y => y.TaskId).FirstOrDefault();
        //                                    if ((dependencyTaskId == "" || dependencyTaskId == null) && arrDependencies[dIndex] != "")
        //                                    {
        //                                        dependencyTaskId = importedTasksList.Where(x => x.Name == arrDependencies[dIndex]).Select(y => y.TaskId).FirstOrDefault();
        //                                    }
        //                                    if (dependencyTaskValues == "" && dependencyTaskId != "")
        //                                        dependencyTaskValues = dependencyTaskId;
        //                                    else if (dependencyTaskId != "")
        //                                        dependencyTaskValues = dependencyTaskValues + "," + dependencyTaskId;
        //                                }
        //                                if (dependencyTaskValues != "")
        //                                    input.Dependency = dependencyTaskValues;
        //                            }

        //                        }
        //                    }

        //                    inputModel.TaskId = input.TaskId;
        //                    inputModel.Name = input.Name;

        //                    var taskExist = await _singleton.wellTaskBusiness.IsTaskExist(inputModel);
        //                    if (taskExist == 0 && input.Name != "")
        //                    {
        //                        importedTasksList.Add(input);
        //                        categoryTasksList.Add(categoryTask);
        //                    }
        //                    if (taskExist == 1)
        //                    {
        //                        //if (duplicateServices == "")
        //                        //    duplicateServices = input.Name;
        //                        //else

        //                        var Tasks = new TaskModel
        //                        {
        //                            Name = input.Name,
        //                            ServiceDuration = input.ServiceDuration,
        //                            StageType = db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault(),
        //                            Depth = input.Depth,
        //                            SeletedDependency = input.Dependency,
        //                            Description = input.Description,
        //                            Day = input.Day,
        //                            CategoryName = db.serviceCategories.Where(x => x.ServiceCategoryId == categoryTask.ServiceCategoryId).Select(y => y.Name).FirstOrDefault()
        //                        };

        //                        ServiceTasks.Add(Tasks);

        //                        // duplicateServices = duplicateServices + "; "  + input.Name;
        //                    }
        //                }
        //            }
        //            if (importedTasksList.Count > 0)
        //            {
        //                //var result = await _singleton.wellTaskBusiness.ImportTasks(importedTasksList, categoryTasksList);
        //            }

        //        }

        //        if (ServiceTasks.Count > 0)
        //        {
        //            return Json(new { Value = ServiceTasks });
        //        }

        //        return Content("");
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}
        //#endregion
    }

    public class StageFilter
    {
        public string StageTypeName { get; set; }
    }

}
