using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimTests")]
    public partial class CementJobEtimTests
    {
        public CementJobEtimTests()
        {
            CementJobCementTests = new HashSet<CementJobCementTests>();
        }

        [Key]
        [Column("ETimTestId")]
        public int EtimTestId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimTest")]
        public virtual ICollection<CementJobCementTests> CementJobCementTests { get; set; }
    }
}
