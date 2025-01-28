using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportBitRecord
    {
        public DrillReportBitRecord()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int Id { get; set; }
        public string NumBit { get; set; }
        public int? DiaBitId { get; set; }
        public string Manufacturer { get; set; }
        public string CodeMfg { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(DiaBitId))]
        [InverseProperty(nameof(DrillReportDiaBit.DrillReportBitRecord))]
        public virtual DrillReportDiaBit DiaBit { get; set; }
        [InverseProperty("BitRecord")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
