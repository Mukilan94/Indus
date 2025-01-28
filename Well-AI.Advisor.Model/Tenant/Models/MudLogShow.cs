using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogShow
    {
        public MudLogShow()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
        }

        [Key]
        public int ShowId { get; set; }
        public string ShowRat { get; set; }
        public string StainColor { get; set; }
        public string StainDistr { get; set; }
        public int? StainPcId { get; set; }
        public string NatFlorColor { get; set; }
        public int? NatFlorPcId { get; set; }
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

        [ForeignKey(nameof(NatFlorPcId))]
        [InverseProperty(nameof(MudLogNatFlorPc.MudLogShow))]
        public virtual MudLogNatFlorPc NatFlorPc { get; set; }
        [ForeignKey(nameof(StainPcId))]
        [InverseProperty(nameof(MudLogStainPc.MudLogShow))]
        public virtual MudLogStainPc StainPc { get; set; }
        [InverseProperty("Show")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
    }
}
