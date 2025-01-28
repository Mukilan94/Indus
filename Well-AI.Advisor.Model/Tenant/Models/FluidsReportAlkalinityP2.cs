using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportAlkalinityP2
    {
        public FluidsReportAlkalinityP2()
        {
            FluidsReportFluid = new HashSet<FluidsReportFluid>();
        }

        [Key]
        [Column("AlkalinityP2Id")]
        public int AlkalinityP2id { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("AlkalinityP2")]
        public virtual ICollection<FluidsReportFluid> FluidsReportFluid { get; set; }
    }
}
