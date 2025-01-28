using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models
{
    [Table ("WellDepthData_Stage")]
    public class WellDepthDataViewModel
    {
        [Key]
        [Column ("WellDepthID")]
        public System.Guid WellDepthID { get; set; }
        [Column("WID")]
        public string WELLID { get; set; }
        [Column("RID")]
        public string RECID { get; set; }
        [Column("DATE")]
        public long DATE { get; set; }
        [Column("TIME")]
        public long TIME { get; set; }

        [Column("TENANTID")]
        public string TENANTID { get; set; }

         [Column("DMEA")]
        public float DEPTMEAS { get; set; }
        [Column("DVER")]
        public float DEPTVERT { get; set; }

        [Column("DBTM")]
        public float DEPTBITM { get; set; }
        [Column("DBTV")]
        public float DEPTBITV { get; set; }

        [Column ("ISPROCESSED")]
        public bool IsProcessed { get; set; }

        
    }
}
