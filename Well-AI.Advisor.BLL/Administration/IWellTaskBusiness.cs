using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
//DWOP
using EFCore.BulkExtensions;
using Newtonsoft.Json;

namespace WellAI.Advisor.BLL.Administration
{
    public interface IWellTaskBusiness
    {
        #region Task Method
        Task<List<TaskModel>> GetTasks();
        Task<List<TaskModel>> GetTasks1();

        Task<TaskModel> AddTask(TaskModel taskModel);
        Task<TaskModel> UpdateTask(TaskModel taskModel);
        Task<List<TaskModel>> GetTasksForCategoryLink();
        Task<int> IsTaskExist(TaskModel taskModel);
        //DWOP
        Task<int> ImportTasks(List<Tasks> tasksList, List<CategoryTask> categoryTasksList);
        Task<string> SaveWellDesignChecklist(List<ChecklistTemplateModel> tasksList,string userId);
        Task<List<ChecklistTemplateModel>> ReadChecklistTemplate(string welltype);


        #endregion

        #region WellTask Method
        Task<List<WellTaskModel>> GetWellTaskIdByWellTypeId(string wellTypeId);
        Task<bool> AddWellTask(AddWellTask input, string CreatedBy);
        Task<WellTaskModel> UpdateWellTask(WellTaskModel input);
        Task<bool> IsWellTaskExist(WellTaskModel input);
        #endregion



        #region Category Task Methods
        Task<CategoryTaskModel> AddCategoryTask(CategoryTaskModel input);
        Task<CategoryTaskModel> UpdateCategoryTask(CategoryTaskModel input);
        Task<List<CategoryTaskModel>> GetCategoryTask();
        #endregion

        #region Supplies Methods
        Task<SuppliesModel> AddSupplies(SuppliesModel input);
        Task<SuppliesModel> UpdateSupplies(SuppliesModel input);
        Task<List<SuppliesModel>> GetSupplies();
        #endregion

        #region WellType Methods
        Task<List<WellTypeModel>> GetWellTypes(); //Well Type
        Task<WellTypeModel> AddWellType(WellTypeModel input);
        Task<WellTypeModel> UpdateWellType(WellTypeModel input);
        #endregion
    }

    public class WellTaskBusiness : IWellTaskBusiness
    {
        private readonly WebAIAdvisorContext db;
        UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        protected readonly IMapper _mapper;

