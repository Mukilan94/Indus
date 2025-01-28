using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportPersonnels
    {
        [Key]
        public int PersonnelId { get; set; }
        public string Company { get; set; }
        public string TypeService { get; set; }
        public string NumPeople { get; set; }
        public int? TotalTimeId { get; set; }
        public string Uid { get; set; }
        public int? OpsReportId { get; set; }

        [ForeignKey(nameof(OpsReportId))]
        [InverseProperty(nameof(OpsReports.OpsReportPersonnels))]
        public virtual OpsReports OpsReport { get; set; }
        [ForeignKey(nameof(TotalTimeId))]
        [InverseProperty(nameof(OpsReportTotalTimes.OpsReportPersonnels))]
        public virtual OpsReportTotalTimes TotalTime { get; set; }
    }
}
