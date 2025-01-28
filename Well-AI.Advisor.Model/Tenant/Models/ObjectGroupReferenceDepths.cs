using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupReferenceDepths
    {
        public ObjectGroupReferenceDepths()
        {
            ObjectGroupMemberObjects = new HashSet<ObjectGroupMemberObjects>();
        }

        [Key]
        public int ReferenceDepthId { get; set; }
        public string Uom { get; set; }
        public string Datum { get; set; }
        public string Text { get; set; }

        [InverseProperty("ReferenceDepth")]
        public virtual ICollection<ObjectGroupMemberObjects> ObjectGroupMemberObjects { get; set; }
    }
}
