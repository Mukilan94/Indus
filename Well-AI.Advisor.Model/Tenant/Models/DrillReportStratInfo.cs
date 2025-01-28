using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportStratInfo
    {
        public DrillReportStratInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int StratInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdTopId { get; set; }
        public int? TvdTopDrillReportTvdTopId { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(DrillReportMdTop.DrillReportStratInfo))]
        public virtual DrillReportMdTop MdTop { get; set; }
        [ForeignKey(nameof(TvdTopDrillReportTvdTopId))]
        [InverseProperty(nameof(DrillReportTvdTop.DrillReportStratInfo))]
        public virtual DrillReportTvdTop TvdTopDrillReportTvdTop { get; set; }
        [InverseProperty("StratInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
