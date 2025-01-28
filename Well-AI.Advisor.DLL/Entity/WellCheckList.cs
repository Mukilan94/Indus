using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WellCheckList")]
    public class WellCheckList
    {
        [Key]
        [StringLength(40)]
        public string WellChecklistId { get; set; }

        [StringLength(100)]
        public string WellId { get; set; }

        [StringLength(40)]
        public string TenantID { get; set; }

        public string CheckList { get; set; }

        [StringLength(40)]
        public string RigId { get; set; }

    }
}
