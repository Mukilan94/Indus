using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("erdos_GeneralTimeBased")]
    public partial class ErdosGeneralTimeBased
    {
        [Key]
        public int GeneralTimeBasedId { get; set; }
        [Column("WID")]
        public string Wid { get; set; }
        [Column("SKNO")]
        public string Skno { get; set; }
        [Column("RID")]
        public string Rid { get; set; }
        [Column("SQID")]
        public long? Sqid { get; set; }
        [Column("DATE")]
        public long? Date { get; set; }
        [Column("TIME")]
        public long? Time { get; set; }
        [Column("ACTC")]
        public string Actc { get; set; }
        [Column("DBTM")]
        public float? Dbtm { get; set; }
        [Column("DBTV")]
        public float? Dbtv { get; set; }
        [Column("DMEA")]
        public float? Dmea { get; set; }
        [Column("DVER")]
        public float? Dver { get; set; }
        [Column("BPOS")]
        public float? Bpos { get; set; }
        [Column("ROPA")]
        public float? Ropa { get; set; }
        [Column("HKLA")]
        public float? Hkla { get; set; }
        [Column("HKLX")]
        public float? Hklx { get; set; }
        [Column("WOBA")]
        public float? Woba { get; set; }
        [Column("WOBX")]
        public float? Wobx { get; set; }
        [Column("TQA")]
        public float? Tqa { get; set; }
        [Column("TQX")]
        public float? Tqx { get; set; }
        [Column("RPMA")]
        public string Rpma { get; set; }
        [Column("SPPA")]
        public float? Sppa { get; set; }
        [Column("CHKP")]
        public float? Chkp { get; set; }
        [Column("SPM1")]
        public string Spm1 { get; set; }
        [Column("SPM2")]
        public string Spm2 { get; set; }
        [Column("SPM3")]
        public string Spm3 { get; set; }
        [Column("TVA")]
        public float? Tva { get; set; }
        [Column("TVCA")]
        public float? Tvca { get; set; }
        [Column("MFOP")]
        public string Mfop { get; set; }
        [Column("MFOA")]
        public float? Mfoa { get; set; }
        [Column("MFIA")]
        public float? Mfia { get; set; }
        [Column("MDOA")]
        public float? Mdoa { get; set; }
        [Column("MDIA")]
        public float? Mdia { get; set; }
        [Column("MTOA")]
        public float? Mtoa { get; set; }
        [Column("MTIA")]
        public float? Mtia { get; set; }
        [Column("MCOA")]
        public float? Mcoa { get; set; }
        [Column("MCIA")]
        public float? Mcia { get; set; }
        [Column("STKC")]
        public long? Stkc { get; set; }
        [Column("LSTK")]
        public string Lstk { get; set; }
        [Column("DRTM")]
        public float? Drtm { get; set; }
        [Column("GASA")]
        public float? Gasa { get; set; }
        [Column("SPR1")]
        public float? Spr1 { get; set; }
        [Column("SPR2")]
        public float? Spr2 { get; set; }
        [Column("SPR3")]
        public float? Spr3 { get; set; }
        [Column("SPR4")]
        public float? Spr4 { get; set; }
        [Column("SPR5")]
        public float? Spr5 { get; set; }
    }
}
