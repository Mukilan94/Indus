using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("erdos_DrillingDepthBased")]
    public partial class ErdosDrillingDepthBased
    {
        [Key]
        [Column("DRILLINGDEPTHBASEDId")]
        public int Drillingdepthbasedid { get; set; }
        [Column("WID")]
        public string Wid { get; set; } = "";
        [Column("SKNO")]
        public string Skno { get; set; } = "";
        [Column("RID")]
        public string Rid { get; set; } = "";
        [Column("SQID")]
        public long? Sqid { get; set; } = 0;
        [Column("DATE")]
        public long? Date { get; set; } = 0;
        [Column("TIME")]
        public long? Time { get; set; } = 0;
        [Column("ACTC")]
        public string Actc { get; set; } = "";
        [Column("DMEA")]
        public float? Dmea { get; set; } = 0;
        [Column("DVER")]
        public float? Dver { get; set; } = 0;
        [Column("ROPA")]
        public float? Ropa { get; set; } = 0;
        [Column("WOBA")]
        public float? Woba { get; set; } = 0;
        [Column("HKLA")]
        public float? Hkla { get; set; } = 0;
        [Column("SPPA")]
        public float? Sppa { get; set; } = 0;
        [Column("TQA")]
        public float? Tqa { get; set; } = 0;
        [Column("RPMA")]
        public string Rpma { get; set; } = "";
        [Column("BRVC")]
        public long? Brvc { get; set; } = 0;
        [Column("MDIA")]
        public float? Mdia { get; set; } = 0;
        [Column("ECDT")]
        public float? Ecdt { get; set; } = 0;
        [Column("MFIA")]
        public float? Mfia { get; set; } = 0;
        [Column("MFOA")]
        public float? Mfoa { get; set; } = 0;
        [Column("MFOP")]
        public string Mfop { get; set; } = "";
        [Column("TVA")]
        public float? Tva { get; set; } = 0;
        [Column("CPDI")]
        public float? Cpdi { get; set; } = 0;
        [Column("CPDC")]
        public float? Cpdc { get; set; } = 0;
        [Column("BDTI")]
        public float? Bdti { get; set; } = 0;
        [Column("BDDI")]
        public float? Bddi { get; set; } = 0;
        [Column("DXC")]
        public float? Dxc { get; set; } = 0;
        [Column("SPR1")]
        public float? Spr1 { get; set; } = 0;
        [Column("SPR2")]
        public float? Spr2 { get; set; } = 0;
        [Column("SPR3")]
        public float? Spr3 { get; set; } = 0;
        [Column("SPR4")]
        public float? Spr4 { get; set; } = 0;
        [Column("SPR5")]
        public float? Spr5 { get; set; } = 0;
        [Column("SPR6")]
        public float? Spr6 { get; set; } = 0;
        [Column("SPR7")]
        public float? Spr7 { get; set; } = 0;
        [Column("SPR8")]
        public float? Spr8 { get; set; } = 0;
        [Column("SPR9")]
        public float? Spr9 { get; set; } = 0;
    }
}
