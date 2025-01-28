using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WbGeometryMdTop
    {
        public WbGeometryMdTop()
        {
            WbGeometrySection = new HashSet<WbGeometrySection>();
        }

        [Key]
        public int MdTopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdTop")]
        public virtual ICollection<WbGeometrySection> WbGeometrySection { get; set; }
    }
}
