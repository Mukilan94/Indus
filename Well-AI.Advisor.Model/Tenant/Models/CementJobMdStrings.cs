using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobMdStrings
    {
        public CementJobMdStrings()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int MdStringId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdString")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
