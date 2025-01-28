using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("CementJobETimBeforeTests")]
    public partial class CementJobEtimBeforeTests
    {
        public CementJobEtimBeforeTests()
        {
            CementJobCementTests = new HashSet<CementJobCementTests>();
        }

        [Key]
        [Column("ETimBeforeTestId")]
        public int EtimBeforeTestId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EtimBeforeTest")]
        public virtual ICollection<CementJobCementTests> CementJobCementTests { get; set; }
    }
}
