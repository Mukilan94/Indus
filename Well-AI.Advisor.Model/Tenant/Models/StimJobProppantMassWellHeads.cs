using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobProppantMassWellHeads
    {
        public StimJobProppantMassWellHeads()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        public int ProppantMassWellHeadId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ProppantMassWellHead")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
