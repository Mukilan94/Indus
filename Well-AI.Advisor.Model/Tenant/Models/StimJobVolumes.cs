using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobVolumes
    {
        public StimJobVolumes()
        {
            StimJobAdditives = new HashSet<StimJobAdditives>();
        }

        [Key]
        public int VolumeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Volume")]
        public virtual ICollection<StimJobAdditives> StimJobAdditives { get; set; }
    }
}
