using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("StimJobStartFoamRateCO2s")]
    public partial class StimJobStartFoamRateCo2s
    {
        public StimJobStartFoamRateCo2s()
        {
            StimJobJobStages = new HashSet<StimJobJobStages>();
        }

        [Key]
        [Column("StartFoamRateCO2Id")]
        public int StartFoamRateCo2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StartFoamRateCo2")]
        public virtual ICollection<StimJobJobStages> StimJobJobStages { get; set; }
    }
}
