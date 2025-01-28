using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogChronostratigraphic
    {
        [Key]
        public int ChronostratigraphicId { get; set; }
        public string Kind { get; set; }
        public string Text { get; set; }
        public int? MudLogGeologyIntervalGeologyIntervalId { get; set; }

        [ForeignKey(nameof(MudLogGeologyIntervalGeologyIntervalId))]
        [InverseProperty(nameof(MudLogGeologyInterval.MudLogChronostratigraphic))]
        public virtual MudLogGeologyInterval MudLogGeologyIntervalGeologyInterval { get; set; }
    }
}
