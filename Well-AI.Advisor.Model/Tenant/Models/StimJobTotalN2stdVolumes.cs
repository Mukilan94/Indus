using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobTotalN2StdVolumes")]
    public partial class StimJobTotalN2stdVolumes
    {
        public StimJobTotalN2stdVolumes()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
            StimJobs = new HashSet<StimJobs>();
        }

        [Key]
        [Column("TotalN2StdVolumeId")]
        public int TotalN2stdVolumeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TotalN2stdVolume")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
        [InverseProperty("TotalN2stdVolume")]
        public virtual ICollection<StimJobs> StimJobs { get; set; }
    }
}
