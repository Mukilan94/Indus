using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogMudGas
    {
        public MudLogMudGas()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
        }

        [Key]
        public int MudGasId { get; set; }
        public int? GasAvId { get; set; }
        public int? GasPeakId { get; set; }
        public string GasPeakType { get; set; }
        public int? GasBackgndId { get; set; }
        public int? GasConAvId { get; set; }
        public int? GasConMxId { get; set; }
        public int? GasTripId { get; set; }

        [ForeignKey(nameof(GasAvId))]
        [InverseProperty(nameof(MudLogGasAv.MudLogMudGas))]
        public virtual MudLogGasAv GasAv { get; set; }
        [ForeignKey(nameof(GasBackgndId))]
        [InverseProperty(nameof(MudLogGasBackgnd.MudLogMudGas))]
        public virtual MudLogGasBackgnd GasBackgnd { get; set; }
        [ForeignKey(nameof(GasConAvId))]
        [InverseProperty(nameof(MudLogGasConAv.MudLogMudGas))]
        public virtual MudLogGasConAv GasConAv { get; set; }
        [ForeignKey(nameof(GasConMxId))]
        [InverseProperty(nameof(MudLogGasConMx.MudLogMudGas))]
        public virtual MudLogGasConMx GasConMx { get; set; }
        [ForeignKey(nameof(GasPeakId))]
        [InverseProperty(nameof(MudLogGasPeak.MudLogMudGas))]
        public virtual MudLogGasPeak GasPeak { get; set; }
        [ForeignKey(nameof(GasTripId))]
        [InverseProperty(nameof(MudLogGasTrip.MudLogMudGas))]
        public virtual MudLogGasTrip GasTrip { get; set; }
        [InverseProperty("MudGas")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
    }
}
