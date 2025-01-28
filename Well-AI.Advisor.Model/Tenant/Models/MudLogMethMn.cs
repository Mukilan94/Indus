using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogMethMn
    {
        public MudLogMethMn()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int MethMnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MethMn")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
