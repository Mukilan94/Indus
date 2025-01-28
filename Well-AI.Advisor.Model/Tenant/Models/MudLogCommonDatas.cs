using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogCommonDatas
    {
        public MudLogCommonDatas()
        {
            MudLogs = new HashSet<MudLogs>();
        }

        [Key]
        public int MudLogCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataMudLogCommonData")]
        public virtual ICollection<MudLogs> MudLogs { get; set; }
    }
}
