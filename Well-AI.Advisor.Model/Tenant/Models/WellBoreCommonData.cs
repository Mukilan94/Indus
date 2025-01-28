using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellBoreCommonData
    {
        public WellBoreCommonData()
        {
            WellBores = new HashSet<WellBores>();
        }

        [Key]
        public int WellBoreCommonDataId { get; set; }
        [Column("DTimCreation")]
        public DateTime DtimCreation { get; set; }
        [Column("DTimLastChange")]
        public DateTime DtimLastChange { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataWellBoreCommonData")]
        public virtual ICollection<WellBores> WellBores { get; set; }
    }
}
