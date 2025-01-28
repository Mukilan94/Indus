using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("DrillReportWellCR")]
    public partial class DrillReportWellCr
    {
        public DrillReportWellCr()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public string Uid { get; set; }
        public string Name { get; set; }
        [Column("GeodeticCRSId")]
        public int? GeodeticCrsid { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(GeodeticCrsid))]
        [InverseProperty(nameof(DrillReportGeodeticCrs.DrillReportWellCr))]
        public virtual DrillReportGeodeticCrs GeodeticCrs { get; set; }
        [InverseProperty("WellCrsu")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
