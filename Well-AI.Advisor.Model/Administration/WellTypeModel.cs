using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class WellTypeModel
    {
        public string WellTypeId { get; set; }
        [Display(Name ="Well Type")]
        [Required]
        public string WellTypeName { get; set; }
        public string ErrorMessage { get; set; }
        public string CheckList { get; set; }
    }
}
