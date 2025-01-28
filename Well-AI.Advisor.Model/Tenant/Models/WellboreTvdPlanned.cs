using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellboreTvdPlanned
    {
        public WellboreTvdPlanned()
        {
            WellBores = new HashSet<WellBores>();
        }

        [Key]
        public int TvdPlannedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdPlanned")]
        public virtual ICollection<WellBores> WellBores { get; set; }
    }
}
