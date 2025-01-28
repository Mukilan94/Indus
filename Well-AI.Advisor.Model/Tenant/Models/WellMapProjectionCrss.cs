using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("WellMapProjectionCRSs")]
    public partial class WellMapProjectionCrss
    {
        public WellMapProjectionCrss()
        {
            WellCrss = new HashSet<WellCrss>();
        }

        [Key]
        [Column("MapProjectionCRSId")]
        public int MapProjectionCrsid { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("MapProjectionCrs")]
        public virtual ICollection<WellCrss> WellCrss { get; set; }
    }
}
