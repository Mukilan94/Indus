using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class SalesModel
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string Custname { get; set; }

        [Display(Name = "IsQualified")]
        public bool IsQualified { get; set; }

        [Display(Name = "IsConverted")]
        public bool IsConverted { get; set; }

        [Display(Name = "Channel")]
        public string Channel { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Planned date of delivery")]
        [DataType(DataType.Date)]
        public DateTime Planneddateofdelivery { get; set; }

        [Display(Name = "Payment Terms")]
        public string PaymentTerms { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "Availability delay")]
        public string Availabilitydelay { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Estimated Revenue")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue)]
        public decimal  EstimatedRevenue { get; set; }

    }
}

