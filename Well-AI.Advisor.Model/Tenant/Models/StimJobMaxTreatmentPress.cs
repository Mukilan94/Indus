using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobMaxTreatmentPress
    {
        public StimJobMaxTreatmentPress()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
        }

        [Key]
        public int MaxTreatmentPresId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxTreatmentPres")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
    }
}
