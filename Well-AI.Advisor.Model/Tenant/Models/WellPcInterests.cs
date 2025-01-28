using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellPcInterests
    {
        public WellPcInterests()
        {
            Wells = new HashSet<Wells>();
        }

        [Key]
        public int PcInterestId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("PcInterest")]
        public virtual ICollection<Wells> Wells { get; set; }
    }
}
