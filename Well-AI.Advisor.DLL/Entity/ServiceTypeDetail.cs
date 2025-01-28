using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("ServiceTypeDetails")]
    public class ServiceTypeDetail
    {
        [Key]
        public string category_id { get; set; }
        public string subservicetype { get; set; }
        public int servicetypecount { get; set; }
        public int parent_id { get; set; }

    }
}
