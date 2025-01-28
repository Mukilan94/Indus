using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using static WellAI.Advisor.Function.DrillPlan.RecalculateAndSavePlanDetails;

namespace WellAI.Advisor.Function.DrillPlan
{
    public class DrillingPlanRepository 
    {
        public static async Task<int> SaveUpdatePlandetails(PlanDetailsModel PlanDetails, string TenantId, WellAIFunctionHandlerContext db, ILogger log)
        {
            try
            {
                var Result = 0;

                DrillPlanWells drillPlanWells = new DrillPlanWells();
                DrillPlanDetails drillPlanDetails = new DrillPlanDetails();

                //Step 1  - Order the task details by Plan date in ascending
                List<PlannedTasksModel> tasksObj = PlanDetails.drillPlanTasks;
                List<PlannedTasksModel> tasksResultObj = new List<PlannedTasksModel>();

                //Step 2 - Check for Plan Start Date already assigned
                var drillWellPlans = db.DrillPlanWells.Where(p => p.Wellid == PlanDetails.WellId && p.DrillPlanId==PlanDetails.DrillingPlanId).FirstOrDefault();

                if (drillWellPlans != null)
                {
                    var planItem = db.DrillPlanDetails.Where(p => p.DrillPlanWellsId == drillWellPlans.DrillPlanWellsId && p.DrillPlanId == PlanDetails.DrillingPlanId && p.PlanStartDate != null).FirstOrDefault();

                    bool IsPlanStartDateExists = false;
                    if (planItem != null)
                    {
                        IsPlanStartDateExists = true;
                    }

                    if (IsPlanStartDateExists == false)
                    {
                        //WellAIAppContext.Current.Session.SetString("PlanStart",Convert.ToString(PlanDetails.PlanStartDate));
                        //Step 3  - Order the task details by Plan date in ascending
                        tasksResultObj = await DrillPlanRecalculation.ReCalculateTasks(tasksObj, log, PlanDetails);
                    }
                    else if (IsPlanStartDateExists == true)
                    {
                        //Step 3  - Order the task details by Plan date in ascending
                        List<PlannedTasksModel> tasksObj1 = tasksObj.OrderBy(t => t.TaskOrder).ToList();
                        //Step 4  - Rearrange plan
                        tasksResultObj = await DrillPlanRecalculation.ReCalculateTasks(tasksObj1, log,PlanDetails);
                        //tasksResultObj = await OrderTasksByDate(tasksObj, log);
                    }
                    //if (tasksResultObj != null)
                    //{
                    //    tasksResultObj = await ReScheduleTasks(tasksResultObj, log);
                    //}
                }

                PlanDetails.drillPlanTasks = tasksResultObj;

                if (PlanDetails != null)
                {
                    var wellExist = (from dw in db.DrillPlanWells
                                     join Dh in db.DrillPlanHeader on dw.DrillPlanId equals Dh.DrillPlanId
                                     where Dh.DrillPlanId == PlanDetails.DrillingPlanId && dw.Wellid == PlanDetails.WellId && Dh.TenantId == TenantId
                                     select dw).FirstOrDefault();

                    if (wellExist != null)
                    {
                        wellExist.RigRealese = PlanDetails.RigRealese;
                        wellExist.SPUDWell = PlanDetails.SpudWell;
                        wellExist.LastBOPTest = PlanDetails.LastBopTest;
                        wellExist.NextBOPTest = PlanDetails.NextBopTest;
                        wellExist.PlannedTD = PlanDetails.PlannedTD;
                        wellExist.RigId = PlanDetails.RigId;
                        db.DrillPlanWells.Update(wellExist);
                        Result = await db.SaveChangesAsync();

                        if (Result == 1)
                        {
                            List<PlannedTasksModel> Newtasks = await UpdateWelldetailsTasks(PlanDetails, wellExist, db, log);

                            if (PlanDetails.isStageUpdate == false)
                            {
                                await DrillPlanNotification(null, PlanDetails, "TasksUpdate", null, null, null, TenantId, db, log);
                            }
                            
                            if (Newtasks != null && Newtasks.Count > 0)
                            {
                                PlanDetails.drillPlanTasks = Newtasks;
                                await SaveWelldetailsTasks(PlanDetails, wellExist, db, log);
                                if (PlanDetails.isStageUpdate == false)
                                {
                                    await DrillPlanNotification(null, PlanDetails, "TasksCreate", null, null, null, TenantId, db, log);
                                }                                    
                            }

                            if (PlanDetails.DeleteTasks.Count > 0)
                            {
                                Result = await DeleteTasks(PlanDetails, wellExist, TenantId, db, log);
                            }
                        }
                    }
                    else
                    {
                        drillPlanWells = new DrillPlanWells
                        {
                            DrillPlanWellsId = Guid.NewGuid().ToString(),
                            DrillPlanId = PlanDetails.DrillingPlanId,
                            Wellid = PlanDetails.WellId,
                            RigRealese = PlanDetails.RigRealese,
                            SPUDWell = PlanDetails.SpudWell,
                            LastBOPTest = PlanDetails.LastBopTest,
                            NextBOPTest = PlanDetails.NextBopTest,
                            PlannedTD = PlanDetails.PlannedTD,
                            RigId = PlanDetails.RigId
                        };

                        await db.DrillPlanWells.AddAsync(drillPlanWells);
                        Result = db.SaveChanges();

                        if (Result == 1)
                        {
                            Result = await SaveWelldetailsTasks(PlanDetails, drillPlanWells, db, log);
                            if (PlanDetails.isStageUpdate == false)
                            {
                                await DrillPlanNotification(null, PlanDetails, "TasksCreate", null, null, null, TenantId, db, log);
                            }                                
                        }
                    }
                }

                return await Task.FromResult(Result);
            }
            catch (Exception ex)
            {
                //CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                //customErrorHandler.WriteError(ex, "CommonRepository SaveUpdatePlandetails", null);
                log.LogInformation($"Recalculate - SaveUpdatePlanDetails Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return await Task.FromResult(0);
            }
        }

        private static async Task<int> DeleteTasks(PlanDetailsModel PlanDetails, DrillPlanWells wellExist, string TenantId, WellAIFunctionHandlerContext db, ILogger log)
        {
            try
            {
                var Result = 0;
                if (PlanDetails.DeleteTasks.Count > 0)
                {
                    foreach (var task in PlanDetails.DeleteTasks)
                    {
                        var isTasksExist = db.DrillPlanDetails.Where(x => x.TaskId == task && x.DrillPlanWellsId == wellExist.DrillPlanWellsId).FirstOrDefault();
                        if (isTasksExist != null)
                        {
                            db.DrillPlanDetails.Remove(isTasksExist);
                        }
                    }
                    await DrillPlanNotification(null, PlanDetails, "TasksDelete", null, null, null, TenantId, db, log); ;
                }

                return Result = await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                //customErrorHandler.WriteError(ex, "CommonRepository SaveUpdatePlandetails", null);
                log.LogInformation($"Recalculate - DeleteTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return 0;
            }
        }

        private static async Task<List<PlannedTasksModel>> UpdateWelldetailsTasks(PlanDetailsModel PlanDetails, DrillPlanWells wellExist, WellAIFunctionHandlerContext db, ILogger log)
        {
            try
            {
                var result = 0;
                List<PlannedTasksModel> PlannedTasksModelListUpadete = new List<PlannedTasksModel>();
                List<DrillPlanDetails> drillPlanDetailsList = new List<DrillPlanDetails>();
                List<DrillPlanDetails> drillPlanDetailsListUpadete = new List<DrillPlanDetails>();

                //Step 1  - Order the task details by Plan date in ascending
                List<PlannedTasksModel> tasksObj = PlanDetails.drillPlanTasks;
                List<PlannedTasksModel> tasksResultObj = new List<PlannedTasksModel>();

                //Step 2 - Check for Plan Start Date already assigned
                //var drillWellPlans = db.DrillPlanWells.Where(p => p.Wellid == PlanDetails.WellId).FirstOrDefault();

                //if (drillWellPlans != null)
                //{
                //    var planItem = db.DrillPlanDetails.Where(p => p.DrillPlanWellsId == drillWellPlans.DrillPlanWellsId).FirstOrDefault();

                //    bool IsPlanStartDateExists = false;
                //    if (planItem != null)
                //    {
                //        IsPlanStartDateExists = true;
                //    }

                //    if (IsPlanStartDateExists == false)
                //    {
                //        //Step 3  - Order the task details by Plan date in ascending
                //        tasksResultObj = await OrderTasksByDate(tasksObj, log);
                //    }
                //    else if (IsPlanStartDateExists == true)
                //    {
                //        //Step 3  - Order the task details by Plan date in ascending
                //        List<PlannedTasksModel> tasksObj1 = tasksObj.OrderBy(t => t.TaskOrder )/*.ThenBy(t => t.TaskOrder)*/.ToList();
                //        //Step 4  - Rearrange plan
                //        tasksResultObj = await DrillPlanRecalculation.ReCalculateTasks(tasksObj1, log);
                //    }
                //    //if (tasksResultObj != null)
                //    //{
                //    //    tasksResultObj = await ReScheduleTasks(tasksResultObj, log);
                //    //}
                //}

                //PlanDetails.drillPlanTasks = tasksResultObj;

                if (PlanDetails.drillPlanTasks != null)
                {

                    foreach (var task in PlanDetails.drillPlanTasks)
                    {
                        var IsTaskexist = db.DrillPlanDetails.Where(x => x.DrillPlanWellsId == wellExist.DrillPlanWellsId && x.DrillPlanId == wellExist.DrillPlanId && x.TaskId == task.TaskId).FirstOrDefault();
                        var days = task.ServiceDurationDays == "" ? "00" : task.ServiceDurationDays == null ? "00" : task.ServiceDurationDays;
                        var hours = task.ServiceDurationHours == null ? "00" : task.ServiceDurationHours;
                        var minitus = task.ServiceDurationMinutes == null ? "00" : task.ServiceDurationMinutes;
                        var ServiceDuration = days + ":" + hours + ":" + minitus;
                        dynamic ScheduleTime = task.ScheduleTime == "" ? "00:00" : task.ScheduleTime == null ? "00:00" : task.ScheduleTime;

                        DateTime planDate = Convert.ToDateTime(task.PlanStart);

                        if (IsTaskexist != null)
                        {
                            IsTaskexist.TaskId = task.TaskId;
                            IsTaskexist.TaskName = task.TaskName;
                            IsTaskexist.EmployeeId = task.EmployeeId;
                            IsTaskexist.PlanStartDate = task.PlanStart == null ? null : planDate.Year.ToString() == "1" ? null : task.PlanStart;
                            IsTaskexist.OperationHours = (decimal?)await CalculateHours(Convert.ToInt32(days), Convert.ToInt32(hours), Convert.ToInt32(minitus), log);
                            if (IsTaskexist.PlanStartDate != null)
                            {
                                string[] hoursArray = Convert.ToString(IsTaskexist.OperationHours).Split(".");
                                int mins = 0;
                                if (hoursArray != null)
                                {
                                    if (hoursArray.Length > 0)
                                    {
                                        if (hoursArray.Length == 2)
                                        {
                                            mins = Convert.ToInt32(hoursArray[1]);

                                            if(mins.ToString().Length == 1)
                                            {
                                                var value = mins.ToString() + "0";
                                                mins = Convert.ToInt32(value);
                                            }
                                            mins = mins == 15 ? mins : mins == 30 ? mins : mins == 45 ? mins : 00;
                                        }
                                        else if (hoursArray.Length == 1)
                                        {
                                            mins = Convert.ToInt32("00");
                                        }
                                   
                                    }
                                }

                                IsTaskexist.PlanFinishedDate = Convert.ToDateTime(task.PlanStart).AddHours(Convert.ToInt32(hoursArray[0])).AddMinutes(mins);
                            }
                            else
                            {
                                IsTaskexist.PlanFinishedDate = task.PlanFinishedDate == null ? null : task.PlanFinishedDate;
                            }
                            if (task.TaskId == "")
                            {
                                IsTaskexist.IsPlanTask = true;
                            }
                            IsTaskexist.StageId = task.StageType;
                            IsTaskexist.CategoryId = task.ServiceCategoryId;
                            IsTaskexist.ServiceDuration = ServiceDuration;
                            IsTaskexist.ScheduleTime = (TimeSpan?)TimeSpan.Parse(ScheduleTime);
                            IsTaskexist.Day = task.Day;
                            IsTaskexist.Dependency = task.SeletedDependency;
                            IsTaskexist.Depth = task.Depth;
                            IsTaskexist.Description = task.Description;
                            IsTaskexist.LeadTime = task.LeadTime;
                            IsTaskexist.TaskOrder = task.TaskOrder;

                            //if(task.ActualPlanStart != null && IsTaskexist.ActualStartDate == null)
                            if (task.ActualPlanStart != null)
                            {
                                IsTaskexist.ActualStartDate = task.ActualPlanStart == null ? null : task.ActualPlanStart;
                            }
                            //if (task.ActualPlanFinishedDate != null && IsTaskexist.ActualFinishedDate == null)
                            if (task.ActualPlanFinishedDate != null)// && IsTaskexist.ActualFinishedDate == null)
                            {
                                IsTaskexist.ActualFinishedDate = task.ActualPlanFinishedDate == null ? null : task.ActualPlanFinishedDate;
                            }
                               
                            IsTaskexist.ServiceOperatorId = task.Vendor;
                            IsTaskexist.IsBiddable = task.IsBiddable;
                            IsTaskexist.Comments = task.commands;
                            IsTaskexist.IsPreSpud = task.IsPreSpud;
                            IsTaskexist.IsBenchMark = task.IsBenchMark;
                            IsTaskexist.IsSpecialServices = task.IsSpecialServices;

                            drillPlanDetailsListUpadete.Add(IsTaskexist);
                            PlannedTasksModelListUpadete.Add(task);
                        }

                    }

                    db.DrillPlanDetails.UpdateRange(drillPlanDetailsListUpadete);
                    await SaveMasterTasks(PlannedTasksModelListUpadete, db);
                }
                //DrillPlanHeader planHeader = new DrillPlanHeader();
                //planHeader.PlanStartDate = tasksResultObj.Select(s => s.PlanStart).FirstOrDefault();
                //planHeader.DrillPlanId = PlanDetails.DrillingPlanId;
                //db.DrillPlanHeader.Update(planHeader);


                    var headerResult = db.DrillPlanHeader.SingleOrDefault(b => b.DrillPlanId == PlanDetails.DrillingPlanId);
                    if (headerResult != null)
                    {
                        DateTime planDate = Convert.ToDateTime(tasksResultObj.Select(s => s.PlanStart).FirstOrDefault());
                            if (planDate.Year!=1)
                                headerResult.PlanStartDate = tasksResultObj.Select(s => s.PlanStart).FirstOrDefault();
                        //db.SaveChanges();
                    }

                await db.SaveChangesAsync();

                DateTime?[] planStartDates = new DateTime?[2];
                DateTime?[] planCompletedDates = new DateTime?[2];

                planStartDates = GetPlanStartDate(PlanDetails.DrillingPlanId, db, log);
                planCompletedDates = GetPlanCompletedDate(PlanDetails.DrillingPlanId, db, log);

                var Plan = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();

                if (Plan != null)
                {
                    Plan.DrillPlanName = Plan.DrillPlanName;
                    Plan.PlanStartDate = Plan.PlanStartDate;
                    Plan.Prediction = Plan.Prediction;
                    Plan.PlanLastModifyDate = DateTime.Now;
                    Plan.PlanStartDate = planStartDates[0];
                    Plan.ActualStartDate = planStartDates[1];
                    Plan.PlanCompleteDate = planCompletedDates[0];
                    Plan.ActualCompletedDate = planCompletedDates[1];

                    db.DrillPlanHeader.Update(Plan);
                    //await db.SaveChangesAsync();
                }


                result = await db.SaveChangesAsync();

                return await Task.FromResult(PlanDetails.drillPlanTasks.Except(PlannedTasksModelListUpadete).ToList());

            }
            catch (Exception ex)
            {
                //CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                //customErrorHandler.WriteError(ex, "CommonRepository UpdateWelldetailsTasks", null);
                log.LogInformation($"Recalculate - UpdateWellDetailsTask Error Message : {DateTime.Now}, Message : {ex.InnerException.ToString()} ");
                return null;
            }
        }

        private static DateTime? GetPlanFinshDate(PlannedTasksModel PlanTasks)
        {
            try
            {
                PlanTasks.PlanFinishedDate = null;

                if (PlanTasks.PlanStart != null)
                {
                    string[] hoursArray = Convert.ToString(PlanTasks.OperationHours).Split(".");
                    int mins = 0;
                    if (hoursArray != null)
                    {
                        if (hoursArray.Length > 0)
                        {
                            if (hoursArray.Length == 2)
                            {
                                mins = Convert.ToInt32(hoursArray[1]);

                                if (mins.ToString().Length == 1)
                                {
                                    var value = mins.ToString() + "0";
                                    mins = Convert.ToInt32(value);

                                }

                                mins = mins == 15 ? mins : mins == 30 ? mins : mins == 45 ? mins : 00;
                            }
                            else if (hoursArray.Length == 1)
                            {
                                mins = Convert.ToInt32("00");
                            }

                        }
                    }

                    PlanTasks.PlanFinishedDate = Convert.ToDateTime(PlanTasks.PlanStart).AddHours(Convert.ToInt32(hoursArray[0])).AddMinutes(mins);
                }
                else
                {
                    PlanTasks.PlanFinishedDate = PlanTasks.PlanFinishedDate == null ? null : PlanTasks.PlanFinishedDate;
                }

                return PlanTasks.PlanFinishedDate;
            }
            catch(Exception ex)
            {
                return PlanTasks.PlanFinishedDate;
            }
        }

        public static async Task<int> SaveWelldetailsTasks(PlanDetailsModel PlanDetails, DrillPlanWells wellExist, WellAIFunctionHandlerContext db, ILogger log)
        {
            try
            {
                var result = 0;

                if (PlanDetails.drillPlanTasks != null)
                {
                    DrillPlanDetails drillPlanDetails = new DrillPlanDetails();

                    List<DrillPlanDetails> drillPlanDetailsList = new List<DrillPlanDetails>();
                                        
                    foreach (var task in PlanDetails.drillPlanTasks)
                    {
                        var days = task.ServiceDurationDays == "" ? "00" : task.ServiceDurationDays == null ? "00" : task.ServiceDurationDays;
                        var hours = task.ServiceDurationHours == null ? "00" : task.ServiceDurationHours;
                        var minitus = task.ServiceDurationMinutes == null ? "00" : task.ServiceDurationMinutes;
                        var ServiceDuration = days + ":" + hours + ":" + minitus;
                        dynamic ScheduleTime = task.ScheduleTime == "" ? "00:00" : task.ScheduleTime == null ? "00:00" : task.ScheduleTime;

                        DateTime planDate = Convert.ToDateTime(task.PlanStart);

                        drillPlanDetails = new DrillPlanDetails
                        {
                            DrillPlanDetailsId = Guid.NewGuid().ToString(),
                            DrillPlanId = PlanDetails.DrillingPlanId,
                            DrillPlanWellsId = wellExist.DrillPlanWellsId,
                            TaskId = task.TaskId == null ? Guid.NewGuid().ToString() : task.TaskId == "" ? Guid.NewGuid().ToString() : task.TaskId,
                            TaskName = task.TaskName,
                            EmployeeId = task.EmployeeId,
                            PlanStartDate = task.PlanStart == null ? null : planDate.Year.ToString() == "1" ? null : task.PlanStart,
                            OperationHours = (decimal?)await CalculateHours(Convert.ToInt32(days), Convert.ToInt32(hours), Convert.ToInt32(minitus), log),
                            PlanFinishedDate = GetPlanFinshDate(task),
                            IsPlanTask = task.TaskId == "" ? true : false,
                            StageId = task.StageType,
                            CategoryId = task.ServiceCategoryId,
                            ServiceDuration = ServiceDuration,
                            LeadTime = task.LeadTime,
                            IsSpecialServices = Convert.ToByte(task.IsSpecialServices),
                            ScheduleTime = (TimeSpan?)TimeSpan.Parse(ScheduleTime),
                            Dependency = task.SeletedDependency,
                            Description = task.Description,
                            Day = task.Day,
                            CreatedDate = DateTime.Now,
                            Depth = task.Depth,
                            ActualStartDate = task.ActualPlanStart == null ? null : task.ActualPlanStart,
                            ActualFinishedDate = task.ActualPlanFinishedDate == null ? null : task.ActualPlanFinishedDate,
                            ServiceOperatorId = task.Vendor,
                            Comments = task.commands,
                            TaskOrder = task.TaskOrder,
                            IsBiddable = task.IsBiddable,
                            IsBenchMark = task.IsBenchMark,
                            IsPreSpud = task.IsPreSpud
                        };

                        drillPlanDetailsList.Add(drillPlanDetails);
                    }
                    db.DrillPlanDetails.AddRange(drillPlanDetailsList);
                    await SaveMasterTasks(PlanDetails.drillPlanTasks, db);
                }

                await db.SaveChangesAsync();

                DateTime?[] planStartDates = new DateTime?[2];
                DateTime?[] planCompletedDates = new DateTime?[2];

                planStartDates = GetPlanStartDate(PlanDetails.DrillingPlanId, db, log);
                planCompletedDates = GetPlanCompletedDate(PlanDetails.DrillingPlanId, db, log);

                var Plan = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();

                if (Plan != null)
                {
                    Plan.DrillPlanName = Plan.DrillPlanName;
                    Plan.PlanStartDate = Plan.PlanStartDate;
                    Plan.Prediction = Plan.Prediction;
                    Plan.PlanLastModifyDate = DateTime.Now;
                    Plan.PlanStartDate = planStartDates[0];
                    Plan.ActualStartDate = planStartDates[1];
                    Plan.PlanCompleteDate = planCompletedDates[0];
                    Plan.ActualCompletedDate = planCompletedDates[1];

                    db.DrillPlanHeader.Update(Plan);
                    //await db.SaveChangesAsync();
                }

                return result = await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                //CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                //customErrorHandler.WriteError(ex, "CommonRepository SaveWelldetailsTasks", null);
                log.LogInformation($"Recalculate - SaveWellDetailsTask Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return 0;
            }
        }

        private static DateTime?[] GetPlanStartDate(string drillingPlanId, WellAIFunctionHandlerContext db, ILogger log)
        {
            DateTime? planStartDate = null;
            DateTime? planActualStartDate = null;
            DateTime?[] plandates = new DateTime?[2];
            try
            {
                //Iterate on all PlanDetails's first row and get the least Plan Start Date, Actual Start
                var planDetailFirstRows = (from dp in db.DrillPlanDetails
                                           where dp.DrillPlanId == drillingPlanId
                                           group dp by dp.DrillPlanWellsId into g
                                           select new DrillPlanDetailsModel
                                           {
                                               DrillPlanWellsId = g.Key,
                                               TaskOrder = g.Min(a => a.TaskOrder)
                                           }).ToList();
                if (planDetailFirstRows != null)
                {
                    foreach (var item in planDetailFirstRows)
                    {
                        var planDetails = (from dp in db.DrillPlanDetails
                                           where dp.DrillPlanWellsId == item.DrillPlanWellsId && dp.TaskOrder == item.TaskOrder
                                           select new DrillPlanDetailsModel
                                           {
                                               PlanStartDate = dp.PlanStartDate,
                                               ActualStartDate = dp.ActualStartDate
                                           }).FirstOrDefault();
                        if (planDetails != null)
                        {
                            //plan start date
                            if (planStartDate != null && planDetails.PlanStartDate != null)
                            {
                                if (planDetails.PlanStartDate < planStartDate)
                                {
                                    planStartDate = planDetails.PlanStartDate;
                                }
                            }
                            else if (planStartDate == null && planDetails.PlanStartDate != null)
                            {
                                planStartDate = planDetails.PlanStartDate;
                            }
                            //actual start date
                            if (planActualStartDate != null && planDetails.ActualStartDate != null)
                            {
                                if (planDetails.ActualStartDate < planActualStartDate)
                                {
                                    planActualStartDate = planDetails.ActualStartDate;
                                }
                            }
                            else if (planActualStartDate == null && planDetails.ActualStartDate != null)
                            {
                                planActualStartDate = planDetails.ActualStartDate;
                            }

                        }
                    }

                   
                    plandates[0] = planStartDate;
                    plandates[1] = planActualStartDate;                    
                }
                return plandates;
            }
            catch(Exception ex)
            {
                log.LogInformation($"Recalculate - GetPlanStartDateAndActualStartDates Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return plandates;
            }
        }

        private static DateTime?[] GetPlanCompletedDate(string drillingPlanId, WellAIFunctionHandlerContext db, ILogger log)
        {
            DateTime? planFinishedDate = null;
            DateTime? actualFinishedDate = null;
            DateTime?[] plandates = new DateTime?[2];
            try
            {
                 var planDetailLastRows = (from dp in db.DrillPlanDetails
                                          where dp.DrillPlanId == drillingPlanId
                                          group dp by dp.DrillPlanWellsId into g
                                          select new DrillPlanDetailsModel
                                          {
                                              DrillPlanWellsId = g.Key,
                                              TaskOrder = g.Max(a => a.TaskOrder)
                                          }).ToList();

                //Iterate on all PlanDetails's Max row and get the max Plan Complated Date, Actual Completed Date
                if (planDetailLastRows != null)
                {
                    foreach (var item in planDetailLastRows)
                    {
                        var planDetails = (from dp in db.DrillPlanDetails
                                           where dp.DrillPlanWellsId == item.DrillPlanWellsId && dp.TaskOrder == item.TaskOrder
                                           select new DrillPlanDetailsModel
                                           {
                                               PlanFinishedDate = dp.PlanFinishedDate,
                                               ActualFinisheDate = dp.ActualFinishedDate
                                           }).FirstOrDefault();
                        if (planDetails != null)
                        {
                            //plan start date
                            if (planFinishedDate != null && planDetails.PlanFinishedDate != null)
                            {
                                if (planDetails.PlanFinishedDate > planFinishedDate)
                                {
                                    planFinishedDate = planDetails.PlanFinishedDate;
                                }
                            }
                            else if (planFinishedDate == null && planDetails.PlanFinishedDate != null)
                            {
                                planFinishedDate = planDetails.PlanFinishedDate;
                            }
                            //actual start date
                            if (actualFinishedDate != null && planDetails.ActualFinisheDate != null)
                            {
                                if (planDetails.ActualFinisheDate > actualFinishedDate)
                                {
                                    actualFinishedDate = planDetails.ActualFinisheDate;
                                }
                            }
                            else if (actualFinishedDate == null && planDetails.ActualFinisheDate != null)
                            {
                                actualFinishedDate = planDetails.ActualFinisheDate;
                            }

                        }
                    }
                    //set plan finished and actualfinished as null even if not having values in single well
                    foreach (var item in planDetailLastRows)
                    {
                        var planCompletedDetails = (from dp in db.DrillPlanDetails
                                       where dp.DrillPlanWellsId == item.DrillPlanWellsId && dp.TaskOrder == item.TaskOrder
                                       select new DrillPlanDetailsModel
                                       {
                                           PlanFinishedDate = dp.PlanFinishedDate,
                                           ActualFinisheDate = dp.ActualFinishedDate
                                       }).FirstOrDefault();

                        if (planCompletedDetails != null)
                        {
                            if (planCompletedDetails.PlanFinishedDate == null)
                            {
                                planFinishedDate = null;
                            }                            
                        }
                        if (planCompletedDetails != null)
                        {
                            if (planCompletedDetails.ActualFinisheDate == null)
                            {
                                actualFinishedDate = planCompletedDetails.ActualFinisheDate;
                            }                                
                        }
                    }
                    plandates[0] = planFinishedDate;
                    plandates[1] = actualFinishedDate;
                }
                return plandates;
            }
            catch (Exception ex)
            {
                log.LogInformation($"Recalculate - GetPlanCompletedDates Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return plandates;
            }
        }

        //private static async Task<float> CalculateHours(int days, int hours, int minutes,ILogger log)
        //{
        //    float result = 00;
        //    try
        //    {
        //        int calDays = days != 00 ? days * 24 : 00;
        //        int calMinutes = minutes != 00 ? ((minutes / 100) * 60) / 100 : 00;
        //        result = calDays + hours + calMinutes;
        //        return await Task.FromResult(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        //CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
        //        //customErrorHandler.WriteError(ex, "CommonRepository CalculateHours", null);
        //        log.LogInformation($"Recalculate - CalculateHours Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
        //        return result;
        //    }
        //}

        private static async Task<List<PlannedTasksModel>> OrderTasksByDate(List<PlannedTasksModel> taskObj, ILogger log)
        {
            List<PlannedTasksModel> resultObj = new List<PlannedTasksModel>();
            try
            {
                var dateAssignedItems = taskObj.Where(t => t.PlanStart != null).OrderBy(t => t.PlanStart).ToList();

                var dateEmptyAssignedItems = taskObj.Where(t => t.PlanStart == null).OrderBy(t => t.TaskOrder).ToList();

                //Step 4  - Rearrange plan
                resultObj = await ReArrangeAllTasks(dateAssignedItems, dateEmptyAssignedItems, log);

                return await Task.FromResult(resultObj);
            }
            catch (Exception ex)
            {
                //CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //customErrorHandler.WriteError(ex, "ReArrangeTasks", User.Identity.Name);
                log.LogInformation($"Recalculate - OrderTasksByDate Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return taskObj;
            }

        }

        private static async Task<List<PlannedTasksModel>> ReArrangeTasks(List<PlannedTasksModel> rescheduleObj, ILogger log)
        {
            List<PlannedTasksModel> resultObj = new List<PlannedTasksModel>();
            try
            {
                var NewTask = rescheduleObj.Where(x => x.PlanStart == null).OrderBy(y => y.TaskOrder).ToList();
                var PlanStartTasks = rescheduleObj.Except(NewTask).OrderBy(r => r.TaskOrder).ToList();

                if (NewTask != null)
                {
                    int i = 0;
                    foreach (var item in PlanStartTasks)
                    {
                        item.TaskOrder = i + 1;
                        resultObj.Add(item);
                        i++;
                    }
                    int j = i;
                    foreach (var item in NewTask)
                    {
                        item.TaskOrder = j + 1;
                        resultObj.Add(item);
                        j++;
                    }
                }
                else
                {
                    int i = 0;
                    foreach (var item in rescheduleObj)
                    {
                        item.TaskOrder = i + 1;
                        i++;
                    }

                    resultObj = rescheduleObj;
                }
                return await Task.FromResult(resultObj);
            }
            catch (Exception ex)
            {
                //CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //customErrorHandler.WriteError(ex, "ReArrangeTasks", User.Identity.Name);
                log.LogInformation($"Recalculate - ReArrangeTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return rescheduleObj;
            }
        }

        private static async Task<List<PlannedTasksModel>> ReScheduleTasks(List<PlannedTasksModel> rescheduleObj, ILogger log)
        {
            //List<PlannedTasksModel> resultObj = new List<PlannedTasksModel>();
            try
            {
                int i = 0;
                foreach (var item in rescheduleObj)
                {
                    if (i > 0)
                    {
                        var previousItem = rescheduleObj[i - 1];
                        //item.PlanStart = Convert.ToDateTime(previousItem.PlanStart).AddHours(Convert.ToInt32(previousItem.OperationHours));
                        //decimal? hoursValue = previousItem.OperationHours;
                        //string hoursString = hoursValue.ToString();
                        //int indexOfDecimal = hoursString.IndexOf(".");
                        var operationDisplayHoursArr = previousItem.OperationHours.ToString("#.##").Split(".");
                        var operationMinutes = "";
                        if (operationDisplayHoursArr.Length > 1)
                        {
                            if (Convert.ToString(operationDisplayHoursArr[1]).Length == 1 || operationDisplayHoursArr[1].ToString() == "0")
                            {
                                operationMinutes = operationDisplayHoursArr[1].ToString() + "0";
                            }
                            else
                            {
                                operationMinutes = operationDisplayHoursArr[1].ToString();
                            }                            
                        }
                        else
                        {
                            operationMinutes = "00";
                        }
                        var operationHours = operationDisplayHoursArr[0].ToString()=="" ? "00" : operationDisplayHoursArr[0].ToString();
                        if (previousItem.PlanStart != null)
                        {
                            item.PlanStart = Convert.ToDateTime(previousItem.PlanStart).AddHours(Convert.ToInt32(operationHours)).AddMinutes(Convert.ToInt32(operationMinutes));
                        }                       
                    }
                    i++;
                }
                return await Task.FromResult(rescheduleObj);
            }
            catch (Exception ex)
            {
                //CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //customErrorHandler.WriteError(ex, "ReScheduleTasks", User.Identity.Name);
                log.LogInformation($"Recalculate - ReScheduleTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return rescheduleObj;
            }
        }

        private static async Task<List<PlannedTasksModel>> ReArrangeAllTasks(List<PlannedTasksModel> dateTasks, List<PlannedTasksModel> dateEmptyTasks, ILogger log)
        {
            List<PlannedTasksModel> resultObj = new List<PlannedTasksModel>();
            try
            {
                int i = 0;
                //resultObj.Add()
                foreach (var item in dateTasks)
                {
                    i = i + 1;
                    item.TaskOrder = i;
                    resultObj.Add(item);
                }
                //resultObj.Add()
                foreach (var item in dateEmptyTasks)
                {
                    i = i + 1;
                    item.TaskOrder = i;
                    resultObj.Add(item);
                }

                return await Task.FromResult(resultObj);
            }
            catch (Exception ex)
            {
                //CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //customErrorHandler.WriteError(ex, "ReArrangeTasks", User.Identity.Name);
                log.LogInformation($"Recalculate - ReArrangeAllTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return dateTasks;
            }

        }

        public static async Task<float> CalculateHours(int days, int hours, int minutes, ILogger log)
        {
            float result = 00;
            try
            {
                //int calDays = days != 00 ? days * 24 : 00;
                //float calMinutes = minutes != 00 ? (((float)minutes / (float)100) * (float)60) : 00;
                //result = calDays + hours + ((float)calMinutes / (float)100);
                //return await Task.FromResult(result);
                int calDays = days != 00 ? days * 24 : 00;
                string calMinutes = (minutes / 100.00).ToString("0.00");
                result = calDays + hours + (float)Math.Round(Convert.ToDouble(calMinutes), 2); //15.30
                result = (float)Math.Round(result, 2);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                log.LogInformation($"Recalculate - CalculateHours Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return result;
            }
        }

        public static async Task<int> DrillPlanNotification(DrillPlanDetails PlanTasks, PlanDetailsModel PlanDetails, string Status, DrillPlanHeader DrillPlanHeader, DrillPlanWells DrillPlanWells, DrillPlandetailsViewModel DrillPlan, string TenantId, WellAIFunctionHandlerContext db, ILogger log)
        {
            try
            {

                //DrillPlanHeader.DrillPlanId
                //PlanDetails.DrillingPlanId,
                DrillPlanWells Rigs = new DrillPlanWells();
                if (PlanDetails != null)
                {
                    if (PlanDetails.DrillingPlanId != "")
                    {
                        Rigs = db.DrillPlanWells.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();
                    }
                }
                else
                {
                    if (DrillPlanHeader != null)
                    {
                        if (DrillPlanHeader.DrillPlanId != "")
                        {
                            Rigs = db.DrillPlanWells.Where(x => x.DrillPlanId == DrillPlanHeader.DrillPlanId).FirstOrDefault();
                        }
                    }
                }
                List<string> userRig = new List<string>();
                if (Rigs.RigId != "")
                {
                    userRig = db.UserRigs.Where(x => x.RigID == Rigs.RigId).Select(s => s.UserId).ToList();
                }

                int Result = 0;
                MessageQueue MessageQueue = new MessageQueue();
                //var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var Operator = db.CorporateProfile.Where(x => x.TenantId == TenantId).FirstOrDefault();


                if (Status == "AssignedTasks")
                {
                    var Vendor = db.CorporateProfile.Where(x => x.TenantId == PlanTasks.ServiceOperatorId).FirstOrDefault();
                    var PlanName = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();

                    if (Vendor != null)
                    {
                        var Well = db.WellRegister.Where(x => x.well_id == PlanDetails.WellId).FirstOrDefault();

                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = Vendor.UserId,
                            Type = 10,
                            IsActive = 1,
                            EntityId = PlanDetails.DrillingPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan :  " + PlanName.DrillPlanName + ", " + "Well Name : " + Well.wellname + ", " + "Operator Name : " + Operator.Name + ", " + "Task Name : " + PlanTasks.TaskName + ", " + "Task Date & Time : " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),
                            CreatedDate = DateTime.Now
                        };

                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }

                }//group notification
                else if (Status == "DrillPlanCreate" && userRig.Count > 0)
                {
                    var OperatorUsers = db.WellIdentityUser.Where(x => userRig.Contains(x.Id)).ToList();
                    List<string> Wellnames = new List<string>();
                    foreach (var well in DrillPlan.WellId)
                    {
                        Wellnames.Add(db.WellRegister.Where(x => x.well_id == well).Select(y => y.wellname).FirstOrDefault());
                    }

                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = DrillPlanHeader.DrillPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Created On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + DrillPlanHeader.DrillPlanName + ", " + "Wells Added : " + string.Join(",", Wellnames),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }

                }
                else if (Status == "DrillPlanUpdate" && userRig.Count > 0)
                {
                    //var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var OperatorUsers = db.WellIdentityUser.Where(x => userRig.Contains(x.Id)).ToList();
                    List<string> Wellnames = new List<string>();
                    foreach (var well in DrillPlan.WellId)
                    {
                        Wellnames.Add(db.WellRegister.Where(x => x.well_id == well).Select(y => y.wellname).FirstOrDefault());
                    }

                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = DrillPlanHeader.DrillPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Modified On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + DrillPlanHeader.DrillPlanName + ", " + "Wells Updated  : " + string.Join(",", Wellnames),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }


                }
                else if (Status == "DrillPlanWellRemove" && userRig.Count > 0)
                {
                    //var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var OperatorUsers = db.WellIdentityUser.Where(x => userRig.Contains(x.Id)).ToList();

                    var Wellnames = db.WellRegister.Where(x => x.well_id == DrillPlanWells.Wellid).Select(y => y.wellname).FirstOrDefault();

                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = DrillPlanHeader.DrillPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Modified On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + DrillPlanHeader.DrillPlanName + ", " + "Wells Deleted : " + Wellnames,
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }

                }
                else if (Status == "TasksCreate" && userRig.Count > 0)
                {
                    //var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var OperatorUsers = db.WellIdentityUser.Where(x => userRig.Contains(x.Id)).ToList();
                    var Wellnames = db.WellRegister.Where(x => x.well_id == PlanDetails.WellId).Select(y => y.wellname).FirstOrDefault();
                    var PlanName = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();
                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = PlanDetails.DrillingPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Tasks Created On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + PlanName.DrillPlanName + ", " + "Well : " + Wellnames + ", " + "Tasks Added : " + PlanDetails.drillPlanTasks.Count(),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }
                }
                else if (Status == "TasksUpdate" && userRig.Count > 0)
                {
                    //var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var OperatorUsers = db.WellIdentityUser.Where(x => userRig.Contains(x.Id)).ToList();
                    var Wellnames = db.WellRegister.Where(x => x.well_id == PlanDetails.WellId).Select(y => y.wellname).FirstOrDefault();
                    var PlanName = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();

                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = PlanDetails.DrillingPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Tasks Updated On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + PlanName.DrillPlanName + ", " + "Well : " + Wellnames + ", " + "Tasks Updated : " + PlanDetails.drillPlanTasks.Count(),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }
                }
                else if (Status == "TasksDelete" && userRig.Count > 0)
                {
                    //var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var OperatorUsers = db.WellIdentityUser.Where(x => userRig.Contains(x.Id)).ToList();
                    var Wellnames = db.WellRegister.Where(x => x.well_id == PlanDetails.WellId).Select(y => y.wellname).FirstOrDefault();
                    var PlanName = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();

                    List<string> TasksName = new List<string>();
                    foreach (var task in PlanDetails.DeleteTasks)
                    {
                        TasksName.Add(db.DrillPlanDetails.Where(x => x.TaskId == task && x.DrillPlanId == PlanDetails.DrillingPlanId).Select(y => y.TaskName).FirstOrDefault());
                    }
                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = PlanDetails.DrillingPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Tasks Deleted On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + PlanName.DrillPlanName + ", " + "Well : " + Wellnames + ", " + "Tasks Deleted : " + string.Join(",", TasksName),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }
                }
                else if (Status == "DrillPlanDelete" && userRig.Count > 0)
                {
                    //var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var OperatorUsers = db.WellIdentityUser.Where(x => userRig.Contains(x.Id)).ToList();
                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = null,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Deleted On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + DrillPlanHeader.DrillPlanName,
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                       
                    }
                }

                return Result;
            }
            catch (Exception ex)
            {
                //CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                ///customErrorHandler.WriteError(ex, "CommonRepository DrillPlanNotification", null);
                return 0;
            }
        }

        public static async Task<int> SaveMasterTasks(List<PlannedTasksModel> drillPlanDetailsModel, WellAIFunctionHandlerContext db)
        {
            int result = 0;
            try
            {
                foreach (var taskModel in drillPlanDetailsModel)
                {
                    if (taskModel.ExportToMaster)
                    {
                        var TasksExits = db.Tasks.Where(x => x.TaskId == taskModel.TaskId).FirstOrDefault();

                        if (TasksExits != null)
                        {
                            continue;
                        }
                        taskModel.TaskId = taskModel.TaskId == "" ? Guid.NewGuid().ToString() : taskModel.TaskId;

                        Tasks tasks = new Tasks()
                        {
                            //CreatedBy = taskModel.,
                            CreatedDate = DateTime.UtcNow,
                            Description = taskModel.Description,
                            IsActive = true,
                            Day = taskModel.Day,
                            Dependency = taskModel.SeletedDependency,
                            Depth = taskModel.Depth,
                            LeadTime = taskModel.LeadTime,
                            ScheduleTime = (TimeSpan?)TimeSpan.Parse(taskModel.ScheduleTime),
                            Name = taskModel.TaskName,
                            IsSpecialServices = Convert.ToInt32(taskModel.IsSpecialServices),
                            TaskId = taskModel.TaskId,
                            IsBiddable = (bool)taskModel.IsBiddable,
                            StageType = taskModel.StageType,
                            ServiceDuration = taskModel.ServiceDuration
                        };

                        CategoryTask CategoryTask = new CategoryTask
                        {
                            CategoryTaskId = Guid.NewGuid().ToString(),
                            ServiceCategoryId = taskModel.ServiceCategoryId,
                            TaskId = taskModel.TaskId,
                            //CreatedBy = userId,
                            CreatedDate = DateTime.UtcNow,
                            IsActive = true
                        };
                        db.CategoryTask.Add(CategoryTask);
                        db.Tasks.Add(tasks);
                        result = db.SaveChanges();
                    }
                }

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
