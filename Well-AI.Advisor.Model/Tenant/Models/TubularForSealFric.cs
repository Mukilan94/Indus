using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularForSealFric
    {
        public TubularForSealFric()
        {
            TubularJar = new HashSet<TubularJar>();
        }

        [Key]
        public int ForSealFricId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ForSealFric")]
        public virtual ICollection<TubularJar> TubularJar { get; set; }
    }
}
