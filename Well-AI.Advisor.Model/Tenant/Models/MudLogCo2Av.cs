using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogCo2Av
    {
        public MudLogCo2Av()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int Co2AvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Co2Av")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
