using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WbGeometryMdBottom
    {
        public WbGeometryMdBottom()
        {
            WbGeometrySection = new HashSet<WbGeometrySection>();
            WbGeometrys = new HashSet<WbGeometrys>();
        }

        [Key]
        public int DiaDriftId { get; set; }
        public int MdBottomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdBottomDiaDrift")]
        public virtual ICollection<WbGeometrySection> WbGeometrySection { get; set; }
        [InverseProperty("MdBottomDiaDrift")]
        public virtual ICollection<WbGeometrys> WbGeometrys { get; set; }
    }
}
