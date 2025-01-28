using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    public class CompanyModel
    {
        public string CompanyID { get; set; }

        [Display(Name = "Company name")]
        [Required]
        public string name { get; set; }

        [Display(Name = "Web Site")]
        public string Website { get; set; }

        [Display(Name = "Phone")]
        [Required]
        public string phone { get; set; }

        [Display(Name = "Mobile Phone")]
        public string mobilephone { get; set; }

        [Display(Name = "Address 1")]
        public string address1 { get; set; }

        [Display(Name = "Address 2")]
        public string address2 { get; set; }

        [Display(Name = "City")]
        public string city { get; set; }

        [Display(Name = "State")]
        public string state { get; set; }

        [Display(Name = "Country")]
        public string country { get; set; }

        [Display(Name = "Postal Code")]
        public string postalcode { get; set; }

        [Display(Name = "Comments")]
        public string comments { get; set; }

        [Display(Name = "FAX")]
        public string fax { get; set; }
    }
}

