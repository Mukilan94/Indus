using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupParam
    {
        public ObjectGroupParam()
        {
            ObjectGroupMemberObjects = new HashSet<ObjectGroupMemberObjects>();
            ObjectGroups = new HashSet<ObjectGroups>();
        }

        [Key]
        public int ParamId { get; set; }
        public string Uid { get; set; }
        public string Description { get; set; }
        public string Uom { get; set; }
        public string Name { get; set; }
        public string Index { get; set; }
        public string Text { get; set; }

        [InverseProperty("Param")]
        public virtual ICollection<ObjectGroupMemberObjects> ObjectGroupMemberObjects { get; set; }
        [InverseProperty("Param")]
        public virtual ICollection<ObjectGroups> ObjectGroups { get; set; }
    }
}
