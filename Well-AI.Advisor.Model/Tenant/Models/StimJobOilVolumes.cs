using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobOilVolumes
    {
        public StimJobOilVolumes()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int OilVolumeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OilVolume")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
