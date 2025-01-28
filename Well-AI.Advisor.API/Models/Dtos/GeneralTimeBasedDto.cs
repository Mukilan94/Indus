using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models.Dtos
{
    public class GeneralTimeBasedDto
    {
       

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
        [Column("DBTM")]
        public float DEPTBITM { get; set; }
        [Column("DBTV")]
        public float DEPTBITV { get; set; }
        [Column("DMEA")]
        public float DEPTMEAS { get; set; }
        [Column("DVER")]
        public float DEPTVERT { get; set; }
        [Column("BPOS")]
        public float BLKPOS { get; set; }
        [Column("ROPA")]
        public float ROPA { get; set; }
        [Column("HKLA")]
        public float HKLA { get; set; }
        [Column("HKLX")]
        public float HKLX { get; set; }
        [Column("WOBA")]
        public float WOBA { get; set; }
        [Column("WOBX")]
        public float WOBX { get; set; }
        [Column("TQA")]
        public float TORQA { get; set; }
        [Column("TQX")]
        public float TORQX { get; set; }
        [Column("RPMA")]
        public string RPMA { get; set; }
        [Column("SPPA")]
        public float SPPA { get; set; }
        [Column("CHKP")]
        public float CHKP { get; set; }
        [Column("SPM1")]
        public string SPM1 { get; set; }
        [Column("SPM2")]
        public string SPM2 { get; set; }
        [Column("SPM3")]
        public string SPM3 { get; set; }
        [Column("TVA")]
        public float TVOLACT { get; set; }
        [Column("TVCA")]
        public float TVOLCACT { get; set; }
        [Column("MFOP")]
        public string MFOP { get; set; }
        [Column("MFOA")]
        public float MFOA { get; set; }
        [Column("MFIA")]
        public float MFIA { get; set; }
        [Column("MDOA")]
        public float MDOA { get; set; }
        [Column("MDIA")]
        public float MDIA { get; set; }
        [Column("MTOA")]
        public float MTOA { get; set; }
        [Column("MTIA")]
        public float MTIA { get; set; }
        [Column("MCOA")]
        public float MCOA { get; set; }
        [Column("MCIA")]
        public float MCIA { get; set; }
        [Column("STKC")]
        public long STKC { get; set; }
        [Column("LSTK")]
        public string LAGSTKS { get; set; }
        [Column("DRTM")]
        public float DEPTRETM { get; set; }
        [Column("GASA")]
        public float GASA { get; set; }
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
