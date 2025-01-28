using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ChangeLogs
    {
        public ChangeLogs()
        {
            ChangeLogChangeHistory = new HashSet<ChangeLogChangeHistory>();
        }

        [Key]
        public string Uid { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string NameObject { get; set; }
        public string ObjectType { get; set; }
        public string LastChangeType { get; set; }
        public string LastChangeInfo { get; set; }
        public int? CommonDataId { get; set; }
        public string UidObject { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(CommonDataId))]
        [InverseProperty(nameof(ChangeLogCommonData.ChangeLogs))]
        public virtual ChangeLogCommonData CommonData { get; set; }
        [InverseProperty("ChangeLogU")]
        public virtual ICollection<ChangeLogChangeHistory> ChangeLogChangeHistory { get; set; }
    }
}
