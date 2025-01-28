using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularOdBladeMx
    {
        public TubularOdBladeMx()
        {
            TubularStabilizer = new HashSet<TubularStabilizer>();
        }

        [Key]
        public int OdBladeMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OdBladeMx")]
        public virtual ICollection<TubularStabilizer> TubularStabilizer { get; set; }
    }
}
