using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogLithostratigraphic
    {
        public MudLogLithostratigraphic()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
        }

        [Key]
        public int LithostratigraphicId { get; set; }
        public string Kind { get; set; }
        public string Text { get; set; }

        [InverseProperty("Lithostratigraphic")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
    }
}
