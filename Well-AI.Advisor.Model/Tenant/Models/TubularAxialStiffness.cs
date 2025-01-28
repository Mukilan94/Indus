using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularAxialStiffness
    {
        public TubularAxialStiffness()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int Id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AxialStiffness")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
