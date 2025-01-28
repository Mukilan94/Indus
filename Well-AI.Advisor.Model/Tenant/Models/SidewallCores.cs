using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class SidewallCores
    {
        [Key]
        public string Uid { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        [Column("DTimToolRun")]
        public string DtimToolRun { get; set; }
        [Column("DTimToolPull")]
        public string DtimToolPull { get; set; }
        public string MdToolReferenceUom { get; set; }
        public string CoreReferenceLog { get; set; }
        public string MdCoreUom { get; set; }
        public string ServiceCompany { get; set; }
        public string AnalysisContractor { get; set; }
        public string AnalysisBy { get; set; }
        public string SidewallCoringTool { get; set; }
        public string DiaHoleUom { get; set; }
        public string DiaPlugUom { get; set; }
        public string NumPlugsShot { get; set; }
        public string NumRecPlugs { get; set; }
        public string NumMisfiredPlugs { get; set; }
        public string NumEmptyPlugs { get; set; }
        public string NumLostPlugs { get; set; }
        public string NumPaidPlugs { get; set; }
        public string SwcSampleUid { get; set; }
        public int? CommonDataSidewallCoresCommonDataid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataSidewallCoresCommonDataid))]
        [InverseProperty(nameof(SideWallCoreCommonData.SidewallCores))]
        public virtual SideWallCoreCommonData CommonDataSidewallCoresCommonData { get; set; }
        [ForeignKey(nameof(DiaHoleUom))]
        [InverseProperty(nameof(SideWallCoreDiaHole.SidewallCores))]
        public virtual SideWallCoreDiaHole DiaHoleUomNavigation { get; set; }
        [ForeignKey(nameof(DiaPlugUom))]
        [InverseProperty(nameof(DiaPlug.SidewallCores))]
        public virtual DiaPlug DiaPlugUomNavigation { get; set; }
        [ForeignKey(nameof(MdCoreUom))]
        [InverseProperty(nameof(SideWallMdCore.SidewallCores))]
        public virtual SideWallMdCore MdCoreUomNavigation { get; set; }
        [ForeignKey(nameof(MdToolReferenceUom))]
        [InverseProperty(nameof(SideWallMdToolReference.SidewallCores))]
        public virtual SideWallMdToolReference MdToolReferenceUomNavigation { get; set; }
        [ForeignKey(nameof(SwcSampleUid))]
        [InverseProperty(nameof(SideWallCoreSwcSample.SidewallCores))]
        public virtual SideWallCoreSwcSample SwcSampleU { get; set; }
    }
}
