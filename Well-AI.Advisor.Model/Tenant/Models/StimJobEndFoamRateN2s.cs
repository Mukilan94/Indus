using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobEndFoamRateN2s
    {
        public StimJobEndFoamRateN2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("EndFoamRateN2Id")]
        public int EndFoamRateN2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndFoamRateN2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
