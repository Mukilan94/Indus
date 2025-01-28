using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreMudGass
    {
        public ConvCoreMudGass()
        {
            ConvCoreGeologyIntervals = new HashSet<ConvCoreGeologyIntervals>();
        }

        [Key]
        public int MudGasId { get; set; }
        public int? GasAvId { get; set; }
        public int? GasPeakId { get; set; }
        public string GasPeakType { get; set; }
        public int? GasBackgndId { get; set; }
        public int? GasConAvId { get; set; }
        public int? GasConMxId { get; set; }
        public int? GasTripGasConTripId { get; set; }

        [ForeignKey(nameof(GasAvId))]
        [InverseProperty(nameof(ConvCoreGasAvs.ConvCoreMudGass))]
        public virtual ConvCoreGasAvs GasAv { get; set; }
        [ForeignKey(nameof(GasBackgndId))]
        [InverseProperty(nameof(ConvCoreGasBackgnds.ConvCoreMudGass))]
        public virtual ConvCoreGasBackgnds GasBackgnd { get; set; }
        [ForeignKey(nameof(GasConAvId))]
        [InverseProperty(nameof(ConvCoreGasConAvs.ConvCoreMudGass))]
        public virtual ConvCoreGasConAvs GasConAv { get; set; }
        [ForeignKey(nameof(GasConMxId))]
        [InverseProperty(nameof(ConvCoreGasConMxs.ConvCoreMudGass))]
        public virtual ConvCoreGasConMxs GasConMx { get; set; }
        [ForeignKey(nameof(GasPeakId))]
        [InverseProperty(nameof(ConvCoreGasPeaks.ConvCoreMudGass))]
        public virtual ConvCoreGasPeaks GasPeak { get; set; }
        [ForeignKey(nameof(GasTripGasConTripId))]
        [InverseProperty(nameof(ConvCoreGasTrips.ConvCoreMudGass))]
        public virtual ConvCoreGasTrips GasTripGasConTrip { get; set; }
        [InverseProperty("MudGas")]
        public virtual ICollection<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
    }
}
