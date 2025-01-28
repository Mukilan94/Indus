using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreCecs
    {
        public ConvCoreCecs()
        {
            ConvCoreGeologyIntervals = new HashSet<ConvCoreGeologyIntervals>();
        }

        [Key]
        public int CecId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Cec")]
        public virtual ICollection<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
    }
}
