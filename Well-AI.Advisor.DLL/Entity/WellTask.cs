using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellAI.Advisor.Model.Tenant.Models;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WellTask")]

    public class WellTask
    {
        [Key]
        public string welltask_id { get; set; }

        public string taskname { get; set; }

        public string welltype_id { get; set; }
    }
}
