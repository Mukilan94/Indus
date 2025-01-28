using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    [Serializable]
    public class WellCheckListModel
    {
        [Key]
        [Required]
        [StringLength(40)]
        public string WellCheckListId { get; set; }
        [StringLength(40)]
        public string WellId { get; set; }
        [StringLength(40)]
        public string TenantId { get; set; }
        public string CheckList { get; set; }
    }
    [Serializable]
    public class WellCheckListDetailModel
    {
        public string WellTaskId { get; set; }
        public string WellTaskName { get; set; }
        public byte CheckListStatus { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? Depth { get; set; }
        public int? Duration { get; set; }
        public int? Day { get; set; }
        public TimeSpan? Time { get; set; }
        public string Vendor { get; set; }
        public int Type { get; set; }
        public bool IsBiddable { get; set; }
        public string StageType { get; set; }
        public string ServiceCategory { get; set; }
        public string ServiceDuration { get; set; }
    }

    public class ProjectWellCheckListModel
    {
        public string CheckListId { get; set; }
        public string WellCheckListId { get; set; }
        public string WellId { get; set; }
        public string Well { get; set; }
        public string ProjectId { get; set; }
        public string ProjectIdent { get; set; }
        public string Vendor { get; set; }
        public string VendorName { get; set; }
        public string VendorNumber { get; set; }
        public string WellTaskId { get; set; }
        public string WellTaskName { get; set; }
        public int? Depth { get; set; }
        public int? Duration { get; set; }
        public int? Day { get; set; }
        public DateTime? Time { get; set; }
        public byte CheckListStatus { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public bool IsBiddable { get; set; }
        public string RigId { get; set; }
        public string RigName{ get; set; }
        public string StageType { get; set; }
        public string ServiceCategory { get; set; }

    }

    public class CheclistCounts
    {
        public int TotalTasks { get; set; }
        public int TotalService { get; set; }
        public int TotalSpecial { get; set; }
        public int TotalSupply { get; set; }
    }
}
