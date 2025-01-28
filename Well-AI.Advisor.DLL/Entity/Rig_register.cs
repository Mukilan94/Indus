using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("rig_register")]
   public class Rig_register
    {
        [Key]
        [StringLength(40)]
        public string Rig_id { get; set; }
    
        public string Rig_Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [StringLength(80)]
        public string TenantID { get; set; }

        public bool isActive { get; set; }


    }
}
