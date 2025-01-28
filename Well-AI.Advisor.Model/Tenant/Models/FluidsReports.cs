using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FluidsReports
    {
        [Key]
        public int FluidsReportId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdId { get; set; }
        public int? TvdId { get; set; }
        public string NumReport { get; set; }
        public string FluidUid { get; set; }
        public int? CommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataId))]
        [InverseProperty(nameof(FluidsReportCommonDatas.FluidsReports))]
        public virtual FluidsReportCommonDatas CommonData { get; set; }
        [ForeignKey(nameof(FluidUid))]
        [InverseProperty(nameof(FluidsReportFluid.FluidsReports))]
        public virtual FluidsReportFluid FluidU { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(FluidsReportMd.FluidsReports))]
        public virtual FluidsReportMd Md { get; set; }
        [ForeignKey(nameof(TvdId))]
        [InverseProperty(nameof(FluidsReportTvd.FluidsReports))]
        public virtual FluidsReportTvd Tvd { get; set; }
    }
}
