using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogPropMx
    {
        public MudLogPropMx()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int PropMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PropMx")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
