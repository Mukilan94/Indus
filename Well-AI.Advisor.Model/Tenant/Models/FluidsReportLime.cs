using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportLime
    {
        public FluidsReportLime()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int LimeId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Lime")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
