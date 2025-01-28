using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobPropMasss
    {
        public StimJobPropMasss()
        {
            StimJobFlowPaths = new HashSet<StimJobFlowPaths>();
        }

        [Key]
        public int PropMassId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PropMass")]
        public virtual ICollection<StimJobFlowPaths> StimJobFlowPaths { get; set; }
    }
}
