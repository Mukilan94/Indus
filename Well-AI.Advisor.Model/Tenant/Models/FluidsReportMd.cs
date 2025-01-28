using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReportMd
    {
        public FluidsReportMd()
        {
            FluidsReports = new HashSet<FluidsReports>();
        }

        [Key]
        public int MdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Md")]
        public virtual ICollection<FluidsReports> FluidsReports { get; set; }
    }
}
