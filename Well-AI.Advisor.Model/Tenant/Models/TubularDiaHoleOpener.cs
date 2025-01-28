using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDiaHoleOpener
    {
        public TubularDiaHoleOpener()
        {
            TubularHoleOpener = new HashSet<TubularHoleOpener>();
        }

        [Key]
        public int DiaHoleOpenerId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaHoleOpener")]
        public virtual ICollection<TubularHoleOpener> TubularHoleOpener { get; set; }
    }
}
