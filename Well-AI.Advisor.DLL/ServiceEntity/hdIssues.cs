using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.ServiceEntity
{
    [Table("hdIssues")]
    public class hdIssues
    {
       
        [Key]
        [Required]
        public int IssueID { get; set; }
        public int InstanceID { get; set; }
        public DateTime IssueDate { get; set; }  
        public string UserID { get; set; }
        public int CategoryID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ResolvedDate { get; set; }
        public string AssignedToUserID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Boolean UpdatedByUser { get; set; }
        public Boolean UpdatedByPerformer { get; set; }
        public Boolean UpdatedForTechView { get; set; }
        public int StatusID { get; set; }
        public Int16 Priority { get; set; }
        public int TimeSpentInSeconds { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DueDate { get; set; }
        public Int16 Origin { get; set; }
       
        public int TimesReopened { get; set; }
        public string TenantId { get; set; }
    }
}
