using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    [Table("erdos_DrillingConnections")]
    public partial class ErdosDrillingConnections
    {
        [Key]
        public int DrillingConnectionId { get; set; }
        [Column("WID")]
        public string Wid { get; set; }
        [Column("SKNO")]
        public string Skno { get; set; }
        [Column("RID")]
        public string Rid { get; set; }
        [Column("SQID")]
        public long Sqid { get; set; }
        [Column("DATE")]
        public long Date { get; set; }
        [Column("TIME")]
        public long? Time { get; set; }
        [Column("ACTC")]
        public string Actc { get; set; }
        [Column("DCNM")]
        public float Dcnm { get; set; }
        [Column("DCNV")]
        public float Dcnv { get; set; }
        [Column("DMEA")]
        public float Dmea { get; set; }
        [Column("DVER")]
        public float? Dver { get; set; }
        [Column("ETBS")]
        public string Etbs { get; set; }
        [Column("ETSL")]
        public string Etsl { get; set; }
        [Column("ETSB")]
        public string Etsb { get; set; }
        [Column("ETPO")]
        public string Etpo { get; set; }
        [Column("RSUX")]
        public float Rsux { get; set; }
        [Column("RSDX")]
        public float Rsdx { get; set; }
        [Column("HKLX")]
        public float Hklx { get; set; }
        [Column("STWT")]
        public float Stwt { get; set; }
        [Column("TQMX")]
        public float Tqmx { get; set; }
        [Column("TQBX")]
        public float Tqbx { get; set; }
        [Column("SPR1")]
        public float Spr1 { get; set; }
        [Column("SPR2")]
        public float Spr2 { get; set; }
        [Column("SPR3")]
        public float Spr3 { get; set; }
        [Column("SPR4")]
        public float Spr4 { get; set; }
        [Column("SPR5")]
        public float Spr5 { get; set; }
    }
}
