using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogEthMx
    {
        public MudLogEthMx()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int EthMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EthMx")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
