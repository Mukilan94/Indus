using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularDiaPilot
    {
        public TubularDiaPilot()
        {
            TubularBitRecord = new HashSet<TubularBitRecord>();
        }

        [Key]
        public int DiaPilotId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DiaPilot")]
        public virtual ICollection<TubularBitRecord> TubularBitRecord { get; set; }
    }
}
