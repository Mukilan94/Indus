using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("CheckListTemplate")]
    public class CheckListTemplate
    {
        [Key]
        [StringLength(40)]
        public string CheckListTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string WellTypeId { get; set; }
        public string TenantId { get; set; }
        public string Checklist { get; set; }
        public bool IsDefault { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int? BopFrequency { get; set; }
    }
}
