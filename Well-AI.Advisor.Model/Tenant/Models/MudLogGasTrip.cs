using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogGasTrip
    {
        public MudLogGasTrip()
        {
            MudLogMudGas = new HashSet<MudLogMudGas>();
        }

        [Key]
        public int GasTripId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GasTrip")]
        public virtual ICollection<MudLogMudGas> MudLogMudGas { get; set; }
    }
}
