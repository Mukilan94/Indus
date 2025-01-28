using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularCosts
    {
        public TubularCosts()
        {
            TubularBitRecord = new HashSet<TubularBitRecord>();
        }

        [Key]
        public int CostId { get; set; }
        public string Currency { get; set; }
        public string Text { get; set; }

        [InverseProperty("Cost")]
        public virtual ICollection<TubularBitRecord> TubularBitRecord { get; set; }
    }
}
