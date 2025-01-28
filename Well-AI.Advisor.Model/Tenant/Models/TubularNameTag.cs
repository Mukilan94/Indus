using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularNameTag
    {
        public TubularNameTag()
        {
            TubularComponent = new HashSet<TubularComponent>();
        }

        [Key]
        public int NameTagId { get; set; }
        public string Name { get; set; }
        public string NumberingScheme { get; set; }
        public string Uid { get; set; }

        [InverseProperty("NameTag")]
        public virtual ICollection<TubularComponent> TubularComponent { get; set; }
    }
}
