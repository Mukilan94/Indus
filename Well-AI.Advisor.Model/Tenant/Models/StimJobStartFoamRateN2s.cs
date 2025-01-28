using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class StimJobStartFoamRateN2s
    {
        public StimJobStartFoamRateN2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("StartFoamRateN2Id")]
        public int StartFoamRateN2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StartFoamRateN2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
