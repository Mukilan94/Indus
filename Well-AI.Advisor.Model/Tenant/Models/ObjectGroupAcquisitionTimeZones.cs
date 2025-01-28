using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ObjectGroupAcquisitionTimeZones
    {
        public ObjectGroupAcquisitionTimeZones()
        {
            ObjectGroupCommonDatas = new HashSet<ObjectGroupCommonDatas>();
        }

        [Key]
        public int AcquisitionTimeZoneId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public string Text { get; set; }

        [InverseProperty("AcquisitionTimeZone")]
        public virtual ICollection<ObjectGroupCommonDatas> ObjectGroupCommonDatas { get; set; }
    }
}
