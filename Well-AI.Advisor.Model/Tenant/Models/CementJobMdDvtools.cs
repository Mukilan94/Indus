using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobMdDVTools")]
    public partial class CementJobMdDvtools
    {
        public CementJobMdDvtools()
        {
            CementJobCementTests = new HashSet<CementJobCementTests>();
        }

        [Key]
        [Column("JobMdDVToolId")]
        public int JobMdDvtoolId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdDvtoolJobMdDvtool")]
        public virtual ICollection<CementJobCementTests> CementJobCementTests { get; set; }
    }
}
