using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobIds
    {
        public StimJobIds()
        {
            StimJobTubulars = new HashSet<StimJobTubulars>();
        }

        [Key]
        public int IdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Id")]
        public virtual ICollection<StimJobTubulars> StimJobTubulars { get; set; }
    }
}
