using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMeshYs
    {
        public OpsReportMeshYs()
        {
            OpsReportShakerScreens = new HashSet<OpsReportShakerScreens>();
        }

        [Key]
        [Column("MeshYId")]
        public int MeshYid { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MeshY")]
        public virtual ICollection<OpsReportShakerScreens> OpsReportShakerScreens { get; set; }
    }
}
