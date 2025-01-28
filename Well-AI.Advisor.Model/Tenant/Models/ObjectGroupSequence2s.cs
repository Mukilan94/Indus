using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupSequence2s
    {
        public ObjectGroupSequence2s()
        {
            ObjectGroupMemberObjects = new HashSet<ObjectGroupMemberObjects>();
        }

        [Key]
        public int Sequence2Id { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        [InverseProperty("Sequence2")]
        public virtual ICollection<ObjectGroupMemberObjects> ObjectGroupMemberObjects { get; set; }
    }
}
