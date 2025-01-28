using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Attachments
    {
        [Key]
        [Column("uid")]
        public string Uid { get; set; }
        [Column("uidWell")]
        public string UidWell { get; set; }
        [Column("uidWellbore")]
        public string UidWellbore { get; set; }
        [Column("nameWell")]
        public string NameWell { get; set; }
        [Column("nameWellbore")]
        public string NameWellbore { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("objectReferenceuidRef")]
        public string ObjectReferenceuidRef { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("fileType")]
        public string FileType { get; set; }
        [Column("content")]
        public string Content { get; set; }
        public int? CommonDataId { get; set; }

        [ForeignKey(nameof(CommonDataId))]
        [InverseProperty(nameof(AttchmentCommonDatas.Attachments))]
        public virtual AttchmentCommonDatas CommonData { get; set; }
        [ForeignKey(nameof(ObjectReferenceuidRef))]
        [InverseProperty(nameof(AttachmentObjectReferences.Attachments))]
        public virtual AttachmentObjectReferences ObjectReferenceuidRefNavigation { get; set; }
    }
}
