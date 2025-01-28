using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WbGeometrys
    {
        [Key]
        public int WbGeometryId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        [Column("DTimReport")]
        public string DtimReport { get; set; }
        public int? MdBottomDiaDriftId { get; set; }
        public int? GapAirId { get; set; }
        public int? DepthWaterMeanId { get; set; }
        public int? WbGeometrySectionId { get; set; }
        public int? CommonDataWbGeometryCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataWbGeometryCommonDataId))]
        [InverseProperty(nameof(WbGeometryCommonData.WbGeometrys))]
        public virtual WbGeometryCommonData CommonDataWbGeometryCommonData { get; set; }
        [ForeignKey(nameof(DepthWaterMeanId))]
        [InverseProperty(nameof(WbGeometryDepthWaterMean.WbGeometrys))]
        public virtual WbGeometryDepthWaterMean DepthWaterMean { get; set; }
        [ForeignKey(nameof(GapAirId))]
        [InverseProperty(nameof(WbGeometryGapAir.WbGeometrys))]
        public virtual WbGeometryGapAir GapAir { get; set; }
        [ForeignKey(nameof(MdBottomDiaDriftId))]
        [InverseProperty(nameof(WbGeometryMdBottom.WbGeometrys))]
        public virtual WbGeometryMdBottom MdBottomDiaDrift { get; set; }
        [ForeignKey(nameof(WbGeometrySectionId))]
        [InverseProperty("WbGeometrys")]
        public virtual WbGeometrySection WbGeometrySection { get; set; }
    }
}
