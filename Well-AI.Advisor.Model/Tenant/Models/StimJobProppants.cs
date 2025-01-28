using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobProppants
    {
        public StimJobProppants()
        {
            StimJobStageFluids = new HashSet<StimJobStageFluids>();
        }

        [Key]
        public int ProppantId { get; set; }
        public string Name { get; set; }
        public string Kind { get; set; }
        public int? WeightId { get; set; }
        public string SieveSize { get; set; }

        [ForeignKey(nameof(WeightId))]
        [InverseProperty(nameof(StimJobWeights.StimJobProppants))]
        public virtual StimJobWeights Weight { get; set; }
        [InverseProperty("Proppant")]
        public virtual ICollection<StimJobStageFluids> StimJobStageFluids { get; set; }
    }
}
