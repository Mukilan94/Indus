using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobMdCementTops
    {
        public CementJobMdCementTops()
        {
            CementJobCementTests = new HashSet<CementJobCementTests>();
        }

        [Key]
        public int MdCementTopId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdCementTop")]
        public virtual ICollection<CementJobCementTests> CementJobCementTests { get; set; }
    }
}
