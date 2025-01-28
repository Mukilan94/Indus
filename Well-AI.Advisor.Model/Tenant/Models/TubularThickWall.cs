using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularThickWall
    {
        public TubularThickWall()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int ThickWallId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ThickWall")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
