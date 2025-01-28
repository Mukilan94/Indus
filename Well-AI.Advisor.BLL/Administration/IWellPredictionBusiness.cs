using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.BLL.Administration
{
    public interface IWellPredictionBusiness
    {
        Task<Tuple<List<WellMasterDataViewModel>, int>> GetWellDatas(int pageNumber, int pageSize);
    }

    public class WellPredictionBusiness : IWellPredictionBusiness
    {
        
        private readonly WebAIAdvisorContext db;
        protected readonly IMapper _mapper;

        public WellPredictionBusiness(WebAIAdvisorContext db, IMapper mapper)
        {
            this.db = db;
            _mapper = mapper;
        }
        public Task<Tuple<List<WellMasterDataViewModel>, int>> GetWellDatas(int pageNumber, int pageSize)
        {
            try
            {
                var groupList = GetRIGAIGroup();
                var total = db.WellRegister.Count();

                var wellMaster = (from wel in db.WellRegister
                                  join rig in db.rig_register on wel.RigID equals rig.Rig_id into t1
                                  from rigresult in t1.DefaultIfEmpty()
                                  join pad in db.pad_register on wel.PadID equals pad.Pad_id into t2
                                  from padresult in t2.DefaultIfEmpty()
                                  join batch in db.BatchDillingType_Register on wel.BatchDrillingTypeID equals batch.BatchDrillingType_Id into t3
                                  from batchresult in t3.DefaultIfEmpty()
                                  join typ in db.WellType on wel.welltype_id equals typ.welltype_id into tj
                                  from subresult in tj.DefaultIfEmpty()
                                  join bas in db.BasinTypes on wel.Basin equals bas.Basin_ID into t4
                                  from basinresult in t4.DefaultIfEmpty()
                                  join Operator in db.CorporateProfile on wel.customer_id equals Operator.TenantId
                                  select new WellMasterDataViewModel
                                  {
                                      wellId = wel.well_id,
                                      wellName = wel.wellname,
                                      wellType = subresult.welltype_name ?? String.Empty,
                                      wellTypeId = new Model.OperatingCompany.Models.WellTypeModel { wellTypeId = subresult.welltype_id, wellTypeName = subresult.welltype_name },
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
                                      basin = basinresult.BasinType_name,
                                      OperatorName = Operator.Name
                                  }).ToList();

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
                                            basin = wel.basin,
                                            OperatorName = wel.OperatorName
                                        }
                                       ).ToList();

                return Task.FromResult(new Tuple<List<WellMasterDataViewModel>, int>(wellMasterResult, total));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "IWellPrediction GetWellDatas", null);
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
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "IWellPrediction GetRIGAIGroup", null);
                return null;
            } 
        }
    }
}
