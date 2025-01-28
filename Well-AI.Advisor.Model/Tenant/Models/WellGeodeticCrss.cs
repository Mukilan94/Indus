using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("WellGeodeticCRSs")]
    public partial class WellGeodeticCrss
    {
        public WellGeodeticCrss()
        {
            WellCrss = new HashSet<WellCrss>();
        }

        [Key]
        [Column("GeodeticCRSId")]
        public int GeodeticCrsid { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("GeodeticCrs")]
        public virtual ICollection<WellCrss> WellCrss { get; set; }
    }
}
