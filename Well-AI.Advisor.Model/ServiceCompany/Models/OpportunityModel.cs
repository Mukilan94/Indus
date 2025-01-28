using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class OpportunityModel
    {

        [Display(Name = "Created Date & Time")]
        [Required]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Probablity")]
        public string Probablity { get; set; }

        [Display(Name = "Estimated Closing Date")]
        [Required]
        public DateTime EstimatedClosingDate { get; set; }

        [Display(Name = "Estimated Revenue")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue)]
        public int EstimatedRevenue { get; set; }

        [Display(Name = "Opportunity Name")]
        public string OpportunityName { get; set; }

        public string CompanyID { get; set; }

        public CompanyModel CompanyModel { get; set; }

    }
}

