using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ChangeLogChangeHistory
    {
        [Key]
        public int ChangeHistoryId { get; set; }
        [Column("DTimChange")]
        public string DtimChange { get; set; }
        public string ChangeType { get; set; }
        public string ChangeInfo { get; set; }
        public string StartIndexUom { get; set; }
        public string EndIndexUom { get; set; }
        public string Mnemonics { get; set; }
        public string ChangeLogUid { get; set; }

        [ForeignKey(nameof(ChangeLogUid))]
        [InverseProperty(nameof(ChangeLogs.ChangeLogChangeHistory))]
        public virtual ChangeLogs ChangeLogU { get; set; }
        [ForeignKey(nameof(EndIndexUom))]
        [InverseProperty(nameof(ChangeLogEndIndexs.ChangeLogChangeHistory))]
        public virtual ChangeLogEndIndexs EndIndexUomNavigation { get; set; }
        [ForeignKey(nameof(StartIndexUom))]
        [InverseProperty(nameof(ChangeLogStartIndexs.ChangeLogChangeHistory))]
        public virtual ChangeLogStartIndexs StartIndexUomNavigation { get; set; }
    }
}
