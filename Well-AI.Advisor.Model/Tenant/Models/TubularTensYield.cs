using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularTensYield
    {
        public TubularTensYield()
        {
            TubularComponent = new HashSet<TubularComponent>();
            TubularConnection = new HashSet<TubularConnection>();
        }

        [Key]
        public int TensYieldId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TensYield")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
        [InverseProperty("TensYield")]
        public virtual ICollection<TubularConnection> TubularConnection { get; set; }
    }
}
