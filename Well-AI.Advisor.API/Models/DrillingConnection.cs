using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models
{
    public class DrillingConnection
    {
        [Key]
        public int DrillingConnectionId { get; set; }

        [Column("WID")]
        public string WELLID { get; set; }
        [Column("SKNO")]
        public string STKNUM { get; set; }
        [Column("RID")]
        public string RECID { get; set; }
        [Column("SQID")]
        public long SEQID { get; set; }
        [Column("DATE")]
        public long DATE { get; set; }
        [Column("TIME")]
        public long TIME { get; set; }
        [Column("ACTC")]
        public string ACTCOD { get; set; }
        [Column("DCNM")]
        public float DEPTCONM { get; set; }
        [Column("DCNV")]
        public float DEPTCONV { get; set; }
        [Column("DMEA")]
        public float DEPTMEAS { get; set; }
        [Column("DVER")]
        public float DEPTVERT { get; set; }
        [Column("ETBS")]
        public string ETIMEBTS { get; set; }
        [Column("ETSL")]
        public string ETIMESLP { get; set; }
        [Column("ETSB")]
        public string ETIMESTB { get; set; }
        [Column("ETPO")]
        public string ETIMEPOF { get; set; }
        [Column("RSUX")]
        public float RSUX { get; set; }
        [Column("RSDX")]
        public float RSDX { get; set; }
        [Column("HKLX")]
        public float HKLX { get; set; }
        [Column("STWT")]
        public float STRGWT { get; set; }
        [Column("TQMX")]
        public float TORQMUX { get; set; }
        [Column("TQBX")]
        public float TORQBOX { get; set; }
        [Column("SPR1")]
        public float SPARE1 { get; set; }
        [Column("SPR2")]
        public float SPARE2 { get; set; }
        [Column("SPR3")]
        public float SPARE3 { get; set; }
        [Column("SPR4")]
        public float SPARE4 { get; set; }
        [Column("SPR5")]
        public float SPARE5 { get; set; }
    }
}
