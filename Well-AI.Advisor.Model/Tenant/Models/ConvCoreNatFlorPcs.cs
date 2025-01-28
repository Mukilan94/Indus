using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreNatFlorPcs
    {
        public ConvCoreNatFlorPcs()
        {
            ConvCoreShows = new HashSet<ConvCoreShows>();
        }

        [Key]
        public int NatFlorPcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("NatFlorPc")]
        public virtual ICollection<ConvCoreShows> ConvCoreShows { get; set; }
    }
}
