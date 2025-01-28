using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class WellData
    {
        [ScaffoldColumn(false)]
        public int Id
        {
            get;
            set;
        }
        public string Rig
        {
            get;
            set;
        }

        public string CementStage { get; set; }
        public string ServiceCompany { get; set; }

        public string BitDepth
        {
            get;
            set;
        }
        public string HoleDepth
        {
            get;
            set;
        }
        public string Hookload
        {
            get;
            set;
        }
        public string WOB
        {
            get;
            set;
        }
        public string ROP
        {
            get;
            set;
        }
        public DateTime Updated
        {
            get;
            set;
        }
    }

    public class AIWellDataModel
    {
        public string customerId { get; set; }
        public string wellId { get; set; }
        public string wellTypeId { get; set; }
        public string wellTaskId { get; set; }
        public double day { get; set; }
        public double depth { get; set; }
        public string time { get; set; }
        public double duration { get; set; }
        public double leadTime { get; set; }
        public double dependencyFlag { get; set; }
        public string dependency { get; set; }
        public string taskName { get; set; }
        public string actionDate { get; set; }
        public double taskStatus { get; set; }
        public DateTime adt { get; set; }
        public DateTime scheduleDate { get; set; }
        public DateTime startTime { get; set; }
        public double eFlag { get; set; }

        public string dependencyTask { get; set; }
        public string wellName { get; set; }
    }

    public class WellMasterDataViewModel
    {
        public string wellId { get; set; }

        [Required]
        public string wellName { get; set; }

        public string fieldName { get; set; }

        public string wellType { get; set; }

        public string taskCount { get; set; }

        public string minSchdDate { get; set; }

        public string maxSchdDate { get; set; }

        [Required]
        public WellTypeModel wellTypeId { get; set; }
        public BasinTypeModel Basin_ID { get; set; }
        [Required]
        public string county { get; set; }

        public bool complete_well_drill { get; set; }
        public bool batch_drill_casing { get; set; }
        public bool batch_drill_horizontal { get; set; }
        public bool casing_string { get; set; }

        [Display(Name ="API Number")]
        [Required]
        public string numAPI { get; set; }
        [Required]
        public string numAFE { get; set; }

        public List<WellTypeModel> wellTypes { get; set; }

        public List<RigList> rigList { get; set; }

        public List<PadList> padlist { get; set; }
        public string state { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public string padID { get; set; }
        public string rigID { get; set; }

        public string casingString { get; set; }

        public bool batchFlag { get; set; }
        public string batchDrillingTypeId { get; set; }

        public List<WellTypeModel> batchDrillingTypes { get; set; }

        public string padName { get; set; }
        public string rigName { get; set; }
        public string basin { get; set; }
        [Display(Name = "Chart Color")]
        [Required]
        public string chartColor { get; set; }
        public bool OldPredictionForUpdate { get; set; }
        public bool Prediction { get; set; }
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        public string OperatorName { get; set; }
        //DWOP
        public string ChecklistTemplateId { get; set; }
        public string ChecklistTemplateName { get; set; }
        public DateTime? RigRelease { get; set; }
        public DateTime? SpudWell { get; set; }
        public DateTime? Lastboptest { get; set; }
        public DateTime? NextbopTest { get; set; }
        public string PlannedTd { get; set; }
        public string Router_WellId { get; set; }
    }

    public class WellMasterGroupDataViewModel
    {
        public string wellId { get; set; }

        public string wellName { get; set; }

        public string wellType { get; set; }

        public string taskCount { get; set; }

        public string minSchdDate { get; set; }

        public string maxSchdDate { get; set; }
    }

    public class WellTypeModel
    {
        public string wellTypeId { get; set; }
        public string wellTypeName { get; set; }
    }


    public class RigList
    {
        public string Rig_Id { get; set; }
        public string Rig_Name { get; set; }
    }

    public class PadList
    {
        public string Pad_Id { get; set; }
        public string Pad_Name { get; set; }
    }
    public class WellDataServiceCompanyViewModel
    {
        public string operatingCompanyId { get; set; }

        public string operatingCompanyName { get; set; }

        public string wellId { get; set; }

        public string wellName { get; set; }

        public string wellType { get; set; }

        public string wellTypeId { get; set; }

        public string county { get; set; }

        public string projectID { get; set; }

        public string projectTitle { get; set; }

        public string projectDescription { get; set; }

        public string projectStartDate { get; set; }

        public string projectEndDate { get; set; }

        public string proposalID { get; set; }

        public string jobID { get; set; }

    }

    public class WellAIStatusViewModel
    {
        public string WellId { get; set; }
        public string WellName { get; set; }
        public int? AssociatedTasksCount { get; set; }
        public int? PredictiveTasksCount { get; set; }
        public int? ExemptionTasksCount { get; set; }
    }

    public class BatchAndDrillingTypes
    {
        public string wellTypeId { get; set; }
        public string wellTypeName { get; set; }
    }

    public class BatchDillingType
    {
        public string BatchDrillingType_Id { get; set; }
        public string BatchDrillingType { get; set; }

    }
    public class BasinTypeModel
    {
        public string Basin_ID { get; set; }
        public string BasinType_name { get; set; }
    }

    public class WellsCountModel
    {
        public int RigCounts { get; set; }
        public int PadCounts { get; set; }
        public int WellCounts { get; set; }
        public int DrillingPlanCounts { get; set; }
    }

    public class WellApiData
    {
        public int total_results_count { get; set; }
        public int current_results_count { get; set; }
        public string next_set { get; set; }
        public List<Result> results { get; set; }
        public string error { get; set; }
        public string message { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string name { get; set; }
        public object lease_name { get; set; }
        public object well_number { get; set; }
        public string @operator { get; set; }
        public string api_number { get; set; }
        public string county { get; set; }
        public string state { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string approval_date { get; set; }
        public string error { get; set; }

    }
}