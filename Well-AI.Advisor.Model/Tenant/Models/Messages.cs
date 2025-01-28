using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Messages
    {
        [Key]
        public int MessageId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string ActivityCode { get; set; }
        public int? MdId { get; set; }
        public int? MdBitId { get; set; }
        public string TypeMessage { get; set; }
        public string MessageText { get; set; }
        public string ParamIndex { get; set; }
        public string Severity { get; set; }
        public string WarnProbability { get; set; }
        public int? CommonDataMessageCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataMessageCommonDataId))]
        [InverseProperty(nameof(MessageCommonDatas.Messages))]
        public virtual MessageCommonDatas CommonDataMessageCommonData { get; set; }
        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(MessageMd.Messages))]
        public virtual MessageMd Md { get; set; }
        [ForeignKey(nameof(MdBitId))]
        [InverseProperty(nameof(MessageMdBit.Messages))]
        public virtual MessageMdBit MdBit { get; set; }
        [ForeignKey(nameof(ParamIndex))]
        [InverseProperty(nameof(MessageParam.Messages))]
        public virtual MessageParam ParamIndexNavigation { get; set; }
    }
}
