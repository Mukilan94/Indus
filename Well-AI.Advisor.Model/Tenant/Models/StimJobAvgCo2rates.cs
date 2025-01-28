using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobAvgCO2Rates")]
    public partial class StimJobAvgCo2rates
    {
        public StimJobAvgCo2rates()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("AvgCO2RateId")]
        public int AvgCo2rateId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AvgCo2rate")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
