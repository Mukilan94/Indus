using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("ServiceStage")]
    public class ServiceStage
    {
        [Key]
        public int StageId { get; set; }

        public string Stage_Type { get; set; }

    }
}
