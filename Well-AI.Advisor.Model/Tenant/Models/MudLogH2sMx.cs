using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogH2sMx
    {
        public MudLogH2sMx()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int H2sMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("H2sMx")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
