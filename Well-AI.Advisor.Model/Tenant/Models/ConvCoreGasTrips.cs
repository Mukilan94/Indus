using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreGasTrips
    {
        public ConvCoreGasTrips()
        {
            ConvCoreMudGass = new HashSet<ConvCoreMudGass>();
        }

        [Key]
        public int GasConTripId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GasTripGasConTrip")]
        public virtual ICollection<ConvCoreMudGass> ConvCoreMudGass { get; set; }
    }
}
