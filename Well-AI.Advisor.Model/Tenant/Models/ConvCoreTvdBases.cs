using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreTvdBases
    {
        public ConvCoreTvdBases()
        {
            ConvCoreGeologyIntervals = new HashSet<ConvCoreGeologyIntervals>();
        }

        [Key]
        public int TvdBaseId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdBase")]
        public virtual ICollection<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
    }
}
