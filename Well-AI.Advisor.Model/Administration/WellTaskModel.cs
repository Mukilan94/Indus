using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class WellTaskModel
    {
        public string WellTaskId { get; set; }
        [Required]
        public string WellTypeId { get; set; }
        [Display(Name ="Well Type")]
        public string WellType { get; set; }
        [Required]
        public string TaskId { get; set; }
        [Display(Name ="Task")]
        public string TaskName { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string ErrorMessage { get; set; }
    }

    [Serializable]
    public class AddWellTask
    {
        public List<string> TaskId { get; set; }
        public string WellTypeId { get; set; }
    }

}
