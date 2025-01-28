using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupRangeMaxs
    {
        public ObjectGroupRangeMaxs()
        {
            ObjectGroupMemberObjects = new HashSet<ObjectGroupMemberObjects>();
        }

        [Key]
        public int RangeMaxId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("RangeMax")]
        public virtual ICollection<ObjectGroupMemberObjects> ObjectGroupMemberObjects { get; set; }
    }
}
