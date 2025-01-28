using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupDefaultDatum
    {
        public ObjectGroupDefaultDatum()
        {
            ObjectGroupCommonDatas = new HashSet<ObjectGroupCommonDatas>();
        }

        [Key]
        public int DefaultDatumId { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("DefaultDatum")]
        public virtual ICollection<ObjectGroupCommonDatas> ObjectGroupCommonDatas { get; set; }
    }
}
