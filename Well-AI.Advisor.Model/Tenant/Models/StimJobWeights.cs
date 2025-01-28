using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobWeights
    {
        public StimJobWeights()
        {
            StimJobProppants = new HashSet<StimJobProppants>();
            StimJobTubulars = new HashSet<StimJobTubulars>();
        }

        [Key]
        public int WeightId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Weight")]
        public virtual ICollection<StimJobProppants> StimJobProppants { get; set; }
        [InverseProperty("Weight")]
        public virtual ICollection<StimJobTubulars> StimJobTubulars { get; set; }
    }
}
