using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportShakerScreens
    {
        public OpsReportShakerScreens()
        {
            OpsReportShakerOps = new HashSet<OpsReportShakerOps>();
        }

        [Key]
        public int ShakerScreenId { get; set; }
        [Column("DTimStart")]
        public string DtimStart { get; set; }
        [Column("DTimEnd")]
        public string DtimEnd { get; set; }
        public string NumDeck { get; set; }
        [Column("MeshXId")]
        public int? MeshXid { get; set; }
        [Column("MeshYId")]
        public int? MeshYid { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int? CutPointId { get; set; }

        [ForeignKey(nameof(CutPointId))]
        [InverseProperty(nameof(OpsReportCutPoints.OpsReportShakerScreens))]
        public virtual OpsReportCutPoints CutPoint { get; set; }
        [ForeignKey(nameof(MeshXid))]
        [InverseProperty(nameof(OpsReportMeshXs.OpsReportShakerScreens))]
        public virtual OpsReportMeshXs MeshX { get; set; }
        [ForeignKey(nameof(MeshYid))]
        [InverseProperty(nameof(OpsReportMeshYs.OpsReportShakerScreens))]
        public virtual OpsReportMeshYs MeshY { get; set; }
        [InverseProperty("ShakerScreen")]
        public virtual ICollection<OpsReportShakerOps> OpsReportShakerOps { get; set; }
    }
}
