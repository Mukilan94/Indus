using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportSolCorPc
    {
        public FluidsReportSolCorPc()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int SolCorPcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("SolCorPc")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
