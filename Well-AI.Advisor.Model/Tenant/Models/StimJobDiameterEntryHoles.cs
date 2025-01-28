using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobDiameterEntryHoles
    {
        public StimJobDiameterEntryHoles()
        {
            StimJobStepDownTests = new HashSet<StimJobStepDownTests>();
        }

        [Key]
        public int PipeFrictionId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiameterEntryHolePipeFriction")]
        public virtual ICollection<StimJobStepDownTests> StimJobStepDownTests { get; set; }
    }
}
