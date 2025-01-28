using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellboreTvdSubSeaPlanned
    {
        public WellboreTvdSubSeaPlanned()
        {
            WellBores = new HashSet<WellBores>();
        }

        [Key]
        public int TvdSubSeaPlannedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdSubSeaPlanned")]
        public virtual ICollection<WellBores> WellBores { get; set; }
    }
}
