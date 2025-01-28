using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogGasConAv
    {
        public MudLogGasConAv()
        {
            MudLogMudGas = new HashSet<MudLogMudGas>();
        }

        [Key]
        public int GasConAvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("GasConAv")]
        public virtual ICollection<MudLogMudGas> MudLogMudGas { get; set; }
    }
}
