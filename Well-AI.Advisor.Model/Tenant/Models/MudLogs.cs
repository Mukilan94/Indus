using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogs
    {
        public MudLogs()
        {
            MudLogParameter = new HashSet<MudLogParameter>();
        }

        [Key]
        public int MudLogId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string ObjectGrowing { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string MudLogCompany { get; set; }
        public string MudLogEngineers { get; set; }
        public int? StartMdId { get; set; }
        public int? EndMdId { get; set; }
        public int? GeologyIntervalId { get; set; }
        public int? CommonDataMudLogCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataMudLogCommonDataId))]
        [InverseProperty(nameof(MudLogCommonDatas.MudLogs))]
        public virtual MudLogCommonDatas CommonDataMudLogCommonData { get; set; }
        [ForeignKey(nameof(EndMdId))]
        [InverseProperty(nameof(MudLogEndMd.MudLogs))]
        public virtual MudLogEndMd EndMd { get; set; }
        [ForeignKey(nameof(GeologyIntervalId))]
        [InverseProperty(nameof(MudLogGeologyInterval.MudLogs))]
        public virtual MudLogGeologyInterval GeologyInterval { get; set; }
        [ForeignKey(nameof(StartMdId))]
        [InverseProperty(nameof(MudLogStartMd.MudLogs))]
        public virtual MudLogStartMd StartMd { get; set; }
        [InverseProperty("MudLog")]
        public virtual ICollection<MudLogParameter> MudLogParameter { get; set; }
    }
}
