using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportWaterPc
    {
        public FluidsReportWaterPc()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int WaterPcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("WaterPc")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
