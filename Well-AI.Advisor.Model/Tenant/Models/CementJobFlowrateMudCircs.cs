using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class CementJobFlowrateMudCircs
    {
        public CementJobFlowrateMudCircs()
        {
            CementJobCementStages = new HashSet<CementJobCementStages>();
        }

        [Key]
        public int CementJobFlowrateMudCircId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("FlowrateMudCircCementJobFlowrateMudCirc")]
        public virtual ICollection<CementJobCementStages> CementJobCementStages { get; set; }
    }
}
