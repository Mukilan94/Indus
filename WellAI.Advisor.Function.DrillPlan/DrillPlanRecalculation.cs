using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Function.DrillPlan
{
    public static class DrillPlanRecalculation
    {
        public static async Task<List<PlannedTasksModel>> ReCalculateTasks(List<PlannedTasksModel> rescheduleObj, ILogger log, PlanDetailsModel PlanDetails)
        {
            List<PlannedTasksModel> plannedTasksList = new List<PlannedTasksModel>();
            List<PlannedTasksModel> preSpudTasksList = new List<PlannedTasksModel>();
            try
            {
                var isPreSpudTasks = rescheduleObj.Where(x => x.IsPreSpud == true).OrderBy(y => y.TaskOrder).ToList();
                var otherTasks = rescheduleObj.Except(isPreSpudTasks).OrderBy(r => r.TaskOrder).ToList();

                if (isPreSpudTasks != null && otherTasks != null)
                {
                    var tasksList = await RecalcualtePlanByStartDate(await ReOrderTasksList(isPreSpudTasks,PlanDetails.PlanStartDate));

                    if(tasksList != null)
                    {
                        plannedTasksList.AddRange(tasksList);
                        preSpudTasksList.AddRange(tasksList);
                    }

                    if (otherTasks != null)
                    {
                        var benchMarkTasks = otherTasks.Where(x => x.IsBenchMark == true && x.IsPreSpud != true).OrderBy(o => o.TaskOrder).ToList();
                        if (benchMarkTasks.Count > 1)
                        {
                            for (var k = 0; benchMarkTasks.Count > k; k++)
                            {
                                if (k + 1 < benchMarkTasks.Count)
                                {
                                    var getBenchMarkTasks = new List<PlannedTasksModel>();
                                    int nextBenchMarkTaskOrder = (int)benchMarkTasks[k + 1].TaskOrder;
                                    if (k > 0)
                                    {
                                        getBenchMarkTasks = otherTasks.Where(x => x.TaskOrder >= benchMarkTasks[k].TaskOrder && x.TaskOrder < nextBenchMarkTaskOrder && x.IsPreSpud != true).OrderByDescending(o => o.IsBenchMark).ToList();
                                    }
                                    else
                                    {
                                        getBenchMarkTasks = otherTasks.Where(x => x.TaskOrder < nextBenchMarkTaskOrder && x.IsPreSpud != true).OrderByDescending(o => o.IsBenchMark).ToList();
                                    }

                                    var benchMarkTasksList = await RecalcualtePlanByStartDate(await ReOrderBenchMarkTasksList(getBenchMarkTasks, plannedTasksList.Count, PlanDetails.PlanStartDate));
                                    if (tasksList != null)
                                    {
                                        plannedTasksList.AddRange(benchMarkTasksList);
                                    }
                                }
                                else
                                {
                                    var getBenchMarkTasks = otherTasks.Where(x => x.TaskOrder >= benchMarkTasks[benchMarkTasks.Count - 1].TaskOrder && x.IsPreSpud != true).OrderByDescending(o => o.IsBenchMark).ToList();
                                    var benchMarkTasksList = await RecalcualtePlanByStartDate(await ReOrderBenchMarkTasksList(getBenchMarkTasks, plannedTasksList.Count, PlanDetails.PlanStartDate));
                                    if (tasksList != null)
                                    {
                                        plannedTasksList.AddRange(benchMarkTasksList);
                                    }
                                }
                            }
                        }
                        else if(benchMarkTasks.Count == 1)
                        {
                            var getBenchMarkTasks = otherTasks.Where(x => x.IsPreSpud != true).OrderByDescending(o => o.IsBenchMark).ToList();
                            var benchMarkTasksList = await RecalcualtePlanByStartDate(await ReOrderBenchMarkTasksList(getBenchMarkTasks, plannedTasksList.Count, PlanDetails.PlanStartDate));
                            if (tasksList != null)
                            {
                                plannedTasksList.AddRange(benchMarkTasksList);
                            }
                        }
                        else
                        {
                            plannedTasksList = await RecalcualtePlanByStartDate(await OrderTasksByDate(otherTasks, PlanDetails.PlanStartDate));
                            plannedTasksList.AddRange(preSpudTasksList);
                            //plannedTasksList = await RecalcualtePlanByStartDate(rescheduleObj);
                        }
                    }
                }               

                return await Task.FromResult(plannedTasksList);
            }
            catch (Exception ex)
            {
                //CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //customErrorHandler.WriteError(ex, "ReArrangeTasks", User.Identity.Name);
                log.LogInformation($"Recalculate - ReArrangeTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return rescheduleObj;
            }
        }
        private static async Task<List<PlannedTasksModel>> RecalcualtePlanByStartDate(List<PlannedTasksModel> PlanTasksList)
        {
            //List<PlannedTasksModel> resultObj = new List<PlannedTasksModel>();
            try
            {
                if (PlanTasksList != null)
                {
                    int i = 0;
                    foreach (var item in PlanTasksList)
                    {
                        if (i > 0)
                        {
                            var previousItem = PlanTasksList[i - 1];                       
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
                            var operationHours = operationDisplayHoursArr[0].ToString() == "" ? "00" : operationDisplayHoursArr[0].ToString();
                            if (previousItem.PlanStart != null)
                            {
                                item.PlanStart = Convert.ToDateTime(previousItem.PlanStart).AddHours(Convert.ToInt32(operationHours)).AddMinutes(Convert.ToInt32(operationMinutes));
                            }
                        }
                        i++;
                    }
                }

                return await Task.FromResult(PlanTasksList);
            }
            catch (Exception ex)
            {
                //CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //customErrorHandler.WriteError(ex, "ReScheduleTasks", User.Identity.Name);
                //log.LogInformation($"Recalculate - ReScheduleTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return PlanTasksList;
            }
        }

        public static async Task<List<PlannedTasksModel>> ReOrderTasksList(List<PlannedTasksModel> OrderTasksList, DateTime? PlanStartDate)
        {
            List<PlannedTasksModel> resultObj = new List<PlannedTasksModel>();
            try
            {
                var isExsitsPlanDate = OrderTasksList.Where(x => x.PlanStart != null).ToList();

                if(isExsitsPlanDate.Count > 0)
                {
                    var plannedTasks = OrderTasksList.OrderBy(y => y.TaskOrder).ThenBy(o => o.PlanStart).ToList();

                    int i = 0;
                    foreach (var item in plannedTasks)
                    {
                        item.TaskOrder = i + 1;
                        resultObj.Add(item);
                        i++;
                    }
                }
                else
                {
                    var plannedTasks = OrderTasksList.OrderBy(y => y.TaskOrder).ToList();
                    plannedTasks[0].PlanStart = PlanStartDate;
                    int i = 0;
                    foreach (var item in plannedTasks)
                    {
                        item.TaskOrder = i + 1;
                        resultObj.Add(item);
                        i++;
                    }
                }

               return await Task.FromResult(resultObj);
            }
            catch (Exception ex)
            {
                //CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //customErrorHandler.WriteError(ex, "ReArrangeTasks", User.Identity.Name);
                //log.LogInformation($"Recalculate - ReArrangeTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return resultObj;
            }
        }

        public static async Task<List<PlannedTasksModel>> ReOrderBenchMarkTasksList(List<PlannedTasksModel> OrderTasksList, int TaskOrderCount, DateTime? PlanStartDate)
        {
            List<PlannedTasksModel> resultObj = new List<PlannedTasksModel>();
            try
            {
                var isExsitsPlanDate = OrderTasksList.Where(x => x.PlanStart != null).OrderByDescending(o => o.IsBenchMark).ThenBy(y => y.TaskOrder).ToList();

                var PlanTasks = OrderTasksList.OrderByDescending(o => o.IsBenchMark).ThenBy(y => y.TaskOrder).ToList();

                if (OrderTasksList[0].PlanStart != null)
                {
                    //var plannedTasks = OrderTasksList.OrderBy(y => y.TaskOrder).ToList();

                    int i = 0;
                    foreach (var item in PlanTasks)
                    {
                        item.TaskOrder = TaskOrderCount+ 1;
                        resultObj.Add(item);
                        TaskOrderCount++;
                    }
                }
                else
                {
                    var plannedTasks = OrderTasksList.OrderByDescending(y => y.IsBenchMark).ThenBy(o => o.TaskOrder).ToList();
                    plannedTasks[0].PlanStart = PlanStartDate;
                    int i = 0;
                    foreach (var item in plannedTasks)
                    {
                        item.TaskOrder = TaskOrderCount + 1;
                        resultObj.Add(item);
                        TaskOrderCount++;
                    }
                }

                return await Task.FromResult(resultObj);
            }
            catch (Exception ex)
            {
                //CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                //customErrorHandler.WriteError(ex, "ReArrangeTasks", User.Identity.Name);
                //log.LogInformation($"Recalculate - ReArrangeTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return resultObj;
            }
        }

        private static async Task<List<PlannedTasksModel>> OrderTasksByDate(List<PlannedTasksModel> taskObj, DateTime? PlanStartDate)
        {
            List<PlannedTasksModel> resultObj = new List<PlannedTasksModel>();
            try
            {
                var preSpudTasks = taskObj.Where(t => t.PlanStart != null).OrderBy(o => o.PlanStart).ToList();

                var dateEmptyAssignedItems = taskObj.Where(t => t.PlanStart == null).OrderBy(t => t.TaskOrder).ToList();

                //Step 4  - Rearrange plan
                resultObj = await ReArrangeAllTasks(preSpudTasks, dateEmptyAssignedItems, PlanStartDate);

                return await Task.FromResult(resultObj);
            }
            catch (Exception ex)
            {
                return taskObj;
            }

        }

        private static async Task<List<PlannedTasksModel>> ReArrangeAllTasks(List<PlannedTasksModel> dateTasks, List<PlannedTasksModel> dateEmptyTasks, DateTime? PlanStartDate)
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
                //log.LogInformation($"Recalculate - ReArrangeAllTasks Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return dateTasks;
            }

        }

    }
}
