using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreGasConMxs
    {
        public ConvCoreGasConMxs()
        {
            ConvCoreMudGass = new HashSet<ConvCoreMudGass>();
        }

        [Key]
        public int GasConMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GasConMx")]
        public virtual ICollection<ConvCoreMudGass> ConvCoreMudGass { get; set; }
    }
}
