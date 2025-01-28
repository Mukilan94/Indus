using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreDensShales
    {
        public ConvCoreDensShales()
        {
            ConvCoreGeologyIntervals = new HashSet<ConvCoreGeologyIntervals>();
            ConvCoreLithologys = new HashSet<ConvCoreLithologys>();
        }

        [Key]
        public int DensShaleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DensShale")]
        public virtual ICollection<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
        [InverseProperty("DensShale")]
        public virtual ICollection<ConvCoreLithologys> ConvCoreLithologys { get; set; }
    }
}
