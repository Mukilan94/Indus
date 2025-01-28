using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularTempMx
    {
        public TubularTempMx()
        {
            TubularMwdTool = new HashSet<TubularMwdTool>();
        }

        [Key]
        public int TempMxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TempMx")]
        public virtual ICollection<TubularMwdTool> TubularMwdTool { get; set; }
    }
}
