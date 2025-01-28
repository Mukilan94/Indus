using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WbGeometryDiaDrift
    {
        public WbGeometryDiaDrift()
        {
            WbGeometrySection = new HashSet<WbGeometrySection>();
        }

        [Key]
        public int DiaDriftId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaDrift")]
        public virtual ICollection<WbGeometrySection> WbGeometrySection { get; set; }
    }
}
