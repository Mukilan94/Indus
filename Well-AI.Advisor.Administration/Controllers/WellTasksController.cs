using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Xls;
//DWOP
using Telerik.Windows.Documents.Spreadsheet.Model;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;


namespace Well_AI.Advisor.Administration.Controllers
{
    //Phase II Changes - 03/10/2021 - Session Timeout Wrapper
    //[SessionTimeOut]
    [Produces("application/json")]
    public class WellTasksController : BaseController
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

        //Phase II - Clear Warning
        private new readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<StaffWellIdentityUser> _userManager;
        public WellTasksController(WebAIAdvisorContext db, ISingletonAdministration _singleton,
            RoleManager<IdentityRole> roleManager,
            UserManager<StaffWellIdentityUser> userManager) : base(_singleton, db)
        {
            this.db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        //WellTasks

        public IActionResult Index()
        {
            try
            {
                var StageType = db.Stages.Select(x => new ServiceStageModel
                {
                    Stage_id = x.Id,
                    Stage_Type = x.Name
                }).ToList();

                ViewData["StageList"] = StageType;

                return View("TaskIndex");
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Index", User.Identity.Name);

                return null;
            }
        }
        #region Service Methods and Actions Details
        public ActionResult Task_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var serviceCategory = _singleton.wellTaskBusiness.GetTasks1().Result;
                serviceCategory.OrderBy(x => x.Name);
                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Task_Read", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Task_Create([DataSourceRequest] DataSourceRequest request, TaskModel input)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                input.TaskId = Id;
                input.CreatedBy = UserId;
                input.SeletedDependency = input.SeletedDependency != null ? input.SeletedDependency.Replace(";", ",") : "";
                var result = await _singleton.wellTaskBusiness.AddTask(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Task_Create", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Task_Update([DataSourceRequest] DataSourceRequest request, TaskModel input)
        {
            try
            {
                var result = await _singleton.wellTaskBusiness.UpdateTask(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Task_Update", User.Identity.Name);

                return null;
            }
        }

        public async Task<IActionResult> Task_Destroy([DataSourceRequest] DataSourceRequest request, TaskModel input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Result = db.Tasks.Where(x => x.TaskId == input.TaskId).FirstOrDefault();
                    Result.IsActive = false;
                    db.Tasks.Update(Result);
                    await db.SaveChangesAsync();
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Task_Destroy", User.Identity.Name);

                return null;
            }
        }
        public async Task<JsonResult> GetTaskList()
        {
            try
            {
                List<TaskModel> result = new List<TaskModel>();
                result = await _singleton.wellTaskBusiness.GetTasksForCategoryLink();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetTaskList", User.Identity.Name);

                return null;
            }
        }

        public JsonResult GetServiceStage()
        {
            try
            {
                var StageType = db.Stages.Select(x => new ServiceStageModel
                {
                    Stage_id = x.Id,
                    Stage_Type = x.Name
                }).ToList();

                return Json(StageType);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetServiceStage", User.Identity.Name);

                return Json("");
            }
        }

        //DWOP
        public JsonResult GetServiceDays()
        {
            try
            {
                List<ServiceDuration> days = new List<ServiceDuration>();

                for (int i = 0; i < 100; i++)
                {

                    var value = Convert.ToString(i + 1);
                    if (value.Length < 2)
                    {
                        value = "0" + i;
                    }
                    var result = new ServiceDuration
                    {
                        Text = Convert.ToString(value),
                        Value = Convert.ToString(value)
                    };

                    days.Add(result);

                }

                return Json(days);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetServiceDays", User.Identity.Name);

                return Json("");
            }
        }

        //DWOP
        public JsonResult GetServiceHours()
        {
            try
            {
                List<ServiceDuration> Hours = new List<ServiceDuration>();

                for (var i = 0; i < 23; i++)
                {
                    var value = Convert.ToString(i + 1);
                    if (value.Length < 2)
                    {
                        value = "0" + i;
                    }
                    var result = new ServiceDuration
                    {
                        Text = Convert.ToString(value),
                        Value = Convert.ToString(value)
                    };

                    Hours.Add(result);

                }

                return Json(Hours);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetServiceHours", User.Identity.Name);

                return Json("");
            }
        }
        //DWOP
        public JsonResult GetServiceMinutes()
        {
            try
            {
                List<ServiceDuration> Minutes = new List<ServiceDuration>();

                for (var i = 0; i < 45; i++)
                {
                    var value = "";
                    var Text = "";

                    if (i != 0)
                    {
                        Text = Convert.ToString(15 + i - 1);
                        i = Convert.ToInt16(Text);
                        value = Text == "15" ? "15" : Text == "30" ? "30" : "45";
                    }
                    if (Text.Length < 2)
                    {
                        Text = "0" + i;
                        value = "00";
                    }
                    var result = new ServiceDuration
                    {
                        Text = Convert.ToString(Text),
                        Value = Convert.ToString(value)
                    };

                    Minutes.Add(result);

                }

                return Json(Minutes);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetServiceDays", User.Identity.Name);

                return Json("");
            }
        }


        public JsonResult GetCategoriesList(string text)
        {
            try
            {
                var result = db.serviceCategories.Where(x => x.ServiceCategoryId == x.ParentId).Select(x => new ServiceCategoryModel
                //var result = db.serviceCategories.Where(x => x.ServiceCategoryId == x.ParentId && x.IsActive == true).Select(x => new ServiceCategoryModel
                {
                    ServiceCategoryId = x.ServiceCategoryId,
                    Name = x.Name
                }).OrderBy(x => x.Name).ToList();

                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetCategoriesList", User.Identity.Name);

                return null;
            }
        }

        public JsonResult GetTaskDependencyList(string id)
        {
            try
            {
                List<TaskModel> result = new List<TaskModel>();
                if (string.IsNullOrEmpty(id))
                {
                    result = _singleton.wellTaskBusiness.GetTasks().Result;

                }
                else
                    result = _singleton.wellTaskBusiness.GetTasks().Result.Where(x => x.TaskId != id).ToList();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetTaskDependencyList", User.Identity.Name);

                return null;
            }
        }
        #endregion

        #region WellService Methods and Actions Details
        public IActionResult AssignedTask(string id)
        {
            try
            {
                ViewBag.WellType = id;
                var assignedTaskid = _singleton.wellTaskBusiness.GetWellTaskIdByWellTypeId(id).Result.Select(x => x.TaskId).ToList();
                var fulltaskList = _singleton.wellTaskBusiness.GetTasks().Result;
                var AssignTasks = fulltaskList.Where(x => assignedTaskid.Contains(x.TaskId));
                var UnassignTask = fulltaskList.Except(AssignTasks);

                ViewBag.AssignedTask = AssignTasks.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.TaskId
                });

                ViewBag.UnassignTask = UnassignTask.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.TaskId
                });

                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks AssignedTask", User.Identity.Name);

