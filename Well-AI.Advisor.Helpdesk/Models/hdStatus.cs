using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Well_AI.Helpdesk.Models
{
    [Table("hdStatus")]
    public class hdStatus
    {
        [Key]
        [Required]
        public int StatusID { get; set; }
        public int InstanceID { get; set; }
        [Column("Name")]
        [Display(Name = "status")]
        public string status { get; set; }
        public string ButtonCaption { get; set; }
        public bool  StopTimeSpent { get; set; }
        public byte AccessType { get; set; }
      
    }
}
