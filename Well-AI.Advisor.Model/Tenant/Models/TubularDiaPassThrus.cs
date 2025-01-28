using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDiaPassThrus
    {
        public TubularDiaPassThrus()
        {
            TubularBitRecord = new HashSet<TubularBitRecord>();
        }

        [Key]
        public int DiaPassThruId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaPassThru")]
        public virtual ICollection<TubularBitRecord> TubularBitRecord { get; set; }
    }
}
