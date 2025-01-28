using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupSequence3s
    {
        public ObjectGroupSequence3s()
        {
            ObjectGroupMemberObjects = new HashSet<ObjectGroupMemberObjects>();
        }

        [Key]
        public int Sequence3Id { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        [InverseProperty("Sequence3")]
        public virtual ICollection<ObjectGroupMemberObjects> ObjectGroupMemberObjects { get; set; }
    }
}
