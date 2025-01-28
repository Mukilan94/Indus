using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SideWallCoreShow
    {
        public SideWallCoreShow()
        {
            SideWallCoreSwcSample = new HashSet<SideWallCoreSwcSample>();
        }

        [Key]
        public int SideWallCoreId { get; set; }
        public string ShowRat { get; set; }
        public string StainColor { get; set; }
        public string StainDistr { get; set; }
        public string StainPcUom { get; set; }
        public string NatFlorColor { get; set; }
        public string NatFlorPcUom { get; set; }
        public string NatFlorLevel { get; set; }
        public string NatFlorDesc { get; set; }
        public string CutColor { get; set; }
        public string CutSpeed { get; set; }
        public string CutStrength { get; set; }
        public string CutForm { get; set; }
        public string CutLevel { get; set; }
        public string CutFlorColor { get; set; }
        public string CutFlorSpeed { get; set; }
        public string CutFlorStrength { get; set; }
        public string CutFlorForm { get; set; }
        public string CutFlorLevel { get; set; }
        public string ResidueColor { get; set; }
        public string ShowDesc { get; set; }
        public string ImpregnatedLitho { get; set; }
        public string Odor { get; set; }

        [ForeignKey(nameof(NatFlorPcUom))]
        [InverseProperty(nameof(SideWallCoreNatFlorPc.SideWallCoreShow))]
        public virtual SideWallCoreNatFlorPc NatFlorPcUomNavigation { get; set; }
        [ForeignKey(nameof(StainPcUom))]
        [InverseProperty(nameof(SideWallCoreStainPc.SideWallCoreShow))]
        public virtual SideWallCoreStainPc StainPcUomNavigation { get; set; }
        [InverseProperty("ShowSideWallCore")]
        public virtual ICollection<SideWallCoreSwcSample> SideWallCoreSwcSample { get; set; }
    }
}
