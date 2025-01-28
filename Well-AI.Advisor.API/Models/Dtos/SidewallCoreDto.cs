   /* 
    Licensed under the Apache License, Version 2.0
    
    http://www.apache.org/licenses/LICENSE-2.0
    */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Well_AI.Advisor.API.Dtos
{
    public class MdToolReferenceDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class MdCoreDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class SideWallCoreDiaHoleDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class DiaPlugDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class SideWallMdDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class SideWallCoreLithPcDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class SideWallCoreDensShaleDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class SideWallCoreAbundanceDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class SideWallCoreQualifierDto
    {
        [Key]
        public string Uid { get; set; }
        public string Type { get; set; }
        public SideWallCoreAbundanceDto Abundance { get; set; }
        public string AbundanceCode { get; set; }
        public string Description { get; set; }

    }

    public class SideWallCoreLithologyDto
    {
        [Key]
        public string Uid { get; set; }
        public string Type { get; set; }
        public string CodeLith { get; set; }
        public SideWallCoreLithPcDto LithPc { get; set; }
        public string Description { get; set; }
        public string LithClass { get; set; }
        public string GrainType { get; set; }
        public string DunhamClass { get; set; }
        public string Color { get; set; }
        public string Texture { get; set; }
        public string Hardness { get; set; }
        public string SizeGrain { get; set; }
        public string Roundness { get; set; }
        public string Sorting { get; set; }
        public string MatrixCement { get; set; }
        public string PorosityVisible { get; set; }
        public string Permeability { get; set; }
        public SideWallCoreDensShaleDto DensShale { get; set; }
        public SideWallCoreQualifierDto Qualifier { get; set; }

    }

    public class SideWallCoreStainPcDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class SideWallCoreNatFlorPcDto
    {
        [Key]
        public string Uom { get; set; }

        public string Text { get; set; }
    }

    public class SideWallCoreShowDto
    {
        [Key]
        public int SideWallCoreId { get; set; }
        public string ShowRat { get; set; }
        public string StainColor { get; set; }
        public string StainDistr { get; set; }
        public SideWallCoreStainPcDto StainPc { get; set; }
        public string NatFlorColor { get; set; }
        public SideWallCoreNatFlorPcDto NatFlorPc { get; set; }
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
    }

    public class SideWallCoreSwcSampleDto
    {
        [Key]
        public string Uid { get; set; }
        public SideWallMdDto Md { get; set; }
        public SideWallCoreLithologyDto Lithology { get; set; }
        public SideWallCoreShowDto Show { get; set; }
        public string NameFormation { get; set; }
        public string Comments { get; set; }

    }

    public class SideWallCoreCommonDataDto
    {
        [Key]
        public int SidewallCoresCommonDataid { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }
    }

    public class SidewallCoreDto
    {
        [Key]
        public string Uid { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public string DTimToolRun { get; set; }
        public string DTimToolPull { get; set; }
        public MdToolReferenceDto MdToolReference { get; set; }
        public string CoreReferenceLog { get; set; }
        public MdCoreDto MdCore { get; set; }
        public string ServiceCompany { get; set; }
        public string AnalysisContractor { get; set; }
        public string AnalysisBy { get; set; }
        public string SidewallCoringTool { get; set; }
        public SideWallCoreDiaHoleDto DiaHole { get; set; }
        public DiaPlugDto DiaPlug { get; set; }
        public string NumPlugsShot { get; set; }
        public string NumRecPlugs { get; set; }
        public string NumMisfiredPlugs { get; set; }
        public string NumEmptyPlugs { get; set; }
        public string NumLostPlugs { get; set; }
        public string NumPaidPlugs { get; set; }
        public SideWallCoreSwcSampleDto SwcSample { get; set; }
        public SideWallCoreCommonDataDto CommonData { get; set; }

        public string UidWellbore { get; set; }
        public string UidWell { get; set; }
    }
 
}
