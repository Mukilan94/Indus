using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularTorsionalStiffness
    {
        public TubularTorsionalStiffness()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int TorsionalStiffnessId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TorsionalStiffness")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
