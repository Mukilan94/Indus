using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportGel30Min
    {
        public FluidsReportGel30Min()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int Gel30MinId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Gel30Min")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
