using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupCommonDatas
    {
        public ObjectGroupCommonDatas()
        {
            ObjectGroups = new HashSet<ObjectGroups>();
        }

        [Key]
        public int ObjectGroupCommonDataId { get; set; }
        public string SourceName { get; set; }
        [Column("DTimCreation")]
        public string DtimCreation { get; set; }
        [Column("DTimLastChange")]
        public string DtimLastChange { get; set; }
        public string ItemState { get; set; }
        public string ServiceCategory { get; set; }
        public string Comments { get; set; }
        public int? AcquisitionTimeZoneId { get; set; }
        public int? DefaultDatumId { get; set; }
        public string PrivateGroupOnly { get; set; }
        public string ExtensionAny { get; set; }
        public int? ExtensionNameValueId { get; set; }

        [ForeignKey(nameof(AcquisitionTimeZoneId))]
        [InverseProperty(nameof(ObjectGroupAcquisitionTimeZones.ObjectGroupCommonDatas))]
        public virtual ObjectGroupAcquisitionTimeZones AcquisitionTimeZone { get; set; }
        [ForeignKey(nameof(DefaultDatumId))]
        [InverseProperty(nameof(ObjectGroupDefaultDatum.ObjectGroupCommonDatas))]
        public virtual ObjectGroupDefaultDatum DefaultDatum { get; set; }
        [ForeignKey(nameof(ExtensionNameValueId))]
        [InverseProperty(nameof(ObjectGroupExtensionNameValues.ObjectGroupCommonDatas))]
        public virtual ObjectGroupExtensionNameValues ExtensionNameValue { get; set; }
        [InverseProperty("CommonDataObjectGroupCommonData")]
        public virtual ICollection<ObjectGroups> ObjectGroups { get; set; }
    }
}
