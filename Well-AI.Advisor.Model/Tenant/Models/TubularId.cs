using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularId
    {
        public TubularId()
        {
            TubularComponent = new HashSet<TubularComponent>();
            TubularConnection = new HashSet<TubularConnection>();
        }

        [Key]
        public int TubularIdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("IdTubularId")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
        [InverseProperty("IdTubularId")]
        public virtual ICollection<TubularConnection> TubularConnection { get; set; }
    }
}
