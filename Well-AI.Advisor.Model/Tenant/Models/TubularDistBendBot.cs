using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDistBendBot
    {
        public TubularDistBendBot()
        {
            TubularBend = new HashSet<TubularBend>();
        }

        [Key]
        public int DistBendBotId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DistBendBot")]
        public virtual ICollection<TubularBend> TubularBend { get; set; }
    }
}
