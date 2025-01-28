using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellboreTvdKickoff
    {
        public WellboreTvdKickoff()
        {
            WellBores = new HashSet<WellBores>();
        }

        [Key]
        public int TvdKickoffId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdKickoff")]
        public virtual ICollection<WellBores> WellBores { get; set; }
    }
}
