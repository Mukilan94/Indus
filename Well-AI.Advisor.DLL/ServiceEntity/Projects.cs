using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.ServiceEntity
{
    [Table("Projects")]
    public class Projects
    {
        [Key]
        [StringLength(40)]
        public string ID { get; set; }
        [StringLength(50)]
        public string ProjectID { get; set; }
        public DateTime? ActualEnd { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ModifyDate { get; set; }

        [StringLength(40)]
        public string ProposalID { get; set; }
        public string ProjectDescription { get; set; }
        [StringLength(50)]
        public string OprTenantID { get; set; }
        [StringLength(50)]
        public string CreateById { get; set; }
        [StringLength(40)]
        public string BidID { get; set; }
        [StringLength(150)]
        public string ServiceCompID { get; set; }
        [StringLength(50)]
        public int ProjectStatus { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectSummary { get; set; }
        public DateTime? ProposedStartDate { get; set; }
        public string WellID { get; set; }

    }
}
