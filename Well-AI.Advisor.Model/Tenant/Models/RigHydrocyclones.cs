using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigHydrocyclones
    {
        public RigHydrocyclones()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public int HydrocycloneId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        [Column("DTimInstall")]
        public string DtimInstall { get; set; }
        [Column("DTimRemove")]
        public string DtimRemove { get; set; }
        public string Type { get; set; }
        public string DescCone { get; set; }
        public string Owner { get; set; }
        public string Uid { get; set; }

        [InverseProperty("Hydrocyclone")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
