using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularCriticalCrossSection
    {
        public TubularCriticalCrossSection()
        {
            TubularConnection = new HashSet<TubularConnection>();
        }

        [Key]
        public int CriticalCrossSectionId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("CriticalCrossSection")]
        public virtual ICollection<TubularConnection> TubularConnection { get; set; }
    }
}
