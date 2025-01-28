using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogEndMd
    {
        public MudLogEndMd()
        {
            MudLogs = new HashSet<MudLogs>();
        }

        [Key]
        public int EndMdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("EndMd")]
        public virtual ICollection<MudLogs> MudLogs { get; set; }
    }
}
