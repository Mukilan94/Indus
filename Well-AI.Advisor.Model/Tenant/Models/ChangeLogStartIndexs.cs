using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ChangeLogStartIndexs
    {
        public ChangeLogStartIndexs()
        {
            ChangeLogChangeHistory = new HashSet<ChangeLogChangeHistory>();
        }

        [Key]
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("StartIndexUomNavigation")]
        public virtual ICollection<ChangeLogChangeHistory> ChangeLogChangeHistory { get; set; }
    }
}
