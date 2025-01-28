using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogDensBulk
    {
        public MudLogDensBulk()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
        }

        [Key]
        public int DensBulkId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DensBulk")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
    }
}
