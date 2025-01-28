using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{

    [Table("ServiceTypeHead")]
    public class ServiceTypeHead
    {
        [Key]
        public string servicetype { get; set; }

        public int parent_id { get; set; }

        public int servicetypecount { get; set; }
    }
}
