using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportOilCtg
    {
        public FluidsReportOilCtg()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int OilCtgId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("OilCtg")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
