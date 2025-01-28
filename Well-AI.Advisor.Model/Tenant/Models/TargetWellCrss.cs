using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("TargetWellCRSs")]
    public partial class TargetWellCrss
    {
        public TargetWellCrss()
        {
            TargetLocations = new HashSet<TargetLocations>();
        }

        [Key]
        [Column("WellCRSId")]
        public int WellCrsid { get; set; }
        public string UidRef { get; set; }
        public string Text { get; set; }

        [InverseProperty("WellCrs")]
        public virtual ICollection<TargetLocations> TargetLocations { get; set; }
    }
}
