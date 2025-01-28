using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDistBladeBot
    {
        public TubularDistBladeBot()
        {
            TubularStabilizer = new HashSet<TubularStabilizer>();
        }

        [Key]
        public int DistBladeBotId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DistBladeBot")]
        public virtual ICollection<TubularStabilizer> TubularStabilizer { get; set; }
    }
}
