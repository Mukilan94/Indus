using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class TechnicianTracker
    {
        public string ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string Job { get; set; }
        [Display(Name = "Well")]
        public string WellName { get; set; }
        [Display(Name = "Date Awared")]
        public DateTime DateAwared { get; set; }

        public string OperatorTenantId { get; set; }
        [Display(Name = "Vendor")]
        public string OperatorCompanyName { get; set; }
        [Display(Name = "Name")]
        public string OperatorUserName { get; set; }
        [Display(Name = "Mobile")]
        public string OperatorMobile { get; set; }
        public string ProposalId { get; set; }
        [Display(Name = "Expected Start Date")]
        [DataType(DataType.Date)]
        public DateTime ExpectedStartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Expected End Date")]
        public DateTime ExpectedEndDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Actual Start Date")]
        public DateTime? ActualStartDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Actual End Date")]
        public DateTime? ActualEndDate { get; set; }
        public string Description { get; set; }
        [Display(Name = "Project Status")]
        public int ProjectStatus { get; set; }
        public string ProjectStatusName { get; set; }
        public string Title { get; set; }
        public string WellId { get; set; }

        public string RigName { get; set; }
        public List<WellAI.Advisor.Model.ServiceCompany.Models.ServiceVehicleViewModel> ServiceVehicleViewModels { get; set; }

    }
}
