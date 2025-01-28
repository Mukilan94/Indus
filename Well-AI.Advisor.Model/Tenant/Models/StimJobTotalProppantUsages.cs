using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobTotalProppantUsages
    {
        [Key]
        public int TotalProppantUsageId { get; set; }
        public string Name { get; set; }
        public int? MassId { get; set; }
        public string Uid { get; set; }
        public int? JobIntervalId { get; set; }

        [ForeignKey(nameof(JobIntervalId))]
        [InverseProperty(nameof(StimJobJobIntervals.StimJobTotalProppantUsages))]
        public virtual StimJobJobIntervals JobInterval { get; set; }
        [ForeignKey(nameof(MassId))]
        [InverseProperty(nameof(StimJobMasss.StimJobTotalProppantUsages))]
        public virtual StimJobMasss Mass { get; set; }
    }
}
