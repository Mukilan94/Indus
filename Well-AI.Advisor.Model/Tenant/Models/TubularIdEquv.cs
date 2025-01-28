using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularIdEquv
    {
        public TubularIdEquv()
        {
            TubularMwdTool = new HashSet<TubularMwdTool>();
        }

        [Key]
        public int IdEquvId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("IdEquv")]
        public virtual ICollection<TubularMwdTool> TubularMwdTool { get; set; }
    }
}
