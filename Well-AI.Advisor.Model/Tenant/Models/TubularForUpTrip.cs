using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularForUpTrip
    {
        public TubularForUpTrip()
        {
            TubularJar = new HashSet<TubularJar>();
        }

        [Key]
        public int ForUpTripId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ForUpTrip")]
        public virtual ICollection<TubularJar> TubularJar { get; set; }
    }
}
