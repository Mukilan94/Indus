using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class ProjectDashboardSerViewModel
    {
        [Display(Name = "Last Month")]
        public int ProjectsStartedLastMonthCount { get; set; }
        [Display(Name = "This Month")]
        public int ProjectsStartedThisMonthCount { get; set; }

        [Display(Name = "Active")]
        public int ProjectActiveCount { get; set; }

        [Display(Name = "Last Month")]
        public int ProjectsAwardedLastMonthCount { get; set; }
        [Display(Name = "This Month")]
        public int ProjectsAwardedThisMonthCount { get; set; }

        public int ProjectSuspendedCount { get; set; }
    }
    public class ProjectViewSRVModel
    {

        public string  ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string Job { get; set; }
        [Display(Name = "Well")]
        public string WellName { get; set; }
        [Display(Name = "Date Awarded")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime DateAwared { get; set; }

        public string OperatorTenantId { get; set; }
        [Display(Name = "Operator")]
        public string OperatorCompanyName { get; set; }
        [Display(Name = "Name")]
        public string OperatorUserName { get; set; }
        [Display(Name = "Mobile")]
        public string OperatorMobile { get; set; }
        public string ProposalId { get; set; }

        [Display(Name = "Expected Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime ExpectedStartDate { get; set; }
        [Display(Name = "Expected End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime ExpectedEndDate { get; set; }
        [Display(Name = "Actual Start Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime? ActualStartDate { get; set; }
        [Display(Name = "Actual End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime? ActualEndDate { get; set; }
        public string Description { get; set; }     
        [Display(Name = "Project Status")]
        public int ProjectStatus { get; set; }
        public string ProjectStatusName { get; set; }
        public string  Title { get; set; }
        [Display(Name = "Rig")]
        public string RigName { get; set; }
        public double? ProjectDuration { get; set; }
        public string JobName { get; set; }
        public int? Depth { get; set;}

        [Display(Name = "Modified Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifyDate { get; set; }

    }
    public enum OngoingProjectStatusList
    {
        UpCommingProject = 0,
        OnGoingProjects =1,
        CloseProject=2,
        SuspendProject = 3,

    }

    public class TechnicianViewModel
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        [Display(Name = "Vehicle")]
        public string ServiceVehicleId { get; set; }
        public string TechWorkingStatus { get; set; }
        
        [Display(Name = "Name")]
        public string TechName { get; set; }
        public string TechUserId { get; set; }
        public string TechMobile { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Assigned Start")]
        public DateTime TechAssignStartDate { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Assigned End")]
        public DateTime TechAssignEndDate { get; set; }
        public string Notes { get; set; }
        public string Samsaraid { get; set; }
    }

    public class FieldTicketSRV
    {
        [ScaffoldColumn(false)]
        public int fdId { get; set; }
        public int ProjectID { get; set; }
        public string Ticket { get; set; }
        public string Invoice { get; set; }
        public DateTime? Date { get; set; }
        public decimal Amount { get; set; }
        public string Rig { get; set; }
        public string Lease { get; set; }
        public string PoAfe { get; set; }
        public string County { get; set; }
        public string PDFPath { get; set; }
        public string BillTo { get; set; }
        public string ItemsDescription { get; set; }
        public List<TicketFieldItemSRV> Items;
        [Display(Name = "Subtotal")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue)]
        public double Subtotal { get; set; }
        public double SalesTaxCount { get; set; }
        public double SalesTaxValue { get; set; }
        public double Total { get; set; }
    }

    public class TicketFieldItemSRV
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public double Qty { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
    }
}
