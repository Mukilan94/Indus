using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobVolumeFactors
    {
        public StimJobVolumeFactors()
        {
            StimJobTubulars = new HashSet<StimJobTubulars>();
        }

        [Key]
        public int VolumeFactorId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("VolumeFactor")]
        public virtual ICollection<StimJobTubulars> StimJobTubulars { get; set; }
    }
}
