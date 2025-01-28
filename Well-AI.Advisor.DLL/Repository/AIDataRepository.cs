using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.DLL.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace WellAI.Advisor.DLL.Repository
{
    public class AIDataRepository : IAIDataRepository
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public AIDataRepository(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            //User = (UserManager<WellIdentityUser>)userManager.Users;
        }

        public IEnumerable<AIWellDataModel> GetRIGAIResult(string wellId)
        {
            try
            {
                List<AIWellDataModel> AIWellDataModelList = new List<AIWellDataModel>();

                var associatedList = db.AIAssociatedTasks.AsNoTracking();
                var predictiveList = db.AIPredictiveTasks.AsNoTracking();
                var exemptionlist = db.AIExemptionTasks.AsNoTracking();

                var associatedtasks = (from ai in associatedList
                                       join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                       from subresult in tj.DefaultIfEmpty()
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                       from subresult1 in wj.DefaultIfEmpty()
                                       where ai.well_id.Equals(wellId)
                                       select new AIWellDataModel
                                       {
                                           actionDate = ai.ActionDate,
                                           adt = ai.ADT,
                                           customerId = ai.customer_id,
                                           dependency = ai.dependency,
                                           dependencyFlag = ai.dependency_flag,
                                           depth = ai.depth,
                                           duration = ai.duration,
                                           eFlag = ai.Eflag,
                                           leadTime = ai.leadtime,
                                           scheduleDate = ai.ScheduleDate,
                                           startTime = ai.StartTime,
                                           taskName = ai.taskname,
                                           taskStatus = ai.taskstatus,
                                           time = ai.time,
                                           wellTaskId = ai.welltask_id,
                                           wellTypeId = ai.welltype_id,
                                           wellId = ai.well_id,
                                           dependencyTask = subresult.taskname ?? String.Empty,
                                           wellName = subresult1.wellname ?? String.Empty,
                                       }).ToList();

                var predictiveTasks = (from ai in predictiveList
                                       join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                       from subresult in tj.DefaultIfEmpty()
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                       from subresult1 in wj.DefaultIfEmpty()
                                       where ai.well_id.Equals(wellId)
                                       select new AIWellDataModel
                                       {
                                           actionDate = ai.ActionDate,
                                           adt = ai.ADT,
                                           customerId = ai.customer_id,
                                           dependency = ai.dependency,
                                           dependencyFlag = ai.dependency_flag,
                                           depth = ai.depth,
                                           duration = ai.duration,
                                           eFlag = ai.Eflag,
                                           leadTime = ai.leadtime,
                                           scheduleDate = ai.ScheduleDate,
                                           startTime = ai.StartTime,
                                           taskName = ai.taskname,
                                           taskStatus = ai.taskstatus,
                                           time = ai.time,
                                           wellTaskId = ai.welltask_id,
                                           wellTypeId = ai.welltype_id,
                                           wellId = ai.well_id,
                                           dependencyTask = subresult.taskname ?? String.Empty,
                                           wellName = subresult1.wellname ?? String.Empty,
                                       }).ToList();

                var exemptionTasks = (from ai in exemptionlist
                                      join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                      from subresult in tj.DefaultIfEmpty()
                                      join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                      from subresult1 in wj.DefaultIfEmpty()
                                      where ai.well_id.Equals(wellId)
                                      select new AIWellDataModel
                                      {
                                          actionDate = ai.ActionDate,
                                          adt = ai.ADT,
                                          customerId = ai.customer_id,
                                          dependency = ai.dependency,
                                          dependencyFlag = ai.dependency_flag,
                                          depth = ai.depth,
                                          duration = ai.duration,
                                          eFlag = ai.Eflag,
                                          leadTime = ai.leadtime,
                                          scheduleDate = ai.ScheduleDate,
                                          startTime = ai.StartTime,
                                          taskName = ai.taskname,
                                          taskStatus = ai.taskstatus,
                                          time = ai.time,
                                          wellTaskId = ai.welltask_id,
                                          wellTypeId = ai.welltype_id,
                                          wellId = ai.well_id,
                                          dependencyTask = subresult.taskname ?? String.Empty,
                                          wellName = subresult1.wellname ?? String.Empty,
                                      }).ToList();


                var welldata = associatedtasks.Union(predictiveTasks).Union(exemptionTasks);


                return welldata;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetRIGAIResult", null);
                return null;
            }
        }

        public Task<List<WellMasterDataViewModel>> GetWellMaster(string wellId, WellIdentityUser user = null)
        {
            try
            {
                List<WellMasterDataViewModel> AIWellDataModelList = new List<WellMasterDataViewModel>();

                var groupList = GetRIGAIGroup();

                var userwellIds = new List<string>();
                if (user != null && user.WellUser.HasValue && user.WellUser.Value)
                {
                    userwellIds = db.UsersWells.Where(x => x.UserId == user.Id).Select(x => x.WellId).ToList();
                }

                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                var wellMaster = (from wel in db.WellRegister
                                  join rig in db.rig_register on wel.RigID equals rig.Rig_id into t1
                                  from rigresult in t1.DefaultIfEmpty()
                                  join pad in db.pad_register on wel.PadID equals pad.Pad_id into t2
                                  from padresult in t2.DefaultIfEmpty()
                                  join batch in db.BatchDillingType_Register on wel.BatchDrillingTypeID equals batch.BatchDrillingType_Id into t3
                                  from batchresult in t3.DefaultIfEmpty()
                                  join typ in db.WellType on wel.welltype_id equals typ.welltype_id into tj
                                  from subresult in tj.DefaultIfEmpty()
                                  where wel.customer_id == (user == null ? wel.customer_id : user.TenantId)
                                    && (wel.well_id == wellId && !checkwellFilter || checkwellFilter)
                                  select new WellMasterDataViewModel
                                  {
                                      wellId = wel.well_id,
                                      wellName = wel.wellname,
                                      wellType = subresult.welltype_name ?? String.Empty,
                                      wellTypeId = new WellTypeModel { wellTypeId = subresult.welltype_id, wellTypeName = subresult.welltype_name },
                                      county = wel.County,
                                      complete_well_drill = wel.Conplete_well_drill == 1 ? true : false,
                                      batch_drill_casing = wel.Batch_drill_casing == 1 ? true : false,
                                      batch_drill_horizontal = wel.Batch_drill_horizontal == 1 ? true : false,
                                      casing_string = wel.Casing_string == 1 ? true : false,
                                      numAPI = wel.NumAPI,
                                      numAFE = wel.NumAFE,
                                      rigName = rigresult.Rig_Name ?? String.Empty,
                                      padName = padresult.Pad_Name ?? String.Empty,
                                      state = wel.State,
                                      batchFlag = wel.BatchFlag == 1 ? true : false,
                                      batchDrillingTypeId = wel.BatchDrillingTypeID ?? String.Empty,
                                      casingString = wel.CasingString,
                                      padID = wel.PadID,
                                      rigID = wel.RigID,
                                      latitude = wel.Latitude,
                                      longitude = wel.Longitude,
                                      fieldName = wel.FieldName,
                                      basin = wel.Basin
                                  }).ToList();

                if (user != null && user.WellUser.HasValue && user.WellUser.Value && userwellIds.Count > 0)
                {
                    wellMaster = wellMaster.Where(x => userwellIds.FirstOrDefault(y => y == x.wellId) != null).ToList();
                }

                var wellMasterResult = (from wel in wellMaster
                                        join gp in groupList on wel.wellId equals gp.wellId into gj
                                        from subresult1 in gj.DefaultIfEmpty()
                                        orderby wel.wellName
                                        select new WellMasterDataViewModel
                                        {
                                            wellId = wel.wellId,
                                            wellName = wel.wellName,
                                            wellType = wel.wellType,
                                            taskCount = subresult1?.taskCount ?? String.Empty,
                                            minSchdDate = subresult1?.minSchdDate ?? String.Empty,
                                            maxSchdDate = subresult1?.maxSchdDate ?? String.Empty,
                                            wellTypeId = wel.wellTypeId,
                                            county = wel.county,
                                            complete_well_drill = wel.complete_well_drill,
                                            batch_drill_casing = wel.batch_drill_casing,
                                            batch_drill_horizontal = wel.batch_drill_horizontal,
                                            casing_string = wel.casing_string,
                                            numAPI = wel.numAPI,
                                            numAFE = wel.numAFE,
                                            rigName = wel.rigName,
                                            padName = wel.padName,
                                            state = wel.state,
                                            batchFlag = wel.batchFlag,
                                            batchDrillingTypeId = wel.batchDrillingTypeId,
                                            casingString = wel.casingString,
                                            padID = wel.padID,
                                            rigID = wel.rigID,
                                            latitude = wel.latitude,
                                            longitude = wel.longitude,
                                            fieldName = wel.fieldName,
                                            basin = wel.basin
                                        }
                                       ).ToList();

                return Task.FromResult(wellMasterResult);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellMaster", null);
                return null;
            }
        }

        private IList<WellMasterGroupDataViewModel> GetRIGAIGroup()
        {
            try
            {
                List<AIWellDataModel> AIWellDataModelList = new List<AIWellDataModel>();

                var associatedList = db.AIAssociatedTasks.AsNoTracking();
                var predictiveList = db.AIPredictiveTasks.AsNoTracking();
                var exemptionlist = db.AIExemptionTasks.AsNoTracking();

                var associatedtasks = (from ai in associatedList
                                       join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                       from subresult in tj.DefaultIfEmpty()
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                       from subresult1 in wj.DefaultIfEmpty()
                                       select new AIWellDataModel
                                       {
                                           actionDate = ai.ActionDate,
                                           adt = ai.ADT,
                                           customerId = ai.customer_id,
                                           dependency = ai.dependency,
                                           dependencyFlag = ai.dependency_flag,
                                           depth = ai.depth,
                                           duration = ai.duration,
                                           eFlag = ai.Eflag,
                                           leadTime = ai.leadtime,
                                           scheduleDate = ai.ScheduleDate,
                                           startTime = ai.StartTime,
                                           taskName = ai.taskname,
                                           taskStatus = ai.taskstatus,
                                           time = ai.time,
                                           wellTaskId = ai.welltask_id,
                                           wellTypeId = ai.welltype_id,
                                           wellId = ai.well_id,
                                           dependencyTask = subresult.taskname ?? String.Empty,
                                           wellName = subresult1.wellname ?? String.Empty,
                                       }).ToList();

                var predictiveTasks = (from ai in predictiveList
                                       join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                       from subresult in tj.DefaultIfEmpty()
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                       from subresult1 in wj.DefaultIfEmpty()
                                       select new AIWellDataModel
                                       {
                                           actionDate = ai.ActionDate,
                                           adt = ai.ADT,
                                           customerId = ai.customer_id,
                                           dependency = ai.dependency,
                                           dependencyFlag = ai.dependency_flag,
                                           depth = ai.depth,
                                           duration = ai.duration,
                                           eFlag = ai.Eflag,
                                           leadTime = ai.leadtime,
                                           scheduleDate = ai.ScheduleDate,
                                           startTime = ai.StartTime,
                                           taskName = ai.taskname,
                                           taskStatus = ai.taskstatus,
                                           time = ai.time,
                                           wellTaskId = ai.welltask_id,
                                           wellTypeId = ai.welltype_id,
                                           wellId = ai.well_id,
                                           dependencyTask = subresult.taskname ?? String.Empty,
                                           wellName = subresult1.wellname ?? String.Empty,
                                       }).ToList();

                var exemptionTasks = (from ai in exemptionlist
                                      join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                      from subresult in tj.DefaultIfEmpty()
                                      join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                      from subresult1 in wj.DefaultIfEmpty()
                                      select new AIWellDataModel
                                      {
                                          actionDate = ai.ActionDate,
                                          adt = ai.ADT,
                                          customerId = ai.customer_id,
                                          dependency = ai.dependency,
                                          dependencyFlag = ai.dependency_flag,
                                          depth = ai.depth,
                                          duration = ai.duration,
                                          eFlag = ai.Eflag,
                                          leadTime = ai.leadtime,
                                          scheduleDate = ai.ScheduleDate,
                                          startTime = ai.StartTime,
                                          taskName = ai.taskname,
                                          taskStatus = ai.taskstatus,
                                          time = ai.time,
                                          wellTaskId = ai.welltask_id,
                                          wellTypeId = ai.welltype_id,
                                          wellId = ai.well_id,
                                          dependencyTask = subresult.taskname ?? String.Empty,
                                          wellName = subresult1.wellname ?? String.Empty,
                                      }).ToList();


                var welldata = associatedtasks.Union(predictiveTasks).Union(exemptionTasks);

                var groupdata = (from wel in welldata
                                 group wel by wel.wellId into g

                                 select new WellMasterGroupDataViewModel
                                 {
                                     wellId = g.Key,
                                     taskCount = g.Count().ToString(),
                                     minSchdDate = g.Min(c => c.scheduleDate).ToString(),
                                     maxSchdDate = g.Max(c => c.scheduleDate).ToString()
                                 }).ToList();


                return groupdata.ToList();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetRIGAIGroup", null);
                return null;
            }
        }
        public IEnumerable<WellDataServiceCompanyViewModel> GetServiceWellMaster(string tenantId)
        {
            try
            {
                List<WellMasterDataViewModel> AIWellDataModelList = new List<WellMasterDataViewModel>();

                var wellServiceMaster = (from prj in db.Projects
                                         join opcmp in db.CorporateProfile on prj.OprTenantID equals opcmp.TenantId
                                         join wel in db.WellRegister on prj.WellID equals wel.well_id
                                         join auc in db.AuctionProposals on prj.ProposalID equals auc.ProposalId into aj
                                         from aucsubresult in aj.DefaultIfEmpty()
                                         join typ in db.WellType on wel.welltype_id equals typ.welltype_id into tj
                                         from typsubresult in tj.DefaultIfEmpty()
                                         where prj.ServiceCompID == tenantId
                                         orderby prj.ProjectID
                                         select new WellDataServiceCompanyViewModel
                                         {
                                             operatingCompanyId = opcmp.ID,
                                             operatingCompanyName = opcmp.Name,
                                             wellId = wel.well_id,
                                             wellName = wel.wellname,
                                             wellType = typsubresult.welltype_name ?? String.Empty,
                                             wellTypeId = wel.welltype_id,
                                             county = wel.County,
                                             projectID = prj.ProjectID,
                                             projectTitle = prj.ProjectTitle,
                                             projectDescription = prj.ProjectDescription,
                                             projectStartDate = prj.ActualStart.ToString() ?? String.Empty,
                                             projectEndDate = prj.ActualEnd.ToString() ?? String.Empty,
                                             proposalID = prj.ProposalID,
                                             jobID = aucsubresult.JobId
                                         }).ToList();


                var wellMaster = (from wm in wellServiceMaster
                                  orderby wm.projectID
                                  select new WellDataServiceCompanyViewModel
                                  {
                                      operatingCompanyId = wm.operatingCompanyId,
                                      operatingCompanyName = wm.operatingCompanyName,
                                      wellId = wm.wellId,
                                      wellName = wm.wellName,
                                      wellType = wm.wellType,
                                      wellTypeId = wm.wellTypeId,
                                      county = wm.county,
                                      projectID = wm.projectID,
                                      projectTitle = wm.projectTitle,
                                      projectDescription = wm.projectDescription,
                                      projectStartDate = wm.projectStartDate.ToString() != "" ? Convert.ToString(Convert.ToDateTime(wm.projectStartDate).ToString("MM/dd/yyyy")) : "",
                                      projectEndDate = wm.projectEndDate.ToString() != "" ? Convert.ToString(Convert.ToDateTime(wm.projectEndDate).ToString("MM/dd/yyyy")) : "",
                                      proposalID = wm.proposalID,
                                      jobID = wm.jobID
                                  }).ToList();
                return wellMaster;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetServiceWellMaster", null);
                return null;
            }
        }
        public IEnumerable<WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel> GetWellAIStatusChartData(string tenantId)
        {
            try
            {
                List<WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel> WellAIStatusModelList = new List<WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel>();

                var associatedList = db.AIAssociatedTasks.AsNoTracking();
                var predictiveList = db.AIPredictiveTasks.AsNoTracking();
                var exemptionlist = db.AIExemptionTasks.AsNoTracking();


                var associatedtasks = (from ai in associatedList
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id
                                       group wl by wl.wellname into g
                                       select new WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel
                                       {
                                           WellName = g.Key,
                                           AssociatedTasksCount = g.Count(),
                                           PredictiveTasksCount = 0,
                                           ExemptionTasksCount = 0
                                       }
                                      ).ToList();

                var predictivetasks = (from pr in predictiveList
                                       join wl in db.WellRegister on pr.well_id equals wl.well_id
                                       group wl by wl.wellname into g
                                       select new WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel
                                       {
                                           WellName = g.Key,
                                           AssociatedTasksCount = 0,
                                           PredictiveTasksCount = g.Count(),
                                           ExemptionTasksCount = 0
                                       }
                                       ).ToList();

                var exemptionTasks = (from ex in exemptionlist
                                      join wl in db.WellRegister on ex.well_id equals wl.well_id
                                      group wl by wl.wellname into g
                                      select new WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel
                                      {
                                          WellName = g.Key,
                                          AssociatedTasksCount = 0,
                                          PredictiveTasksCount = 0,
                                          ExemptionTasksCount = g.Count()
                                      }
                                        ).ToList();

                var wellAIStatusModelList = associatedtasks.Union(predictivetasks).Union(exemptionTasks);


                var groupResult = (from wl in wellAIStatusModelList
                                   group wl by wl.WellName into g
                                   select new WellAI.Advisor.Model.ServiceCompany.Models.WellAIStatusViewModel
                                   {
                                       WellName = g.Key,
                                       AssociatedTasksCount = g.Sum(c => c.AssociatedTasksCount),
                                       PredictiveTasksCount = g.Sum(c => c.PredictiveTasksCount),
                                       ExemptionTasksCount = g.Sum(c => c.ExemptionTasksCount)
                                   }
                                  ).ToList();

                return groupResult;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellAIStatusChartData", null);
                return null;
            }
        }

        public IEnumerable<Model.ServiceCompany.Models.WellAIRAWStatusViewModel> GetWellAIRAWStatusChartData(string tenantId)
        {
            try
            {
                List<WellAIRAWStatusViewModel> WellAIRAWStatusModelList = new List<WellAIRAWStatusViewModel>();


                var UpcomingCount = (from wel in db.WELL_REGISTERs
                                     join ts in db.TaskStatus on wel.well_id equals ts.Well_ID
                                     join tt in db.TaskTable on ts.Tasktable_id equals tt.Tasktable_id
                                     join wt in db.WellTask on tt.Welltask_id equals wt.welltask_id
                                     where ts.Taskstatus == 0
                                     group wel by wel.wellname into g
                                     select new WellAIRAWStatusViewModel
                                     {
                                         WellName = g.Key,
                                         UpcomingCount = g.Count(),
                                         OngoingCount = 0,
                                         ClosedCount = 0
                                     }
                               ).ToList();

                var OngoingCount = (from wel in db.WELL_REGISTERs
                                    join ts in db.TaskStatus on wel.well_id equals ts.Well_ID
                                    join tt in db.TaskTable on ts.Tasktable_id equals tt.Tasktable_id
                                    join wt in db.WellTask on tt.Welltask_id equals wt.welltask_id
                                    where ts.Taskstatus == 1
                                    group wel by wel.wellname into g
                                    select new WellAIRAWStatusViewModel
                                    {
                                        WellName = g.Key,
                                        UpcomingCount = 0,
                                        OngoingCount = g.Count(),
                                        ClosedCount = 0
                                    }
                           ).ToList();

                var ClosedCount = (from wel in db.WELL_REGISTERs
                                   join ts in db.TaskStatus on wel.well_id equals ts.Well_ID
                                   join tt in db.TaskTable on ts.Tasktable_id equals tt.Tasktable_id
                                   join wt in db.WellTask on tt.Welltask_id equals wt.welltask_id
                                   where ts.Taskstatus == 2
                                   group wel by wel.wellname into g
                                   select new WellAIRAWStatusViewModel
                                   {
                                       WellName = g.Key,
                                       UpcomingCount = 0,
                                       OngoingCount = 0,
                                       ClosedCount = g.Count()
                                   }
                           ).ToList();


                var wellAIRAWStatusModelList = UpcomingCount.Union(OngoingCount).Union(ClosedCount);

                var RAWgroupResult = (from wl in wellAIRAWStatusModelList
                                      group wl by wl.WellName into g
                                      select new WellAIRAWStatusViewModel
                                      {
                                          WellName = g.Key,
                                          UpcomingCount = g.Sum(c => c.UpcomingCount),
                                          OngoingCount = g.Sum(c => c.OngoingCount),
                                          ClosedCount = g.Sum(c => c.ClosedCount)
                                      }
                                  ).ToList();

                return RAWgroupResult;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellAIRAWStatusChartData", null);
                return null;
            }
        }

        public async Task<int> AssignWellsToUser(string userId, List<string> wellIds)
        {
            try
            {
                var assignedOldWells = db.UsersWells.Where(x => x.UserId == userId).ToList();
                var todelete = assignedOldWells.Where(x => wellIds.FirstOrDefault(y => y == x.WellId) == null).ToList();
                var toinsert = wellIds.Where(x => assignedOldWells.FirstOrDefault(y => y.WellId == x) == null).ToList();

                foreach (var delItem in todelete)
                {
                    db.UsersWells.Remove(delItem);
                }

                foreach (var insertItem in toinsert)
                {
                    db.UsersWells.Add(new UserWell
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        UserId = userId,
                        WellId = insertItem
                    });
                }

                return await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository AssignWellsToUser", null);
                return 0;
            }
        }


        public async Task<int> AssignRigsToUser(string userId, List<string> RigId)
        {
            try
            {
                var assignedOldWells = db.UserRigs.Where(x => x.UserId == userId).ToList();
                var todelete = assignedOldWells.Where(x => RigId.FirstOrDefault(y => y == x.RigID) == null).ToList();
                var toinsert = RigId.Where(x => assignedOldWells.FirstOrDefault(y => y.RigID == x) == null).ToList();

                foreach (var delItem in todelete)
                {
                    db.UserRigs.Remove(delItem);
                }

                foreach (var insertItem in toinsert)
                {
                    db.UserRigs.Add(new UserRig
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        UserId = userId,
                        RigID = insertItem
                    });
                }

                return await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository AssignRigsToUser", null);
                return 0;
            }
        }


        public async Task<string> GetTenantIdByWelllId(string wellId)
        {
            try
            {
                var well = db.WellRegister.FirstOrDefault(x => x.well_id == wellId);

                var result = well == null ? "" : well.customer_id;

                return await Task.FromResult(result);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetTenantIdByWelllId", null);
                return null;
            }
        }

        public async Task<WellRegister> GetWellRegisterById(string wellId)
        {
            try
            {
                var well = db.WellRegister.FirstOrDefault(x => x.well_id == wellId);

                return await Task.FromResult(well);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellRegisterById", null);
                return null;
            }
        }

        public async Task UpdatePredictionWellRegisterById(string wellId, bool prediction)
        {
            try
            {
                var well = db.WellRegister.FirstOrDefault(x => x.well_id == wellId);

                if (well != null)
                {
                    well.Prediction = prediction;

                    await db.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository UpdatePredictionWellRegisterById", null);
            }
        }

        public Task<List<WellMasterDataViewModel>> GetUserAssignedRigs(string userId, string tenantId)
        {
            try
            {
                var result = new List<WellMasterDataViewModel>();

                if (string.IsNullOrEmpty(userId))
                {
                    result = (from well in db.WellRegister
                              join rig in db.rig_register on well.RigID equals rig.Rig_id
                              where well.customer_id == tenantId

                              select new WellMasterDataViewModel
                              {
                                  wellId = well.well_id,
                                  wellName = well.wellname,
                                  rigID = well.RigID,
                                  rigName = rig.Rig_Name

                              }).ToList();
                }
                else
                {
                    result = (from uw in db.UserRigs
                              join vr in db.rig_register on uw.RigID equals vr.Rig_id
                              where uw.UserId == userId
                              select new WellMasterDataViewModel
                              {
                                  rigID = uw.RigID,
                                  rigName = vr.Rig_Name
                              }).ToList();
                }

                return Task.FromResult(result);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetUserAssignedRigs", null);
                return null;
            }
        }

        public Task<List<WellMasterDataViewModel>> GetUserAssignedWells(string userId, string tenantId)
        {
            try
            {
                var result = new List<WellMasterDataViewModel>();

                if (string.IsNullOrEmpty(userId))
                {
                    result = (from well in db.WellRegister
                              join rig in db.rig_register on well.RigID equals rig.Rig_id
                              where
                               well.customer_id == tenantId
                              select new WellMasterDataViewModel
                              {
                                  wellId = well.well_id,
                                  wellName = well.wellname,
                                  rigID = well.RigID,
                                  rigName = rig.Rig_Name

                              }).ToList();
                }
                else
                {
                    result = (from uw in db.UserRigs
                              join vr in db.rig_register on uw.RigID equals vr.Rig_id
                              join wr in db.WellRegister on vr.Rig_id equals wr.RigID
                              where uw.UserId == userId
                              select new WellMasterDataViewModel
                              {
                                  wellId = wr.well_id,
                                  wellName = wr.wellname,
                                  rigID = wr.RigID,
                                  rigName = vr.Rig_Name
                              }).Distinct().ToList();
                }

                return Task.FromResult(result);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetUserAssignedWells", null);
                return null;
            }
        }


        public async Task<List<WellMasterDataViewModel>> GetRigsForOperationCompany(string tenantId)
        {
            try
            {
                var wells = (from rig in db.rig_register
                             where rig.TenantID == tenantId && rig.isActive == true
                             select new WellMasterDataViewModel
                             {
                                 rigID = rig.Rig_id,
                                 rigName = rig.Rig_Name
                             }).ToList();

                return await Task.FromResult(wells);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetRigsForOperationCompany", null);
                return null;
            }
        }

        public async Task<List<WellMasterDataViewModel>> GetWellsForOperationCompany(string tenantId)
        {
            try
            {
                var wells = (from wel in db.WellRegister
                             join rig in db.rig_register on wel.RigID equals rig.Rig_id into t1
                             from rigresult in t1.DefaultIfEmpty()
                             select new WellMasterDataViewModel
                             {
                                 wellId = wel.well_id,
                                 wellName = (string.IsNullOrEmpty(rigresult.Rig_Name) ? "N/A" : rigresult.Rig_Name) + ": " + wel.wellname
                             }).ToList();

                return await Task.FromResult(wells);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellsForOperationCompany", null);
                return null;
            }
        }

        public async Task<List<WellMasterDataViewModel>> GetWellsForOperationCompanyOnServiceSite(string tenantId)
        {
            try
            {
                var result = new List<WellMasterDataViewModel>();

                var nospecificTenant = tenantId == DLL.Constants.NoSpecificWellFilterKey;

                result = (from well in db.WellRegister
                          where (well.customer_id == tenantId && !nospecificTenant || nospecificTenant)
                          select new WellMasterDataViewModel
                          {
                              wellId = well.well_id,
                              wellName = well.wellname
                          }).ToList();

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellsForOperationCompanyOnServiceSite", null);
                return null;
            }
        }

        public Task<List<WellAI.Advisor.Model.OperatingCompany.Models.RigModel>> GetRigMaster(string tenantId,string RigId)
        {
            try
            {
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;

                var rigModel = (from rig in db.rig_register
                                where rig.TenantID == tenantId
                                select new WellAI.Advisor.Model.OperatingCompany.Models.RigModel
                                {
                                    Rig_id = rig.Rig_id,
                                    Rig_Name = rig.Rig_Name,
                                    Latitude = rig.Latitude,
                                    Longitude = rig.Longitude,
                                    TenantID = rig.TenantID,
                                    isActive = rig.isActive
                                }).ToList();

                return Task.FromResult(rigModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetRigMaster", null);
                return null;
            }
        }

        public Task<List<WellAI.Advisor.Model.OperatingCompany.Models.PadModel>> GetPadMaster(string tenantId)
        {
            try
            {
                var padModel = (from rig in db.pad_register
                                where rig.TenantID == tenantId && rig.isActive == true
                                select new WellAI.Advisor.Model.OperatingCompany.Models.PadModel
                                {
                                    Pad_id = rig.Pad_id,
                                    Pad_Name = rig.Pad_Name,
                                    Latitude = rig.Latitude,
                                    Longitude = rig.Longitude,
                                    TenantID = rig.TenantID
                                }).ToList();

                return Task.FromResult(padModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetPadMaster", null);
                return null;
            }
        }

        public IEnumerable<OperatingWellAIStatusViewModel> GetWellAIStatusChartDataForOpr(string tenantId)
        {
            try
            {
                List<OperatingWellAIStatusViewModel> WellAIStatusModelList = new List<OperatingWellAIStatusViewModel>();

                var associatedList = db.AIAssociatedTasks.AsNoTracking();
                var predictiveList = db.AIPredictiveTasks.AsNoTracking();
                var exemptionlist = db.AIExemptionTasks.AsNoTracking();


                var associatedtasks = (from ai in associatedList
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id
                                       group wl by wl.wellname into g
                                       select new OperatingWellAIStatusViewModel
                                       {
                                           OPrWellName = g.Key,
                                           OprAssociatedTasksCount = g.Count(),
                                           OprPredictiveTasksCount = 0,
                                           OprExemptionTasksCount = 0

                                       }
                                      ).ToList();

                var predictivetasks = (from pr in predictiveList
                                       join wl in db.WellRegister on pr.well_id equals wl.well_id
                                       group wl by wl.wellname into g
                                       select new OperatingWellAIStatusViewModel
                                       {
                                           OPrWellName = g.Key,
                                           OprAssociatedTasksCount = 0,
                                           OprPredictiveTasksCount = g.Count(),
                                           OprExemptionTasksCount = 0
                                       }
                                       ).ToList();

                var exemptionTasks = (from ex in exemptionlist
                                      join wl in db.WellRegister on ex.well_id equals wl.well_id
                                      group wl by wl.wellname into g
                                      select new OperatingWellAIStatusViewModel
                                      {
                                          OPrWellName = g.Key,
                                          OprAssociatedTasksCount = 0,
                                          OprPredictiveTasksCount = 0,
                                          OprExemptionTasksCount = g.Count()
                                      }
                                        ).ToList();

                var wellAIStatusModelList = associatedtasks.Union(predictivetasks).Union(exemptionTasks);
                var groupResult = (from wl in wellAIStatusModelList
                                   group wl by wl.OPrWellName into g
                                   select new OperatingWellAIStatusViewModel
                                   {
                                       OPrWellName = g.Key,
                                       OprAssociatedTasksCount = g.Sum(c => c.OprAssociatedTasksCount),
                                       OprPredictiveTasksCount = g.Sum(c => c.OprPredictiveTasksCount),
                                       OprExemptionTasksCount = g.Sum(c => c.OprExemptionTasksCount)
                                   }
                                  ).ToList();

                return groupResult;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellAIStatusChartDataForOpr", null);
                return null;
            }
        }

        public IEnumerable<OperatingWellAIRAWStatusViewModel> GetWellAIRAWStatusChartDataForOpr(string tenantId)
        {
            try
            {
                List<OperatingWellAIRAWStatusViewModel> WellAIRAWStatusModelList = new List<OperatingWellAIRAWStatusViewModel>();


                var UpcomingCount = (from wel in db.WELL_REGISTERs
                                     join ts in db.TaskStatus on wel.well_id equals ts.Well_ID
                                     join tt in db.TaskTable on ts.Tasktable_id equals tt.Tasktable_id
                                     join wt in db.WellTask on tt.Welltask_id equals wt.welltask_id
                                     where ts.Taskstatus == 0
                                     group wel by wel.wellname into g
                                     select new OperatingWellAIRAWStatusViewModel
                                     {
                                         WellName = g.Key,
                                         UpcomingCount = g.Count(),
                                         OngoingCount = 0,
                                         ClosedCount = 0
                                     }
                               ).ToList();

                var OngoingCount = (from wel in db.WELL_REGISTERs
                                    join ts in db.TaskStatus on wel.well_id equals ts.Well_ID
                                    join tt in db.TaskTable on ts.Tasktable_id equals tt.Tasktable_id
                                    join wt in db.WellTask on tt.Welltask_id equals wt.welltask_id
                                    where ts.Taskstatus == 1
                                    group wel by wel.wellname into g
                                    select new OperatingWellAIRAWStatusViewModel
                                    {
                                        WellName = g.Key,
                                        UpcomingCount = 0,
                                        OngoingCount = g.Count(),
                                        ClosedCount = 0
                                    }
                           ).ToList();

                var ClosedCount = (from wel in db.WELL_REGISTERs
                                   join ts in db.TaskStatus on wel.well_id equals ts.Well_ID
                                   join tt in db.TaskTable on ts.Tasktable_id equals tt.Tasktable_id
                                   join wt in db.WellTask on tt.Welltask_id equals wt.welltask_id
                                   where ts.Taskstatus == 2
                                   group wel by wel.wellname into g
                                   select new OperatingWellAIRAWStatusViewModel
                                   {
                                       WellName = g.Key,
                                       UpcomingCount = 0,
                                       OngoingCount = 0,
                                       ClosedCount = g.Count()
                                   }
                           ).ToList();


                var wellAIRAWStatusModelList = UpcomingCount.Union(OngoingCount).Union(ClosedCount);

                var RAWgroupResult = (from wl in wellAIRAWStatusModelList
                                      group wl by wl.WellName into g
                                      select new OperatingWellAIRAWStatusViewModel
                                      {
                                          WellName = g.Key,
                                          UpcomingCount = g.Sum(c => c.UpcomingCount),
                                          OngoingCount = g.Sum(c => c.OngoingCount),
                                          ClosedCount = g.Sum(c => c.ClosedCount)
                                      }
                                  ).ToList();

                return RAWgroupResult;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellAIRAWStatusChartDataForOpr", null);
                return null;
            }
        }

        public async Task<List<InDepthRigData>> GetWellDepthDataAIRow(string wellId)
        {
            try
            {
                List<InDepthRigData> result = new List<InDepthRigData>();

                var alldepths = (from ai in db.AIRawDatas
                                 where ai.well_id == wellId
                                 select new InDepthRigData
                                 {
                                     Day = ai.day.HasValue ? (int)ai.day.Value : 1,
                                     Value = ai.depth.HasValue ? ai.depth.Value : 0,
                                     WellId = wellId
                                 }).ToList();

                foreach (var item in alldepths)
                {
                    if (!result.Any(x => x.Day == item.Day && x.Value == item.Value))
                    {
                        result.Add(item);
                    }
                }

                return await Task.FromResult(result.OrderBy(x => x.Day).ThenBy(x => x.Value).ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellDepthDataAIRow", null);
                return null;
            }
        }

        public async Task<List<InDepthRigData>> GetWellDepthTimeChartFromTasks(string wellId)
        {
            try
            {
                List<InDepthRigData> result = new List<InDepthRigData>();
                var alldepths = (from ai in db.Tasks
                                 where ai.IsBiddable == true && ai.IsActive == true
                                 select new InDepthRigData
                                 {
                                     Day = ai.Day.HasValue ? (int)ai.Day.Value : 1,
                                     Value = ai.Depth.HasValue ? ai.Depth.Value : 0,
                                     WellId = wellId
                                 }).OrderBy(X => X.Day).ToList();

                foreach (var item in alldepths)
                {
                    if (!result.Any(x => x.Day == item.Day && !(Math.Abs(x.Value - item.Value) > 0)))
                    {
                        result.Add(item);
                    }
                }

                return await Task.FromResult(result.ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellDepthDataAIRow", null);
                return null;
            }
        }

        public async Task<List<InDepthRigData>> GetWellDepthData(string wellId)
        {
            try
            {
                List<InDepthRigData> result = new List<InDepthRigData>();

                var alldepths = (from task in db.Tasks
                                 where task.IsActive == true
                                 orderby task.Day, task.Depth
                                 select new InDepthRigData
                                 {
                                     Day = task.Day.HasValue ? task.Day.Value : 1,
                                     Value = task.Depth.HasValue ? task.Depth.Value : 0,
                                     WellId = wellId
                                 }).ToList();

                foreach (var item in alldepths)
                {
                    if (!result.Any(x => x.Day == item.Day && x.Value == item.Value))
                    {
                        result.Add(item);
                    }
                }

                return await Task.FromResult(result.OrderBy(x => x.Day).ThenBy(x => x.Value).ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellDepthData", null);
                return null;
            }
        }

        public async Task<float> GetWellCurrentDepth(string wellId)
        {
            try
            {
                var curdepth = db.WellDepthDataStages.Where(x => x.WID == wellId).Max(x => x.DMEA);
                var result = (from ai in db.Tasks
                              join stage in db.Stages on ai.StageType equals stage.Id
                              where (ai.Depth >= curdepth)
                              select new InDepthRigDataGridModel
                              {
                                  Day = ai.Day.HasValue ? (int)ai.Day.Value : 1,
                                  Depth = ai.Depth.HasValue ? (float)ai.Depth.Value : 0,
                                  Name = ai.Name,
                                  WellId = wellId,
                                  IsBiddable = ai.IsBiddable,
                                  TaskId = ai.TaskId,
                                  StageType = stage.Name == null ? "N/A" : stage.Name
                              }).Min(x => x.Depth);

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellCurrentDepth", null);
                return 0;
            }
        }

        public async Task<List<InDepthRigDataGridModel>> GetWellDepthGridData(string wellId)
        {
            try
            {
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;
                var wellfilter = db.WellDepthDataStages.Where(x => x.WID == wellId).Max(x => x.DMEA);

                var result = new List<InDepthRigDataGridModel>();
                if (wellfilter != null)
                {
                    result = (from ai in db.Tasks
                              join stage in db.Stages on ai.StageType equals stage.Id
                              where (ai.Depth >= wellfilter && !checkwellFilter || checkwellFilter)
                              select new InDepthRigDataGridModel
                              {
                                  Day = ai.Day.HasValue ? (int)ai.Day.Value : 1,
                                  Depth = ai.Depth.HasValue ? (float)ai.Depth.Value : 0,
                                  Name = ai.Name,
                                  WellId = wellId,
                                  IsBiddable = ai.IsBiddable,
                                  TaskId = ai.TaskId,
                                  StageType = stage.Name == null ? "N/A" : stage.Name
                              }).OrderBy(x => x.Day).ThenBy(x => x.Depth).ToList();
                }
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWellDepthGridData", null);
                return null;
            }
        }

        public Task<List<WorkstationModel>> GetWorkstationDetail(string tenantId)
        {
            try
            {
                var workstationModel = (from Workstation in db.WorkstationRegister
                                        where Workstation.CustomerAccountIdentifier == tenantId
                                        select new WorkstationModel
                                        {
                                            RegisterationId = Workstation.RegisterationId,
                                            CustomerAccountIdentifier = Workstation.CustomerAccountIdentifier,
                                            DeviceName = Workstation.DeviceName,
                                            WorkstationIdentifier = Workstation.WorkstationIdentifier,
                                            WorkstationToken = Workstation.WorkstationToken.ToString(),
                                            IsActive = Workstation.IsActive,
                                            CreatedDate = Workstation.CreatedDate,
                                            ModifiedDate = Workstation.ModifiedDate
                                        }).ToList();

                return Task.FromResult(workstationModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "AIDataRepository GetWorkstationDetail", null);
                return null;
            }
        }
    }
}

