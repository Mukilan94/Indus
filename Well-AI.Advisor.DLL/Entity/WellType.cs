using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellAI.Advisor.Model.Tenant.Models;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WellType")]
    public class WellType
    {
        [Key]
        public string welltype_id { get; set; }
        public string welltype_name { get; set; }
        public string DrillPlanChecklist { get; set; }
    }
}
