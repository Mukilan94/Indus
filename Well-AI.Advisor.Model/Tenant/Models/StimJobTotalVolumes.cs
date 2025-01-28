using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobTotalVolumes
    {
        public StimJobTotalVolumes()
        {
            StimJobJobIntervals = new HashSet<StimJobJobIntervals>();
        }

        [Key]
        public int TotalVolumeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TotalVolume")]
        public virtual ICollection<StimJobJobIntervals> StimJobJobIntervals { get; set; }
    }
}
