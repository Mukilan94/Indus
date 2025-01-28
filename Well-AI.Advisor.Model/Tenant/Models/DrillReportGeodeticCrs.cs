using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("DrillReportGeodeticCRS")]
    public partial class DrillReportGeodeticCrs
    {
        public DrillReportGeodeticCrs()
        {
            DrillReportWellCr = new HashSet<DrillReportWellCr>();
        }

        [Key]
        [Column("GeodeticCRSId")]
        public int GeodeticCrsid { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("GeodeticCrs")]
        public virtual ICollection<DrillReportWellCr> DrillReportWellCr { get; set; }
    }
}
