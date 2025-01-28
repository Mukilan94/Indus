using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogParameter
    {
        [Key]
        public int ParameterId { get; set; }
        public string Type { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomId { get; set; }
        public string Text { get; set; }
        public int? CommonTimeId { get; set; }
        public string Uid { get; set; }
        public int? ForceId { get; set; }
        public int? MudLogId { get; set; }

        [ForeignKey(nameof(CommonTimeId))]
        [InverseProperty(nameof(MudLogCommonTime.MudLogParameter))]
        public virtual MudLogCommonTime CommonTime { get; set; }
        [ForeignKey(nameof(ForceId))]
        [InverseProperty(nameof(MudLogForce.MudLogParameter))]
        public virtual MudLogForce Force { get; set; }
        [ForeignKey(nameof(MdBottomId))]
        [InverseProperty(nameof(MudLogMdBottom.MudLogParameter))]
        public virtual MudLogMdBottom MdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(MudLogMdTop.MudLogParameter))]
        public virtual MudLogMdTop MdTop { get; set; }
        [ForeignKey(nameof(MudLogId))]
        [InverseProperty(nameof(MudLogs.MudLogParameter))]
        public virtual MudLogs MudLog { get; set; }
    }
}
