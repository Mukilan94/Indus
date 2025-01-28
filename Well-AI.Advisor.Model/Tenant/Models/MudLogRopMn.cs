using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogRopMn
    {
        public MudLogRopMn()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
        }

        [Key]
        public int RopMnId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RopMn")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
    }
}
