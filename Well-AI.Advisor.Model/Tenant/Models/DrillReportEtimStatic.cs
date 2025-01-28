using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("DrillReportETimStatic")]
    public partial class DrillReportEtimStatic
    {
        public DrillReportEtimStatic()
        {
            DrillReportLogInfo = new HashSet<DrillReportLogInfo>();
        }

        [Key]
        [Column("ETimStaticId")]
        public int EtimStaticId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimStatic")]
        public virtual ICollection<DrillReportLogInfo> DrillReportLogInfo { get; set; }
    }
}
