using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WbGeometryTvdTop
    {
        public WbGeometryTvdTop()
        {
            WbGeometrySection = new HashSet<WbGeometrySection>();
        }

        [Key]
        public int TvdTopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdTop")]
        public virtual ICollection<WbGeometrySection> WbGeometrySection { get; set; }
    }
}
