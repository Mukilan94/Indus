using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogDensShale
    {
        public MudLogDensShale()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
            MudLogLithology = new HashSet<MudLogLithology>();
        }

        [Key]
        public int DensShaleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DensShale")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
        [InverseProperty("DensShale")]
        public virtual ICollection<MudLogLithology> MudLogLithology { get; set; }
    }
}
