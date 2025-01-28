using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularBendStiffness
    {
        public TubularBendStiffness()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("BendStiffness")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
