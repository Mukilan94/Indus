using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models.Dtos
{
    public class DRILLINGDEPTHBASEDDto
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
        [Column("DMEA")]
        public float DEPTMEAS { get; set; }
        [Column("DVER")]
        public float DEPTVERT { get; set; }
        [Column("ROPA")]
        public float ROPA { get; set; }
        [Column("WOBA")]
        public float WOBA { get; set; }
        [Column("HKLA")]
        public float HKLA { get; set; }
        [Column("SPPA")]
        public float SPPA { get; set; }
        [Column("TQA")]
        public float TORQA { get; set; }
        [Column("RPMA")]
        public string RPMA { get; set; }
        [Column("BRVC")]
        public long BTREVC { get; set; }
        [Column("MDIA")]
        public float MDIA { get; set; }
        [Column("ECDT")]
        public float ECDTD { get; set; }
        [Column("MFIA")]
        public float MFIA { get; set; }
        [Column("MFOA")]
        public float MFOA { get; set; }
        [Column("MFOP")]
        public string MFOP { get; set; }
        [Column("TVA")]
        public float TVOLACT { get; set; }
        [Column("CPDI")]
        public float CPDI { get; set; }
        [Column("CPDC")]
        public float CPDC { get; set; }
        [Column("BDTI")]
        public float BTDTIME { get; set; }
        [Column("BDDI")]
        public float BTDDIST { get; set; }
        [Column("DXC")]
        public float DXC { get; set; }
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
        [Column("SPR6")]
        public float SPARE6 { get; set; }
        [Column("SPR7")]
        public float SPARE7 { get; set; }
        [Column("SPR8")]
        public float SPARE8 { get; set; }
        [Column("SPR9")]
        public float SPARE9 { get; set; }
    }
}
