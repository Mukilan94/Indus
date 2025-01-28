using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogStartMd
    {
        public MudLogStartMd()
        {
            MudLogs = new HashSet<MudLogs>();
        }

        [Key]
        public int StartMdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StartMd")]
        public virtual ICollection<MudLogs> MudLogs { get; set; }
    }
}
