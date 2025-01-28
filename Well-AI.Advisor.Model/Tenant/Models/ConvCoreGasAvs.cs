using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreGasAvs
    {
        public ConvCoreGasAvs()
        {
            ConvCoreMudGass = new HashSet<ConvCoreMudGass>();
        }

        [Key]
        public int GasAvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GasAv")]
        public virtual ICollection<ConvCoreMudGass> ConvCoreMudGass { get; set; }
    }
}
