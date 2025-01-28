using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularWearWall
    {
        public TubularWearWall()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int WearWallId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("WearWall")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
