using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularForPmpOpen
    {
        public TubularForPmpOpen()
        {
            TubularJar = new HashSet<TubularJar>();
        }

        [Key]
        public int ForPmpOpenId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("ForPmpOpen")]
        public virtual ICollection<TubularJar> TubularJar { get; set; }
    }
}
