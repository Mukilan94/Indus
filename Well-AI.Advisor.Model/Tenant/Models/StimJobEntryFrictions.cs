using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobEntryFrictions
    {
        public StimJobEntryFrictions()
        {
            StimJobSteps = new HashSet<StimJobSteps>();
        }

        [Key]
        public int EntryFrictionId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EntryFriction")]
        public virtual ICollection<StimJobSteps> StimJobSteps { get; set; }
    }
}
