using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobTotalCO2Masss")]
    public partial class StimJobTotalCo2masss
    {
        public StimJobTotalCo2masss()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        [Column("TotalCO2MassId")]
        public int TotalCo2massId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TotalCo2mass")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
        [InverseProperty("TotalCo2mass")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
