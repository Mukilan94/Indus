using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogDolomite
    {
        public MudLogDolomite()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
        }

        [Key]
        public int DolomiteId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Dolomite")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
    }
}
