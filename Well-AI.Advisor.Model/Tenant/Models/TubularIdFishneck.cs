using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularIdFishneck
    {
        public TubularIdFishneck()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int IdFishneckId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("IdFishneck")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
