using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ChangeLogCommonData
    {
        public ChangeLogCommonData()
        {
            ChangeLogs = new HashSet<ChangeLogs>();
        }

        [Key]
        public int CommonDataId { get; set; }
        [Column("DTimCreation")]
        public DateTime DtimCreation { get; set; }
        [Column("DTimLastChange")]
        public DateTime DtimLastChange { get; set; }

        [InverseProperty("CommonData")]
        public virtual ICollection<ChangeLogs> ChangeLogs { get; set; }
    }
}
