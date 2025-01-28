using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class LogDatas
    {
        public LogDatas()
        {
            Logs = new HashSet<Logs>();
        }

        [Key]
        public int LogDataId { get; set; }
        public string MnemonicList { get; set; }
        public string UnitList { get; set; }
        public string Data { get; set; }

        [InverseProperty("LogData")]
        public virtual ICollection<Logs> Logs { get; set; }
    }
}
