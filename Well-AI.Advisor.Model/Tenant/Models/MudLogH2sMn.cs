using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogH2sMn
    {
        public MudLogH2sMn()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int H2sMnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("H2sMn")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
