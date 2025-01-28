using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupExtensionNameValues
    {
        public ObjectGroupExtensionNameValues()
        {
            ObjectGroupCommonDatas = new HashSet<ObjectGroupCommonDatas>();
            ObjectGroupMemberObjects = new HashSet<ObjectGroupMemberObjects>();
        }

        [Key]
        public int ExtensionNameValueId { get; set; }
        public string Name { get; set; }
        public int? ValueId { get; set; }
        public string DataType { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdId { get; set; }
        public string Index { get; set; }
        public string MeasureClass { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }

        [ForeignKey(nameof(MdId))]
        [InverseProperty(nameof(ObjectGroupMd.ObjectGroupExtensionNameValues))]
        public virtual ObjectGroupMd Md { get; set; }
        [ForeignKey(nameof(ValueId))]
        [InverseProperty(nameof(ObjectGroupValue.ObjectGroupExtensionNameValues))]
        public virtual ObjectGroupValue Value { get; set; }
        [InverseProperty("ExtensionNameValue")]
        public virtual ICollection<ObjectGroupCommonDatas> ObjectGroupCommonDatas { get; set; }
        [InverseProperty("ExtensionNameValue")]
        public virtual ICollection<ObjectGroupMemberObjects> ObjectGroupMemberObjects { get; set; }
    }
}
