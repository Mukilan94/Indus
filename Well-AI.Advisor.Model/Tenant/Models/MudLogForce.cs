using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogForce
    {
        public MudLogForce()
        {
            MudLogParameter = new HashSet<MudLogParameter>();
        }

        [Key]
        public int ForceId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Force")]
        public virtual ICollection<MudLogParameter> MudLogParameter { get; set; }
    }
}
