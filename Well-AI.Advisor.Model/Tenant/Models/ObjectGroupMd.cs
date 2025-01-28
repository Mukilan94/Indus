using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupMd
    {
        public ObjectGroupMd()
        {
            ObjectGroupExtensionNameValues = new HashSet<ObjectGroupExtensionNameValues>();
        }

        [Key]
        public int MdId { get; set; }
        public string Uom { get; set; }
        public string Datum { get; set; }
        public string Text { get; set; }

        [InverseProperty("Md")]
        public virtual ICollection<ObjectGroupExtensionNameValues> ObjectGroupExtensionNameValues { get; set; }
    }
}
