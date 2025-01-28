using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobAdditives
    {
        public CementJobAdditives()
        {
            CementJobCementAdditives = new HashSet<CementJobCementAdditives>();
        }

        [Key]
        public int AdditiveId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Additive")]
        public virtual ICollection<CementJobCementAdditives> CementJobCementAdditives { get; set; }
    }
}
