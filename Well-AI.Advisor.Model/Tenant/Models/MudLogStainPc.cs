using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogStainPc
    {
        public MudLogStainPc()
        {
            MudLogShow = new HashSet<MudLogShow>();
        }

        [Key]
        public int StainPcId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StainPc")]
        public virtual ICollection<MudLogShow> MudLogShow { get; set; }
    }
}
