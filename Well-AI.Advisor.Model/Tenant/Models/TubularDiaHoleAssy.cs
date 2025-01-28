using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDiaHoleAssy
    {
        public TubularDiaHoleAssy()
        {
            Tubulars = new HashSet<Tubulars>();
        }

        [Key]
        public int DiaHoleAssyId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaHoleAssy")]
        public virtual ICollection<Tubulars> Tubulars { get; set; }
    }
}
