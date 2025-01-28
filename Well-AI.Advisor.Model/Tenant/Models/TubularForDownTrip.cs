using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularForDownTrip
    {
        public TubularForDownTrip()
        {
            TubularJar = new HashSet<TubularJar>();
        }

        [Key]
        public int ForDownTripId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ForDownTrip")]
        public virtual ICollection<TubularJar> TubularJar { get; set; }
    }
}
