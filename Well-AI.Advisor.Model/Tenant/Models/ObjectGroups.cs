using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroups
    {
        [Key]
        public int ObjectGroupId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string GroupType { get; set; }
        public string Sequence { get; set; }
        public string Description { get; set; }
        public int? ParamId { get; set; }
        public int? MemberObjectId { get; set; }
        public int? CommonDataObjectGroupCommonDataId { get; set; }
        public string CustomData { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataObjectGroupCommonDataId))]
        [InverseProperty(nameof(ObjectGroupCommonDatas.ObjectGroups))]
        public virtual ObjectGroupCommonDatas CommonDataObjectGroupCommonData { get; set; }
        [ForeignKey(nameof(MemberObjectId))]
        [InverseProperty(nameof(ObjectGroupMemberObjects.ObjectGroups))]
        public virtual ObjectGroupMemberObjects MemberObject { get; set; }
        [ForeignKey(nameof(ParamId))]
        [InverseProperty(nameof(ObjectGroupParam.ObjectGroups))]
        public virtual ObjectGroupParam Param { get; set; }
    }
}
