using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogWtMudOut
    {
        public MudLogWtMudOut()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int WtMudOutId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("WtMudOut")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
