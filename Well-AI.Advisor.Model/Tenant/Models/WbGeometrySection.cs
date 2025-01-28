using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WbGeometrySection
    {
        public WbGeometrySection()
        {
            WbGeometrys = new HashSet<WbGeometrys>();
        }

        [Key]
        public int WbGeometrySectionId { get; set; }
        public string TypeHoleCasing { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomDiaDriftId { get; set; }
        public int? TvdTopId { get; set; }
        public int? TvdBottomId { get; set; }
        public int? IdSectionId { get; set; }
        public int? OdSectionId { get; set; }
        public int? WtPerLenId { get; set; }
        public string Grade { get; set; }
        public string CurveConductor { get; set; }
        public int? DiaDriftId { get; set; }
        public string FactFric { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(DiaDriftId))]
        [InverseProperty(nameof(WbGeometryDiaDrift.WbGeometrySection))]
        public virtual WbGeometryDiaDrift DiaDrift { get; set; }
        [ForeignKey(nameof(IdSectionId))]
        [InverseProperty(nameof(WbGeometryIdSection.WbGeometrySection))]
        public virtual WbGeometryIdSection IdSection { get; set; }
        [ForeignKey(nameof(MdBottomDiaDriftId))]
        [InverseProperty(nameof(WbGeometryMdBottom.WbGeometrySection))]
        public virtual WbGeometryMdBottom MdBottomDiaDrift { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(WbGeometryMdTop.WbGeometrySection))]
        public virtual WbGeometryMdTop MdTop { get; set; }
        [ForeignKey(nameof(OdSectionId))]
        [InverseProperty(nameof(WbGeometryOdSection.WbGeometrySection))]
        public virtual WbGeometryOdSection OdSection { get; set; }
        [ForeignKey(nameof(TvdBottomId))]
        [InverseProperty(nameof(WbGeometryTvdBottom.WbGeometrySection))]
        public virtual WbGeometryTvdBottom TvdBottom { get; set; }
        [ForeignKey(nameof(TvdTopId))]
        [InverseProperty(nameof(WbGeometryTvdTop.WbGeometrySection))]
        public virtual WbGeometryTvdTop TvdTop { get; set; }
        [ForeignKey(nameof(WtPerLenId))]
        [InverseProperty(nameof(WbGeometryWtPerLen.WbGeometrySection))]
        public virtual WbGeometryWtPerLen WtPerLen { get; set; }
        [InverseProperty("WbGeometrySection")]
        public virtual ICollection<WbGeometrys> WbGeometrys { get; set; }
    }
}
