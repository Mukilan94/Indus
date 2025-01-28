using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreSizeMxs
    {
        public ConvCoreSizeMxs()
        {
            ConvCoreGeologyIntervals = new HashSet<ConvCoreGeologyIntervals>();
        }

        [Key]
        public int SizeMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SizeMx")]
        public virtual ICollection<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
    }
}
