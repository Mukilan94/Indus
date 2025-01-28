using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobPresHelds
    {
        public CementJobPresHelds()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int PresHeldId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PresHeld")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
