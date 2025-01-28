using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobEndFoamRateCO2s")]
    public partial class StimJobEndFoamRateCo2s
    {
        public StimJobEndFoamRateCo2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("EndFoamRateCO2Id")]
        public int EndFoamRateCo2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndFoamRateCo2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
