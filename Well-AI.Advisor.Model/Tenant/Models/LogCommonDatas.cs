using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class LogCommonDatas
    {
        public LogCommonDatas()
        {
            Logs = new HashSet<Logs>();
        }

        [Key]
        public int LogCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataLogCommonData")]
        public virtual ICollection<Logs> Logs { get; set; }
    }
}
