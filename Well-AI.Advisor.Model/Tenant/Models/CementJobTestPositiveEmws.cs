using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobTestPositiveEmws
    {
        public CementJobTestPositiveEmws()
        {
            CementJobCementTests = new HashSet<CementJobCementTests>();
        }

        [Key]
        public int TestPositiveEmwId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TestPositiveEmw")]
        public virtual ICollection<CementJobCementTests> CementJobCementTests { get; set; }
    }
}
