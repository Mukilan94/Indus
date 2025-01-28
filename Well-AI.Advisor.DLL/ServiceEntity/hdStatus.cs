using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WellAI.Advisor.DLL.ServiceEntity
{
    [Table("hdStatus")]
    public class hdStatus
    {
        [Key]
        [Required]
        public int StatusID { get; set; }
        public int InstanceID { get; set; }
        public string Name { get; set; }
        public string ButtonCaption { get; set; }
        public bool  StopTimeSpent { get; set; }
        public byte AccessType { get; set; }
      
    }
}
