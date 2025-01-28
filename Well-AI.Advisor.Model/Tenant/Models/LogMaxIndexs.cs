using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class LogMaxIndexs
    {
        public LogMaxIndexs()
        {
            LogCurveInfos = new HashSet<LogCurveInfos>();
        }

        [Key]
        public int MaxIndexId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MaxIndex")]
        public virtual ICollection<LogCurveInfos> LogCurveInfos { get; set; }
    }
}
