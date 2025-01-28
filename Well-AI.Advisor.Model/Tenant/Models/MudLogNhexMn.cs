using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogNhexMn
    {
        public MudLogNhexMn()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int NhexMnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("NhexMn")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