        public WellTaskBusiness(WebAIAdvisorContext db, UserManager<WellIdentityUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        #region Task Method
        public Task<TaskModel> AddTask(TaskModel taskModel)
        {
            try
            {
                int servicetype;

                servicetype = taskModel.IsSpecialServices == "1" ? 1 : taskModel.IsSpecialServices == "2" ? 2 : taskModel.IsSpecialServices == "3" ? 3 : taskModel.IsSpecialServices == "4" ? 4 : 1;
                string Days = taskModel.ServiceDurationDays == null ? "00" : taskModel.ServiceDurationDays;
                string Hours = taskModel.ServiceDurationDays == null ? "00" : taskModel.ServiceDurationHours;
                string Minutes = taskModel.ServiceDurationMinutes == null ? "00" : taskModel.ServiceDurationMinutes;
                
                taskModel.ErrorMessage = null;

                taskModel.TaskId = taskModel.TaskId == "" ? Guid.NewGuid().ToString() : taskModel.TaskId;

                Tasks tasks = new Tasks()
                {
                    CreatedBy = taskModel.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    Description = taskModel.Description,
                    IsActive = true,
                    Day = taskModel.Day,
                    Dependency = taskModel.SeletedDependency,
                    Depth = taskModel.Depth,
                    Duration = taskModel.Duration,
                    LeadTime = taskModel.LeadTime,
                    ScheduleTime = (TimeSpan?)taskModel.ScheduleTime,
                    Name = taskModel.Name,
                    IsSpecialServices = servicetype,
                    TaskId = taskModel.TaskId,
                    IsBiddable = taskModel.IsBiddable,
                    StageType = taskModel.StageType,
                    ServiceDuration = Days + ":" + Hours + ":" + Minutes,
                    IsBenchMark = taskModel.IsBenchMark,
                    IsPreSpud = taskModel.IsPreSpud,
                    IsCalendar = taskModel.IsCalendar
            };

                CategoryTask CategoryTask = new CategoryTask
                {
                    CategoryTaskId = Guid.NewGuid().ToString(),
                    ServiceCategoryId = taskModel.ServiceCategoryId,
                    TaskId = taskModel.TaskId,
                    CreatedBy = taskModel.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };
                db.CategoryTasks.Add(CategoryTask);
                db.Tasks.Add(tasks);
                db.SaveChanges();
                return Task.FromResult(taskModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask AddTask", null);
                return null;
            }
        }

        public Task<List<TaskModel>> GetTasks1()
        {
            try
            {
                //DWOP - Get Category Active Status
                return (from task in db.Tasks
                        join category in db.CategoryTasks on task.TaskId equals category.TaskId
                        join service in db.serviceCategories on category.ServiceCategoryId equals service.ServiceCategoryId
                        join Stage in db.Stages on task.StageType equals Stage.Id into Stage1
                        from Stage in Stage1.DefaultIfEmpty()
                        where category.IsActive == true && task.IsActive == true
                        select new TaskModel
                        {
                            Name = task.Name,
                            Description = task.Description,
                            TaskId = task.TaskId,
                            Day = task.Day,
                            Depth = task.Depth,
                            SeletedDependency = task.Dependency != null ? task.Dependency.Replace(";", ",") : null,
                            Duration = task.Duration,
                            LeadTime = task.LeadTime,
                            ScheduleTime = (TimeSpan?)task.ScheduleTime,
                            IsActive = task.IsActive,
                            IsSpecialServices = task.IsSpecialServices == 0 ? "1" : task.IsSpecialServices.ToString(),
                            IsBiddable = task.IsBiddable,
                            StageType = task.StageType,
                            ServiceCategoryId = category.ServiceCategoryId,
                            StageTypeName = Stage.Name == null ? "N/A" : Stage.Name,
                            CategoryName = service.Name,
                            ServiceDuration = task.ServiceDuration,
                            ServiceDurationDays = task.ServiceDuration != null ? (task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0])) : "00",
                            ServiceDurationHours = task.ServiceDuration != null ? (task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])) : "00",
                            ServiceDurationMinutes = task.ServiceDuration != null ? (task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2])): "00",
                            IsActiveCategory = service.IsActive ,
                            IsPreSpud= task.IsPreSpud,
                            IsBenchMark = task.IsBenchMark,
                            IsCalendar=task.IsCalendar
                        }).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask GetTasks1", null);
                return null;
            }
        }

        public Task<List<TaskModel>> GetTasksForCategoryLink()
        {
            try
            {
                return (from task in db.Tasks
                        join Stage in db.Stages on task.StageType equals Stage.Id into Stage1
                        from Stage in Stage1.DefaultIfEmpty()
                        select new TaskModel
                        {
                            Name = task.Name,
                            Description = task.Description,
                            TaskId = task.TaskId,
                            Day = task.Day,
                            Depth = task.Depth,
                            SeletedDependency = task.Dependency.Replace(";", ","),
                            Duration = task.Duration,
                            LeadTime = task.LeadTime,
                            ScheduleTime = (TimeSpan)task.ScheduleTime,
                            IsActive = task.IsActive,
                            IsSpecialServices = task.IsSpecialServices == 0 ? "1" : task.IsSpecialServices.ToString(),
                            IsBiddable = task.IsBiddable,
                            StageType = task.StageType,
                            StageTypeName = Stage.Name == null ? "N/A" : Stage.Name
                        }).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask GetTasksForCategoryLink", null);
                return null;
            }
        }

        public Task<List<TaskModel>> GetTasks()
        {
            try
            {
                var result = db.Tasks.Select(x => new TaskModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    TaskId = x.TaskId,
                    Day = x.Day,
                    Depth = x.Depth,
                    SeletedDependency = x.Dependency.Replace(";", ","),
                    Duration = x.Duration,
                    LeadTime = x.LeadTime,
                    ScheduleTime = (TimeSpan)x.ScheduleTime,
                    IsActive = x.IsActive,
                    IsSpecialServices = x.IsSpecialServices == 0 ? "1" : x.IsSpecialServices.ToString(),
                    IsBiddable = x.IsBiddable,
                    StageType = x.StageType
                }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask GetTasks", null);
                return null;
            }
        }

        public Task<int> IsTaskExist(TaskModel taskModel)
        {
            try
            {
                return db.Tasks.Where(x => x.Name == taskModel.Name && x.IsActive == true).CountAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask IsTaskExist", null);
                return null;
            }
        }
        private Task<int> IsTaskExistForUpdate(TaskModel taskModel)
        {
            try
            {
                return db.Tasks.Where(x => x.Name.Contains(taskModel.Name) && x.TaskId != taskModel.TaskId).CountAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask IsTaskExistForUpdate", null);
                return null;
            }
        }

        public Task<TaskModel> UpdateTask(TaskModel taskModel)
        {
            int servicetype;

            servicetype = taskModel.IsSpecialServices == "1" ? 1 : taskModel.IsSpecialServices == "2" ? 2 : taskModel.IsSpecialServices == "3" ? 3 : taskModel.IsSpecialServices == "4" ? 4 : 1;

            try
            {
                taskModel.ErrorMessage = null;
                var result = db.Tasks.Where(x => x.TaskId == taskModel.TaskId).FirstOrDefault();
                if (result != null)
                {
                    result.Description = taskModel.Description;
                    result.IsActive = taskModel.IsActive;
                    result.Name = taskModel.Name;
                    result.Day = taskModel.Day;
                    result.Dependency = taskModel.SeletedDependency;
                    result.Depth = taskModel.Depth;
                    result.Duration = taskModel.Duration;
                    result.LeadTime = taskModel.LeadTime;
                    result.ScheduleTime = (TimeSpan?)taskModel.ScheduleTime;
                    result.IsSpecialServices = servicetype;
                    result.IsBiddable = taskModel.IsBiddable;
                    result.StageType = taskModel.StageType;
                    result.ServiceDuration = taskModel.ServiceDurationDays + ":" + taskModel.ServiceDurationHours + ":" + taskModel.ServiceDurationMinutes;
                    result.IsBenchMark = taskModel.IsBenchMark;
                    result.IsPreSpud = taskModel.IsPreSpud;
                    result.IsCalendar = taskModel.IsCalendar;
                    db.SaveChanges();
                }
                else
                {
                    taskModel.ErrorMessage = "Task not found";
                }

                var Result1 = db.CategoryTasks.Where(x => x.TaskId == taskModel.TaskId).FirstOrDefault();
                if (Result1 != null)
                {
                    Result1.ServiceCategoryId = taskModel.ServiceCategoryId;
                    Result1.IsActive = taskModel.IsActive;
                    db.SaveChanges();
                }
                else
                {
                    CategoryTask CategoryTask = new CategoryTask
                    {
                        CategoryTaskId = Guid.NewGuid().ToString(),
                        ServiceCategoryId = taskModel.ServiceCategoryId,
                        TaskId = taskModel.TaskId,
                        CreatedBy = taskModel.CreatedBy,
                        CreatedDate = DateTime.UtcNow,
                        IsActive = true
                    };
                    db.CategoryTasks.Add(CategoryTask);

                    db.SaveChanges();
                }

                return Task.FromResult(taskModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask UpdateTask", null);
                return null;
            }

        }
        //DWOP
        public Task<int> ImportTasks(List<Tasks> tasksList, List<CategoryTask> categoryTasksList)
        {
            db.BulkInsert(tasksList);
            db.BulkInsert(categoryTasksList);
            return Task.FromResult(1);
        }


        public Task<string> SaveWellDesignChecklist(List<ChecklistTemplateModel> tasksList,string userId)
        {
            string result = "";
            try
            {
                var TaskObj = JsonConvert.SerializeObject(tasksList);

                if (tasksList[0].WellDesignId == "")
                {
                    if(tasksList[0].WellDesignName != null)
                    {
                        WellType Tasks = new WellType
                        {
                            welltype_id = Guid.NewGuid().ToString(),
                            welltype_name = tasksList[0].WellDesignName,
                            DrillPlanChecklist = TaskObj
                        };

                        db.WellType.Add(Tasks);
                        db.SaveChanges();

                        tasksList[0].WellDesignId = Tasks.welltype_id;
                        result = Tasks.welltype_id;
                    }
                }
                else
                {
                    var ExistingTasksObject = db.WellType.Where(x => x.welltype_id == tasksList[0].WellDesignId).FirstOrDefault();                  
                    ExistingTasksObject.DrillPlanChecklist = TaskObj;
                    ExistingTasksObject.welltype_name = tasksList[0].WellDesignName;
                    db.WellType.Update(ExistingTasksObject);
                    db.SaveChanges();
                }
                
                foreach(var taskModel in tasksList)
                {
                    if (taskModel.ExportToMaster)
                    {
                        taskModel.TaskId = taskModel.TaskId == "" ? Guid.NewGuid().ToString() : taskModel.TaskId;
                        Tasks tasks = new Tasks()
                        {
                            CreatedBy = userId,
                            CreatedDate = DateTime.UtcNow,
                            Description = taskModel.Description,
                            IsActive = true,
                            Day = taskModel.Day,
                            Dependency = taskModel.SeletedDependency,
                            Depth = taskModel.Depth,
                            LeadTime = taskModel.LeadTime,
                            ScheduleTime = (TimeSpan?)taskModel.ScheduleTime,
                            Name = taskModel.Name,
                            IsSpecialServices = Convert.ToInt32(taskModel.IsSpecialServices),
                            TaskId = taskModel.TaskId,
                            IsBiddable = taskModel.IsBiddable,
                            StageType = taskModel.StageType,
                            ServiceDuration = taskModel.ServiceDuration
                        };

                        CategoryTask CategoryTask = new CategoryTask
                        {
                            CategoryTaskId = Guid.NewGuid().ToString(),
                            ServiceCategoryId = taskModel.ServiceCategoryId,
                            TaskId = taskModel.TaskId,
                            CreatedBy = userId,
                            CreatedDate = DateTime.UtcNow,
                            IsActive = true
                        };
                        db.CategoryTasks.Add(CategoryTask);
                        db.Tasks.Add(tasks);
                        db.SaveChanges();
                    }
                }
               

                return Task.FromResult(result == "" ? tasksList[0].WellDesignId : result);
            }
            catch(Exception ex)
            {
                return Task.FromResult(result);
            }

        }

        //DWOP
        public async Task<List<ChecklistTemplateModel>> ReadChecklistTemplate(string welltype)
        {
            try
            {
                List<ChecklistTemplateModel> templateList = new List<ChecklistTemplateModel>();
                if (welltype != null && welltype != "")
                {
                    var GetPlanList = db.WellType.Where(x => x.welltype_id == welltype).FirstOrDefault();
                    if (GetPlanList.DrillPlanChecklist != null)
                    {
                        templateList = JsonConvert.DeserializeObject<List<ChecklistTemplateModel>>(GetPlanList.DrillPlanChecklist);

                        foreach(var item in templateList)
                        {
                            if (item.ServiceDuration != null)
                            {
                                
                                item.ServiceDurationDays = item.ServiceDuration != null ? (item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0])) : "00";
                                item.ServiceDurationHours = item.ServiceDuration != null ? (item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])) : "00";
                                item.ServiceDurationMinutes = item.ServiceDuration != null ? (item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2])) : "00";
                            }
                            else
                            {
                                item.ServiceDuration = "00:00:00";
                            }
                        }
                    }                   
                }
                if (templateList == null || templateList.Count == 0 && (welltype == null && welltype != "Select Well Design"))
                {
                    var tasks = GetTasks1().Result;

                    templateList = (from task in tasks
                                    select new ChecklistTemplateModel
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
                                        ServiceDurationDays = task.ServiceDuration != null ? (task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0])) : "00",
                                        ServiceDurationHours = task.ServiceDuration != null ? (task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])) : "00",
                                        ServiceDurationMinutes = task.ServiceDuration != null ? (task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2])) : "00",
                                        IsActiveCategory = task.IsActiveCategory

                                    }).ToList();

                    templateList = templateList.OrderBy(x => x.StageTypeName).ThenBy(x => x.Name).ToList();
                }

                return await Task.FromResult(templateList);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask ReadChecklistTemplate", null);
                return null;
            }
        }
        #endregion

        #region WellTask Method
        public Task<bool> AddWellTask(AddWellTask input, string CreatedBy)
        {
            try
            {
                var fullWellTask = db.WellTasks.Where(x => x.WellTypeId == input.WellTypeId).ToList();
                var existingWellTask = fullWellTask.Where(x => input.TaskId.Contains(x.TaskId)).ToList();
                var deletewellTask = fullWellTask.Except(existingWellTask).ToList();
                List<WellTasks> wellTasksList = new List<WellTasks>();
                foreach (var item in input.TaskId)
                {
                    if (!existingWellTask.Where(x => x.TaskId.Contains(item)).Any())
                    {
                        WellTasks wellTasks = new WellTasks()
                        {
                            WellTaskId = Guid.NewGuid().ToString(),
                            IsActive = true,
                            CreatedBy = CreatedBy,
                            CreatedDate = DateTime.UtcNow,
                            TaskId = item,
                            WellTypeId = input.WellTypeId
                        };
                        wellTasksList.Add(wellTasks);
                    }
                }
                try
                {
                    if (wellTasksList.Count > 0)
                    {
                        db.WellTasks.AddRange(wellTasksList);
                    }
                    var deactivateId = deletewellTask.Select(x => x.WellTaskId).ToArray();
                    db.WellTasks.Where(x => deactivateId.Contains(x.WellTaskId)).ToList().ForEach(u => u.IsActive = false);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                    customErrorHandler.WriteError(ex, "IWellTask AddWellTask", null);
                    return Task.FromResult(false);
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask AddWellTask", null);
                return Task.FromResult(false);
            }
        }

        public Task<List<WellTaskModel>> GetWellTaskIdByWellTypeId(string wellTypeId)
        {
            try
            {
                var result = (from wtsk in db.WellTasks
                              where wtsk.WellTypeId.Equals(wellTypeId) && wtsk.IsActive == true
                              select new WellTaskModel()
                              {
                                  TaskId = wtsk.TaskId

                              }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask GetWellTaskIdByWellTypeId", null);
                return null;
            }
        }

        public Task<bool> IsWellTaskExist(WellTaskModel input)
        {
            try
            {
                if (db.WellTasks.Where(x => x.WellTypeId == input.WellTypeId && x.TaskId == input.TaskId).Any())
                {
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask IsWellTaskExist", null);
                return null;
            }
        }

        private Task<bool> IsWellTaskExistForUpdate(WellTaskModel input)
        {
            try
            {
                if (db.WellTasks.Where(x => x.WellTypeId == input.WellTypeId && x.WellTaskId != input.WellTaskId && x.TaskId == input.TaskId).Any())
                {
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask IsWellTaskExistForUpdate", null);
                return null;
            }
        }
        public Task<WellTaskModel> UpdateWellTask(WellTaskModel input)
        {
            try
            {
                if (IsWellTaskExistForUpdate(input).Result)
                {
                    input.ErrorMessage = "Task aleady creaded for well";
                    return Task.FromResult(input);

                }
                var wellTasks = db.WellTasks.Where(x => x.WellTaskId == input.WellTaskId).FirstOrDefault();
                wellTasks.IsActive = input.IsActive;
                wellTasks.TaskId = input.TaskId;
                wellTasks.WellTypeId = input.WellTypeId;
                db.SaveChanges();
                return Task.FromResult(input);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask UpdateWellTask", null);
                return null;
            }
        }
        #endregion

        #region Category Task Methods

        public Task<CategoryTaskModel> AddCategoryTask(CategoryTaskModel input)
        {
            try
            {
                string SubCategoryId = input.SubCategoryId == null ? input.CategoryId : input.SubCategoryId;
                if (IsNewTaskExsting(input))
                {
                    input.ErrorMessage = "Task aleady exist for Category Service";
                    return Task.FromResult(input);
                }
                CategoryTask categoryTask = new CategoryTask()
                {
                    CategoryTaskId = input.CategoryTaskId,
                    TaskId = input.TaskId,
                    CreatedBy = input.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    ServiceCategoryId = SubCategoryId
                };
                try
                {
                    db.CategoryTasks.Add(categoryTask);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    input.ErrorMessage = ex.Message;
                    return Task.FromResult(input);
                }

                return Task.FromResult(input);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask AddCategoryTask", null);
                return null;
            }
        }
        private bool IsNewTaskExsting(CategoryTaskModel input)
        {
            try
            {
                return db.CategoryTasks.Where(x => x.TaskId == input.TaskId).Any();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask IsNewTaskExsting", null);
                return false;
            }
        }
        public Task<CategoryTaskModel> UpdateCategoryTask(CategoryTaskModel input)
        {
            try
            {
                string SubCategoryId = input.SubCategoryId == null ? input.CategoryId : input.SubCategoryId;
                if (IsTaskExsting(input))
                {
                    input.ErrorMessage = "Task aleady exist for Category Service";
                    return Task.FromResult(input);
                }

                var result = db.CategoryTasks.Where(x => x.CategoryTaskId.Equals(input.CategoryTaskId)).FirstOrDefault();
                if (result == null)
                {
                    input.ErrorMessage = "Category Service Not Found";
                    return Task.FromResult(input);
                }
                else
                {
                    result.TaskId = input.TaskId;
                    result.IsActive = input.IsActive;
                    result.ServiceCategoryId = SubCategoryId;
                    db.CategoryTasks.Update(result);
                    db.SaveChanges();
                    return Task.FromResult(input);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask UpdateCategoryTask", null);
                return null;
            }
        }
        private bool IsTaskExsting(CategoryTaskModel input)
        {
            try
            {
                return db.CategoryTasks.Where(x => x.TaskId == input.TaskId && x.CategoryTaskId != x.CategoryTaskId).Any();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask IsTaskExsting", null);
                return false;
            }
        }
        public Task<List<CategoryTaskModel>> GetCategoryTask()
        {
            try
            {
                return (from cat in db.CategoryTasks
                        join serCat in db.serviceCategories on cat.ServiceCategoryId equals serCat.ServiceCategoryId
                        join task in db.Tasks on cat.TaskId equals task.TaskId
                        where serCat.IsActive == true && cat.IsActive == true
                        select new CategoryTaskModel
                        {
                            CategoryTaskId = cat.CategoryTaskId,
                            TaskName = task.Name,
                            TaskId = task.TaskId,
                            ServiceCategoryName = serCat.Name,
                            SubCategoryId = serCat.ServiceCategoryId,
                            CategoryId = serCat.ParentId,
                            IsActive = cat.IsActive
                        }).OrderBy(x => x.ServiceCategoryName).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask GetCategoryTask", null);
                return null;
            }
        }
        #endregion

        #region Well Type Methods
        public Task<List<WellTypeModel>> GetWellTypes()
        {
            try
            {
                return db.WellType.Select(x => new WellTypeModel { WellTypeId = x.welltype_id, WellTypeName = x.welltype_name }).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask GetWellTypes", null);
                return null;
            }
        }

        public Task<WellTypeModel> AddWellType(WellTypeModel input)
        {
            try
            {
                if (IsExixtWellType(input))
                {
                    input.ErrorMessage = "Well type already exist";
                    return Task.FromResult(input);
                }
                WellType wellType = new WellType
                {
                    welltype_id = input.WellTypeId,
                    welltype_name = input.WellTypeName
                };
                db.WellType.Add(wellType);
                db.SaveChanges();
                return Task.FromResult(input);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask AddWellType", null);
                return null;
            }
        }

        public Task<WellTypeModel> UpdateWellType(WellTypeModel input)
        {
            try
            {
                if (IsExixtWellType(input))
                {
                    input.ErrorMessage = "Well type already exist";
                    return Task.FromResult(input);
                }
                var result = db.WellType.Where(x => x.welltype_id == input.WellTypeId).FirstOrDefault();
                result.welltype_name = input.WellTypeName;
                db.SaveChanges();
                return Task.FromResult(input);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask UpdateWellType", null);
                return null;
            }
        }
        private bool IsExixtWellType(WellTypeModel input)
        {
            try
            {
                return db.WellType.Where(x => x.welltype_name == input.WellTypeName && x.welltype_id != input.WellTypeId).Any();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask UpdateWellType", null);
                return false;
            }
        }


        #endregion

        #region Supplies
        public Task<SuppliesModel> AddSupplies(SuppliesModel input)
        {
            try
            {
                Supplies supplies = new Supplies()
                {
                    CreatedBy = input.CreatedBy,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    SupplyId = input.SupplyId,
                    SupplyName = input.SupplyName,
                    Description = input.Description

                };
                db.Supplies.Add(supplies);
                db.SaveChanges();
                return Task.FromResult(input);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask AddSupplies", null);
                return null;
            }
        }

        public Task<SuppliesModel> UpdateSupplies(SuppliesModel input)
        {
            try
            {
                var result = db.Supplies.Where(x => x.SupplyId == input.SupplyId).FirstOrDefault();
                if (result == null)
                {
                    input.ErrorMessage = "Supplies Not found";
                }
                else
                {
                    result.IsActive = input.IsActive;
                    result.SupplyName = input.SupplyName;
                    result.Description = input.Description;
                    db.SaveChanges();
                }
                return Task.FromResult(input);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask UpdateSupplies", null);
                return null;
            }
        }

        public async Task<List<SuppliesModel>> GetSupplies()
        {
            try
            {
                return await db.Supplies.Select(x => new SuppliesModel() { IsActive = x.IsActive, SupplyId = x.SupplyId, SupplyName = x.SupplyName, Description = x.Description }).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IWellTask GetSupplies", null);
                return null;
            }
        }
        #endregion
    }
}
