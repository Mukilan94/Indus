using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularOd
    {
        public TubularOd()
        {
            TubularComponent = new HashSet<TubularComponent>();
            TubularConnection = new HashSet<TubularConnection>();
        }

        [Key]
        public int OdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Od")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
        [InverseProperty("Od")]
        public virtual ICollection<TubularConnection> TubularConnection { get; set; }
    }
}
