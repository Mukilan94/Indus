using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class SuppliesModel
    {
        public string SupplyId { get; set; }
        [Display(Name = "Supply Name")]
        [Required]
        public string SupplyName { get; set; }
        public string Description { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string ErrorMessage { get; set; }
    }

}
