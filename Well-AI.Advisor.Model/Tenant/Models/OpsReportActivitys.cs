using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportActivitys
    {
        public OpsReportActivitys()
        {
            OpsReports = new HashSet<OpsReports>();
        }

        [Key]
        public string Uid { get; set; }
        [Column("DTimStart")]
        public string DtimStart { get; set; }
        [Column("DTimEnd")]
        public string DtimEnd { get; set; }
        public int? DurationId { get; set; }
        public string Phase { get; set; }
        public string ActivityCode { get; set; }
        public string DetailActivity { get; set; }
        public string TypeActivityClass { get; set; }
        public int? MdHoleStartId { get; set; }
        public int? TvdHoleStartId { get; set; }
        public int? MdHoleEndId { get; set; }
        public int? TvdHoleEndId { get; set; }
        public int? MdBitStartId { get; set; }
        public int? MdBitEndId { get; set; }
        public string State { get; set; }
        public string Operator { get; set; }
        public string Optimum { get; set; }
        public string Productive { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [ForeignKey(nameof(DurationId))]
        [InverseProperty(nameof(OpsReportDurations.OpsReportActivitys))]
        public virtual OpsReportDurations Duration { get; set; }
        [ForeignKey(nameof(MdBitEndId))]
        [InverseProperty(nameof(OpsReportMdBitEnds.OpsReportActivitys))]
        public virtual OpsReportMdBitEnds MdBitEnd { get; set; }
        [ForeignKey(nameof(MdBitStartId))]
        [InverseProperty(nameof(OpsReportMdBitStarts.OpsReportActivitys))]
        public virtual OpsReportMdBitStarts MdBitStart { get; set; }
        [ForeignKey(nameof(MdHoleEndId))]
        [InverseProperty(nameof(OpsReportMdHoleEnds.OpsReportActivitys))]
        public virtual OpsReportMdHoleEnds MdHoleEnd { get; set; }
        [ForeignKey(nameof(MdHoleStartId))]
        [InverseProperty(nameof(OpsReportMdHoleStarts.OpsReportActivitys))]
        public virtual OpsReportMdHoleStarts MdHoleStart { get; set; }
        [ForeignKey(nameof(TvdHoleEndId))]
        [InverseProperty(nameof(OpsReportTvdHoleEnds.OpsReportActivitys))]
        public virtual OpsReportTvdHoleEnds TvdHoleEnd { get; set; }
        [ForeignKey(nameof(TvdHoleStartId))]
        [InverseProperty(nameof(OpsReportTvdHoleStarts.OpsReportActivitys))]
        public virtual OpsReportTvdHoleStarts TvdHoleStart { get; set; }
        [InverseProperty("ActivityU")]
        public virtual ICollection<OpsReports> OpsReports { get; set; }
    }
}
