using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogNbutMx
    {
        public MudLogNbutMx()
        {
            MudLogChromatograph = new HashSet<MudLogChromatograph>();
        }

        [Key]
        public int NbutMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("NbutMx")]
        public virtual ICollection<MudLogChromatograph> MudLogChromatograph { get; set; }
    }
}
