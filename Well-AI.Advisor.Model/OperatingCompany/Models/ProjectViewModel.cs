using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{

    public class ProjectDashboardOperViewModel
    {
       [Display(Name ="Last Month")]
       public int ProjectsStartedLastMonthCount { get; set; }
       [Display(Name ="This Month")]
       public int ProjectsStartedThisMonthCount { get; set; }

        [Display(Name ="Active")]
       public int ProjectActiveCount { get; set; }

        [Display(Name = "Suspended")]
       public int ProjectSuspendedCount { get; set; }

        [Display(Name ="Last Month")]
       public int ProjectsAwardedLastMonthCount { get; set; }
        [Display(Name ="This Month")]
       public int ProjectsAwardedThisMonthCount { get; set; }
    }
    public class ProjectViewModel
    {
        public string ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string Job { get; set; }
        [Display(Name = "Well")]
        public string WellName { get; set; }
        [Display(Name = "Date Awared")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        public DateTime DateAwared { get; set; }

        public string OperatorTenantId { get; set; }
        [Display(Name = "Provider")]
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
        public string Title { get; set; }
        public string WellId { get; set; }
        [Display(Name ="Rig")]
        public string RigName { get; set; }

        public string RigId { get; set; }
        public string CheckIds { get; set; }
        [Display(Name = "Service Company")]
        public string SRVTenantId { get; set; }
        public AddAuctionProposalViewModel AddAuction { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int Month { get; set; }
        public DateTime CreatedMonths { get; set; }
        public int? Depth { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifyDate { get; set; }

    }

    public class FieldTicket
    {
        public int fdId
        {
            get;
            set;
        }
        public int ProjectID
        {
            get;
            set;
        }
        public string Ticket
        {
            get;
            set;
        }
        public string Invoice
        {
            get;
            set;
        }

        public string Rig { get; set; }
        public string Lease { get; set; }
        public string PoAfe { get; set; }
        public string County { get; set; }
        public string BillTo { get; set; }

        public string ItemsDescription { get; set; }
        public List<TicketFieldItem> Items;
        public double Subtotal { get; set; }
        public double SalesTaxCount { get; set; }
        public double SalesTaxValue { get; set; }
        public double Total { get; set; }

        public DateTime? Date
        {
            get;
            set;
        }

        public double Amount
        {
            get;
            set;
        }
    }

    public class TicketFieldItem
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
