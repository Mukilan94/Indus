using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class OpsReportMudLossess
    {
        public OpsReportMudLossess()
        {
            OpsReportMudVolumes = new HashSet<OpsReportMudVolumes>();
        }

        [Key]
        public int MudLossesId { get; set; }
        public int? VolLostShakerSurfId { get; set; }
        public int? VolLostMudCleanerSurfId { get; set; }
        public int? VolLostPitsSurfId { get; set; }
        public int? VolLostTrippingSurfId { get; set; }
        public int? VolLostOtherSurfId { get; set; }
        public int? VolTotMudLostSurfId { get; set; }
        public int? VolLostCircHoleId { get; set; }
        public int? VolLostCsgHoleId { get; set; }
        public int? VolLostCmtHoleId { get; set; }
        public int? VolLostBhdCsgHoleId { get; set; }
        public int? VolLostAbandonHoleId { get; set; }
        public int? VolLostOtherHoleId { get; set; }
        public int? VolTotMudLostHoleId { get; set; }

        [ForeignKey(nameof(VolLostAbandonHoleId))]
        [InverseProperty(nameof(OpsReportVolLostAbandonHoles.OpsReportMudLossess))]
        public virtual OpsReportVolLostAbandonHoles VolLostAbandonHole { get; set; }
        [ForeignKey(nameof(VolLostBhdCsgHoleId))]
        [InverseProperty(nameof(OpsReportVolLostBhdCsgHoles.OpsReportMudLossess))]
        public virtual OpsReportVolLostBhdCsgHoles VolLostBhdCsgHole { get; set; }
        [ForeignKey(nameof(VolLostCircHoleId))]
        [InverseProperty(nameof(OpsReportVolLostCircHoles.OpsReportMudLossess))]
        public virtual OpsReportVolLostCircHoles VolLostCircHole { get; set; }
        [ForeignKey(nameof(VolLostCmtHoleId))]
        [InverseProperty(nameof(OpsReportVolLostCmtHoles.OpsReportMudLossess))]
        public virtual OpsReportVolLostCmtHoles VolLostCmtHole { get; set; }
        [ForeignKey(nameof(VolLostCsgHoleId))]
        [InverseProperty(nameof(OpsReportVolLostCsgHoles.OpsReportMudLossess))]
        public virtual OpsReportVolLostCsgHoles VolLostCsgHole { get; set; }
        [ForeignKey(nameof(VolLostMudCleanerSurfId))]
        [InverseProperty(nameof(OpsReportVolLostMudCleanerSurfs.OpsReportMudLossess))]
        public virtual OpsReportVolLostMudCleanerSurfs VolLostMudCleanerSurf { get; set; }
        [ForeignKey(nameof(VolLostOtherHoleId))]
        [InverseProperty(nameof(OpsReportVolLostOtherHoles.OpsReportMudLossess))]
        public virtual OpsReportVolLostOtherHoles VolLostOtherHole { get; set; }
        [ForeignKey(nameof(VolLostOtherSurfId))]
        [InverseProperty(nameof(OpsReportVolLostOtherSurfs.OpsReportMudLossess))]
        public virtual OpsReportVolLostOtherSurfs VolLostOtherSurf { get; set; }
        [ForeignKey(nameof(VolLostPitsSurfId))]
        [InverseProperty(nameof(OpsReportVolLostPitsSurfs.OpsReportMudLossess))]
        public virtual OpsReportVolLostPitsSurfs VolLostPitsSurf { get; set; }
        [ForeignKey(nameof(VolLostShakerSurfId))]
        [InverseProperty(nameof(OpsReportVolLostShakerSurfs.OpsReportMudLossess))]
        public virtual OpsReportVolLostShakerSurfs VolLostShakerSurf { get; set; }
        [ForeignKey(nameof(VolLostTrippingSurfId))]
        [InverseProperty(nameof(OpsReportVolLostTrippingSurfs.OpsReportMudLossess))]
        public virtual OpsReportVolLostTrippingSurfs VolLostTrippingSurf { get; set; }
        [ForeignKey(nameof(VolTotMudLostHoleId))]
        [InverseProperty(nameof(OpsReportVolTotMudLostHoles.OpsReportMudLossess))]
        public virtual OpsReportVolTotMudLostHoles VolTotMudLostHole { get; set; }
        [ForeignKey(nameof(VolTotMudLostSurfId))]
        [InverseProperty(nameof(OpsReportVolTotMudLostSurfs.OpsReportMudLossess))]
        public virtual OpsReportVolTotMudLostSurfs VolTotMudLostSurf { get; set; }
        [InverseProperty("MudLosses")]
        public virtual ICollection<OpsReportMudVolumes> OpsReportMudVolumes { get; set; }
    }
}
