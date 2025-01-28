using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimPitStarts")]
    public partial class CementJobEtimPitStarts
    {
        public CementJobEtimPitStarts()
        {
            CementJobCementTests = new HashSet<CementJobCementTests>();
        }

        [Key]
        [Column("ETimPitStartId")]
        public int EtimPitStartId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimPitStart")]
        public virtual ICollection<CementJobCementTests> CementJobCementTests { get; set; }
    }
}
