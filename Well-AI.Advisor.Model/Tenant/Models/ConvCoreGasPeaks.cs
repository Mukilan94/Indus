using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreGasPeaks
    {
        public ConvCoreGasPeaks()
        {
            ConvCoreMudGass = new HashSet<ConvCoreMudGass>();
        }

        [Key]
        public int GasPeakId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GasPeak")]
        public virtual ICollection<ConvCoreMudGass> ConvCoreMudGass { get; set; }
    }
}
