using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPipeFrictions
    {
        public StimJobPipeFrictions()
        {
            StimJobSteps = new HashSet<StimJobSteps>();
        }

        [Key]
        public int PipeFrictionId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PipeFriction")]
        public virtual ICollection<StimJobSteps> StimJobSteps { get; set; }
    }
}
