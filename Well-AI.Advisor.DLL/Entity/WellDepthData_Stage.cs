using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WellDepthData_Stage")]
    public class WellDepthDataStage
    {
        [Key]
        public Guid WellDepthID { get; set; }
        public string WID { get; set; }
        public string RID { get; set; }
        public Int64? DATE { get; set; }
        public Int64? TIME { get; set; }
        public string TENANTID { get; set; }
        public Single? DBTM { get; set; }
        public Single? DBTV { get; set; }
        public Single? DMEA { get; set; }
        public Single? DVER { get; set; }
        public bool? ISPROCESSED { get; set; }

    }
}
