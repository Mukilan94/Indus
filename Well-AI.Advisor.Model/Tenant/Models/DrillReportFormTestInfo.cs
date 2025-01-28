using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class DrillReportFormTestInfo
    {
        public DrillReportFormTestInfo()
        {
            DrillReports = new HashSet<DrillReports>();
        }

        [Key]
        public int FormTestInfoId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public int? PresPoreId { get; set; }
        public string GoodSeal { get; set; }
        public int? MdSampleId { get; set; }
        public string DominateComponent { get; set; }
        [Column("DensityHCId")]
        public int? DensityHcid { get; set; }
        public int? VolumeSampleId { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(DensityHcid))]
        [InverseProperty(nameof(DrillReportDensityHc.DrillReportFormTestInfo))]
        public virtual DrillReportDensityHc DensityHc { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(DrillReportMd.DrillReportFormTestInfo))]
        public virtual DrillReportMd Md { get; set; }
        [ForeignKey(nameof(MdSampleId))]
        [InverseProperty(nameof(DrillReportMdSample.DrillReportFormTestInfo))]
        public virtual DrillReportMdSample MdSample { get; set; }
        [ForeignKey(nameof(PresPoreId))]
        [InverseProperty(nameof(DrillReportPresPore.DrillReportFormTestInfo))]
        public virtual DrillReportPresPore PresPore { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(DrillReportTvd.DrillReportFormTestInfo))]
        public virtual DrillReportTvd Tvd { get; set; }
        [ForeignKey(nameof(VolumeSampleId))]
        [InverseProperty(nameof(DrillReportVolumeSample.DrillReportFormTestInfo))]
        public virtual DrillReportVolumeSample VolumeSample { get; set; }
        [InverseProperty("FormTestInfo")]
        public virtual ICollection<DrillReports> DrillReports { get; set; }
    }
}
