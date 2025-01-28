using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportCalciumChloride
    {
        public FluidsReportCalciumChloride()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        public int CalciumChlorideId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CalciumChloride")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
