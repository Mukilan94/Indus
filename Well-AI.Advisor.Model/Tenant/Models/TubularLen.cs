using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularLen
    {
        public TubularLen()
        {
            TubularComponent = new HashSet<TubularComponent>();
            TubularConnection = new HashSet<TubularConnection>();
            TubularNozzle = new HashSet<TubularNozzle>();
        }

        [Key]
        public int LenId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Len")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
        [InverseProperty("Len")]
        public virtual ICollection<TubularConnection> TubularConnection { get; set; }
        [InverseProperty("Len")]
        public virtual ICollection<TubularNozzle> TubularNozzle { get; set; }
    }
}
