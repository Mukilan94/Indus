using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class CategoryTaskModel
    {
        public string CategoryTaskId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string ServiceCategoryName { get; set; }
        public string TaskId { get; set; }
        [Display(Name = "Tasks")]
        public string TaskName { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string ErrorMessage { get; set; }
    }

}
