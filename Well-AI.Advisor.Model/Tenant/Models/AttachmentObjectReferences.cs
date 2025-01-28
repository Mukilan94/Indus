using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class AttachmentObjectReferences
    {
        public AttachmentObjectReferences()
        {
            Attachments = new HashSet<Attachments>();
        }

        [Key]
        [Column("uidRef")]
        public string UidRef { get; set; }
        public string Objects { get; set; }
        [Column("text")]
        public string Text { get; set; }

        [InverseProperty("ObjectReferenceuidRefNavigation")]
        public virtual ICollection<Attachments> Attachments { get; set; }
    }
}