                return null;
            }
        }
        [HttpPost]
        public JsonResult AddWellTasks([FromBody] AddWellTask input)
        {
            try
            {
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var result = _singleton.wellTaskBusiness.AddWellTask(input, UserId);
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks AddWellTasks", User.Identity.Name);

                return null;
            }
        }
        public IActionResult WellTask()
        {
            return View();
        }


        #endregion

        #region CategoryTask Methods and Actiond details
        public IActionResult CategoryTask()
        {
            return View();
        }

        public JsonResult GetCategoryList()
        {
            try
            {
                List<ServiceCategoryModel> result = new List<ServiceCategoryModel>();
                result = _singleton.serviceCategoryBusiness.GetCategoryParentList().Result;
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetCategoryList", User.Identity.Name);

                return null;
            }
        }
        public JsonResult GetSubCategoryList(string categories)
        {
            try
            {
                List<ServiceCategoryModel> result = new List<ServiceCategoryModel>();
                result = _singleton.serviceCategoryBusiness.GetSubCategoryList(categories).Result;
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks GetSubCategoryList", User.Identity.Name);

                return null;
            }
        }

        public ActionResult CategoryTask_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var serviceCategory = _singleton.wellTaskBusiness.GetCategoryTask().Result;

                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks CategoryTask_Read", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CategoryTask_Create([DataSourceRequest] DataSourceRequest request, CategoryTaskModel input)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                input.CategoryTaskId = Id;
                input.CreatedBy = UserId;

                var result = await _singleton.wellTaskBusiness.AddCategoryTask(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                input.IsActive = result.IsActive;
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks CategoryTask_Create", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CategoryTask_Update([DataSourceRequest] DataSourceRequest request, CategoryTaskModel input)
        {
            try
            {
                var result = await _singleton.wellTaskBusiness.UpdateCategoryTask(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks CategoryTask_Update", User.Identity.Name);

                return null;
            }
        }

        #endregion

        #region WellType


        public ActionResult WellType_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var serviceCategory = _singleton.wellTaskBusiness.GetWellTypes().Result;

                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks WellType_Read", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> WellType_Create([DataSourceRequest] DataSourceRequest request, WellTypeModel input)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                input.WellTypeId = Id;
                var result = await _singleton.wellTaskBusiness.AddWellType(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks WellType_Create", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> WellType_Update([DataSourceRequest] DataSourceRequest request, WellTypeModel input)
        {
            try
            {
                var result = await _singleton.wellTaskBusiness.UpdateWellType(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks WellType_Update", User.Identity.Name);

                return null;
            }
        }
        #endregion

        #region Supplies

        public IActionResult Supplies()
        {

            return View();
        }
        public ActionResult Supplies_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var serviceCategory = _singleton.wellTaskBusiness.GetSupplies().Result;

                return Json(serviceCategory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Supplies_Read", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Supplies_Create([DataSourceRequest] DataSourceRequest request, SuppliesModel input)
        {
            try
            {
                string Id = Guid.NewGuid().ToString();
                string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                input.SupplyId = Id;
                var result = await _singleton.wellTaskBusiness.AddSupplies(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Supplies_Create", User.Identity.Name);

                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Supplies_Update([DataSourceRequest] DataSourceRequest request, SuppliesModel input)
        {
            try
            {
                var result = await _singleton.wellTaskBusiness.UpdateSupplies(input);
                if (result.ErrorMessage != null)
                {
                    ModelState.AddModelError("Name", result.ErrorMessage);
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdmin customErrorHandler = new CustomErrorHandlerForAdmin(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)));
                customErrorHandler.WriteError(ex, "WellTasks Supplies_Update", User.Identity.Name);

                return null;
            }
        }
        #endregion

        #region Import
        //DWOP
        [HttpPost]
        public async Task<IActionResult> ImportTasks(IFormCollection form)
        {
            try
            {
                List<Tasks> importedTasksList = new List<Tasks>();
                List<CategoryTask> categoryTasksList = new List<CategoryTask>();
                List<TaskModel> ServiceTasks = new List<TaskModel>();

                string duplicateServices = "";

                if (form.Files.Count > 0)
                {
                    string fileName = form.Files[0].FileName;

                    string[] fileInfo = fileName.Split(".");
                    string fileType = "";
                    if (fileInfo.Length > 0)
                    {
                        fileType = fileInfo[fileInfo.Length - 1];
                    }

                    var inputStream = form.Files[0].OpenReadStream();
                    var workbook = new Workbook();
                    if (fileType == "xlsx" )
                    {
                        XlsxFormatProvider formatProvider = new XlsxFormatProvider();

                        workbook = formatProvider.Import(inputStream);
                    }
                    else if (fileType == "xls")
                    {
                        XlsFormatProvider formatProvider = new XlsFormatProvider();

                        workbook = formatProvider.Import(inputStream);
                    }
                    else if (fileType == "csv")
                    {
                        IWorkbookFormatProvider formatCSVProvider = new CsvFormatProvider();

                        workbook = formatCSVProvider.Import(inputStream);
                    }
                    workbook.ActiveWorksheet = workbook.Worksheets[0];

                    Worksheet worksheet = workbook.ActiveWorksheet;
                    CellRange usedCellRange = worksheet.UsedCellRange;
                    for (int rowIndex = usedCellRange.FromIndex.RowIndex; rowIndex <= usedCellRange.ToIndex.RowIndex; rowIndex++)
                    {
                        Tasks input = new Tasks();
                        CategoryTask categoryTask = new CategoryTask();
                        TaskModel inputModel = new TaskModel();
                        string Id = Guid.NewGuid().ToString();
                        string UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        input.TaskId = Id;
                        input.CreatedBy = UserId;
                        input.Dependency = "";
                        input.CreatedDate = DateTime.UtcNow;
                        input.IsActive = true;
                        input.Day = null;
                        input.Depth = null;
                        input.Duration = null;
                        input.LeadTime = null;
                        input.ScheduleTime = null;
                        input.IsSpecialServices = 1;
                        input.IsBiddable = false;
                        input.StageType = null;
                        input.IsBenchMark = false;
                        input.IsCalendar = false;
                        input.IsPreSpud = false;

                        if (rowIndex > 0)
                        {
                            for (int columnIndex = usedCellRange.FromIndex.ColumnIndex; columnIndex <= usedCellRange.ToIndex.ColumnIndex; columnIndex++)
                            {
                                if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.TaskName))
                                {
                                    input.Name = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString();
                                    if (input.Name == "")
                                    {
                                        break;
                                    }
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.TaskDescription)) {
                                    if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        input.Description = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString();
                                    }
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.Day))
                                {
                                    if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        input.Day = Convert.ToInt16(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString());
                                    }
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.ScheduleTime))
                                {

                                    //CellValueFormat format = new CellValueFormat("hh:mm:ss tt"
                                    //cell.SetFormat(format);
                                    //CellValueFormat format = new CellValueFormat();

                                    //input.ScheduleTime = DateTime.ParseExact(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString().Replace("\"", ""), "h:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
                                    //worksheet.Cells[rowIndex, columnIndex].SetFormat(format);

                                    //if (value.Contains("="))
                                    //{
                                    //    value = value.ToString().Replace("=", "");
                                    //    input.ScheduleTime = DateTime.ParseExact(value, "h:mm:ss tt", CultureInfo.InvariantCulture).TimeOfDay;
                                    //}
                                    //else
                                    //{
                                    if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        input.ScheduleTime = DateTime.ParseExact(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString(), "HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                                    }
                                    //}
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.Depth)) { 
                                    if(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString()!="")
                                    {
                                        input.Depth = Convert.ToInt32(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString());
                                    }                                    
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.LeadTime))
                                {
                                    if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        input.LeadTime = Convert.ToInt32(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString());
                                    }
                                }                                    
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.SpecialServices))
                                {
                                    if(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() == "Task")
                                            input.IsSpecialServices = 1;
                                        else if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() == "Service")
                                            input.IsSpecialServices = 2;
                                        else if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() == "Special Service")
                                            input.IsSpecialServices = 3;
                                        else if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() == "Supply")
                                            input.IsSpecialServices = 4;
                                    }                                    
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.Biddable))
                                {
                                    if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        input.IsBiddable = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString().ToUpper() == "YES" ? true : false;
                                    }
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.StageType))
                                {
                                    if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        var stageType = db.Stages.Where(x => x.Name == worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString()).Select(y => y.Id).FirstOrDefault();
                                        if (stageType != "")
                                        {
                                            input.StageType = stageType;
                                        }
                                    }
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.ServiceCategory))
                                {
                                    if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        var serviceCategoryId = db.serviceCategories.Where(x => x.Name == worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString()).Select(y => y.ServiceCategoryId).FirstOrDefault();
                                        if (serviceCategoryId != "")
                                        {
                                            //input.ServiceCategoryId = serviceCategoryId;

                                            categoryTask = new CategoryTask
                                            {
                                                CategoryTaskId = Guid.NewGuid().ToString(),
                                                ServiceCategoryId = serviceCategoryId,
                                                TaskId = input.TaskId,
                                                CreatedBy = input.CreatedBy,
                                                CreatedDate = DateTime.UtcNow,
                                                IsActive = true
                                            };
                                        }
                                    }
                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.ServiceDuration))
                                {
                                    if (worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        //var value = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString();
                                        input.ServiceDuration = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString();
                                        input.ServiceDuration = CalculateServiceDuration(input.ServiceDuration);

                                        //csv expe
                                        ////if (value.Contains("="))
                                        ////{
                                        //value = value.ToString().Replace("=", "");
                                        //string[] valueArr = value.ToString().Split(":");
                                        //string hours = "00";
                                        //string mins = "00";
                                        //string sec = "00";
                                        //if (valueArr.Length > 0)
                                        //{
                                        //    if (valueArr.Length >= 1)
                                        //    {
                                        //        hours = valueArr[0].ToString().Length == 1 ? "0".ToString() + valueArr[0].ToString() : valueArr[0].ToString();
                                        //    }
                                        //    if (valueArr.Length >= 2)
                                        //    {
                                        //        mins = valueArr[1].ToString().Length == 1 ? "0".ToString() + valueArr[1].ToString() : valueArr[1].ToString();
                                        //    }
                                        //    if (valueArr.Length >= 3)
                                        //    {
                                        //        sec = valueArr[2].ToString().Length == 1 ? "0".ToString() + valueArr[2].ToString() : valueArr[2].ToString();
                                        //    }
                                        //    value = hours.ToString() + ":" + mins.ToString() + ":" + sec.ToString();
                                        //    input.ServiceDuration = CalculateServiceDuration(input.ServiceDuration);
                                        //}
                                        //else
                                        //{
                                        //    input.ServiceDuration = "00:00:00";
                                        //}
                                        //input.ServiceDuration = value.ToString().Replace("\"", "");

                                        //}
                                        //else
                                        //{

                                        //}    
                                    }

                                }
                                else if (Convert.ToInt16(columnIndex) == Convert.ToInt16(EnumImporTaksColumn.Dependency))
                                {
                                    if(worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString() != "")
                                    {
                                        string[] arrDependencies = worksheet.Cells[rowIndex, columnIndex].GetValue().Value.RawValue.Trim().ToString().Split("$$");
                                        string dependencyTaskValues = "";
                                        if (arrDependencies.Length > 0)
                                        {
                                            for (int dIndex = 0; dIndex < arrDependencies.Length; dIndex++)
                                            {
                                                var dependencyTaskId = db.Tasks.Where(x => x.Name == arrDependencies[dIndex]).Select(y => y.TaskId).FirstOrDefault();
                                                if ((dependencyTaskId == "" || dependencyTaskId == null) && arrDependencies[dIndex] != "")
                                                {
                                                    dependencyTaskId = importedTasksList.Where(x => x.Name == arrDependencies[dIndex]).Select(y => y.TaskId).FirstOrDefault();
                                                }
                                                if (dependencyTaskValues == "" && dependencyTaskId != "")
                                                    dependencyTaskValues = dependencyTaskId;
                                                else if (dependencyTaskId != "")
                                                    dependencyTaskValues = dependencyTaskValues + "," + dependencyTaskId;
                                            }
                                            if (dependencyTaskValues != "")
                                                input.Dependency = dependencyTaskValues;
                                        }
                                    }                                    
                                }
                            }

                            inputModel.TaskId = input.TaskId;
                            inputModel.Name = input.Name;

                            var taskExist = await _singleton.wellTaskBusiness.IsTaskExist(inputModel);
                            if (taskExist == 0 && input.Name != "")
                            {
                                importedTasksList.Add(input);
                                categoryTasksList.Add(categoryTask);
                            }
                            if (taskExist == 1)
                            {
                                //if (duplicateServices == "")
                                //    duplicateServices = input.Name;
                                //else

                                var Tasks = new TaskModel
                                {
                                    Name = input.Name,
                                    ServiceDuration = input.ServiceDuration,
                                    StageType = db.Stages.Where(x => x.Id == input.StageType).Select(y => y.Name).FirstOrDefault(),
                                    Depth = input.Depth,
                                    SeletedDependency = input.Dependency,
                                    Description = input.Description,
                                    Day = input.Day,
                                    CategoryName = db.serviceCategories.Where(x => x.ServiceCategoryId == categoryTask.ServiceCategoryId).Select(y => y.Name).FirstOrDefault(),
                                    IsBenchMark = false,
                                    IsCalendar = false,
                                    IsPreSpud = false
                            };

                                ServiceTasks.Add(Tasks);

                                // duplicateServices = duplicateServices + "; "  + input.Name;
                            }
                        }
                    }
                    if (importedTasksList.Count > 0)
                    {
                        var result = await _singleton.wellTaskBusiness.ImportTasks(importedTasksList, categoryTasksList);
                    }

                }

                if (ServiceTasks.Count > 0)
                {
                    return Json(new { Value = ServiceTasks });
                }

                //TempData["DuplicationMessage"] = duplicateServices.ToString();
                //return RedirectToAction("Index");
                return Content("");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        #endregion


        public string CalculateServiceDuration(string ServiceDuration)
        {
            try
            {
                string duration = "";
                var serviceDurationArray = ServiceDuration.Split(":");
                if (serviceDurationArray.Length == 3)
                {
                   var Min = (decimal)((decimal)Convert.ToDecimal(serviceDurationArray[2]) / 100) * (decimal)60;
                   duration = serviceDurationArray[0] + ":" + serviceDurationArray[1] + ":" + Min.ToString("00");
                }
                return duration;
            }
            catch(Exception ex)
            {
                return "00:00:00";
            }
        }
    }
}
