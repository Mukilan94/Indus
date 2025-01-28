using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogCommonTime
    {
        public MudLogCommonTime()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
            MudLogParameter = new HashSet<MudLogParameter>();
        }

        [Key]
        public int CommonTimeId { get; set; }
        [Column("DTimCreation")]
        public string DtimCreation { get; set; }
        [Column("DTimLastChange")]
        public string DtimLastChange { get; set; }

        [InverseProperty("CommonTime")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
        [InverseProperty("CommonTime")]
        public virtual ICollection<MudLogParameter> MudLogParameter { get; set; }
    }
}
