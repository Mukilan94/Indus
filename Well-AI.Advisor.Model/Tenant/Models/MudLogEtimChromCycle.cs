using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("MudLogETimChromCycle")]
    public partial class MudLogEtimChromCycle
    {
        public MudLogEtimChromCycle()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        [Column("ETimChromCycleId")]
        public int EtimChromCycleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimChromCycle")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
