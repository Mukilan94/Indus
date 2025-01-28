using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportHardnessCa
    {
        public FluidsReportHardnessCa()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int HardnessCaId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("HardnessCa")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
