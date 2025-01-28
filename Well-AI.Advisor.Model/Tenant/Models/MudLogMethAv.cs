using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogMethAv
    {
        public MudLogMethAv()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int MethAvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MethAv")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
