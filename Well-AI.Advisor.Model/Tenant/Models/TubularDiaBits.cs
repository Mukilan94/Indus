using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDiaBits
    {
        public TubularDiaBits()
        {
            TubularBitRecord = new HashSet<TubularBitRecord>();
        }

        [Key]
        public int DiaBitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaBit")]
        public virtual ICollection<TubularBitRecord> TubularBitRecord { get; set; }
    }
}
