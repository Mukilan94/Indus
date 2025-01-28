using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportRawDatas
    {
        public OpsReportRawDatas()
        {
            OpsReportTrajectoryStations = new HashSet<OpsReportTrajectoryStations>();
        }

        [Key]
        public int RawDataId { get; set; }
        public int? GravAxialRawId { get; set; }
        public int? GravTran1RawId { get; set; }
        public int? GravTran2RawId { get; set; }
        public int? MagAxialRawId { get; set; }
        public int? MagTran1RawId { get; set; }
        public int? MagTran2RawId { get; set; }

        [ForeignKey(nameof(GravAxialRawId))]
        [InverseProperty(nameof(OpsReportGravAxialRaws.OpsReportRawDatas))]
        public virtual OpsReportGravAxialRaws GravAxialRaw { get; set; }
        [ForeignKey(nameof(GravTran1RawId))]
        [InverseProperty(nameof(OpsReportGravTran1Raws.OpsReportRawDatas))]
        public virtual OpsReportGravTran1Raws GravTran1Raw { get; set; }
        [ForeignKey(nameof(GravTran2RawId))]
        [InverseProperty(nameof(OpsReportGravTran2Raws.OpsReportRawDatas))]
        public virtual OpsReportGravTran2Raws GravTran2Raw { get; set; }
        [ForeignKey(nameof(MagAxialRawId))]
        [InverseProperty(nameof(OpsReportMagAxialRaws.OpsReportRawDatas))]
        public virtual OpsReportMagAxialRaws MagAxialRaw { get; set; }
        [ForeignKey(nameof(MagTran1RawId))]
        [InverseProperty(nameof(OpsReportMagTran1Raws.OpsReportRawDatas))]
        public virtual OpsReportMagTran1Raws MagTran1Raw { get; set; }
        [ForeignKey(nameof(MagTran2RawId))]
        [InverseProperty(nameof(OpsReportMagTran2Raws.OpsReportRawDatas))]
        public virtual OpsReportMagTran2Raws MagTran2Raw { get; set; }
        [InverseProperty("RawData")]
        public virtual ICollection<OpsReportTrajectoryStations> OpsReportTrajectoryStations { get; set; }
    }
}
