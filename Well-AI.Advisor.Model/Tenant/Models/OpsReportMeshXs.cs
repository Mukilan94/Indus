using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMeshXs
    {
        public OpsReportMeshXs()
        {
            OpsReportShakerScreens = new HashSet<OpsReportShakerScreens>();
        }

        [Key]
        [Column("MeshXId")]
        public int MeshXid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MeshX")]
        public virtual ICollection<OpsReportShakerScreens> OpsReportShakerScreens { get; set; }
    }
}
