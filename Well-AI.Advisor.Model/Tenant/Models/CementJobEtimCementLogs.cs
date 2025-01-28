using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimCementLogs")]
    public partial class CementJobEtimCementLogs
    {
        public CementJobEtimCementLogs()
        {
            CementJobCementTests = new HashSet<CementJobCementTests>();
        }

        [Key]
        [Column("ETimCementLogId")]
        public int EtimCementLogId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimCementLog")]
        public virtual ICollection<CementJobCementTests> CementJobCementTests { get; set; }
    }
}
