using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPerfFrictions
    {
        public StimJobPerfFrictions()
        {
            StimJobSteps = new HashSet<StimJobSteps>();
        }

        [Key]
        public int PerfFrictionId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PerfFriction")]
        public virtual ICollection<StimJobSteps> StimJobSteps { get; set; }
    }
}
