using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobOds
    {
        public StimJobOds()
        {
            StimJobTubulars = new HashSet<StimJobTubulars>();
        }

        [Key]
        public int OdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Od")]
        public virtual ICollection<StimJobTubulars> StimJobTubulars { get; set; }
    }
}
