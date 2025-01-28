using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("WellCRSs")]
    public partial class WellCrss
    {
        public WellCrss()
        {
            WellLocations = new HashSet<WellLocations>();
        }

        [Key]
        [Column("WellCRSUid")]
        public int WellCrsuid { get; set; }
        public string Uid { get; set; }
        public string UidRef { get; set; }
        public string Name { get; set; }
        [Column("GeodeticCRSId")]
        public int? GeodeticCrsid { get; set; }
        public string Description { get; set; }
        [Column("MapProjectionCRSId")]
        public int? MapProjectionCrsid { get; set; }
        [Column("LocalCRSId")]
        public int? LocalCrsid { get; set; }
        public string Text { get; set; }
        public int? WellId { get; set; }

        [ForeignKey(nameof(GeodeticCrsid))]
        [InverseProperty(nameof(WellGeodeticCrss.WellCrss))]
        public virtual WellGeodeticCrss GeodeticCrs { get; set; }
        [ForeignKey(nameof(LocalCrsid))]
        [InverseProperty(nameof(WellLocalCrss.WellCrss))]
        public virtual WellLocalCrss LocalCrs { get; set; }
        [ForeignKey(nameof(MapProjectionCrsid))]
        [InverseProperty(nameof(WellMapProjectionCrss.WellCrss))]
        public virtual WellMapProjectionCrss MapProjectionCrs { get; set; }
        [ForeignKey(nameof(WellId))]
        [InverseProperty(nameof(Wells.WellCrss))]
        public virtual Wells Well { get; set; }
        [InverseProperty("WellCrsu")]
        public virtual ICollection<WellLocations> WellLocations { get; set; }
    }
}
