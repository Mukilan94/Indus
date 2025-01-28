using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupMemberObjects
    {
        public ObjectGroupMemberObjects()
        {
            ObjectGroups = new HashSet<ObjectGroups>();
        }

        [Key]
        public int MemberObjectId { get; set; }
        public int? ObjectReferenceId { get; set; }
        public string IndexType { get; set; }
        public int? Sequence1Id { get; set; }
        public int? Sequence2Id { get; set; }
        public int? Sequence3Id { get; set; }
        public int? RangeMinId { get; set; }
        public int? RangeMaxId { get; set; }
        public string RangeDateTimeMin { get; set; }
        public string RangeDateTimeMax { get; set; }
        public string MnemonicList { get; set; }
        public int? ReferenceDepthId { get; set; }
        public string ReferenceDateTime { get; set; }
        public int? ParamId { get; set; }
        public int? ExtensionNameValueId { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(ExtensionNameValueId))]
        [InverseProperty(nameof(ObjectGroupExtensionNameValues.ObjectGroupMemberObjects))]
        public virtual ObjectGroupExtensionNameValues ExtensionNameValue { get; set; }
        [ForeignKey(nameof(ObjectReferenceId))]
        [InverseProperty(nameof(ObjectGroupObjectReference.ObjectGroupMemberObjects))]
        public virtual ObjectGroupObjectReference ObjectReference { get; set; }
        [ForeignKey(nameof(ParamId))]
        [InverseProperty(nameof(ObjectGroupParam.ObjectGroupMemberObjects))]
        public virtual ObjectGroupParam Param { get; set; }
        [ForeignKey(nameof(RangeMaxId))]
        [InverseProperty(nameof(ObjectGroupRangeMaxs.ObjectGroupMemberObjects))]
        public virtual ObjectGroupRangeMaxs RangeMax { get; set; }
        [ForeignKey(nameof(RangeMinId))]
        [InverseProperty(nameof(ObjectGroupRangeMins.ObjectGroupMemberObjects))]
        public virtual ObjectGroupRangeMins RangeMin { get; set; }
        [ForeignKey(nameof(ReferenceDepthId))]
        [InverseProperty(nameof(ObjectGroupReferenceDepths.ObjectGroupMemberObjects))]
        public virtual ObjectGroupReferenceDepths ReferenceDepth { get; set; }
        [ForeignKey(nameof(Sequence1Id))]
        [InverseProperty(nameof(ObjectGroupSequence1s.ObjectGroupMemberObjects))]
        public virtual ObjectGroupSequence1s Sequence1 { get; set; }
        [ForeignKey(nameof(Sequence2Id))]
        [InverseProperty(nameof(ObjectGroupSequence2s.ObjectGroupMemberObjects))]
        public virtual ObjectGroupSequence2s Sequence2 { get; set; }
        [ForeignKey(nameof(Sequence3Id))]
        [InverseProperty(nameof(ObjectGroupSequence3s.ObjectGroupMemberObjects))]
        public virtual ObjectGroupSequence3s Sequence3 { get; set; }
        [InverseProperty("MemberObject")]
        public virtual ICollection<ObjectGroups> ObjectGroups { get; set; }
    }
}
